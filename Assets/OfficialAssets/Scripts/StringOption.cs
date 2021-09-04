using System;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class StringOption : OptionBehaviour
{
	// Token: 0x06000B1E RID: 2846 RVA: 0x0004632C File Offset: 0x0004452C
	public void OnEnable()
	{
		GameOptionsData gameOptions = PlayerControl.GameOptions;
		StringNames title = this.Title;
		if (title != StringNames.GameKillDistance)
		{
			if (title != StringNames.GameTaskBarMode)
			{
				Debug.Log("Ono, unrecognized setting: " + this.Title.ToString());
			}
			else
			{
				this.Value = (int)gameOptions.TaskBarMode;
			}
		}
		else
		{
			this.Value = gameOptions.KillDistance;
		}
		this.TitleText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.Title, Array.Empty<object>());
		this.ValueText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.Values[this.Value], Array.Empty<object>());
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x000463DC File Offset: 0x000445DC
	private void FixedUpdate()
	{
		if (this.oldValue != this.Value)
		{
			this.oldValue = this.Value;
			this.ValueText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.Values[this.Value], Array.Empty<object>());
		}
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0004642A File Offset: 0x0004462A
	public void Increase()
	{
		this.Value = Mathf.Clamp(this.Value + 1, 0, this.Values.Length - 1);
		this.OnValueChanged(this);
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00046456 File Offset: 0x00044656
	public void Decrease()
	{
		this.Value = Mathf.Clamp(this.Value - 1, 0, this.Values.Length - 1);
		this.OnValueChanged(this);
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00046482 File Offset: 0x00044682
	public override int GetInt()
	{
		return this.Value;
	}

	// Token: 0x04000C82 RID: 3202
	public TextRenderer TitleText;

	// Token: 0x04000C83 RID: 3203
	public TextRenderer ValueText;

	// Token: 0x04000C84 RID: 3204
	public StringNames[] Values;

	// Token: 0x04000C85 RID: 3205
	public int Value;

	// Token: 0x04000C86 RID: 3206
	private int oldValue = -1;
}
