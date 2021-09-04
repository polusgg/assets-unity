using System;
using InnerNet;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class WaitForHostPopup : DestroyableSingleton<WaitForHostPopup>
{
	// Token: 0x06000DB6 RID: 3510 RVA: 0x00053076 File Offset: 0x00051276
	public void Show()
	{
		if (AmongUsClient.Instance && AmongUsClient.Instance.ClientId > 0)
		{
			this.Content.SetActive(true);
			ControllerManager.Instance.OpenOverlayMenu(base.name, null, this.DefaultButtonSelected);
		}
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x000530B4 File Offset: 0x000512B4
	public void ExitGame()
	{
		AmongUsClient.Instance.ExitGame(DisconnectReasons.ExitGame);
		this.Content.SetActive(false);
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x000530DD File Offset: 0x000512DD
	public void Hide()
	{
		this.Content.SetActive(false);
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x040011BC RID: 4540
	public GameObject Content;

	// Token: 0x040011BD RID: 4541
	[Header("Console Controller Navigation")]
	public UiElement DefaultButtonSelected;
}
