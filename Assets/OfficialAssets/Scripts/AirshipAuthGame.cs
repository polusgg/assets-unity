using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class AirshipAuthGame : Minigame
{
	// Token: 0x060000CC RID: 204 RVA: 0x00005580 File Offset: 0x00003780
	public override void Begin(PlayerTask task)
	{
		this.OtherConsoleId = (base.ConsoleId + 1) % 2;
		base.Begin(task);
		this.system = (ShipStatus.Instance.Systems[SystemTypes.Reactor] as HeliSabotageSystem);
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 64 | base.ConsoleId);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x000055D3 File Offset: 0x000037D3
	public override void Close()
	{
		base.Close();
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 32 | base.ConsoleId);
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000055F0 File Offset: 0x000037F0
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
			StringNames id = (this.OtherConsoleId == 1) ? StringNames.AuthRightOkay : StringNames.AuthLeftOkay;
			this.OtherStatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(id, Array.Empty<object>());
			return;
		}
		if (this.system.IsConsoleActive(this.OtherConsoleId))
		{
			this.TheirLight.color = (this.evenColor ? Color.white : Color.yellow);
			StringNames id2 = (this.OtherConsoleId == 1) ? StringNames.AuthRightActive : StringNames.AuthLeftActive;
			this.OtherStatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(id2, Array.Empty<object>());
			return;
		}
		this.TheirLight.color = Color.red;
		StringNames id3 = (this.OtherConsoleId == 1) ? StringNames.AuthRightNotActive : StringNames.AuthLeftNotActive;
		this.OtherStatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(id3, Array.Empty<object>());
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00005838 File Offset: 0x00003A38
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

	// Token: 0x060000D0 RID: 208 RVA: 0x000058BC File Offset: 0x00003ABC
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

	// Token: 0x060000D1 RID: 209 RVA: 0x00005912 File Offset: 0x00003B12
	public void Enter()
	{
		if (this.animating)
		{
			return;
		}
		base.StartCoroutine(this.Animate());
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000592A File Offset: 0x00003B2A
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
			ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16 | base.ConsoleId);
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

	// Token: 0x040000C6 RID: 198
	public TextRenderer TargetText;

	// Token: 0x040000C7 RID: 199
	public TextRenderer NumberText;

	// Token: 0x040000C8 RID: 200
	public TextRenderer OtherStatusText;

	// Token: 0x040000C9 RID: 201
	public int number;

	// Token: 0x040000CA RID: 202
	public string numString = string.Empty;

	// Token: 0x040000CB RID: 203
	private bool animating;

	// Token: 0x040000CC RID: 204
	private HeliSabotageSystem system;

	// Token: 0x040000CD RID: 205
	public SpriteRenderer OurLight;

	// Token: 0x040000CE RID: 206
	public SpriteRenderer TheirLight;

	// Token: 0x040000CF RID: 207
	public SpriteRenderer TimeBar;

	// Token: 0x040000D0 RID: 208
	public AudioClip ButtonSound;

	// Token: 0x040000D1 RID: 209
	public AudioClip AcceptSound;

	// Token: 0x040000D2 RID: 210
	public AudioClip RejectSound;

	// Token: 0x040000D3 RID: 211
	private int OtherConsoleId;

	// Token: 0x040000D4 RID: 212
	private bool evenColor;
}
