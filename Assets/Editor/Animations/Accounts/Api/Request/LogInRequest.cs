using Newtonsoft.Json;

namespace Editor.Accounts.Api.Request
{
    public class LogInRequest
    {
        [JsonProperty("email")] public string Email { get; set; } = "";

        [JsonProperty("password")] public string Password { get; set; } = "";
    }
}