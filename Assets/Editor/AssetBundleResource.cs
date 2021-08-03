using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    [CreateAssetMenu(fileName = "AssetBundleResource", menuName = "Create Resource - Asset Bundle", order = 0)]
    public class AssetBundleResource : ScriptableObject {
        public uint BaseId;
        public Object[] Assets = new Object[0];

        public IEnumerable<Object> DistinctAssets => Assets.Distinct(new AssetComparer());

        public class AssetComparer : IEqualityComparer<Object> {
            public bool Equals(Object x, Object y) => AssetDatabase.GetAssetPath(x) == AssetDatabase.GetAssetPath(y);

            public int GetHashCode(Object obj) => AssetDatabase.GetAssetPath(obj).GetHashCode();
        }
    }
}