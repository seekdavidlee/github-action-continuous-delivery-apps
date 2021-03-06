param(
    [Parameter(Mandatory = $true)][string]$APP_NAME, 
    [Parameter(Mandatory = $true)][string]$APP_VERSION, 
    [Parameter(Mandatory = $true)][string]$BUILD_ENV, 
    [Parameter(Mandatory = $true)][string]$BUILD_PATH,
    [Parameter(Mandatory = $true)][string]$STACK_NAME_TAG_PREFIX)

$ErrorActionPreference = "Stop"

$tagValue = "$STACK_NAME_TAG_PREFIX-shared-container-registry"
$platformRes = (az resource list --tag stack-name=$tagValue | ConvertFrom-Json)
if (!$platformRes) {
    throw "Unable to find eligible container registry! Did you add a tag of key stack-name and value of $tagValue?"
}
if ($platformRes.Length -eq 0) {
    throw "Unable to find 'ANY' eligible platform container registry!"
}

$acr = ($platformRes | Where-Object { $_.tags.'stack-environment' -eq $BUILD_ENV })
if (!$acr) {
    throw "Unable to find eligible container registry! Did you add a tag of key stack-environment and value of dev?"
}
$AcrName = $acr.Name

az acr login --name $AcrName
if ($LastExitCode -ne 0) {
    throw "An error has occured. Unable to login to acr."
}

$shouldBuild = $true
$tags = az acr repository show-tags --name $AcrName --repository $APP_NAME | ConvertFrom-Json
if ($tags) {
    if ($tags.Contains($APP_VERSION)) {
        $shouldBuild = $false
    }
}

if ($shouldBuild -eq $true) {    
    $imageName = "$APP_NAME`:$APP_VERSION"
    Write-Host "Image name: $imageName"
    az acr build --image $imageName -r $AcrName --file ./$BUILD_PATH/Dockerfile .

    if ($LastExitCode -ne 0) {
        throw "An error has occured. Unable to build image."
    }
}