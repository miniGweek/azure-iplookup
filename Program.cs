using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace azure_iplookup
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            
            string ip = string.Empty;
            string serviceTagJsonFileName = "ServiceTags_Public_Active.json";
            if (args != null &&  "--update".Equals(args[0],StringComparison.InvariantCultureIgnoreCase))
            {
                var downloadedServiceTagsJsonFileName = await DownloadJson.DownloadServiceTagJsonFile();
                if (!string.IsNullOrWhiteSpace(downloadedServiceTagsJsonFileName))
                {

                    Console.WriteLine("Latest Service Tag Json file downloaded");
                    var archiveJsonFileName = $"ServiceTags_Public_Arhive_{DateTime.Today.ToString("ddMMyyyy")}.json";
                    File.Move("ServiceTags_Public_Active.json", archiveJsonFileName);
                    Console.WriteLine($"Previous {serviceTagJsonFileName} renamed to {archiveJsonFileName}");
                    File.Copy(downloadedServiceTagsJsonFileName, serviceTagJsonFileName);
                    Console.WriteLine(
                        $"Newly downloaded file {downloadedServiceTagsJsonFileName} renamed to {serviceTagJsonFileName}");
                    return;
                }
            }
            else
            {

                if (args == null || args.Length == 0)
                {
                    Console.WriteLine("Enter IP for searching in Azure IP/ServiceTag Ranges :");
                    ip = Console.ReadLine();
                    Console.WriteLine(
                        "Enter name of the json file ( if not specified ,ServiceTags_Public_Active.json will be used ) :");
                    string userInputJsonFileName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(userInputJsonFileName))
                    {
                        serviceTagJsonFileName = userInputJsonFileName;
                    }
                }
                else if (args.Length == 1)
                {
                    ip = args[0];
                }
                else if (args.Length == 2)
                {
                    ip = args[0];
                    if (!string.IsNullOrWhiteSpace(args[1]))
                    {
                        serviceTagJsonFileName = args[1];
                    }
                }

                var azureIpAndServiceTagJson = File.ReadAllText(serviceTagJsonFileName);
                var azureIPs = JsonSerializer.Deserialize<Root>(azureIpAndServiceTagJson);
                var match = IPHelper.ReturnMatchedIPRange(ip, azureIPs.values);
                Console.WriteLine($"Matched IP Range is {match}");
            }
        }
    }
}
