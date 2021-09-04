using System;
using InnerNet;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class ExitGameButton : MonoBehaviour
{
	// Token: 0x060006C2 RID: 1730 RVA: 0x0002B28C File Offset: 0x0002948C
	public void Start()
	{
		if (!DestroyableSingleton<HudManager>.InstanceExists)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0002B2A1 File Offset: 0x000294A1
	public void OnClick()
	{
		if (AmongUsClient.Instance)
		{
			AmongUsClient.Instance.ExitGame(DisconnectReasons.ExitGame);
			return;
		}
		SceneChanger.ChangeScene("MainMenu");
	}
}
