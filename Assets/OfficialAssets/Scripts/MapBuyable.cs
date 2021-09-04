using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
[CreateAssetMenu]
public class MapBuyable : ScriptableObject, IBuyable, ISteamBuyable, IEpicBuyable
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600097F RID: 2431 RVA: 0x0003DB89 File Offset: 0x0003BD89
	public string ProdId
	{
		get
		{
			return this.productId;
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000980 RID: 2432 RVA: 0x0003DB91 File Offset: 0x0003BD91
	public string SteamPrice
	{
		get
		{
			return "$3.99";
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000981 RID: 2433 RVA: 0x0003DB98 File Offset: 0x0003BD98
	public string EpicAppId
	{
		get
		{
			return this.EpicId;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000982 RID: 2434 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
	public string EpicPrice
	{
		get
		{
			return "$3.99";
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x0003DBA7 File Offset: 0x0003BDA7
	public uint SteamAppId
	{
		get
		{
			return this.SteamId;
		}
	}

	// Token: 0x04000AEF RID: 2799
	public string StoreName;

	// Token: 0x04000AF0 RID: 2800
	public string productId;

	// Token: 0x04000AF1 RID: 2801
	public bool IncludeHats;

	// Token: 0x04000AF2 RID: 2802
	public string EpicId;

	// Token: 0x04000AF3 RID: 2803
	public uint SteamId;

	// Token: 0x04000AF4 RID: 2804
	public string Win10Id;

	// Token: 0x04000AF5 RID: 2805
	public Sprite StoreImage;
}
