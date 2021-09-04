using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class DiagnosticGame : Minigame
{
	// Token: 0x06000658 RID: 1624 RVA: 0x00029310 File Offset: 0x00027510
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00029324 File Offset: 0x00027524
	public override void Begin(PlayerTask task)
	{
		this.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.BeginDiagnostics, Array.Empty<object>());
		base.Begin(task);
		if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.NotStarted)
		{
			base.StartCoroutine(this.BlinkButton());
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00029394 File Offset: 0x00027594
	private IEnumerator BlinkButton()
	{
		for (;;)
		{
			this.StartButton.color = Color.red;
			yield return Effects.Wait(0.5f);
			this.StartButton.color = Color.white;
			yield return Effects.Wait(0.5f);
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x000293A4 File Offset: 0x000275A4
	public void PickAnomaly(int num)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		if (this.MyNormTask.TimerStarted != NormalPlayerTask.TimerState.Finished)
		{
			return;
		}
		if (num == this.TargetNum)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.CorrectSound, false, 1f);
			}
			this.Targets[this.TargetNum].color = this.goodBarColor;
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00029428 File Offset: 0x00027628
	public void StartDiagnostic()
	{
		if (this.MyNormTask.TimerStarted != NormalPlayerTask.TimerState.NotStarted)
		{
			return;
		}
		this.StartButton.GetComponent<PassiveButton>().enabled = false;
		base.StopAllCoroutines();
		this.StartButton.color = Color.white;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.StartSound, false, 1f);
		}
		this.MyNormTask.TaskTimer = this.TimePerStep;
		this.MyNormTask.TimerStarted = NormalPlayerTask.TimerState.Started;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x000294A8 File Offset: 0x000276A8
	public void Update()
	{
		switch (this.MyNormTask.TimerStarted)
		{
		case NormalPlayerTask.TimerState.NotStarted:
			this.Gauge.gameObject.SetActive(false);
			this.Targets.ForEach(delegate(SpriteRenderer f)
			{
				f.gameObject.SetActive(false);
			});
			return;
		case NormalPlayerTask.TimerState.Started:
		{
			this.Gauge.gameObject.SetActive(true);
			this.Gauge.MaxValue = this.TimePerStep;
			this.Gauge.value = this.MyNormTask.TaskTimer;
			int num = (int)(100f * this.MyNormTask.TaskTimer / this.TimePerStep);
			if (num != this.lastPercent && Constants.ShouldPlaySfx())
			{
				this.lastPercent = num;
				SoundManager.Instance.PlaySound(this.TickSound, false, 0.8f);
			}
			this.Text.Text = num.ToString() + "%";
			this.Targets.ForEach(delegate(SpriteRenderer f)
			{
				f.gameObject.SetActive(false);
			});
			return;
		}
		case NormalPlayerTask.TimerState.Finished:
			this.Gauge.gameObject.SetActive(true);
			this.Gauge.MaxValue = 1f;
			this.Gauge.value = 1f;
			if (this.TargetNum == -1)
			{
				this.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.PickAnomaly, Array.Empty<object>());
				this.Targets.ForEach(delegate(SpriteRenderer f)
				{
					f.gameObject.SetActive(true);
					f.color = this.goodBarColor;
				});
				this.TargetNum = this.Targets.RandomIdx<SpriteRenderer>();
				this.Targets[this.TargetNum].color = Color.red;
			}
			return;
		default:
			return;
		}
	}

	// Token: 0x04000723 RID: 1827
	public VerticalGauge Gauge;

	// Token: 0x04000724 RID: 1828
	public SpriteRenderer StartButton;

	// Token: 0x04000725 RID: 1829
	public float TimePerStep = 90f;

	// Token: 0x04000726 RID: 1830
	public TextRenderer Text;

	// Token: 0x04000727 RID: 1831
	private int TargetNum = -1;

	// Token: 0x04000728 RID: 1832
	public SpriteRenderer[] Targets;

	// Token: 0x04000729 RID: 1833
	private Color goodBarColor = new Color32(100, 193, byte.MaxValue, byte.MaxValue);

	// Token: 0x0400072A RID: 1834
	public AudioClip StartSound;

	// Token: 0x0400072B RID: 1835
	public AudioClip CorrectSound;

	// Token: 0x0400072C RID: 1836
	public AudioClip TickSound;

	// Token: 0x0400072D RID: 1837
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400072E RID: 1838
	public UiElement DefaultButtonSelected;

	// Token: 0x0400072F RID: 1839
	public List<UiElement> ControllerSelectable;

	// Token: 0x04000730 RID: 1840
	private int lastPercent;
}
