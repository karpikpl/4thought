# Download the file to a specific location
$clnt = new-object System.Net.WebClient
$workingDir = Get-Location
Write-Host "Working dir " $workingDir

$url = "https://open.kattis.com/download/sampledata?id=" + $args[0]
$file = $workingDir.ToString() + "\sampledata.zip"
$clnt.DownloadFile($url,$file)
Write-Host "File downloaded to " $file

# Unzip the file to specified location
$shell_app=new-object -com shell.application
$zip_file = $shell_app.namespace($file)
$destination = $shell_app.namespace($workingDir.ToString())
$destination.Copyhere($zip_file.items())
Write-Host "Unzipped:" $zip_file

$newSlnName = $args[0]+".sln"

Rename-Item KattisSolution.sln $newSlnName

$newSubmitScript = "python submit.py -p" + $args[0] +" KattisSolution\Program.cs KattisSolution\InputOutput.cs"
New-Item "submitMe.bat" -type file -force -value $newSubmitScript

Start-Process $newSlnName
