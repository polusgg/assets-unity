using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class Ladder : MonoBehaviour, IUsable
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000EC RID: 236 RVA: 0x00006034 File Offset: 0x00004234
	public float UsableDistance
	{
		get
		{
			return 0.5f;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000ED RID: 237 RVA: 0x0000603B File Offset: 0x0000423B
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000EE RID: 238 RVA: 0x00006042 File Offset: 0x00004242
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.UseButton;
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00006048 File Offset: 0x00004248
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		couldUse = (!pc.IsDead && @object.CanMove);
		canUse = couldUse;
		if (canUse)
		{
			Vector2 truePosition = @object.GetTruePosition();
			Vector3 position = base.transform.position;
			num = Vector2.Distance(truePosition, position);
			canUse &= (num <= this.UsableDistance && !PhysicsHelpers.AnythingBetween(truePosition, position, Constants.ShipOnlyMask, false));
		}
		return num;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000060C4 File Offset: 0x000042C4
	public void SetOutline(bool on, bool mainTarget)
	{
		this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
		this.Image.material.SetColor("_OutlineColor", Color.white);
		this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000612C File Offset: 0x0000432C
	public void Use()
	{
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		bool flag;
		bool flag2;
		this.CanUse(data, out flag, out flag2);
		if (flag)
		{
			PlayerControl.LocalPlayer.MyPhysics.RpcClimbLadder(this);
		}
	}

	// Token: 0x040000E8 RID: 232
	public byte Id;

	// Token: 0x040000E9 RID: 233
	public SpriteRenderer SpotArea;

	// Token: 0x040000EA RID: 234
	public bool IsTop;

	// Token: 0x040000EB RID: 235
	public Ladder Destination;

	// Token: 0x040000EC RID: 236
	public AudioClip UseSound;

	// Token: 0x040000ED RID: 237
	public SpriteRenderer Image;
}
