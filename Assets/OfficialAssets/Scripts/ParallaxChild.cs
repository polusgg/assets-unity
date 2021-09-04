using System;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class ParallaxChild : MonoBehaviour
{
	// Token: 0x060007A6 RID: 1958 RVA: 0x000311BC File Offset: 0x0002F3BC
	public void OnEnable()
	{
		this.BasePosition = base.transform.localPosition;
	}

	// Token: 0x040008B4 RID: 2228
	[HideInInspector]
	public Vector3 BasePosition;
}
