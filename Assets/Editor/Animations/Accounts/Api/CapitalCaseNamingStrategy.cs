using Newtonsoft.Json.Serialization;

namespace Editor.Accounts.Api {
    public class CapitalCaseNamingStrategy : NamingStrategy {
        protected override string ResolvePropertyName(string name) => name.ToUpper();
    }
}