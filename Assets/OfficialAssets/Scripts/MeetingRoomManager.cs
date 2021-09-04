using System;
using InnerNet;

// Token: 0x020000D4 RID: 212
public class MeetingRoomManager : IDisconnectHandler
{
	// Token: 0x0600052C RID: 1324 RVA: 0x000234AE File Offset: 0x000216AE
	public void AssignSelf(PlayerControl reporter, GameData.PlayerInfo target)
	{
		this.reporter = reporter;
		this.target = target;
		AmongUsClient.Instance.DisconnectHandlers.AddUnique(this);
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000234CE File Offset: 0x000216CE
	public void RemoveSelf()
	{
		AmongUsClient.Instance.DisconnectHandlers.Remove(this);
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x000234E1 File Offset: 0x000216E1
	public void HandleDisconnect(PlayerControl pc, DisconnectReasons reason)
	{
		if (AmongUsClient.Instance.AmHost)
		{
			this.reporter.CmdReportDeadBody(this.target);
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00023500 File Offset: 0x00021700
	public void HandleDisconnect()
	{
		this.HandleDisconnect(null, DisconnectReasons.ExitGame);
	}

	// Token: 0x040005DE RID: 1502
	public static readonly MeetingRoomManager Instance = new MeetingRoomManager();

	// Token: 0x040005DF RID: 1503
	private PlayerControl reporter;

	// Token: 0x040005E0 RID: 1504
	private GameData.PlayerInfo target;
}
