using System;

// Token: 0x020001F3 RID: 499
public interface IStepWatcher
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000BBA RID: 3002
	int Priority { get; }

	// Token: 0x06000BBB RID: 3003
	SoundGroup MakeFootstep(PlayerControl player);
}
