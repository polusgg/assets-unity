using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class LanguageSetter : MonoBehaviour
{
	// Token: 0x06000AF1 RID: 2801 RVA: 0x0004564C File Offset: 0x0004384C
	private void OnEnable()
	{
		if (this.AllButtons != null)
		{
			LanguageButton languageButton = null;
			foreach (LanguageButton languageButton2 in this.AllButtons)
			{
				if (languageButton2.Title.Color == Color.green)
				{
					languageButton = languageButton2;
				}
			}
			ControllerManager.Instance.OpenOverlayMenu(base.gameObject.name, this.backButton, languageButton.Button, this.selectableButtons, false);
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x000456BD File Offset: 0x000438BD
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.gameObject.name);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x000456D4 File Offset: 0x000438D4
	public void Start()
	{
		//TranslatedImageSet[] languages = DestroyableSingleton<TranslationController>.Instance.Languages;
		//Collider2D component = this.ButtonParent.GetComponent<Collider2D>();
		//bool flag = false;
		//LanguageButton languageButton = null;
		//if (this.AllButtons == null)
		//{
		//	flag = true;
		//}
		//Vector3 localPosition;
		//localPosition..ctor(0f, this.ButtonStart, -0.5f);
		//this.AllButtons = new LanguageButton[languages.Length];
		//for (int i = 0; i < languages.Length; i++)
		//{
		//	LanguageButton button = Object.Instantiate<LanguageButton>(this.ButtonPrefab, this.ButtonParent.Inner);
		//	this.AllButtons[i] = button;
		//	button.Language = languages[i];
		//	button.Title.Text = languages[i].Name;
		//	if ((long)i == (long)((ulong)SaveManager.LastLanguage))
		//	{
		//		languageButton = button;
		//		button.Title.Color = Color.green;
		//		this.parentLangButton.Text = languages[i].Name;
		//	}
		//	button.Button.OnClick.AddListener(delegate()
		//	{
		//		this.SetLanguage(button);
		//	});
		//	button.Button.ClickMask = component;
		//	button.transform.localPosition = localPosition;
		//	localPosition.y -= this.ButtonHeight;
		//}
		//if (flag)
		//{
		//	foreach (LanguageButton languageButton2 in this.AllButtons)
		//	{
		//		this.selectableButtons.Add(languageButton2.Button);
		//	}
		//	ControllerManager.Instance.OpenOverlayMenu(base.gameObject.name, this.backButton, (languageButton != null) ? languageButton.Button : this.AllButtons[0].Button, this.selectableButtons, false);
		//}
		//this.ButtonParent.YBounds.max = (float)languages.Length * this.ButtonHeight - 2f * this.ButtonStart - 0.1f;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x000458E8 File Offset: 0x00043AE8
	public void SetLanguage(LanguageButton selected)
	{
		for (int i = 0; i < this.AllButtons.Length; i++)
		{
			this.AllButtons[i].Title.Color = Color.white;
		}
		selected.Title.Color = Color.green;
		this.parentLangButton.Text = selected.Language.Name;
		DestroyableSingleton<TranslationController>.Instance.SetLanguage(selected.Language);
		this.Close();
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0004595B File Offset: 0x00043B5B
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00045969 File Offset: 0x00043B69
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000C50 RID: 3152
	public LanguageButton ButtonPrefab;

	// Token: 0x04000C51 RID: 3153
	public Scroller ButtonParent;

	// Token: 0x04000C52 RID: 3154
	public float ButtonStart = 0.5f;

	// Token: 0x04000C53 RID: 3155
	public float ButtonHeight = 0.5f;

	// Token: 0x04000C54 RID: 3156
	private LanguageButton[] AllButtons;

	// Token: 0x04000C55 RID: 3157
	public TextRenderer parentLangButton;

	// Token: 0x04000C56 RID: 3158
	private List<UiElement> selectableButtons = new List<UiElement>();

	// Token: 0x04000C57 RID: 3159
	public UiElement backButton;
}
