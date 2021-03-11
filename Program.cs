using System;
using System.IO;
using System.Text.Json;

namespace azure_iplookup
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = string.Empty;
            string serviceTagJsonFileName = "ServiceTags_Public_Active.json";

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Enter IP for searching in Azure IP/ServiceTag Ranges :");
                ip = Console.ReadLine();
                Console.WriteLine("Enter name of the json file ( if not specified ,ServiceTags_Public_Active.json will be used ) :");
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
