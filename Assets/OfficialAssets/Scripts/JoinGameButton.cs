using System;
using System.Collections;
using InnerNet;
using PowerTools;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class JoinGameButton : MonoBehaviour, IConnectButton
{
	// Token: 0x060006DE RID: 1758 RVA: 0x0002BA5C File Offset: 0x00029C5C
	public void OnClick()
	{
		if (string.IsNullOrWhiteSpace(this.netAddress))
		{
			return;
		}
		if (NameTextBehaviour.Instance && NameTextBehaviour.Instance.ShakeIfInvalid())
		{
			return;
		}
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
		AmongUsClient.Instance.GameMode = this.GameMode;
		if (this.GameMode == GameModes.OnlineGame)
		{
			AmongUsClient.Instance.SetEndpoint(DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress, 22023);
			AmongUsClient.Instance.MainMenuScene = "MMOnline";
			int num = GameCode.GameNameToInt(this.GameIdText.text);
			if (num == -1)
			{
				base.StartCoroutine(Effects.SwayX(this.GameIdText.transform, 0.75f, 0.25f));
				DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
				return;
			}
			AmongUsClient.Instance.GameId = num;
		}
		else
		{
			AmongUsClient.Instance.SetEndpoint(this.netAddress, 22023);
			AmongUsClient.Instance.GameId = 32;
			AmongUsClient.Instance.GameMode = GameModes.LocalGame;
			AmongUsClient.Instance.MainMenuScene = "MatchMaking";
		}
		base.StartCoroutine(this.JoinGame());
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0002BB9B File Offset: 0x00029D9B
	private IEnumerator JoinGame()
	{
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
		AmongUsClient.Instance.OnlineScene = "OnlineGame";
		AmongUsClient.Instance.Connect(MatchMakerModes.Client);
		yield return AmongUsClient.Instance.WaitForConnectionOrFail();
		if (AmongUsClient.Instance.mode == MatchMakerModes.None)
		{
			if (this.FillScreen)
			{
				SoundManager.Instance.CrossFadeSound("MainBG", this.IntroMusic, 0.5f, 1.5f);
				for (float time = 0f; time < 0.25f; time += Time.deltaTime)
				{
					this.FillScreen.color = Color.Lerp(Color.black, Color.clear, time / 0.25f);
					yield return null;
				}
				this.FillScreen.color = Color.clear;
			}
			DestroyableSingleton<MatchMaker>.Instance.NotConnecting();
		}
		yield break;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0002BBAC File Offset: 0x00029DAC
	public void SetGameName(string[] gameNameParts)
	{
		int num = 10;
		this.gameNameText.Text = string.Format("{0} ({1}/{2})", gameNameParts[0], gameNameParts[2], num);
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0002BBDD File Offset: 0x00029DDD
	public void StartIcon()
	{
		this.connectIcon.Play(this.connectClip, 1f);
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0002BBF5 File Offset: 0x00029DF5
	public void StopIcon()
	{
		this.connectIcon.Stop();
		this.connectIcon.GetComponent<SpriteRenderer>().sprite = null;
	}

	// Token: 0x040007AB RID: 1963
	public AudioClip IntroMusic;

	// Token: 0x040007AC RID: 1964
	public TextBox GameIdText;

	// Token: 0x040007AD RID: 1965
	public TextRenderer gameNameText;

	// Token: 0x040007AE RID: 1966
	public float timeRecieved;

	// Token: 0x040007AF RID: 1967
	public SpriteRenderer FillScreen;

	// Token: 0x040007B0 RID: 1968
	public SpriteAnim connectIcon;

	// Token: 0x040007B1 RID: 1969
	public AnimationClip connectClip;

	// Token: 0x040007B2 RID: 1970
	public GameModes GameMode;

	// Token: 0x040007B3 RID: 1971
	public string netAddress;
}
