using System;

namespace InnerNet
{
	// Token: 0x02000296 RID: 662
	public enum DisconnectReasons
	{
		// Token: 0x04001541 RID: 5441
		ExitGame,
		// Token: 0x04001542 RID: 5442
		GameFull,
		// Token: 0x04001543 RID: 5443
		GameStarted,
		// Token: 0x04001544 RID: 5444
		GameNotFound,
		// Token: 0x04001545 RID: 5445
		IncorrectVersion = 5,
		// Token: 0x04001546 RID: 5446
		Banned,
		// Token: 0x04001547 RID: 5447
		Kicked,
		// Token: 0x04001548 RID: 5448
		Custom,
		// Token: 0x04001549 RID: 5449
		InvalidName,
		// Token: 0x0400154A RID: 5450
		Hacking,
		// Token: 0x0400154B RID: 5451
		NotAuthorized,
		// Token: 0x0400154C RID: 5452
		Destroy = 16,
		// Token: 0x0400154D RID: 5453
		Error,
		// Token: 0x0400154E RID: 5454
		IncorrectGame,
		// Token: 0x0400154F RID: 5455
		ServerRequest,
		// Token: 0x04001550 RID: 5456
		ServerFull,
		// Token: 0x04001551 RID: 5457
		IntentionalLeaving = 208,
		// Token: 0x04001552 RID: 5458
		FocusLostBackground = 207,
		// Token: 0x04001553 RID: 5459
		FocusLost = 209,
		// Token: 0x04001554 RID: 5460
		NewConnection
	}
}
