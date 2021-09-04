using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class ChatLanguageMenu : MonoBehaviour
{
	// Token: 0x060006AD RID: 1709 RVA: 0x0002AD2A File Offset: 0x00028F2A
	private void Awake()
	{
		this.defaultButtonSelected = null;
		this.controllerSelectable = new List<UiElement>();
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0002AD40 File Offset: 0x00028F40
	public void OnEnable()
	{
		uint keywords = (uint)this.Parent.GetTargetOptions().Keywords;
		int num = ChatLanguageSet.Instance.Languages.Count / 10;
		if (ChatLanguageSet.Instance.Languages.Count != 10)
		{
			num++;
		}
		float num2 = ((float)num / 2f - 0.5f) * -2.5f;
		this.controllerSelectable.Clear();
		int num3 = 0;
		foreach (KeyValuePair<string, uint> keyValuePair in ChatLanguageSet.Instance.Languages)
		{
			uint lang = keyValuePair.Value;
			ChatLanguageButton chatLanguageButton = this.ButtonPool.Get<ChatLanguageButton>();
			chatLanguageButton.transform.localPosition = new Vector3(num2 + (float)(num3 / 10) * 2.5f, 2f - (float)(num3 % 10) * 0.5f, 0f);
			if (keyValuePair.Key == "Other")
			{
				chatLanguageButton.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.OtherLanguage, Array.Empty<object>());
			}
			else
			{
				chatLanguageButton.Text.Text = keyValuePair.Key;
			}
			chatLanguageButton.Text.RefreshMesh();
			chatLanguageButton.Button.OnClick.RemoveAllListeners();
			chatLanguageButton.Button.OnClick.AddListener(delegate()
			{
				this.ChooseOption(lang);
			});
			chatLanguageButton.SetSelected(keyValuePair.Value == keywords);
			this.controllerSelectable.Add(chatLanguageButton.Button);
			if (keyValuePair.Value == keywords)
			{
				this.defaultButtonSelected = chatLanguageButton.Button;
			}
			num3++;
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.defaultButtonSelected, this.controllerSelectable, false);
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0002AF44 File Offset: 0x00029144
	public void OnDisable()
	{
		this.ButtonPool.ReclaimAll();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0002AF61 File Offset: 0x00029161
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0002AF6F File Offset: 0x0002916F
	public void ChooseOption(uint language)
	{
		this.Parent.SetLanguageFilter(language);
		this.Close();
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0002AF83 File Offset: 0x00029183
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400077A RID: 1914
	public CreateOptionsPicker Parent;

	// Token: 0x0400077B RID: 1915
	public ObjectPoolBehavior ButtonPool;

	// Token: 0x0400077C RID: 1916
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400077D RID: 1917
	private UiElement defaultButtonSelected;

	// Token: 0x0400077E RID: 1918
	private List<UiElement> controllerSelectable;
}
