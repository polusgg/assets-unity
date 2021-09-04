using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class ShowerMinigame : Minigame
{
	// Token: 0x0600018B RID: 395 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.timer = this.MaxTime * (float)this.MyNormTask.Data[0] / 100f;
		this.PercentText.Text = ((int)(100 - this.MyNormTask.Data[0])).ToString() + "%";
		AirshipStatus airshipStatus = ShipStatus.Instance as AirshipStatus;
		if (airshipStatus && airshipStatus.ShowerParticles)
		{
			airshipStatus.ShowerParticles.Play();
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.washSound, true, 1f);
		}
		base.SetupInput(true);
		VibrationManager.Vibrate(2f, 2f, 0f, VibrationManager.VibrationFalloff.None, this.washSound, true);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000C878 File Offset: 0x0000AA78
	public void Update()
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		this.timer += Time.deltaTime;
		this.MyNormTask.Data[0] = (byte)(this.timer / this.MaxTime * 100f);
		this.Gauge.value = 1f - this.timer / this.MaxTime;
		this.PercentText.Text = ((int)(100 - this.MyNormTask.Data[0])).ToString() + "%";
		if (this.MyNormTask.Data[0] >= 100)
		{
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.5f));
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000C93A File Offset: 0x0000AB3A
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.washSound);
		AirshipStatus airshipStatus = ShipStatus.Instance as AirshipStatus;
		if (airshipStatus != null)
		{
			airshipStatus.ShowerParticles.Stop();
		}
		VibrationManager.CancelVibration(this.washSound);
		base.Close();
	}

	// Token: 0x04000252 RID: 594
	public VerticalGauge Gauge;

	// Token: 0x04000253 RID: 595
	public TextRenderer PercentText;

	// Token: 0x04000254 RID: 596
	private float timer;

	// Token: 0x04000255 RID: 597
	public float MaxTime = 12f;

	// Token: 0x04000256 RID: 598
	public AudioClip washSound;
}
