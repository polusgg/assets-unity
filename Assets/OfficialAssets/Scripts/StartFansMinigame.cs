using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class StartFansMinigame : Minigame
{
	// Token: 0x06000170 RID: 368 RVA: 0x0000B330 File Offset: 0x00009530
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		if (base.ConsoleId == 0)
		{
			this.ActionText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.RevealCode, Array.Empty<object>());
		}
		else
		{
			this.ActionText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EnterCode, Array.Empty<object>());
		}
		byte[] data = this.MyNormTask.Data;
		for (int i = 0; i < this.CodeIcons.Length; i++)
		{
			SpriteRenderer target = this.CodeIcons[i];
			this.CodeIcons[i].transform.parent.gameObject.SetActive(false);
			if (base.ConsoleId == 0)
			{
				target.sprite = this.IconSprites[(int)data[i]];
			}
			else
			{
				this.CodeIcons[i].GetComponent<PassiveButton>().OnClick.AddListener(delegate()
				{
					this.RotateImage(target);
				});
			}
		}
		List<UiElement> list = new List<UiElement>(1);
		list.Add(this.mainCodeButton);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.closeButton, this.mainCodeButton, list, false);
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000B455 File Offset: 0x00009655
	public override void Close()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
		base.Close();
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000B470 File Offset: 0x00009670
	public void RevealCode()
	{
		for (int i = 0; i < this.CodeIcons.Length; i++)
		{
			this.CodeIcons[i].transform.parent.gameObject.SetActive(true);
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.revealSound, false, 1f);
		}
		if (this.MyNormTask.taskStep == 0)
		{
			this.MyNormTask.NextStep();
		}
		if (base.ConsoleId != 0)
		{
			ControllerManager.Instance.CloseOverlayMenu(base.name);
			ControllerManager.Instance.OpenOverlayMenu(base.name, this.closeButton, this.codeButtons[0], this.codeButtons, false);
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000B524 File Offset: 0x00009724
	public void RotateImage(SpriteRenderer target)
	{
		if (base.ConsoleId == 0)
		{
			return;
		}
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		int num = this.IconSprites.IndexOf(target.sprite);
		num = (num + 1) % this.IconSprites.Length;
		target.sprite = this.IconSprites[num];
		for (int i = 0; i < this.CodeIcons.Length; i++)
		{
			SpriteRenderer spriteRenderer = this.CodeIcons[i];
			int num2 = this.IconSprites.IndexOf(spriteRenderer.sprite);
			if ((int)this.MyNormTask.Data[i] != num2)
			{
				if (Constants.ShouldPlaySfx())
				{
					SoundManager.Instance.PlaySound(this.cycleSound, false, 1f);
				}
				return;
			}
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.completeSound, false, 1f);
		}
		this.MyNormTask.NextStep();
		base.StartCoroutine(base.CoStartClose(0.75f));
	}

	// Token: 0x040001FD RID: 509
	public TextRenderer ActionText;

	// Token: 0x040001FE RID: 510
	public SpriteRenderer[] CodeIcons;

	// Token: 0x040001FF RID: 511
	public Sprite[] IconSprites;

	// Token: 0x04000200 RID: 512
	public AudioClip revealSound;

	// Token: 0x04000201 RID: 513
	public AudioClip cycleSound;

	// Token: 0x04000202 RID: 514
	public AudioClip completeSound;

	// Token: 0x04000203 RID: 515
	public PassiveButton mainCodeButton;

	// Token: 0x04000204 RID: 516
	public PassiveButton closeButton;

	// Token: 0x04000205 RID: 517
	public List<UiElement> codeButtons;

	// Token: 0x04000206 RID: 518
	public ControllerButtonBehavior enterCodeHotkey;
}
