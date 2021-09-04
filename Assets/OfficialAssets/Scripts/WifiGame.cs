using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class WifiGame : Minigame
{
	// Token: 0x0600082B RID: 2091 RVA: 0x0003567D File Offset: 0x0003387D
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.NotStarted)
		{
			this.TurnOn(true);
		}
		else
		{
			this.TurnOff(true);
		}
		base.SetupInput(true);
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000356AC File Offset: 0x000338AC
	public void Update()
	{
		//if (this.MyNormTask.IsComplete)
		//{
		//	return;
		//}
		//this.controller.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	float num = ReInput.players.GetPlayer(0).GetAxis(17);
		//	if (!this.WifiOff && this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.NotStarted)
		//	{
		//		num = Mathf.Clamp01(-num);
		//		float value = this.Slider.Value - num * Time.deltaTime * 3f;
		//		this.Slider.SetValue(value);
		//	}
		//	else if (this.WifiOff && this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.Finished)
		//	{
		//		num = Mathf.Clamp01(num);
		//		float value2 = this.Slider.Value + num * Time.deltaTime * 3f;
		//		this.Slider.SetValue(value2);
		//	}
		//}
		//if (this.WifiOff)
		//{
		//	if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.Finished)
		//	{
		//		this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WifiPleasePowerOn, Array.Empty<object>());
		//	}
		//	else if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.Started)
		//	{
		//		this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WifiPleaseReturnIn, new object[]
		//		{
		//			Mathf.CeilToInt(this.MyNormTask.TaskTimer)
		//		});
		//	}
		//}
		//if (!this.WifiOff && (double)this.Slider.Value < 0.1)
		//{
		//	this.TurnOff(false);
		//	return;
		//}
		//if (this.WifiOff && (double)this.Slider.Value > 0.9)
		//{
		//	this.TurnOn(false);
		//}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00035848 File Offset: 0x00033A48
	private void TurnOn(bool first = false)
	{
		if (Constants.ShouldPlaySfx() && !first)
		{
			SoundManager.Instance.PlaySound(this.SliderClick, false, 1f);
		}
		this.Slider.Value = 1f;
		this.Slider.UpdateValue();
		this.WifiOff = false;
		if (!first)
		{
			base.StopAllCoroutines();
		}
		base.StartCoroutine(this.RunLights(this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.Finished));
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000358BC File Offset: 0x00033ABC
	private void TurnOff(bool first = false)
	{
		if (Constants.ShouldPlaySfx() && !first)
		{
			SoundManager.Instance.PlaySound(this.SliderClick, false, 1f);
		}
		this.Slider.Value = 0f;
		this.Slider.UpdateValue();
		this.WifiOff = true;
		if (!first)
		{
			base.StopAllCoroutines();
		}
		this.Lights.ForEach(delegate(SpriteRenderer s)
		{
			s.sprite = this.LightOff;
		});
		if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.NotStarted)
		{
			this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WifiPleaseReturnIn, Array.Empty<object>()) + " " + 60.ToString();
			this.MyNormTask.TaskTimer = 60f;
			this.MyNormTask.TimerStarted = NormalPlayerTask.TimerState.Started;
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00035988 File Offset: 0x00033B88
	private IEnumerator RunLights(bool finishing)
	{
		if (!finishing)
		{
			this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WifiRebootRequired, Array.Empty<object>());
			this.Lights.ForEach(delegate(SpriteRenderer s)
			{
				s.sprite = this.LightOn;
			});
			this.Lights[1].sprite = this.LightOff;
			this.Lights[3].sprite = this.LightOff;
			this.Lights[4].sprite = this.LightOff;
			this.Lights[6].sprite = this.LightOff;
			yield return Effects.All(new IEnumerator[]
			{
				this.CoBlinkLight(this.Lights[2], 0.3f)
			});
		}
		else
		{
			this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WifiPleaseWait, Array.Empty<object>());
			for (float timer = 0f; timer < 3f; timer += Time.deltaTime)
			{
				float num = timer / 3f;
				for (int i = 0; i < this.Lights.Length; i++)
				{
					float num2 = 0.75f * (float)i / (float)this.Lights.Length;
					float num3 = 0.75f * (float)(i + 1) / (float)this.Lights.Length;
					this.Lights[i].sprite = ((num > num2 && num < num3) ? this.LightOn : this.LightOff);
				}
				yield return null;
			}
			this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WifiRebootComplete, Array.Empty<object>());
			this.Lights.ForEach(delegate(SpriteRenderer s)
			{
				s.sprite = this.LightOn;
			});
			base.StartCoroutine(Effects.All(new IEnumerator[]
			{
				this.CoBlinkLight(this.Lights[3], 0.1f),
				this.CoBlinkLight(this.Lights[4], 0.09f),
				this.CoBlinkLight(this.Lights[5], 0.1f),
				this.CoBlinkLight(this.Lights[6], 0.05f),
				this.CoBlinkLight(this.Lights[7], 0.5f)
			}));
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
		yield break;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0003599E File Offset: 0x00033B9E
	private IEnumerator CoBlinkLight(SpriteRenderer light, float delay)
	{
		for (;;)
		{
			light.sprite = this.LightOn;
			yield return Effects.Wait(delay);
			light.sprite = this.LightOff;
			yield return Effects.Wait(delay);
		}
	}

	// Token: 0x040009A5 RID: 2469
	private const int WaitDuration = 60;

	// Token: 0x040009A6 RID: 2470
	public SlideBar Slider;

	// Token: 0x040009A7 RID: 2471
	public TextRenderer StatusText;

	// Token: 0x040009A8 RID: 2472
	public SpriteRenderer[] Lights;

	// Token: 0x040009A9 RID: 2473
	public Sprite LightOn;

	// Token: 0x040009AA RID: 2474
	public Sprite LightOff;

	// Token: 0x040009AB RID: 2475
	public AudioClip SliderClick;

	// Token: 0x040009AC RID: 2476
	private bool WifiOff;

	// Token: 0x040009AD RID: 2477
	private Controller controller = new Controller();
}
