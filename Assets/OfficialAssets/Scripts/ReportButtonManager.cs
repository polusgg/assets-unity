using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class ReportButtonManager : MonoBehaviour
{
	// Token: 0x0600059D RID: 1437 RVA: 0x00024FD8 File Offset: 0x000231D8
	public void SetActive(bool isActive)
	{
		if (isActive)
		{
			this.renderer.color = Palette.EnabledColor;
			this.renderer.material.SetFloat("_Desat", 0f);
			return;
		}
		this.renderer.color = Palette.DisabledClear;
		this.renderer.material.SetFloat("_Desat", 1f);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0002503D File Offset: 0x0002323D
	public void DoClick()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		PlayerControl.LocalPlayer.ReportClosest();
	}

	// Token: 0x04000639 RID: 1593
	public SpriteRenderer renderer;
}
