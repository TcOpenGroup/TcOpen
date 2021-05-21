# [START] ensure_gitversion.ps1 

function DeGZip-File
{
    Param(
        $infile,
        $outfile = ($infile -replace '\.gz$','')
    )
    $input = New-Object System.IO.FileStream $inFile, ([IO.FileMode]::Open), ([IO.FileAccess]::Read), ([IO.FileShare]::Read)
    $output = New-Object System.IO.FileStream $outFile, ([IO.FileMode]::Create), ([IO.FileAccess]::Write), ([IO.FileShare]::None)
    $gzipStream = New-Object System.IO.Compression.GzipStream $input, ([IO.Compression.CompressionMode]::Decompress)
    $buffer = New-Object byte[](1024)
    while($true){
        $read = $gzipstream.Read($buffer, 0, 1024)
        if ($read -le 0){break}
        $output.Write($buffer, 0, $read)
        }
    $gzipStream.Close()
    $output.Close()
    $input.Close()
}

function EnsureGitVersion {
    param (
        [string] $pathToGitVersion,
        [string] $gitversionUrl = "https://github.com/GitTools/GitVersion/releases/download/5.5.0/gitversion-win-x64-5.5.0.tar.gz"        
    )

   
    
    $gitVersionFolder = Split-Path $pathToGitVersion
    Push-Location $gitVersionFolder
    $gitVersionExecutableFullName = ((Join-Path (pwd) "gitversion.exe"))
    $gitVersionPwd = Get-Location
    Pop-Location

    DownloadIfMissing -folder $gitVersionFolder -url $gitversionUrl 
    
    
    if(Test-Path $gitVersionExecutableFullName)
    {
        Pop-Location
        return "GitVersion exists"
    }
   
    
    $gitVersionFile = Join-Path ($gitVersionPwd) (Split-Path $gitversionUrl -Leaf)
    $tar = $gitVersionFile.Replace(".gz","")

    DeGZip-File -infile $gitVersionFile -outfile $tar
    if (-not (Get-Command Expand-7Zip -ErrorAction Ignore)) {
        Install-Package -Scope CurrentUser -Force 7Zip4PowerShell > $null
    }      
      Expand-7Zip $tar .\_toolz
      Remove-Item $gitVersionFile -ErrorAction Ignore
      Remove-Item $tar -ErrorAction Ignore
      Pop-Location
      return "Downloaded GitVersion"

}

if($gitVersion.Exists)
{
    Write-Host 'Git version is already retrieved.'
    return(0);
}
# [END] ensure_gitversion.ps1 

# [START] download_file.ps1 

function DownloadIfMissing{
    param
    (
        [string] $folder,
        [string] $url 
    )
    $fileName = (Split-Path ($url) -Leaf)
    $absolutePath =  Resolve-Path $folder
    $outFilePath= (Join-Path $absolutePath $fileName)
    if (!(Test-Path $outFilePath))
    {
        Write-Host "Couldn't find $outFilePath"
        Write-Host "Downloading from $url to $outFilePath"
        try 
        {
            (New-Object System.Net.WebClient).DownloadFile($url,  $outFilePath)
            return  "Downloaded from $url to $outFilePath"
        }
        catch {
            return "An error occured while downloading from $url to $outFilePath"
        }
    }
    else
    {
        return "File exists at $folder"
    }
}

function EnsureNuget{
    param
    (
        [string] $pathToNugetFolder = ".\.nuget",
        [string] $nugetExeUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
    )
    DownloadIfMissing -folder $pathToNugetFolder -url $nugetExeUrl
}


function PushNugets
{
    param
    (
       [Parameter(Mandatory)]
       [string] $folderWithNugets,
       [Parameter(Mandatory)]
       [string] $token,
       [Parameter(Mandatory)]
       [string] $source 
    )
    Push-Location $folderWithNugets
    $names = Get-ChildItem -n -file

    foreach($name in $names){
        Write-Output  "Try to push $name"
       try{
          & dotnet nuget push $name -k $token  -s $source  --skip-duplicate
          Write-Output  "OK $name"
       } catch {
          Write-Output $_
          Write-Output "Error while $name"
       } 
    }
    Pop-Location
}
# [END] push_nugets_in_folder.ps1 

# [START] clean.ps1 

# Will clean every bin obj of every csproj in subfolders
function GetRootDirectoryOf{
    param(
        [string] $filter
    )
    return Get-ChildItem -Path .\ -Filter $filter -Recurse -ErrorAction SilentlyContinue | Resolve-path | Split-Path -Parent
}

function CleanObjBin {
    GetRootDirectoryOf "*.csproj" | ForEach-Object { 
        Push-Location ($_)

        $removeFolders = @( ".\obj",".\bin")
        # $removeFolderContent = @(".\_meta\*", ".\_meta\*")

        $folder = $_.Replace("Microsoft.PowerShell.Core\FileSystem::","")

        foreach($item in ($removeFolders)){
            Write-Host "In $folder trying to remove $item"
            Remove-Item -r $item  -ErrorAction SilentlyContinue
        }

        Pop-Location
    }
}

function RemoveTcBins {
    GetRootDirectoryOf "*.tsproj" | ForEach-Object { 
        Push-Location ($_)

        $removeFolders = @( ".\_Boot",".\_Config")
        # $removeFolderContent = @(".\_meta\*", ".\_meta\*")

        $folder = $_.Replace("Microsoft.PowerShell.Core\FileSystem::","")

        foreach($item in ($removeFolders)){
            Write-Host "In $folder trying to remove $item"
            Remove-Item -r $item  -ErrorAction SilentlyContinue
        }

        Pop-Location
    }
}


function RemoveTcProjBins {
    GetRootDirectoryOf "*.plcproj" | ForEach-Object { 
        Push-Location ($_)

        $removeFolders = @( ".\_CompileInfo")
        # $removeFolderContent = @(".\_meta\*", ".\_meta\*")

        $folder = $_.Replace("Microsoft.PowerShell.Core\FileSystem::","")

        foreach($item in ($removeFolders)){
            Write-Host "In $folder trying to remove $item"
            Remove-Item -r $item  -ErrorAction SilentlyContinue
        }

        Pop-Location
    }
}

function RemoveGenerated {
    GetRootDirectoryOf "*.csproj" | ForEach-Object { 
        Push-Location ($_)

        $removeFolders = @( ".\_generated")
        # $removeFolderContent = @(".\_meta\*", ".\_meta\*")

        $folder = $_.Replace("Microsoft.PowerShell.Core\FileSystem::","")

        foreach($item in ($removeFolders)){
            Write-Host "In $folder trying to remove $item"
            Remove-Item -r $item  -ErrorAction SilentlyContinue
        }

        Pop-Location
    }
}

function RemoveMeta {
    GetRootDirectoryOf "*.csproj" | ForEach-Object { 
        Push-Location ($_)

        $removeFolders = @( ".\_meta")
        # $removeFolderContent = @(".\_meta\*", ".\_meta\*")

        $folder = $_.Replace("Microsoft.PowerShell.Core\FileSystem::","")

        foreach($item in ($removeFolders)){
            Write-Host "In $folder trying to remove $item"
            Remove-Item -r $item  -ErrorAction SilentlyContinue
        }

        Pop-Location
    }
}

function GitVersionToPlcVersion{
    param($gitVersion)
    return $gitVersion.Major.ToString() + "." + $gitVersion.Minor.ToString() + "." + $gitVersion.Patch.ToString() + "." + $gitVersion.PreReleaseNumber.ToString()
}

# [END] clean.ps1 

