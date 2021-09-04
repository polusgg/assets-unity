using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class TuneRadioMinigame : Minigame
{
	// Token: 0x060001AE RID: 430 RVA: 0x0000D760 File Offset: 0x0000B960
    public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.targetAngle = this.dial.DialRange.Next();
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlayDynamicSound("CommsRadio", this.RadioSound, true, new DynamicSound.GetDynamicsFunction(this.GetRadioVolume), true);
			SoundManager.Instance.PlayDynamicSound("RadioStatic", this.StaticSound, true, new DynamicSound.GetDynamicsFunction(this.GetStaticVolume), true);
		}
		base.SetupInput(true);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
	private void GetRadioVolume(AudioSource player, float dt)
	{
		player.volume = 1f - this.actualSignal.NoiseLevel;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000D7F9 File Offset: 0x0000B9F9
	private void GetStaticVolume(AudioSource player, float dt)
	{
		player.volume = this.actualSignal.NoiseLevel;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000D80C File Offset: 0x0000BA0C
	public void Update()
	{
		//if (this.finished)
		//{
		//	return;
		//}
		//Vector2 axis2DRaw = ReInput.players.GetPlayer(0).GetAxis2DRaw(16, 17);
		//if (axis2DRaw.sqrMagnitude > 0.9f)
		//{
		//	Vector2 normalized = axis2DRaw.normalized;
		//	float value = Vector2.SignedAngle(Vector2.up, normalized);
		//	value = this.dial.DialRange.Clamp(value);
		//	this.dial.SetValue(value);
		//}
		//float num = Mathf.Abs((this.targetAngle - this.dial.Value) / this.dial.DialRange.Width) * 2f;
		//this.actualSignal.NoiseLevel = Mathf.Clamp(Mathf.Sqrt(num), 0f, 1f);
		//if (this.actualSignal.NoiseLevel <= this.Tolerance)
		//{
		//	if (!this.dial.Engaged)
		//	{
		//		this.FinishGame();
		//		return;
		//	}
		//	this.redLight.color = new Color(Mathf.Lerp(1f, 0.35f, this.steadyTimer), 0f, 0f);
		//	this.steadyTimer += Time.deltaTime;
		//	if (this.steadyTimer > 1f)
		//	{
		//		this.FinishGame();
		//		return;
		//	}
		//}
		//else
		//{
		//	this.redLight.color = new Color(1f, 0f, 0f);
		//	this.steadyTimer = 0f;
		//}
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000D96C File Offset: 0x0000BB6C
	private void FinishGame()
	{
		this.greenLight.color = Color.green;
		this.finished = true;
		this.dial.enabled = false;
		this.dial.SetValue(this.targetAngle);
		this.actualSignal.NoiseLevel = 0f;
		if (PlayerControl.LocalPlayer)
		{
			ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 0);
		}
		base.StartCoroutine(base.CoStartClose(0.75f));
		try
		{
			((SabotageTask)this.MyTask).MarkContributed();
		}
		catch
		{
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000DA10 File Offset: 0x0000BC10
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.StaticSound);
		SoundManager.Instance.StopSound(this.RadioSound);
		base.Close();
	}

	// Token: 0x04000293 RID: 659
	public RadioWaveBehaviour actualSignal;

	// Token: 0x04000294 RID: 660
	public DialBehaviour dial;

	// Token: 0x04000295 RID: 661
	public SpriteRenderer redLight;

	// Token: 0x04000296 RID: 662
	public SpriteRenderer greenLight;

	// Token: 0x04000297 RID: 663
	public float Tolerance = 0.1f;

	// Token: 0x04000298 RID: 664
	public float targetAngle;

	// Token: 0x04000299 RID: 665
	public bool finished;

	// Token: 0x0400029A RID: 666
	private float steadyTimer;

	// Token: 0x0400029B RID: 667
	public AudioClip StaticSound;

	// Token: 0x0400029C RID: 668
	public AudioClip RadioSound;
}
