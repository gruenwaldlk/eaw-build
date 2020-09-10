using Newtonsoft.Json;

namespace eaw.build.data.info.mod.v1
{
    public static class Serialize
    {
        public static string ToJson(this ModInfo self) => JsonConvert.SerializeObject(self, Converter.SETTINGS);
    }
}
