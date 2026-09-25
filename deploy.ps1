param(
	[string] $ResourceGroup = "PortfolioRG",
	[string] $AppName = "keerthiportfolioapi",
	[string] $Runtime = "linux-x64",
	[string] $Configuration = "Release",
	[string] $PublishFolder = ".\\publish",
	[string] $PublishZip = ".\\publish.zip",
	[switch] $SelfContained
)

Write-Output "Starting publish and deploy script"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
	Write-Error "dotnet CLI not found. Install .NET SDK and try again."
	exit 1
}

if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
	Write-Error "Azure CLI not found. Install Azure CLI and login (az login) before running this script."
	exit 1
}

$rid = $Runtime
$selfContainedFlag = $SelfContained.IsPresent ? "--self-contained true" : ""

if (Test-Path $PublishFolder) { Remove-Item $PublishFolder -Recurse -Force }

Write-Output "Running: dotnet publish -c $Configuration -r $rid $selfContainedFlag -o $PublishFolder"
dotnet publish -c $Configuration -r $rid $selfContainedFlag -o $PublishFolder
if ($LASTEXITCODE -ne 0) {
	Write-Error "dotnet publish failed"
	exit 1
}

if (Test-Path $PublishZip) { Remove-Item $PublishZip -Force }

Write-Output "Creating zip: $PublishZip"
Compress-Archive -Path "$PublishFolder\*" -DestinationPath $PublishZip -Force

Write-Output "Deploying $PublishZip to webapp $AppName in resource group $ResourceGroup"
az webapp deploy --resource-group $ResourceGroup --name $AppName --src-path $PublishZip --type zip

if ($LASTEXITCODE -ne 0) {
	Write-Error "az webapp deploy failed. Check 'az webapp log deployment show' for details."
	exit 1
}

Write-Output "Deployment command completed. Check Azure portal or Log Stream for runtime logs."
