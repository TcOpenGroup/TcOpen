param( [Parameter(Mandatory=$True, Position=0, ValueFromPipeline=$false)]
       [string]
       $TargetAmsId, 
       [Parameter(Mandatory=$True, Position=1, ValueFromPipeline=$false)]
       [string]
       $XaeProjectPath
       )

#-------------------------------------------
# Imports
#-------------------------------------------
Import-Module C:\TwinCAT\AdsApi\Powershell\TcXaeMgmt\TcXaeMgmt.psd1

#-------------------------------------------
#Set target state into the config mode
#-------------------------------------------
Write-Host "Switching target '$TargetAmsId' into the Config mode" -ForegroundColor Blue
Set-AdsState -NetId $TargetAmsId -State Config -Force 
$Session = New-TcSession -NetId $TargetAmsId -Port 10000 

#-------------------------------------------
#Create cleanup file on target
#-------------------------------------------
Write-Host "Cleaning up the target's boot directory" 


$FileHandle = $Session | Send-TcReadWrite -IndexGroup 0x78 -IndexOffset 0x10002 -ReadType UInt32 -ReadLength 4 -WriteValue "C:\Twincat\3.1\Boot\PlcCleanup.bat" -Force 

#Write commands into the cleanup file
$CmdList = New-Object Collections.Generic.List[String]
$CmdList.Add("rmdir /q /s C:\Twincat\3.1\Boot\CurrentConfig")
$CmdList.Add("rmdir /q /s C:\Twincat\3.1\Boot\Plc")
$CmdList.Add("mkdir C:\Twincat\3.1\Boot\Plc")
$CmdList.Add("del C:\Twincat\3.1\Boot\*.* /F /Q")

try {
    $CmdList | ForEach-Object{
        $CmdLine = $_ + "`r`n"
        $Retv = $Session | Send-TcReadWrite -IndexGroup 0x7f -IndexOffset $FileHandle -ReadLength 0 -WriteValue $CmdLine -Force -ErrorAction Ignore        
    }

    # Close cleanup file on target
    $Retv = $Session | Send-TcReadWrite -IndexGroup 0x79 -IndexOffset $FileHandle -ReadLength 0 -WriteValue "" -Force -ErrorAction Ignore
}
catch {
    
}

# Prepare data for starting remote process on target

$Enc = [system.Text.Encoding]::UTF8
$Process = $Enc.GetBytes("C:\TwinCAT\3.1\Boot\PlcCleanup.bat") 
$Path = $Enc.GetBytes("C:\TwinCAT\3.1\Boot") 
$Data = new-object byte[] 780
$Data[0] = 0x22
$Data[4] = 0x13
$Offset =12
$Pos = 0
while($Pos -lt $Process.Count)
{
    $Data[$Offset+$Pos] = $Process[$Pos]
    $Pos++
}
$Offset = $Offset + $Pos + 1
$Pos = 0
while($Pos -lt $Path.Count)
{
    $Data[$Offset+$Pos] = $Path[$Pos]
    $Pos++
}

#Start remote process on target
$Retv = $Session | Write-TcValue -IndexGroup 0x1f4 -IndexOffset 0x0 -Value $Data -Force

#-------------------------------------------
#Copying files to the target
#-------------------------------------------
Start-Sleep -Milliseconds 1000

    Write-Host "Uploading boot projects files from '$XaeProjectPath' " 

    $PlcBootPath = Join-Path -Path $XaeProjectPath -ChildPath "_Boot\TwinCAT RT (x64)"
    $PlcProjectPath = Join-Path -Path $PlcBootPath -ChildPath "\Plc"
    $ConfigFiles = Get-ChildItem -Path $PlcBootPath -Name -Filter *.xml
    
    $ConfigFiles| ForEach-Object{
        $FileSource = ($PlcBootPath + "\" + $_).Replace('\\','\')
        $FileDestination = "C:\TwinCAT\3.1\Boot\"+$_
        try {
            Copy-AdsFile -SessionId $Session.Id -Upload -Path $FileSource -Destination $FileDestination -Directory Generic -Force -ErrorAction Ignore    
        }
        catch {
            
        }
        
    }
    
    #Copy files of all PLC projects
    $PlcProjectFiles = Get-ChildItem -Path $PlcProjectPath -Name 
    $PlcProjectFiles| ForEach-Object{
        $FileSource = ($PlcProjectPath + "\" + $_).Replace('\\','\')
        $FileDestination = "C:\TwinCAT\3.1\Boot\Plc\"+$_
        $FileSource
        $FileDestination 
        try {
            Copy-AdsFile -SessionId $Session.Id -Upload -Path $FileSource -Destination $FileDestination -Directory Generic -Force -ErrorAction Ignore
        }
        catch {
            
        }
        
    }
    Close-TcSession -InputObject $session    




#-------------------------------------------
#Set target state into the Run mode
#-------------------------------------------

Write-Host "Switching target '$TargetAmsId' into the Run mode" -ForegroundColor Green
Set-AdsState -NetId $TargetAmsId -State Run -Force 
