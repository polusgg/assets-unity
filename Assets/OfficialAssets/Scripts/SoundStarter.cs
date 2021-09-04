using System;
using UnityEngine;

// Token: 0x02000213 RID: 531
public class SoundStarter : MonoBehaviour
{
	// Token: 0x06000C8F RID: 3215 RVA: 0x0004D49B File Offset: 0x0004B69B
	public void Awake()
	{
		if (this.StopAll)
		{
			SoundManager.Instance.StopAllSound();
		}
		SoundManager.Instance.CrossFadeSound(this.Name, this.SoundToPlay, this.Volume, 1.5f);
	}

	// Token: 0x04000E0A RID: 3594
	public string Name;

	// Token: 0x04000E0B RID: 3595
	public AudioClip SoundToPlay;

	// Token: 0x04000E0C RID: 3596
	public bool StopAll;

	// Token: 0x04000E0D RID: 3597
	[Range(0f, 1f)]
	public float Volume = 1f;
}
