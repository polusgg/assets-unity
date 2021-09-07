using Cosmetics;
using UnityEngine;

public class SkinData : ScriptableObject, Cosmetic, IBuyable {
    // Token: 0x04000BDB RID: 3035
    public Sprite IdleFrame;

    // Token: 0x04000BDC RID: 3036
    public AnimationClip IdleAnim;

    // Token: 0x04000BDD RID: 3037
    public AnimationClip RunAnim;

    // Token: 0x04000BDE RID: 3038
    public AnimationClip EnterVentAnim;

    // Token: 0x04000BDF RID: 3039
    public AnimationClip ExitVentAnim;

    // Token: 0x04000BE0 RID: 3040
    public AnimationClip ClimbAnim;

    // Token: 0x04000BE1 RID: 3041
    public AnimationClip ClimbDownAnim;

    // Token: 0x04000BE2 RID: 3042
    public AnimationClip KillTongueImpostor;

    // Token: 0x04000BE3 RID: 3043
    public AnimationClip KillTongueVictim;

    // Token: 0x04000BE4 RID: 3044
    public AnimationClip KillShootImpostor;

    // Token: 0x04000BE5 RID: 3045
    public AnimationClip KillShootVictim;

    // Token: 0x04000BE6 RID: 3046
    public AnimationClip KillNeckImpostor;

    // Token: 0x04000BE7 RID: 3047
    public AnimationClip KillStabImpostor;

    // Token: 0x04000BE8 RID: 3048
    public AnimationClip KillStabVictim;

    // Token: 0x04000BE9 RID: 3049
    public AnimationClip KillNeckVictim;

    // Token: 0x04000BEA RID: 3050
    public AnimationClip KillRHMVictim;

    // Token: 0x04000BEB RID: 3051
    public Sprite EjectFrame;

    // Token: 0x04000BEC RID: 3052
    public AnimationClip SpawnAnim;

    // Token: 0x04000BED RID: 3053
    public AnimationClip IdleLeftAnim;

    // Token: 0x04000BEE RID: 3054
    public AnimationClip RunLeftAnim;

    // Token: 0x04000BEF RID: 3055
    public AnimationClip EnterLeftVentAnim;

    // Token: 0x04000BF0 RID: 3056
    public AnimationClip ExitLeftVentAnim;

    // Token: 0x04000BF1 RID: 3057
    public AnimationClip SpawnLeftAnim;

    [HideInInspector]
    public OverlayKillAnimation[] KillAnims;

    [HideInInspector]
    public bool NotInStore;

    [HideInInspector]
    public bool Free;

    [HideInInspector]
    public HatBehaviour RelatedHat;

    [HideInInspector]
    public string StoreName;

    [HideInInspector]
    public string ProductId;

    [HideInInspector]
    public int Order;
    public Object GetMain() => IdleFrame;
    public string ProdId => ProductId;
}