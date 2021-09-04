using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class QuickChatTasksSubmenu : MonoBehaviour
{
	// Token: 0x06000902 RID: 2306 RVA: 0x0003AD2C File Offset: 0x00038F2C
	private void Awake()
	{
		this.Rebuild();
		this.menu = base.GetComponent<QuickChatSubmenu>();
		QuickChatSubmenu quickChatSubmenu = this.menu;
		quickChatSubmenu.OnWillDisplay = (Action)Delegate.Combine(quickChatSubmenu.OnWillDisplay, new Action(this.Rebuild));
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0003AD68 File Offset: 0x00038F68
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
			List<TaskTypes> list = new List<TaskTypes>();
			foreach (NormalPlayerTask normalPlayerTask in ShipStatus.Instance.CommonTasks)
			{
				if (!list.Contains(normalPlayerTask.TaskType))
				{
					list.Add(normalPlayerTask.TaskType);
				}
			}
			foreach (NormalPlayerTask normalPlayerTask2 in ShipStatus.Instance.NormalTasks)
			{
				if (!list.Contains(normalPlayerTask2.TaskType))
				{
					list.Add(normalPlayerTask2.TaskType);
				}
			}
			foreach (NormalPlayerTask normalPlayerTask3 in ShipStatus.Instance.LongTasks)
			{
				if (!list.Contains(normalPlayerTask3.TaskType))
				{
					list.Add(normalPlayerTask3.TaskType);
				}
			}
			if (list.Count > 8)
			{
				menu.hasAlternateSet = true;
				menu.alternateSetName = StringNames.QCMore;
				menu.primarySetName = StringNames.QCMore;
				int num = list.Count / 2 + ((list.Count % 2 == 1) ? 1 : 0);
				for (int j = 0; j < num; j++)
				{
					int index = j;
					int num2 = j + num;
					QuickChatMenuItem menuItem = new QuickChatMenuItem();
					menuItem.text = DestroyableSingleton<TranslationController>.Instance.GetString(list[index]);
					if (num2 < list.Count)
					{
						menuItem.alternateText = DestroyableSingleton<TranslationController>.Instance.GetString(list[num2]);
					}
					menuItem.OnClick.AddListener(delegate()
					{
						menu.parentChatMenu.QuickChat(menuItem);
					});
					menuItem.initialized = true;
					menu.menuItems.Add(menuItem);
				}
				return;
			}
			menu.hasAlternateSet = false;
			using (List<TaskTypes>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TaskTypes task = enumerator.Current;
					QuickChatMenuItem menuItem = new QuickChatMenuItem();
					menuItem.text = DestroyableSingleton<TranslationController>.Instance.GetString(task);
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
			StringNames[] array2 = this.lobbyTasks;
			for (int i = 0; i < array2.Length; i++)
			{
				StringNames locStringKey = array2[i];
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

	// Token: 0x04000A76 RID: 2678
	private QuickChatSubmenu menu;

	// Token: 0x04000A77 RID: 2679
	public StringNames[] lobbyTasks;

	// Token: 0x04000A78 RID: 2680
	private string currentMap;
}
