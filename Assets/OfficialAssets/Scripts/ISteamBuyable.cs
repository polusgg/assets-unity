using System;

// Token: 0x02000130 RID: 304
internal interface ISteamBuyable
{
	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000747 RID: 1863
	uint SteamAppId { get; }

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000748 RID: 1864
	string SteamPrice { get; }
}
