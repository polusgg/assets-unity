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

                AssetBundleResource dedupedResource = CreateInstance<AssetBundleResource>();
                dedupedResource.BaseId = resource.BaseId;
                dedupedResource.Assets = resource.DistinctAssets.ToArray();

                JsonSerializerSettings serialzationOpts = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };


                try
                {
                    string jsonPath = $"Assets/AssetBundles/PggResources/{resource.name}-Manifest.json";
                    File.WriteAllText(jsonPath, JsonConvert.SerializeObject(
                        new SerializableResourceForClient(dedupedResource), serialzationOpts
                    ));

                    AssetBundleManifest bundles = BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PggResources",
                        new[]
                        {
                            new AssetBundleBuild
                            {
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
                        File.OpenRead($"Assets/AssetBundles/PggResources/{bundles.GetAllAssetBundles()[0]}"))
                    {
                        nodepolusSerializable.Hash = file.SHA256Hash();
                    }

                    string nodepolusJsonPath = $"Assets/AssetBundles/PggResources/{resource.name}.json";
                    File.WriteAllText(nodepolusJsonPath,
                        JsonConvert.SerializeObject(nodepolusSerializable, serialzationOpts));
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error when generating AssetBundleResource: {e.Message}.\n {e.StackTrace}");
                }
            }

            if (GUILayout.Button("Dedupe Assets"))
                resource.Assets = resource.DistinctAssets.ToArray();

            if (GUILayout.Button("Load all from folder"))
            {
                string folder = EditorUtility.OpenFolderPanel("Asset folder", "Assets", null);
                if (!string.IsNullOrEmpty(folder))
                {
                    resource.Assets = Directory.EnumerateFiles(folder)
                        .Select(x => x.Substring(x.IndexOf("Assets/", StringComparison.Ordinal)))
                        .Select(AssetDatabase.LoadAssetAtPath<Object>)
                        .Where(x => x != null && x != resource)
                        .Concat(resource.Assets)
                        .ToArray();
                }
            }
        }

        public static SerializableForNodePolus GenerateSerializableForNodePolus(AssetBundleResource resource)
        {
            SerializableForNodePolus serializableResource = new SerializableForNodePolus();
            serializableResource.AssetBundleId = resource.BaseId;
            serializableResource.Assets = resource.Assets.Select(asset => {
                AssetDecl decl = new AssetDecl
                {
                    Path = AssetDatabase.GetAssetPath(asset)
                };

                if (asset is AudioClip audio) {
                    decl.Type = AssetType.Audio;
                    decl.Details = new AudioDetails {
                        Samples = audio.samples,
                        SampleRate = audio.samples / audio.length,
                    };
                } else {
                    decl.Type = AssetType.Other;
                }

                return decl;
            }).ToArray();
            return serializableResource;
        }
    }
}