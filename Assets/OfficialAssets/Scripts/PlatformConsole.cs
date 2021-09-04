using System;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class PlatformConsole : MonoBehaviour, IUsable
{
	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00049E15 File Offset: 0x00048015
	public float UsableDistance
	{
		get
		{
			return this.usableDistance;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00049E1D File Offset: 0x0004801D
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00049E24 File Offset: 0x00048024
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.UseButton;
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00049E28 File Offset: 0x00048028
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		couldUse = (!pc.IsDead && @object.CanMove && !this.Platform.InUse && Vector2.Distance(this.Platform.transform.position, base.transform.position) < 2f);
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

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00049EE4 File Offset: 0x000480E4
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.white);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00049F5C File Offset: 0x0004815C
	public void Use()
	{
		bool flag;
		bool flag2;
		this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		this.Platform.Use();
	}

	// Token: 0x04000D2E RID: 3374
	public float usableDistance = 0.5f;

	// Token: 0x04000D2F RID: 3375
	public SpriteRenderer Image;

	// Token: 0x04000D30 RID: 3376
	public MovingPlatformBehaviour Platform;
}
