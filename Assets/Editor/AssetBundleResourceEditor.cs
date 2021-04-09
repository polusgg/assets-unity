using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    [CustomEditor(typeof(AssetBundleResource))]
    public class AssetBundleResourceEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (GUILayout.Button("Build")) {
                if (!Directory.Exists("Assets/AssetBundles/PggResources"))
                    Directory.CreateDirectory("Assets/AssetBundles/PggResources");

                AssetBundleResource resource = (AssetBundleResource) serializedObject.targetObject;

                if (resource.Assets.Length == 0)
                    return;

                var jsonPath = $"Assets/AssetBundles/PggResources/{resource.name}-Manifest.json";
                File.WriteAllText(jsonPath, JsonUtility.ToJson(
                    new SerializableAssetBundleResource(resource)
                ));

                SerializableForRose rose = new SerializableForRose();
                rose.AssetBundleId = resource.BaseId;
                rose.Assets = resource.Assets.Select(asset => {
                    AssetDecl decl = new AssetDecl();

                    if (asset is AudioClip) {
                        var audio = asset as AudioClip;
                        decl.Details = new AudioDetails {
                            Samples = audio.samples,
                            SampleRate = audio.samples / audio.length,
                        };
                        decl.Type = AssetType.Audio;
                    } else {
                        decl.Type = AssetType.Other;
                    }

                    decl.Name = asset.name;

                    using (FileStream fs = File.OpenRead(AssetDatabase.GetAssetPath(asset))) {
                        decl.Hash = MD5.Create().ComputeHash(fs);
                    }

                    return decl;
                }).ToArray();

                var rosePath = $"Assets/AssetBundles/PggResources/{resource.name}-Rose.json";
                File.WriteAllText(rosePath, JsonConvert.SerializeObject(rose, new JsonSerializerSettings {
                    ContractResolver = new DefaultContractResolver {
                        NamingStrategy = new CamelCaseNamingStrategy(),
                    },
                    Formatting = Formatting.Indented
                }));

                BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PggResources",
                    new[] {
                        new AssetBundleBuild {
                            assetNames = resource.Assets.Select(AssetDatabase.GetAssetPath).Append(jsonPath).ToArray(),
                            addressableNames = resource.Assets.Select(AssetDatabase.GetAssetPath).Append("Assets/AssetListing.json").ToArray(),
                            assetBundleName = resource.name
                        }
                    },
                    BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows
                );
            }
        }

        public struct SerializableAssetBundleResource {
            public uint BaseId;
            public string[] Assets;
            public SerializableAssetBundleResource(AssetBundleResource assetBundleResource) {
                BaseId = assetBundleResource.BaseId;
                Assets = assetBundleResource.Assets.Select(AssetDatabase.GetAssetPath).ToArray();
            }
        }

        public enum AssetType {
            Other = 0,
            Audio
        }
        public class SerializableForRose {
            public uint AssetBundleId;
            public AssetDecl[] Assets;
        }

        public class AssetDecl {
            public AssetType Type;
            public string Name;
            public byte[] Hash;
            public Details Details;
        }

        public class Details {}
        public class AudioDetails : Details {
            public int Samples;
            public float SampleRate;
        }
    }
}