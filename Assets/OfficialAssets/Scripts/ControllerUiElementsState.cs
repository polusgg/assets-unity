using System;
using System.Collections.Generic;

// Token: 0x0200016F RID: 367
public class ControllerUiElementsState
{
	// Token: 0x0600086B RID: 2155 RVA: 0x00036C98 File Offset: 0x00034E98
	public void Reset()
	{
		this.CurrentSelection = null;
		this.SelectableUiElements = new List<UiElement>();
		this.BackButton = null;
		this.EnforceGridNavigation = false;
		this.MenuName = string.Empty;
		this.IsScene = false;
	}

	// Token: 0x040009D0 RID: 2512
	public UiElement CurrentSelection;

	// Token: 0x040009D1 RID: 2513
	public List<UiElement> SelectableUiElements = new List<UiElement>();

	// Token: 0x040009D2 RID: 2514
	public UiElement BackButton;

	// Token: 0x040009D3 RID: 2515
	public bool EnforceGridNavigation;

	// Token: 0x040009D4 RID: 2516
	public string MenuName = string.Empty;

	// Token: 0x040009D5 RID: 2517
	public bool IsScene;
}
