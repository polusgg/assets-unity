using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200011F RID: 287
public class OptionsConsole : MonoBehaviour, IUsable
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x0002C62F File Offset: 0x0002A82F
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.OptionsButton;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060006FC RID: 1788 RVA: 0x0002C633 File Offset: 0x0002A833
	public float UsableDistance
	{
		get
		{
			return 1f;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060006FD RID: 1789 RVA: 0x0002C63A File Offset: 0x0002A83A
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0002C644 File Offset: 0x0002A844
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		couldUse = @object.CanMove;
		canUse = couldUse;
		if (canUse)
		{
			num = Vector2.Distance(@object.GetTruePosition(), base.transform.position);
			canUse &= (num <= this.UsableDistance);
		}
		return num;
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0002C69C File Offset: 0x0002A89C
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Outline)
		{
			this.Outline.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Outline.material.SetColor("_OutlineColor", Color.white);
			this.Outline.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0002C714 File Offset: 0x0002A914
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
		CustomPlayerMenu customPlayerMenu = Object.Instantiate<CustomPlayerMenu>(this.MenuPrefab);
		customPlayerMenu.transform.SetParent(Camera.main.transform, false);
		customPlayerMenu.transform.localPosition = new Vector3(0f, 0f, -20f);
	}

	// Token: 0x040007D8 RID: 2008
	public CustomPlayerMenu MenuPrefab;

	// Token: 0x040007D9 RID: 2009
	public SpriteRenderer Outline;
}
