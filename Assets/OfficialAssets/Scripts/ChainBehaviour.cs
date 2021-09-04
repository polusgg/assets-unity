using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class ChainBehaviour : MonoBehaviour
{
	// Token: 0x06000B4D RID: 2893 RVA: 0x00048008 File Offset: 0x00046208
	public void Awake()
	{
		this.swingTime = FloatRange.Next(0f, this.SwingPeriod);
		this.vec.z = this.SwingRange.Lerp(Mathf.Sin(this.swingTime));
		base.transform.eulerAngles = this.vec;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00048060 File Offset: 0x00046260
	public void Update()
	{
		this.swingTime += Time.deltaTime / this.SwingPeriod;
		this.vec.z = this.SwingRange.Lerp(Mathf.Sin(this.swingTime * 3.1415927f) / 2f + 0.5f);
		base.transform.eulerAngles = this.vec;
	}

	// Token: 0x04000CC0 RID: 3264
	public FloatRange SwingRange = new FloatRange(0f, 30f);

	// Token: 0x04000CC1 RID: 3265
	public float SwingPeriod = 2f;

	// Token: 0x04000CC2 RID: 3266
	public float swingTime;

	// Token: 0x04000CC3 RID: 3267
	private Vector3 vec;
}
