using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class KeypadGame : Minigame
{
	// Token: 0x06000660 RID: 1632 RVA: 0x000296C4 File Offset: 0x000278C4
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x000296D8 File Offset: 0x000278D8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.oxyTask = (NoOxyTask)task;
		this.TargetText.Text = "today's code:\r\n" + this.oxyTask.targetNumber.ToString("D5");
		this.NumberText.Text = string.Empty;
		this.system = (LifeSuppSystemType)ShipStatus.Instance.Systems[SystemTypes.LifeSupp];
		this.done = this.system.GetConsoleComplete(base.ConsoleId);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x00029788 File Offset: 0x00027988
	public void ClickNumber(int i)
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
			base.StartCoroutine(this.BlinkAccept());
			return;
		}
		this.numString += i.ToString();
		this.number = this.number * 10 + i;
		this.NumberText.Text = this.numString;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x00029802 File Offset: 0x00027A02
	private IEnumerator BlinkAccept()
	{
		int num;
		for (int i = 0; i < 5; i = num)
		{
			this.AcceptButton.color = Color.gray;
			yield return null;
			yield return null;
			this.AcceptButton.color = Color.white;
			yield return null;
			yield return null;
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00029811 File Offset: 0x00027A11
	public void ClearEntry()
	{
		if (this.animating)
		{
			return;
		}
		this.number = 0;
		this.numString = string.Empty;
		this.NumberText.Text = string.Empty;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0002983E File Offset: 0x00027A3E
	public void Enter()
	{
		if (this.animating)
		{
			return;
		}
		base.StartCoroutine(this.Animate());
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00029856 File Offset: 0x00027A56
	private IEnumerator Animate()
	{
		this.animating = true;
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		yield return wait;
		this.NumberText.Text = string.Empty;
		yield return wait;
		if (this.oxyTask.targetNumber == this.number)
		{
			this.done = true;
			byte amount = (byte)(base.ConsoleId | 64);
			ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, (int)amount);
			try
			{
				((SabotageTask)this.MyTask).MarkContributed();
			}
			catch
			{
			}
			string okStr = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.OK, Array.Empty<object>());
			this.NumberText.Text = okStr;
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = okStr;
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = okStr;
			base.StartCoroutine(base.CoStartClose(0.75f));
			okStr = null;
		}
		else
		{
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

	// Token: 0x04000731 RID: 1841
	public TextRenderer TargetText;

	// Token: 0x04000732 RID: 1842
	public TextRenderer NumberText;

	// Token: 0x04000733 RID: 1843
	public int number;

	// Token: 0x04000734 RID: 1844
	public string numString = string.Empty;

	// Token: 0x04000735 RID: 1845
	private bool animating;

	// Token: 0x04000736 RID: 1846
	public SpriteRenderer AcceptButton;

	// Token: 0x04000737 RID: 1847
	private LifeSuppSystemType system;

	// Token: 0x04000738 RID: 1848
	private NoOxyTask oxyTask;

	// Token: 0x04000739 RID: 1849
	private bool done;

	// Token: 0x0400073A RID: 1850
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400073B RID: 1851
	public UiElement DefaultButtonSelected;

	// Token: 0x0400073C RID: 1852
	public List<UiElement> ControllerSelectable;
}
