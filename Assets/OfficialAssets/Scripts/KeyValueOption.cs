using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class KeyValueOption : OptionBehaviour
{
	// Token: 0x06000AEA RID: 2794 RVA: 0x000454A8 File Offset: 0x000436A8
	public void OnEnable()
	{
		GameOptionsData gameOptions = PlayerControl.GameOptions;
		if (this.Title == StringNames.GameMapName)
		{
			this.Selected = (int)((gameOptions.MapId > 3) ? (gameOptions.MapId - 1) : gameOptions.MapId);
		}
		else
		{
			Debug.Log("Ono, unrecognized setting: " + this.Title.ToString());
		}
		this.TitleText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.Title, Array.Empty<object>());
		this.ValueText.Text = this.Values[Mathf.Clamp(this.Selected, 0, this.Values.Count - 1)].Key;
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x00045560 File Offset: 0x00043760
	private void FixedUpdate()
	{
		if (this.oldValue != this.Selected)
		{
			this.oldValue = this.Selected;
			this.ValueText.Text = this.Values[this.Selected].Key;
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x000455AB File Offset: 0x000437AB
	public void Increase()
	{
		this.Selected = Mathf.Clamp(this.Selected + 1, 0, this.Values.Count - 1);
		this.OnValueChanged(this);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x000455DA File Offset: 0x000437DA
	public void Decrease()
	{
		this.Selected = Mathf.Clamp(this.Selected - 1, 0, this.Values.Count - 1);
		this.OnValueChanged(this);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0004560C File Offset: 0x0004380C
	public override int GetInt()
	{
		return this.Values[this.Selected].Value;
	}

	// Token: 0x04000C48 RID: 3144
	public TextRenderer TitleText;

	// Token: 0x04000C49 RID: 3145
	public TextRenderer ValueText;

	// Token: 0x04000C4A RID: 3146
	public List<KeyValuePair<string, int>> Values;

	// Token: 0x04000C4B RID: 3147
	private int Selected;

	// Token: 0x04000C4C RID: 3148
	private int oldValue = -1;
}
