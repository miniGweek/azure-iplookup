using System.Collections.Generic;

namespace azure_iplookup
{
    public class Properties
    {
        public int changeNumber { get; set; }
        public string region { get; set; }
        public int regionId { get; set; }
        public string platform { get; set; }
        public string systemService { get; set; }
        public List<string> addressPrefixes { get; set; }
        public List<string> networkFeatures { get; set; }
    }

    public class Value
    {
        public string name { get; set; }
        public string id { get; set; }
        public Properties properties { get; set; }
    }

    public class Root
    {
        public int changeNumber { get; set; }
        public string cloud { get; set; }
        public List<Value> values { get; set; }
    }

}
