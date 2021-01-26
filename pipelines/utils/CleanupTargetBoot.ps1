param( [Parameter(Mandatory=$True, Position=0, ValueFromPipeline=$false)]
       [string]
       $TargetAmsId)

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

$FileHandle = Send-TcReadWrite -IndexGroup 0x78 -IndexOffset 0x10002 -ReadType UInt32 -ReadLength 4 -WriteValue "C:\Twincat\3.1\Boot\PlcCleanup.bat" -NetId $TargetAmsId -Port 10000 -Force -ErrorAction Ignore 

#Write commands into the cleanup file
$CmdList = New-Object Collections.Generic.List[String]
$CmdList.Add("rmdir /q /s C:\Twincat\3.1\Boot\CurrentConfig")
$CmdList.Add("rmdir /q /s C:\Twincat\3.1\Boot\Plc")
$CmdList.Add("mkdir C:\Twincat\3.1\Boot\Plc")
$CmdList.Add("del C:\Twincat\3.1\Boot\*.* /F /Q")


try {
    $CmdList | ForEach-Object{
        $CmdLine = $_ + "`r`n"
        $WriteLength = $CmdLine.Length+1
        Send-TcReadWrite -IndexGroup 0x7f -IndexOffset $FileHandle -WriteValue $CmdLine -WriteLength $WriteLength -ReadLength 4 -ReadType UInt32 -NetId $TargetAmsId -Port 10000  -Force -ErrorAction Ignore 
    }

}
catch {
        Write-Host "Error writing file:"
        Write-Host $_
}
finally {
    # Close cleanup file on target
    Send-TcReadWrite -IndexGroup 0x79 -IndexOffset $FileHandle -ReadLength 4 -ReadType UInt32 -WriteValue "" -WriteLength 1 -NetId $TargetAmsId -Port 10000 -Force -ErrorAction Ignore 
    Write-Host "Cleanup file has been created and closed on target."
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
