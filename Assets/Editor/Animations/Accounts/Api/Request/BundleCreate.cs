using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class BundleCreate {
        [JsonProperty("id")] public string Id;
        [JsonProperty("keyArtUrl")] public string CoverArt;
        [JsonProperty("color")] public string Color;
        [JsonProperty("name")] public string Name;
        [JsonProperty("items")] public string[] Cosmetics;
        [JsonProperty("priceUsd")] public int Price;
        [JsonProperty("description")] public string Description;
        [JsonProperty("forSale")] public bool ForSale;
    }
}