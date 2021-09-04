using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class VerticalGauge : MonoBehaviour
{
	// Token: 0x060005F7 RID: 1527 RVA: 0x00026EC4 File Offset: 0x000250C4
	public void Update()
	{
		if (this.lastValue != this.value)
		{
			this.lastValue = this.value;
			float num = Mathf.Clamp(this.lastValue / this.MaxValue, 0f, 1f) * this.maskScale;
			Vector3 localScale = this.Mask.transform.localScale;
			localScale.y = num;
			this.Mask.transform.localScale = localScale;
			this.Mask.transform.localPosition = new Vector3(0f, -this.Mask.sprite.bounds.size.y * (this.maskScale - num) / 2f, 0f);
		}
	}

	// Token: 0x040006AB RID: 1707
	public float value = 0.5f;

	// Token: 0x040006AC RID: 1708
	public float MaxValue = 1f;

	// Token: 0x040006AD RID: 1709
	public float maskScale = 1f;

	// Token: 0x040006AE RID: 1710
	public SpriteMask Mask;

	// Token: 0x040006AF RID: 1711
	private float lastValue = float.MinValue;
}
