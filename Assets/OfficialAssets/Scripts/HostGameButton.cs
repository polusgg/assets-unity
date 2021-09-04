using System;
using System.Collections;
using InnerNet;
using PowerTools;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class HostGameButton : MonoBehaviour, IConnectButton
{
	// Token: 0x060006D7 RID: 1751 RVA: 0x0002B970 File Offset: 0x00029B70
	public void Start()
	{
		if (DestroyableSingleton<MatchMaker>.InstanceExists)
		{
			DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0002B984 File Offset: 0x00029B84
	public void OnClick()
	{
		if (this.GameMode == GameModes.FreePlay)
		{
			if (!NameTextBehaviour.IsValidName(SaveManager.PlayerName))
			{
				SaveManager.PlayerName = "";
			}
		}
		else
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
		}
		base.StartCoroutine(this.CoStartGame());
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0002B9F3 File Offset: 0x00029BF3
	public void StartIcon()
	{
		if (!this.connectIcon)
		{
			return;
		}
		this.connectIcon.Play(this.connectClip, 1f);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0002BA19 File Offset: 0x00029C19
	public void StopIcon()
	{
		if (!this.connectIcon)
		{
			return;
		}
		this.connectIcon.Stop();
		this.connectIcon.GetComponent<SpriteRenderer>().sprite = null;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0002BA45 File Offset: 0x00029C45
	private IEnumerator CoStartGame()
	{
		try
		{
			SoundManager.Instance.StopAllSound();
			AmongUsClient.Instance.GameMode = this.GameMode;
			switch (this.GameMode)
			{
			case GameModes.LocalGame:
				DestroyableSingleton<InnerNetServer>.Instance.StartAsServer();
				AmongUsClient.Instance.SetEndpoint("127.0.0.1", 22023);
				AmongUsClient.Instance.MainMenuScene = "MatchMaking";
				break;
			case GameModes.OnlineGame:
				AmongUsClient.Instance.SetEndpoint(DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress, 22023);
				AmongUsClient.Instance.MainMenuScene = "MMOnline";
				break;
			case GameModes.FreePlay:
				DestroyableSingleton<InnerNetServer>.Instance.StartAsLocalServer();
				AmongUsClient.Instance.SetEndpoint("127.0.0.1", 22023);
				AmongUsClient.Instance.MainMenuScene = "MainMenu";
				break;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("HostGameButton::CoStartGame: Exception:");
			Debug.LogException(ex, this);
			DestroyableSingleton<DisconnectPopup>.Instance.ShowCustom(ex.Message);
			DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
			yield break;
		}
		yield return new WaitForSeconds(0.1f);
		if (this.FillScreen)
		{
			SoundManager.Instance.CrossFadeSound("MainBG", null, 0.5f, 1.5f);
			this.FillScreen.gameObject.SetActive(true);
			for (float time = 0f; time < 0.25f; time += Time.deltaTime)
			{
				this.FillScreen.color = Color.Lerp(Color.clear, Color.black, time / 0.25f);
				yield return null;
			}
			this.FillScreen.color = Color.black;
		}
		AmongUsClient.Instance.OnlineScene = this.targetScene;
		AmongUsClient.Instance.Connect(MatchMakerModes.HostAndClient);
		yield return AmongUsClient.Instance.WaitForConnectionOrFail();
		DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
		if (AmongUsClient.Instance.mode == MatchMakerModes.None && this.FillScreen)
		{
			SoundManager.Instance.CrossFadeSound("MainBG", this.IntroMusic, 0.5f, 1.5f);
			for (float time = 0f; time < 0.25f; time += Time.deltaTime)
			{
				this.FillScreen.color = Color.Lerp(Color.black, Color.clear, time / 0.25f);
				yield return null;
			}
			this.FillScreen.color = Color.clear;
		}
		yield break;
	}

	// Token: 0x040007A5 RID: 1957
	public AudioClip IntroMusic;

	// Token: 0x040007A6 RID: 1958
	public string targetScene;

	// Token: 0x040007A7 RID: 1959
	public SpriteRenderer FillScreen;

	// Token: 0x040007A8 RID: 1960
	public SpriteAnim connectIcon;

	// Token: 0x040007A9 RID: 1961
	public AnimationClip connectClip;

	// Token: 0x040007AA RID: 1962
	public GameModes GameMode;
}
