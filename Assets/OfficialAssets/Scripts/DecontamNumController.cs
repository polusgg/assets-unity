using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class DecontamNumController : MonoBehaviour
{
	// Token: 0x060003EE RID: 1006 RVA: 0x0001A284 File Offset: 0x00018484
	[ContextMenu("Space Evenly")]
	public void SpaceEvenly()
	{
		List<float> list = FloatRange.SpreadToEdges(-2.9f, 2.9f, this.Images.Length).ToList<float>();
		for (int i = 0; i < this.Images.Length; i++)
		{
			this.Images[i].transform.localPosition = new Vector3(0f, list[i], 0.1f);
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0001A2EC File Offset: 0x000184EC
	internal void SetSecond(float curSecond, float maxSecond)
	{
		for (int i = 0; i < this.Images.Length; i++)
		{
			int num = Mathf.CeilToInt(Mathf.Lerp(0f, (float)(this.NumImages.Length - 1), curSecond / maxSecond));
			this.Images[i].sprite = this.NumImages[num];
		}
	}

	// Token: 0x0400049E RID: 1182
	public SpriteRenderer[] Images;

	// Token: 0x0400049F RID: 1183
	public Sprite[] NumImages;
}
