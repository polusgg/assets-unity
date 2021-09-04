using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class SweepMinigame : Minigame
{
	// Token: 0x0600034E RID: 846 RVA: 0x00015A44 File Offset: 0x00013C44
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00015A58 File Offset: 0x00013C58
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.ResetGauges();
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.SpinningSound, true, 1f);
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, true);
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00015AB3 File Offset: 0x00013CB3
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.SpinningSound);
		base.Close();
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00015ACC File Offset: 0x00013CCC
	public void FixedUpdate()
	{
		float num = Mathf.Clamp(2f - this.timer / 30f, 1f, 2f);
		this.timer += Time.fixedDeltaTime * num;
		if (this.spinnerIdx < this.Spinners.Length)
		{
			float num2 = this.CalcXPerc();
			this.Gauges[this.spinnerIdx].Value = ((num2 < 13f) ? 0.9f : 0.1f);
			Quaternion localRotation = Quaternion.Euler(0f, 0f, this.timer * this.SpinRate);
			this.Spinners[this.spinnerIdx].transform.localRotation = localRotation;
			this.Shadows[this.spinnerIdx].transform.localRotation = localRotation;
			this.Lights[this.spinnerIdx].enabled = (num2 < 13f);
		}
		for (int i = 0; i < this.Gauges.Length; i++)
		{
			HorizontalGauge horizontalGauge = this.Gauges[i];
			if (i < this.spinnerIdx)
			{
				horizontalGauge.Value = 0.95f;
			}
			if (i > this.spinnerIdx)
			{
				horizontalGauge.Value = 0.05f;
			}
			horizontalGauge.Value += (Mathf.PerlinNoise((float)i, Time.time * 51f) - 0.5f) * 0.025f;
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00015C2C File Offset: 0x00013E2C
	private float CalcXPerc()
	{
		int num = (int)(this.timer * this.SpinRate) % 360;
		return (float)Mathf.Min(360 - num, num);
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00015C5C File Offset: 0x00013E5C
	public void HitButton(int i)
	{
		if (i != this.spinnerIdx)
		{
			return;
		}
		if (this.CalcXPerc() < 13f)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
			}
			this.Spinners[this.spinnerIdx].transform.localRotation = Quaternion.identity;
			this.Shadows[this.spinnerIdx].transform.localRotation = Quaternion.identity;
			this.spinnerIdx++;
			this.timer = this.initialTimer;
			if (this.spinnerIdx >= this.Gauges.Length)
			{
				this.MyNormTask.NextStep();
				base.StartCoroutine(base.CoStartClose(0.75f));
				return;
			}
		}
		else
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.RejectSound, false, 1f);
			}
			this.ResetGauges();
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00015D48 File Offset: 0x00013F48
	private void ResetGauges()
	{
		this.spinnerIdx = 0;
		this.timer = FloatRange.Next(1f, 3f);
		this.initialTimer = this.timer;
		for (int i = 0; i < this.Gauges.Length; i++)
		{
			this.Lights[i].enabled = false;
			this.Spinners[i].transform.localRotation = Quaternion.Euler(0f, 0f, this.timer * this.SpinRate);
			this.Shadows[i].transform.localRotation = Quaternion.Euler(0f, 0f, this.timer * this.SpinRate);
		}
	}

	// Token: 0x040003CB RID: 971
	public SpriteRenderer[] Spinners;

	// Token: 0x040003CC RID: 972
	public SpriteRenderer[] Shadows;

	// Token: 0x040003CD RID: 973
	public SpriteRenderer[] Lights;

	// Token: 0x040003CE RID: 974
	public HorizontalGauge[] Gauges;

	// Token: 0x040003CF RID: 975
	private int spinnerIdx;

	// Token: 0x040003D0 RID: 976
	private float timer;

	// Token: 0x040003D1 RID: 977
	public float SpinRate = 45f;

	// Token: 0x040003D2 RID: 978
	private float initialTimer;

	// Token: 0x040003D3 RID: 979
	public AudioClip SpinningSound;

	// Token: 0x040003D4 RID: 980
	public AudioClip AcceptSound;

	// Token: 0x040003D5 RID: 981
	public AudioClip RejectSound;

	// Token: 0x040003D6 RID: 982
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040003D7 RID: 983
	public UiElement DefaultButtonSelected;

	// Token: 0x040003D8 RID: 984
	public List<UiElement> ControllerSelectable;
}
