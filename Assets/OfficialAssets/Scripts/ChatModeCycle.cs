﻿using System;
using InnerNet;
using UnityEngine;

// Token: 0x020001C8 RID: 456
public class ChatModeCycle : MonoBehaviour
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x00043F33 File Offset: 0x00042133
	public void OnEnable()
	{
		this.UpdateDisplay();
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00043F3C File Offset: 0x0004213C
	public void UpdateDisplay()
	{
		//this.chatMode.options = new StringNames[]
		//{
		//	StringNames.FreeOrQuickChat,
		//	StringNames.QuickChatOnly
		//};
		//if (!EOSManager.Instance.IsFreechatAllowed())
		//{
		//	SaveManager.ChatModeType = QuickChatModes.QuickChatOnly;
		//	this.chatMode.Rollover.OverColor = Color.grey;
		//	this.chatMode.Rollover.OutColor = Color.grey;
		//	this.chatMode.Text.Color = Color.grey;
		//	this.chatModeText.Color = Color.grey;
		//	this.backgroundSprite.color = Color.grey;
		//}
		//else
		//{
		//	this.chatMode.Rollover.OverColor = Color.green;
		//	this.chatMode.Rollover.OutColor = Color.white;
		//	this.chatMode.Text.Color = Color.white;
		//	this.chatModeText.Color = Color.white;
		//	this.backgroundSprite.color = Color.white;
		//}
		//this.chatMode.UpdateText(SaveManager.ChatModeType - QuickChatModes.FreeChatOrQuickChat);
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00044050 File Offset: 0x00042250
	public void CycleChatMode()
	{
		//if (!EOSManager.Instance.IsFreechatAllowed())
		//{
		//	return;
		//}
		//int num = (int)(SaveManager.ChatModeType + 1);
		//num %= 3;
		//if (num == 0)
		//{
		//	num++;
		//}
		//SaveManager.ChatModeType = (QuickChatModes)num;
		//this.chatMode.UpdateText(SaveManager.ChatModeType - QuickChatModes.FreeChatOrQuickChat);
	}

	// Token: 0x04000C14 RID: 3092
	public CycleButtonBehaviour chatMode;

	// Token: 0x04000C15 RID: 3093
	public TextRenderer chatModeText;

	// Token: 0x04000C16 RID: 3094
	public SpriteRenderer backgroundSprite;
}
