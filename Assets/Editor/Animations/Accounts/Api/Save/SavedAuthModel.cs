using UnityEngine;
using UnityEngine.Serialization;

namespace Editor.Accounts.Api.Save {
    public class SavedAuthModel : ScriptableObject {
        [FormerlySerializedAs("ClientIdString")] public string ClientId;

        // Base64 string Client Token, but used as UTF-8 encoded
        public string ClientToken;
        public string DisplayName;
        public string S3Key;
        public string S3Secret;
        public string DoPersonalToken;

        public string[] Perks;
    }
}