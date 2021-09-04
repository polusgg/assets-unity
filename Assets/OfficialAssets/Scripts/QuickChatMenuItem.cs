using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
[Serializable]
public class QuickChatMenuItem
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x0003A39C File Offset: 0x0003859C
	public void CreateFillInFromRegularText()
	{
		string text = this.text;
		string text2 = this.alternateText;
		if (!string.IsNullOrEmpty(text))
		{
			int num = 0;
			for (int i = 0; i < 26; i++)
			{
				string text3 = "(" + ((char)(65 + i)).ToString() + ")";
				string newValue = "{" + i.ToString() + "}";
				if (text.Contains(text3))
				{
					num++;
				}
				text = text.Replace(text3, newValue);
			}
			if (text != this.text)
			{
				this.fillInText = text;
				if (this.fillBlankSelectionsInOder == null || this.fillBlankSelectionsInOder.Length != num)
				{
					this.fillBlankSelectionsInOder = new QuickChatSubmenu[num];
				}
			}
			else
			{
				this.fillBlankSelectionsInOder = new QuickChatSubmenu[0];
			}
		}
		if (!string.IsNullOrEmpty(text2))
		{
			int num2 = 0;
			for (int j = 0; j < 26; j++)
			{
				string text4 = "(" + ((char)(65 + j)).ToString() + ")";
				string newValue2 = "{" + j.ToString() + "}";
				if (text2.Contains(text4))
				{
					num2++;
				}
				text2 = text2.Replace(text4, newValue2);
			}
			if (text2 != this.alternateText)
			{
				this.alternateFillInText = text2;
				if (this.alternateFillBlankSelectionsInOder == null || this.alternateFillBlankSelectionsInOder.Length != num2)
				{
					this.alternateFillBlankSelectionsInOder = new QuickChatSubmenu[num2];
					return;
				}
			}
			else
			{
				this.alternateFillBlankSelectionsInOder = new QuickChatSubmenu[0];
			}
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0003A518 File Offset: 0x00038718
	private string GeneratePreviewText(string formatStr)
	{
		string[] array = new string[]
		{
			"{0}",
			"{1}",
			"{2}",
			"{3}"
		};
		string[] array2 = new string[]
		{
			"(A)",
			"(B)",
			"(C)",
			"(D)"
		};
		List<QuickChatMenuItem.TempReplaceBit> list = new List<QuickChatMenuItem.TempReplaceBit>();
		for (int i = 0; i < array.Length; i++)
		{
			int num = formatStr.IndexOf(array[i]);
			if (num != -1)
			{
				list.Add(new QuickChatMenuItem.TempReplaceBit
				{
					location = num,
					originalStrIndex = i
				});
			}
		}
		list.Sort();
		string[] array3 = new string[list.Count];
		for (int j = 0; j < list.Count; j++)
		{
			array3[j] = array2[list[j].originalStrIndex];
		}
		object[] args = array3;
		return string.Format(formatStr, args);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0003A5FC File Offset: 0x000387FC
	public void InitLocKeys()
	{
		if (this.locStringKey != StringNames.ExitButton)
		{
			if (this.itemType == QuickChatMenuItem.QuickChatMenuItemType.FillInBlank)
			{
				this.fillInText = DestroyableSingleton<TranslationController>.Instance.GetString(this.locStringKey, Array.Empty<object>());
				this.text = this.GeneratePreviewText(this.fillInText);
			}
			else
			{
				this.text = DestroyableSingleton<TranslationController>.Instance.GetString(this.locStringKey, Array.Empty<object>());
			}
		}
		if (this.locStringAltKey != StringNames.ExitButton)
		{
			if (this.itemType == QuickChatMenuItem.QuickChatMenuItemType.FillInBlank)
			{
				this.alternateFillInText = DestroyableSingleton<TranslationController>.Instance.GetString(this.locStringAltKey, Array.Empty<object>());
				this.alternateText = this.GeneratePreviewText(this.alternateFillInText);
				return;
			}
			this.alternateText = DestroyableSingleton<TranslationController>.Instance.GetString(this.locStringAltKey, Array.Empty<object>());
		}
	}

	// Token: 0x04000A54 RID: 2644
	public Sprite icon;

	// Token: 0x04000A55 RID: 2645
	public string text;

	// Token: 0x04000A56 RID: 2646
	public QuickChatMenuItem.QuickChatMenuItemType itemType;

	// Token: 0x04000A57 RID: 2647
	public StringNames locStringKey;

	// Token: 0x04000A58 RID: 2648
	public StringNames locStringAltKey;

	// Token: 0x04000A59 RID: 2649
	public string alternateText;

	// Token: 0x04000A5A RID: 2650
	public QuickChatSubmenu targetSubmenu;

	// Token: 0x04000A5B RID: 2651
	public string fillInText;

	// Token: 0x04000A5C RID: 2652
	public string alternateFillInText;

	// Token: 0x04000A5D RID: 2653
	public QuickChatSubmenu[] fillBlankSelectionsInOder;

	// Token: 0x04000A5E RID: 2654
	public QuickChatSubmenu[] alternateFillBlankSelectionsInOder;

	// Token: 0x04000A5F RID: 2655
	public Button.ButtonClickedEvent OnClick = new Button.ButtonClickedEvent();

	// Token: 0x04000A60 RID: 2656
	[HideInInspector]
	public bool initialized;

	// Token: 0x020003F2 RID: 1010
	public enum QuickChatMenuItemType
	{
		// Token: 0x04001AFD RID: 6909
		Text,
		// Token: 0x04001AFE RID: 6910
		GoToSubmenu,
		// Token: 0x04001AFF RID: 6911
		FillInBlank,
		// Token: 0x04001B00 RID: 6912
		CustomButton
	}

	// Token: 0x020003F3 RID: 1011
	private class TempReplaceBit : IComparable
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x00075FB5 File Offset: 0x000741B5
		public int CompareTo(object obj)
		{
			return this.location.CompareTo(((QuickChatMenuItem.TempReplaceBit)obj).location);
		}

		// Token: 0x04001B01 RID: 6913
		public int location;

		// Token: 0x04001B02 RID: 6914
		public int originalStrIndex;
	}
}
