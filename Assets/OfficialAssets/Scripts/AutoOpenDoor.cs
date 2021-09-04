using System;

// Token: 0x020001E5 RID: 485
public class AutoOpenDoor : PlainDoor
{
	// Token: 0x06000B71 RID: 2929 RVA: 0x0004896B File Offset: 0x00046B6B
	public override void SetDoorway(bool open)
	{
		if (!open)
		{
			this.ClosedTimer = 10f;
			this.CooldownTimer = 30f;
		}
		base.SetDoorway(open);
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00048990 File Offset: 0x00046B90
	public override bool DoUpdate(float dt)
	{
		this.CooldownTimer = Math.Max(this.CooldownTimer - dt, 0f);
		if (this.ClosedTimer > 0f)
		{
			this.ClosedTimer = Math.Max(this.ClosedTimer - dt, 0f);
			if (this.ClosedTimer == 0f)
			{
				this.SetDoorway(true);
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000CDB RID: 3291
	private const float ClosedDuration = 10f;

	// Token: 0x04000CDC RID: 3292
	public const float CooldownDuration = 30f;

	// Token: 0x04000CDD RID: 3293
	public float ClosedTimer;

	// Token: 0x04000CDE RID: 3294
	public float CooldownTimer;
}
