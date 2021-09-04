using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class PetsTab : MonoBehaviour
{
	// Token: 0x06000702 RID: 1794 RVA: 0x0002C790 File Offset: 0x0002A990
	public void OnEnable()
	{
		//PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		//this.HatImage.SetHat(SaveManager.LastHat, PlayerControl.LocalPlayer.Data.ColorId);
		//PlayerControl.SetSkinImage(SaveManager.LastSkin, this.SkinImage);
		//PlayerControl.SetPetImage(SaveManager.LastPet, PlayerControl.LocalPlayer.Data.ColorId, this.PetImage);
		//PetBehaviour[] unlockedPets = DestroyableSingleton<HatManager>.Instance.GetUnlockedPets();
		//for (int i = 0; i < unlockedPets.Length; i++)
		//{
		//	PetBehaviour pet = unlockedPets[i];
		//	float num = this.XRange.Lerp((float)(i % this.NumPerRow) / ((float)this.NumPerRow - 1f));
		//	float num2 = this.YStart - (float)(i / this.NumPerRow) * this.YOffset;
		//	ColorChip chip = Object.Instantiate<ColorChip>(this.ColorTabPrefab, this.scroller.Inner);
		//	chip.transform.localPosition = new Vector3(num, num2, -1f);
		//	chip.InUseForeground.SetActive(DestroyableSingleton<HatManager>.Instance.GetIdFromPet(pet) == SaveManager.LastPet);
		//	chip.Button.OnClick.AddListener(delegate()
		//	{
		//		this.SelectPet(chip, pet);
		//	});
		//	PlayerControl.SetPetImage(pet, PlayerControl.LocalPlayer.Data.ColorId, chip.Inner.FrontLayer);
		//	this.ColorChips.Add(chip);
		//}
		//this.scroller.YBounds.max = -(this.YStart - (float)(unlockedPets.Length / this.NumPerRow) * this.YOffset) - 3f;
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0002C964 File Offset: 0x0002AB64
	public void OnDisable()
	{
		//for (int i = 0; i < this.ColorChips.Count; i++)
		//{
		//	Object.Destroy(this.ColorChips[i].gameObject);
		//}
		//this.ColorChips.Clear();
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
	private void SelectPet(ColorChip sender, PetBehaviour pet)
	{
		uint idFromPet = DestroyableSingleton<HatManager>.Instance.GetIdFromPet(pet);
		SaveManager.LastPet = idFromPet;
		PlayerControl.SetPetImage(pet, PlayerControl.LocalPlayer.Data.ColorId, this.PetImage);
		if (PlayerControl.LocalPlayer)
		{
			PlayerControl.LocalPlayer.RpcSetPet(idFromPet);
		}
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			ColorChip colorChip = this.ColorChips[i];
			colorChip.InUseForeground.SetActive(colorChip == sender);
		}
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0002CA30 File Offset: 0x0002AC30
	public ColorChip GetDefaultSelectable()
	{
		PetBehaviour[] unlockedPets = DestroyableSingleton<HatManager>.Instance.GetUnlockedPets();
		DestroyableSingleton<HatManager>.Instance.GetHatById(SaveManager.LastHat);
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			PetBehaviour pet = unlockedPets[i];
			ColorChip result = this.ColorChips[i];
			if (DestroyableSingleton<HatManager>.Instance.GetIdFromPet(pet) == SaveManager.LastPet)
			{
				return result;
			}
		}
		return this.ColorChips[0];
	}

	// Token: 0x040007DA RID: 2010
	public ColorChip ColorTabPrefab;

	// Token: 0x040007DB RID: 2011
	public SpriteRenderer DemoImage;

	// Token: 0x040007DC RID: 2012
	public HatParent HatImage;

	// Token: 0x040007DD RID: 2013
	public SpriteRenderer SkinImage;

	// Token: 0x040007DE RID: 2014
	public SpriteRenderer PetImage;

	// Token: 0x040007DF RID: 2015
	public FloatRange XRange = new FloatRange(1.5f, 3f);

	// Token: 0x040007E0 RID: 2016
	public float YStart = 0.8f;

	// Token: 0x040007E1 RID: 2017
	public float YOffset = 0.8f;

	// Token: 0x040007E2 RID: 2018
	public int NumPerRow = 4;

	// Token: 0x040007E3 RID: 2019
	public Scroller scroller;

	// Token: 0x040007E4 RID: 2020
	[HideInInspector]
	public List<ColorChip> ColorChips = new List<ColorChip>();
}
