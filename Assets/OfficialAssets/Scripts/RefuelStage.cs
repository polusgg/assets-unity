using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class RefuelStage : Minigame
{
	// Token: 0x0600038B RID: 907 RVA: 0x00017A1D File Offset: 0x00015C1D
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.timer = (float)this.MyNormTask.Data[0] / 255f;
		base.SetupInput(true);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00017A48 File Offset: 0x00015C48
	public void FixedUpdate()
	{
		//if (ReInput.players.GetPlayer(0).GetButton(21))
		//{
		//	if (!this.isDown)
		//	{
		//		this.usingController = true;
		//		this.Refuel();
		//	}
		//}
		//else if (this.isDown && this.usingController)
		//{
		//	this.usingController = false;
		//	this.Refuel();
		//}
		//if (this.complete)
		//{
		//	return;
		//}
		//if (this.isDown && this.timer < 1f)
		//{
		//	this.timer += Time.fixedDeltaTime / this.RefuelDuration;
		//	this.MyNormTask.Data[0] = (byte)Mathf.Min(255f, this.timer * 255f);
		//	if (this.timer >= 1f)
		//	{
		//		this.complete = true;
		//		if (this.greenLight)
		//		{
		//			this.greenLight.color = this.green;
		//		}
		//		if (this.redLight)
		//		{
		//			this.redLight.color = this.darkRed;
		//		}
		//		if (this.MyNormTask.MaxStep == 1)
		//		{
		//			this.MyNormTask.NextStep();
		//		}
		//		else if (this.MyNormTask.StartAt == SystemTypes.CargoBay || this.MyNormTask.StartAt == SystemTypes.Engine)
		//		{
		//			this.MyNormTask.Data[0] = 0;
		//			this.MyNormTask.Data[1] = (BoolRange.Next(0.5f) ? 1 : 2);
		//			this.MyNormTask.NextStep();
		//		}
		//		else
		//		{
		//			this.MyNormTask.Data[0] = 0;
		//			byte[] data = this.MyNormTask.Data;
		//			int num = 1;
		//			data[num] += 1;
		//			if (this.MyNormTask.Data[1] % 2 == 0)
		//			{
		//				this.MyNormTask.NextStep();
		//			}
		//			this.MyNormTask.UpdateArrow();
		//		}
		//	}
		//}
		//this.destGauge.value = this.timer;
		//if (this.srcGauge)
		//{
		//	this.srcGauge.value = 1f - this.timer;
		//}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00017C48 File Offset: 0x00015E48
	public void Refuel()
	{
		if (this.complete)
		{
			base.transform.parent.GetComponent<Minigame>().Close();
			return;
		}
		this.isDown = !this.isDown;
		if (this.redLight)
		{
			this.redLight.color = (this.isDown ? this.red : this.darkRed);
		}
		if (this.isDown)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlayDynamicSound("Refuel", this.RefuelSound, true, new DynamicSound.GetDynamicsFunction(this.GetRefuelDynamics), true);
				return;
			}
		}
		else
		{
			SoundManager.Instance.StopSound(this.RefuelSound);
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00017CF4 File Offset: 0x00015EF4
	private void GetRefuelDynamics(AudioSource player, float dt)
	{
		player.volume = 1f;
		player.pitch = Mathf.Lerp(0.75f, 1.25f, this.timer);
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00017D1C File Offset: 0x00015F1C
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.RefuelSound);
		if (Minigame.Instance)
		{
			Minigame.Instance.Close();
		}
	}

	// Token: 0x0400042C RID: 1068
	public float RefuelDuration = 5f;

	// Token: 0x0400042D RID: 1069
	private Color darkRed = new Color32(90, 0, 0, byte.MaxValue);

	// Token: 0x0400042E RID: 1070
	private Color red = new Color32(byte.MaxValue, 58, 0, byte.MaxValue);

	// Token: 0x0400042F RID: 1071
	private Color green = Color.green;

	// Token: 0x04000430 RID: 1072
	public SpriteRenderer redLight;

	// Token: 0x04000431 RID: 1073
	public SpriteRenderer greenLight;

	// Token: 0x04000432 RID: 1074
	public VerticalGauge srcGauge;

	// Token: 0x04000433 RID: 1075
	public VerticalGauge destGauge;

	// Token: 0x04000434 RID: 1076
	public AudioClip RefuelSound;

	// Token: 0x04000435 RID: 1077
	private float timer;

	// Token: 0x04000436 RID: 1078
	private bool isDown;

	// Token: 0x04000437 RID: 1079
	private bool complete;

	// Token: 0x04000438 RID: 1080
	private bool usingController;
}
