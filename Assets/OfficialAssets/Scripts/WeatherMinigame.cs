using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class WeatherMinigame : Minigame
{
	// Token: 0x060001A2 RID: 418 RVA: 0x0000D38E File Offset: 0x0000B58E
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected);
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000D3B3 File Offset: 0x0000B5B3
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000D3C5 File Offset: 0x0000B5C5
	public void StartStopFill()
	{
		this.StartButton.enabled = false;
		base.StartCoroutine(this.CoDoAnimation());
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
	private IEnumerator CoDoAnimation()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.StartSound, false, 1f);
		}
		yield return Effects.ScaleIn(this.StartButton.transform, 1f, 0f, 0.15f);
		this.EtaText.gameObject.SetActive(true);
		yield return Effects.ScaleIn(this.EtaText.transform, 0f, 1f, 0.15f);
		for (float timer = 0f; timer < this.Duration; timer += Time.deltaTime)
		{
			int num = Mathf.CeilToInt(this.Duration - timer);
			this.EtaText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WeatherEta, new object[]
			{
				num
			});
			this.destGauge1.Value = Mathf.Lerp(0f, 1f, timer / this.Duration * 5f);
			this.destGauge2.Value = Mathf.Lerp(0f, 1f, timer / this.Duration * 3f);
			this.destGauge3.Value = Mathf.Lerp(0f, 1f, timer / this.Duration);
			yield return null;
		}
		this.EtaText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WeatherComplete, Array.Empty<object>());
		this.MyNormTask.NextStep();
		yield return base.CoStartClose(0.75f);
		yield break;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000D3EF File Offset: 0x0000B5EF
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.StartSound);
		base.Close();
	}

	// Token: 0x04000277 RID: 631
	public float Duration = 5f;

	// Token: 0x04000278 RID: 632
	public HorizontalGauge destGauge1;

	// Token: 0x04000279 RID: 633
	public HorizontalGauge destGauge2;

	// Token: 0x0400027A RID: 634
	public HorizontalGauge destGauge3;

	// Token: 0x0400027B RID: 635
	public PassiveButton StartButton;

	// Token: 0x0400027C RID: 636
	public TextRenderer EtaText;

	// Token: 0x0400027D RID: 637
	public AudioClip StartSound;

	// Token: 0x0400027E RID: 638
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400027F RID: 639
	public UiElement DefaultButtonSelected;
}
