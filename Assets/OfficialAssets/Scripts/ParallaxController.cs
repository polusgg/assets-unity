using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class ParallaxController : MonoBehaviour
{
	// Token: 0x060007A8 RID: 1960 RVA: 0x000311D7 File Offset: 0x0002F3D7
	public void Start()
	{
		this.Children = base.GetComponentsInChildren<ParallaxChild>();
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000311E8 File Offset: 0x0002F3E8
	public void SetParallax(float x)
	{
		for (int i = 0; i < this.Children.Length; i++)
		{
			ParallaxChild parallaxChild = this.Children[i];
			Vector3 basePosition = parallaxChild.BasePosition;
			float scale = this.Scale;
			if (basePosition.z >= 0f)
			{
				basePosition.x += x / (basePosition.z * this.Scale + 1f);
			}
			else
			{
				basePosition.x += x * (-basePosition.z * this.Scale + 1f);
			}
			parallaxChild.transform.localPosition = basePosition;
		}
	}

	// Token: 0x040008B5 RID: 2229
	public ParallaxChild[] Children;

	// Token: 0x040008B6 RID: 2230
	public float Scale = 1f;
}
