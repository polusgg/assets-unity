using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class MapConsole : MonoBehaviour, IUsable
{
	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00049974 File Offset: 0x00047B74
	public ImageNames UseIcon
	{
		get
		{
			return this.useIcon;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0004997C File Offset: 0x00047B7C
	public float UsableDistance
	{
		get
		{
			return this.usableDistance;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00049984 File Offset: 0x00047B84
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0004998C File Offset: 0x00047B8C
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.white);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00049A04 File Offset: 0x00047C04
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		couldUse = pc.Object.CanMove;
		canUse = couldUse;
		if (canUse)
		{
			num = Vector2.Distance(@object.GetTruePosition(), base.transform.position);
			canUse &= (num <= this.UsableDistance);
		}
		return num;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00049A64 File Offset: 0x00047C64
	public void Use()
	{
		bool flag;
		bool flag2;
		this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		PlayerControl.LocalPlayer.NetTransform.Halt();
		DestroyableSingleton<HudManager>.Instance.ShowMap(delegate(MapBehaviour m)
		{
			m.ShowCountOverlay();
		});
		if (PlayerControl.LocalPlayer.AmOwner)
		{
			PlayerControl.LocalPlayer.MyPhysics.inputHandler.enabled = true;
			ConsoleJoystick.SetMode_Task();
		}
	}

	// Token: 0x04000D15 RID: 3349
	public ImageNames useIcon = ImageNames.AdminMapButton;

	// Token: 0x04000D16 RID: 3350
	public float usableDistance = 1f;

	// Token: 0x04000D17 RID: 3351
	public SpriteRenderer Image;
}
