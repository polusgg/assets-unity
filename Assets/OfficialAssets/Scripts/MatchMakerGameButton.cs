using System;
using System.Collections;
using InnerNet;
using PowerTools;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class MatchMakerGameButton : PoolableBehavior, IConnectButton
{
	// Token: 0x06000720 RID: 1824 RVA: 0x0002D37C File Offset: 0x0002B57C
	public void OnClick()
	{
		if (!DestroyableSingleton<MatchMaker>.Instance.Connecting(this))
		{
			return;
		}
		if (this.myListing.IP != 0U)
		{
			AmongUsClient.Instance.GameMode = GameModes.OnlineGame;
			AmongUsClient.Instance.OnlineScene = "OnlineGame";
			AmongUsClient.Instance.SetEndpoint(InnerNetClient.AddressToString(this.myListing.IP), this.myListing.Port);
			Debug.Log("Connecting to: " + InnerNetClient.AddressToString(this.myListing.IP) + " for " + GameCode.IntToGameName(this.myListing.GameId));
			AmongUsClient.Instance.GameId = this.myListing.GameId;
			AmongUsClient.Instance.Connect(MatchMakerModes.Client);
			base.StartCoroutine(this.ConnectForFindGame());
			return;
		}
		AmongUsClient.Instance.GameMode = GameModes.OnlineGame;
		AmongUsClient.Instance.OnlineScene = "OnlineGame";
		AmongUsClient.Instance.GameId = this.myListing.GameId;
		AmongUsClient.Instance.JoinGame();
		base.StartCoroutine(this.ConnectForFindGame());
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0002D48E File Offset: 0x0002B68E
	private IEnumerator ConnectForFindGame()
	{
		yield return EndGameManager.WaitWithTimeout(() => AmongUsClient.Instance.ClientId >= 0 || AmongUsClient.Instance.LastDisconnectReason > DisconnectReasons.ExitGame);
		DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
		yield break;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0002D496 File Offset: 0x0002B696
	public void StartIcon()
	{
		this.connectIcon.Play(this.connectClip, 1f);
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0002D4AE File Offset: 0x0002B6AE
	public void StopIcon()
	{
		this.connectIcon.Stop();
		this.connectIcon.GetComponent<SpriteRenderer>().sprite = null;
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0002D4CC File Offset: 0x0002B6CC
	public void SetGame(GameListing gameListing)
	{
		this.myListing = gameListing;
		this.NameText.Text = this.myListing.HostName;
		this.ImpostorCountText.Text = this.myListing.NumImpostors.ToString();
		this.PlayerCountText.Text = string.Format("{0}/{1}", this.myListing.PlayerCount, this.myListing.MaxPlayers);
		this.MapIcon.sprite = this.MapIcons[Mathf.Clamp((int)this.myListing.MapId, 0, this.MapIcons.Length - 1)];
	}

	// Token: 0x04000805 RID: 2053
	public TextRenderer NameText;

	// Token: 0x04000806 RID: 2054
	public TextRenderer PlayerCountText;

	// Token: 0x04000807 RID: 2055
	public TextRenderer ImpostorCountText;

	// Token: 0x04000808 RID: 2056
	public SpriteRenderer MapIcon;

	// Token: 0x04000809 RID: 2057
	public Sprite[] MapIcons;

	// Token: 0x0400080A RID: 2058
	public SpriteAnim connectIcon;

	// Token: 0x0400080B RID: 2059
	public AnimationClip connectClip;

	// Token: 0x0400080C RID: 2060
	public GameListing myListing;
}
