using System.Linq;
using UnityEditor;

namespace Assets.Editor
{
    public class SerializableResourceForClient
    {
        public uint BaseId;
        public string[] Assets;

        public SerializableResourceForClient(AssetBundleResource assetBundleResource) {
            BaseId = assetBundleResource.BaseId;
            Assets = assetBundleResource.DistinctAssets.Select(AssetDatabase.GetAssetPath).ToArray();
        }
    }
}