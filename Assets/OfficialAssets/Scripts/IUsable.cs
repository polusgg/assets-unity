using System;

// Token: 0x020001F4 RID: 500
public interface IUsable
{
	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000BBC RID: 3004
	float UsableDistance { get; }

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000BBD RID: 3005
	float PercentCool { get; }

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000BBE RID: 3006
	ImageNames UseIcon { get; }

	// Token: 0x06000BBF RID: 3007
	void SetOutline(bool on, bool mainTarget);

	// Token: 0x06000BC0 RID: 3008
	float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse);

	// Token: 0x06000BC1 RID: 3009
	void Use();
}
