using System;
using System.Collections.Generic;
using Cosmetics;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Assets.Editor.HatCreator {
    [CreateAssetMenu(fileName = "CosmeticBundle", menuName = "Create Cosmetic Bundle", order = 1)]
    public class CosmeticBundleObject : ScriptableObject {
        public string BundleName;
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

        [Serializable]
        public class CosmeticData {
            public uint Id;
            public string Name = "New Cosmetic";
            public Sprite Thumbnail => Cosmetic.GetMainSprite();
            public bool Registered;
            public bool Built;
            public Cosmetic Cosmetic;
            public CosmeticType Type = CosmeticType.Hat;
            public string ServerId = "";

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