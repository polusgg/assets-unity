using System;

// Token: 0x020001F1 RID: 497
public interface ICriticalSabotage
{
	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000BAD RID: 2989
	bool IsActive { get; }

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06000BAE RID: 2990
	float Countdown { get; }

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000BAF RID: 2991
	int UserCount { get; }

	// Token: 0x06000BB0 RID: 2992
	void ClearSabotage();
}
