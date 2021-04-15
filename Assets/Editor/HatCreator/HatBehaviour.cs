using UnityEngine;

namespace Assets.Editor.HatCreator
{
    public class HatBehaviour : ScriptableObject
    {
        public Sprite MainImage;

        // Token: 0x04000AD9 RID: 2777
        public Sprite BackImage;

        // Token: 0x04000ADB RID: 2779
        public Sprite ClimbImage;

        // Token: 0x04000ADC RID: 2780
        public Sprite FloorImage;

        // Token: 0x04000ADD RID: 2781
        public bool InFront;

        // Token: 0x04000ADE RID: 2782
        public bool NoBounce;

        // Token: 0x04000ADF RID: 2783
        public bool NotInStore;

        // Token: 0x04000AE0 RID: 2784
        public bool Free;

        // Token: 0x04000AE1 RID: 2785
        public Material AltShader;

        // Token: 0x04000AE2 RID: 2786
        public Vector2 ChipOffset;

        // Token: 0x04000AE4 RID: 2788
        public int LimitedYear;
    }
}