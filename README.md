# Azure SignalR Service Client(s)
This is a sample client that will use an Azure SignalR Service. Attempting to document this up enough for a Denver Dev Day presentation.

### Hypothesis

Configurable service that would provide communication from Azure Service Bus topics.

I thought this service was going to be something that I could configure in Azure that would
talk to the Azure Service Bus and, with some minor code, would send messages back to connected
clients.  This feature in Azure is **only** to monitor the hubs within a SignalR enabled WebApp.

### Let's Move On

#### Quickstart (.Net Core)
There is a "quickstart" section in the SignalR Service which outlines what code to paste where.

#### Additional Nuget Packages
* Microsoft.AspNetCore.SignalR.Client
* Microsoft.Azure.SignalR

#### Azure SignalR Service Reference
* [Microsoft Overview](https://azure.microsoft.com/en-us/services/signalr-service/)
* [Azure SDK GitHub Repo](https://github.com/Azure/azure-signalr)
* [Azure SignalR Samples](https://github.com/aspnet/AzureSignalR-samples)
* [Quickstarts Serverless Chat](https://github.com/Azure-Samples/signalr-service-quickstart-serverless-chat)
* [Azure Function Bindings](https://github.com/Azure/azure-functions-signalrservice-extension)
