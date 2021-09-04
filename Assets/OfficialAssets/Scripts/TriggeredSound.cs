using System;
using UnityEngine;

// Token: 0x02000215 RID: 533
public class TriggeredSound : MonoBehaviour
{
	// Token: 0x06000C96 RID: 3222 RVA: 0x0004D5B8 File Offset: 0x0004B7B8
	public void Start()
	{
		this.Player = SoundManager.Instance.GetNamedAudioSource(base.name + base.GetInstanceID().ToString());
		this.Player.playOnAwake = false;
		this.Player.loop = false;
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0004D608 File Offset: 0x0004B808
	public void PlaySound()
	{
		if (!this.Player)
		{
			return;
		}
		if (this.SoundToPlay == null)
		{
			return;
		}
		if (this.PitchRange == null)
		{
			return;
		}
		this.Player.clip = this.SoundToPlay.Random<AudioClip>();
		this.Player.pitch = this.PitchRange.Next();
		this.GetAmbientSoundVolume(this.Player);
		this.Player.Play();
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0004D678 File Offset: 0x0004B878
	private void GetAmbientSoundVolume(AudioSource player)
	{
		if (!PlayerControl.LocalPlayer)
		{
			player.volume = 0f;
			return;
		}
		Vector2 vector = base.transform.position;
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		float num = Vector2.Distance(vector, truePosition);
		if (num > this.MaxDist)
		{
			player.volume = 0f;
			return;
		}
		ContactFilter2D contactFilter2D = default(ContactFilter2D);
		contactFilter2D.useTriggers = false;
		contactFilter2D.layerMask = Constants.ShipOnlyMask;
		contactFilter2D.useLayerMask = true;
		Vector2 vector2 = truePosition - vector;
		int num2 = Physics2D.Raycast(vector, vector2, contactFilter2D, this.volumeBuffer, num);
		float num3 = 1f - num / this.MaxDist - (float)num2 * this.HitModifier;
		player.volume = num3 * this.MaxVolume;
	}

	// Token: 0x04000E12 RID: 3602
	public AudioClip[] SoundToPlay;

	// Token: 0x04000E13 RID: 3603
	public FloatRange PitchRange = new FloatRange(1f, 1f);

	// Token: 0x04000E14 RID: 3604
	private AudioSource Player;

	// Token: 0x04000E15 RID: 3605
	public float MaxVolume = 1f;

	// Token: 0x04000E16 RID: 3606
	public float MaxDist = 6f;

	// Token: 0x04000E17 RID: 3607
	public float HitModifier = 0.25f;

	// Token: 0x04000E18 RID: 3608
	private RaycastHit2D[] volumeBuffer = new RaycastHit2D[5];
}
