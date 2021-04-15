using UnityEngine;

namespace Assets.Editor.HatCreator
{
    [CreateAssetMenu(fileName = "HatGenerator", menuName = "Create Hat Generator", order = 1)]
    public class HatGeneratorObject : ScriptableObject
    {
        public Object[] HatSprites;
    }
}