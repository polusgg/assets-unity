using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[CreateAssetMenu]
public class SoundGroup : ScriptableObject
{
	// Token: 0x060002AD RID: 685 RVA: 0x00010FE1 File Offset: 0x0000F1E1
	public AudioClip Random()
	{
		return this.Clips.Random<AudioClip>();
	}

	// Token: 0x04000314 RID: 788
	public AudioClip[] Clips;
}
