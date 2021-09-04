using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class EngineBehaviour : MonoBehaviour
{
	// Token: 0x06000380 RID: 896 RVA: 0x0001777E File Offset: 0x0001597E
	public void PlayElectricSound()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlayDynamicSound("EngineShock" + base.name, this.ElectricSound, false, new DynamicSound.GetDynamicsFunction(this.GetSoundDistance), false);
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000177B8 File Offset: 0x000159B8
	public void PlaySteamSound()
	{
		if (Constants.ShouldPlaySfx())
		{
			float pitch = FloatRange.Next(0.7f, 1.1f);
			SoundManager.Instance.PlayDynamicSound("EngineSteam" + base.name, this.SteamSound, false, delegate(AudioSource p, float d)
			{
				this.GetSoundDistance(p, d, pitch);
			}, false);
			VibrationManager.Vibrate(1f, base.transform.position, this.SoundDistance, 0f, VibrationManager.VibrationFalloff.None, this.SteamSound, false);
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001784A File Offset: 0x00015A4A
	private void GetSoundDistance(AudioSource player, float dt)
	{
		this.GetSoundDistance(player, dt, 1f);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001785C File Offset: 0x00015A5C
	private void GetSoundDistance(AudioSource player, float dt, float pitch)
	{
		if (!PlayerControl.LocalPlayer)
		{
			player.volume = 0f;
			return;
		}
		float num = Vector2.Distance(base.transform.position, PlayerControl.LocalPlayer.GetTruePosition());
		float num2 = 1f - num / this.SoundDistance;
		player.volume = num2 * 0.8f;
		player.pitch = pitch;
	}

	// Token: 0x04000423 RID: 1059
	public AudioClip ElectricSound;

	// Token: 0x04000424 RID: 1060
	public AudioClip SteamSound;

	// Token: 0x04000425 RID: 1061
	public float SoundDistance = 5f;
}
