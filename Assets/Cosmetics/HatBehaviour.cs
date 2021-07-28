using UnityEngine;

[CreateAssetMenu(fileName = "HatBehaviour", menuName = "Create Resource - Hat", order = 0)]
public class HatBehaviour : ScriptableObject {
    // Token: 0x04000B51 RID: 2897
    public Sprite MainImage;

    // Token: 0x04000B52 RID: 2898
    public Sprite BackImage;

    // Token: 0x04000B53 RID: 2899
    public Sprite LeftMainImage;

    // Token: 0x04000B54 RID: 2900
    public Sprite LeftBackImage;

    // Token: 0x04000B55 RID: 2901
    public string EpicId;

    // Token: 0x04000B56 RID: 2902
    public Sprite ClimbImage;

    // Token: 0x04000B57 RID: 2903
    public Sprite FloorImage;

    // Token: 0x04000B58 RID: 2904
    public Sprite LeftClimbImage;

    // Token: 0x04000B59 RID: 2905
    public Sprite LeftFloorImage;

    // Token: 0x04000B5A RID: 2906
    public bool InFront;

    // Token: 0x04000B5B RID: 2907
    public bool NoBounce;

    // Token: 0x04000B5C RID: 2908
    public bool NotInStore;

    // Token: 0x04000B5D RID: 2909
    public bool Free;

    // Token: 0x04000B5E RID: 2910
    public Material AltShader;

    // Token: 0x04000B5F RID: 2911
    public Vector2 ChipOffset;

    // Token: 0x04000B60 RID: 2912
    public int LimitedMonth;

    // Token: 0x04000B61 RID: 2913
    public int LimitedYear;

    // Token: 0x04000B62 RID: 2914
    public SkinData RelatedSkin;

    // Token: 0x04000B63 RID: 2915
    public string StoreName;

    // Token: 0x04000B64 RID: 2916
    public string ProductId;

    // Token: 0x04000B65 RID: 2917
    public int Order;
}