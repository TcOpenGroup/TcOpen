properties {
  $buildConfig = "Release"
  $isTestingEnabled = $false
  $msbuildVerbosity = "minimal"
  $baseDir  = resolve-path ..
  $solutionDir = "$baseDir\"
  $nugetSource = "https://api.nuget.org/v3/index.json"
  $nugetToken = ([System.Environment]::GetEnvironmentVariable('TcoOpenNugetdotorgPAT'))
  $publishNugets = $false
  $updateAssemblyInfo = $false
  $gitVersion
  $msbuild = '"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"'
  $dotnet = "C:\Program Files\dotnet\dotnet.exe"
  $devenv = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.com"
  $testTargetAmsId = ([System.Environment]::GetEnvironmentVariable('Tc3Target'))
  $testingStrength = 0
}

task default -depends CloseVs, Tests, CreatePackages, Finish


FormatTaskName (("="*25) + " [ {0} ] " + ("="*25))

task Init {
  Write-Host "Setting location to $baseDir"
  Set-Location $baseDir  
  $psake.context.Peek().properties
  $properties
}

task BuildScripts -continueOnError -depends Init {
  Push-Location .\pipelines
  Import-Module .\tcobuildutils.psm1 -Force -Verbose 
  Pop-Location
}

task Start -depends BuildScripts {
  #Assert $gitOK "[📩] Please commit your changes before running build"
} 

task Clean -depends Start {
  Write-Host "Clean obj bin"
  CleanObjBin
  RemoveTcBins
  RemoveTcProjBins
  RemoveGenerated
  RemoveMeta
  mkdir .\_Vortex\builder -ErrorAction SilentlyContinue 
  mkdir .\.nuget -ErrorAction SilentlyContinue 
  mkdir .\_toolz -ErrorAction SilentlyContinue 
  mkdir .\nugets -ErrorAction SilentlyContinue
  mkdir .\nugets\dependants -ErrorAction SilentlyContinue
}
 
task NugetRestore -depends Clean {
   EnsureNuget

   exec{
       & $dotnet restore TcOpen.build.slnf
   }
   
   try {
      # Try to Restore IVC into _Vortex directory
      # May fail due to missing g.cs files
      $command = $msbuild + " -v:$msbuildVerbosity /consoleloggerparameters:ErrorsOnly src\TcoCore\TcoCore.slnf"
      Write-Host $command
      cmd /c $command
   }
   catch {
     # Swallow
   }
      
} 

task GitVersion -depends NugetRestore {
  EnsureGitVersion -pathToGitVersion ".\_toolz\gitversion.exe"
  $updateAssemblyInfoFlag = if( $updateAssemblyInfo)  {"/updateassemblyinfo"} else {""}
  $script:gitVersion =  & ".\_toolz\gitversion.exe" "$updateAssemblyInfoFlag" "/nofetch" "/config" "$baseDir/GitVersion.yml" |  ConvertFrom-Json 
  $buildNumber =$script:gitVersion.SemVer

  if($script:gitVersion.BuildMetaData -ne ""){
    $v= $script:gitVersion.BuildMetaData;
    Write-Host "##vso[build.updatebuildnumber]$buildNumber-$v"
  }
  else {
    Write-Host "##vso[build.updatebuildnumber]$buildNumber"
  }
  
  if($script:gitVersion.BuildMetaData -ne "" -and $script:gitVersion.PreReleaseTag -ne ""){
    $plcversion = $script:gitVersion.Major.ToString() + "." + $script:gitVersion.Minor.ToString() + "." + $script:gitVersion.Patch.ToString() +"." + $script:gitVersion.BuildMetaData.ToString()
  }
  else {
    if($script:gitVersion.PreReleaseNumber -ne "")
    {
      $plcversion = $script:gitVersion.Major.ToString() + "." + $script:gitVersion.Minor.ToString() + "." + $script:gitVersion.Patch.ToString() +"." + $script:gitVersion.PreReleaseNumber.ToString()
    }
    else
    {
      $plcversion = $script:gitVersion.Major.ToString() + "." + $script:gitVersion.Minor.ToString() + "." + $script:gitVersion.Patch.ToString() +".0"
    }
  }

  #(GitVersionToPlcVersion $script:gitVersion)
  if( $updateAssemblyInfo){.\_Vortex\builder\uvn.exe -v $plcversion}
}

task OpenVisualStudio -depends GitVersion {
  Start-Process .\TcOpen.plc.slnf
}


task BuildWithInxtonBuilder -depends OpenVisualStudio {
  $projects = @(  
     "src\Tc.Prober\Tc.Prober.slnf",   
     "src\TcoCore\TcoCore.slnf",
     "src\TcoIoBeckhoff\TcoIoBeckhoff.slnf",
     "src\TcoPneumatics\TcoPneumatics.slnf"
     "src\TcoApplicationExamples\TcoApplicationExamples.slnf"
   )

   foreach($project in $projects)
  {   
    $command = ".\_Vortex\builder\vortex.compiler.console.exe -s " + $project
    Write-Host $command
    exec { 
      cmd /c $command
    }       
  }

}

task Build -depends BuildWithInxtonBuilder {
  #/consoleloggerparameters:ErrorsOnly  
   $command = $msbuild + " /p:Configuration=$buildConfig -noWarn:CS1591;CS0067;CS0108;CS1570 /consoleloggerparameters:ErrorsOnly  -v:$msbuildVerbosity  .\TcOpen.build.slnf"
  Write-Host $command
  exec{
      cmd /c $command
  }
}


task CloseVs -depends Build {
  exec{
    Get-Process devenv | Stop-Process -ErrorAction SilentlyContinue
  }
}


task Tests -depends CloseVs  -precondition { return $isTestingEnabled } {

    Set-Location $baseDir 

    Write-Host '--------------------------------------------' -ForegroundColor Cyan
    Write-Host 'Building plc projects' -ForegroundColor Cyan
    Write-Host '--------------------------------------------' -ForegroundColor Cyan

    $command = "`"$devenv`" .\TcOpen.plc.slnf /Rebuild " + "`"$buildConfig|TwinCAT RT (x64)`""
      
    exec{
        cmd /c $command
    }
        	
    $testProjects = @(                    
                      [System.Tuple]::Create(".\src\TcoCore\TcoCore.slnf", "\src\TcoCore\src\XaeTcoCore\", -1, "TcoCore"),
                      [System.Tuple]::Create(".\src\TcoIoBeckhoff\TcoIoBeckhoff.slnf", "\src\TcoIoBeckhoff\src\XaeTcoIoBeckhoff\", -1, "TcoIoBeckhoff"),
                      [System.Tuple]::Create(".\src\TcoPneumatics\TcoPneumatics.slnf", "src\TcoPneumatics\src\XaeTcoPneumatics\", -1, "TcoPneumatics")                     
                    )
                      


    Write-Host "Running tests up to strength $testingStrength"

    foreach ($testProject in $testProjects) 
    {  
      $testProjectOrSolution =  $testProject.Item1;  
      $xaeProjectFolder =  $testProject.Item2;           
      $tier =  $testProject.Item3;
      $projName = $testProject.Item4;


      if($tier -le $testingStrength)
      {      
        Write-Host '--------------------------------------------' -ForegroundColor Cyan
        Write-Host "Running test for $testProjectOrSolution | plc: $xaeProjectFolder" -ForegroundColor Cyan
        Write-Host '--------------------------------------------' -ForegroundColor Cyan

       
        if($xaeProjectFolder -ne "")
        {
          Start-Sleep 5
          .\pipelines\utils\CleanupTargetBoot.ps1 $testTargetAmsId;
          Start-Sleep 5
          $BootDir = $solutionDir + $xaeProjectFolder;               
          .\pipelines\utils\Load-XaeProject.ps1 $testTargetAmsId $BootDir;                    
          Start-Sleep 5
        }


        $LogFileName = "TEST-$projName.xml"
        exec{   
          & $dotnet test $testProjectOrSolution `
            -c $buildConfig `
            -f net48 `
            -v $msbuildVerbosity `
            -l:"trx;LogFileName=$LogFileName" `
            --no-build `
            --no-restore
        }

       
      }
      else {
        Write-Host '--------------------------------------------' -ForegroundColor Yellow
        Write-Host "Skipping tests for $testProjectOrSolution due to testing strenght settings $tier -le $testingStrength" -ForegroundColor Yellow
        Write-Host '--------------------------------------------' -ForegroundColor Yellow
      }    
    }
} 


task ClearPackages -depends Tests {
  mkdir nugets -ErrorAction SilentlyContinue
  mkdir nugets\dependants -ErrorAction SilentlyContinue
  Get-ChildItem -Path .\nugets\ -Include *.* -File -Recurse | ForEach-Object { $_.Delete()}
}

task CreatePackages -depends ClearPackages {
  $semVer = $script:gitVersion.SemVer
  $projects = @(
    #Packaging
    "src\TcoCore\src\TcoCore.Wpf\TcoCore.Wpf.csproj",
    "src\TcoCore\src\TcoCoreConnector\TcoCoreConnector.csproj",
    "src\TcoIoBeckhoff\src\TcoIoBeckhoff.Wpf\TcoIoBeckhoff.Wpf.csproj",
    "src\TcoIoBeckhoff\src\TcoIoBeckhoffConnector\TcoIoBeckhoffConnector.csproj",
    "src\TcoPneumatics\src\TcoPneumatics.Wpf\TcoPneumatics.Wpf.csproj",
    "src\TcoPneumatics\src\TcoPneumaticsConnector\TcoPneumaticsConnector.csproj"   
  )
  foreach($project in $projects)
  {  
    exec { 
       & $dotnet pack $project -p:PackageVersion=$semVer --output nugets -c $buildConfig /p:SolutionDir=$solutionDir -v $msbuildVerbosity --no-restore --no-build
    }
  }
}

task PublishPackages -depends CreatePackages -precondition {return $publishNugets} {
  Write-Host "About to" 
  PushNugets -folderWithNugets .\nugets -token $nugetToken -source $nugetSource
}

task Finish -depends PublishPackages {
  Write-Host "Done"
} 

