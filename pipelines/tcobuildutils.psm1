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
        Install-Package -Scope CurrentUser -Force 7Zip4PowerShell > $null -ProviderName PowerShellGet
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

# Isolated core functions 

function Has-Isolated-Core($tcproj)
{
    $settingsNode = Select-Xml -Path $tcproj -XPath "TcSmProject/Project/System/Settings"
    return $settingsNode.Node.NonWinCpus -gt 0
}

function Shared-Cores($tcproj)
{
    $settingsNode = Select-Xml -Path $tcproj -XPath "TcSmProject/Project/System/Settings"
    if($settingsNode -eq $null){ return 1 }
    $isolated = $settingsNode.Node.NonWinCpus
    $max = $settingsNode.Node.MaxCpus
    return $max - $isolated
}

function Has-RT-Isolated-Core($tcproj)
{
    $cpuNodes = Select-Xml -Path $tcproj -XPath "TcSmProject/Project/System/Settings/Cpu"
    if($cpuNodes -eq $null){ return $false }
    $firstIsolatedCoreId = Shared-Cores $tcproj 
    foreach($cpuNode in $cpuNodes)
    {
        if($cpuNode.Node.CpuId -ige $firstIsolatedCoreId){ return $true;}
    }
    return $false

}

function Set-CPU-Cores($tcproj, $shared, $isolated)
{
    $xml = [System.Xml.XmlDocument](Get-Content $tcproj);
    $settingsNode = $xml.SelectSingleNode("TcSmProject/Project/System/Settings")
    if($settingsNode -eq $null)
    {
        $settingsNode = $xml.CreateElement('Settings')
        $settingsNode.SetAttribute("MaxCpus", "-1")
        $settingsNode.SetAttribute("NonWinCpus", "-1")
        $xml.SelectSingleNode("TcSmProject/Project/System").PrependChild($settingsNode)
    }
    $settingsNode.MaxCpus= ($shared + $isolated).ToString()
    if(-Not $settingsNode.HasAttribute("NonWinCpus"))
    {
    $settingsNode.SetAttribute("NonWinCpus", "-1")
    }
    $settingsNode.NonWinCpus= ($isolated).ToString()

    $isolatedCoreXmlNode = $xml.CreateElement('Cpu')
    $isolatedCoreXmlNode.SetAttribute("CpuId", ($shared).ToString())
    
    $settingsCpuNodes= $xml.SelectNodes("TcSmProject/Project/System/Settings/Cpu")
    foreach($cpuNode in $settingsCpuNodes)
    {
        $settingsNode.RemoveChild($cpuNode)
    }
    
    $settingsNode.AppendChild($isolatedCoreXmlNode)
    
    $xml.Save($tcproj)
}

function Is-AMS-Id-Local($TargetAmsId)
{
    Import-Module "C:\TwinCAT\AdsApi\Powershell\TcXaeMgmt\TcXaeMgmt.psd1" -Scope Local
    $route2local = Get-AdsRoute -Local
    $LocalAmsId = $route2local.NetId.ToString()
    $IsLocal = -Not $TargetAmsId -or ($TargetAmsId -eq "127.0.0.1.1.1") -or ($TargetAmsId -eq $LocalAmsId)
    return $IsLocal
}

function Remove-TargetNetId($tcproj)
{
    $xml = [System.Xml.XmlDocument](Get-Content $tcproj);
    $projectNode = $xml.SelectSingleNode("TcSmProject/Project")
    if($projectNode.HasAttribute("TargetNetId"))
    {
        $projectNode.RemoveAttribute("TargetNetId")
        $xml.save($tcproj)
    }
}

function Is-License-Valid($targetAmsId)
{
    Import-Module "C:\TwinCAT\AdsApi\Powershell\TcXaeMgmt\TcXaeMgmt.psd1" -Scope Local
    $licenses = Get-TcLicense -Address $targetAmsId  -OrderId TC1200
    $result = $false
    foreach($licence in $licenses )
    {
        $result = $licence.Valid -or $result 
    }
    if ($result)
    {
        return $result
    }
    else
    {
        $licenses
        return $result
    }
}

function GetTwincat3Dir{
    try 
    {
        $TC3DIR = Get-ChildItem -Path Env:\TWINCAT3DIR
    }
    catch 
    {
        Write-Host "Twincat 3 directory not discovered."
    }
    if($TC3DIR.Value)
    {
        return $TC3DIR.Value
    }
    else
    {
        Write-Host "Twincat 3 directory not discovered."
    }
}


function GetInstalledBuilds{
    $TC3DIR = GetTwincat3Dir
    $MajorVersion = -join((get-item $TC3DIR ).Name,".")
    $TC3BASEDIR = -join($TC3DIR ,"Components\Base")
    $sBuilds = (Get-ChildItem -Path $TC3BASEDIR -Filter "Build_*" -Recurse -Directory).Name.Replace("Build_",$MajorVersion)
    $Builds = New-Object Collections.Generic.List[Version]
    foreach ($sBuild in $sBuilds)
    {
        $Build = [System.Version]::Parse($sBuild)
        $Builds.Add($Build)
    }
    return $Builds
}


function GetInstalledTwincatVersion{
    return Get-ItemProperty HKLM:\Software\Wow6432Node\Beckhoff\TwinCAT3\System | Select-Object TcVersion
}


function RemovePinVersion($tsproj)
{
    $xml = [System.Xml.XmlDocument](Get-Content $tsproj);
    $TcSmProjectNode = $xml.SelectSingleNode("TcSmProject")
    if($TcSmProjectNode.HasAttribute("TcVersionFixed"))
    {
        $TcSmProjectNode.RemoveAttribute("TcVersionFixed")
        $xml.save($tsproj)
        Write-Host "Pin version removed in`t" $tsProject 
    }
}

function DisableDevices($tsproj)
{
    $xml = [System.Xml.XmlDocument](Get-Content $tsproj);
    $TcSmProjectNode = $xml.SelectSingleNode("TcSmProject")
    if($TcSmProjectNode.HasAttribute("TcVersionFixed"))
    {
        $TcSmProjectNode.RemoveAttribute("TcVersionFixed")
        $xml.save($tsproj)
        Write-Host "Pinned to version removed in`t" $tsProject 
    }
}
