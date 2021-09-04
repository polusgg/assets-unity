using System;
using PowerTools;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class SurvCamera : MonoBehaviour
{
	// Token: 0x06000C2E RID: 3118 RVA: 0x0004BBDE File Offset: 0x00049DDE
	public void Awake()
	{
		if (this.Images == null || this.Images.Length == 0)
		{
			this.Images = base.GetComponents<SpriteAnim>();
		}
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0004BC00 File Offset: 0x00049E00
	public virtual void SetAnimation(bool on)
	{
		SpriteAnim[] images = this.Images;
		for (int i = 0; i < images.Length; i++)
		{
			images[i].Play(on ? this.OnAnim : this.OffAnim, 1f);
		}
	}

	// Token: 0x04000D91 RID: 3473
	public string CamName;

	// Token: 0x04000D92 RID: 3474
	public StringNames NewName;

	// Token: 0x04000D93 RID: 3475
	public SpriteAnim[] Images;

	// Token: 0x04000D94 RID: 3476
	public float CamSize = 3f;

	// Token: 0x04000D95 RID: 3477
	public float CamAspect = 1f;

	// Token: 0x04000D96 RID: 3478
	public Vector3 Offset;

	// Token: 0x04000D97 RID: 3479
	public AnimationClip OnAnim;

	// Token: 0x04000D98 RID: 3480
	public AnimationClip OffAnim;

	// Token: 0x04000D99 RID: 3481
	public StringNames camNameString;
}
