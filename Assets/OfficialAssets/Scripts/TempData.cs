using System;
using System.Collections.Generic;

// Token: 0x02000235 RID: 565
public static class TempData
{
	// Token: 0x06000D66 RID: 3430 RVA: 0x000515DC File Offset: 0x0004F7DC
	public static bool DidHumansWin(GameOverReason reason)
	{
		return reason == GameOverReason.HumansByTask || reason == GameOverReason.HumansByVote;
	}

	// Token: 0x04000EED RID: 3821
	public static DeathReason LastDeathReason;

	// Token: 0x04000EEE RID: 3822
	public static GameOverReason EndReason = GameOverReason.HumansByTask;

	// Token: 0x04000EEF RID: 3823
	public static bool showAd;

	// Token: 0x04000EF0 RID: 3824
	public static List<WinningPlayerData> winners = new List<WinningPlayerData>
	{
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 0,
			SkinId = 0U,
			IsDead = true
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 1,
			SkinId = 1U,
			IsDead = true
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 2,
			SkinId = 2U,
			IsDead = true
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 3,
			SkinId = 0U
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 4,
			SkinId = 1U
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 5,
			SkinId = 2U
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 6
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 7
		},
		new WinningPlayerData
		{
			Name = "WWWWWWWWWW",
			ColorId = 8
		}
	};
}
