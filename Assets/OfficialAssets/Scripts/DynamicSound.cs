using System;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class DynamicSound : ISoundPlayer
{
	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0004CC29 File Offset: 0x0004AE29
	// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0004CC31 File Offset: 0x0004AE31
	public string Name { get; set; }

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0004CC3A File Offset: 0x0004AE3A
	// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0004CC42 File Offset: 0x0004AE42
	public AudioSource Player { get; set; }

	// Token: 0x06000C73 RID: 3187 RVA: 0x0004CC4B File Offset: 0x0004AE4B
	public void Update(float dt)
	{
		if (!this.Player.isPlaying)
		{
			return;
		}
		this.volumeFunc(this.Player, dt);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0004CC6D File Offset: 0x0004AE6D
	public void SetTarget(AudioClip clip, DynamicSound.GetDynamicsFunction volumeFunc)
	{
		this.volumeFunc = volumeFunc;
		this.Player.clip = clip;
		this.volumeFunc(this.Player, 1f);
		this.Player.Play();
	}

	// Token: 0x04000DFA RID: 3578
	public DynamicSound.GetDynamicsFunction volumeFunc;

	// Token: 0x02000449 RID: 1097
	// (Invoke) Token: 0x06001A49 RID: 6729
	public delegate void GetDynamicsFunction(AudioSource source, float dt);
}
