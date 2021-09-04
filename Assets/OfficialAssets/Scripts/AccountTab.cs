using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class AccountTab : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000023 RID: 35 RVA: 0x0000288C File Offset: 0x00000A8C
	private UiElement DefaultSelection
	{
		get
		{
			foreach (UiElement uiElement in this.PotentialDefaultSelections)
			{
				if (uiElement.isActiveAndEnabled)
				{
					return uiElement;
				}
			}
			return null;
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000028E8 File Offset: 0x00000AE8
	public void TurnAllSectionsOff()
	{
		this.fullAccount.gameObject.SetActive(false);
		this.kidAccount.gameObject.SetActive(false);
		this.guestAccount.SetActive(false);
		this.waitingForGuardian.SetActive(false);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002924 File Offset: 0x00000B24
	public void GoToGuestMode()
	{
		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002930 File Offset: 0x00000B30
	public void ShowGuestMode()
	{
		//this.TurnAllSectionsOff();
		//this.idCard.SetActive(true);
		//if (EOSManager.Instance.IsMinor())
		//{
		//	if (DestroyableSingleton<AccountManager>.Instance.CanMinorSignIntoAccount())
		//	{
		//		this.signIntoAccountButton.gameObject.SetActive(true);
		//	}
		//	else
		//	{
		//		this.signIntoAccountButton.gameObject.SetActive(false);
		//		this.askForGuardianEmailButton.gameObject.SetActive(!DestroyableSingleton<AccountManager>.Instance.HasMinorsGuardianEverUpdatedAnything() || DestroyableSingleton<AccountManager>.Instance.HasGuardianRejectedEverything());
		//	}
		//}
		//else
		//{
		//	this.askForGuardianEmailButton.gameObject.SetActive(false);
		//}
		//this.guestAccount.SetActive(true);
		//if (string.IsNullOrEmpty(SaveManager.PlayerName))
		//{
		//	DestroyableSingleton<AccountManager>.Instance.RandomizeName();
		//}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000029E9 File Offset: 0x00000BE9
	public void ShowWaitingForGuardian()
	{
		this.TurnAllSectionsOff();
		this.idCard.SetActive(false);
		this.guardianEmailText.Text = SaveManager.GuardianEmail;
		this.waitingForGuardian.SetActive(true);
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002A1C File Offset: 0x00000C1C
	public void ShowLoggedInMode()
	{
		//this.TurnAllSectionsOff();
		//this.idCard.SetActive(true);
		//this.fullAccount.gameObject.SetActive(true);
		//if (EOSManager.Instance.IsMinor())
		//{
		//	this.fullAccount.CanSetCustomName(DestroyableSingleton<AccountManager>.Instance.CanMinorSetCustomDisplayName());
		//}
		//else
		//{
		//	this.fullAccount.CanSetCustomName(true);
		//}
		//if (string.IsNullOrEmpty(SaveManager.PlayerName))
		//{
		//	DestroyableSingleton<AccountManager>.Instance.RandomizeName();
		//}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002A94 File Offset: 0x00000C94
	public void ShowMinorNotLoggedInMode()
	{
		this.TurnAllSectionsOff();
		this.idCard.SetActive(true);
		this.kidAccount.gameObject.SetActive(true);
		if (string.IsNullOrEmpty(SaveManager.PlayerName))
		{
			DestroyableSingleton<AccountManager>.Instance.RandomizeName();
		}
		this.UpdateKidAccountDisplay();
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002AE0 File Offset: 0x00000CE0
	public void UpdateKidAccountDisplay()
	{
		this.kidAccount.CanSetCustomName(DestroyableSingleton<AccountManager>.Instance.CanMinorSetCustomDisplayName());
		this.kidAccount.CanSignIntoAccount(DestroyableSingleton<AccountManager>.Instance.CanMinorSignIntoAccount());
		this.fullAccount.CanSetCustomName(DestroyableSingleton<AccountManager>.Instance.CanMinorSetCustomDisplayName());
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002B2C File Offset: 0x00000D2C
	public void UpdateKidAccountCanChangeName()
	{
		this.kidAccount.CanSetCustomName(DestroyableSingleton<AccountManager>.Instance.CanMinorSetCustomDisplayName());
		this.fullAccount.CanSetCustomName(DestroyableSingleton<AccountManager>.Instance.CanMinorSetCustomDisplayName());
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002B58 File Offset: 0x00000D58
	public void Toggle()
	{
		if (base.GetComponent<SlideOpen>().isOpen)
		{
			this.Close();
			return;
		}
		this.Open();
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002B74 File Offset: 0x00000D74
	public void Close()
	{
		base.GetComponent<SlideOpen>().Close();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002B91 File Offset: 0x00000D91
	public void Open()
	{
		this.UpdateNameDisplay();
		base.GetComponent<SlideOpen>().Open();
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultSelection, this.selectableObjects, false);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002BC7 File Offset: 0x00000DC7
	public void SignIn()
	{
		//if (EOSManager.Instance.IsMinor() && !DestroyableSingleton<AccountManager>.Instance.CanMinorSignIntoAccount())
		//{
		//	base.StartCoroutine(DestroyableSingleton<AccountManager>.Instance.ShowPermissionsRequestForm());
		//	return;
		//}
		//EOSManager.Instance.LoginWithCorrectPlatform();
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002BFD File Offset: 0x00000DFD
	public void CreateAccount()
	{
		this.Close();
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002C05 File Offset: 0x00000E05
	public void LinkAccount()
	{
		Debug.Log("TODO: Link Accounts");
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002C11 File Offset: 0x00000E11
	public void LogOut()
	{
		//EOSManager.Instance.LogOut();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002C1D File Offset: 0x00000E1D
	public void AskForGuardianEmail()
	{
		base.StartCoroutine(DestroyableSingleton<AccountManager>.Instance.ShowPermissionsRequestForm());
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002C30 File Offset: 0x00000E30
	public void RandomizeName()
	{
		DestroyableSingleton<AccountManager>.Instance.RandomizeName();
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002C3C File Offset: 0x00000E3C
	public void UpdateNameDisplay()
	{
		this.userName.Text = SaveManager.PlayerName;
		this.playerImage.UpdateFromSaveManager();
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002C59 File Offset: 0x00000E59
	public void ChangeName()
	{
		this.editNameScreen.gameObject.SetActive(true);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002C6C File Offset: 0x00000E6C
	public void ResendEmail()
	{
		DestroyableSingleton<AccountManager>.Instance.ShowGuardianEmailSentConfirm();
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002C78 File Offset: 0x00000E78
	public void EditGuardianEmail()
	{
		base.StartCoroutine(DestroyableSingleton<AccountManager>.Instance.EditGuardianEmail());
	}

	// Token: 0x04000016 RID: 22
	public TextRenderer userName;

	// Token: 0x04000017 RID: 23
	public PoolablePlayer playerImage;

	// Token: 0x04000018 RID: 24
	public GameObject guestAccount;

	// Token: 0x04000019 RID: 25
	public GameObject waitingForGuardian;

	// Token: 0x0400001A RID: 26
	public FullAccount fullAccount;

	// Token: 0x0400001B RID: 27
	public KidAccount kidAccount;

	// Token: 0x0400001C RID: 28
	public TextRenderer guardianEmailText;

	// Token: 0x0400001D RID: 29
	public EditName editNameScreen;

	// Token: 0x0400001E RID: 30
	public GameObject idCard;

	// Token: 0x0400001F RID: 31
	public GameObject createAccountButton;

	// Token: 0x04000020 RID: 32
	public GameObject signIntoAccountButton;

	// Token: 0x04000021 RID: 33
	public GameObject askForGuardianEmailButton;

	// Token: 0x04000022 RID: 34
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000023 RID: 35
	public List<UiElement> PotentialDefaultSelections;

	// Token: 0x04000024 RID: 36
	public List<UiElement> selectableObjects;
}
