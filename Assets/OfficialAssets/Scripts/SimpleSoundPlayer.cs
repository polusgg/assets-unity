using System;
using UnityEngine;

// Token: 0x02000199 RID: 409
public class SimpleSoundPlayer : MonoBehaviour
{
	// Token: 0x0600092A RID: 2346 RVA: 0x0003C73A File Offset: 0x0003A93A
	private void OnEnable()
	{
		this.soundSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0003C748 File Offset: 0x0003A948
	public void PlaySound()
	{
		this.soundSource.Play();
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0003C755 File Offset: 0x0003A955
	public void PlaySpecificSound(int index)
	{
		this.soundSource.clip = this.clips[index];
		this.soundSource.Play();
	}

	// Token: 0x04000AA2 RID: 2722
	public AudioClip[] clips;

	// Token: 0x04000AA3 RID: 2723
	private AudioSource soundSource;
}
