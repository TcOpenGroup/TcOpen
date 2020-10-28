.\_toolz\gitversion.exe /updateassemblyinfo


cd _Vortex
.\builder\vortex.compiler.console.exe -i true
.\builder\vortex.compiler.console.exe -p true
cd ..



$gitVersionInfo = .\_toolz\gitversion.exe | ConvertFrom-Json 
$semVer = $gitVersionInfo.SemVer;


$nugets = Get-ChildItem -Path nugets\
foreach($nuget in $nugets)
{   
    Remove-Item $nuget.FullName
}



dotnet pack .\src\connectors\TcOpenConnector\TcOpenConnector.csproj -p:PackageVersion=$semVer --output nugets -c Release
dotnet pack .\src\presentation\wpf\TcOpen.Wpf\TcOpen.Wpf.csproj -p:PackageVersion=$semVer --output nugets -c Release

$nugets = Get-ChildItem -Path nugets\

foreach($nuget in $nugets)
{   
    dotnet nuget push $nuget.FullName --source 'nuget.org' -k API_KEY_REGEN
}


