using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class FreeplayPopover : MonoBehaviour
{
	// Token: 0x0600039B RID: 923 RVA: 0x00017F30 File Offset: 0x00016130
	public void Show()
	{
		int num = 0;
		for (int i = 0; i < this.MapButtons.Length; i++)
		{
			num++;
		}
		int num2 = 0;
		for (int j = 0; j < this.MapButtons.Length; j++)
		{
			SpriteRenderer spriteRenderer = this.MapButtons[j];
			if (spriteRenderer.gameObject.activeSelf)
			{
				Vector3 localPosition = spriteRenderer.transform.localPosition;
				localPosition.y = -0.65f * ((float)num2 - (float)(num - 1) / 2f) + 0.15f;
				spriteRenderer.transform.localPosition = localPosition;
				num2++;
			}
		}
		if (num == 1)
		{
			this.HostGame.OnClick();
			return;
		}
		this.Content.SetActive(true);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00018002 File Offset: 0x00016202
	public void Close()
	{
		this.Content.SetActive(false);
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00018020 File Offset: 0x00016220
	public void PlayMap(int i)
	{
		AmongUsClient.Instance.TutorialMapId = i;
		this.HostGame.OnClick();
	}

	// Token: 0x0400043F RID: 1087
	public GameObject Content;

	// Token: 0x04000440 RID: 1088
	public SpriteRenderer[] MapButtons;

	// Token: 0x04000441 RID: 1089
	public HostGameButton HostGame;

	// Token: 0x04000442 RID: 1090
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000443 RID: 1091
	public UiElement DefaultButtonSelected;

	// Token: 0x04000444 RID: 1092
	public List<UiElement> ControllerSelectable;
}
