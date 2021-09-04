using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class HatManager : DestroyableSingleton<HatManager>
{
	// Token: 0x06000971 RID: 2417 RVA: 0x0003D8C9 File Offset: 0x0003BAC9
	internal PetBehaviour GetPetById(uint petId)
	{
		if ((ulong)petId >= (ulong)((long)this.AllPets.Count))
		{
			return this.AllPets[0];
		}
		return this.AllPets[(int)petId];
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
	public uint GetIdFromPet(PetBehaviour pet)
	{
		return (uint)this.AllPets.FindIndex((PetBehaviour p) => p.idleClip == pet.idleClip);
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0003D925 File Offset: 0x0003BB25
	public PetBehaviour[] GetUnlockedPets()
	{
		return (from h in this.AllPets
		where h.Free || SaveManager.GetPurchase(h.ProductId)
		select h).ToArray<PetBehaviour>();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0003D956 File Offset: 0x0003BB56
	public HatBehaviour GetHatById(uint hatId)
	{
		if ((ulong)hatId >= (ulong)((long)this.AllHats.Count))
		{
			return this.NoneHat;
		}
		return this.AllHats[(int)hatId];
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0003D97C File Offset: 0x0003BB7C
	public HatBehaviour[] GetUnlockedHats()
	{
		return (from h in this.AllHats
		where (!HatManager.IsMapStuff(h.ProdId) && h.LimitedMonth == 0) || SaveManager.GetPurchase(h.ProductId)
		select h into o
		orderby o.Order descending, o.name
		select o).ToArray<HatBehaviour>();
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0003DA00 File Offset: 0x0003BC00
	public static bool IsMapStuff(string prodId)
	{
		return prodId == "map_mira" || prodId == "map_polus" || prodId == "hat_geoff";
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0003DA29 File Offset: 0x0003BC29
	public uint GetIdFromHat(HatBehaviour hat)
	{
		return (uint)this.AllHats.IndexOf(hat);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0003DA38 File Offset: 0x0003BC38
	public SkinData[] GetUnlockedSkins()
	{
		return (from s in this.AllSkins
		where !HatManager.IsMapStuff(s.ProdId) || SaveManager.GetPurchase(s.ProdId)
		select s into o
		orderby o.Order descending, o.name
		select o).ToArray<SkinData>();
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0003DABC File Offset: 0x0003BCBC
	public uint GetIdFromSkin(SkinData skin)
	{
		return (uint)this.AllSkins.IndexOf(skin);
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0003DACA File Offset: 0x0003BCCA
	internal SkinData GetSkinById(uint skinId)
	{
		if ((ulong)skinId >= (ulong)((long)this.AllSkins.Count))
		{
			return this.AllSkins[0];
		}
		return this.AllSkins[(int)skinId];
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0003DAF8 File Offset: 0x0003BCF8
	internal void SetSkin(SpriteRenderer skinRend, uint skinId)
	{
		SkinData skinById = this.GetSkinById(skinId);
		if (skinById)
		{
			skinRend.sprite = skinById.IdleFrame;
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0003DB24 File Offset: 0x0003BD24
	internal HatBehaviour GetHatByProdId(string prodId)
	{
		return this.AllHats.FirstOrDefault((HatBehaviour h) => h.ProdId == prodId);
	}

	// Token: 0x04000AE9 RID: 2793
	public HatBehaviour NoneHat;

	// Token: 0x04000AEA RID: 2794
	public Material DefaultHatShader;

	// Token: 0x04000AEB RID: 2795
	public List<PetBehaviour> AllPets = new List<PetBehaviour>();

	// Token: 0x04000AEC RID: 2796
	public List<HatBehaviour> AllHats = new List<HatBehaviour>();

	// Token: 0x04000AED RID: 2797
	public List<SkinData> AllSkins = new List<SkinData>();

	// Token: 0x04000AEE RID: 2798
	public List<MapBuyable> AllMaps = new List<MapBuyable>();
}
