using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class SpriteScroller : MonoBehaviour
{
	// Token: 0x06000CA7 RID: 3239 RVA: 0x0004DFCC File Offset: 0x0004C1CC
	private void Update()
	{
		if (this.rend)
		{
			this.rend.material.SetTextureOffset("_MainTex", new Vector2(Time.time * this.XRate, Time.time * this.YRate));
		}
	}

	// Token: 0x04000E28 RID: 3624
	public Renderer rend;

	// Token: 0x04000E29 RID: 3625
	public float XRate = 1f;

	// Token: 0x04000E2A RID: 3626
	public float YRate = 1f;
}
