using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class HatsTab : MonoBehaviour
{
	// Token: 0x060006EE RID: 1774 RVA: 0x0002C154 File Offset: 0x0002A354
	public void OnEnable()
	{
		//PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		//this.HatImage.SetHat(SaveManager.LastHat, PlayerControl.LocalPlayer.Data.ColorId);
		//PlayerControl.SetSkinImage(SaveManager.LastSkin, this.SkinImage);
		//PlayerControl.SetPetImage(SaveManager.LastPet, PlayerControl.LocalPlayer.Data.ColorId, this.PetImage);
		//HatBehaviour[] unlockedHats = DestroyableSingleton<HatManager>.Instance.GetUnlockedHats();
		//for (int i = 0; i < unlockedHats.Length; i++)
		//{
		//	HatBehaviour hat = unlockedHats[i];
		//	float num = this.XRange.Lerp((float)(i % this.NumPerRow) / ((float)this.NumPerRow - 1f));
		//	float num2 = this.YStart - (float)(i / this.NumPerRow) * this.YOffset;
		//	ColorChip colorChip = Object.Instantiate<ColorChip>(this.ColorTabPrefab, this.scroller.Inner);
		//	colorChip.transform.localPosition = new Vector3(num, num2, -1f);
		//	colorChip.Button.OnClick.AddListener(delegate()
		//	{
		//		this.SelectHat(hat);
		//	});
		//	colorChip.Inner.SetHat(hat, PlayerControl.LocalPlayer.Data.ColorId);
		//	colorChip.Inner.transform.localPosition = hat.ChipOffset;
		//	colorChip.Tag = hat;
		//	this.ColorChips.Add(colorChip);
		//}
		//this.scroller.YBounds.max = -(this.YStart - (float)(unlockedHats.Length / this.NumPerRow) * this.YOffset) - 3f;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0002C314 File Offset: 0x0002A514
	public void OnDisable()
	{
		//for (int i = 0; i < this.ColorChips.Count; i++)
		//{
		//	Object.Destroy(this.ColorChips[i].gameObject);
		//}
		//this.ColorChips.Clear();
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0002C358 File Offset: 0x0002A558
	public void Update()
	{
		PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		HatBehaviour hatById = DestroyableSingleton<HatManager>.Instance.GetHatById(SaveManager.LastHat);
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			ColorChip colorChip = this.ColorChips[i];
#pragma warning disable 252,253
			colorChip.InUseForeground.SetActive(hatById == colorChip.Tag);
#pragma warning restore 252,253
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0002C3C8 File Offset: 0x0002A5C8
	private void SelectHat(HatBehaviour hat)
	{
		uint idFromHat = DestroyableSingleton<HatManager>.Instance.GetIdFromHat(hat);
		SaveManager.LastHat = idFromHat;
		this.HatImage.SetHat(idFromHat, PlayerControl.LocalPlayer.Data.ColorId);
		if (PlayerControl.LocalPlayer)
		{
			PlayerControl.LocalPlayer.RpcSetHat(idFromHat);
		}
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002C41C File Offset: 0x0002A61C
	public ColorChip GetDefaultSelectable()
	{
		HatBehaviour hatById = DestroyableSingleton<HatManager>.Instance.GetHatById(SaveManager.LastHat);
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			ColorChip colorChip = this.ColorChips[i];
#pragma warning disable 252,253
			if (hatById == colorChip.Tag)
#pragma warning restore 252,253
			{
				return colorChip;
			}
		}
		return this.ColorChips[0];
	}

	// Token: 0x040007C6 RID: 1990
	public ColorChip ColorTabPrefab;

	// Token: 0x040007C7 RID: 1991
	public SpriteRenderer DemoImage;

	// Token: 0x040007C8 RID: 1992
	public HatParent HatImage;

	// Token: 0x040007C9 RID: 1993
	public SpriteRenderer SkinImage;

	// Token: 0x040007CA RID: 1994
	public SpriteRenderer PetImage;

	// Token: 0x040007CB RID: 1995
	public FloatRange XRange = new FloatRange(1.5f, 3f);

	// Token: 0x040007CC RID: 1996
	public float YStart = 0.8f;

	// Token: 0x040007CD RID: 1997
	public float YOffset = 0.8f;

	// Token: 0x040007CE RID: 1998
	public int NumPerRow = 4;

	// Token: 0x040007CF RID: 1999
	public Scroller scroller;

	// Token: 0x040007D0 RID: 2000
	[HideInInspector]
	public List<ColorChip> ColorChips = new List<ColorChip>();
}
