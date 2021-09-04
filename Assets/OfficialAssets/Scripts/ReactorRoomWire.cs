using System;
using PowerTools;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class ReactorRoomWire : MonoBehaviour
{
	// Token: 0x06000BE5 RID: 3045 RVA: 0x00049FA0 File Offset: 0x000481A0
	public void Update()
	{
		if (this.reactor == null)
		{
			ISystemType systemType;
			if (!ShipStatus.Instance || !ShipStatus.Instance.Systems.TryGetValue(SystemTypes.Reactor, out systemType))
			{
				return;
			}
			this.reactor = (ReactorSystemType)systemType;
		}
		if (this.reactor.IsActive)
		{
			if (this.reactor.GetConsoleComplete(this.myConsole.ConsoleId))
			{
				if (!this.Image.IsPlaying(this.MeltdownReady))
				{
					this.Image.Play(this.MeltdownReady, 1f);
					return;
				}
			}
			else if (!this.Image.IsPlaying(this.MeltdownNeed))
			{
				this.Image.Play(this.MeltdownNeed, 1f);
				return;
			}
		}
		else if (!this.Image.IsPlaying(this.Normal))
		{
			this.Image.Play(this.Normal, 1f);
		}
	}

	// Token: 0x04000D31 RID: 3377
	public global::Console myConsole;

	// Token: 0x04000D32 RID: 3378
	public SpriteAnim Image;

	// Token: 0x04000D33 RID: 3379
	public AnimationClip Normal;

	// Token: 0x04000D34 RID: 3380
	public AnimationClip MeltdownNeed;

	// Token: 0x04000D35 RID: 3381
	public AnimationClip MeltdownReady;

	// Token: 0x04000D36 RID: 3382
	private ReactorSystemType reactor;
}
