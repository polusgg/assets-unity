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

                var dedupedResource = new AssetBundleResource
                {
                    BaseId = resource.BaseId,
                    Assets = resource.DistinctAssets.ToArray()
                };

                var serialzationOpts = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };


                try
                {
                    var jsonPath = $"Assets/AssetBundles/PggResources/{resource.name}-Manifest.json";
                    File.WriteAllText(jsonPath, JsonConvert.SerializeObject(
                        new SerializableResourceForClient(dedupedResource), serialzationOpts
                    ));

                    var bundles = BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PggResources",
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

                    var nodepolusSerializable = GenerateSerializableForNodePolus(dedupedResource);

                    using (var file =
                        File.OpenRead($"Assets/AssetBundles/PggResources/{bundles.GetAllAssetBundles()[0]}"))
                    {
                        nodepolusSerializable.Hash = file.MD5Hash().Select(x => (int) x).ToArray();
                    }

                    var nodepolusJsonPath = $"Assets/AssetBundles/PggResources/{resource.name}.json";
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
                var folder = EditorUtility.OpenFolderPanel("Asset folder", "Assets", null);
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
            var serializableResource = new SerializableForNodePolus();
            serializableResource.AssetBundleId = resource.BaseId;
            serializableResource.Assets = resource.Assets.Select(asset => {
                var decl = new AssetDecl
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