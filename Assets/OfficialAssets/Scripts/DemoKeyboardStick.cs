using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class DemoKeyboardStick : VirtualJoystick
{
	// Token: 0x060004AE RID: 1198 RVA: 0x0001DCFB File Offset: 0x0001BEFB
	protected override void FixedUpdate()
	{
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0001DD00 File Offset: 0x0001BF00
	public override void UpdateJoystick(FingerBehaviour finger, Vector2 velocity, bool syncFinger)
	{
		this.UpKey.enabled = (velocity.y > 0.1f);
		this.DownKey.enabled = (velocity.y < -0.1f);
		this.RightKey.enabled = (velocity.x > 0.1f);
		this.LeftKey.enabled = (velocity.x < -0.1f);
	}

	// Token: 0x0400057D RID: 1405
	public SpriteRenderer UpKey;

	// Token: 0x0400057E RID: 1406
	public SpriteRenderer DownKey;

	// Token: 0x0400057F RID: 1407
	public SpriteRenderer LeftKey;

	// Token: 0x04000580 RID: 1408
	public SpriteRenderer RightKey;
}
