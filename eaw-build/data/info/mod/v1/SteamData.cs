using System.Collections.Generic;
using Newtonsoft.Json;

namespace eaw.build.data.info.mod.v1
{
    public class SteamData
    {
        [JsonProperty("publishedfileid", Required = Required.Always)]
        public string PublishedFileId { get; set; }

        [JsonProperty("contentfolder", Required = Required.Always)]
        public string ContentFolder { get; set; }

        [JsonProperty("visibility", Required = Required.Always)]
        public int Visibility { get; set; }

        [JsonProperty("metadata", Required = Required.Always)]
        public string MetaData { get; set; }

        [JsonProperty("tags", Required = Required.Always)]
        public List<string> Tags { get; set; }
    }
}
