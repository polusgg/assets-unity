using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class BundleUpdate {
        [JsonProperty("id")] public string Id;
        [JsonProperty("keyArtUrl")] public string CoverArt;
        [JsonProperty("color")] public string Color;
        [JsonProperty("author")] public string Author;
        [JsonProperty("name")] public string Name;
        [JsonProperty("items")] public string[] Cosmetics;
        [JsonProperty("priceUsd")] public float Price;
        [JsonProperty("description")] public string Description;
        [JsonProperty("forSale")] public bool ForSale;
    }
}