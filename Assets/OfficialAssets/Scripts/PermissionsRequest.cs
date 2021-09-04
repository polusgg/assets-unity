using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class PermissionsRequest : MonoBehaviour
{
	// Token: 0x06000082 RID: 130 RVA: 0x00003CCE File Offset: 0x00001ECE
	public IEnumerator Show()
	{
		base.gameObject.SetActive(true);
		while (base.gameObject.activeSelf)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003CDD File Offset: 0x00001EDD
	public void Close()
	{
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003CEA File Offset: 0x00001EEA
	public void ContinueWithout()
	{
		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
		this.Close();
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00003CFC File Offset: 0x00001EFC
	public void SendEmail()
	{
		if (!this.emailText.ShakeIfInvalid())
		{
			SaveManager.GuardianEmail = this.emailText.GetEmailValidEmail();
			DestroyableSingleton<AccountManager>.Instance.SetNotGuest();
			DestroyableSingleton<AccountManager>.Instance.ShowGuardianEmailSentConfirm();
			DestroyableSingleton<AccountManager>.Instance.SetMinorWaitingForGuardian();
			this.Close();
		}
	}

	// Token: 0x04000055 RID: 85
	public EmailTextBehaviour emailText;
}
