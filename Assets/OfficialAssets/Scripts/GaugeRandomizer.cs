using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class GaugeRandomizer : MonoBehaviour
{
	// Token: 0x060003C7 RID: 967 RVA: 0x00018EFA File Offset: 0x000170FA
	public void Start()
	{
		this.naturalSizeY = this.Gauge.size.y;
		this.naturalY = base.transform.localPosition.y;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00018F28 File Offset: 0x00017128
	private void Update()
	{
		float num = this.Range.Lerp(Mathf.PerlinNoise(this.Offset, Time.time * this.Frequency) / 2f + 0.5f);
		Vector2 size = this.Gauge.size;
		size.y = num;
		this.Gauge.size = size;
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = this.naturalY - (this.naturalSizeY - num) / 2f;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x0400045B RID: 1115
	public FloatRange Range;

	// Token: 0x0400045C RID: 1116
	public SpriteRenderer Gauge;

	// Token: 0x0400045D RID: 1117
	public float Frequency = 1f;

	// Token: 0x0400045E RID: 1118
	public float Offset = 1f;

	// Token: 0x0400045F RID: 1119
	private float naturalY;

	// Token: 0x04000460 RID: 1120
	private float naturalSizeY;

	// Token: 0x04000461 RID: 1121
	private Color goodLineColor = new Color(100f, 193f, 255f);
}
