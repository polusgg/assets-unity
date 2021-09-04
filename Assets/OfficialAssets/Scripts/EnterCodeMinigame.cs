using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class EnterCodeMinigame : Minigame
{
	// Token: 0x06000098 RID: 152 RVA: 0x00003F26 File Offset: 0x00002126
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003F38 File Offset: 0x00002138
	public void ShowCard()
	{
		base.StartCoroutine(this.CoShowCard());
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003F47 File Offset: 0x00002147
	private IEnumerator CoShowCard()
	{
		if (this.cardOut)
		{
			yield break;
		}
		this.cardOut = true;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.WalletOut, false, 1f);
		}
		Vector3 pos = this.Card.transform.localPosition;
		Vector3 targ = new Vector3(pos.x, 0.84f, pos.z);
		float time = 0f;
		for (;;)
		{
			float num = Mathf.Min(1f, time / 0.6f);
			this.Card.transform.localPosition = Vector3.Lerp(pos, targ, num);
			this.Card.transform.localScale = Vector3.Lerp(Vector3.one * 0.75f, Vector3.one, num);
			if (time > 0.6f)
			{
				break;
			}
			yield return null;
			time += Time.deltaTime;
		}
		yield break;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003F58 File Offset: 0x00002158
	public void EnterDigit(int i)
	{
		if (this.animating)
		{
			return;
		}
		if (this.done)
		{
			return;
		}
		if (this.NumberText.Text.Length >= 5)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.RejectSound, false, 1f);
			}
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.NumberSound, false, 1f).pitch = Mathf.Lerp(0.8f, 1.2f, (float)i / 9f);
		}
		this.numString += i.ToString();
		this.number = this.number * 10 + i;
		this.NumberText.Text = this.numString;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000401C File Offset: 0x0000221C
	public void ClearDigits()
	{
		if (this.animating)
		{
			return;
		}
		this.number = 0;
		this.numString = string.Empty;
		this.NumberText.Text = string.Empty;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.NumberSound, false, 1f);
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00004072 File Offset: 0x00002272
	public void AcceptDigits()
	{
		if (this.animating)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.NumberSound, false, 1f);
		}
		base.StartCoroutine(this.Animate());
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000040A8 File Offset: 0x000022A8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.targetNumber = BitConverter.ToInt32(this.MyNormTask.Data, 0);
		this.NumberText.Text = string.Empty;
		this.TargetText.Text = this.targetNumber.ToString("D5");
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00004121 File Offset: 0x00002321
	private IEnumerator Animate()
	{
		this.animating = true;
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		yield return wait;
		this.NumberText.Text = string.Empty;
		yield return wait;
		if (this.targetNumber == this.number)
		{
			this.done = true;
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
			}
			this.MyNormTask.NextStep();
			string okStr = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.OK, Array.Empty<object>());
			this.NumberText.Text = okStr;
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = okStr;
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return base.CoStartClose(0.5f);
			okStr = null;
		}
		else
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.RejectSound, false, 1f);
			}
			string okStr = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Bad, Array.Empty<object>());
			this.NumberText.Text = okStr;
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = okStr;
			yield return wait;
			this.numString = string.Empty;
			this.number = 0;
			this.NumberText.Text = this.numString;
			okStr = null;
		}
		this.animating = false;
		yield break;
	}

	// Token: 0x0400005D RID: 93
	public TextRenderer NumberText;

	// Token: 0x0400005E RID: 94
	public TextRenderer TargetText;

	// Token: 0x0400005F RID: 95
	public SpriteRenderer Card;

	// Token: 0x04000060 RID: 96
	public int number;

	// Token: 0x04000061 RID: 97
	public string numString = string.Empty;

	// Token: 0x04000062 RID: 98
	private bool animating;

	// Token: 0x04000063 RID: 99
	private bool cardOut;

	// Token: 0x04000064 RID: 100
	private bool done;

	// Token: 0x04000065 RID: 101
	private int targetNumber;

	// Token: 0x04000066 RID: 102
	public AudioClip WalletOut;

	// Token: 0x04000067 RID: 103
	public AudioClip NumberSound;

	// Token: 0x04000068 RID: 104
	public AudioClip AcceptSound;

	// Token: 0x04000069 RID: 105
	public AudioClip RejectSound;

	// Token: 0x0400006A RID: 106
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400006B RID: 107
	public UiElement DefaultButtonSelected;

	// Token: 0x0400006C RID: 108
	public List<UiElement> ControllerSelectable;
}
