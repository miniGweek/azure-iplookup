# azure-iplookup
Lookup a given ip against Azure IP / Servicetag json file to find out if it's a valid Azure IP and find which service it belongs to

# Setup and PreRequisites
 - Requires [.net core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1) runtime installed
 - Download the latest servicet tag json from [Azure IP Ranges and Service Tags – Public Cloud](https://www.microsoft.com/en-us/download/details.aspx?id=56519)
-  Copy it to this applications directory and rename it as ServiceTags_Public_Active.json


# Instruction on how to use

### Example 1 : 
``` Powershell
dotnet run 20.193.17.157
Matched IP Range is 20.193.0.0/18 - AzureCloud.australiaeast
```
### Example 2 :

``` Powershell
dotnet run
Enter IP for searching in Azure IP/ServiceTag Ranges :
20.193.17.157
Matched IP Range is 20.193.0.0/18 - AzureCloud.australiaeast
```

### Example 3 : Specify a **json** file

``` Powershell
dotnet run 20.193.17.157 "ServiceTags_Public_Active.json"
Matched IP Range is 20.193.0.0/18 - AzureCloud.australiaeast
```