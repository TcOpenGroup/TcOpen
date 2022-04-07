Param([Parameter(Mandatory=$true)][string]$newCuName)

function Copy-Template($newName)
{
    $templateFolder = "CU00x"
    Copy-Item  $templateFolder $newName -Recurse
}

function Rename-Files($newName)
{
    Get-ChildItem -File -Recurse | Rename-Item -NewName { $_.Name -Replace "CU00x", $newName }
}

function Rename-Function-Blocks($newName)
{
    $files = Get-ChildItem -File -Recurse 
    foreach($file in $files)
    {
        $newContent = (Get-Content $file.FullName ).Replace("CU00x",$newName) 
        Set-Content $file.FullName $newContent
    }
}

function Remove-Tc-Id
{
    $files = Get-ChildItem -File -Recurse 
    foreach($file in $files)
    {
        $regex = 'Id=\"{.+}\"'

        $newContent = (Get-Content $file.FullName ) -Replace($regex,"") 
        Set-Content $file.FullName $newContent
    }
}

function Add-ProcessData-Instance($name, $processDataDutPath)
{
    $processDataTemplate = "`n`t`tCU00x : CU00xProcessData := (Parent := THISSTRUCT);"
    $newProcessData = $processDataTemplate.Replace("CU00x",$name)
    $startOfProcessDataRegion = (Get-Content $processDataDutPath | Select-String "END_STRUCT" ).LineNumber
    $processDataDUT = Get-Content $processDataDutPath
    $processDataDUT[$startOfProcessDataRegion -2] += $newProcessData
    Set-Content $processDataDutPath $processDataDUT
}

function Add-TechnologyData-Instance($name, $processDataDutPath)
{
    $techDataTemplate = "`n`t`tCU00x : CU00xTechnologicalData := (Parent := THISSTRUCT);"
    $newProcessData = $techDataTemplate.Replace("CU00x",$name)
    $startOfProcessDataRegion = (Get-Content $processDataDutPath | Select-String "END_STRUCT" ).LineNumber
    $processDataDUT = Get-Content $processDataDutPath
    $processDataDUT[$startOfProcessDataRegion -2] += $newProcessData
    Set-Content $processDataDutPath $processDataDUT
}

function Add-Cu-Folder($name, $plcProjPath)
{
    $plcProjContent = Get-Content $plcProjPath
    $folderDefinition = '<Folder Include="Technology\PLACEHOLDER" />'.Replace("PLACEHOLDER",$name)    
    $alreadyExists =  $plcProjContent | Select-String $folderDefinition -SimpleMatch
    if($alreadyExists)
    {
        Write-Host 'Folder exists'
        return
    }
    $technologyFolderLineNumer = ($plcProjContent | Select-String '<Folder Include="Technology" />').LineNumber

    $plcProjContent[$technologyFolderLineNumer+1] += $folderDefinition
    Set-Content $plcProjPath $plcProjContent
}

function Link-Files-With-Project($name, $plcProjPath)
{
    $plcProjContent = Get-Content $plcProjPath
    $technologyNode = '<Compile Include="Technology\Technology.TcPOU">'   
    $technologyNodeLineNumber = ($plcProjContent | Select-String $technologyNode -SimpleMatch).LineNumber
    $template =@"
    <Compile Include="Technology\PLACEHOLDER">
      <SubType>Code</SubType>
    </Compile>
"@
    $filesToAdd = Get-ChildItem $name -Recurse -File | Resolve-Path -Relative 
    $toAdd = ""
    foreach($file in $filesToAdd)
    {
        $toAdd += $template.Replace("PLACEHOLDER", $file.Replace(".\","") )
    }
    $plcProjContent[$technologyNodeLineNumber-2] += $toAdd
    Set-Content $plcProjPath $plcProjContent
}


function Create-New-Controlled-Unit($name)
{
   Push-Location ".\src\XAE\MainPlc\Technology"   
   Copy-Template $name
   Push-Location $name  
   Rename-Files $name
   Rename-Function-Blocks $name
   Remove-Tc-Id 
   Pop-Location
   Add-ProcessData-Instance $name ((Get-Item ".\Data\ProcessData.TcDUT").FullName)
   Add-TechnologyData-Instance $name ((Get-Item ".\Data\TechnologyData.TcDUT").FullName)
   Add-Cu-Folder $name ((Get-Item ".\..\MainPlc.plcproj").FullName)
   Link-Files-With-Project $name ((Get-Item ".\..\MainPlc.plcproj").FullName)
   Pop-Location
}


Create-New-Controlled-Unit $newCuName