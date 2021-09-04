using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class AutoParallaxController : MonoBehaviour
{
	// Token: 0x0600019D RID: 413 RVA: 0x0000D21E File Offset: 0x0000B41E
	public void Start()
	{
		this.Children = base.GetComponentsInChildren<ParallaxChild>();
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000D22C File Offset: 0x0000B42C
	public void Update()
	{
		if (!PlayerControl.LocalPlayer)
		{
			return;
		}
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		for (int i = 0; i < this.Children.Length; i++)
		{
			ParallaxChild parallaxChild = this.Children[i];
			Vector3 basePosition = parallaxChild.BasePosition;
			if (basePosition.z < 0f)
			{
				basePosition.z = -basePosition.z;
			}
			basePosition.x -= truePosition.x / (basePosition.z * this.XScale);
			basePosition.y -= truePosition.y / (basePosition.z * this.YScale);
			parallaxChild.transform.localPosition = basePosition;
		}
	}

	// Token: 0x0400026F RID: 623
	public ParallaxChild[] Children;

	// Token: 0x04000270 RID: 624
	public float XScale = 1f;

	// Token: 0x04000271 RID: 625
	public float YScale = 1f;
}
