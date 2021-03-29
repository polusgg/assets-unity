using System.IO;
using System.Linq;
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

                BuildPipeline.BuildAssetBundles("Assets/AssetBundles/PggResources",
                    new[] {
                        new AssetBundleBuild {
                            assetNames = resource.Assets.Select(AssetDatabase.GetAssetPath).Append(jsonPath).ToArray(),
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
    }
}