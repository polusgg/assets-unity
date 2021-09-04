using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class WaterStage : Minigame
{
	// Token: 0x060007D8 RID: 2008 RVA: 0x00031FBA File Offset: 0x000301BA
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.timer = (float)this.MyNormTask.Data[0] / 255f;
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00031FE0 File Offset: 0x000301E0
	public void FixedUpdate()
	{
		if (this.complete)
		{
			return;
		}
		if (this.isDown && this.timer < 1f)
		{
			this.timer += Time.fixedDeltaTime / this.RefuelDuration;
			this.MyNormTask.Data[0] = (byte)Mathf.Min(255f, this.timer * 255f);
			if (this.timer >= 1f)
			{
				this.complete = true;
				if (this.greenLight)
				{
					this.greenLight.color = this.green;
				}
				if (this.redLight)
				{
					this.redLight.color = this.darkRed;
				}
				this.MyNormTask.Data[0] = 0;
				this.MyNormTask.NextStep();
			}
		}
		if (this.destGauge)
		{
			this.destGauge.value = this.timer;
		}
		if (this.srcGauge)
		{
			this.srcGauge.value = 1f - this.timer;
		}
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x000320FC File Offset: 0x000302FC
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

	// Token: 0x060007DB RID: 2011 RVA: 0x000321A8 File Offset: 0x000303A8
	private void GetRefuelDynamics(AudioSource player, float dt)
	{
		player.volume = 1f;
		if (this.MyNormTask.taskStep == 0)
		{
			player.pitch = Mathf.Lerp(0.75f, 1.25f, this.timer);
			return;
		}
		player.pitch = Mathf.Lerp(1.25f, 0.75f, this.timer);
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00032204 File Offset: 0x00030404
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.RefuelSound);
		if (Minigame.Instance)
		{
			Minigame.Instance.Close();
		}
	}

	// Token: 0x040008FE RID: 2302
	public float RefuelDuration = 5f;

	// Token: 0x040008FF RID: 2303
	private Color darkRed = new Color32(90, 0, 0, byte.MaxValue);

	// Token: 0x04000900 RID: 2304
	private Color red = new Color32(byte.MaxValue, 58, 0, byte.MaxValue);

	// Token: 0x04000901 RID: 2305
	private Color green = Color.green;

	// Token: 0x04000902 RID: 2306
	public SpriteRenderer redLight;

	// Token: 0x04000903 RID: 2307
	public SpriteRenderer greenLight;

	// Token: 0x04000904 RID: 2308
	public VerticalGauge srcGauge;

	// Token: 0x04000905 RID: 2309
	public VerticalGauge destGauge;

	// Token: 0x04000906 RID: 2310
	public AudioClip RefuelSound;

	// Token: 0x04000907 RID: 2311
	private float timer;

	// Token: 0x04000908 RID: 2312
	private bool isDown;

	// Token: 0x04000909 RID: 2313
	private bool complete;
}
