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
            string serviceTagJsonFileName = string.Empty;
            string defaultSeriveTagJsonFileName = "ServiceTags_Public_Active.json";
            string ipFlePath = string.Empty;
            if (args != null && args.Length == 1 &&
                "--update".Equals(args[0], StringComparison.InvariantCultureIgnoreCase))
            {
                await DownloadJson.DownloadAndRenameServiceTagsAndIPJsonFile(defaultSeriveTagJsonFileName);
                return;
            }
            else
            {
                if (args == null || args.Length == 0)
                {
                    Console.WriteLine(
                        "Enter IP or the path to the file with list of IPs for searching in Azure IP/ServiceTag Ranges :");
                    ip = Console.ReadLine();
                    ips = ValidateAndLoadIp(ip);

                    Console.WriteLine(
                        "Enter name of the json file ( if not specified ,ServiceTags_Public_Active.json will be used ) :");
                    string userInputJsonFileName = Console.ReadLine();
                    serviceTagJsonFileName = ValidateAndLoadJsonFileName(userInputJsonFileName, defaultSeriveTagJsonFileName);
                }
                else if (args.Length == 1)
                {
                    ip = args[0];
                    ips = ValidateAndLoadIp(ip);
                    serviceTagJsonFileName = defaultSeriveTagJsonFileName;
                }
                else if (args.Length == 2)
                {
                    ip = args[0];
                    ips = ValidateAndLoadIp(ip);
                    serviceTagJsonFileName = ValidateAndLoadJsonFileName(args[1], defaultSeriveTagJsonFileName);
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

        internal static string ValidateAndLoadJsonFileName(string userInputJsonFileName, string defaultSeriveTagJsonFileName)
        {
            string serviceTagJsonFileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(userInputJsonFileName))
            {
                serviceTagJsonFileName = userInputJsonFileName;
            }
            else
            {
                serviceTagJsonFileName = defaultSeriveTagJsonFileName;
            }

            return serviceTagJsonFileName;
        }

        internal static List<string> ValidateAndLoadIp(string ip)
        {
            List<string> ips = null;
            if (IPHelper.IsValidIP(ip))
            {
                ips = new List<string>() { ip };
            }

            if (!IPHelper.IsValidIP(ip) && File.Exists(ip))
            {
                ips = File.ReadAllLines(ip).ToList();
            }
            else if (!IPHelper.IsValidIP(ip) && !File.Exists(ip))
            {
                Console.WriteLine("Invalid IP/File doesn't exist. Stopping.");
                return ips;
            }

            return ips;
        }
    }
}
