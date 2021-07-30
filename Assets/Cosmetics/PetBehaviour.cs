using PowerTools;
using UnityEngine;

public class PetBehaviour : ScriptableObject {
    // Token: 0x04000BAD RID: 2989
    private const float SnapDistance = 2f;

    // Token: 0x04000BAE RID: 2990
    public bool Free;

    // Token: 0x04000BAF RID: 2991
    public bool NotInStore;

    // Token: 0x04000BB0 RID: 2992
    public string ProductId = "Hell nah";

    // Token: 0x04000BB1 RID: 2993
    public StringNames StoreName;

    // Token: 0x04000BB2 RID: 2994
    public uint SteamId = 0;

    // Token: 0x04000BB3 RID: 2995
    public string EpicId = null;

    // Token: 0x04000BB4 RID: 2996
    public int ItchId = -1;

    // Token: 0x04000BB5 RID: 2997
    public string ItchUrl = null;

    // Token: 0x04000BB6 RID: 2998
    public string Win10Id = null;

    // Token: 0x04000BB7 RID: 2999
    public PlayerControl Source = null;

    // Token: 0x04000BBC RID: 3004
    public float YOffset = -0.25f;

    // Token: 0x04000BBD RID: 3005
    public SpriteAnim animator;

    // Token: 0x04000BBE RID: 3006
    public SpriteRenderer rend;

    // Token: 0x04000BBF RID: 3007
    public SpriteRenderer shadowRend;

    // Token: 0x04000BC0 RID: 3008
    public Rigidbody2D body;

    // Token: 0x04000BC1 RID: 3009
    public Collider2D Collider;

    // Token: 0x04000BC2 RID: 3010
    public AnimationClip idleClip;

    // Token: 0x04000BC3 RID: 3011
    public AnimationClip sadClip;

    // Token: 0x04000BC4 RID: 3012
    public AnimationClip scaredClip;

    // Token: 0x04000BC5 RID: 3013
    public AnimationClip walkClip;
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