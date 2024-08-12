.\pipelines\runbuild.ps1 -properties @{
  "buildConfig" = "Release";
  "updateAssemblyInfo" = $false;
  "testingStrength" = 1;
  "isTestingEnabled" = $false;
  "packNugets" = $true;
  "publishNugets" = $false;
  "nugetToken" = ([System.Environment]::GetEnvironmentVariable('gh-packages'));
  "nugetSource" = ([System.Environment]::GetEnvironmentVariable('tc-open-gh-packages'));
  "msbuild" = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe";
  "dotnet" = "C:\Program Files\dotnet\dotnet.exe";
  "devenv" = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.com"
  }
