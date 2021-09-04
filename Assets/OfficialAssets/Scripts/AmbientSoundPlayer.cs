using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class AmbientSoundPlayer : MonoBehaviour
{
	// Token: 0x06000C5F RID: 3167 RVA: 0x0004C918 File Offset: 0x0004AB18
	public void Start()
	{
		SoundManager.Instance.PlayDynamicSound(base.name + base.GetInstanceID().ToString(), this.AmbientSound, true, new DynamicSound.GetDynamicsFunction(this.Dynamics), false);
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0004C960 File Offset: 0x0004AB60
	private void Dynamics(AudioSource source, float dt)
	{
		if (!PlayerControl.LocalPlayer)
		{
			source.volume = 0f;
			return;
		}
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		bool flag = false;
		for (int i = 0; i < this.HitAreas.Length; i++)
		{
			if (this.HitAreas[i].OverlapPoint(truePosition))
			{
				flag = true;
				break;
			}
		}
		float num = 0f;
		if (flag)
		{
			num = 1f;
		}
		else if (this.DistanceFallOff >= 0f)
		{
			float num2 = Vector2.Distance(truePosition, base.transform.position);
			num = 1f - Mathf.Clamp(num2 / this.DistanceFallOff, 0f, 1f);
		}
		source.volume = Mathf.Lerp(source.volume, num * this.MaxVolume, dt * this.FallOffRate);
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0004CA30 File Offset: 0x0004AC30
	public void OnDestroy()
	{
		SoundManager.Instance.StopNamedSound(base.name + base.GetInstanceID().ToString());
	}

	// Token: 0x04000DEC RID: 3564
	public AudioClip AmbientSound;

	// Token: 0x04000DED RID: 3565
	public Collider2D[] HitAreas;

	// Token: 0x04000DEE RID: 3566
	public float MaxVolume = 1f;

	// Token: 0x04000DEF RID: 3567
	public float DistanceFallOff = -1f;

	// Token: 0x04000DF0 RID: 3568
	public float FallOffRate = 1f;
}
