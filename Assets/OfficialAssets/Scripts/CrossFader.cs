using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class CrossFader : ISoundPlayer
{
	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0004CA89 File Offset: 0x0004AC89
	// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0004CA91 File Offset: 0x0004AC91
	public string Name { get; set; }

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0004CA9A File Offset: 0x0004AC9A
	// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0004CAA2 File Offset: 0x0004ACA2
	public AudioSource Player { get; set; }

	// Token: 0x06000C67 RID: 3175 RVA: 0x0004CAAC File Offset: 0x0004ACAC
	public void Update(float dt)
	{
		if (this.timer < this.Duration)
		{
			this.timer += dt;
			float num = this.timer / this.Duration;
			if (num < 0.5f)
			{
				this.Player.volume = (1f - num * 2f) * this.MaxVolume;
				return;
			}
			if (!this.didSwitch)
			{
				this.didSwitch = true;
				this.Player.Stop();
				this.Player.clip = this.target;
				if (this.target)
				{
					this.Player.Play();
				}
			}
			this.Player.volume = (num - 0.5f) * 2f * this.MaxVolume;
		}
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0004CB70 File Offset: 0x0004AD70
	public void SetTarget(AudioClip clip)
	{
		if (!this.Player.clip)
		{
			this.didSwitch = false;
			this.Player.volume = 0f;
			this.timer = 0.5f;
		}
		else
		{
			if (this.Player.clip == clip)
			{
				return;
			}
			if (this.didSwitch)
			{
				this.didSwitch = false;
				if (this.timer >= this.Duration)
				{
					this.timer = 0f;
				}
				else
				{
					this.timer = this.Duration - this.timer;
				}
			}
		}
		this.target = clip;
	}

	// Token: 0x04000DF2 RID: 3570
	public float MaxVolume = 1f;

	// Token: 0x04000DF4 RID: 3572
	public AudioClip target;

	// Token: 0x04000DF5 RID: 3573
	public float Duration = 1.5f;

	// Token: 0x04000DF6 RID: 3574
	private float timer;

	// Token: 0x04000DF7 RID: 3575
	private bool didSwitch;
}
