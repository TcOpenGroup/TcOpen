$nugetToken = ([System.Environment]::GetEnvironmentVariable('Tco__Nuget_dot_org_PAT'))

.\_toolz\gitversion.exe /updateassemblyinfo

$gitVersionInfo = .\_toolz\gitversion.exe | ConvertFrom-Json 
$semVer = $gitVersionInfo.SemVer;
$plcVersion = $gitVersionInfo.Major.ToString() + "." +  $gitVersionInfo.Minor.ToString() + "." + $gitVersionInfo.Patch.ToString() + "." +  $gitVersionInfo.PreReleaseNumber;

.\_Vortex\builder\uvn.exe -v $plcVersion 

Start-Process .\XaeTcOpen.sln


.\_Vortex\builder\vortex.compiler.console.exe -s .\TcoPneumatics\TcoPneumatics.slnf
.\_Vortex\builder\vortex.compiler.console.exe -s .\TcoIoBeckhoff\TcoIoBeckhoff.slnf


$nugets = Get-ChildItem -Path nugets\
foreach($nuget in $nugets)
{   
    Remove-Item $nuget.FullName
}


dotnet pack .\TcoIoBeckhoff\src\connectors\TcoIoBeckhoffConnector\TcoIoBeckhoffConnector.csproj -p:PackageVersion=$semVer --output nugets -c Release
dotnet pack .\TcoIoBeckhoff\src\presentation\wpf\TcoIoBeckhoff.Wpf\TcoIoBeckhoff.Wpf.csproj -p:PackageVersion=$semVer --output nugets -c Release

dotnet pack .\TcoPneumatics\src\connectors\TcoPneumaticsConnector\TcoPneumaticsConnector.csproj -p:PackageVersion=$semVer --output nugets -c Release
dotnet pack .\TcoPneumatics\src\presentation\wpf\TcoPneumatics.Wpf\TcoPneumatics.Wpf.csproj -p:PackageVersion=$semVer --output nugets -c Release


$nugets = Get-ChildItem -Path nugets\

foreach($nuget in $nugets)
{       
    dotnet nuget push $nuget.FullName --source 'nuget.org' -k API_KEY_REGEN
}


