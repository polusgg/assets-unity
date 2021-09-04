using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class SkeldShipRoom : PlainShipRoom, IStepWatcher
{
	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0004B90D File Offset: 0x00049B0D
	public int Priority { get; }

	// Token: 0x06000C24 RID: 3108 RVA: 0x0004B918 File Offset: 0x00049B18
	public void Start()
	{
		if (Constants.ShouldPlaySfx() && this.AmbientSound)
		{
			SoundManager.Instance.PlayDynamicSound("Amb " + this.RoomId.ToString(), this.AmbientSound, true, delegate(AudioSource player, float dt)
			{
				this.GetAmbientSoundVolume(player, dt);
			}, false);
		}
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0004B974 File Offset: 0x00049B74
	public SoundGroup MakeFootstep(PlayerControl player)
	{
		if (!DestroyableSingleton<HudManager>.InstanceExists)
		{
			return null;
		}
		RoomTracker roomTracker = DestroyableSingleton<HudManager>.Instance.roomTracker;
		if (roomTracker && roomTracker.LastRoom == this)
		{
			return this.FootStepSounds;
		}
		return null;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0004B9B4 File Offset: 0x00049BB4
	private void GetAmbientSoundVolume(AudioSource player, float dt)
	{
		//if (!PlayerControl.LocalPlayer)
		//{
		//	player.volume = 0f;
		//	return;
		//}
		//Vector2 vector = base.transform.position + this.AmbientOffset;
		//Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		//float num = Vector2.Distance(vector, truePosition);
		//if (num > this.AmbientMaxDist)
		//{
		//	player.volume = 0f;
		//	return;
		//}
		//Vector2 vector2 = truePosition - vector;
		//int num2 = Physics2D.RaycastNonAlloc(vector, vector2, this.volumeBuffer, num, Constants.ShipOnlyMask);
		//float num3 = 1f - num / this.AmbientMaxDist - (float)num2 * 0.25f;
		//player.volume = Mathf.Lerp(player.volume, num3 * this.AmbientVolume, dt);
	}

	// Token: 0x04000D86 RID: 3462
	public AudioClip AmbientSound;

	// Token: 0x04000D87 RID: 3463
	public float AmbientVolume = 0.7f;

	// Token: 0x04000D88 RID: 3464
	public float AmbientMaxDist = 8f;

	// Token: 0x04000D89 RID: 3465
	public Vector2 AmbientOffset;

	// Token: 0x04000D8B RID: 3467
	public SoundGroup FootStepSounds;

	// Token: 0x04000D8C RID: 3468
	private RaycastHit2D[] volumeBuffer = new RaycastHit2D[5];
}
