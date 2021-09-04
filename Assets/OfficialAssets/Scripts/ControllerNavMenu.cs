using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class ControllerNavMenu : MonoBehaviour
{
	// Token: 0x06000889 RID: 2185 RVA: 0x0003821C File Offset: 0x0003641C
	private void Start()
	{
		if (this.openInOnEnable)
		{
			base.StartCoroutine(this.OpenCoroutine());
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00038233 File Offset: 0x00036433
	private void OnEnable()
	{
		if (this.openInOnEnable)
		{
			base.StartCoroutine(this.OpenCoroutine());
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0003824A File Offset: 0x0003644A
	private IEnumerator OpenCoroutine()
	{
		yield return null;
		this.OpenMenu();
		yield break;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00038259 File Offset: 0x00036459
	private void OnDisable()
	{
		this.CloseMenu();
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00038264 File Offset: 0x00036464
	public void OpenMenu()
	{
		if (!this.isOpen && ControllerManager.Instance)
		{
			UiElement uiElement = this.DefaultButtonSelected;
			if ((!uiElement || !uiElement.isActiveAndEnabled) && this.trySelectAny)
			{
				foreach (UiElement uiElement2 in this.ControllerSelectable)
				{
					if (uiElement2 && uiElement2.isActiveAndEnabled)
					{
						uiElement = uiElement2;
					}
				}
			}
			ControllerManager.Instance.OpenOverlayMenu(base.gameObject.name, this.BackButton, uiElement, this.ControllerSelectable, this.gridNavigation);
			this.isOpen = true;
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0003832C File Offset: 0x0003652C
	public void CloseMenu()
	{
		if (this.isOpen)
		{
			ControllerManager.Instance.CloseOverlayMenu(base.gameObject.name);
			this.isOpen = false;
		}
	}

	// Token: 0x040009EB RID: 2539
	public bool openInOnEnable;

	// Token: 0x040009EC RID: 2540
	public bool gridNavigation;

	// Token: 0x040009ED RID: 2541
	public bool trySelectAny;

	// Token: 0x040009EE RID: 2542
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040009EF RID: 2543
	public UiElement DefaultButtonSelected;

	// Token: 0x040009F0 RID: 2544
	public List<UiElement> ControllerSelectable;

	// Token: 0x040009F1 RID: 2545
	private bool isOpen;
}
