using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class NameTextBehaviour : MonoBehaviour
{
	// Token: 0x06000548 RID: 1352 RVA: 0x00023984 File Offset: 0x00021B84
	public void Start()
	{
		NameTextBehaviour.Instance = this;
		if (!NameTextBehaviour.IsValidName(SaveManager.PlayerName))
		{
			this.nameSource.SetText(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EnterName, Array.Empty<object>()), "");
			return;
		}
		this.nameSource.SetText(SaveManager.PlayerName, "");
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x000239DD File Offset: 0x00021BDD
	public void UpdateName()
	{
		if (this.ShakeIfInvalid())
		{
			return;
		}
		SaveManager.PlayerName = this.nameSource.text;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x000239F8 File Offset: 0x00021BF8
	public static bool IsValidName(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}
		if (text.Equals(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EnterName, Array.Empty<object>()), StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		foreach (char c in text)
		{
			if (c < ' ')
			{
				return false;
			}
			if (NameTextBehaviour.SymbolChars.Contains(c))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00023A5E File Offset: 0x00021C5E
	public bool ShakeIfInvalid()
	{
		if (!NameTextBehaviour.IsValidName(this.nameSource.text))
		{
			base.StartCoroutine(Effects.SwayX(this.nameSource.transform, 0.75f, 0.25f));
			return true;
		}
		return false;
	}

	// Token: 0x040005F8 RID: 1528
	public static readonly HashSet<char> SymbolChars = new HashSet<char>
	{
		'?',
		'!',
		',',
		'.',
		'\'',
		':',
		';',
		'(',
		')',
		'/',
		'\\',
		'%',
		'^',
		'&',
		'-',
		'=',
		'\r',
		'\n',
		'[',
		']'
	};

	// Token: 0x040005F9 RID: 1529
	public static NameTextBehaviour Instance;

	// Token: 0x040005FA RID: 1530
	public TextBox nameSource;
}
