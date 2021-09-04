using System;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class OpenDoorConsole : MonoBehaviour, IUsable
{
	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00049CB4 File Offset: 0x00047EB4
	public ImageNames UseIcon
	{
		get
		{
			return this.useIcon;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00049CBC File Offset: 0x00047EBC
	public float UsableDistance
	{
		get
		{
			return this.usableDisance;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00049CC4 File Offset: 0x00047EC4
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00049CCB File Offset: 0x00047ECB
	public void Awake()
	{
		this.MyDoor = base.GetComponent<PlainDoor>();
		this.Image = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00049CE8 File Offset: 0x00047EE8
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = Vector2.Distance(pc.Object.GetTruePosition(), base.transform.position);
		couldUse = (!pc.IsDead && !this.MyDoor.Open);
		canUse = (couldUse && num <= this.UsableDistance);
		return num;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00049D48 File Offset: 0x00047F48
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.white);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00049DC0 File Offset: 0x00047FC0
	public void Use()
	{
		bool flag;
		bool flag2;
		this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		this.MyDoor.SetDoorway(true);
	}

	// Token: 0x04000D27 RID: 3367
	private ImageNames useIcon = ImageNames.UseButton;

	// Token: 0x04000D28 RID: 3368
	private PlainDoor MyDoor;

	// Token: 0x04000D29 RID: 3369
	private SpriteRenderer Image;

	// Token: 0x04000D2A RID: 3370
	public float usableDisance = 1.5f;
}
