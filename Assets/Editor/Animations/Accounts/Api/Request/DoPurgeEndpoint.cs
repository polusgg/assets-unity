using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class DoPurgeEndpoint {
        [JsonProperty("files")] public string[] Files;
    }
}