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
        public string Author;
        [HideInInspector]
        public bool Registered;
        [HideInInspector]
        public CosmeticData[] Cosmetics = new CosmeticData[0];
        public string LowerName => Name.ToLower();

        [Serializable]
        public class CosmeticData {
            public uint Id;
            public string Name = "New Cosmetic";
            public string Author = "";

            public Sprite Thumbnail {
                get {
                    if (Cosmetic == null) return null;

                    switch (Type) {
                        case CosmeticType.Hat: {
                            return (Sprite) Cosmetic.GetMain();
                        }
                        case CosmeticType.Pet:
                            AnimationClip clip = (AnimationClip) Cosmetic.GetMain();
                            EditorCurveBinding[] curve = AnimationUtility.GetObjectReferenceCurveBindings(clip);
                            Debug.Log(curve.Length);
                            foreach (EditorCurveBinding curveBinding in curve) {
                                if (curveBinding.type != typeof(SpriteRenderer)) continue;
                                foreach (ObjectReferenceKeyframe ork in AnimationUtility.GetObjectReferenceCurve(clip, curveBinding))
                                    if (ork.value != null)
                                        return (Sprite)ork.value;
                            }
                            return null;
                        case CosmeticType.Skin:
                            return (Sprite) Cosmetic.GetMain();
                        case CosmeticType.Body:
                            throw new Exception("Bodies have no thumb");
                        default:
                            throw new Exception("Unsupported type has no thumbnail handler");
                    }
                }
            }
            public bool Registered;
            public Cosmetic Cosmetic;

            public CosmeticType Type {
                get {
                    switch (Cosmetic.GetType().Name) {
                        case nameof(HatBehaviour): {
                            return CosmeticType.Hat;
                        }
                        case nameof(PetBehaviour): {
                            return CosmeticType.Pet;
                        }
                        case nameof(SkinData): {
                            return CosmeticType.Skin;
                        }
                        default: {
                            return CosmeticType.Unknown;
                        }
                    }
                }
            }

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