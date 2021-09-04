using System;
using System.Collections;
using System.Collections.Generic;
using InnerNet;
using PowerTools;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class CreateGameOptions : MonoBehaviour, IConnectButton
{
	// Token: 0x060006B4 RID: 1716 RVA: 0x0002AF9C File Offset: 0x0002919C
	public void Show()
	{
		if (StatsManager.Instance.AmBanned)
		{
			AmongUsClient.Instance.LastDisconnectReason = DisconnectReasons.IntentionalLeaving;
			DestroyableSingleton<DisconnectPopup>.Instance.Show();
			return;
		}
		base.gameObject.SetActive(true);
		this.Content.SetActive(false);
		base.StartCoroutine(this.CoShow());
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0002AFF4 File Offset: 0x000291F4
	private IEnumerator CoShow()
	{
		this.Foreground.gameObject.SetActive(true);
		yield return Effects.ColorFade(this.Foreground, Color.clear, Color.black, 0.1f);
		this.Content.SetActive(true);
		this.RegionButton.gameObject.SetActive(false);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, true);
		yield return Effects.ColorFade(this.Foreground, Color.black, Color.clear, 0.1f);
		this.Foreground.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0002B003 File Offset: 0x00029203
	public void StartIcon()
	{
		if (!this.connectIcon)
		{
			return;
		}
		this.connectIcon.Play(this.connectClip, 1f);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0002B029 File Offset: 0x00029229
	public void StopIcon()
	{
		if (!this.connectIcon)
		{
			return;
		}
		this.connectIcon.Stop();
		this.connectIcon.GetComponent<SpriteRenderer>().sprite = null;
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0002B055 File Offset: 0x00029255
	public void Hide()
	{
		base.StartCoroutine(this.CoHide());
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0002B064 File Offset: 0x00029264
	private IEnumerator CoHide()
	{
		this.Foreground.gameObject.SetActive(true);
		yield return Effects.ColorFade(this.Foreground, Color.clear, Color.black, 0.1f);
		this.Content.SetActive(false);
		yield return Effects.ColorFade(this.Foreground, Color.black, Color.clear, 0.1f);
		this.Foreground.gameObject.SetActive(false);
		base.gameObject.SetActive(false);
		ControllerManager.Instance.CloseOverlayMenu(base.name);
		this.RegionButton.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0002B073 File Offset: 0x00029273
	public void Confirm()
	{
		if (!DestroyableSingleton<MatchMaker>.Instance.Connecting(this))
		{
			return;
		}
		base.StartCoroutine(this.CoStartGame());
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0002B090 File Offset: 0x00029290
	private IEnumerator CoStartGame()
	{
		SoundManager.Instance.CrossFadeSound("MainBG", null, 0.5f, 1.5f);
		this.Foreground.gameObject.SetActive(true);
		yield return Effects.ColorFade(this.Foreground, Color.clear, Color.black, 0.2f);
		AmongUsClient.Instance.GameMode = GameModes.OnlineGame;
		AmongUsClient.Instance.SetEndpoint(DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress, DestroyableSingleton<ServerManager>.Instance.OnlineNetPort);
		AmongUsClient.Instance.MainMenuScene = "MMOnline";
		AmongUsClient.Instance.OnlineScene = "OnlineGame";
		AmongUsClient.Instance.Connect(MatchMakerModes.HostAndClient);
		yield return AmongUsClient.Instance.WaitForConnectionOrFail();
		DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
		yield break;
	}

	// Token: 0x0400077F RID: 1919
	public AudioClip IntroMusic;

	// Token: 0x04000780 RID: 1920
	public GameObject Content;

	// Token: 0x04000781 RID: 1921
	public SpriteRenderer Foreground;

	// Token: 0x04000782 RID: 1922
	public SpriteAnim connectIcon;

	// Token: 0x04000783 RID: 1923
	public AnimationClip connectClip;

	// Token: 0x04000784 RID: 1924
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000785 RID: 1925
	public UiElement DefaultButtonSelected;

	// Token: 0x04000786 RID: 1926
	public List<UiElement> ControllerSelectable;

	// Token: 0x04000787 RID: 1927
	public PassiveButton RegionButton;
}
