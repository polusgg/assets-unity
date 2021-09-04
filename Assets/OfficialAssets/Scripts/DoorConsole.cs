using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020001E2 RID: 482
public class DoorConsole : MonoBehaviour, IUsable
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00048526 File Offset: 0x00046726
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.UseButton;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0004852A File Offset: 0x0004672A
	public float UsableDistance
	{
		get
		{
			return 1f;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00048531 File Offset: 0x00046731
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00048538 File Offset: 0x00046738
	public void Awake()
	{
		this.MyDoor = base.GetComponent<PlainDoor>();
		this.Image = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00048554 File Offset: 0x00046754
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = Vector2.Distance(pc.Object.GetTruePosition(), base.transform.position);
		couldUse = (!pc.IsDead && !this.MyDoor.Open);
		canUse = (couldUse && num <= this.UsableDistance);
		return num;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x000485B4 File Offset: 0x000467B4
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.white);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0004862C File Offset: 0x0004682C
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
		Minigame minigame = Object.Instantiate<Minigame>(this.MinigamePrefab, Camera.main.transform);
		minigame.transform.localPosition = new Vector3(0f, 0f, -50f);
		((IDoorMinigame)minigame).SetDoor(this.MyDoor);
		minigame.Begin(null);
	}

	// Token: 0x04000CD5 RID: 3285
	private PlainDoor MyDoor;

	// Token: 0x04000CD6 RID: 3286
	public Minigame MinigamePrefab;

	// Token: 0x04000CD7 RID: 3287
	private SpriteRenderer Image;
}
