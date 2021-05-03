param([Parameter(Mandatory=$true)]
      [string]$libraryName,
      [string]$baseDirectory)

$templateDirectory = pwd
cd ..
$baseDirectory = pwd
$libraryDirectory = $baseDirectory.Path + '\' + $libraryName
$templateString = "PlcTemplate"


if($libraryName.Contains(".") -or $libraryName.Contains(" "))
{
    Write-Host "Library name cannot contain '.' or space ' ' "
    return
}



$existingDir = Get-ChildItem $baseDirectory -Directory -Filter $libraryName

if($existingDir.Name -eq $libraryName) 
{   
    Write-Host "Directory '"$libraryName"' already exists."
    return
}



#--------------------------------------------------------------------------
#                       Copy template files 
#--------------------------------------------------------------------------

Copy-Item templateDirectory $baseDirectory



$files = Get-ChildItem $baseDirectory -File -Recurse

#--------------------------------------------------------------------------
#                       Replace file content 
#--------------------------------------------------------------------------

foreach ($file in $files)
{
    $fileContent = Get-Content -Path $file.FullName

    if ($fileContent -match $templateString)
    {
        $newFileContent = $fileContent -replace $templateString, $libraryName
        Set-Content -Path $file.FullName -Value $newFileContent
    }
}

#--------------------------------------------------------------------------
#                       Change file names
#--------------------------------------------------------------------------

foreach ($file in $files)
{
    if ($file -match $templateString)
    {
        $newName = $file.Name -replace $templateString, $libraryName
        Rename-Item -Path $file.FullName -NewName $newName
    }
}


#--------------------------------------------------------------------------
#                       Change diretory names
#--------------------------------------------------------------------------

$directories = Get-ChildItem $baseDirectory -Directory -Recurse

[array]::Reverse($directories)

foreach ($directory in $directories)
{
    if ($directory -match $templateString)
    {
        $newName = $directory.Name -replace $templateString, $libraryName
        Rename-Item -Path $directory.FullName -NewName $newName
    }
}


#--------------------------------------------------------------------------
#                       Clean up
#--------------------------------------------------------------------------

Write-Host "Scaffold for '" $libraryName "' created in " $templateDirectory\$libraryName
Write-Host "Press enter to continue" 