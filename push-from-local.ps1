.\pipelines\runbuild.ps1 -properties @{
  "buildConfig" = "Release";
  "updateAssemblyInfo" = $true;
  "testingStrength" = 1;
  "isTestingEnabled" = $false;
  "packNugets" = $true;
  "publishNugets" = $true
  "nugetToken" = ([System.Environment]::GetEnvironmentVariable('gh-packages'));
  "nugetSource" = ([System.Environment]::GetEnvironmentVariable('tc-open-gh-packages'));  
  }