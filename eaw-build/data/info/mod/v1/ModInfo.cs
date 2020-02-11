using Newtonsoft.Json;

namespace eaw.build.data.info.mod.v1
{
    public class ModInfo
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
        public string Summary { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("steamdata", NullValueHandling = NullValueHandling.Ignore)]
        public SteamData SteamData { get; set; }

        [JsonProperty("custom", NullValueHandling = NullValueHandling.Ignore)]
        public object Custom { get; set; }

        public static ModInfo FromJson(string json) =>
            JsonConvert.DeserializeObject<ModInfo>(json, Converter.SETTINGS);
    }
}
