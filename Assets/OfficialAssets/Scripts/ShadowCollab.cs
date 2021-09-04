using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class ShadowCollab : MonoBehaviour
{
	// Token: 0x06000B24 RID: 2852 RVA: 0x00046499 File Offset: 0x00044699
	public void OnEnable()
	{
		base.StartCoroutine(this.Run());
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x000464A8 File Offset: 0x000446A8
	private IEnumerator Run()
	{
		Camera cam = Camera.main;
		for (;;)
		{
			if (this.oldAspect != cam.aspect)
			{
				this.oldAspect = cam.aspect;
				this.ShadowCamera.aspect = cam.aspect;
				this.ShadowCamera.orthographicSize = cam.orthographicSize;
				this.ShadowQuad.transform.localScale = new Vector3(cam.orthographicSize * cam.aspect, cam.orthographicSize) * 2f;
			}
			yield return Effects.Wait(1f);
		}
	}

	// Token: 0x04000C87 RID: 3207
	public Camera ShadowCamera;

	// Token: 0x04000C88 RID: 3208
	public MeshRenderer ShadowQuad;

	// Token: 0x04000C89 RID: 3209
	private float oldAspect;
}
