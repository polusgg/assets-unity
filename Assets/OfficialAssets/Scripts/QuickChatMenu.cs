using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class QuickChatMenu : MonoBehaviour
{
	// Token: 0x060008DF RID: 2271 RVA: 0x000399BC File Offset: 0x00037BBC
	private void Start()
	{
		if (this.topLevelMenu)
		{
			this.DisplayMenu(this.topLevelMenu, false);
		}
		if (this.playerHeadSprite)
		{
			PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.playerHeadSprite);
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x000399F5 File Offset: 0x00037BF5
	private void OnEnable()
	{
		this.targetTextBox.LoseFocus();
		this.targetTextBox.enabled = false;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00039A0E File Offset: 0x00037C0E
	private void OnDisable()
	{
		this.targetTextBox.enabled = true;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00039A1C File Offset: 0x00037C1C
	private void SetColors(QuickChatSubmenu.QuickChatColorSet colorSet)
	{
		this.radialMenuRenderer.material.SetColor(QuickChatMenu.colorIndex, colorSet.fillColor);
		this.radialMenuRenderer.material.SetColor(QuickChatMenu.edgeColorIndex, colorSet.edgeColor);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00039A54 File Offset: 0x00037C54
	public void DisplayMenu(QuickChatSubmenu menu, bool alternate = false)
	{
		this.showingAlternate = alternate;
		this.currentMenu = menu;
		if (this.currentMenu.OnWillDisplay != null)
		{
			this.currentMenu.OnWillDisplay();
		}
		RadialMenu.CachedButtonObject[] array = this.childRadialMenu.CreateButtonsForStrings(menu.GetMenuButtonStrings(alternate));
		this.childRadialMenu.prevSelectedButton = -1;
		this.alternateTooltipObject.SetActive(menu.hasAlternateSet);
		QuickChatSubmenu.QuickChatColorSet colors = menu.hasCustomColorSet ? menu.customColorSet : this.defaultColorSet;
		if (menu.hasAlternateSet)
		{
			this.alternateText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(alternate ? menu.primarySetName : menu.alternateSetName, Array.Empty<object>());
			if (alternate)
			{
				this.SetColors(menu.alternateColorSet);
			}
			else
			{
				this.SetColors(colors);
			}
		}
		else
		{
			this.SetColors(colors);
		}
		for (int i = 0; i < array.Length; i++)
		{
			array[i].button.OnClick = menu.menuItems[i].OnClick;
			if (menu.menuItems[i].icon)
			{
				array[i].AddIcon(menu.menuItems[i].icon);
			}
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00039B8C File Offset: 0x00037D8C
	public void QuickChat(QuickChatMenuItem item)
	{
		if (this.fillInBlankMode)
		{
			this.fillInBlankEntries.Add(this.showingAlternate ? item.alternateText : item.text);
			StringNames stringNames = this.showingAlternate ? item.locStringAltKey : item.locStringKey;
			if (stringNames == StringNames.ExitButton)
			{
				stringNames = StringNames.ANY;
			}
			this.fillInBlankEntryIDs.Add(stringNames);
			QuickChatSubmenu[] array = this.fillInBlankIsAlternate ? this.fillInBlankTarget.alternateFillBlankSelectionsInOder : this.fillInBlankTarget.fillBlankSelectionsInOder;
			if (this.fillInBlankEntries.Count == array.Length)
			{
				StringNames stringNames2 = this.fillInBlankIsAlternate ? this.fillInBlankTarget.locStringAltKey : this.fillInBlankTarget.locStringKey;
				if (stringNames2 != StringNames.ExitButton)
				{
					string fitbvariant = DestroyableSingleton<TranslationController>.Instance.GetFITBVariant(stringNames2, this.fillInBlankEntryIDs);
					if (fitbvariant != null)
					{
						TextBoxTMP textBoxTMP = this.targetTextBox;
						string format = fitbvariant;
						object[] args = this.fillInBlankEntries.ToArray();
						textBoxTMP.SetText(string.Format(format, args), "");
					}
					else
					{
						TextBoxTMP textBoxTMP2 = this.targetTextBox;
						string format2 = this.fillInBlankIsAlternate ? this.fillInBlankTarget.alternateFillInText : this.fillInBlankTarget.fillInText;
						object[] args = this.fillInBlankEntries.ToArray();
						textBoxTMP2.SetText(string.Format(format2, args), "");
					}
				}
				else
				{
					TextBoxTMP textBoxTMP3 = this.targetTextBox;
					string format3 = this.fillInBlankIsAlternate ? this.fillInBlankTarget.alternateFillInText : this.fillInBlankTarget.fillInText;
					object[] args = this.fillInBlankEntries.ToArray();
					textBoxTMP3.SetText(string.Format(format3, args), "");
				}
				this.fillInBlankMode = false;
				this.DisplayMenu(this.stashedMenu, this.fillInBlankIsAlternate);
				this.stashedMenu = null;
				this.Toggle();
			}
			else
			{
				this.DisplayMenu(array[this.fillInBlankEntries.Count], false);
			}
			this.UpdateFillInBlankPreview();
			return;
		}
		this.targetTextBox.AddText(this.showingAlternate ? item.alternateText : item.text);
		this.Toggle();
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00039D7F File Offset: 0x00037F7F
	public void QuickChat(string text)
	{
		this.targetTextBox.AddText(text);
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00039D90 File Offset: 0x00037F90
	public void BeginFillInBlank(QuickChatMenuItem item)
	{
		this.fillInBlankMode = true;
		this.fillInBlankEntries.Clear();
		this.fillInBlankEntryIDs.Clear();
		this.fillInBlankIsAlternate = this.showingAlternate;
		this.fillInBlankTarget = item;
		this.stashedMenu = this.currentMenu;
		this.DisplayMenu(this.showingAlternate ? item.alternateFillBlankSelectionsInOder[0] : item.fillBlankSelectionsInOder[0], false);
		this.UpdateFillInBlankPreview();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00039E00 File Offset: 0x00038000
	public void UpdateFillInBlankPreview()
	{
		this.currentBlankArrow.gameObject.SetActive(this.fillInBlankMode);
		if (this.fillInBlankMode)
		{
			this.updateFITBArrow = true;
			this.fillInBlankPreviewList.Clear();
			QuickChatSubmenu[] array = this.fillInBlankIsAlternate ? this.fillInBlankTarget.alternateFillBlankSelectionsInOder : this.fillInBlankTarget.fillBlankSelectionsInOder;
			foreach (string item in this.fillInBlankEntries)
			{
				this.fillInBlankPreviewList.Add(item);
			}
			if (this.fillInBlankPreviewList.Count < array.Length)
			{
				this.fillInBlankPreviewList.Add(QuickChatMenu.fitbCurrentBlank);
			}
			while (this.fillInBlankPreviewList.Count < array.Length)
			{
				this.fillInBlankPreviewList.Add(QuickChatMenu.fitbBlank);
			}
			string text = this.fillInBlankIsAlternate ? this.fillInBlankTarget.alternateFillInText : this.fillInBlankTarget.fillInText;
			TextBoxTMP textBoxTMP = this.targetTextBox;
			string format = text;
			object[] args = this.fillInBlankPreviewList.ToArray();
			textBoxTMP.SetText(string.Format(format, args), "");
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00039F34 File Offset: 0x00038134
	public void UpdateFITBArrow()
	{
		this.updateFITBArrow = false;
		Vector3 vector;
		Vector3 vector2;
		if (this.targetTextBox.outputText.GetWordPosition(QuickChatMenu.fitbCurrentBlank, out vector, out vector2))
		{
			vector = this.targetTextBox.outputText.transform.TransformPoint(vector);
			vector2 = this.targetTextBox.outputText.transform.TransformPoint(vector2);
			Vector3 position = vector2;
			position.x = (vector.x + vector2.x) * 0.5f;
			this.currentBlankArrow.gameObject.SetActive(true);
			this.currentBlankArrow.transform.position = position;
			return;
		}
		this.currentBlankArrow.gameObject.SetActive(false);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00039FE1 File Offset: 0x000381E1
	public void QuickChatButtonPressed()
	{
		if (ActiveInputManager.currentControlType != ActiveInputManager.InputType.Joystick || string.IsNullOrEmpty(this.targetTextBox.text))
		{
			this.Toggle();
			return;
		}
		this.chatController.SendChat();
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0003A00E File Offset: 0x0003820E
	public void ResetGlyphs()
	{
		this.sendButtonGlyph.enabled = false;
		this.quickChatGlyph.enabled = true;
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0003A028 File Offset: 0x00038228
	public void Toggle()
	{
		bool flag = !base.gameObject.activeSelf;
		this.sendMessageButton.gameObject.SetActive(!flag);
		base.gameObject.SetActive(flag);
		this.currentBlankArrow.gameObject.SetActive(false);
		if (flag)
		{
			this.targetTextBox.SetText("", "");
			if (this.currentMenu != this.topLevelMenu)
			{
				this.DisplayMenu(this.topLevelMenu, false);
				return;
			}
		}
		else if (!string.IsNullOrEmpty(this.targetTextBox.text) && !this.sendButtonGlyph.enabled)
		{
			this.sendButtonGlyph.enabled = true;
			this.quickChatGlyph.enabled = false;
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x0003A0E8 File Offset: 0x000382E8
	private void HandleBackspace()
	{
		if (this.backspaceHeldTimer == 0f)
		{
			this.targetTextBox.ClearLastWord();
		}
		else if (this.backspaceHeldTimer > 0.6f)
		{
			if (this.backspaceRepeatTimer >= 0.1f)
			{
				this.backspaceRepeatTimer -= 0.1f;
				this.targetTextBox.ClearLastWord();
			}
			this.backspaceRepeatTimer += Time.deltaTime;
		}
		this.backspaceHeldTimer += Time.deltaTime;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0003A16C File Offset: 0x0003836C
	public void BackPressed()
	{
		if (!this.fillInBlankMode)
		{
			if (this.currentMenu.parentMenu)
			{
				this.DisplayMenu(this.currentMenu.parentMenu, false);
				return;
			}
			if (this.currentMenu == this.topLevelMenu)
			{
				this.Toggle();
				return;
			}
		}
		else
		{
			if (this.fillInBlankEntries.Count == 0)
			{
				this.targetTextBox.SetText("", "");
				this.fillInBlankMode = false;
				this.DisplayMenu(this.stashedMenu, this.fillInBlankIsAlternate);
			}
			else
			{
				QuickChatSubmenu menu = this.fillInBlankIsAlternate ? this.fillInBlankTarget.alternateFillBlankSelectionsInOder[this.fillInBlankEntries.Count - 1] : this.fillInBlankTarget.fillBlankSelectionsInOder[this.fillInBlankEntries.Count - 1];
				this.fillInBlankEntries.RemoveAt(this.fillInBlankEntries.Count - 1);
				this.fillInBlankEntryIDs.RemoveAt(this.fillInBlankEntryIDs.Count - 1);
				this.DisplayMenu(menu, false);
			}
			this.UpdateFillInBlankPreview();
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x0003A27B File Offset: 0x0003847B
	public void ToggleAlternateMenu()
	{
		if (this.currentMenu != null && this.currentMenu.hasAlternateSet)
		{
			this.DisplayMenu(this.currentMenu, !this.showingAlternate);
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0003A2B0 File Offset: 0x000384B0
	private void Update()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//if (this.updateFITBArrow)
		//{
		//	this.UpdateFITBArrow();
		//}
		//if (player.GetButtonDown(32) || Input.GetMouseButtonDown(2))
		//{
		//	this.ToggleAlternateMenu();
		//}
		//if (this.currentMenu.allowBackspace && player.GetButton(29))
		//{
		//	this.HandleBackspace();
		//}
		//else
		//{
		//	this.backspaceHeldTimer = 0f;
		//	this.backspaceRepeatTimer = 0.1f;
		//}
		//if (player.GetButtonDown(12) || Input.GetMouseButtonDown(1))
		//{
		//	this.BackPressed();
		//}
	}

	// Token: 0x04000A35 RID: 2613
	public ChatController chatController;

	// Token: 0x04000A36 RID: 2614
	public SpriteRenderer playerHeadSprite;

	// Token: 0x04000A37 RID: 2615
	public TextBoxTMP targetTextBox;

	// Token: 0x04000A38 RID: 2616
	public ButtonBehavior sendMessageButton;

	// Token: 0x04000A39 RID: 2617
	public RadialMenu childRadialMenu;

	// Token: 0x04000A3A RID: 2618
	public Transform currentBlankArrow;

	// Token: 0x04000A3B RID: 2619
	public SpriteRenderer sendButtonGlyph;

	// Token: 0x04000A3C RID: 2620
	public SpriteRenderer quickChatGlyph;

	// Token: 0x04000A3D RID: 2621
	public QuickChatSubmenu topLevelMenu;

	// Token: 0x04000A3E RID: 2622
	public QuickChatSubmenu currentMenu;

	// Token: 0x04000A3F RID: 2623
	private QuickChatSubmenu stashedMenu;

	// Token: 0x04000A40 RID: 2624
	private bool showingAlternate;

	// Token: 0x04000A41 RID: 2625
	public GameObject alternateTooltipObject;

	// Token: 0x04000A42 RID: 2626
	public TextRenderer alternateText;

	// Token: 0x04000A43 RID: 2627
	public MeshRenderer radialMenuRenderer;

	// Token: 0x04000A44 RID: 2628
	public QuickChatSubmenu.QuickChatColorSet defaultColorSet;

	// Token: 0x04000A45 RID: 2629
	private QuickChatMenuItem fillInBlankTarget;

	// Token: 0x04000A46 RID: 2630
	private bool fillInBlankIsAlternate;

	// Token: 0x04000A47 RID: 2631
	private bool fillInBlankMode;

	// Token: 0x04000A48 RID: 2632
	private bool updateFITBArrow;

	// Token: 0x04000A49 RID: 2633
	public List<string> fillInBlankEntries = new List<string>();

	// Token: 0x04000A4A RID: 2634
	public List<StringNames> fillInBlankEntryIDs = new List<StringNames>();

	// Token: 0x04000A4B RID: 2635
	private List<string> fillInBlankPreviewList = new List<string>();

	// Token: 0x04000A4C RID: 2636
	private static string fitbCurrentBlank = "(-----)";

	// Token: 0x04000A4D RID: 2637
	private static string fitbBlank = "-------";

	// Token: 0x04000A4E RID: 2638
	private static int colorIndex = Shader.PropertyToID("_Color");

	// Token: 0x04000A4F RID: 2639
	private static int edgeColorIndex = Shader.PropertyToID("_EdgeColor");

	// Token: 0x04000A50 RID: 2640
	private float backspaceHeldTimer;

	// Token: 0x04000A51 RID: 2641
	private float backspaceRepeatTimer;

	// Token: 0x04000A52 RID: 2642
	private const float backspaceHoldUntilRepeatDelay = 0.6f;

	// Token: 0x04000A53 RID: 2643
	private const float backspaceRepeatDelay = 0.1f;
}
