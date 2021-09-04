using System;
using PowerTools;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class HatParent : MonoBehaviour
{
	// Token: 0x1700008F RID: 143
	// (get) Token: 0x0600095F RID: 2399 RVA: 0x0003D5E5 File Offset: 0x0003B7E5
	// (set) Token: 0x06000960 RID: 2400 RVA: 0x0003D5ED File Offset: 0x0003B7ED
	public HatBehaviour Hat { get; set; }

	// Token: 0x17000090 RID: 144
	// (set) Token: 0x06000961 RID: 2401 RVA: 0x0003D5F6 File Offset: 0x0003B7F6
	public Color color
	{
		set
		{
			this.BackLayer.color = value;
			this.FrontLayer.color = value;
		}
	}

	// Token: 0x17000091 RID: 145
	// (set) Token: 0x06000962 RID: 2402 RVA: 0x0003D610 File Offset: 0x0003B810
	public bool flipX
	{
		set
		{
			this.BackLayer.flipX = value;
			this.FrontLayer.flipX = value;
		}
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003D62A File Offset: 0x0003B82A
	public void SetHat(HatBehaviour hat, int color)
	{
		this.Hat = hat;
		this.SetHat(color);
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0003D63A File Offset: 0x0003B83A
	public void SetHat(int color)
	{
		this.SetIdleAnim();
		this.SetColor(color);
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0003D64C File Offset: 0x0003B84C
	public void SetIdleAnim()
	{
		if (!this.Hat)
		{
			return;
		}
		if (this.Hat.AltShader)
		{
			this.FrontLayer.sharedMaterial = this.Hat.AltShader;
			this.BackLayer.sharedMaterial = this.Hat.AltShader;
		}
		else
		{
			this.FrontLayer.sharedMaterial = DestroyableSingleton<HatManager>.Instance.DefaultHatShader;
			this.BackLayer.sharedMaterial = DestroyableSingleton<HatManager>.Instance.DefaultHatShader;
		}
		PowerTools.SpriteAnimNodeSync component = base.GetComponent<SpriteAnimNodeSync>();
		if (component)
		{
			component.NodeId = (this.Hat.NoBounce ? 1 : 0);
		}
		if (this.Hat.InFront)
		{
			this.BackLayer.enabled = false;
			this.FrontLayer.enabled = true;
			this.FrontLayer.sprite = this.Hat.MainImage;
			return;
		}
		if (this.Hat.BackImage)
		{
			this.BackLayer.enabled = true;
			this.FrontLayer.enabled = true;
			this.BackLayer.sprite = this.Hat.BackImage;
			this.FrontLayer.sprite = this.Hat.MainImage;
			return;
		}
		this.BackLayer.enabled = true;
		this.FrontLayer.enabled = false;
		this.BackLayer.sprite = this.Hat.MainImage;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
	public void SetHat(uint hatId, int color)
	{
		if (!DestroyableSingleton<HatManager>.InstanceExists)
		{
			return;
		}
		this.Hat = DestroyableSingleton<HatManager>.Instance.GetHatById(hatId);
		this.SetHat(color);
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0003D7DA File Offset: 0x0003B9DA
	internal void SetFloorAnim()
	{
		this.BackLayer.enabled = false;
		this.FrontLayer.enabled = true;
		this.FrontLayer.sprite = this.Hat.FloorImage;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0003D80A File Offset: 0x0003BA0A
	internal void SetClimbAnim()
	{
		this.BackLayer.enabled = false;
		this.FrontLayer.enabled = true;
		this.FrontLayer.sprite = this.Hat.ClimbImage;
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0003D83A File Offset: 0x0003BA3A
	public void LateUpdate()
	{
		if (this.Parent)
		{
			this.flipX = this.Parent.flipX;
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0003D85A File Offset: 0x0003BA5A
	internal void SetColor(int color)
	{
		PlayerControl.SetPlayerMaterialColors(color, this.FrontLayer);
		PlayerControl.SetPlayerMaterialColors(color, this.BackLayer);
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0003D874 File Offset: 0x0003BA74
	internal void SetMaskLayer(int layer)
	{
		this.FrontLayer.material.SetInt("_MaskLayer", layer);
		this.BackLayer.material.SetInt("_MaskLayer", layer);
	}

	// Token: 0x04000AD4 RID: 2772
	public SpriteRenderer BackLayer;

	// Token: 0x04000AD5 RID: 2773
	public SpriteRenderer FrontLayer;

	// Token: 0x04000AD6 RID: 2774
	public SpriteRenderer Parent;
}
