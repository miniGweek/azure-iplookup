using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace azure_iplookup
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string ip = string.Empty;
            List<string> ips = null;
            string serviceTagJsonFileName = "ServiceTags_Public_Active.json";
            string ipFlePath = string.Empty;
            if (args != null && args.Length == 1 && "--update".Equals(args[0], StringComparison.InvariantCultureIgnoreCase))
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
                    Console.WriteLine("Enter IP or the path to the file with list of IPs for searching in Azure IP/ServiceTag Ranges :");
                    ip = Console.ReadLine();
                    if (IPHelper.IsValidIP(ip))
                    {
                        ips = new List<string>() {ip};
                    }
                    if (!IPHelper.IsValidIP(ip) && File.Exists(ip))
                    {
                        ips = File.ReadAllLines(ip).ToList();
                    }
                    else if (!IPHelper.IsValidIP(ip) && !File.Exists(ip))
                    {
                        Console.WriteLine("Invalid IP/File doesn't exist. Stopping.");
                        return;
                    }
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
                    if (IPHelper.IsValidIP(ip))
                    {
                        ips = new List<string>() { ip };
                    }
                    else
                    {
                        Console.WriteLine("Invalid IP/File doesn't exist. Stopping.");
                        return;
                    }
                }
                else if (args.Length == 2)
                {
                    ip = args[0];
                    if (IPHelper.IsValidIP(ip))
                    {
                        ips = new List<string>() { ip };
                    }
                    else
                    {
                        Console.WriteLine("Invalid IP/File doesn't exist. Stopping.");
                        return;
                    }
                    if (!string.IsNullOrWhiteSpace(args[1]))
                    {
                        serviceTagJsonFileName = args[1];
                    }
                }

                var azureIpAndServiceTagJson = File.ReadAllText(serviceTagJsonFileName);
                var azureIPs = JsonSerializer.Deserialize<Root>(azureIpAndServiceTagJson);
                var match = IPHelper.ReturnMatchedIPRange(ips, azureIPs.values);
                foreach (var m in match.Keys)
                {
                    Console.WriteLine($"IP : {m}, Matched IP Range is {match[m]}");
                }
                
            }
        }
    }
}
