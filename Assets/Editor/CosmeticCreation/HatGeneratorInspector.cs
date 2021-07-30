using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Editor.HatCreator {
    [CustomEditor(typeof(HatGeneratorObject))]
    public class HatGeneratorInspector : UnityEditor.Editor {
        private HatGeneratorObject targetObj => (HatGeneratorObject) serializedObject.targetObject;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (GUILayout.Button("Load all from folder")) {
                string folder = EditorUtility.OpenFolderPanel("Asset folder", "Assets", null);
                if (!string.IsNullOrEmpty(folder)) {
                    targetObj.HatSprites = (Sprite[]) Directory.EnumerateFiles(folder)
                        .Select(x => x.Substring(x.IndexOf("Assets/", StringComparison.Ordinal)))
                        .Select(AssetDatabase.LoadAssetAtPath<Object>)
                        .Where(x => x != null && x != targetObj)
                        .Concat(targetObj.HatSprites)
                        .ToArray();
                }
            }

            if (GUILayout.Button("Dedupe Assets"))
                targetObj.HatSprites = targetObj.DistinctAssets.ToArray();

            if (GUILayout.Button("Generate")) {
                if (!Directory.Exists(targetObj.AssetPath))
                    Directory.CreateDirectory(targetObj.AssetPath);

                foreach (Sprite sprite in targetObj.DistinctAssets) {
                    HatBehaviour hatBehaviour = CreateInstance<HatBehaviour>();
                    hatBehaviour.MainImage = sprite;

                    hatBehaviour.FloorImage = sprite;
                    hatBehaviour.ClimbImage = sprite;

                    hatBehaviour.InFront = true;
                    hatBehaviour.NoBounce = false;
                    hatBehaviour.NotInStore = true;
                    hatBehaviour.Free = true;
                    hatBehaviour.ChipOffset = new Vector2(0, 0.8f);

                    string uniqueName = AssetDatabase.GenerateUniqueAssetPath(sprite.name);
                    AssetDatabase.CreateAsset(hatBehaviour, $"{targetObj.AssetPath}/{uniqueName}.asset");
                }

                AssetDatabase.SaveAssets();
            }
        }
    }
}