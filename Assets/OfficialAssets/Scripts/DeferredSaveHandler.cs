using System;
using System.Collections.Generic;

// Token: 0x02000174 RID: 372
public class DeferredSaveHandler : DestroyableSingleton<DeferredSaveHandler>
{
	// Token: 0x040009F2 RID: 2546
	public bool savePlayerPrefs;

	// Token: 0x040009F3 RID: 2547
	public bool saveSecureData;

	// Token: 0x040009F4 RID: 2548
	public bool saveAnnouncements;

	// Token: 0x040009F5 RID: 2549
	public bool saveQuickChatFavorites;

	// Token: 0x040009F6 RID: 2550
	public List<DeferredSaveHandler.GameOptionsSaveRequest> saveGameOptions = new List<DeferredSaveHandler.GameOptionsSaveRequest>();

	// Token: 0x020003E3 RID: 995
	public struct GameOptionsSaveRequest : IEquatable<DeferredSaveHandler.GameOptionsSaveRequest>
	{
		// Token: 0x060018E3 RID: 6371 RVA: 0x00075D3C File Offset: 0x00073F3C
		public bool Equals(DeferredSaveHandler.GameOptionsSaveRequest other)
		{
			return this.filename.Equals(other.filename);
		}

		// Token: 0x04001AD8 RID: 6872
		public string filename;

		// Token: 0x04001AD9 RID: 6873
		public GameOptionsData data;
	}
}
