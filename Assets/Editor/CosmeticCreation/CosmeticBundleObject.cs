using System;
using System.Collections.Generic;
using System.Linq;
using Cosmetics;
using Editor.Accounts;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Assets.Editor.HatCreator {
    [CreateAssetMenu(fileName = "CosmeticBundle", menuName = "Create Cosmetic Bundle", order = 1)]
    public class CosmeticBundleObject : ScriptableObject {
        [FormerlySerializedAs("BundleName")] public string Name;
        public Sprite CoverArt;
        public Color32 Color;
        public float Price;
        public bool ForSale;
        [TextArea] public string Description;
        [HideInInspector] public bool Registered;
        [HideInInspector] public CosmeticData[] Cosmetics = Array.Empty<CosmeticData>();
        public string SanitizedName => new SnakeCaseNamingStrategy().GetPropertyName(Name.ToLower(), false);

        [Serializable]
        public class CosmeticData {
            public uint Id;
            public string Name = "New Cosmetic";
            public string Author = "";

            public string CosmeticBundleName {
                set {
                    switch (Type) {
                        case CosmeticType.Hat:
                            ((HatBehaviour) Cosmetic).ProductId = $"_${value}";
                            break;
                        case CosmeticType.Pet:
                            ((HatBehaviour) Cosmetic).ProductId = $"_${value}";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            public Sprite Thumbnail {
                get {
                    if (Cosmetic == null) return null;
                    switch (Type) {
                        case CosmeticType.Hat:
                            return (Sprite) ((HatBehaviour) Cosmetic).GetMain();
                        case CosmeticType.Pet:
                            AnimationClip clip = (AnimationClip) ((PetCreator) Cosmetic).GetMain();
                            EditorCurveBinding[] curve = AnimationUtility.GetObjectReferenceCurveBindings(clip);
                            foreach (EditorCurveBinding curveBinding in curve) {
                                if (curveBinding.type != typeof(SpriteRenderer)) continue;
                                foreach (ObjectReferenceKeyframe ork in AnimationUtility.GetObjectReferenceCurve(clip, curveBinding))
                                    if (ork.value != null)
                                        return (Sprite) ork.value;
                            }

                            return null;
                        case CosmeticType.Skin:
                            return (Sprite) ((SkinData) Cosmetic).GetMain();
                        case CosmeticType.Body:
                            throw new Exception("Bodies have no thumb");
                        default:
                            throw new Exception("Unsupported type has no thumbnail handler");
                    }
                }
            }

            public bool Registered;
            public Object Cosmetic;
            public CosmeticType Type;

            public Type TypeType {
                get {
                    switch (Type) {
                        case CosmeticType.Hat:
                            return typeof(HatBehaviour);
                        case CosmeticType.Pet:
                            return typeof(PetCreator);
                        case CosmeticType.Skin:
                            return typeof(SkinData);
                        default:
                            return typeof(bool);
                    };
                }
            }

            public string SanitizedName => new SnakeCaseNamingStrategy().GetPropertyName(Name.ToLower(), false);

            //ui
            [NonSerialized] public bool foldedOut;

            public CosmeticData() { }

            public CosmeticData(string name) {
                Name = name;
            }
        }
    }
}