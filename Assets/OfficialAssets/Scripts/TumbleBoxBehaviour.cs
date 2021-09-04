using System;
using UnityEngine;

// Token: 0x02000209 RID: 521
public class TumbleBoxBehaviour : MonoBehaviour
{
	// Token: 0x06000C42 RID: 3138 RVA: 0x0004BEE8 File Offset: 0x0004A0E8
	public void FixedUpdate()
	{
		float num = Time.time * 15f;
		float v = Mathf.Cos(Time.time * 3.1415927f / 10f) / 2f + 0.5f;
		float num2 = this.shadowScale.Lerp(v);
		this.Shadow.transform.localScale = new Vector3(num2, num2, num2);
		float num3 = this.BoxHeight.Lerp(v);
		this.Box.transform.localPosition = new Vector3(0f, num3, -0.01f);
		this.Box.transform.eulerAngles = new Vector3(0f, 0f, num);
	}

	// Token: 0x04000DD1 RID: 3537
	public FloatRange BoxHeight;

	// Token: 0x04000DD2 RID: 3538
	public FloatRange shadowScale;

	// Token: 0x04000DD3 RID: 3539
	public SpriteRenderer Shadow;

	// Token: 0x04000DD4 RID: 3540
	public SpriteRenderer Box;
}
