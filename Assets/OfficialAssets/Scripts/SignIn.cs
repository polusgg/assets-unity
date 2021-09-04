using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class SignIn : MonoBehaviour
{
	// Token: 0x0600008B RID: 139 RVA: 0x00003D92 File Offset: 0x00001F92
	public void DisableScreens()
	{
		this.mainScreen.gameObject.SetActive(false);
		this.signInScreen.gameObject.SetActive(false);
		this.createAccountScreen.gameObject.SetActive(false);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003DC7 File Offset: 0x00001FC7
	public void ShowSignIn()
	{
		base.gameObject.SetActive(true);
		this.DisableScreens();
		this.signInScreen.gameObject.SetActive(true);
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00003DEC File Offset: 0x00001FEC
	public void ShowCreateAccount()
	{
		base.gameObject.SetActive(true);
		this.DisableScreens();
		this.createAccountScreen.gameObject.SetActive(true);
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00003E11 File Offset: 0x00002011
	public void Back()
	{
		this.DisableScreens();
		this.mainScreen.gameObject.SetActive(true);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00003E2A File Offset: 0x0000202A
	public void ContinueAsGuest()
	{
		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
		this.Close();
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00003E3C File Offset: 0x0000203C
	public void Close()
	{
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00003E49 File Offset: 0x00002049
	public void Open()
	{
		base.GetComponent<TransitionOpen>().OnEnable();
	}

	// Token: 0x04000058 RID: 88
	[SerializeField]
	private GameObject mainScreen;

	// Token: 0x04000059 RID: 89
	[SerializeField]
	private GameObject signInScreen;

	// Token: 0x0400005A RID: 90
	[SerializeField]
	private GameObject createAccountScreen;
}
