using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class UpdateGuardianEmail : MonoBehaviour
{
	// Token: 0x06000093 RID: 147 RVA: 0x00003E5E File Offset: 0x0000205E
	public IEnumerator Show()
	{
		base.gameObject.SetActive(true);
		while (base.gameObject.activeSelf)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00003E6D File Offset: 0x0000206D
	public void Close()
	{
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00003E7A File Offset: 0x0000207A
	public void ContinueWithout()
	{
		DestroyableSingleton<AccountManager>.Instance.SetIsGuest();
		this.Close();
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00003E8C File Offset: 0x0000208C
	public void SendUpdateGuardianEmail()
	{
		//if (!this.emailText.ShakeIfInvalid() && !this.emailConfirmText.ShakeIfInvalid() && !this.emailConfirmText.ShakeIfDoesntMatch(this.emailText.nameSource.text, this.emailConfirmText.nameSource.text))
		//{
		//	SaveManager.GuardianEmail = this.emailText.GetEmailValidEmail();
		//	EOSManager.Instance.UpdateGuardianEmail();
		//	DestroyableSingleton<AccountManager>.Instance.SetNotGuest();
		//	DestroyableSingleton<AccountManager>.Instance.ShowGuardianEmailSentConfirm();
		//	DestroyableSingleton<AccountManager>.Instance.SetMinorWaitingForGuardian();
		//	this.Close();
		//}
	}

	// Token: 0x0400005B RID: 91
	public EmailTextBehaviour emailText;

	// Token: 0x0400005C RID: 92
	public EmailTextBehaviour emailConfirmText;
}
