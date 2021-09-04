using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000204 RID: 516
public class SystemConsole : MonoBehaviour, IUsable
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0004BC5E File Offset: 0x00049E5E
	public ImageNames UseIcon
	{
		get
		{
			return this.useIcon;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0004BC66 File Offset: 0x00049E66
	public float UsableDistance
	{
		get
		{
			return this.usableDistance;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0004BC6E File Offset: 0x00049E6E
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0004BC75 File Offset: 0x00049E75
	public void Start()
	{
		if (this.FreeplayOnly && !DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0004BC94 File Offset: 0x00049E94
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.white);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0004BD0C File Offset: 0x00049F0C
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		Vector2 truePosition = @object.GetTruePosition();
		couldUse = (@object.CanMove && (!pc.IsDead || !(this.MinigamePrefab is EmergencyMinigame)));
		canUse = (couldUse && (!this.onlyFromBelow || truePosition.y < base.transform.position.y));
		if (canUse)
		{
			num = Vector2.Distance(truePosition, base.transform.position);
			canUse &= (num <= this.UsableDistance);
		}
		return num;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0004BDB0 File Offset: 0x00049FB0
	public void Use()
	{
		//bool flag;
		//bool flag2;
		//this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		//if (!flag)
		//{
		//	return;
		//}
		//PlayerControl.LocalPlayer.NetTransform.Halt();
		//Minigame minigame = Object.Instantiate<Minigame>(this.MinigamePrefab);
		//minigame.transform.SetParent(Camera.main.transform, false);
		//minigame.transform.localPosition = new Vector3(0f, 0f, -50f);
		//minigame.Begin(null);
	}

	// Token: 0x04000D9A RID: 3482
	public ImageNames useIcon = ImageNames.UseButton;

	// Token: 0x04000D9B RID: 3483
	public float usableDistance = 1f;

	// Token: 0x04000D9C RID: 3484
	public bool FreeplayOnly;

	// Token: 0x04000D9D RID: 3485
	public bool onlyFromBelow;

	// Token: 0x04000D9E RID: 3486
	public SpriteRenderer Image;

	// Token: 0x04000D9F RID: 3487
	public Minigame MinigamePrefab;
}
