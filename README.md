# Introduction
This is an introduction to GitHub Action Continuous delivery or CD for building .NET Web API Docker Container and pushing into Azure Container Registry

## Prerequsite
1. Create a Azure Container Registry with a tag for prefix. The key should be stack-name with a value of STACK_NAME_TAG_PREFIX-shared-container-registry.

# Steps
To run the demo, please follow the steps below.

1. [Fork](https://docs.github.com/en/get-started/quickstart/fork-a-repo) this git repo.
2. In your forked repo, go to settings, then Actions on the left blade, scroll down to the bottom and under Workflow permissions check the read and write permissions option.
3. Clone a local copy
4. Create a Service Principal via app registration and assign it AcrPush role assignment on your Azure Container Registry.
5. Create the secret in the dev environment from values below. Review the Secrets section below for the JSON body format.
    1. The client Id comes from the app registration.
    2. The client secret can be generated from the app registration.
    3. Use the subscription Id of your Azure Subscription.
    4. Use the tenant Id of your Azure AAD.
6. Create a branch
7. Push your branch and the GitHub Action workflow will run. Check out Actions tab to see it in action.
8. Once this is completed, you can check the Azure Container Registry for a new repository with ```mywebapp:0.1```.
9. Next, we can deploy to Azure Container Instance or ACI for testing. Run the following command to get the password which is the vale in the accessToken.
```
az acr login -n <ACR NAME> --expose-token
```
10. We can now deploy to our Container registry with the following command.
```
az container create -g <RESOURCE GROUP NAME> --name myapp --image <ACR NAME>.azurecr.io/mywebapp:0.1 --registry-password <VALUE_FROM_ACCESS_TOKEN> -registry-username 00000000-0000-0000-0000-000000000000 --dns-name-label <DNS name>
```
11. You should be able to launch the swagger page with /swagger. An example would be ```http://<DNS name>.centralus.azurecontainer.io/swagger/```.

## Secrets
| Name | Value |
| --- | --- |
| AZURE_CREDENTIALS | <pre>{<br/>&nbsp;&nbsp;&nbsp;&nbsp;"clientId": "",<br/>&nbsp;&nbsp;&nbsp;&nbsp;"clientSecret": "", <br/>&nbsp;&nbsp;&nbsp;&nbsp;"subscriptionId": "",<br/>&nbsp;&nbsp;&nbsp;&nbsp;"tenantId": "" <br/>}</pre> |
| STACK_NAME_TAG_PREFIX | Prefix used to identify shared resources such as the Azure Container Registry |
