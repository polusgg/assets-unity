using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
internal class FootstepWatcher : MonoBehaviour, IStepWatcher
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00049593 File Offset: 0x00047793
	public int Priority
	{
		get
		{
			return this.priority;
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0004959B File Offset: 0x0004779B
	public SoundGroup MakeFootstep(PlayerControl player)
	{
		if (this.Area.OverlapPoint(player.GetTruePosition()))
		{
			return this.Sounds;
		}
		return null;
	}

	// Token: 0x04000D06 RID: 3334
	public int priority;

	// Token: 0x04000D07 RID: 3335
	public Collider2D Area;

	// Token: 0x04000D08 RID: 3336
	public SoundGroup Sounds;
}
