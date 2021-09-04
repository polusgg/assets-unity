using System;
using InnerNet;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200009C RID: 156
public class GameStartManager : DestroyableSingleton<GameStartManager>, IDisconnectHandler
{
	// Token: 0x060003BB RID: 955 RVA: 0x00018984 File Offset: 0x00016B84
	public void Start()
	{
		if (DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		string text = GameCode.IntToGameName(AmongUsClient.Instance.GameId);
		if (text != null)
		{
			this.GameRoomName.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.RoomCode, Array.Empty<object>()) + "\r\n" + text;
		}
		else
		{
			this.StartButton.transform.localPosition = new Vector3(0f, -0.2f, 0f);
			this.PlayerCounter.transform.localPosition = new Vector3(0f, -0.8f, 0f);
		}
		AmongUsClient.Instance.DisconnectHandlers.AddUnique(this);
		if (!AmongUsClient.Instance.AmHost)
		{
			this.StartButton.gameObject.SetActive(false);
			this.MakePublicButton.GetComponent<ControllerHeldButtonBehaviour>().enabled = false;
			ActionMapGlyphDisplay componentInChildren = this.MakePublicButton.GetComponentInChildren<ActionMapGlyphDisplay>(true);
			if (componentInChildren)
			{
				componentInChildren.gameObject.SetActive(false);
			}
		}
		else
		{
			LobbyBehaviour.Instance = Object.Instantiate<LobbyBehaviour>(this.LobbyPrefab);
			AmongUsClient.Instance.Spawn(LobbyBehaviour.Instance, -2, SpawnFlags.None);
		}
		this.MakePublicButton.gameObject.SetActive(AmongUsClient.Instance.GameMode == GameModes.OnlineGame);
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00018ACB File Offset: 0x00016CCB
	public void MakePublic()
	{
		if (AmongUsClient.Instance.AmHost)
		{
			AmongUsClient.Instance.ChangeGamePublic(!AmongUsClient.Instance.IsGamePublic);
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00018AF0 File Offset: 0x00016CF0
	public void Update()
	{
		//if (!GameData.Instance)
		//{
		//	return;
		//}
		//this.MakePublicButton.sprite = (AmongUsClient.Instance.IsGamePublic ? DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.PublicButton) : DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.PrivateButton));
		//if ((Input.GetKey(306) || Input.GetKey(305)) && Input.GetKeyDown(99))
		//{
		//	ClipboardHelper.PutClipboardString(GameCode.IntToGameName(AmongUsClient.Instance.GameId));
		//}
		//if (GameData.Instance.PlayerCount != this.LastPlayerCount)
		//{
		//	this.LastPlayerCount = GameData.Instance.PlayerCount;
		//	string arg = "[FF0000FF]";
		//	if (this.LastPlayerCount > this.MinPlayers)
		//	{
		//		arg = "[00FF00FF]";
		//	}
		//	if (this.LastPlayerCount == this.MinPlayers)
		//	{
		//		arg = "[FFFF00FF]";
		//	}
		//	this.PlayerCounter.Text = string.Format("{0}{1}/{2}", arg, this.LastPlayerCount, PlayerControl.GameOptions.MaxPlayers);
		//	this.StartButton.color = ((this.LastPlayerCount >= this.MinPlayers) ? Palette.EnabledColor : Palette.DisabledClear);
		//	if (DestroyableSingleton<DiscordManager>.InstanceExists)
		//	{
		//		if (AmongUsClient.Instance.AmHost && AmongUsClient.Instance.GameMode == GameModes.OnlineGame)
		//		{
		//			DestroyableSingleton<DiscordManager>.Instance.SetInLobbyHost(this.LastPlayerCount, AmongUsClient.Instance.GameId);
		//		}
		//		else
		//		{
		//			DestroyableSingleton<DiscordManager>.Instance.SetInLobbyClient(this.LastPlayerCount, AmongUsClient.Instance.GameId);
		//		}
		//	}
		//}
		//if (AmongUsClient.Instance.AmHost)
		//{
		//	if (this.startState == GameStartManager.StartingStates.Countdown)
		//	{
		//		int num = Mathf.CeilToInt(this.countDownTimer);
		//		this.countDownTimer -= Time.deltaTime;
		//		int num2 = Mathf.CeilToInt(this.countDownTimer);
		//		this.GameStartText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameStarting, new object[]
		//		{
		//			num2
		//		});
		//		if (num != num2)
		//		{
		//			PlayerControl.LocalPlayer.RpcSetStartCounter(num2);
		//		}
		//		if (num2 <= 0)
		//		{
		//			this.FinallyBegin();
		//			return;
		//		}
		//	}
		//	else
		//	{
		//		this.GameStartText.Text = string.Empty;
		//	}
		//}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00018D04 File Offset: 0x00016F04
	public void ResetStartState()
	{
		this.startState = GameStartManager.StartingStates.NotStarting;
		if (this.StartButton && this.StartButton.gameObject)
		{
			this.StartButton.gameObject.SetActive(AmongUsClient.Instance.AmHost);
		}
		if (PlayerControl.LocalPlayer)
		{
			PlayerControl.LocalPlayer.RpcSetStartCounter(-1);
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00018D68 File Offset: 0x00016F68
	public void SetStartCounter(sbyte sec)
	{
		if (sec == -1)
		{
			this.GameStartText.Text = string.Empty;
			return;
		}
		this.GameStartText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameStarting, new object[]
		{
			sec
		});
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00018DA8 File Offset: 0x00016FA8
	public void BeginGame()
	{
		if (this.startState != GameStartManager.StartingStates.NotStarting)
		{
			return;
		}
		if (SaveManager.ShowMinPlayerWarning && GameData.Instance.PlayerCount == this.MinPlayers)
		{
			this.GameSizePopup.SetActive(true);
			return;
		}
		if (GameData.Instance.PlayerCount < this.MinPlayers)
		{
			base.StartCoroutine(Effects.SwayX(this.PlayerCounter.transform, 0.75f, 0.25f));
			return;
		}
		this.ReallyBegin(false);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00018E20 File Offset: 0x00017020
	public void ReallyBegin(bool neverShow)
	{
		this.startState = GameStartManager.StartingStates.Countdown;
		this.GameSizePopup.SetActive(false);
		if (neverShow)
		{
			SaveManager.ShowMinPlayerWarning = false;
		}
		this.StartButton.gameObject.SetActive(false);
		this.countDownTimer = 5.0001f;
		this.startState = GameStartManager.StartingStates.Countdown;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00018E6C File Offset: 0x0001706C
	public void FinallyBegin()
	{
		//if (this.startState != GameStartManager.StartingStates.Countdown)
		//{
		//	return;
		//}
		//this.startState = GameStartManager.StartingStates.Starting;
		//AmongUsClient.Instance.StartGame();
		//AmongUsClient.Instance.DisconnectHandlers.Remove(this);
		//Object.Destroy(base.gameObject);
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00018EA5 File Offset: 0x000170A5
	public void HandleDisconnect(PlayerControl pc, DisconnectReasons reason)
	{
		if (AmongUsClient.Instance.AmHost)
		{
			this.LastPlayerCount = -1;
			if (this.StartButton)
			{
				this.StartButton.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00018ED8 File Offset: 0x000170D8
	public void HandleDisconnect()
	{
		this.HandleDisconnect(null, DisconnectReasons.ExitGame);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00018EE2 File Offset: 0x000170E2
	public void ShowInviteMenu()
	{
	}

	// Token: 0x0400044B RID: 1099
	private const float CountdownDuration = 5.0001f;

	// Token: 0x0400044C RID: 1100
	public int MinPlayers = 4;

	// Token: 0x0400044D RID: 1101
	public TextRenderer PlayerCounter;

	// Token: 0x0400044E RID: 1102
	private int LastPlayerCount = -1;

	// Token: 0x0400044F RID: 1103
	public GameObject GameSizePopup;

	// Token: 0x04000450 RID: 1104
	public TextRenderer GameRoomName;

	// Token: 0x04000451 RID: 1105
	public LobbyBehaviour LobbyPrefab;

	// Token: 0x04000452 RID: 1106
	public TextRenderer GameStartText;

	// Token: 0x04000453 RID: 1107
	public SpriteRenderer StartButton;

	// Token: 0x04000454 RID: 1108
	public SpriteRenderer MakePublicButton;

	// Token: 0x04000455 RID: 1109
	public Sprite PublicGameImage;

	// Token: 0x04000456 RID: 1110
	public Sprite PrivateGameImage;

	// Token: 0x04000457 RID: 1111
	public GameObject InviteFriendsButton;

	// Token: 0x04000458 RID: 1112
	private GameStartManager.StartingStates startState;

	// Token: 0x04000459 RID: 1113
	private float countDownTimer;

	// Token: 0x0400045A RID: 1114
	private ImageTranslator publicButtonTranslator;

	// Token: 0x02000338 RID: 824
	private enum StartingStates
	{
		// Token: 0x04001844 RID: 6212
		NotStarting,
		// Token: 0x04001845 RID: 6213
		Countdown,
		// Token: 0x04001846 RID: 6214
		Starting
	}
}
