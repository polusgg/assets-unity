using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class PooledMapIcon : PoolableBehavior
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x0001F714 File Offset: 0x0001D914
	public void Update()
	{
		if (this.alphaPulse.enabled)
		{
			float num = Mathf.Abs(Mathf.Cos((this.alphaPulse.Offset + Time.time) * 3.1415927f / this.alphaPulse.Duration));
			if ((double)num > 0.9)
			{
				num -= 0.9f;
				num = this.NormalSize + num;
				base.transform.localScale = new Vector3(num, num, num);
			}
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0001F78D File Offset: 0x0001D98D
	public override void Reset()
	{
		this.lastMapTaskStep = -1;
		this.alphaPulse.enabled = false;
		this.rend.material.SetFloat("_Outline", 0f);
		base.Reset();
	}

	// Token: 0x040005B4 RID: 1460
	public float NormalSize = 0.3f;

	// Token: 0x040005B5 RID: 1461
	public int lastMapTaskStep = -1;

	// Token: 0x040005B6 RID: 1462
	public SpriteRenderer rend;

	// Token: 0x040005B7 RID: 1463
	public AlphaPulse alphaPulse;
}
