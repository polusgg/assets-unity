using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class KidAccount : MonoBehaviour
{
	// Token: 0x0600007B RID: 123 RVA: 0x00003B73 File Offset: 0x00001D73
	public void CanSetCustomName(bool canSetName)
	{
		this.randomizeNameButton.SetActive(!canSetName);
		this.editNameButton.SetActive(canSetName);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003B90 File Offset: 0x00001D90
	public void CanSignIntoAccount(bool canSignIn)
	{
		this.signInButton.SetActive(canSignIn);
		this.createAccountButton.SetActive(false);
		this.requestPermission.SetActive(!canSignIn);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003BB9 File Offset: 0x00001DB9
	public void HasSignedIntoAccount(bool hasSignedIn)
	{
		this.signInButton.SetActive(!hasSignedIn);
		this.logOutButton.SetActive(hasSignedIn);
		this.requestPermission.SetActive(false);
		this.createAccountButton.SetActive(false);
	}

	// Token: 0x0400004B RID: 75
	[SerializeField]
	private GameObject signInButton;

	// Token: 0x0400004C RID: 76
	[SerializeField]
	private GameObject createAccountButton;

	// Token: 0x0400004D RID: 77
	[SerializeField]
	private GameObject randomizeNameButton;

	// Token: 0x0400004E RID: 78
	[SerializeField]
	private GameObject editNameButton;

	// Token: 0x0400004F RID: 79
	[SerializeField]
	private GameObject requestPermission;

	// Token: 0x04000050 RID: 80
	[SerializeField]
	private GameObject logOutButton;
}
