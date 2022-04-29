$projects = Get-ChildItem .\src -recurse "*.csproj" 


foreach($project in $projects)
{
    $referencingProjects = (Get-Content -Path $project.FullName | %{$_ -match "<!--PROJ-START-->"}).Contains($true);
    $referencingPackages = (Get-Content -Path $project.FullName | %{$_ -match "<!--PCKG-START-->"}).Contains($true);

    
    if($referencingProjects)
    {
        # Select Package refereces
        (Get-Content -Path $project.FullName) -replace "<!--PCKG-START", "<!--PCKG-START-->" `
                                                                            -replace "PCKG-END-->",  "<!--PCKG-END-->" `
                                                                            -replace "<!--PROJ-START-->",  "<!--PROJ-START" `
                                                                            -replace "<!--PROJ-END-->",  "PROJ-END-->" `
                                                                            | Set-Content  $project.FullName
    }
}