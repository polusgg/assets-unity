using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request {
    public class BundleUpdate {
        [JsonProperty("id")] public string Id;
        [JsonProperty("type")] public string Type = "COSMETIC"; //hardcode this because nothing needs to be anything else rn
        [JsonProperty("keyArtUrl")] public string CoverArt;
        [JsonProperty("color")] public string Color;
        [JsonProperty("name")] public string Name;
        [JsonProperty("items")] public string[] Cosmetics;
        [JsonProperty("priceUsd")] public int Price;
        [JsonProperty("description")] public string Description;
        [JsonProperty("forSale")] public bool ForSale;
        [JsonProperty("recurring")] public bool Recurring;
    }
}