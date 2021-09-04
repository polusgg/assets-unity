using System;

namespace Multiplayer
{
	// Token: 0x02000288 RID: 648
	internal class MultiplayerController : IDisposable
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x0005FC37 File Offset: 0x0005DE37
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0005FC46 File Offset: 0x0005DE46
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
