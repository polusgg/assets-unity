using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
public static class MeshRendererExtensions
{
	// Token: 0x06000252 RID: 594 RVA: 0x0000FB00 File Offset: 0x0000DD00
	public static void SetSprite(this MeshRenderer self, Texture2D spr)
	{
		if (spr != null)
		{
			self.SetCutout(spr);
			self.material.color = Color.white;
			return;
		}
		self.SetCutout(null);
		self.material.color = Color.clear;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000FB3A File Offset: 0x0000DD3A
	public static void SetCutout(this MeshRenderer self, Texture2D txt)
	{
		self.material.SetTexture("_MainTex", txt);
		self.material.SetTexture("_EmissionMap", txt);
	}
}
