using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class PrivacyPolicyScreen : MonoBehaviour
{
	// Token: 0x0600068E RID: 1678 RVA: 0x0002A465 File Offset: 0x00028665
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002A477 File Offset: 0x00028677
	public IEnumerator Show()
	{
		if (SaveManager.AcceptedPrivacyPolicy != 1)
		{
			base.gameObject.SetActive(true);
			ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.BackButton);
			while (base.gameObject.activeSelf)
			{
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002A486 File Offset: 0x00028686
	public void Close()
	{
		SaveManager.AcceptedPrivacyPolicy = 1;
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x0400076A RID: 1898
	[Header("Console Controller Navigation")]
	public UiElement BackButton;
}
