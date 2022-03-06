$resourceGroup = "orleansintro"
$location = "eastus"
$storageAccount = "orleansintrostorage"
$clusterName = "orleansintro"
$containerRegistry = "orleansintroacr"

az login

# Create a resource group
az group create --name $resourceGroup --location $location

# Create an Azure storage account
az storage account create --location $location --name $storageAccount --resource-group $resourceGroup --kind "StorageV2" --sku "Standard_LRS"

# Create an AKS cluster. This can take a few minutes
az aks create --resource-group $resourceGroup --name $clusterName --node-count 3

# If you haven't already, install the Kubernetes CLI
az aks install-cli

# Authenticate the Kubernetes CLI
az aks get-credentials --resource-group $resourceGroup --name $clusterName

# Create an Azure Container Registry account and login to it
az acr create --name $containerRegistry --resource-group $resourceGroup --sku Standard

# Create a service principal for the container registry and register it with Kubernetes as an image pulling secret
$acrId = $(az acr show --name $containerRegistry --query id --output tsv)
$acrServicePrincipalName = "$($containerRegistry)-aks-service-principal"
$acrSpPw = $(az ad sp create-for-rbac --name http://$acrServicePrincipalName --scopes $acrId --role acrpull --query password --output tsv)
$acrSpAppId = $(az ad sp show --id http://$acrServicePrincipalName --query appId --output tsv)
$acrLoginServer = $(az acr show --name $containerRegistry --resource-group $resourceGroup --query loginServer).Trim('"')
kubectl create secret docker-registry $containerRegistry --namespace default --docker-server=$acrLoginServer --docker-username=$acrSpAppId --docker-password=$acrSpPw

# Configure the storage account that the application is going to use by adding a new secret to Kubernetes
kubectl create secret generic az-storage-acct --from-literal=key=$(az storage account show-connection-string --name $storageAccount --resource-group $resourceGroup --output tsv)