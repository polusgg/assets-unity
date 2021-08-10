using Newtonsoft.Json;

namespace Editor.Accounts.Api.Response {
    public class DoListEndpoint {
        [JsonProperty("endpoints")] public Endpoint[] Endpoints;

        public class Endpoint {
            [JsonProperty("id")] public string Id;
            [JsonProperty("origin")] public string Origin;
            [JsonProperty("custom_domain")] public string CustomDomain;
        }
    }
}