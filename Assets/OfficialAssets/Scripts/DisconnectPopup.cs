using System;
using InnerNet;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class DisconnectPopup : DestroyableSingleton<DisconnectPopup>
{
	// Token: 0x0600031A RID: 794 RVA: 0x00014617 File Offset: 0x00012817
	private void OnEnable()
	{
		if (ControllerManager.Instance)
		{
			ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton);
			return;
		}
		Debug.LogWarning("DisconnectPopup: ControllerManager.Instance not initialized yet.");
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00014646 File Offset: 0x00012846
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00014658 File Offset: 0x00012858
	public void Start()
	{
		if (DestroyableSingleton<DisconnectPopup>.Instance == this)
		{
			this.Show();
		}
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0001466D File Offset: 0x0001286D
	public void Show()
	{
		base.gameObject.SetActive(true);
		this.DoShow();
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00014684 File Offset: 0x00012884
	private void DoShow()
	{
		if (DestroyableSingleton<WaitForHostPopup>.InstanceExists)
		{
			DestroyableSingleton<WaitForHostPopup>.Instance.Hide();
		}
		if (!AmongUsClient.Instance)
		{
			base.gameObject.SetActive(false);
			return;
		}
		string text = GameCode.IntToGameName(AmongUsClient.Instance.GameId);
		DisconnectReasons lastDisconnectReason = AmongUsClient.Instance.LastDisconnectReason;
		switch (lastDisconnectReason)
		{
		case DisconnectReasons.ExitGame:
		case DisconnectReasons.Destroy:
			base.gameObject.SetActive(false);
			return;
		case DisconnectReasons.GameFull:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorFullGame, Array.Empty<object>());
			return;
		case DisconnectReasons.GameStarted:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorStartedGame, Array.Empty<object>());
			return;
		case DisconnectReasons.GameNotFound:
		case DisconnectReasons.IncorrectGame:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorNotFoundGame, Array.Empty<object>());
			return;
		case (DisconnectReasons)4:
		case DisconnectReasons.Custom:
		case (DisconnectReasons)12:
		case (DisconnectReasons)13:
		case (DisconnectReasons)14:
		case (DisconnectReasons)15:
			break;
		case DisconnectReasons.IncorrectVersion:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorIncorrectVersion, Array.Empty<object>());
			return;
		case DisconnectReasons.Banned:
			if (text != null)
			{
				this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorBanned, new object[]
				{
					text
				});
				return;
			}
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorBannedNoCode, Array.Empty<object>());
			return;
		case DisconnectReasons.Kicked:
			if (text != null)
			{
				this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorKicked, new object[]
				{
					text
				});
				return;
			}
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorKickedNoCode, Array.Empty<object>());
			return;
		case DisconnectReasons.InvalidName:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorInvalidName, new object[]
			{
				SaveManager.PlayerName
			});
			return;
		case DisconnectReasons.Hacking:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorHacking, Array.Empty<object>());
			return;
		case DisconnectReasons.NotAuthorized:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorNotAuthenticated, Array.Empty<object>());
			return;
		case DisconnectReasons.Error:
			if (AmongUsClient.Instance.GameMode == GameModes.OnlineGame)
			{
				this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorGenericOnlineDisconnect, Array.Empty<object>());
				return;
			}
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorGenericLocalDisconnect, Array.Empty<object>());
			return;
		case DisconnectReasons.ServerRequest:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorInactivity, Array.Empty<object>());
			return;
		case DisconnectReasons.ServerFull:
			this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorServerOverload, Array.Empty<object>());
			return;
		default:
			if (lastDisconnectReason == DisconnectReasons.IntentionalLeaving)
			{
				this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorIntentionalLeaving, new object[]
				{
					StatsManager.Instance.BanMinutesLeft
				});
				return;
			}
			if (lastDisconnectReason == DisconnectReasons.FocusLost)
			{
				this.TextArea.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorFocusLost, Array.Empty<object>());
				return;
			}
			break;
		}
		this.TextArea.Text = (string.IsNullOrWhiteSpace(AmongUsClient.Instance.LastCustomDisconnect) ? DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorUnknown, Array.Empty<object>()) : AmongUsClient.Instance.LastCustomDisconnect);
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000149E7 File Offset: 0x00012BE7
	public void ShowCustom(string message)
	{
		base.gameObject.SetActive(true);
		this.TextArea.Text = message;
	}

	// Token: 0x06000320 RID: 800 RVA: 0x00014A01 File Offset: 0x00012C01
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040003A2 RID: 930
	public TextRenderer TextArea;

	// Token: 0x040003A3 RID: 931
	[Header("Console Controller Navigation")]
	public UiElement BackButton;
}
