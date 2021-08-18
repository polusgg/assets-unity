using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Cosmetics;
using Editor.Accounts;
using Editor.Accounts.Api.Request;
using Editor.Accounts.Api.Response;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Editor.HatCreator {
    [CustomEditor(typeof(CosmeticBundleObject))]
    public class CosmeticBundleInspector : UnityEditor.Editor {
        private bool loggedIn => AccountMenu.Save != null;
        private CosmeticBundleObject targetObj => (CosmeticBundleObject) serializedObject.targetObject;
        private uint baseId;
        private bool endedProgress;
        private List<IEnumerator> Coroutines = new List<IEnumerator>();
        public const string bundleRoot = "Assets/AssetBundles/Cosmetics";
        public object lockable = new object();

        IEnumerator FetchId(CosmeticBundleObject.CosmeticData cosmetic) {
            EditorUtility.DisplayProgressBar("Fetching your mom", "(she's really far away)", 0.420f);
            Task task = Fetch(cosmetic);
            while (!task.IsCompleted) yield return null;
            EditorUtility.ClearProgressBar();
            if (task.IsFaulted) throw task.Exception;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("DEBUG: Price for server");
            EditorGUILayout.IntField(int.Parse(targetObj.Price.ToString("###.00", CultureInfo.InvariantCulture).Replace(".", "")));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add slot")) {
                // targetObj.Cosmetics = targetObj.Cosmetics.Append(CreateInstance<CosmeticBundleObject.CosmeticData>()).ToArray();
                targetObj.Cosmetics = targetObj.Cosmetics.Append(new CosmeticBundleObject.CosmeticData()).ToArray();
            }

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < targetObj.Cosmetics.Length; i++) {
                CosmeticBundleObject.CosmeticData cosmetic = targetObj.Cosmetics[i];
                cosmetic.foldedOut = EditorGUILayout.Foldout(cosmetic.foldedOut, $"Element {i} ({(string.IsNullOrEmpty(cosmetic.Name) ? "No name" : cosmetic.Name)})");
                if (!cosmetic.foldedOut) continue;
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Name");
                // string nameBefore = cosmetic.Name;
                cosmetic.Name = EditorGUILayout.TextField(cosmetic.Name);
                // if (cosmetic.Name != nameBefore) EditorUtility.SetDirty(targetObj);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Author");
                // string authorBefore = cosmetic.Name;
                cosmetic.Author = EditorGUILayout.TextField(cosmetic.Author);
                // if (cosmetic.Author != authorBefore) EditorUtility.SetDirty(targetObj);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Type");
                CosmeticType typeBefore = cosmetic.Type;
                cosmetic.Type = (CosmeticType) EditorGUILayout.EnumPopup(cosmetic.Type);
                if (cosmetic.Type != typeBefore) {
                    cosmetic.Cosmetic = null;
                    EditorUtility.SetDirty(targetObj);
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Cosmetic");
                // Object cosmeticBefore = cosmetic.Cosmetic;
                cosmetic.Cosmetic = EditorGUILayout.ObjectField(cosmetic.Cosmetic, cosmetic.TypeType, false);
                // if (cosmetic.Cosmetic != cosmeticBefore) EditorUtility.SetDirty(targetObj);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Id");
                cosmetic.Id = (uint) Math.Abs(EditorGUILayout.IntField((int) cosmetic.Id));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Registered");
                EditorGUILayout.Toggle(cosmetic.Registered);
                EditorGUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (!cosmetic.Registered) {
                    if (GUILayout.Button("Delete")) {
                        targetObj.Cosmetics = targetObj.Cosmetics.Where((_, j) => i != j).ToArray();
                        i--;
                    }
                } else {
                    if (GUILayout.Button("Repair collisions")) {
                        EditorCoroutineUtility.StartCoroutineOwnerless(Fix(targetObj, cosmetic));
                    }
                }

                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndChangeCheck();

            bool fullyRegistered = targetObj.Registered && targetObj.Cosmetics.All(cosmetic => cosmetic.Registered);

            if (GUILayout.Button(fullyRegistered ? "Update bundle" : "Register bundle")) {
                if (targetObj.Cosmetics.Length == 0) {
                    EditorUtility.DisplayDialog("Registration failure", "There are no cosmetics in this bundle", "cog");
                } else EditorCoroutineUtility.StartCoroutineOwnerless(UploadAll(targetObj));
            }

            GUILayout.Space(100);

            if ((targetObj.Registered || targetObj.Cosmetics.Any(cosmetic => cosmetic.Registered))
                && GUILayout.Button("DEBUG: Unregister Bundle")
                && EditorUtility.DisplayDialog(
                    "Unregister bundle",
                    "Are you sure you want to unregister this bundle?\nThis is a debug button and shouldn't be done unless\nrequested as it will mess your bundle up",
                    "Yes",
                    "No.",
                    DialogOptOutDecisionType.ForThisMachine,
                    "cosmeticUnregisterDialog"
                )
            ) {
                targetObj.Registered = false;
                foreach (CosmeticBundleObject.CosmeticData cosmetic in targetObj.Cosmetics) {
                    cosmetic.Registered = false;
                }

                EditorUtility.SetDirty(targetObj);
            }
        }

        public GameObject CreatePetBehaviour(CosmeticBundleObject.CosmeticData data, out PetBehaviour clone) {
            clone = Instantiate(AssetDatabase.LoadAssetAtPath<PetBehaviour>("Assets/Mods/BundleCosmetics/BasePet.prefab"));
            PetCreator petCreator = (PetCreator) data.Cosmetic;

            clone.name = $"<temp>{data.Cosmetic.name}";
            clone.YOffset = petCreator.yOffset;
            clone.idleClip = petCreator.idleClip;
            clone.walkClip = petCreator.walkClip;
            clone.sadClip = petCreator.sadClip;
            clone.scaredClip = petCreator.scaredClip;
            clone.rend.sprite = data.Thumbnail;
            clone.shadowRend.gameObject.SetActive(false);

            if (!Directory.Exists($"{bundleRoot}/PetTemp")) Directory.CreateDirectory($"{bundleRoot}/PetTemp");

            GameObject obj = PrefabUtility.SaveAsPrefabAsset(clone.gameObject, $"{bundleRoot}/PetTemp/{petCreator.name}.prefab");

            return obj;
        }

        private IEnumerator UploadSingle(CosmeticBundleObject.CosmeticData cosmetic) {
            if (cosmetic.Thumbnail == null) {
                Debug.LogError("No thumbnail provided");
                EditorUtility.ClearProgressBar();
                yield break;
            }

            AssetBundleResource bundleResource = CreateInstance<AssetBundleResource>();
            bundleResource.name = $"{targetObj.Name}_{cosmetic.Name}";
            bundleResource.BaseId = cosmetic.Id;
            PetBehaviour pet = null;
            bundleResource.Assets = cosmetic.Type == CosmeticType.Pet ? new Object[] { CreatePetBehaviour(cosmetic, out pet) } : new[] { cosmetic.Cosmetic };
            AssetBundleResourceEditor.BuildResult buildResult = AssetBundleResourceEditor.Build(bundleResource, bundleRoot);
            if (cosmetic.Type == CosmeticType.Pet) {
                AssetDatabase.DeleteAsset($"{bundleRoot}/PetTemp/{cosmetic.Cosmetic.name}.prefab");
                DestroyImmediate(pet.gameObject);
            }

            // EditorUtility.DisplayProgressBar("Uploading your mom", "(she's really really large)", 0.21f);
            Task task = OceanClient.Upload(new OceanClient(),
                OceanClient.BundleBucket, OceanClient.FormatUrl("Cosmetics", targetObj.Name, cosmetic.Name), File.OpenRead($"{bundleRoot}/{buildResult.Manifest.GetAllAssetBundles()[0]}"));
            while (!task.IsCompleted)
                yield return null;

            Debug.Log($"Faulted 1? {task.IsFaulted}");
            if (task.IsFaulted) throw task.Exception;

            task = OceanClient.Upload(new OceanClient(),
                OceanClient.BundleBucket, OceanClient.FormatUrl("Cosmetics", targetObj.Name, cosmetic.Name + ".json"), File.OpenRead(buildResult.JsonManifest));
            while (!task.IsCompleted)
                yield return null;

            Debug.Log($"Faulted 1.5? {task.IsFaulted}");
            if (task.IsFaulted) throw task.Exception;

            switch (cosmetic.Type) {
                case CosmeticType.Hat:
                    if (((HatBehaviour)cosmetic.Cosmetic).MainImage != null) {
                        task = OceanClient.Upload(new OceanClient(),
                            OceanClient.ThumbnailBucket, OceanClient.FormatUrl(targetObj.Name, cosmetic.Name, "front.png"), File.OpenRead(AssetDatabase.GetAssetPath(((HatBehaviour) cosmetic.Cosmetic).MainImage)));
                        while (!task.IsCompleted)
                            yield return null;
                        Debug.Log($"Faulted 2? {task.IsFaulted}");
                        if (task.IsFaulted) throw task.Exception;
                    }

                    if (((HatBehaviour) cosmetic.Cosmetic).BackImage != null) {
                        task = OceanClient.Upload(new OceanClient(),
                            OceanClient.ThumbnailBucket, OceanClient.FormatUrl(targetObj.Name, cosmetic.Name, "back.png"), File.OpenRead(AssetDatabase.GetAssetPath(((HatBehaviour) cosmetic.Cosmetic).BackImage)));
                        while (!task.IsCompleted)
                            yield return null;
                        Debug.Log($"Faulted 2? {task.IsFaulted}");
                        if (task.IsFaulted) throw task.Exception;
                    }

                    break;
                case CosmeticType.Pet:
                    task = OceanClient.Upload(new OceanClient(),
                        OceanClient.ThumbnailBucket, OceanClient.FormatUrl(targetObj.Name, cosmetic.Name, "pet.png"), File.OpenRead(AssetDatabase.GetAssetPath(cosmetic.Thumbnail)));
                    while (!task.IsCompleted)
                        yield return null;
                    Debug.Log($"Faulted 2? {task.IsFaulted}");
                    if (task.IsFaulted) throw task.Exception;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // task = OceanClient.Purge(new[] {
            //     OceanClient.FormatUrl("Cosmetics", targetObj.Name, cosmetic.Name),
            //     OceanClient.FormatUrl("Cosmetics", targetObj.Name, cosmetic.Name + ".json"),
            // }, OceanClient.BundleLocation);
            // while (!task.IsCompleted)
            //     yield return null;
            // Debug.Log($"Faulted 2.5? {task.IsFaulted}");
            // if (task.IsFaulted) throw task.Exception;

            task = cosmetic.Registered
                ? CosmeticClient.Client.UpdateItem(targetObj.Name, cosmetic)
                : CosmeticClient.Client.UploadItem(targetObj.Name, cosmetic);
            while (!task.IsCompleted)
                yield return null;
            Debug.Log($"Faulted 3? {task.IsFaulted}");
            if (task.IsFaulted) throw task.Exception;

            cosmetic.Registered = true;
            EditorUtility.SetDirty(targetObj);
            Debug.Log($"registered {cosmetic.Name} {cosmetic.Registered} {targetObj}");
        }

        // grabbed off SO LOL https://stackoverflow.com/a/2571393/13161523
        public bool TryParseGuid(string value, out Guid result) {
            try {
                result = new Guid(value.Replace("-", "")); // needed to cater for wron hyphenation (wow this guy really failed at spelling damn -sanae)
                return true;
            } catch {
                result = Guid.Empty;
                return false;
            }
        }

        public static void ClearLogConsole() {
            Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type logEntries = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
            clearConsoleMethod.Invoke(new object(), null);
        }

        private static bool conflicted;
        private IEnumerator Fix(CosmeticBundleObject bundle, CosmeticBundleObject.CosmeticData cosmetic) {
            ClearLogConsole();

            Task task = CheckForConflicts(cosmetic);
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;
            if (conflicted) {
                Debug.LogWarning($"Fetching new id for {cosmetic.Name}");
                task = Fetch(cosmetic);
                while (!task.IsCompleted) yield return null;
                if (task.IsFaulted) throw task.Exception;
                task = CosmeticClient.Client.UpdateItem(targetObj.Name, cosmetic);
                while (!task.IsCompleted)
                    yield return null;
                if (task.IsFaulted) throw task.Exception;
                Debug.Log("Done repairing the cosmetic!");
            } else {
                Debug.Log($"{cosmetic.Name} had no conflicts!");
            }
            EditorUtility.SetDirty(bundle);
        }

        private IEnumerator UploadAll(CosmeticBundleObject bundle) {
            ClearLogConsole();
            yield return null;

            if (!AccountMenu.HasSave) {
                EditorUtility.DisplayDialog("Error", "You are not logged in.", "Ok");
                EditorWindow.GetWindow<AccountMenu>();
                yield break;
            }

            if (bundle.CoverArt == null) {
                EditorUtility.DisplayDialog("Error", "No cover art provided.", "Ok");
                yield break;
            }

            if (bundle.Cosmetics.Any(cosmetic => cosmetic.Cosmetic == null)) {
                EditorUtility.DisplayDialog("Error", $"Not all cosmetics have a hat/pet attached to them\nMissing are {string.Join(", ", bundle.Cosmetics.Where(cosmetic => cosmetic.Cosmetic == null).Select(c => c.Name))}", "Ok");
                yield break;
            }

            if (bundle.Cosmetics.Any(cosmetic => !TryParseGuid(cosmetic.Author, out Guid _))) {
                EditorUtility.DisplayDialog("Error", $"Not all cosmetics have an author uuid attached them\nMissing are {string.Join(", ", bundle.Cosmetics.Where(cosmetic => !TryParseGuid(cosmetic.Author, out Guid _)).Select(c => c.Name))}", "Ok");
                yield break;
            }

            EditorUtility.DisplayProgressBar("Progress bar", "progress bar :)", 0.0f);
            foreach (CosmeticBundleObject.CosmeticData cosmeticData in bundle.Cosmetics) {
                if (!cosmeticData.Registered && cosmeticData.Id < 10000000) {
                    Task fetch = Fetch(cosmeticData);
                    while (!fetch.IsCompleted) yield return null;
                    if (fetch.IsFaulted) throw fetch.Exception;
                }

                // IEnumerator upload = UploadSingle(cosmeticData);
                yield return EditorCoroutineUtility.StartCoroutineOwnerless(UploadSingle(cosmeticData));
                lock (lockable) Debug.Log($"sus {cosmeticData.Name} {cosmeticData.Registered}");
            }

            if (!bundle.Cosmetics.All(cosmetic => cosmetic.Registered)) {
                EditorUtility.DisplayDialog("Error", $"Not all cosmetics have been uploaded\nMissing are {string.Join(", ", bundle.Cosmetics.Where(cosmetic => !cosmetic.Registered).Select(c => c.Name))}", "Ok");
                yield break;
            }

            string assetPath = AssetDatabase.GetAssetPath(bundle.CoverArt);
            Task task = OceanClient.Upload(new OceanClient(), OceanClient.ThumbnailBucket,
                OceanClient.FormatName(bundle.Name, "cover.png"), File.OpenRead(assetPath));
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;

            task = bundle.Registered ? CosmeticClient.Client.UpdateBundle(bundle) : CosmeticClient.Client.UploadBundle(bundle);
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;

            bundle.Registered = true;
            Debug.Log("fully registered");

            EditorUtility.ClearProgressBar();
        }

        public static async Task Fetch(CosmeticBundleObject.CosmeticData data) {
            GenericCosmeticResponse<uint> genericCosmeticResponse = await CosmeticClient.Client.NextCosmeticId();
            if (genericCosmeticResponse.Ok) {
                data.Id = genericCosmeticResponse.Data;
            }
        }

        public static async Task CheckForConflicts(CosmeticBundleObject.CosmeticData data) {
            GenericCosmeticResponse<bool> genericCosmeticResponse = await CosmeticClient.Client.CheckConflicts(new ItemCreation {
                InGameId = data.Id,
            });
            conflicted = genericCosmeticResponse.Data;
        }

        private void OnDisable() {
            Coroutines = new List<IEnumerator>();
        }
    }
}