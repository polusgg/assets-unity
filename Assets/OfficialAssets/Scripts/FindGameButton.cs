using System;
using System.Collections;
using InnerNet;
using PowerTools;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000113 RID: 275
public class FindGameButton : MonoBehaviour, IConnectButton
{
	// Token: 0x060006CD RID: 1741 RVA: 0x0002B664 File Offset: 0x00029864
	public void OnClick()
	{
		if (StatsManager.Instance.AmBanned)
		{
			AmongUsClient.Instance.LastDisconnectReason = DisconnectReasons.IntentionalLeaving;
			DestroyableSingleton<DisconnectPopup>.Instance.Show();
			return;
		}
		if (!DestroyableSingleton<MatchMaker>.Instance.Connecting(this))
		{
			return;
		}
		AmongUsClient.Instance.GameMode = GameModes.OnlineGame;
		AmongUsClient.Instance.MainMenuScene = "MMOnline";
		base.StartCoroutine(this.ConnectForFindGame());
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0002B6CC File Offset: 0x000298CC
	private IEnumerator ConnectForFindGame()
	{
		AmongUsClient.Instance.SetEndpoint(DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress, DestroyableSingleton<ServerManager>.Instance.OnlineNetPort);
		AmongUsClient.Instance.OnlineScene = "OnlineGame";
		AmongUsClient.Instance.mode = MatchMakerModes.Client;
		yield return AmongUsClient.Instance.CoConnect();
		if (AmongUsClient.Instance.LastDisconnectReason != DisconnectReasons.ExitGame)
		{
			DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
		}
		else
		{
			AmongUsClient.Instance.HostId = AmongUsClient.Instance.ClientId;
			SceneManager.LoadScene("FindAGame");
		}
		yield break;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0002B6D4 File Offset: 0x000298D4
	public void StartIcon()
	{
		this.connectIcon.Play(this.connectClip, 1f);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0002B6EC File Offset: 0x000298EC
	public void StopIcon()
	{
		this.connectIcon.Stop();
		this.connectIcon.GetComponent<SpriteRenderer>().sprite = null;
	}

	// Token: 0x04000799 RID: 1945
	public SpriteAnim connectIcon;

	// Token: 0x0400079A RID: 1946
	public AnimationClip connectClip;
}
