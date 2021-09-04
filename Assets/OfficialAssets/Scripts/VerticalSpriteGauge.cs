using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class VerticalSpriteGauge : MonoBehaviour
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00026FBC File Offset: 0x000251BC
	// (set) Token: 0x060005FA RID: 1530 RVA: 0x00026FC4 File Offset: 0x000251C4
	public float TopY { get; private set; }

	// Token: 0x060005FB RID: 1531 RVA: 0x00026FD0 File Offset: 0x000251D0
	public void Update()
	{
		if (this.MaxValue != 0f && this.lastValue != this.Value)
		{
			this.lastValue = this.Value;
			Vector3 localPosition = this.Mask.transform.localPosition;
			this.YRange.Lerp(this.lastValue / this.MaxValue);
			Vector3 localScale = this.Mask.transform.localScale;
			localScale.y = this.lastValue / this.MaxValue * this.YRange.Width;
			this.Mask.transform.localScale = localScale;
			localPosition.y = this.YRange.min + localScale.y / 2f;
			this.Mask.transform.localPosition = localPosition;
			this.TopY = this.YRange.min + localScale.y;
		}
	}

	// Token: 0x040006B0 RID: 1712
	public float Value = 0.5f;

	// Token: 0x040006B1 RID: 1713
	public float MaxValue = 1f;

	// Token: 0x040006B2 RID: 1714
	public FloatRange YRange;

	// Token: 0x040006B3 RID: 1715
	public SpriteRenderer Mask;

	// Token: 0x040006B4 RID: 1716
	private float lastValue = float.MinValue;
}
