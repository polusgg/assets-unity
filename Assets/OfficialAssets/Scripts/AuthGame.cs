using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class AuthGame : Minigame
{
	// Token: 0x060003CF RID: 975 RVA: 0x000197E3 File Offset: 0x000179E3
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x000197F8 File Offset: 0x000179F8
	public override void Begin(PlayerTask task)
	{
		this.OtherConsoleId = (base.ConsoleId + 1) % 2;
		base.Begin(task);
		this.system = (ShipStatus.Instance.Systems[SystemTypes.Comms] as HqHudSystemType);
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 64 | base.ConsoleId);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00019870 File Offset: 0x00017A70
	public override void Close()
	{
		base.Close();
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 32 | base.ConsoleId);
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00019890 File Offset: 0x00017A90
	public void Update()
	{
		this.evenColor = ((int)Time.time * 2 % 2 == 0);
		Vector3 localScale = this.TimeBar.transform.localScale;
		localScale.x = this.system.PercentActive;
		this.TimeBar.transform.localScale = localScale;
		if (this.system.PercentActive < 0.25f)
		{
			this.TimeBar.color = new Color(1f, 0.45f, 0.25f);
		}
		else if ((double)this.system.PercentActive < 0.5)
		{
			this.TimeBar.color = Color.yellow;
		}
		else
		{
			this.TimeBar.color = Color.white;
		}
		this.TargetText.Text = this.system.TargetNumber.ToString("D5");
		if (this.system.IsConsoleOkay(base.ConsoleId))
		{
			this.OurLight.color = Color.green;
		}
		else
		{
			this.OurLight.color = (this.evenColor ? Color.white : Color.yellow);
		}
		if (this.amClosing == Minigame.CloseState.None && !this.system.IsActive)
		{
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
		if (this.system.IsConsoleOkay(this.OtherConsoleId))
		{
			this.TheirLight.color = Color.green;
			StringNames id = (this.OtherConsoleId == 1) ? StringNames.AuthOfficeOkay : StringNames.AuthCommsOkay;
			this.OtherStatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(id, Array.Empty<object>());
			return;
		}
		if (this.system.IsConsoleActive(this.OtherConsoleId))
		{
			this.TheirLight.color = (this.evenColor ? Color.white : Color.yellow);
			StringNames id2 = (this.OtherConsoleId == 1) ? StringNames.AuthOfficeActive : StringNames.AuthCommsActive;
			this.OtherStatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(id2, Array.Empty<object>());
			return;
		}
		this.TheirLight.color = Color.red;
		StringNames id3 = (this.OtherConsoleId == 1) ? StringNames.AuthOfficeNotActive : StringNames.AuthCommsNotActive;
		this.OtherStatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(id3, Array.Empty<object>());
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00019AD8 File Offset: 0x00017CD8
	public void ClickNumber(int i)
	{
		if (this.animating)
		{
			return;
		}
		if (this.NumberText.Text.Length >= 5)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ButtonSound, false, 1f);
		}
		this.numString += i.ToString();
		this.number = this.number * 10 + i;
		this.NumberText.Text = this.numString;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00019B5C File Offset: 0x00017D5C
	public void ClearEntry()
	{
		if (this.animating)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ButtonSound, false, 1f);
		}
		this.number = 0;
		this.numString = string.Empty;
		this.NumberText.Text = string.Empty;
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00019BB2 File Offset: 0x00017DB2
	public void Enter()
	{
		if (this.animating)
		{
			return;
		}
		base.StartCoroutine(this.Animate());
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00019BCA File Offset: 0x00017DCA
	private IEnumerator Animate()
	{
		this.animating = true;
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		yield return wait;
		this.NumberText.Text = string.Empty;
		yield return wait;
		if (this.system.TargetNumber == this.number)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
			}
			ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | base.ConsoleId);
			try
			{
				((SabotageTask)this.MyTask).MarkContributed();
			}
			catch
			{
			}
			this.NumberText.Text = "OK";
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = "OK";
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = "OK";
		}
		else
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.RejectSound, false, 1f);
			}
			this.NumberText.Text = "Bad";
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = "Bad";
			yield return wait;
			this.numString = string.Empty;
			this.number = 0;
			this.NumberText.Text = this.numString;
		}
		this.number = 0;
		this.numString = string.Empty;
		this.NumberText.Text = string.Empty;
		this.animating = false;
		yield break;
	}

	// Token: 0x04000474 RID: 1140
	public TextRenderer TargetText;

	// Token: 0x04000475 RID: 1141
	public TextRenderer NumberText;

	// Token: 0x04000476 RID: 1142
	public TextRenderer OtherStatusText;

	// Token: 0x04000477 RID: 1143
	public int number;

	// Token: 0x04000478 RID: 1144
	public string numString = string.Empty;

	// Token: 0x04000479 RID: 1145
	private bool animating;

	// Token: 0x0400047A RID: 1146
	private HqHudSystemType system;

	// Token: 0x0400047B RID: 1147
	public SpriteRenderer OurLight;

	// Token: 0x0400047C RID: 1148
	public SpriteRenderer TheirLight;

	// Token: 0x0400047D RID: 1149
	public SpriteRenderer TimeBar;

	// Token: 0x0400047E RID: 1150
	public AudioClip ButtonSound;

	// Token: 0x0400047F RID: 1151
	public AudioClip AcceptSound;

	// Token: 0x04000480 RID: 1152
	public AudioClip RejectSound;

	// Token: 0x04000481 RID: 1153
	private int OtherConsoleId;

	// Token: 0x04000482 RID: 1154
	private bool evenColor;

	// Token: 0x04000483 RID: 1155
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000484 RID: 1156
	public UiElement DefaultButtonSelected;

	// Token: 0x04000485 RID: 1157
	public List<UiElement> ControllerSelectable;
}
