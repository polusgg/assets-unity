using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class QuickChatFavoritesMenu : MonoBehaviour
{
	// Token: 0x060008D6 RID: 2262 RVA: 0x000393F0 File Offset: 0x000375F0
	private void OnEnable()
	{
		if (this.AllButtons != null)
		{
			foreach (PassiveButton uiElement in this.AllButtons)
			{
				ControllerManager.Instance.AddSelectableUiElement(uiElement, false);
			}
			ControllerManager.Instance.ClearDestroyedSelectableUiElements();
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00039434 File Offset: 0x00037634
	public void Start()
	{
		//Collider2D component = this.ButtonParent.GetComponent<Collider2D>();
		//bool flag = false;
		//if (this.AllButtons == null)
		//{
		//	flag = true;
		//}
		//Vector3 localPosition;
		//localPosition..ctor(0f, this.ButtonStart, -0.5f);
		//this.AllButtons = new PassiveButton[20];
		//this.AllTextBoxes = new TextBox[20];
		//for (int i = 0; i < 20; i++)
		//{
		//	PassiveButton passiveButton = Object.Instantiate<PassiveButton>(this.ButtonPrefab, this.ButtonParent.Inner);
		//	this.AllButtons[i] = passiveButton;
		//	passiveButton.ClickMask = component;
		//	passiveButton.transform.localPosition = localPosition;
		//	localPosition.y -= this.ButtonHeight;
		//	TextBox component2 = passiveButton.GetComponent<TextBox>();
		//	string input = (SaveManager.QuickChatFavorites[i] == null) ? "___" : SaveManager.QuickChatFavorites[i];
		//	component2.SetText(input, "");
		//	this.AllTextBoxes[i] = component2;
		//	int tempIndex = i;
		//	component2.OnFocusLost.AddListener(delegate()
		//	{
		//		this.UpdateQuickChatFavorite(tempIndex);
		//	});
		//	if (flag)
		//	{
		//		ControllerManager.Instance.AddSelectableUiElement(passiveButton, false);
		//	}
		//}
		//this.ButtonParent.YBounds.max = 20f * this.ButtonHeight - 2f * this.ButtonStart - 0.1f;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0003958E File Offset: 0x0003778E
	public void UpdateQuickChatFavorite(int which)
	{
		Debug.LogError("Updating quick chat favorite #" + which.ToString());
		string[] quickChatFavorites = SaveManager.QuickChatFavorites;
		quickChatFavorites[which] = this.AllTextBoxes[which].text;
		SaveManager.QuickChatFavorites = quickChatFavorites;
	}

	// Token: 0x04000A2C RID: 2604
	public PassiveButton ButtonPrefab;

	// Token: 0x04000A2D RID: 2605
	public Scroller ButtonParent;

	// Token: 0x04000A2E RID: 2606
	public float ButtonStart = 0.5f;

	// Token: 0x04000A2F RID: 2607
	public float ButtonHeight = 0.5f;

	// Token: 0x04000A30 RID: 2608
	private PassiveButton[] AllButtons;

	// Token: 0x04000A31 RID: 2609
	private TextBox[] AllTextBoxes;
}
