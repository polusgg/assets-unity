using System;
using System.Collections.Generic;
using InnerNet;

// Token: 0x02000117 RID: 279
public interface IGameListHandler
{
	// Token: 0x060006DD RID: 1757
	void HandleList(InnerNetClient.TotalGameData totalGames, List<GameListing> availableGames);
}
