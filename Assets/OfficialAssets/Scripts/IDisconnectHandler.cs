using System;
using InnerNet;

// Token: 0x020000F1 RID: 241
public interface IDisconnectHandler
{
	// Token: 0x060005FD RID: 1533
	void HandleDisconnect(PlayerControl pc, DisconnectReasons reason);

	// Token: 0x060005FE RID: 1534
	void HandleDisconnect();
}
