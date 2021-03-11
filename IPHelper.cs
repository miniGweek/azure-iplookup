using System.Collections.Generic;
using System.Net;

namespace azure_iplookup
{
    internal class IPHelper
    {
        public static string ReturnMatchedIPRange(string ip, List<Value> azureIps)
        {
            IPAddress incomingIp = IPAddress.Parse(ip);
            foreach (var azureip in azureIps)
            {
                foreach (var subnet in azureip.properties.addressPrefixes)
                {
                    IPNetwork network = IPNetwork.Parse(subnet);

                    if (IPNetwork.Contains(network, incomingIp))
                    {
                        return $"{subnet} - {azureip.name}";
                    }
                }
            }
            return null;
        }
    }
}
