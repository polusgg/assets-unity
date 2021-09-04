using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
[RequireComponent(typeof(QuickChatSubmenu))]
public class QuickChatFavoritesSubmenu : MonoBehaviour
{
	// Token: 0x060008DA RID: 2266 RVA: 0x000395E0 File Offset: 0x000377E0
	private void Awake()
	{
		QuickChatSubmenu menu = base.GetComponent<QuickChatSubmenu>();
		menu.menuItems.Clear();
		int num = 10;
		string[] quickChatFavorites = SaveManager.QuickChatFavorites;
		for (int i = 0; i < num; i++)
		{
			int num2 = i;
			int num3 = num + i;
			QuickChatMenuItem menuItem = new QuickChatMenuItem();
			menuItem.text = quickChatFavorites[num2];
			menuItem.alternateText = quickChatFavorites[num3];
			menuItem.OnClick.AddListener(delegate()
			{
				menu.parentChatMenu.QuickChat(menuItem);
			});
			menuItem.initialized = true;
			menu.menuItems.Add(menuItem);
		}
	}
}
