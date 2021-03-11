# azure-iplookup
Lookup a given ip against Azure IP / Servicetag json file to find out if it's a valid Azure IP and find which service it belongs to

# Credits
- Using [AngleSharp](https://github.com/AngleSharp/AngleSharp) library for parsing html and fetching download link of the json file.
- Using [IPNetwork2](https://www.nuget.org/packages/IPNetwork2/) library for parsing CIDR ranges and lookup IP in the CIDR.

# Setup and PreRequisites
 - Requires [.net core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1) runtime installed.
 - (Optional - Code supports downloading it now) Download the latest servicet tag json from [Azure IP Ranges and Service Tags – Public Cloud](https://www.microsoft.com/en-us/download/details.aspx?id=56519)
-  (Optional - Code supports downloading it now) Copy it to this applications directory and rename it as ServiceTags_Public_Active.json


# Instruction on how to use
- Navigate to the app directory.
- Run `dotnet run --update` to download the latest servicetags json file.
- Run `dotnet run` for prompts
- Run `dotnet run IP` to do a lookup of an IP against the servicetags json file downloaded.
- Run `dotnet run IP filename` to do a lookup of an IP against the servicetags json file mentioned in the filename.
- Run `dotnet run FileWithIpList` to do a lookup of an List Of IPS from a txt file against the json file in the app dir.

### Example 1 : 
``` Powershell
dotnet run
Enter IP or the path to the file with list of IPs for searching in Azure IP/ServiceTag Ranges :
20.190.167.64
Enter name of the json file ( if not specified ,ServiceTags_Public_Active.json will be used ) :

IP : 20.190.167.64, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
```
### Example 2 : 
``` Powershell
dotnet run 20.193.17.157
IP : 20.193.17.157, Matched IP Range is 20.193.0.0/18 - AzureCloud.australiaeast
```
### Example 3 : Specify a **json** file

``` Powershell
dotnet run 20.193.17.157 "ServiceTags_Public_Active.json"
IP : 20.193.17.157, Matched IP Range is 20.193.0.0/18 - AzureCloud.australiaeast
```

### Example 4 : Download Updated version of the json file

``` Powershell
dotnet run --update
Latest Service Tag Json file downloaded
Previous ServiceTags_Public_Active.json renamed to ServiceTags_Public_Arhive_11032021.json
Newly downloaded file ServiceTags_Public_20210308.json renamed to ServiceTags_Public_Active.json
```

### Example 5 : Read IPs from a txt file and then do lookup against the json #1
```Powershellcd 
❯ cat .\ListOfIPs.txt
20.190.167.64
20.190.167.65
10.20.10.3
20.190.167.19
20.190.167.22
20.190.167.86
13.70.72.238
somejunk
52.241.88.36

❯ dotnet run
Enter IP or the path to the file with list of IPs for searching in Azure IP/ServiceTag Ranges :
ListOfIPs.txt
Enter name of the json file ( if not specified ,ServiceTags_Public_Active.json will be used ) :

IP : 20.190.167.64, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.65, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.19, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.22, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.86, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 13.70.72.238, Matched IP Range is 13.70.72.232/29 - AzureMonitor
IP : somejunk, Matched IP Range is Invalid IP Address
IP : 52.241.88.36, Matched IP Range is 52.241.88.32/28 - Storage
```

### Example 6 : Read IPs from a txt file and then do lookup against the json #2
```Powershell
❯ dotnet run ListOfIPs.txt
IP : 20.190.167.64, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.65, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.19, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.22, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 20.190.167.86, Matched IP Range is 20.190.128.0/18 - AzureActiveDirectory
IP : 13.70.72.238, Matched IP Range is 13.70.72.232/29 - AzureMonitor
IP : somejunk, Matched IP Range is Invalid IP Address
IP : 52.241.88.36, Matched IP Range is 52.241.88.32/28 - Storage
```