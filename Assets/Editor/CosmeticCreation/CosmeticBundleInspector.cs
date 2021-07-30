using UnityEditor;
using UnityEngine;

namespace Assets.Editor.HatCreator {
    [CustomEditor(typeof(CosmeticBundleObject))]
    public class CosmeticBundleInspector : UnityEditor.Editor {
        private CosmeticBundleObject targetObj => (CosmeticBundleObject) serializedObject.targetObject;
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUILayout.Button("Build")) {
                AssetBundleResource bundleResource = CreateInstance<AssetBundleResource>();
                // bundleResource.Assets = targetObj.Hats
            }
        }
    }
}