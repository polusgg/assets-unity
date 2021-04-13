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
            base.OnInspectorGUI();
            AssetBundleResource resource = (AssetBundleResource) serializedObject.targetObject;

            if (GUILayout.Button("Build")) {
                if (!Directory.Exists("Assets/AssetBundles/PggResources"))
                    Directory.CreateDirectory("Assets/AssetBundles/PggResources");

                if (resource.Assets.Length == 0)
                    return;

                Object[] original = resource.Assets;

                try {
                    var jsonPath = $"Assets/AssetBundles/PggResources/{resource.name}-Manifest.json";
                    File.WriteAllText(jsonPath, JsonUtility.ToJson(
                        new SerializableAssetBundleResource(resource)
                    ));

                    SerializableForRose rose = new SerializableForRose();
                    rose.AssetBundleId = resource.BaseId;
                    rose.Assets = resource.Assets.Select(asset => {
                        AssetDecl decl = new AssetDecl();

                        if (asset is AudioClip audio) {
                            decl.Details = new AudioDetails {
                                Samples = audio.samples,
                                SampleRate = audio.samples / audio.length,
                            };
                            decl.Type = AssetType.Audio;
                        } else {
                            decl.Type = AssetType.Other;
                        }

                        decl.Name = AssetDatabase.GetAssetPath(asset);

                        return decl;
                    }).ToArray();

                    resource.Assets = resource.Assets.Distinct(new ObjectComparer()).ToArray();

                    AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PggResources",
                        new[] {
                            new AssetBundleBuild {
                                assetNames = resource.Assets.Select(AssetDatabase.GetAssetPath).Append(jsonPath)
                                    .ToArray(),
                                addressableNames = resource.Assets.Select(AssetDatabase.GetAssetPath)
                                    .Append("Assets/AssetListing.json").ToArray(),
                                assetBundleName = resource.name
                            }
                        },
                        BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows
                    );

                    using (FileStream fs =
                        File.OpenRead("Assets/AssetBundles/PggResources/" + manifest.GetAllAssetBundles()[0])) {
                        rose.Hash = MD5.Create().ComputeHash(fs).Select(x => (int)x).ToArray();
                    }

                    var rosePath = $"Assets/AssetBundles/PggResources/{resource.name}.json";
                    File.WriteAllText(rosePath, JsonConvert.SerializeObject(rose, new JsonSerializerSettings {
                        ContractResolver = new DefaultContractResolver {
                            NamingStrategy = new CamelCaseNamingStrategy(),
                        },
                        Formatting = Formatting.Indented,
                        Converters = { new ByteArrayConverter() }
                    }));
                } finally {
                    resource.Assets = original;
                }
            }

            //funny one statement add to resource assets list
            if (GUILayout.Button("Load all from folder"))
                resource.Assets = Directory.GetFiles(EditorUtility.OpenFolderPanel("Asset folder", "Assets", ""))
                    .Select(x => x.Substring(x.IndexOf("Assets/", StringComparison.Ordinal)))
                    .Select(AssetDatabase.LoadAssetAtPath<Object>)
                    .Concat(resource.Assets)
                    .Where(x => x != null)
                    .Distinct(new ObjectComparer())
                    .ToArray();
        }

        public class ObjectComparer : IEqualityComparer<Object> {
            public bool Equals(Object x, Object y) {
                return AssetDatabase.GetAssetPath(x) == AssetDatabase.GetAssetPath(y);
            }

            public int GetHashCode(Object obj) {
                return AssetDatabase.GetAssetPath(obj).GetHashCode();
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
            public int[] Hash;
            public AssetDecl[] Assets;
        }

        public class AssetDecl {
            public AssetType Type;
            public string Name;
            public Details Details;
        }

        public class Details { }

        public class AudioDetails : Details {
            public int Samples;
            public float SampleRate;
        }

        public class ByteArrayConverter : JsonConverter {
            public override object ReadJson(
                JsonReader reader,
                Type objectType,
                object existingValue,
                JsonSerializer serializer) {
                throw new NotImplementedException();
            }

            public override void WriteJson(
                JsonWriter writer,
                object value,
                JsonSerializer serializer) {
                string base64String = Convert.ToBase64String((byte[]) value);

                serializer.Serialize(writer, base64String);
            }

            public override bool CanRead {
                get { return false; }
            }

            public override bool CanConvert(Type t) {
                return typeof(byte[]).IsAssignableFrom(t);
            }
        }
    }
}