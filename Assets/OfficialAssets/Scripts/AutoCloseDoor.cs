using System;

// Token: 0x020001E3 RID: 483
public class AutoCloseDoor : PlainDoor
{
	// Token: 0x06000B64 RID: 2916 RVA: 0x000486B4 File Offset: 0x000468B4
	public override void SetDoorway(bool open)
	{
		if (open)
		{
			this.OpenTime = 10f;
		}
		base.SetDoorway(open);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x000486CB File Offset: 0x000468CB
	public override bool DoUpdate(float dt)
	{
		if (this.OpenTime > 0f)
		{
			this.OpenTime = Math.Max(this.OpenTime - dt, 0f);
			if (this.OpenTime == 0f)
			{
				this.SetDoorway(false);
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000CD8 RID: 3288
	private const float OpenDuration = 10f;

	// Token: 0x04000CD9 RID: 3289
	private float OpenTime;
}
