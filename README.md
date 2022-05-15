# Introduction
This is an introduction to GitHub Action Continuous delivery or CD for building .NET Web API Docker Container and pushing into Azure Container Registry

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

## Secrets
| Name | Value |
| --- | --- |
| CCH_AZURE_CREDENTIALS | <pre>{<br/>&nbsp;&nbsp;&nbsp;&nbsp;"clientId": "",<br/>&nbsp;&nbsp;&nbsp;&nbsp;"clientSecret": "", <br/>&nbsp;&nbsp;&nbsp;&nbsp;"subscriptionId": "",<br/>&nbsp;&nbsp;&nbsp;&nbsp;"tenantId": "" <br/>}</pre> |