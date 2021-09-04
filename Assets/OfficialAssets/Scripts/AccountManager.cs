using System;
using System.Collections;
//using Epic.OnlineServices.KWS;
using InnerNet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x02000003 RID: 3
public class AccountManager : DestroyableSingleton<AccountManager>
{
	// Token: 0x06000004 RID: 4 RVA: 0x000020AA File Offset: 0x000002AA
	public void SetIsGuest()
	{
		//if (EOSManager.Instance.IsMinor())
		//{
		//	this.SetKidIsGuest();
		//	return;
		//}
		//SaveManager.IsGuest = true;
		//SaveManager.HasLoggedIn = false;
		//SaveManager.ChatModeType = QuickChatModes.QuickChatOnly;
		//this.accountTab.ShowGuestMode();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
	public void SetKidIsGuest()
	{
		SaveManager.HasLoggedIn = false;
		SaveManager.ChatModeType = QuickChatModes.QuickChatOnly;
		if (this.HasMinorsGuardianEverUpdatedAnything())
		{
			if (this.CanMinorSignIntoAccount())
			{
				this.SetApprovedMinorCanLogIn();
				return;
			}
			this.SetApprovedMinorCantLogIn();
			return;
		}
		else
		{
			if (SaveManager.IsGuest)
			{
				this.SetApprovedMinorCantLogIn();
				return;
			}
			this.SetMinorWaitingForGuardian();
			return;
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
	public void SetNotGuest()
	{
		SaveManager.IsGuest = false;
		this.accountTab.ShowLoggedInMode();
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000212F File Offset: 0x0000032F
	public void SetGuestMinor()
	{
		this.accountTab.ShowMinorNotLoggedInMode();
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000213C File Offset: 0x0000033C
	public void SetMinorWaitingForGuardian()
	{
		SaveManager.IsGuest = false;
		this.accountTab.ShowWaitingForGuardian();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000214F File Offset: 0x0000034F
	public void SetApprovedMinorCantLogIn()
	{
		SaveManager.IsGuest = false;
		SaveManager.HasLoggedIn = false;
		this.accountTab.ShowMinorNotLoggedInMode();
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002168 File Offset: 0x00000368
	public void SetApprovedMinorCanLogIn()
	{
		SaveManager.IsGuest = false;
		this.accountTab.ShowMinorNotLoggedInMode();
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000217B File Offset: 0x0000037B
	public void UpdateKidAccountDisplay()
	{
		this.accountTab.UpdateKidAccountCanChangeName();
		this.accountTab.UpdateKidAccountDisplay();
		if (this.chatModeMenuScreen != null)
		{
			this.chatModeMenuScreen.UpdateDisplay();
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021AC File Offset: 0x000003AC
	public void UpdateAccountInfoDisplays()
	{
		this.accountTab.UpdateNameDisplay();
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021B9 File Offset: 0x000003B9
	public void ShowGuardianEmailSentConfirm()
	{
		this.guardianEmailConfirmWindow.gameObject.SetActive(true);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021CC File Offset: 0x000003CC
	public IEnumerator ShowPermissionsRequestForm()
	{
		yield return this.enterGuardianEmailWindow.Show();
		yield break;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000021DB File Offset: 0x000003DB
	public IEnumerator EditGuardianEmail()
	{
		yield return this.updateGuardianEmailWindow.Show();
		yield break;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000021EA File Offset: 0x000003EA
	public IEnumerator ShowAgeGate()
	{
		yield return this.enterDateOfBirthScreen.Show();
		yield break;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000021F9 File Offset: 0x000003F9
	public bool CanMinorSignIntoAccount()
	{
		return true;// this.freeChatAllowed == null || this.customDisplayName == 0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000220E File Offset: 0x0000040E
	public bool CanMinorSetCustomDisplayName()
	{
		return true;//this.customDisplayName == null && SaveManager.HasLoggedIn;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000221F File Offset: 0x0000041F
	public bool HasMinorsGuardianEverUpdatedAnything()
	{
		return true;// this.freeChatAllowed != 2 || this.customDisplayName != 2;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002238 File Offset: 0x00000438
	public bool HasGuardianRejectedEverything()
	{
		return true;// this.freeChatAllowed == 1 && this.customDisplayName == 1;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000224E File Offset: 0x0000044E
	public void RandomizeName()
	{
		SaveManager.PlayerName = this.GetRandomName();
		this.accountTab.UpdateNameDisplay();
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002268 File Offset: 0x00000468
	public string GetRandomName()
	{
		string name;
		do
		{
			name = base.GetComponent<RandomNameGenerator>().GetName();
		}
		while (name.Length > 10 || BlockedWords.ContainsWord(name));
		return name;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002294 File Offset: 0x00000494
	public void UpdateMinorPermissions()
	{
		//if (this.freeChatAllowed != null)
		//{
		//	SaveManager.ChatModeType = QuickChatModes.QuickChatOnly;
		//}
		//if (!this.HasMinorsGuardianEverUpdatedAnything())
		//{
		//	this.SetMinorWaitingForGuardian();
		//}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000022B2 File Offset: 0x000004B2
	public override void Awake()
	{
		base.Awake();
		SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000022CB File Offset: 0x000004CB
	public override void OnDestroy()
	{
		base.OnDestroy();
		SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000022E4 File Offset: 0x000004E4
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		this.accountTab.gameObject.SetActive(scene.name == "MainMenu");
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002308 File Offset: 0x00000508
	public void ShowCreateSuccess()
	{
		//this.genericInfoDisplayBox.SetOneButton();
		//this.genericInfoDisplayBox.titleTexxt.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Success, Array.Empty<object>());
		//this.genericInfoDisplayBox.bodyText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SuccessCreate, Array.Empty<object>());
		//this.genericInfoDisplayBox.button1.OnClick.AddListener(delegate()
		//{
		//	EOSManager.Instance.SetSignInFlowFinished();
		//});
		//this.genericInfoDisplayBox.button1Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Close, Array.Empty<object>());
		//this.genericInfoDisplayBox.gameObject.SetActive(true);
		//this.genericInfoDisplayBox.button1.OnClick.AddListener(delegate()
		//{
		//	EOSManager.Instance.SetSignInFlowFinished();
		//});
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002408 File Offset: 0x00000608
	//public void ShowCreateFail(EOSManager.EOS_ERRORS error)
	//{
	//	this.genericInfoDisplayBox.SetOneButton();
	//	this.genericInfoDisplayBox.titleTexxt.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Failed, Array.Empty<object>());
	//	this.genericInfoDisplayBox.bodyText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorCreate, Array.Empty<object>()) + string.Format("\n(Error {0})", error);
	//	this.genericInfoDisplayBox.button1Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Close, Array.Empty<object>());
	//	this.genericInfoDisplayBox.gameObject.SetActive(true);
	//	this.genericInfoDisplayBox.button1.OnClick.AddListener(delegate()
	//	{
	//		EOSManager.Instance.SetSignInFlowFinished();
	//	});
	//}

	// Token: 0x0600001D RID: 29 RVA: 0x000024E8 File Offset: 0x000006E8
	public void SignInSuccess()
	{
		//this.genericInfoDisplayBox.SetOneButton();
		//this.genericInfoDisplayBox.titleTexxt.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Success, Array.Empty<object>());
		//this.genericInfoDisplayBox.bodyText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SuccessLogIn, Array.Empty<object>());
		//this.genericInfoDisplayBox.button1Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Close, Array.Empty<object>());
		//this.genericInfoDisplayBox.button1.OnClick.AddListener(delegate()
		//{
		//	EOSManager.Instance.SetSignInFlowFinished();
		//});
		//this.genericInfoDisplayBox.gameObject.SetActive(true);
		//this.genericInfoDisplayBox.button1.OnClick.AddListener(delegate()
		//{
		//	EOSManager.Instance.SetSignInFlowFinished();
		//});
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000025E8 File Offset: 0x000007E8
	//public void SignInFail(EOSManager.EOS_ERRORS error)
	//{
	//	this.genericInfoDisplayBox.SetOneButton();
	//	this.genericInfoDisplayBox.titleTexxt.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Failed, Array.Empty<object>());
	//	this.genericInfoDisplayBox.bodyText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ErrorLogIn, Array.Empty<object>()) + string.Format("\n(Error {0})", error);
	//	this.genericInfoDisplayBox.button1Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Close, Array.Empty<object>());
	//	this.genericInfoDisplayBox.gameObject.SetActive(true);
	//	this.genericInfoDisplayBox.button1.OnClick.AddListener(delegate()
	//	{
	//		EOSManager.Instance.SetSignInFlowFinished();
	//	});
	//}

	// Token: 0x04000004 RID: 4
	[SerializeField]
	private AccountTab accountTab;

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private PermissionsRequest enterGuardianEmailWindow;

	// Token: 0x04000006 RID: 6
	[SerializeField]
	private UpdateGuardianEmail updateGuardianEmailWindow;

	// Token: 0x04000007 RID: 7
	[SerializeField]
	private InfoTextBox guardianEmailConfirmWindow;

	// Token: 0x04000008 RID: 8
	[SerializeField]
	private SignIn mainSignInCreateAccountWindow;

	// Token: 0x04000009 RID: 9
	[SerializeField]
	private InfoTextBox genericInfoDisplayBox;

	// Token: 0x0400000A RID: 10
	[SerializeField]
	private AgeGateScreen enterDateOfBirthScreen;

	// Token: 0x0400000B RID: 11
	[SerializeField]
	private ChatModeCycle chatModeMenuScreen;

	// Token: 0x0400000C RID: 12
	//public KWSPermissionStatus freeChatAllowed = 1;

	//// Token: 0x0400000D RID: 13
	//public KWSPermissionStatus customDisplayName = 1;
}
