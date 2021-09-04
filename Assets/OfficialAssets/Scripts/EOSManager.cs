//using System;
//using System.Collections;
////using Epic.OnlineServices;
////using Epic.OnlineServices.Connect;
////using Epic.OnlineServices.KWS;
////using Epic.OnlineServices.Logging;
////using Epic.OnlineServices.Platform;
//using InnerNet;
//using UnityEngine;

//// Token: 0x02000007 RID: 7
//[DisallowMultipleComponent]
//public class EOSManager : MonoBehaviour
//{
//	// Token: 0x17000002 RID: 2
//	// (get) Token: 0x0600003E RID: 62 RVA: 0x00002CBF File Offset: 0x00000EBF
//	public string ProductName
//	{
//		get
//		{
//			return this.productName;
//		}
//	}

//	// Token: 0x17000003 RID: 3
//	// (get) Token: 0x0600003F RID: 63 RVA: 0x00002CC7 File Offset: 0x00000EC7
//	public string ProductVersion
//	{
//		get
//		{
//			return this.productVersion;
//		}
//	}

//	// Token: 0x17000004 RID: 4
//	// (get) Token: 0x06000040 RID: 64 RVA: 0x00002CCF File Offset: 0x00000ECF
//	public string ProductId
//	{
//		get
//		{
//			return this.productId;
//		}
//	}

//	// Token: 0x17000005 RID: 5
//	// (get) Token: 0x06000041 RID: 65 RVA: 0x00002CD7 File Offset: 0x00000ED7
//	public string SandboxId
//	{
//		get
//		{
//			return this.sandboxId;
//		}
//	}

//	// Token: 0x17000006 RID: 6
//	// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CDF File Offset: 0x00000EDF
//	public string DeploymentId
//	{
//		get
//		{
//			return this.deploymentId;
//		}
//	}

//	// Token: 0x17000007 RID: 7
//	// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CE7 File Offset: 0x00000EE7
//	public string ClientId
//	{
//		get
//		{
//			return this.clientId;
//		}
//	}

//	// Token: 0x17000008 RID: 8
//	// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CEF File Offset: 0x00000EEF
//	public string ClientSecret
//	{
//		get
//		{
//			return this.clientSecret;
//		}
//	}

//	// Token: 0x17000009 RID: 9
//	// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CF7 File Offset: 0x00000EF7
//	public static EOSManager Instance
//	{
//		get
//		{
//			if (EOSManager._instance == null)
//			{
//				return new GameObject("EOSManager").AddComponent<EOSManager>();
//			}
//			return EOSManager._instance;
//		}
//	}

//	// Token: 0x06000046 RID: 70 RVA: 0x00002D1B File Offset: 0x00000F1B
//	private void Awake()
//	{
//		if (EOSManager._instance != null)
//		{
//            UnityEngine.Object.Destroy(base.gameObject);
//			return;
//		}
//		EOSManager._instance = this;
//        UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
//		this.InitializePlatformInterface();
//	}

//	// Token: 0x06000047 RID: 71 RVA: 0x00002D50 File Offset: 0x00000F50
//	private void InitializePlatformInterface()
//	{
//		//Result result = PlatformInterface.Initialize(new InitializeOptions
//		//{
//		//	ProductName = this.productName,
//		//	ProductVersion = this.productVersion
//		//});
//		//bool flag = Application.isEditor && result == 15;
//		//if (result != null && !flag)
//		//{
//		//	throw new Exception("Failed to initialize platform: " + result.ToString());
//		//}
//		//LoggingInterface.SetLogLevel(int.MaxValue, 300);
//		//LoggingInterface.SetCallback(delegate(LogMessage logMessage)
//		//{
//		//	Debug.Log(logMessage.Message);
//		//});
//		//Options options = new Options
//		//{
//		//	ProductId = this.productId,
//		//	SandboxId = this.sandboxId,
//		//	DeploymentId = this.deploymentId,
//		//	Flags = 1L,
//		//	ClientCredentials = new ClientCredentials
//		//	{
//		//		ClientId = this.clientId,
//		//		ClientSecret = this.clientSecret
//		//	}
//		//};
//		//this.platformInterface = PlatformInterface.Create(options);
//		//if (this.platformInterface == null)
//		//{
//		//	this.ContinueInOfflineMode();
//		//	throw new Exception("Failed to create platform interface");
//		//}
//		//this.platformInitialized = true;
//	}

//	// Token: 0x06000048 RID: 72 RVA: 0x00002E70 File Offset: 0x00001070
//	[ContextMenu("Delete Device ID")]
//	public void DeleteDeviceID()
//	{
//		SaveManager.BirthDateYear = 2021;
//		SaveManager.GuardianEmail = "";
//		SaveManager.IsGuest = false;
//		SaveManager.HasLoggedIn = false;
//		SaveManager.SaveLocalDoB(2021, 1, 1);
//		//DeleteDeviceIdOptions deleteDeviceIdOptions = new DeleteDeviceIdOptions();
//		//if (this.platformInterface != null)
//		//{
//		//	this.platformInterface.GetConnectInterface().DeleteDeviceId(deleteDeviceIdOptions, null, delegate(DeleteDeviceIdCallbackInfo data)
//		//	{
//		//		Debug.Log("Device ID Deleted");
//		//	});
//		//}
//	}

//	// Token: 0x06000049 RID: 73 RVA: 0x00002EEE File Offset: 0x000010EE
//	public void LoginForKWS(bool allowOffline = true)
//	{
//		this.LogInWithDeviceID();
//	}

//	// Token: 0x0600004A RID: 74 RVA: 0x00002EF8 File Offset: 0x000010F8
//	public void LogInWithDeviceID()
//	{
//		//CreateDeviceIdOptions createDeviceIdOptions = new CreateDeviceIdOptions
//		//{
//		//	DeviceModel = SystemInfo.deviceUniqueIdentifier
//		//};
//		//this.platformInterface.GetConnectInterface().CreateDeviceId(createDeviceIdOptions, null, new OnCreateDeviceIdCallback(this.CreateDeviceIdCallback));
//	}

//	// Token: 0x0600004B RID: 75 RVA: 0x00002F34 File Offset: 0x00001134
//	private void ContinueInOfflineMode()
//	{
//		//Debug.LogError("Continuing in offline mode");
//		//this.waitingText.gameObject.SetActive(false);
//		//this.userId = new ProductUserId();
//		//DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//		//this.loginFlowFinished = true;
//	}

//	// Token: 0x0600004C RID: 76 RVA: 0x00002F70 File Offset: 0x00001170
//	private void CreateDeviceIdCallback(CreateDeviceIdCallbackInfo createDeviceIdCallbackInfo)
//	{
//		if (createDeviceIdCallbackInfo.ResultCode == null)
//		{
//			Debug.Log("Created EOS Connect DeviceId");
//		}
//		else
//		{
//			Debug.Log(createDeviceIdCallbackInfo.ResultCode.ToString());
//		}
//		if (string.IsNullOrEmpty(SaveManager.PlayerName))
//		{
//			SaveManager.PlayerName = DestroyableSingleton<AccountManager>.Instance.GetRandomName();
//		}
//		LoginOptions loginOptions = new LoginOptions
//		{
//			Credentials = new Credentials
//			{
//				Token = null,
//				Type = 10
//			},
//			UserLoginInfo = new UserLoginInfo
//			{
//				DisplayName = SaveManager.PlayerName
//			}
//		};
//		this.platformInterface.GetConnectInterface().Login(loginOptions, null, new OnLoginCallback(this.EOSConnectDeviceIDLoginCallback));
//	}

//	// Token: 0x0600004D RID: 77 RVA: 0x0000301C File Offset: 0x0000121C
//	private void EOSConnectLoginCallback_CreateAccount(LoginCallbackInfo loginCallbackInfo)
//	{
//		if (loginCallbackInfo.ResultCode == null)
//		{
//			this.userId = loginCallbackInfo.LocalUserId;
//			this.StartAgeGateQuery();
//			return;
//		}
//		if (loginCallbackInfo.ResultCode == 3)
//		{
//			CreateUserOptions createUserOptions = new CreateUserOptions
//			{
//				ContinuanceToken = loginCallbackInfo.ContinuanceToken
//			};
//			this.platformInterface.GetConnectInterface().CreateUser(createUserOptions, null, new OnCreateUserCallback(this.EOSConnectCreateCallback));
//			return;
//		}
//		Debug.LogError(string.Format("EOS Failed to login: {0}", loginCallbackInfo.ResultCode));
//		this.ContinueInOfflineMode();
//	}

//	// Token: 0x0600004E RID: 78 RVA: 0x0000309E File Offset: 0x0000129E
//	private void EOSConnectCreateCallback(CreateUserCallbackInfo createUserCallbackInfo)
//	{
//		if (createUserCallbackInfo.ResultCode == null)
//		{
//			this.userId = createUserCallbackInfo.LocalUserId;
//			this.StartAgeGateQuery();
//			return;
//		}
//		this.ContinueInOfflineMode();
//	}

//	// Token: 0x0600004F RID: 79 RVA: 0x000030C4 File Offset: 0x000012C4
//	private void EOSConnectDeviceIDLoginCallback(LoginCallbackInfo loginCallbackInfo)
//	{
//		if (loginCallbackInfo.ResultCode == null)
//		{
//			this.userId = loginCallbackInfo.LocalUserId;
//			this.StartAgeGateQuery();
//			return;
//		}
//		if (loginCallbackInfo.ContinuanceToken != null)
//		{
//			DeleteDeviceIdOptions deleteDeviceIdOptions = new DeleteDeviceIdOptions();
//			if (this.platformInterface != null)
//			{
//				this.platformInterface.GetConnectInterface().DeleteDeviceId(deleteDeviceIdOptions, null, delegate(DeleteDeviceIdCallbackInfo data)
//				{
//					CreateDeviceIdOptions createDeviceIdOptions = new CreateDeviceIdOptions
//					{
//						DeviceModel = SystemInfo.deviceUniqueIdentifier
//					};
//					this.platformInterface.GetConnectInterface().CreateDeviceId(createDeviceIdOptions, null, new OnCreateDeviceIdCallback(this.CreateDeviceIdCallback));
//				});
//				return;
//			}
//		}
//		else
//		{
//			Debug.LogError(string.Format("EOS login callback - FAILURE: {0}", loginCallbackInfo.ResultCode));
//			this.ContinueInOfflineMode();
//		}
//	}

//	// Token: 0x06000050 RID: 80 RVA: 0x00003150 File Offset: 0x00001350
//	private void StartAgeGateQuery()
//	{
//		QueryAgeGateOptions queryAgeGateOptions = new QueryAgeGateOptions();
//		this.platformInterface.GetKWSInterface().QueryAgeGate(queryAgeGateOptions, null, new OnQueryAgeGateCallback(this.KWSQueryAgeGateCallback));
//	}

//	// Token: 0x06000051 RID: 81 RVA: 0x00003184 File Offset: 0x00001384
//	private void KWSQueryAgeGateCallback(QueryAgeGateCallbackInfo ageGateCallbackInfo)
//	{
//		Debug.Log("country " + ageGateCallbackInfo.CountryCode);
//		Debug.Log("consent " + ageGateCallbackInfo.AgeOfConsent.ToString());
//		this.ageOfConsent = (int)ageGateCallbackInfo.AgeOfConsent;
//		QueryPermissionsOptions queryPermissionsOptions = new QueryPermissionsOptions
//		{
//			LocalUserId = this.userId
//		};
//		this.platformInterface.GetKWSInterface().QueryPermissions(queryPermissionsOptions, null, new OnQueryPermissionsCallback(this.KWSQueryPermissionsCallback));
//	}

//	// Token: 0x06000052 RID: 82 RVA: 0x00003200 File Offset: 0x00001400
//	private void KWSQueryPermissionsCallback(QueryPermissionsCallbackInfo permissionsCallbackInfo)
//	{
//		bool showAgePrompt = false;
//		if (string.IsNullOrEmpty(permissionsCallbackInfo.DateOfBirth))
//		{
//			if (!SaveManager.GetLocalDoB())
//			{
//				showAgePrompt = true;
//			}
//			if (SaveManager.BirthDateYear >= DateTime.Now.Year)
//			{
//				showAgePrompt = true;
//			}
//		}
//		else
//		{
//			string[] array = permissionsCallbackInfo.DateOfBirth.Split(new char[]
//			{
//				'-'
//			});
//			if (array.Length != 3)
//			{
//				showAgePrompt = true;
//			}
//			int birthDateYear;
//			if (!int.TryParse(array[0], out birthDateYear))
//			{
//				showAgePrompt = true;
//			}
//			int birthDateMonth;
//			if (!int.TryParse(array[1], out birthDateMonth))
//			{
//				showAgePrompt = true;
//			}
//			int birthDateDay;
//			if (!int.TryParse(array[2], out birthDateDay))
//			{
//				showAgePrompt = true;
//			}
//			SaveManager.BirthDateYear = birthDateYear;
//			SaveManager.BirthDateMonth = birthDateMonth;
//			SaveManager.BirthDateDay = birthDateDay;
//		}
//		this.kwsUserId = permissionsCallbackInfo.KWSUserId;
//		base.StartCoroutine(this.FinishLoginFlow(showAgePrompt));
//	}

//	// Token: 0x06000053 RID: 83 RVA: 0x000032B1 File Offset: 0x000014B1
//	private IEnumerator FinishLoginFlow(bool showAgePrompt)
//	{
//		this.waitingText.gameObject.SetActive(false);
//		if (showAgePrompt)
//		{
//			yield return DestroyableSingleton<AccountManager>.Instance.ShowAgeGate();
//		}
//		SaveManager.SaveLocalDoB(SaveManager.BirthDateYear, SaveManager.BirthDateMonth, SaveManager.BirthDateDay);
//		if (this.IsMinor() && !SaveManager.IsGuest)
//		{
//			AddNotifyPermissionsUpdateReceivedOptions addNotifyPermissionsUpdateReceivedOptions = new AddNotifyPermissionsUpdateReceivedOptions();
//			this.platformInterface.GetKWSInterface().AddNotifyPermissionsUpdateReceived(addNotifyPermissionsUpdateReceivedOptions, null, new OnPermissionsUpdateReceivedCallback(this.KWSPermissionsUpdatedCallback));
//			if (string.IsNullOrEmpty(this.kwsUserId))
//			{
//				yield return DestroyableSingleton<AccountManager>.Instance.ShowPermissionsRequestForm();
//				if (!SaveManager.IsGuest && !string.IsNullOrWhiteSpace(SaveManager.GuardianEmail))
//				{
//					CreateUserOptions createUserOptions = new CreateUserOptions
//					{
//						LocalUserId = this.userId,
//						DateOfBirth = string.Concat(new string[]
//						{
//							SaveManager.BirthDateYear.ToString(),
//							"-",
//							SaveManager.BirthDateMonth.ToString().PadLeft(2, '0'),
//							"-",
//							SaveManager.BirthDateDay.ToString().PadLeft(2, '0')
//						}),
//						ParentEmail = SaveManager.GuardianEmail
//					};
//					this.platformInterface.GetKWSInterface().CreateUser(createUserOptions, null, new OnCreateUserCallback(this.CreateKWSUserCallback));
//				}
//				else
//				{
//					DestroyableSingleton<AccountManager>.Instance.SetGuestMinor();
//					this.loginFlowFinished = true;
//				}
//			}
//			else
//			{
//				this.UpdatePermissionKeys();
//				if (!DestroyableSingleton<AccountManager>.Instance.HasMinorsGuardianEverUpdatedAnything() || DestroyableSingleton<AccountManager>.Instance.HasGuardianRejectedEverything())
//				{
//					DestroyableSingleton<AccountManager>.Instance.SetMinorWaitingForGuardian();
//				}
//				else if (DestroyableSingleton<AccountManager>.Instance.CanMinorSignIntoAccount())
//				{
//					if (SaveManager.HasLoggedIn)
//					{
//						this.LoginWithCorrectPlatform();
//					}
//					else
//					{
//						DestroyableSingleton<AccountManager>.Instance.SetApprovedMinorCanLogIn();
//						this.loginFlowFinished = true;
//					}
//				}
//				else
//				{
//					DestroyableSingleton<AccountManager>.Instance.SetApprovedMinorCantLogIn();
//					this.loginFlowFinished = true;
//				}
//			}
//		}
//		else if (SaveManager.IsGuest)
//		{
//			DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//			this.loginFlowFinished = true;
//		}
//		else if (SaveManager.HasLoggedIn)
//		{
//			this.LoginWithCorrectPlatform();
//		}
//		else
//		{
//			DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//			this.loginFlowFinished = true;
//		}
//		yield break;
//	}

//	// Token: 0x06000054 RID: 84 RVA: 0x000032C7 File Offset: 0x000014C7
//	public void LoginWithCorrectPlatform()
//	{
//		Debug.Log("Login platform not supported - continue as guest");
//		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//		DestroyableSingleton<AccountManager>.Instance.ShowCreateFail(EOSManager.EOS_ERRORS.UnsupportedPlatform);
//	}

//	// Token: 0x06000055 RID: 85 RVA: 0x000032E8 File Offset: 0x000014E8
//	public void LoginWithExistingToken(ExternalCredentialType externalCredentialType)
//	{
//		LoginOptions loginOptions = new LoginOptions
//		{
//			Credentials = new Credentials
//			{
//				Token = this.tokenStr,
//				Type = externalCredentialType
//			},
//			UserLoginInfo = new UserLoginInfo
//			{
//				DisplayName = SaveManager.PlayerName
//			}
//		};
//		this.platformInterface.GetConnectInterface().Login(loginOptions, null, new OnLoginCallback(this.EOSConnectPlatformLoginCallback));
//	}

//	// Token: 0x06000056 RID: 86 RVA: 0x00003350 File Offset: 0x00001550
//	public void CreateAccountWithCorrectPlatform()
//	{
//		if (this.continuanceToken == null)
//		{
//			Debug.LogError("EOS Failed to get continuance token");
//			DestroyableSingleton<AccountManager>.Instance.SignInFail(EOSManager.EOS_ERRORS.NullContinuanceToken);
//			return;
//		}
//		LinkAccountOptions linkAccountOptions = new LinkAccountOptions
//		{
//			ContinuanceToken = this.continuanceToken,
//			LocalUserId = this.userId
//		};
//		this.platformInterface.GetConnectInterface().LinkAccount(linkAccountOptions, null, new OnLinkAccountCallback(this.OnLinkEOSUserCallback));
//	}

//	// Token: 0x06000057 RID: 87 RVA: 0x000033C0 File Offset: 0x000015C0
//	private void OnLinkEOSUserCallback(LinkAccountCallbackInfo linkUserCallbackInfo)
//	{
//		if (linkUserCallbackInfo.ResultCode == null)
//		{
//			string text = "";
//			string text2 = "";
//			this.userId.ToString(ref text);
//			linkUserCallbackInfo.LocalUserId.ToString(ref text2);
//			if (!text.Equals(text2))
//			{
//				Debug.LogError("Mismatching product user IDs " + text + " " + text2);
//			}
//			DestroyableSingleton<AccountManager>.Instance.SetNotGuest();
//			SaveManager.HasLoggedIn = true;
//			if (!this.IsMinor())
//			{
//				SaveManager.ChatModeType = QuickChatModes.FreeChatOrQuickChat;
//			}
//			DestroyableSingleton<AccountManager>.Instance.ShowCreateSuccess();
//			return;
//		}
//		Debug.LogError(string.Format("EOS Failed to link accounts: {0}", linkUserCallbackInfo.ResultCode));
//		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//		DestroyableSingleton<AccountManager>.Instance.ShowCreateFail(EOSManager.EOS_ERRORS.LinkAccountFail);
//	}

//	// Token: 0x06000058 RID: 88 RVA: 0x00003474 File Offset: 0x00001674
//	private void EOSConnectPlatformLoginCallback(LoginCallbackInfo loginCallbackInfo)
//	{
//		if (loginCallbackInfo.ResultCode == null)
//		{
//			string text = "";
//			string value = "";
//			this.userId.ToString(ref text);
//			loginCallbackInfo.LocalUserId.ToString(ref value);
//			if (!text.Equals(value))
//			{
//				Debug.LogError(string.Format("EOS Failed to Login Properly: {0}", loginCallbackInfo.ResultCode));
//				DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//				this.loginFlowFinished = true;
//				DestroyableSingleton<AccountManager>.Instance.SignInFail(EOSManager.EOS_ERRORS.MismatchedProductUserIDs);
//				return;
//			}
//			this.userId = loginCallbackInfo.LocalUserId;
//			if (!SaveManager.HasLoggedIn)
//			{
//				SaveManager.HasLoggedIn = true;
//				DestroyableSingleton<AccountManager>.Instance.SignInSuccess();
//				if (!this.IsMinor())
//				{
//					SaveManager.ChatModeType = QuickChatModes.FreeChatOrQuickChat;
//				}
//			}
//			else
//			{
//				this.loginFlowFinished = true;
//			}
//			DestroyableSingleton<AccountManager>.Instance.SetNotGuest();
//			return;
//		}
//		else
//		{
//			if (!string.IsNullOrEmpty(this.tokenStr) && SaveManager.HasLoggedIn)
//			{
//				this.tokenStr = "";
//				this.LoginWithCorrectPlatform();
//				return;
//			}
//			if (this.continuanceToken == null)
//			{
//				this.continuanceToken = loginCallbackInfo.ContinuanceToken;
//			}
//			this.createAccountAsk.SetTwoButtons();
//			this.createAccountAsk.button1.OnClick.AddListener(delegate()
//			{
//				this.CreateAccountWithCorrectPlatform();
//			});
//			this.createAccountAsk.button2.OnClick.AddListener(delegate()
//			{
//				DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//			});
//			this.createAccountAsk.gameObject.SetActive(true);
//			return;
//		}
//	}

//	// Token: 0x06000059 RID: 89 RVA: 0x000035EA File Offset: 0x000017EA
//	public void SetSignInFlowFinished()
//	{
//		this.loginFlowFinished = true;
//	}

//	// Token: 0x0600005A RID: 90 RVA: 0x000035F4 File Offset: 0x000017F4
//	private void UpdatePermissionKeys()
//	{
//		Debug.Log("KWS User ID " + this.kwsUserId);
//		GetPermissionByKeyOptions getPermissionByKeyOptions = new GetPermissionByKeyOptions
//		{
//			LocalUserId = this.userId,
//			Key = "freeChat"
//		};
//		Result permissionByKey = this.platformInterface.GetKWSInterface().GetPermissionByKey(getPermissionByKeyOptions, ref DestroyableSingleton<AccountManager>.Instance.freeChatAllowed);
//		if (permissionByKey != null)
//		{
//			Debug.Log("freeChat Update: " + permissionByKey.ToString());
//		}
//		GetPermissionByKeyOptions getPermissionByKeyOptions2 = new GetPermissionByKeyOptions
//		{
//			LocalUserId = this.userId,
//			Key = "customDisplayName"
//		};
//		permissionByKey = this.platformInterface.GetKWSInterface().GetPermissionByKey(getPermissionByKeyOptions2, ref DestroyableSingleton<AccountManager>.Instance.customDisplayName);
//		if (permissionByKey != null)
//		{
//			Debug.Log("custom display name Update: " + permissionByKey.ToString());
//		}
//		DestroyableSingleton<AccountManager>.Instance.UpdateMinorPermissions();
//	}

//	// Token: 0x0600005B RID: 91 RVA: 0x000036D0 File Offset: 0x000018D0
//	private void CreateKWSUserCallback(CreateUserCallbackInfo createUserCallbackInfo)
//	{
//		QueryPermissionsOptions queryPermissionsOptions = new QueryPermissionsOptions
//		{
//			LocalUserId = this.userId
//		};
//		this.platformInterface.GetKWSInterface().QueryPermissions(queryPermissionsOptions, null, new OnQueryPermissionsCallback(this.KWSQueryPermissionsCallback));
//	}

//	// Token: 0x0600005C RID: 92 RVA: 0x00003710 File Offset: 0x00001910
//	private void KWSPermissionsUpdatedCallback(PermissionsUpdateReceivedCallbackInfo permissionsCallbackInfo)
//	{
//		bool freeChatAllowed = DestroyableSingleton<AccountManager>.Instance.freeChatAllowed != null;
//		KWSPermissionStatus customDisplayName = DestroyableSingleton<AccountManager>.Instance.customDisplayName;
//		bool flag = !freeChatAllowed || customDisplayName == 0;
//		this.UpdatePermissionKeys();
//		bool flag2 = DestroyableSingleton<AccountManager>.Instance.freeChatAllowed == null || DestroyableSingleton<AccountManager>.Instance.customDisplayName == 0;
//		if (!flag2)
//		{
//			DestroyableSingleton<AccountManager>.Instance.SetApprovedMinorCantLogIn();
//		}
//		if (flag != flag2 && flag2)
//		{
//			DestroyableSingleton<AccountManager>.Instance.SetApprovedMinorCanLogIn();
//		}
//		DestroyableSingleton<AccountManager>.Instance.UpdateKidAccountDisplay();
//	}

//	// Token: 0x0600005D RID: 93 RVA: 0x00003789 File Offset: 0x00001989
//	public void LogOut()
//	{
//		SaveManager.HasLoggedIn = false;
//		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
//	}

//	// Token: 0x0600005E RID: 94 RVA: 0x0000379C File Offset: 0x0000199C
//	public void UpdateGuardianEmail()
//	{
//		UpdateParentEmailOptions updateParentEmailOptions = new UpdateParentEmailOptions
//		{
//			LocalUserId = this.userId,
//			ParentEmail = SaveManager.GuardianEmail
//		};
//		this.platformInterface.GetKWSInterface().UpdateParentEmail(updateParentEmailOptions, null, new OnUpdateParentEmailCallback(this.UpdateGuardianEmailSettingsCallback));
//	}

//	// Token: 0x0600005F RID: 95 RVA: 0x000037E4 File Offset: 0x000019E4
//	private void UpdateGuardianEmailSettingsCallback(UpdateParentEmailCallbackInfo updateParentEmailCallbackInfo)
//	{
//		Debug.Log("Successfully updaed guardian email");
//	}

//	// Token: 0x06000060 RID: 96 RVA: 0x000037F0 File Offset: 0x000019F0
//	public bool IsMinor()
//	{
//		DateTime t = new DateTime(SaveManager.BirthDateYear, SaveManager.BirthDateMonth, SaveManager.BirthDateDay);
//		t = t.AddYears(this.ageOfConsent);
//		return t > DateTime.UtcNow;
//	}

//	// Token: 0x06000061 RID: 97 RVA: 0x00003831 File Offset: 0x00001A31
//	public bool IsFreechatAllowed()
//	{
//		if (SaveManager.HasLoggedIn)
//		{
//			if (this.IsMinor() && DestroyableSingleton<AccountManager>.Instance.freeChatAllowed == null)
//			{
//				return true;
//			}
//			if (!this.IsMinor())
//			{
//				return true;
//			}
//		}
//		SaveManager.ChatModeType = QuickChatModes.QuickChatOnly;
//		return false;
//	}

//	// Token: 0x06000062 RID: 98 RVA: 0x00003861 File Offset: 0x00001A61
//	public bool FinishedLoginFlow()
//	{
//		return this.loginFlowFinished;
//	}

//	// Token: 0x06000063 RID: 99 RVA: 0x00003869 File Offset: 0x00001A69
//	public PlatformInterface GetPlatformInterface()
//	{
//		return this.platformInterface;
//	}

//	// Token: 0x06000064 RID: 100 RVA: 0x00003871 File Offset: 0x00001A71
//	public void HasSignedIn()
//	{
//		DestroyableSingleton<AccountManager>.Instance.SetNotGuest();
//		SaveManager.HasLoggedIn = true;
//		SaveManager.IsGuest = false;
//		if (!this.IsMinor())
//		{
//			SaveManager.ChatModeType = QuickChatModes.FreeChatOrQuickChat;
//		}
//	}

//	// Token: 0x06000065 RID: 101 RVA: 0x00003897 File Offset: 0x00001A97
//	private void OnEnable()
//	{
//		if (EOSManager._instance == null)
//		{
//			EOSManager._instance = this;
//		}
//		bool flag = this.platformInitialized;
//	}

//	// Token: 0x06000066 RID: 102 RVA: 0x000038B3 File Offset: 0x00001AB3
//	private void OnDestroy()
//	{
//		if (EOSManager._instance != this)
//		{
//			return;
//		}
//		EOSManager._instance = null;
//		if (!this.platformInitialized)
//		{
//			return;
//		}
//		this.DoShutdown();
//	}

//	// Token: 0x06000067 RID: 103 RVA: 0x000038D8 File Offset: 0x00001AD8
//	[ContextMenu("Shutdown")]
//	private void DoShutdown()
//	{
//		if (this.platformInterface != null)
//		{
//			this.platformInterface.Release();
//			this.platformInterface = null;
//		}
//		PlatformInterface.Shutdown();
//	}

//	// Token: 0x06000068 RID: 104 RVA: 0x00003900 File Offset: 0x00001B00
//	private void Update()
//	{
//		if (!this.platformInitialized)
//		{
//			return;
//		}
//		if (this.platformInterface != null)
//		{
//			this.platformTickTimer += Time.deltaTime;
//			if (this.platformTickTimer >= 0.1f)
//			{
//				this.platformTickTimer = 0f;
//				this.platformInterface.Tick();
//			}
//		}
//	}

//	// Token: 0x04000025 RID: 37
//	[SerializeField]
//	private string productName;

//	// Token: 0x04000026 RID: 38
//	[SerializeField]
//	private string productVersion;

//	// Token: 0x04000027 RID: 39
//	[SerializeField]
//	private string productId;

//	// Token: 0x04000028 RID: 40
//	[SerializeField]
//	private string sandboxId;

//	// Token: 0x04000029 RID: 41
//	[SerializeField]
//	private string deploymentId;

//	// Token: 0x0400002A RID: 42
//	[SerializeField]
//	private string clientId;

//	// Token: 0x0400002B RID: 43
//	[SerializeField]
//	private string clientSecret;

//	// Token: 0x0400002C RID: 44
//	[SerializeField]
//	private GameObject waitingText;

//	// Token: 0x0400002D RID: 45
//	[SerializeField]
//	private InfoTextBox createAccountAsk;

//	// Token: 0x0400002E RID: 46
//	private string tokenStr = "";

//	// Token: 0x0400002F RID: 47
//	private const float platformTickInterval = 0.1f;

//	// Token: 0x04000030 RID: 48
//	private float platformTickTimer;

//	// Token: 0x04000031 RID: 49
//	private bool platformInitialized;

//	// Token: 0x04000032 RID: 50
//	private bool loginFlowFinished;

//	// Token: 0x04000033 RID: 51
//	private PlatformInterface platformInterface;

//	// Token: 0x04000034 RID: 52
//	private ProductUserId userId;

//	// Token: 0x04000035 RID: 53
//	private int ageOfConsent;

//	// Token: 0x04000036 RID: 54
//	private string kwsUserId;

//	// Token: 0x04000037 RID: 55
//	private static EOSManager _instance;

//	// Token: 0x04000038 RID: 56
//	public TextRenderer debugText;

//	// Token: 0x04000039 RID: 57
//	private ContinuanceToken continuanceToken;

//	// Token: 0x0400003A RID: 58
//	public string exchangeToken = "";

//	// Token: 0x020002B0 RID: 688
//	public enum EOS_ERRORS
//	{
//		// Token: 0x040015D1 RID: 5585
//		FailedEpicAuthToken,
//		// Token: 0x040015D2 RID: 5586
//		UnsupportedPlatform,
//		// Token: 0x040015D3 RID: 5587
//		LinkAccountFail,
//		// Token: 0x040015D4 RID: 5588
//		SteamworksAppTicketFail,
//		// Token: 0x040015D5 RID: 5589
//		SteamworksAuthFail,
//		// Token: 0x040015D6 RID: 5590
//		iOSAuthFail,
//		// Token: 0x040015D7 RID: 5591
//		NullContinuanceToken,
//		// Token: 0x040015D8 RID: 5592
//		MismatchedProductUserIDs,
//		// Token: 0x040015D9 RID: 5593
//		GenericLoginError,
//		// Token: 0x040015DA RID: 5594
//		XboxUserAddError,
//		// Token: 0x040015DB RID: 5595
//		XboxGetTokenError
//	}
//}
