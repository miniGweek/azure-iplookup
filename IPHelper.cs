using System;
using System.Collections.Generic;
using System.Net;

namespace azure_iplookup
{
    internal class IPHelper
    {
        public static Dictionary<string, string> ReturnMatchedIPRange(List<string> ips, List<Value> azureIps)
        {
            Dictionary<string, string> matchedIps = new Dictionary<string, string>();
            foreach (var ip in ips)
            {
                bool ipFound = false;
                IPAddress incomingIp = IsValidIP(ip) ? IPAddress.Parse(ip) : null;
                if (incomingIp != null)
                {
                    foreach (var azureip in azureIps)
                    {
                        foreach (var subnet in azureip.properties.addressPrefixes)
                        {
                            IPNetwork network = IPNetwork.Parse(subnet);

                            if (IPNetwork.Contains(network, incomingIp) && !ipFound)
                            {
                                matchedIps.Add(ip, $"{subnet} - {azureip.name}");
                                ipFound = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    matchedIps.Add(ip,"Invalid IP Address");
                }
                
            }


            return matchedIps;
        }

        public static Boolean IsValidIP(string ip)
        {
            IPAddress checkIp;
            bool result = IPAddress.TryParse(ip, out checkIp);
            return result;
        }
    }
}
