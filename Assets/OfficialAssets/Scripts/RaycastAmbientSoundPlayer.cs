using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class RaycastAmbientSoundPlayer : MonoBehaviour
{
	// Token: 0x06000C76 RID: 3190 RVA: 0x0004CCAB File Offset: 0x0004AEAB
	private void OnEnable()
	{
		RaycastAmbientSoundPlayer.players.Add(this);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0004CCB8 File Offset: 0x0004AEB8
	private void OnDisable()
	{
		RaycastAmbientSoundPlayer.players.Remove(this);
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0004CCC8 File Offset: 0x0004AEC8
	public void Start()
	{
		if (Constants.ShouldPlaySfx() && this.AmbientSound)
		{
			SoundManager.Instance.PlayDynamicSound("Amb " + base.name, this.AmbientSound, true, delegate(AudioSource player, float dt)
			{
				this.GetAmbientSoundVolume(player, dt);
			}, false);
		}
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0004CD18 File Offset: 0x0004AF18
	private void GetAmbientSoundVolume(AudioSource player, float dt)
	{
		this.ambientVolume = 0f;
		if (!PlayerControl.LocalPlayer)
		{
			player.volume = 0f;
			return;
		}
		Vector2 vector = base.transform.position;
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		float num = Vector2.Distance(vector, truePosition);
		if (num > this.AmbientMaxDist)
		{
			player.volume = 0f;
			return;
		}
		Vector2 vector2 = truePosition - vector;
		int num2 = Physics2D.RaycastNonAlloc(vector, vector2, this.volumeBuffer, num, Constants.ShipOnlyMask);
		float num3 = 1f - num / this.AmbientMaxDist - (float)num2 * this.HitModifier;
		player.volume = Mathf.Lerp(player.volume, num3 * this.AmbientVolume, dt);
		this.ambientVolume = player.volume;
	}

	// Token: 0x04000DFB RID: 3579
	public AudioClip AmbientSound;

	// Token: 0x04000DFC RID: 3580
	public float AmbientVolume = 1f;

	// Token: 0x04000DFD RID: 3581
	public float AmbientMaxDist = 8f;

	// Token: 0x04000DFE RID: 3582
	public float HitModifier = 0.25f;

	// Token: 0x04000DFF RID: 3583
	public static List<RaycastAmbientSoundPlayer> players = new List<RaycastAmbientSoundPlayer>();

	// Token: 0x04000E00 RID: 3584
	public float ambientVolume;

	// Token: 0x04000E01 RID: 3585
	public float t;

	// Token: 0x04000E02 RID: 3586
	private RaycastHit2D[] volumeBuffer = new RaycastHit2D[5];
}
