using Assets.Editor.HatCreator;
using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class ItemCreation {
        [JsonProperty("id")] public string Id;
        [JsonProperty("name")] public string Name;
        [JsonProperty("author")] public string Author;
        [JsonProperty("amongUsId")] public uint InGameId;
        [JsonProperty("resource")] public Resource Resource;
        [JsonProperty("thumbnail")] public string ThumbnailUrl;
        [JsonProperty("type")] public PolusCosmeticType Type;
        [JsonProperty("recurring")] public bool Recurring;
    }
}