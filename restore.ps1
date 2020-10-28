mkdir _toolz -ErrorAction SilentlyContinue
mkdir nugets -ErrorAction SilentlyContinue
$gitVersion = Get-ChildItem .\_toolz\ -Filter 'gitversion.exe'


if($gitVersion.Exists)
{
    Write-Host 'Git version is already retrieved.'
    return;
}


Function DeGZip-File{
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


Write-Host "Dowloading GitVersion..."
Invoke-WebRequest -Uri https://github.com/GitTools/GitVersion/releases/download/5.3.7/gitversion-win-x64-5.3.7.tar.gz  -OutFile .\_toolz\gitversion-win-x64-5.3.7.tar.gz
Write-Host "Dowloading GitVersion done"

Write-Host "Upacking GitVersion..."
DeGZip-File .\_toolz\gitversion-win-x64-5.3.7.tar.gz -DestinationPath .\_toolz\

if (-not (Get-Command Expand-7Zip -ErrorAction Ignore)) {
  Install-Package -Scope CurrentUser -Force 7Zip4PowerShell > $null
}

Expand-7Zip .\_toolz\gitversion-win-x64-5.3.7.tar .\_toolz\

Remove-Item .\_toolz\gitversion-win-x64-5.3.7.tar.gz
Remove-Item .\_toolz\gitversion-win-x64-5.3.7.tar