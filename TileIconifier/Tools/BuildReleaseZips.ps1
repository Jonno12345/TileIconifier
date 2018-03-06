function ZipFiles( $zipfilename, $sourcedir )
{
   Add-Type -Assembly System.IO.Compression.FileSystem
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
   [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)
}

function CopyRequiredFiles( $sourceFolder, $destinationFolder)
{
	If (Test-Path $destinationFolder){
		Remove-Item $destinationFolder -Recurse -Force
	}
	New-Item -Force -ItemType directory -Path $destinationFolder
	Copy-Item ($sourceFolder + "TileIconifier.exe") $destinationFolder

	# Copy-Item ($sourceFolder + "ru") $destinationFolder -Recurse
    # Copy-Item ($sourceFolder + "System.Management.Automation.dll") $destinationFolder
	# Copy-Item ($sourceFolder + "Octokit.dll") $destinationFolder
}

function BuildReleaseZipFiles($sourceFolder, $destinationFolder, $zipFilePath)
{
	If (Test-Path $zipFilePath){
		Remove-Item $zipFilePath -Force
	}
	CopyRequiredFiles $sourceFolder $destinationFolder
	ZipFiles $zipFilePath $destinationFolder
}

$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$strVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo(((get-item $scriptDir).Parent.FullName) + "\bin\x64\Release\TileIconifier.exe").FileVersion
$x64BuildFolderPath = Join-Path (Get-Item $PSScriptRoot).Parent.FullName -ChildPath ("Bin\x64\Release\")
$x86BuildFolderPath = Join-Path (Get-Item $PSScriptRoot).Parent.FullName -ChildPath ("Bin\x86\Release\")
$releaseBuildFolderPath = Join-Path (Get-Item $PSScriptRoot).Parent.FullName -ChildPath ("Releases\" + $strVersion + "\")
$tempReleaseBuildFolderPath = $releaseBuildFolderPath + "Temp"
$x64ReleaseFile = $releaseBuildFolderPath + "TileIconifier v" + $strVersion + " x64.zip"
$x86ReleaseFile = $releaseBuildFolderPath + "TileIconifier v" + $strVersion + " x86.zip"

BuildReleaseZipFiles $x64BuildFolderPath $tempReleaseBuildFolderPath $x64ReleaseFile
BuildReleaseZipFiles $x86BuildFolderPath $tempReleaseBuildFolderPath $x86ReleaseFile