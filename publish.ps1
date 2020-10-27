.\_toolz\gitversion.exe /updateprojectfiles
$gitVersionInfo = .\_toolz\gitversion.exe | ConvertFrom-Json 
$semVer = $gitVersionInfo.SemVer;

git add .
git commit -m $semVer;
git push

$nugets = Get-ChildItem -Path nugets\
foreach($nuget in $nugets)
{   
    Remove-Item $nuget.FullName
}



dotnet pack .\src\connectors\TcOpenConnector\TcOpenConnector.csproj -p:PackageVersion=$semVer --output nugets
$nugets = Get-ChildItem -Path nugets\

foreach($nuget in $nugets)
{   
    # dotnet nuget push $nuget.FullName --source 'nuget.org'    
}


