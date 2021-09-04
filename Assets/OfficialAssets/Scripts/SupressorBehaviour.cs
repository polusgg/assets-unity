using System;
using PowerTools;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class SupressorBehaviour : MonoBehaviour
{
	// Token: 0x060007B8 RID: 1976 RVA: 0x0003155B File Offset: 0x0002F75B
	public void Activate()
	{
		this.BaseImage.sprite = this.ActiveBase;
		this.Electric.Play(this.ElectricActive, 1f);
		this.Lights.Play(this.LightsActive, 1f);
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0003159A File Offset: 0x0002F79A
	public void Deactivate()
	{
		this.BaseImage.sprite = this.InactiveBase;
		this.Electric.Play(this.ElectricInactive, 1f);
		this.Lights.Play(this.LightsInactive, 1f);
	}

	// Token: 0x040008BD RID: 2237
	public Sprite ActiveBase;

	// Token: 0x040008BE RID: 2238
	public Sprite InactiveBase;

	// Token: 0x040008BF RID: 2239
	public SpriteRenderer BaseImage;

	// Token: 0x040008C0 RID: 2240
	public AnimationClip ElectricActive;

	// Token: 0x040008C1 RID: 2241
	public AnimationClip ElectricInactive;

	// Token: 0x040008C2 RID: 2242
	public SpriteAnim Electric;

	// Token: 0x040008C3 RID: 2243
	public AnimationClip LightsActive;

	// Token: 0x040008C4 RID: 2244
	public AnimationClip LightsInactive;

	// Token: 0x040008C5 RID: 2245
	public SpriteAnim Lights;
}
