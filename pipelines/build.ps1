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
  $dotnet = '"C:\Program Files\dotnet\dotnet.exe"'
  $devenv = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.com"
  $testTargetAmsId = ""
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
  #Push-Location .\ci.scripting
  #.\Build\build.ps1
  #Pop-Location
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
  mkdir .\_Vortex\builder -ErrorAction SilentlyContinue 
  mkdir .\.nuget -ErrorAction SilentlyContinue 
  mkdir .\_toolz -ErrorAction SilentlyContinue 
  mkdir .\nugets -ErrorAction SilentlyContinue
  mkdir .\nugets\dependants -ErrorAction SilentlyContinue
}
 
task NugetRestore -depends Clean {
   EnsureNuget
   $command = $msbuild + " -t:restore /p:Configuration=$buildConfig -v:$msbuildVerbosity /consoleloggerparameters:ErrorsOnly TcOpen.build.slnf"
   Write-Host $command
   exec{
       cmd /c $command
   }
   
   # To Restore IVC into _Vortex directory
   $command = $msbuild + " -v:$msbuildVerbosity /consoleloggerparameters:ErrorsOnly src\TcoCore\TcoCore.slnf"
   Write-Host $command
   exec{
       cmd /c $command
   }   
} 

task GitVersion -depends NugetRestore {
  EnsureGitVersion -pathToGitVersion ".\_toolz\gitversion.exe"
  $updateAssemblyInfoFlag = if( $updateAssemblyInfo)  {"/updateassemblyinfo"} else {""}
  $updateAssemblyInfoFlag
  $script:gitVersion =  & ".\_toolz\gitversion.exe" "$updateAssemblyInfoFlag" "/nofetch" "/config" "$baseDir" |  ConvertFrom-Json 
  $buildNumber =$script:gitVersion.SemVer
  Write-Host "##vso[build.updatebuildnumber]$buildNumber" 
 
  if($script:gitVersion.BuildMetaData -ne "")
  {
    $v = $script:gitVersion.BuildMetaData.ToString();
  }
  else 
  {
    $v = $script:gitVersion.PreReleaseNumber.ToString();
  } 
  $plcversion = $script:gitVersion.Major.ToString() + "." + $script:gitVersion.Minor.ToString() + "." + $script:gitVersion.Patch.ToString() +"." +  $v
  if($updateAssemblyInfo) {.\_Vortex\builder\uvn.exe -v $plcversion}
}

task OpenVisualStudio -depends GitVersion {
  Start-Process .\TcOpen.plc.slnf
}


task BuildWithInxtonBuilder -depends OpenVisualStudio {
  $projects = @(     
     "src\TcoCore\TcoCore.slnf",
     "src\TcoIoBeckhoff\TcoIoBeckhoff.slnf",
     "src\TcoPneumatics\TcoPneumatics.slnf"
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

  & $devenv .\TcOpen.plc.slnf /Rebuild "$buildConfig|TwinCAT RT (x64)"

  $BootDir = $solutionDir +"\src\TcoCore\src\XaeTcoCore\"
  .\pipelines\utils\Load-XaeProject.ps1 $testTargetAmsId $BootDir
  exec{   
    dotnet test .\src\TcoCore\TcoCore.slnf -c $buildConfig -f net48 -v $msbuildVerbosity
  }

  $BootDir = $solutionDir +"\src\TcoIoBeckhoff\src\XaeTcoIoBeckhoff\"
  .\pipelines\utils\Load-XaeProject.ps1 $testTargetAmsId $BootDir
  exec{   
    dotnet test .\src\TcoIoBeckhoff\TcoIoBeckhoff.slnf -c $buildConfig -f net48 -v $msbuildVerbosity
  }

  $BootDir = $solutionDir +"src\TcoPneumatics\src\XaeTcoPneumatics\"
  .\pipelines\utils\Load-XaeProject.ps1 $testTargetAmsId $BootDir
  exec{   
    dotnet test .\src\TcoPneumatics\TcoPneumatics.slnf -c $buildConfig -f net48 -v $msbuildVerbosity
  }
  .\pipelines\utils\CleanupTargetBoot.ps1 $testTargetAmsId

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
    $command = $dotnet + " pack $project -p:PackageVersion=$semVer --output nugets -c $buildConfig /p:SolutionDir=$solutionDir   -v $msbuildVerbosity --no-restore --no-build"
    Write-Host $command
    exec { 
      cmd /c $command
    }
  }
}

task PublishPackages -depends CreatePackages -precondition {return $publishNugets} {
  Write-Host "About to"
  PushNugets -folderWithNugets .\nugets\dependants -token $nugetToken -source $nugetSource
  PushNugets -folderWithNugets .\nugets -token $nugetToken -source $nugetSource
}

task Finish -depends PublishPackages {
  Write-Host "Done"
} 

