using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class LoadingMarquee : MonoBehaviour
{
	// Token: 0x0600007F RID: 127 RVA: 0x00003BF8 File Offset: 0x00001DF8
	public void Start()
	{
		Camera main = Camera.main;
		float num = main.orthographicSize * main.aspect * 1.1f;
		this.XRange = new FloatRange(num, -num);
		base.transform.localPosition = new Vector3(num, -main.orthographicSize + this.distanceFromBottom, -1f);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003C54 File Offset: 0x00001E54
	public void Update()
	{
		this.timer += Time.deltaTime;
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x = this.XRange.Lerp(this.timer / this.duration % 1f);
		base.transform.localPosition = localPosition;
	}

	// Token: 0x04000051 RID: 81
	public float duration = 5f;

	// Token: 0x04000052 RID: 82
	public float distanceFromBottom = 0.25f;

	// Token: 0x04000053 RID: 83
	private FloatRange XRange;

	// Token: 0x04000054 RID: 84
	private float timer;
}
