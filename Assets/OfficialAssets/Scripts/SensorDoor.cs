using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class SensorDoor : MonoBehaviour
{
	// Token: 0x060003F7 RID: 1015 RVA: 0x0001A5EE File Offset: 0x000187EE
	public void OnEnable()
	{
		base.InvokeRepeating("CheckDoor", 0.1f, 0.1f);
		this.LeftSide.SetCooldownNormalizedUvs();
		this.RightSide.SetCooldownNormalizedUvs();
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0001A61B File Offset: 0x0001881B
	[ContextMenu("Set Right Uvs")]
	public void SetUvs()
	{
		this.RightSide.SetCooldownNormalizedUvs();
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0001A628 File Offset: 0x00018828
	private void Update()
	{
		if (this.Opening && this.openTimer < this.OpenDuration)
		{
			this.openTimer += Time.deltaTime;
			float num = Mathf.SmoothStep(0f, 1f, this.openTimer / this.OpenDuration);
			this.LeftSide.material.SetFloat("_Percent", num);
			this.RightSide.material.SetFloat("_Percent", num);
			return;
		}
		if (!this.Opening && this.openTimer > 0f)
		{
			this.openTimer -= Time.deltaTime;
			float num2 = Mathf.SmoothStep(0f, 1f, this.openTimer / this.OpenDuration);
			this.LeftSide.material.SetFloat("_Percent", num2);
			this.RightSide.material.SetFloat("_Percent", num2);
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0001A718 File Offset: 0x00018918
	private void CheckDoor()
	{
		bool opening = this.Opening;
		this.Opening = PhysicsHelpers.CircleContains(base.transform.position, this.ActivationDistance, Constants.PlayersOnlyMask);
		if (opening && !this.Opening)
		{
			SoundManager.Instance.StopSound(this.OpenSound);
			if (Vector2.Distance(base.transform.position, PlayerControl.LocalPlayer.GetTruePosition()) < 3f)
			{
				SoundManager.Instance.PlaySound(this.CloseSound, false, 1f);
			}
			VibrationManager.Vibrate(3f, base.transform.position, 3f, this.OpenDuration, VibrationManager.VibrationFalloff.None, this.CloseSound, false);
			return;
		}
		if (!opening && this.Opening)
		{
			SoundManager.Instance.StopSound(this.CloseSound);
			if (Vector2.Distance(base.transform.position, PlayerControl.LocalPlayer.GetTruePosition()) < 3f)
			{
				SoundManager.Instance.PlaySound(this.OpenSound, false, 1f);
			}
			VibrationManager.Vibrate(3f, base.transform.position, 3f, this.OpenDuration, VibrationManager.VibrationFalloff.None, this.OpenSound, false);
		}
	}

	// Token: 0x040004AA RID: 1194
	public SpriteRenderer LeftSide;

	// Token: 0x040004AB RID: 1195
	public SpriteRenderer RightSide;

	// Token: 0x040004AC RID: 1196
	public float ActivationDistance = 2f;

	// Token: 0x040004AD RID: 1197
	public bool Opening;

	// Token: 0x040004AE RID: 1198
	public float OpenDuration;

	// Token: 0x040004AF RID: 1199
	private float openTimer;

	// Token: 0x040004B0 RID: 1200
	public AudioClip OpenSound;

	// Token: 0x040004B1 RID: 1201
	public AudioClip CloseSound;

	// Token: 0x040004B2 RID: 1202
	private const float slideVibrationIntensity = 3f;
}
