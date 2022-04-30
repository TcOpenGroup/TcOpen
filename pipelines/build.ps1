properties {
  $buildConfig = "Release"
  $isTestingEnabled = $false
  $msbuildVerbosity = "minimal"
  $baseDir  = resolve-path ..
  $solutionDir = "$baseDir\"
  $nugetSource = "https://api.nuget.org/v3/index.json"
  $nugetToken = ([System.Environment]::GetEnvironmentVariable('TcoOpenNugetdotorgPAT'))
  $publishNugets = $false
  $packNugets = $false
  $updateAssemblyInfo = $false
  $gitVersion
  $msbuild = ([System.Environment]::GetEnvironmentVariable('TcoMsbuild'))
  $dotnet = ([System.Environment]::GetEnvironmentVariable('TcoDotnet'))
  $devenv = ([System.Environment]::GetEnvironmentVariable('TcoDevenv'))
  $testTargetAmsId = ([System.Environment]::GetEnvironmentVariable('Tc3Target'))
  $testingStrength = 0
  $doesNeedVs = $true
  }



task default -depends Init, BuildScripts, Start, Clean, Check-Target-License, NugetRestore, CopyInxton, SetPlcCpuCores, GitVersion, `
              OpenVisualStudio, BuildWithInxtonBuilder, Build, CloseVs, CopyPlcLibs, Tests, ClearPackages, CreatePackages, PublishPackages, Finish


FormatTaskName (("="*25) + " [ {0} ] " + ("="*25))

task Init {
  Write-Host "Setting location to $baseDir"
  Set-Location $baseDir  
  $psake.context.Peek().properties
  $properties
}



task BuildScripts -continueOnError {  
  Push-Location .\pipelines
  Import-Module .\tcobuildutils.psm1 -Force -DisableNameChecking 
  Pop-Location
}

task Start {
  #Assert $gitOK "[📩] Please commit your changes before running build"
} 

task Clean {
  Write-Host "Clean obj bin"
  CleanObjBin
  RemoveTcBins
  RemoveTcProjBins
  RemoveGenerated

  if($doesNeedVs)  
  {
    RemoveMeta
  } 

  mkdir .\_Vortex\builder -ErrorAction SilentlyContinue 
  mkdir .\.nuget -ErrorAction SilentlyContinue 
  mkdir .\_toolz -ErrorAction SilentlyContinue 
  mkdir .\nugets -ErrorAction SilentlyContinue
  mkdir .\nugets\dependants -ErrorAction SilentlyContinue
}
 
task Check-Target-License `
  -precondition { $isTestingEnabled } `
{
  if(Is-License-Valid $testTargetAmsId)
  {
    # continue
  }
  else
  {
    throw "Target has probably invalid license"
  }
}

 
task NugetRestore {
   EnsureNuget

   exec{
       & $dotnet restore TcOpen.build.slnf
   }
} 

task CopyInxton -continueOnError {
  exec {
      & $dotnet build `
        .\src\TcoCore\tests\TcoDummyTest\TcoDummyTest.csproj `
        -v:$msbuildVerbosity `
        --nologo `
        /p:SolutionDir=$solutionDir
  }
}

task SetPlcCpuCores `
  -description "Will set 1 shared 1 isolated core to every tsproj" `
  -precondition { Is-AMS-Id-Local $testTargetAmsId } `
{
  $tsProjects = (Get-ChildItem .\ -recurse "*.tsproj" |  % { $_.FullName })
  foreach ($tsProject in $tsProjects)
  {
    Remove-TargetNetId($tsProject)
    if (Has-RT-Isolated-Core $tsProject -or $tsProject.Contains("XAEVortexBase"))
    {
      Write-Host "Skip`t" $tsProject 
    }
    else
    {
      Write-Host "Setting`t" $tsProject
      Set-CPU-Cores $tsProject 1 1
    }
  }
}


task GitVersion `
  -precondition { $updateAssemblyInfo } `
{
  EnsureGitVersion -pathToGitVersion ".\_toolz\gitversion.exe"
  $updateAssemblyInfoFlag = if( $updateAssemblyInfo)  {"/updateassemblyinfo"} else {""}
  $gitVersionOutput = & ".\_toolz\gitversion.exe" "$updateAssemblyInfoFlag" "/config" "$baseDir/GitVersion.yml"
  $gitVersionOutput
  $script:gitVersion =  $gitVersionOutput |  ConvertFrom-Json   
  $buildNumber =$script:gitVersion.SemVer
  Write-Output ("BuildNumber=$buildNumber") >> $env:GITHUB_ENV

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

task OpenVisualStudio {
  # Start-Process .\TcOpen.plc.slnf
}


task BuildWithInxtonBuilder {

  if($doesNeedVs)
  {
    $command = ".\_Vortex\builder\vortex.compiler.console.exe -s .\TcOpen.plc.slnf"
  }
  else
  {
    $command = ".\_Vortex\builder\vortex.compiler.console.exe -s .\TcOpen.plc.slnf -n false"
  }
  Write-Host $command
   exec { 
      try{Get-Process devenv | Stop-Process -ErrorAction SilentlyContinue} catch{}
      if($doesNeedVs)
      {
        Start-Process .\TcOpen.plc.slnf
      }      
      cmd /c $command
      try{Get-Process devenv | Stop-Process -ErrorAction SilentlyContinue}catch{}
  }  -maxRetries 3       
}

task Build {
  #/consoleloggerparameters:ErrorsOnly  
  Write-Host $command
  exec{
     & $msbuild .\TcOpen.build.slnf `
        /p:Configuration=$buildConfig `
        -noWarn:"CS1591;CS0067;CS0108;CS1570" `
        /consoleloggerparameters:ErrorsOnly `
        -v:$msbuildVerbosity    
  } -maxRetries 3  

   $command = "`"$devenv`" .\TcOpen.test.build.plc.slnf /Rebuild " + "`"$buildConfig|TwinCAT RT (x64)`""

    exec{
        cmd /c $command
    }  -maxRetries 2   
}


task CloseVs {
  exec{
   try{Get-Process devenv | Stop-Process -ErrorAction SilentlyContinue} catch{}
  }
}

task CopyPlcLibs {
  $slnfPath = ".\TcOpen.plc.slnf"
  $slnf = Get-Content $slnfPath | ConvertFrom-Json
  $slnfDir = [System.IO.Path]::GetDirectoryName($slnfPath)
  $tcoLibraries = $slnf.solution.projects  `
    | Where-Object { $_.Contains("Connector") }  `
    | Where-Object { -Not $_.Contains("Test") }  `
    | Where-Object { -Not $_.Contains("Example") }  `
    | Where-Object { -Not $_.Contains("Integration") }  `
    | Where-Object { -Not $_.Contains("Hammer") }  `
    | Where-Object { -Not $_.Contains("Template") } `
    | ForEach-Object { [System.IO.Path]::GetDirectoryName( (Join-Path ($slnfDir) $_)) }`
    | ForEach-Object { Join-Path $_ "_meta" }  `
    | ForEach-Object { @(Get-ChildItem $_ -Filter "*.library" )[0].FullName }

  $destinationFolder = ".\plcLibs"
  mkdir $destinationFolder -ErrorAction SilentlyContinue | Out-Null
  foreach($library in $tcoLibraries)
  {
      Copy-Item $library $destinationFolder
  }
}

task Tests -precondition { return $isTestingEnabled } {

    Set-Location $baseDir 

    Write-Host '--------------------------------------------' -ForegroundColor Cyan
    Write-Host 'Building plc projects' -ForegroundColor Cyan
    Write-Host '--------------------------------------------' -ForegroundColor Cyan
            	
    $testProjects = @(
                      [System.Tuple]::Create(".\src\TcOpen.Inxton\TcOpen.Inxton.slnf", "", 0, "TcOpen.Inxton"),                     
                      [System.Tuple]::Create(".\src\TcoCore\TcoCoreUnitTests.slnf", "\src\TcoCore\src\XaeTcoCore\", 0, "TcoCore_L0"),      
                      [System.Tuple]::Create(".\src\TcoCore\TcoCore_L1_Tests.slnf","", 1, "TcoCore_L1"),
                      [System.Tuple]::Create(".\src\TcoCore\TcoCore_L2_Tests.slnf","", 2, "TcoCore_L2"),
                      [System.Tuple]::Create(".\src\TcoUtilities\TcoUtilities.slnf", "\src\TcoUtilities\src\XAE\XAE\", 1, "TcoUtilities"),
                      [System.Tuple]::Create(".\src\TcoInspectors\TcoInspectors.slnf", "\src\TcoInspectors\src\XAE\XAE\", 1, "TcoInspectors"),                      
                      [System.Tuple]::Create(".\src\TcoElements\TcoElements.slnf", ".\src\TcoElements\src\XAE\XAE\", 1, "TcoElements"),
                      [System.Tuple]::Create(".\src\TcoIo\TcoIo.slnf", "src\TcoIo\src\XAE\XAE\", 1, "TcoIo"),
                      [System.Tuple]::Create(".\src\TcoPneumatics\TcoPneumatics.slnf", "\src\TcoPneumatics\src\XaeTcoPneumatics\", 1, "TcoPneumatics"),
                      [System.Tuple]::Create(".\src\TcoDrivesBeckhoff\TcoDrivesBeckhoff.slnf", "\src\TcoDrivesBeckhoff\src\XaeTcoDrivesBeckhoff\", 1, "TcoDrivesBeckhoff"),
                      [System.Tuple]::Create(".\src\TcoData\TcoData.slnf", "\src\TcoData\src\XAE\XAE\", 1, "TcoData"),
                      [System.Tuple]::Create(".\src\IntegrationProjects\IntegrationProjects.slnf", "\src\IntegrationProjects\src\XAE\XAE\", 0, "Integration"),
                      [System.Tuple]::Create(".\src\Serilog.Sinks.MQTT\Serilog.Sinks.MQTT.slnf", "", 1, "Serilog.Sinks.MQTT"),
                      [System.Tuple]::Create(".\src\TcOpenAppTemplates\StandardMtsTemplate\XAE-tests.slnf", "\src\TcOpenAppTemplates\StandardMtsTemplate\src\XAE\", 1, "MTS-Standard-Template")                       
                    )
                    # removed due to missing hardware  
                    # [System.Tuple]::Create(".\src\TcoDrivesBeckhoff\TcoDrivesBeckhoff.slnf", "\src\TcoDrivesBeckhoff\src\XaeTcoDrivesBeckhoff\", -1, "TcoDrivesBeckhoff"),

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
          exec{   
            Start-Sleep 1
            .\pipelines\utils\CleanupTargetBoot.ps1 $testTargetAmsId;
            Start-Sleep 1
            $BootDir = $solutionDir + $xaeProjectFolder;               
            .\pipelines\utils\Load-XaeProject.ps1 $testTargetAmsId $BootDir;                    
            Start-Sleep 1
          }  -maxRetries 2    
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
        } -maxRetries 2    

       
      }
      else {
        Write-Host '--------------------------------------------' -ForegroundColor Yellow
        Write-Host "Skipping tests for $testProjectOrSolution due to testing strenght settings $tier -le $testingStrength" -ForegroundColor Yellow
        Write-Host '--------------------------------------------' -ForegroundColor Yellow
      }    
    }
} 


task ClearPackages `
  -precondition { $publishNugets } `
{
  mkdir nugets -ErrorAction SilentlyContinue
  mkdir nugets\dependants -ErrorAction SilentlyContinue
  Get-ChildItem -Path .\nugets\ -Include *.* -File -Recurse | ForEach-Object { $_.Delete()}
}

task CreatePackages `
  -precondition { $packNugets } `
{
  $semVer = $script:gitVersion.SemVer
  exec { 
      & $dotnet pack TcOpen.build.slnf `
        --output nugets `
        --no-build `
        --no-restore `
        --nologo `
        --verbosity $msbuildVerbosity `
        --configuration  $buildConfig `
        -p:PackageVersion=$semVer `
        /p:SolutionDir=$solutionDir
    }
}

task PublishPackages -precondition {return $publishNugets} {
  Write-Host "About to" 
  PushNugets -folderWithNugets .\nugets -token $nugetToken -source $nugetSource
}

task Finish {
  Write-Host "Done"
} 

