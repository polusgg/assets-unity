using System;
using UnityEngine;

// Token: 0x02000185 RID: 389
[RequireComponent(typeof(QuickChatSubmenu))]
public class QuickChatCrewmateSubmenu : MonoBehaviour
{
	// Token: 0x060008D3 RID: 2259 RVA: 0x0003924F File Offset: 0x0003744F
	private void Awake()
	{
		this.menu = base.GetComponent<QuickChatSubmenu>();
		QuickChatSubmenu quickChatSubmenu = this.menu;
		quickChatSubmenu.OnWillDisplay = (Action)Delegate.Combine(quickChatSubmenu.OnWillDisplay, new Action(this.Rebuild));
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00039284 File Offset: 0x00037484
	private void Rebuild()
	{
		this.menu.menuItems.Clear();
		foreach (PlayerControl playerControl in PlayerControl.AllPlayerControls)
		{
			QuickChatMenuItem menuItem = new QuickChatMenuItem();
			menuItem.text = playerControl.Data.PlayerName;
			menuItem.OnClick.AddListener(delegate()
			{
				this.menu.parentChatMenu.QuickChat(menuItem);
			});
			menuItem.initialized = true;
			this.menu.menuItems.Add(menuItem);
		}
		StringNames[] array = this.alwaysDisplayedOptions;
		for (int i = 0; i < array.Length; i++)
		{
			StringNames locStringKey = array[i];
			QuickChatMenuItem menuItem = new QuickChatMenuItem();
			menuItem.locStringKey = locStringKey;
			menuItem.InitLocKeys();
			menuItem.OnClick.AddListener(delegate()
			{
				this.menu.parentChatMenu.QuickChat(menuItem);
			});
			menuItem.initialized = true;
			this.menu.menuItems.Add(menuItem);
		}
	}

	// Token: 0x04000A2A RID: 2602
	public StringNames[] alwaysDisplayedOptions;

	// Token: 0x04000A2B RID: 2603
	private QuickChatSubmenu menu;
}
