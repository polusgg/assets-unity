using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class Resource {
        [JsonProperty("path")] public string Path;
        [JsonProperty("id")] public uint Id;
    }
}