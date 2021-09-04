using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class QuickChatSystemsSubmenu : MonoBehaviour
{
	// Token: 0x060008FF RID: 2303 RVA: 0x0003AAA5 File Offset: 0x00038CA5
	private void Awake()
	{
		this.Rebuild();
		this.menu = base.GetComponent<QuickChatSubmenu>();
		QuickChatSubmenu quickChatSubmenu = this.menu;
		quickChatSubmenu.OnWillDisplay = (Action)Delegate.Combine(quickChatSubmenu.OnWillDisplay, new Action(this.Rebuild));
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0003AAE0 File Offset: 0x00038CE0
	private void Rebuild()
	{
		QuickChatSubmenu menu = base.GetComponent<QuickChatSubmenu>();
		if (ShipStatus.Instance)
		{
			if (ShipStatus.Instance.name == this.currentMap)
			{
				return;
			}
			this.currentMap = ShipStatus.Instance.name;
			menu.menuItems.Clear();
			using (IEnumerator<string> enumerator = (from s in ShipStatus.Instance.SystemNames
			select DestroyableSingleton<TranslationController>.Instance.GetString(s, Array.Empty<object>()) into s
			orderby s
			select s).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					QuickChatMenuItem menuItem = new QuickChatMenuItem();
					menuItem.text = text;
					menuItem.OnClick.AddListener(delegate()
					{
						menu.parentChatMenu.QuickChat(menuItem);
					});
					menuItem.initialized = true;
					menu.menuItems.Add(menuItem);
				}
				return;
			}
		}
		if (LobbyBehaviour.Instance)
		{
			if (LobbyBehaviour.Instance.name == this.currentMap)
			{
				return;
			}
			this.currentMap = LobbyBehaviour.Instance.name;
			menu.menuItems.Clear();
			StringNames[] array = this.lobbySystems;
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
	}

	// Token: 0x04000A73 RID: 2675
	private QuickChatSubmenu menu;

	// Token: 0x04000A74 RID: 2676
	public StringNames[] lobbySystems;

	// Token: 0x04000A75 RID: 2677
	private string currentMap;
}
