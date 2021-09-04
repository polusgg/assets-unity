using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class CrossFadeImages : MonoBehaviour
{
	// Token: 0x060002EF RID: 751 RVA: 0x00013650 File Offset: 0x00011850
	private void Update()
	{
		Color white = Color.white;
		white.a = Mathf.Clamp((Mathf.Sin(3.1415927f * Time.time / this.Period) + 0.75f) * 0.75f, 0f, 1f);
		this.Image1.color = white;
		white.a = 1f - white.a;
		this.Image2.color = white;
	}

	// Token: 0x04000365 RID: 869
	public SpriteRenderer Image1;

	// Token: 0x04000366 RID: 870
	public SpriteRenderer Image2;

	// Token: 0x04000367 RID: 871
	public float Period = 5f;
}
