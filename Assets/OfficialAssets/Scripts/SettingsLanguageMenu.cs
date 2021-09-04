using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class SettingsLanguageMenu : MonoBehaviour
{
	// Token: 0x06000B1C RID: 2844 RVA: 0x000462E0 File Offset: 0x000444E0
	public void Awake()
	{
		TranslatedImageSet[] languages = DestroyableSingleton<TranslationController>.Instance.Languages;
		for (int i = 0; i < languages.Length; i++)
		{
			if ((long)i == (long)((ulong)SaveManager.LastLanguage))
			{
				this.selectedLangText.Text = languages[i].Name;
			}
		}
	}

	// Token: 0x04000C81 RID: 3201
	public TextRenderer selectedLangText;
}
