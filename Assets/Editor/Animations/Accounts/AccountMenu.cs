using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Editor.Accounts.Api;
using Editor.Accounts.Api.Response;
using Editor.Accounts.Api.Save;
using UnityEditor;
using UnityEngine;

namespace Editor.Accounts {
    public class AccountMenu : EditorWindow {
        private static string SaveFile => "Assets/account_token.asset";
        private ApiClient client = new ApiClient();
        private static SavedAuthModel _save;

        public static bool HasSave => File.Exists(SaveFile) || _save != null;
        public static SavedAuthModel Save {
            get => File.Exists(SaveFile) || _save ? _save = AssetDatabase.LoadAssetAtPath<SavedAuthModel>(SaveFile) : _save;
            set {
                if (value == null && File.Exists(SaveFile)) {
                    AssetDatabase.DeleteAsset(SaveFile);
                    _save = value;
                }
                else if (value != null && !File.Exists(SaveFile)) AssetDatabase.CreateAsset(value, SaveFile);
                else {
                    _save = value;
                    EditorUtility.SetDirty(value);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        private List<IEnumerator> Coroutines = new List<IEnumerator>();
        private string email = "";
        private string password = "";
        private bool requesting;

        private void OnGUI() {
            GUILayout.Label("Pgg Accounts");
            if (Save == null) {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Email", GUILayout.Width(100));
                email = GUILayout.TextField(email);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Password", GUILayout.Width(100));
                password = GUILayout.PasswordField(password, '*');
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Login")) {
                    IEnumerator Destroyed() {
                        EditorUtility.DisplayProgressBar("Logging in to your mom", "(she left the computer on)", 0.69f);
                        Task task = Login(email, password);
                        while (!task.IsCompleted) yield return null;
                        EditorUtility.ClearProgressBar();
                        if (Save != null) {
                            email = null;
                            password = null;
                        }
                    }

                    StartCoroutine(Destroyed());
                }
            } else {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Username", GUILayout.Width(100));
                GUILayout.TextField(_save.DisplayName);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("S3 Key", GUILayout.Width(100));
                _save.S3Key = GUILayout.TextField(_save.S3Key);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("S3 Secret", GUILayout.Width(100));
                _save.S3Secret = GUILayout.PasswordField(_save.S3Secret ?? "", '*');
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("DigitalOcean Token", GUILayout.Width(100));
                _save.DoPersonalToken = GUILayout.TextField(_save.DoPersonalToken ?? "");
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Save")) {
                    Save = _save;
                }
                if (GUILayout.Button("Log out")) {
                    LogOut();
                }
            }
        }

        [MenuItem("Polus/Accounts")]
        public static void Open() {
            AccountMenu window = (AccountMenu) GetWindow(typeof(AccountMenu));
            window.Show();
        }

        private void Awake() {
            StartCoroutine(CheckToken());
        }

        private void LogOut() => Save = _save = null;

        private IEnumerator CheckToken() {
            Task<GenericResponse<CheckTokenData>> response = client.CheckToken(
                Save.ClientId,
                Save.ClientToken
            );

            while (!response.IsCompleted) yield return null;
            if (response.Result == null) LogOut();
        }

        private void StartCoroutine(IEnumerator enumerator) {
            Coroutines.Add(enumerator);
        }

        private void Update() {
            for (int i = 0; i < Coroutines.Count; i++) {
                if (!Coroutines[i].MoveNext()) {
                    Coroutines.RemoveAt(i);
                    i--;
                }
            }
        }

        private async Task Login(string email, string password) {
            GenericResponse<LoginResponse> result = await client
                .LogIn(email, password);
            if (result != null) {
                SavedAuthModel sauth = CreateInstance<SavedAuthModel>();
                sauth.DisplayName = result.Data.DisplayName;
                sauth.Perks = result.Data.Perks;
                sauth.ClientId = result.Data.ClientId;
                sauth.ClientToken = result.Data.ClientToken;
                Save = sauth;
            }
        }
    }
}