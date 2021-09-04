using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000188 RID: 392
[RequireComponent(typeof(QuickChatSubmenu))]
public class QuickChatLocationSubmenu : MonoBehaviour
{
	// Token: 0x060008DC RID: 2268 RVA: 0x000396BD File Offset: 0x000378BD
	private void Awake()
	{
		this.Rebuild();
		this.menu = base.GetComponent<QuickChatSubmenu>();
		QuickChatSubmenu quickChatSubmenu = this.menu;
		quickChatSubmenu.OnWillDisplay = (Action)Delegate.Combine(quickChatSubmenu.OnWillDisplay, new Action(this.Rebuild));
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x000396F8 File Offset: 0x000378F8
	private void Rebuild()
	{
		QuickChatSubmenu menu = base.GetComponent<QuickChatSubmenu>();
		if (!ShipStatus.Instance)
		{
			if (LobbyBehaviour.Instance)
			{
				if (LobbyBehaviour.Instance.name == this.currentMap)
				{
					return;
				}
				this.currentMap = LobbyBehaviour.Instance.name;
				menu.menuItems.Clear();
				StringNames[] array = this.lobbyLocations;
				for (int i = 0; i < array.Length; i++)
				{
					StringNames locStringKey = array[i];
					QuickChatMenuItem menuItem = new QuickChatMenuItem();
					menuItem.locStringKey = locStringKey;
					menuItem.InitLocKeys();
					menuItem.OnClick.AddListener(delegate()
					{
						menu.parentChatMenu.QuickChat(menuItem);
					});
					menuItem.initialized = true;
					menu.menuItems.Add(menuItem);
				}
			}
			return;
		}
		if (ShipStatus.Instance.name == this.currentMap)
		{
			return;
		}
		this.currentMap = ShipStatus.Instance.name;
		menu.menuItems.Clear();
		List<string> list = (from s in (from r in ShipStatus.Instance.AllRooms
		select DestroyableSingleton<TranslationController>.Instance.GetString(r.RoomId)).Distinct<string>()
		orderby s
		select s).ToList<string>();
		int num = list.Count;
		if (list.Count > 8)
		{
			menu.hasAlternateSet = true;
			menu.alternateSetName = StringNames.QCMore;
			menu.primarySetName = StringNames.QCMore;
			num = Mathf.CeilToInt((float)list.Count / 2f);
		}
		else
		{
			menu.hasAlternateSet = false;
		}
		for (int j = 0; j < num; j++)
		{
			int index = j;
			int num2 = j + num;
			QuickChatMenuItem menuItem = new QuickChatMenuItem();
			menuItem.text = list[index];
			if (num2 < list.Count)
			{
				menuItem.alternateText = list[num2];
			}
			menuItem.OnClick.AddListener(delegate()
			{
				menu.parentChatMenu.QuickChat(menuItem);
			});
			menuItem.initialized = true;
			menu.menuItems.Add(menuItem);
		}
	}

	// Token: 0x04000A32 RID: 2610
	private QuickChatSubmenu menu;

	// Token: 0x04000A33 RID: 2611
	public StringNames[] lobbyLocations;

	// Token: 0x04000A34 RID: 2612
	private string currentMap;
}
