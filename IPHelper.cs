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
                IPNetwork incomingNetwork = null;
                IPAddress incomingIp = IsValidIP(ip) ? IPAddress.Parse(ip) : null;
                bool incomingNetworkParsed = incomingIp == null ? IPNetwork.TryParse(ip, out incomingNetwork) : false;
                if (incomingIp != null || incomingNetwork!=null)
                {
                    foreach (var azureip in azureIps)
                    {
                        foreach (var subnet in azureip.properties.addressPrefixes)
                        {
                            IPNetwork network = IPNetwork.Parse(subnet);
                            

                            if (((incomingIp!=null && network.Contains(incomingIp)) 
                                || (incomingNetwork!=null && network.Contains(incomingNetwork)))
                                && !ipFound)
                            {
                                matchedIps.Add(ip, $"{subnet} - {azureip.name}");
                                ipFound = true;
                                break;
                            }
                        }

                    }

                    if (!ipFound)
                    {
                        if (!matchedIps.ContainsKey(ip))
                        {
                            matchedIps.Add(ip, "No Match");
                        }
                    }
                }
                else
                {
                    if (!matchedIps.ContainsKey(ip))
                    {
                        matchedIps.Add(ip, "No Match");
                    }
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
