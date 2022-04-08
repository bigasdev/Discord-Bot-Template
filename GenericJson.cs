using System;
using System.Text;
using Newtonsoft.Json;

namespace discordbottemplate
{
    public struct GenericJson{
        [JsonProperty("name")]
        public string Name{get;private set;}
        [JsonProperty("id")]
        public string Id{get;private set;}
    }
}
