using UnityEngine;

namespace Assets.Editor {
    [CreateAssetMenu(fileName = "AssetBundleResource", menuName = "Create Resource - Asset Bundle", order = 0)]
    public class AssetBundleResource : ScriptableObject {
        public uint BaseId;
        public Object[] Assets;
    }
}