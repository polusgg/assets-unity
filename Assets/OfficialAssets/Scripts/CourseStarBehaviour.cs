using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class CourseStarBehaviour : MonoBehaviour
{
	// Token: 0x06000798 RID: 1944 RVA: 0x00030980 File Offset: 0x0002EB80
	public void Update()
	{
		this.Upper.transform.Rotate(0f, 0f, Time.deltaTime * this.Speed);
		this.Lower.transform.Rotate(0f, 0f, Time.deltaTime * this.Speed);
	}

	// Token: 0x0400089F RID: 2207
	public SpriteRenderer Upper;

	// Token: 0x040008A0 RID: 2208
	public SpriteRenderer Lower;

	// Token: 0x040008A1 RID: 2209
	public float Speed = 30f;
}
