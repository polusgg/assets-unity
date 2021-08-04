using Assets.Editor.HatCreator;
using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class ItemUpdate {
        [JsonProperty("name")] public string Name;
        [JsonProperty("resource")] public Resource Resource;
        [JsonProperty("thumbnail")] public string ThumbnailUrl;
        [JsonProperty("type")] public CosmeticType Type;
    }
}