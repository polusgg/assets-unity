using UnityEngine;

namespace Cosmetics {
    public abstract class Cosmetic : ScriptableObject {
        public abstract Sprite GetMainSprite();
    }
}