using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.HatCreator {
    [CreateAssetMenu(fileName = "CosmeticBundle", menuName = "Create Cosmetic Bundle", order = 1)]
    public class CosmeticBundleObject : ScriptableObject {
        public uint BaseId;
        public List<HatBehaviour> Hats;
        public List<PetBehaviour> Pets;
    }
}