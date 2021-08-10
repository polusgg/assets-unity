using UnityEngine;

namespace Cosmetics {
    [CreateAssetMenu(fileName = "PetBehaviour", menuName = "Create Resource - Pet", order = -1)]
    public class PetCreator : ScriptableObject, Cosmetic {
        public float yOffset = 0f;

        public AnimationClip idleClip;

        public AnimationClip walkClip;

        public AnimationClip scaredClip;

        public AnimationClip sadClip;

        public bool hasShadow;

        public Object GetMain() {
            return idleClip;
        }
    }
}