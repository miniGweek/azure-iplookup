# azure-iplookup
Lookup a given ip against Azure IP / Servicetag json file to find out if it's a valid Azure IP and find which service it belongs to

# Credits
- Using [AngleSharp](https://github.com/AngleSharp/AngleSharp) library for parsing html and fetching download link of the json file.
- Using [IPNetwork2](https://www.nuget.org/packages/IPNetwork2/) library for parsing CIDR ranges and lookup IP in the CIDR.

# Setup and PreRequisites
 - Requires [.net core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1) runtime installed.
 - (Optional - Code supports downloading it now) Download the latest servicet tag json from [Azure IP Ranges and Service Tags – Public Cloud](https://www.microsoft.com/en-us/download/details.aspx?id=56519)
-  (Optional - Code supports downloading it now)Copy it to this applications directory and rename it as ServiceTags_Public_Active.json


# Instruction on how to use
- Navigate to the app directory.
- Run `dotnet run --update` to download the latest servicetags json file.
- Run `dotnet run IP` to do a lookup of an IP against the servicetags json file downloaded.
- Run `dotnet run IP filename` to do a lookup of an IP against the servicetags json file mentioned in the filename.


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

### Example 4 : Download Updated version of the json file

``` Powershell
dotnet run --update
Latest Service Tag Json file downloaded
Previous ServiceTags_Public_Active.json renamed to ServiceTags_Public_Arhive_11032021.json
Newly downloaded file ServiceTags_Public_20210308.json renamed to ServiceTags_Public_Active.json
```