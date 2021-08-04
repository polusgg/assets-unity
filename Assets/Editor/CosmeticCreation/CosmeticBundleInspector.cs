using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Cosmetics;
using Editor.Accounts;
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

        IEnumerator FetchId(CosmeticBundleObject.CosmeticData cosmetic) {
            EditorUtility.DisplayProgressBar("Fetching your mom", "(she's really far away)", 0.420f);
            Task task = Fetch(cosmetic);
            while (!task.IsCompleted) yield return null;
            EditorUtility.ClearProgressBar();
            if (task.IsFaulted) throw task.Exception;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (GUILayout.Button("Add slot")) {
                // targetObj.Cosmetics = targetObj.Cosmetics.Append(CreateInstance<CosmeticBundleObject.CosmeticData>()).ToArray();
                targetObj.Cosmetics = targetObj.Cosmetics.Append(new CosmeticBundleObject.CosmeticData()).ToArray();
            }

            for (int i = 0; i < targetObj.Cosmetics.Length; i++) {
                CosmeticBundleObject.CosmeticData cosmetic = targetObj.Cosmetics[i];
                cosmetic.foldedOut = EditorGUILayout.Foldout(cosmetic.foldedOut, $"Element {i} ({(string.IsNullOrEmpty(cosmetic.Name) ? "No name" : cosmetic.Name)})");
                if (!cosmetic.foldedOut) continue;
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Name");
                string nameBefore = cosmetic.Name;
                cosmetic.Name = EditorGUILayout.TextField(cosmetic.Name);
                if (cosmetic.Name != nameBefore) EditorUtility.SetDirty(targetObj);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Cosmetic");
                Cosmetic cosmeticBefore = cosmetic.Cosmetic;
                cosmetic.Cosmetic = (Cosmetic) EditorGUILayout.ObjectField(cosmetic.Cosmetic, typeof(Cosmetic), false);
                if (cosmetic.Cosmetic != cosmeticBefore) EditorUtility.SetDirty(targetObj);
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
                    // if (GUILayout.Button("Fetch new ID")) {
                    //     Coroutines.Add(FetchId(cosmetic));
                    // }
                    //
                    // if (GUILayout.Button("Register item")) {
                    //     EditorUtility.DisplayProgressBar("Building asset bundle", "(your mom slept in)", 0.1f);
                    //     
                    //     UploadSingle(cosmetic, EditorUtility.ClearProgressBar);
                    // }

                    if (GUILayout.Button("Delete")) {
                        targetObj.Cosmetics = targetObj.Cosmetics.Where((_, j) => i != j).ToArray();
                        i--;
                    }
                }

                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            if (!targetObj.Registered && GUILayout.Button("Register bundle")) {
                if (targetObj.Cosmetics.Length == 0) {
                    EditorUtility.DisplayDialog("Registration failure", "There are no cosmetics in this bundle", "Ok");
                } else Coroutines.Add(UploadAll(targetObj));
            } else if (targetObj.Registered && GUILayout.Button("Unregister Bundle")) {
                targetObj.Registered = true;
            }

            if (Coroutines.Count > 0) {
                for (int i = 0; i < Coroutines.Count; i++) {
                    IEnumerator coroutine = Coroutines[i];
                    try {
                        if (!coroutine.MoveNext()) {
                            if (Coroutines.Remove(coroutine)) i--;
                        }
                    } catch {
                        EditorUtility.ClearProgressBar();
                        Coroutines.Remove(coroutine);
                        throw;
                    }
                }
            }
        }

        private void UploadSingle(CosmeticBundleObject.CosmeticData cosmetic, Action callback = null) {
            if (cosmetic.Thumbnail == null) {
                Debug.LogError("No thumbnail provided");
                EditorUtility.ClearProgressBar();
                return;
            }

            if (cosmetic.Type == CosmeticType.Unknown) {
                Debug.LogError("Unknown cosmetic type!");
                EditorUtility.ClearProgressBar();
                return;
            }
            AssetBundleResource bundleResource = CreateInstance<AssetBundleResource>();
            bundleResource.name = $"{targetObj.Name}_{cosmetic.Name}";
            bundleResource.BaseId = cosmetic.Id;
            bundleResource.Assets = new Object[] {cosmetic.Cosmetic};
            AssetBundleResourceEditor.BuildResult buildResult = AssetBundleResourceEditor.Build(bundleResource, bundleRoot);
            if (buildResult != null) {
                Coroutines.Add(UploadSingle(cosmetic, $"{bundleRoot}/{buildResult.Manifest.GetAllAssetBundles()[0]}", callback));
            }
        }

        private IEnumerator UploadSingle(CosmeticBundleObject.CosmeticData cosmetic, string location, Action callback = null) {
            // EditorUtility.DisplayProgressBar("Uploading your mom", "(she's really really large)", 0.21f);
            Task task = BundleS3Client.Upload(new BundleS3Client(),
                BundleS3Client.BundleBucket, BundleS3Client.FormatUrl("Cosmetics", targetObj.Name, cosmetic.Name), File.OpenRead(location));
            while (!task.IsCompleted)
                yield return null;
            Debug.Log($"Faulted 1? {task.IsFaulted}");
            if (task.IsFaulted) throw task.Exception;

            task = BundleS3Client.Upload(new BundleS3Client(),
                BundleS3Client.ThumbnailBucket, BundleS3Client.FormatName(targetObj.Name, Path.GetFileName(AssetDatabase.GetAssetPath(cosmetic.Thumbnail))), File.OpenRead(AssetDatabase.GetAssetPath(cosmetic.Thumbnail)));
            while (!task.IsCompleted)
                yield return null;
            Debug.Log($"Faulted 2? {task.IsFaulted}");
            if (task.IsFaulted) throw task.Exception;

            task = cosmetic.Registered 
                ? CosmeticClient.Client.UpdateItem(targetObj.Name, cosmetic)
                : CosmeticClient.Client.UploadItem(targetObj.Name, cosmetic);
            while (!task.IsCompleted)
                yield return null;

            callback?.Invoke();
            Debug.Log($"Faulted 3? {task.IsFaulted}");
            if (task.IsFaulted) throw task.Exception;

            cosmetic.Registered = true;
        }

        private IEnumerator UploadAll(CosmeticBundleObject bundle) {
            if (bundle.CoverArt == null) {
                Debug.LogError("No cover art provided");
                yield break;
            }

            if (bundle.Cosmetics.Any(cosmetic => cosmetic.Cosmetic == null)) {
                Debug.LogError("Not all cosmetics have a hat/pet attached to them");
                yield break;
            }
            EditorUtility.DisplayProgressBar("Progress bar", "progress bar :)", 0.0f);
            foreach (CosmeticBundleObject.CosmeticData cosmeticData in bundle.Cosmetics) {
                if (cosmeticData.Registered)
                    continue;
                if (cosmeticData.Id < 10000000) {
                    Task fetch = Fetch(cosmeticData);
                    while (!fetch.IsCompleted) yield return null;
                    if (fetch.IsFaulted) throw fetch.Exception;
                }

                bool isDone = false;
                UploadSingle(cosmeticData, () => { isDone = true; });
                while (!isDone) {
                    yield return null;
                }
            }//BundleS3Client.FormatUrl(BundleS3Client.ThumbnailLocation + "/CoverArt", bundle.Name, Path.GetFileName(AssetDatabase.GetAssetPath(bundle.CoverArt)))

            string assetPath = AssetDatabase.GetAssetPath(bundle.CoverArt);
            Task task = BundleS3Client.Upload(new BundleS3Client(), BundleS3Client.ThumbnailBucket,
                BundleS3Client.FormatUrl("CoverArt", bundle.Name, Path.GetFileName(assetPath)), File.OpenRead(assetPath));
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;

            task = bundle.Registered ?
                CosmeticClient.Client.UpdateBundle(bundle) :
                CosmeticClient.Client.UploadBundle(bundle);
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;
            bundle.Registered = true;

            EditorUtility.ClearProgressBar();
        }

        public static async Task Fetch(CosmeticBundleObject.CosmeticData data) {
            GenericCosmeticResponse<uint> genericCosmeticResponse = await CosmeticClient.Client.NextCosmeticId();
            if (genericCosmeticResponse.Ok) {
                data.Id = genericCosmeticResponse.Data;
            }
        }

        private void OnDisable() {
            Coroutines = new List<IEnumerator>();
        }
    }
}