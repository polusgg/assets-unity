using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class SkinsTab : MonoBehaviour
{
	// Token: 0x0600070E RID: 1806 RVA: 0x0002CE30 File Offset: 0x0002B030
	public void OnEnable()
	{
		//PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		//this.HatImage.SetHat(SaveManager.LastHat, PlayerControl.LocalPlayer.Data.ColorId);
		//PlayerControl.SetSkinImage(SaveManager.LastSkin, this.SkinImage);
		//PlayerControl.SetPetImage(SaveManager.LastPet, PlayerControl.LocalPlayer.Data.ColorId, this.PetImage);
		//SkinData[] unlockedSkins = DestroyableSingleton<HatManager>.Instance.GetUnlockedSkins();
		//for (int i = 0; i < unlockedSkins.Length; i++)
		//{
		//	SkinData skin = unlockedSkins[i];
		//	float num = this.XRange.Lerp((float)(i % this.NumPerRow) / ((float)this.NumPerRow - 1f));
		//	float num2 = this.YStart - (float)(i / this.NumPerRow) * this.YOffset;
		//	ColorChip colorChip = Object.Instantiate<ColorChip>(this.ColorTabPrefab, this.scroller.Inner);
		//	colorChip.transform.localPosition = new Vector3(num, num2, -1f);
		//	colorChip.Button.OnClick.AddListener(delegate()
		//	{
		//		this.SelectHat(skin);
		//	});
		//	colorChip.Inner.FrontLayer.sprite = skin.IdleFrame;
		//	this.ColorChips.Add(colorChip);
		//}
		//this.scroller.YBounds.max = -(this.YStart - (float)(unlockedSkins.Length / this.NumPerRow) * this.YOffset) - 3f;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0002CFC0 File Offset: 0x0002B1C0
	public void OnDisable()
	{
		//for (int i = 0; i < this.ColorChips.Count; i++)
		//{
		//	Object.Destroy(this.ColorChips[i].gameObject);
		//}
		//this.ColorChips.Clear();
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002D004 File Offset: 0x0002B204
	public void Update()
	{
		PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		SkinData skinById = DestroyableSingleton<HatManager>.Instance.GetSkinById(SaveManager.LastSkin);
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			ColorChip colorChip = this.ColorChips[i];
			colorChip.InUseForeground.SetActive(skinById.IdleFrame == colorChip.Inner.FrontLayer.sprite);
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0002D084 File Offset: 0x0002B284
	private void SelectHat(SkinData skin)
	{
		uint idFromSkin = DestroyableSingleton<HatManager>.Instance.GetIdFromSkin(skin);
		SaveManager.LastSkin = idFromSkin;
		PlayerControl.SetSkinImage(SaveManager.LastSkin, this.SkinImage);
		if (PlayerControl.LocalPlayer)
		{
			PlayerControl.LocalPlayer.RpcSetSkin(idFromSkin);
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0002D0CC File Offset: 0x0002B2CC
	public ColorChip GetDefaultSelectable()
	{
		SkinData skinById = DestroyableSingleton<HatManager>.Instance.GetSkinById(SaveManager.LastSkin);
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			ColorChip colorChip = this.ColorChips[i];
			if (skinById.IdleFrame == colorChip.Inner.FrontLayer.sprite)
			{
				return colorChip;
			}
		}
		return this.ColorChips[0];
	}

	// Token: 0x040007EF RID: 2031
	public ColorChip ColorTabPrefab;

	// Token: 0x040007F0 RID: 2032
	public SpriteRenderer DemoImage;

	// Token: 0x040007F1 RID: 2033
	public HatParent HatImage;

	// Token: 0x040007F2 RID: 2034
	public SpriteRenderer SkinImage;

	// Token: 0x040007F3 RID: 2035
	public SpriteRenderer PetImage;

	// Token: 0x040007F4 RID: 2036
	public FloatRange XRange = new FloatRange(1.5f, 3f);

	// Token: 0x040007F5 RID: 2037
	public float YStart = 0.8f;

	// Token: 0x040007F6 RID: 2038
	public float YOffset = 0.8f;

	// Token: 0x040007F7 RID: 2039
	public int NumPerRow = 4;

	// Token: 0x040007F8 RID: 2040
	public Scroller scroller;

	// Token: 0x040007F9 RID: 2041
	[HideInInspector]
	public List<ColorChip> ColorChips = new List<ColorChip>();
}
