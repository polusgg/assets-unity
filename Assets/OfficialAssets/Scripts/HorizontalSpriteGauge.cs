using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class HorizontalSpriteGauge : MonoBehaviour
{
	// Token: 0x06000490 RID: 1168 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
	public void Update()
	{
		if (this.MaxValue != 0f && this.lastValue != this.Value)
		{
			this.lastValue = this.Value;
			float num = this.lastValue / this.MaxValue * this.maskScale;
			Vector3 localScale = this.Mask.transform.localScale;
			localScale.x = num;
			this.Mask.transform.localScale = localScale;
			Vector3 localPosition = this.Mask.transform.localPosition;
			localPosition.x = -this.Mask.sprite.bounds.size.x * (this.maskScale - num) / 2f;
			this.Mask.transform.localPosition = localPosition;
		}
	}

	// Token: 0x04000557 RID: 1367
	public float Value = 0.5f;

	// Token: 0x04000558 RID: 1368
	public float MaxValue = 1f;

	// Token: 0x04000559 RID: 1369
	public float maskScale = 1f;

	// Token: 0x0400055A RID: 1370
	public SpriteRenderer Mask;

	// Token: 0x0400055B RID: 1371
	private float lastValue = float.MinValue;
}
