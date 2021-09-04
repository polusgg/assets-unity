using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class Rotater : MonoBehaviour
{
	// Token: 0x06000A4F RID: 2639 RVA: 0x00042740 File Offset: 0x00040940
	private void Update()
	{
		base.transform.localEulerAngles = new Vector3(0f, 0f, Time.time * this.DegreesPerSecond);
	}

	// Token: 0x04000BCB RID: 3019
	public float DegreesPerSecond = 360f;
}
