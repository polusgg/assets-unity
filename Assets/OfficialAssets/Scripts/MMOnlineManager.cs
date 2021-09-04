using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class MMOnlineManager : DestroyableSingleton<MMOnlineManager>
{
	// Token: 0x06000717 RID: 1815 RVA: 0x0002D208 File Offset: 0x0002B408
	public void Start()
	{
		if (this.HelpMenu)
		{
			if (SaveManager.ShowOnlineHelp)
			{
				SaveManager.ShowOnlineHelp = false;
			}
			else
			{
				this.HelpMenu.gameObject.SetActive(false);
			}
		}
		ControllerManager.Instance.NewScene(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
		this.IsControllerManagerSceneInit = true;
		if (VirtualCursor.instance)
		{
			VirtualCursor.instance.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0002D288 File Offset: 0x0002B488
	private void Update()
	{
		//if (Input.GetKeyUp(27))
		//{
		//	SceneChanger.ChangeScene("MainMenu");
		//}
	}

	// Token: 0x040007FD RID: 2045
	public GameObject HelpMenu;

	// Token: 0x040007FE RID: 2046
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040007FF RID: 2047
	public UiElement DefaultButtonSelected;

	// Token: 0x04000800 RID: 2048
	public List<UiElement> ControllerSelectable;

	// Token: 0x04000801 RID: 2049
	public bool IsControllerManagerSceneInit;
}
