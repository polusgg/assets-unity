using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.HatCreator
{
    [CreateAssetMenu(fileName = "HatGenerator", menuName = "Create Hat Generator", order = 1)]
    public class HatGeneratorObject : ScriptableObject
    {
        public Sprite[] HatSprites;
        
        public IEnumerable<Sprite> DistinctAssets => HatSprites.Distinct(new AssetComparer());

        public string AssetPath => AssetDatabase.GetAssetPath(this).Replace(".asset", "");

        private class AssetComparer : IEqualityComparer<Sprite> {
            public bool Equals(Sprite x, Sprite y) => AssetDatabase.GetAssetPath(x) == AssetDatabase.GetAssetPath(y);

            public int GetHashCode(Sprite obj) => AssetDatabase.GetAssetPath(obj).GetHashCode();
        }
    }
}