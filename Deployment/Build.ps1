param($appName, $appVersion, $buildEnv, $buildPath)

$ErrorActionPreference = "Stop"
$imageName = "$appName`:$appVersion"

$platformRes = (az resource list --tag stack-name=shared-container-registry | ConvertFrom-Json)
if (!$platformRes) {
    throw "Unable to find eligible container registry!"
}
if ($platformRes.Length -eq 0) {
    throw "Unable to find 'ANY' eligible platform container registry!"
}

$acr = ($platformRes | Where-Object { $_.tags.'stack-environment' -eq $buildEnv })
if (!$acr) {
    throw "Unable to find eligible container registry!"
}
$AcrName = $acr.Name

az acr login --name $AcrName
if ($LastExitCode -ne 0) {
    throw "An error has occured. Unable to login to acr."
}

$shouldBuild = $true
$tags = az acr repository show-tags --name $AcrName --repository $appName | ConvertFrom-Json
if ($tags) {
    if ($tags.Contains($appVersion)) {
        $shouldBuild = $false
    }
}

if ($shouldBuild -eq $true) {
    # Build your app with ACR build command
    az acr build --image $imageName -r $AcrName --file ./$buildPath/Dockerfile .

    if ($LastExitCode -ne 0) {
        throw "An error has occured. Unable to build image."
    }
}