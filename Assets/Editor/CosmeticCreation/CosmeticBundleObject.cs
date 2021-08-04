using System;
using System.Collections.Generic;
using System.Linq;
using Cosmetics;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Editor.HatCreator {
    [CreateAssetMenu(fileName = "CosmeticBundle", menuName = "Create Cosmetic Bundle", order = 1)]
    public class CosmeticBundleObject : ScriptableObject {
        [FormerlySerializedAs("BundleName")] public string Name;
        public Sprite CoverArt;
        public Color32 Color;
        public float Price;
        public bool ForSale;
        [TextArea]
        public string Description;
        [HideInInspector]
        public bool Registered;
        [HideInInspector]
        public CosmeticData[] Cosmetics = new CosmeticData[0];
        public string LowerName => Name.ToLower();

        [Serializable]
        public class CosmeticData {
            public uint Id;
            public string Name = "New Cosmetic";

            public Sprite Thumbnail {
                get {
                    switch (Type) {
                        case CosmeticType.Hat: {
                            return Cosmetic != null ? Cosmetic.GetMainSprite() : null;
                        }
                        case CosmeticType.Pet:
                            EditorCurveBinding[] curve = AnimationUtility.GetObjectReferenceCurveBindings(((PetBehaviour) Cosmetic).idleClip);
                            Debug.Log(curve.Length);
                            foreach (EditorCurveBinding curveBinding in curve) {
                                if (curveBinding.type != typeof(SpriteRenderer)) continue;
                                foreach (ObjectReferenceKeyframe ork in AnimationUtility.GetObjectReferenceCurve(((PetBehaviour) Cosmetic).idleClip, curveBinding)) {
                                    if (ork.value != null) return (Sprite) ork.value;
                                }
                            }
                            return null;
                        default:
                            throw new Exception("Unsupported type has no thumbnail handler");
                    }
                }
            }
            public bool Registered;
            public Cosmetic Cosmetic;
            public CosmeticType Type = CosmeticType.Hat;

            public string SanitizedName => new SnakeCaseNamingStrategy().GetPropertyName(Name, false);
            //ui
            [NonSerialized]
            public bool foldedOut;

            public CosmeticData(){}
            public CosmeticData(string name) {
                Name = name;
            }
        }
    }
}