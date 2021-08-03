namespace Editor.Accounts.Api.Response
{
    public class LoginResponse
    {
        public string ClientId { get; set; } = "";
        public string ClientToken { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string BannedUntil { get; set; } = "";

        public string[] Perks { get; set; } = { };
    }
}