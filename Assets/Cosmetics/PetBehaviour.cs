using Cosmetics;
using PowerTools;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PetBehaviour", menuName = "Create Resource - Pet", order = 0)]
public class PetBehaviour : Cosmetic {
    [HideInInspector]
    public bool Free;

    [HideInInspector]
    public bool NotInStore;

    [HideInInspector]
    public string ProductId = "Hell nah";

    [HideInInspector]
    public StringNames StoreName;

    [HideInInspector]
    public uint SteamId = 0;

    [HideInInspector]
    public string EpicId = null;

    [HideInInspector]
    public int ItchId = -1;

    [HideInInspector]
    public string ItchUrl = null;

    [HideInInspector]
    public string Win10Id = null;

    [HideInInspector]
    public PlayerControl Source = null;

    // Token: 0x04000BBC RID: 3004
    public float YOffset = -0.25f;

    [HideInInspector]
    public SpriteAnim animator;

    [HideInInspector]
    public SpriteRenderer rend;

    [HideInInspector]
    public SpriteRenderer shadowRend;

    [HideInInspector]
    public Rigidbody2D body;

    [HideInInspector]
    public Collider2D Collider;

    // Token: 0x04000BC2 RID: 3010
    public AnimationClip idleClip;

    // Token: 0x04000BC3 RID: 3011
    public AnimationClip sadClip;

    // Token: 0x04000BC4 RID: 3012
    public AnimationClip scaredClip;

    // Token: 0x04000BC5 RID: 3013
    public AnimationClip walkClip;
    public override Object GetMain() {
        // Debug.Log(AnimationUtility.GetCurveBindings(idleClip)[0].type);
        return null;
    }
}

public class PlayerControl { }

public enum StringNames {
    Nothing,
    Why,
    Would,
    I,
    Put,
    Anything,
    Here
}