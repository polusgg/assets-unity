using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class HoverAnimBehaviour : MonoBehaviour
{
	// Token: 0x0600040F RID: 1039 RVA: 0x0001ACDD File Offset: 0x00018EDD
	private void Start()
	{
		this.offset = FloatRange.Next(0f, 10f);
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001ACF4 File Offset: 0x00018EF4
	private void Update()
	{
		Vector3 localPosition = base.transform.localPosition;
		float v = Mathf.Sin((Time.time + this.offset) * this.Speed) / 2f + this.Shift;
		localPosition.y = this.YMovement.Lerp(v);
		base.transform.localPosition = localPosition;
	}

	// Token: 0x040004CC RID: 1228
	public FloatRange YMovement;

	// Token: 0x040004CD RID: 1229
	public float Speed = 1f;

	// Token: 0x040004CE RID: 1230
	public float Shift = 1f;

	// Token: 0x040004CF RID: 1231
	private float offset;
}
