using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class HorizontalGauge : MonoBehaviour
{
	// Token: 0x0600048E RID: 1166 RVA: 0x0001D4E8 File Offset: 0x0001B6E8
	public void Update()
	{
		if (this.MaxValue != 0f && this.lastValue != this.Value)
		{
			this.lastValue = this.Value;
			float num = this.lastValue / this.MaxValue * this.maskScale;
			this.Mask.transform.localScale = new Vector3(num, 1f, 1f);
			this.Mask.transform.localPosition = new Vector3(-this.Mask.sprite.bounds.size.x * (this.maskScale - num) / 2f, 0f, 0f);
		}
	}

	// Token: 0x04000552 RID: 1362
	public float Value = 0.5f;

	// Token: 0x04000553 RID: 1363
	public float MaxValue = 1f;

	// Token: 0x04000554 RID: 1364
	public float maskScale = 1f;

	// Token: 0x04000555 RID: 1365
	public SpriteMask Mask;

	// Token: 0x04000556 RID: 1366
	private float lastValue = float.MinValue;
}
