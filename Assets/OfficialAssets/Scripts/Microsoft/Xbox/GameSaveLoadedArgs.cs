using System;

namespace Microsoft.Xbox
{
	// Token: 0x020002A7 RID: 679
	public class GameSaveLoadedArgs : EventArgs
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x000634B6 File Offset: 0x000616B6
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x000634BE File Offset: 0x000616BE
		public byte[] Data { get; private set; }

		// Token: 0x06001302 RID: 4866 RVA: 0x000634C7 File Offset: 0x000616C7
		public GameSaveLoadedArgs(byte[] data)
		{
			this.Data = data;
		}
	}
}
