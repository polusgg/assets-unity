using System;
using InnerNet;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class MatchMaker : DestroyableSingleton<MatchMaker>
{
	// Token: 0x0600071C RID: 1820 RVA: 0x0002D2A8 File Offset: 0x0002B4A8
	public void Start()
	{
		if (this.GameIdText && AmongUsClient.Instance)
		{
			this.GameIdText.SetText(GameCode.IntToGameName(AmongUsClient.Instance.GameId) ?? "", "");
		}
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
	public bool Connecting(MonoBehaviour button)
	{
		if (!this.Connecter)
		{
			this.Connecter = button;
			((IConnectButton)this.Connecter).StartIcon();
			return true;
		}
		base.StartCoroutine(Effects.SwayX(this.Connecter.transform, 0.75f, 0.25f));
		return false;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0002D34D File Offset: 0x0002B54D
	public void NotConnecting()
	{
		if (this.Connecter)
		{
			((IConnectButton)this.Connecter).StopIcon();
			this.Connecter = null;
		}
	}

	// Token: 0x04000802 RID: 2050
	public TextBox NameText;

	// Token: 0x04000803 RID: 2051
	public TextBox GameIdText;

	// Token: 0x04000804 RID: 2052
	private MonoBehaviour Connecter;
}
