using System;

namespace InnerNet
{
	// Token: 0x02000299 RID: 665
	public enum JoinFailureReasons : byte
	{
		// Token: 0x04001566 RID: 5478
		TooManyPlayers = 1,
		// Token: 0x04001567 RID: 5479
		GameStarted,
		// Token: 0x04001568 RID: 5480
		GameNotFound,
		// Token: 0x04001569 RID: 5481
		HostNotReady,
		// Token: 0x0400156A RID: 5482
		IncorrectVersion,
		// Token: 0x0400156B RID: 5483
		Banned,
		// Token: 0x0400156C RID: 5484
		Kicked
	}
}
