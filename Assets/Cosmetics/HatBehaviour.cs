using Cosmetics;
using UnityEngine;

[CreateAssetMenu(fileName = "HatBehaviour", menuName = "Create Resource - Hat", order = 0)]
public class HatBehaviour : ScriptableObject {
    // Token: 0x04000B51 RID: 2897
    public Sprite MainImage;

    // Token: 0x04000B52 RID: 2898
    public Sprite BackImage;


    public Sprite LeftMainImage;

    // Token: 0x04000B54 RID: 2900
    public Sprite LeftBackImage;

    [HideInInspector] public string EpicId;

    // Token: 0x04000B56 RID: 2902
    public Sprite ClimbImage;

    // Token: 0x04000B57 RID: 2903
    public Sprite FloorImage;

    [HideInInspector] public Sprite LeftClimbImage;

    [HideInInspector] public Sprite LeftFloorImage;

    // Token: 0x04000B5A RID: 2906
    [HideInInspector] public bool InFront;

    // Token: 0x04000B5B RID: 2907
    public bool NoBounce;

    [HideInInspector] public bool NotInStore;

    [HideInInspector] public bool Free;

    public Material AltShader;

    public Vector2 ChipOffset;

    [HideInInspector] public int LimitedMonth = 1;

    [HideInInspector] public int LimitedYear = 1;

    [HideInInspector] public SkinData RelatedSkin;

    public string StoreName;

    [HideInInspector] public string ProductId;

    [HideInInspector] public int Order;
    public Object GetMain() => MainImage == null ? BackImage : MainImage;
}