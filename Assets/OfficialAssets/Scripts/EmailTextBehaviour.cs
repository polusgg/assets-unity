using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class EmailTextBehaviour : MonoBehaviour
{
	// Token: 0x06000488 RID: 1160 RVA: 0x0001D338 File Offset: 0x0001B538
	public string GetEmailValidEmail()
	{
		if (this.ShakeIfInvalid())
		{
			return "";
		}
		return this.nameSource.text;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0001D354 File Offset: 0x0001B554
	private bool IsValidEmail(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}
		if (!new Regex("^([+\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,63})+)$").Match(text).Success)
		{
			return false;
		}
		foreach (char c in text)
		{
			if (c < ' ')
			{
				return false;
			}
			if (EmailTextBehaviour.SymbolChars.Contains(c))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
	public bool ShakeIfInvalid()
	{
		if (!this.IsValidEmail(this.nameSource.text))
		{
			base.StartCoroutine(Effects.SwayX(this.nameSource.transform, 0.75f, 0.25f));
			return true;
		}
		return false;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0001D3ED File Offset: 0x0001B5ED
	public bool ShakeIfDoesntMatch(string email1, string email2)
	{
		if (email1 != email2)
		{
			base.StartCoroutine(Effects.SwayX(this.nameSource.transform, 0.75f, 0.25f));
			return true;
		}
		return false;
	}

	// Token: 0x04000550 RID: 1360
	public static readonly HashSet<char> SymbolChars = new HashSet<char>
	{
		'?',
		'!',
		',',
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

	// Token: 0x04000551 RID: 1361
	public TextBox nameSource;
}
