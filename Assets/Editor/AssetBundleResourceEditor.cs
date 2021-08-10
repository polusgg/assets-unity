using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Editor {
    [CustomEditor(typeof(AssetBundleResource))]
    public class AssetBundleResourceEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            AssetBundleResource resource = (AssetBundleResource) serializedObject.targetObject;

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Add slot")) {
                resource.Assets = resource.Assets.Concat(new Object[1]).ToArray();
            }

            if (GUILayout.Button("Remove last slot")) {
                resource.Assets = resource.Assets.Take(resource.Assets.Length - 1).ToArray();
            }

            if (GUILayout.Button("Build")) {
                const string buildRoot = "Assets/AssetBundles/PggResources";
                BuildResult result = Build(resource, buildRoot);
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Dedupe Assets"))
                resource.Assets = resource.DistinctAssets.ToArray();

            if (GUILayout.Button("Load all from folder")) {
                string folder = EditorUtility.OpenFolderPanel("Asset folder", "Assets", null);
                if (!string.IsNullOrEmpty(folder)) {
                    resource.Assets = Directory.EnumerateFiles(folder)
                        .Select(x => x.Substring(x.IndexOf("Assets/", StringComparison.Ordinal)))
                        .Select(AssetDatabase.LoadAssetAtPath<Object>)
                        .Where(x => x != null && x != resource)
                        .Concat(resource.Assets)
                        .ToArray();
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            base.OnInspectorGUI();
        }

        public static SerializableForNodePolus GenerateSerializableForNodePolus(AssetBundleResource resource) {
            SerializableForNodePolus serializableResource = new SerializableForNodePolus {
                AssetBundleId = resource.BaseId,
                Assets = resource.Assets.Select(asset => {
                    AssetDecl decl = new AssetDecl {Path = AssetDatabase.GetAssetPath(asset)};

                    if (asset is AudioClip audio) {
                        decl.Type = AssetType.Audio;
                        decl.Details = new AudioDetails {Samples = audio.samples, SampleRate = audio.samples / audio.length,};
                    } else {
                        decl.Type = AssetType.Other;
                    }

                    return decl;
                }).ToArray()
            };
            return serializableResource;
        }

        private static JsonSerializerSettings serializationOpts => new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
        };

        public class BuildResult {
            public AssetBundleManifest Manifest;
            public AssetBundleResource DedupedResource;
            public string JsonManifest;
        }

        public static BuildResult Build(AssetBundleResource resource, string buildRoot) {
            if (!Directory.Exists(buildRoot))
                Directory.CreateDirectory(buildRoot);

            if (resource.Assets.Length == 0) {
                Debug.LogWarning("No resources in the bundle.");
                return null;
            }

            AssetBundleResource dedupedResource = CreateInstance<AssetBundleResource>();
            dedupedResource.BaseId = resource.BaseId;
            dedupedResource.Assets = resource.DistinctAssets.ToArray();

            try {
                string jsonPath = $"{buildRoot}/{resource.name}-Manifest.json";
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(
                    new SerializableResourceForClient(dedupedResource), serializationOpts
                ));
                AssetDatabase.ImportAsset(jsonPath);
                AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(buildRoot,
                    new[] {
                        new AssetBundleBuild {
                            assetNames = dedupedResource.Assets.Select(AssetDatabase.GetAssetPath).Append(jsonPath)
                                .ToArray(),
                            addressableNames = dedupedResource.Assets.Select(AssetDatabase.GetAssetPath)
                                .Append("Assets/AssetListing.json").ToArray(),
                            assetBundleName = resource.name
                        }
                    },
                    BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows
                );

                SerializableForNodePolus nodepolusSerializable = GenerateSerializableForNodePolus(dedupedResource);

                using (FileStream file =
                    File.OpenRead($"{buildRoot}/{manifest.GetAllAssetBundles()[0]}")) {
                    nodepolusSerializable.Hash = file.SHA256Hash();
                }

                string nodepolusJsonPath = $"{buildRoot}/{resource.name}.json";
                File.WriteAllText(nodepolusJsonPath,
                    JsonConvert.SerializeObject(nodepolusSerializable, serializationOpts));
                
                Debug.Log(manifest);

                return new BuildResult {
                    DedupedResource = dedupedResource,
                    Manifest = manifest,
                    JsonManifest = nodepolusJsonPath
                };
            } catch (Exception e) {
                Debug.LogError($"Error when generating AssetBundleResource: {e.Message}.\n {e.StackTrace}");
            }

            return null;
        }
    }
}