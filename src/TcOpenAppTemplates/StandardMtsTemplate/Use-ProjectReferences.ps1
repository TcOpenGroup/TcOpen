
$projects = Get-ChildItem .\src -recurse "*.csproj" 


foreach($project in $projects)
{
    $referencingProjects = (Get-Content -Path $project.FullName | %{$_ -match "<!--PROJ-START-->"}).Contains($true);
    $referencingPackages = (Get-Content -Path $project.FullName | %{$_ -match "<!--PCKG-START-->"}).Contains($true);


    # Select Project refereces
    if($referencingPackages)
    {
        (Get-Content -Path $project.FullName) -replace "<!--PROJ-START", "<!--PROJ-START-->" `
                                                                            -replace "PROJ-END-->",  "<!--PROJ-END-->" `
                                                                            -replace "<!--PCKG-START-->",  "<!--PCKG-START" `
                                                                            -replace "<!--PCKG-END-->",  "PCKG-END-->" `
                                                                            | Set-Content  $project.FullName

    }    
}

# Restore projects
foreach($project in $projects)
{
    dotnet restore $project.FullName    
}


