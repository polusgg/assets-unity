using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class SinglePopHelp : MonoBehaviour
{
	// Token: 0x06000C53 RID: 3155 RVA: 0x0004C5FE File Offset: 0x0004A7FE
	public void OnEnable()
	{
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
		ControllerManager.Instance.CurrentUiState.CurrentSelection = this.DefaultButtonSelected;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0004C638 File Offset: 0x0004A838
	public void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x04000DE1 RID: 3553
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000DE2 RID: 3554
	public UiElement DefaultButtonSelected;

	// Token: 0x04000DE3 RID: 3555
	public List<UiElement> ControllerSelectable;
}
