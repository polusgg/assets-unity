using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class ShadowCamera : MonoBehaviour
{
	// Token: 0x06000B40 RID: 2880 RVA: 0x00047B68 File Offset: 0x00045D68
	public void OnEnable()
	{
		base.GetComponent<Camera>().SetReplacementShader(this.Shadozer, "RenderType");
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00047B80 File Offset: 0x00045D80
	public void OnDisable()
	{
		base.GetComponent<Camera>().ResetReplacementShader();
	}

	// Token: 0x04000CB4 RID: 3252
	public Shader Shadozer;
}
