using System;

namespace Multiplayer
{
	// Token: 0x02000289 RID: 649
	internal class MultiplayerSessionController : IDisposable
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x0005FC50 File Offset: 0x0005DE50
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0005FC5F File Offset: 0x0005DE5F
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
