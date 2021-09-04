using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class QuickChatSubmenu : MonoBehaviour
{
	// Token: 0x060008FC RID: 2300 RVA: 0x0003A860 File Offset: 0x00038A60
	public string[] GetMenuButtonStrings(bool doAlternate = false)
	{
		if (!doAlternate)
		{
			this.menuButtonStrings.Clear();
			for (int i = 0; i < this.menuItems.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.menuItems[i].text))
				{
					this.menuButtonStrings.Add(this.menuItems[i].text);
				}
			}
		}
		else
		{
			this.altMenuButtonStrings.Clear();
			for (int j = 0; j < this.menuItems.Count; j++)
			{
				if (!string.IsNullOrEmpty(this.menuItems[j].alternateText))
				{
					this.altMenuButtonStrings.Add(this.menuItems[j].alternateText);
				}
			}
		}
		return (doAlternate ? this.altMenuButtonStrings : this.menuButtonStrings).ToArray();
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0003A934 File Offset: 0x00038B34
	public void Awake()
	{
		this.parentChatMenu = base.GetComponentInParent<QuickChatMenu>();
		using (List<QuickChatMenuItem>.Enumerator enumerator = this.menuItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				QuickChatMenuItem item = enumerator.Current;
				if (!item.initialized)
				{
					item.initialized = true;
					item.InitLocKeys();
					if (item.targetSubmenu)
					{
						item.targetSubmenu.parentMenu = this;
						item.OnClick.AddListener(delegate()
						{
							this.parentChatMenu.DisplayMenu(item.targetSubmenu, false);
						});
					}
					else if (item.itemType == QuickChatMenuItem.QuickChatMenuItemType.Text)
					{
						item.OnClick.AddListener(delegate()
						{
							this.parentChatMenu.QuickChat(item);
						});
					}
					else
					{
						item.OnClick.AddListener(delegate()
						{
							this.parentChatMenu.BeginFillInBlank(item);
						});
					}
				}
			}
		}
	}

	// Token: 0x04000A66 RID: 2662
	[HideInInspector]
	public QuickChatSubmenu parentMenu;

	// Token: 0x04000A67 RID: 2663
	public QuickChatMenu parentChatMenu;

	// Token: 0x04000A68 RID: 2664
	public bool allowBackspace = true;

	// Token: 0x04000A69 RID: 2665
	public bool hasAlternateSet;

	// Token: 0x04000A6A RID: 2666
	public StringNames primarySetName = StringNames.QCMore;

	// Token: 0x04000A6B RID: 2667
	public StringNames alternateSetName = StringNames.QCMore;

	// Token: 0x04000A6C RID: 2668
	public QuickChatSubmenu.QuickChatColorSet alternateColorSet;

	// Token: 0x04000A6D RID: 2669
	public bool hasCustomColorSet;

	// Token: 0x04000A6E RID: 2670
	public QuickChatSubmenu.QuickChatColorSet customColorSet;

	// Token: 0x04000A6F RID: 2671
	public List<QuickChatMenuItem> menuItems = new List<QuickChatMenuItem>();

	// Token: 0x04000A70 RID: 2672
	public Action OnWillDisplay;

	// Token: 0x04000A71 RID: 2673
	private List<string> menuButtonStrings = new List<string>();

	// Token: 0x04000A72 RID: 2674
	private List<string> altMenuButtonStrings = new List<string>();

	// Token: 0x020003F4 RID: 1012
	[Serializable]
	public class QuickChatColorSet
	{
		// Token: 0x04001B03 RID: 6915
		public Color fillColor;

		// Token: 0x04001B04 RID: 6916
		public Color edgeColor;
	}
}
