using Assets.Editor.HatCreator;
using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class ItemUpdate {
        [JsonProperty("name")] public string Name;
        [JsonProperty("author")] public string Author;
        [JsonProperty("amongUsId")] public uint IngameId;
        [JsonProperty("resource")] public Resource Resource;
        [JsonProperty("thumbnail")] public string ThumbnailUrl;
        [JsonProperty("type")] public PolusCosmeticType Type;
        [JsonProperty("recurring")] public bool Recurring;
    }
}