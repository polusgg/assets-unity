using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200011B RID: 283
public class CustomPlayerMenu : MonoBehaviour
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x0002BC2B File Offset: 0x00029E2B
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0002BC40 File Offset: 0x00029E40
	public void Start()
	{
		if (CustomPlayerMenu.Instance && CustomPlayerMenu.Instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			CustomPlayerMenu.Instance = this;
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, true);
		if (this.Tabs.Length != 0 && this.Tabs[0].Tab != null)
		{
			PlayerTab component = this.Tabs[0].Tab.GetComponent<PlayerTab>();
			if (component != null)
			{
				foreach (ColorChip colorChip in component.ColorChips)
				{
					ControllerManager.Instance.AddSelectableUiElement(colorChip.Button, false);
				}
				ColorChip defaultSelectable = component.GetDefaultSelectable();
				if (defaultSelectable)
				{
					ControllerManager.Instance.SetCurrentSelected(defaultSelectable.Button);
					return;
				}
				if (component.ColorChips.Count > 0)
				{
					ControllerManager.Instance.SetCurrentSelected(component.ColorChips[0].Button);
				}
			}
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0002BD78 File Offset: 0x00029F78
	public void OpenTab(GameObject tab)
	{
		for (int i = 0; i < this.Tabs.Length; i++)
		{
			TabButton tabButton = this.Tabs[i];
			if (tabButton.Tab == tab)
			{
				this.selectedTab = i;
				tabButton.Tab.SetActive(true);
				tabButton.Button.sprite = this.SelectedColor;
				List<ColorChip> list = new List<ColorChip>();
				ColorChip colorChip = null;
				PlayerTab component = tabButton.Tab.GetComponent<PlayerTab>();
				if (component != null)
				{
					list = component.ColorChips;
					colorChip = component.GetDefaultSelectable();
				}
				HatsTab component2 = tabButton.Tab.GetComponent<HatsTab>();
				if (component2 != null)
				{
					list = component2.ColorChips;
					colorChip = component2.GetDefaultSelectable();
				}
				PetsTab component3 = tabButton.Tab.GetComponent<PetsTab>();
				if (component3 != null)
				{
					list = component3.ColorChips;
					colorChip = component3.GetDefaultSelectable();
				}
				SkinsTab component4 = tabButton.Tab.GetComponent<SkinsTab>();
				if (component4 != null)
				{
					list = component4.ColorChips;
					colorChip = component4.GetDefaultSelectable();
				}
				foreach (ColorChip colorChip2 in list)
				{
					ControllerManager.Instance.AddSelectableUiElement(colorChip2.Button, false);
				}
				if (colorChip)
				{
					ControllerManager.Instance.SetCurrentSelected(colorChip.Button);
				}
				else if (list.Count > 0)
				{
					ControllerManager.Instance.SetCurrentSelected(list[0].Button);
				}
				else
				{
					ControllerManager.Instance.PickTopSelectable();
				}
			}
			else
			{
				tabButton.Tab.SetActive(false);
				tabButton.Button.sprite = this.NormalColor;
			}
		}
		ControllerManager.Instance.ClearDestroyedSelectableUiElements();
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0002BF3C File Offset: 0x0002A13C
	public void Close(bool canMove)
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0002BF4C File Offset: 0x0002A14C
	private void Update()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//if (this.selectedTab > 0 && player.GetButtonDown(35))
		//{
		//	this.selectedTab--;
		//	this.OpenTab(this.Tabs[this.selectedTab].Tab);
		//}
		//if (this.selectedTab < this.Tabs.Length - 1 && player.GetButtonDown(34))
		//{
		//	this.selectedTab++;
		//	this.OpenTab(this.Tabs[this.selectedTab].Tab);
		//}
	}

	// Token: 0x040007BA RID: 1978
	public static CustomPlayerMenu Instance;

	// Token: 0x040007BB RID: 1979
	public TabButton[] Tabs;

	// Token: 0x040007BC RID: 1980
	private int selectedTab;

	// Token: 0x040007BD RID: 1981
	public Sprite NormalColor;

	// Token: 0x040007BE RID: 1982
	public Sprite SelectedColor;

	// Token: 0x040007BF RID: 1983
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040007C0 RID: 1984
	public UiElement DefaultButtonSelected;

	// Token: 0x040007C1 RID: 1985
	public List<UiElement> ControllerSelectable;
}
