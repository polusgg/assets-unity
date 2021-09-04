using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class BalloonBehaviour : MonoBehaviour
{
	// Token: 0x060001A0 RID: 416 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
	public void Update()
	{
		base.transform.localPosition = this.Origin + new Vector2(Mathf.PerlinNoise(Time.time * this.PeriodX, 1f) * this.MagnitudeX, Mathf.Sin(Time.time * this.PeriodY) * this.MagnitudeY);
	}

	// Token: 0x04000272 RID: 626
	public Vector2 Origin;

	// Token: 0x04000273 RID: 627
	public float PeriodX = 4f;

	// Token: 0x04000274 RID: 628
	public float PeriodY = 4f;

	// Token: 0x04000275 RID: 629
	public float MagnitudeX = 3f;

	// Token: 0x04000276 RID: 630
	public float MagnitudeY = 3f;
}
