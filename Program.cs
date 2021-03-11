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
            if (args == null || args.Length == 0 || args.Length > 1)
            {
                Console.WriteLine("Enter IP for searching in Azure IP/ServiceTag Ranges :");
                ip = Console.ReadLine();
            }
            else if (args.Length == 1)
            {
                ip = args[0];
            }
            var azureIpAndServiceTagJson = File.ReadAllText("ServiceTags_Public_20210308.json");
            var azureIPs = JsonSerializer.Deserialize<Root>(azureIpAndServiceTagJson);
            var match = IPHelper.ReturnMatchedIPRange(ip, azureIPs.values);
            Console.WriteLine($"Matched IP Range is {match}");
        }
    }
}
