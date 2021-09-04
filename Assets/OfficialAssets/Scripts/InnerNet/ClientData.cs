using System;

namespace InnerNet
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	public class ClientData
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x0005FC77 File Offset: 0x0005DE77
		public ClientData(int id)
		{
			this.Id = id;
		}

		// Token: 0x040014F1 RID: 5361
		public int Id;

		// Token: 0x040014F2 RID: 5362
		public bool InScene;

		// Token: 0x040014F3 RID: 5363
		public bool IsReady;

		// Token: 0x040014F4 RID: 5364
		public bool HasBeenReported;

		// Token: 0x040014F5 RID: 5365
		public PlayerControl Character;

		// Token: 0x040014F6 RID: 5366
		public int platformID = -1;
	}
}
