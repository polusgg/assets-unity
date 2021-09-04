using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class EditName : MonoBehaviour
{
	// Token: 0x0600006C RID: 108 RVA: 0x000039BC File Offset: 0x00001BBC
	private void OnEnable()
	{
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultSelection, this.selectableObjects, false);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000039E1 File Offset: 0x00001BE1
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000039F3 File Offset: 0x00001BF3
	public IEnumerator Show()
	{
		base.gameObject.SetActive(true);
		while (base.gameObject.activeSelf)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003A02 File Offset: 0x00001C02
	public void Close()
	{
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00003A0F File Offset: 0x00001C0F
	public void UpdateName()
	{
		if (!this.nameText.ShakeIfInvalid())
		{
			this.nameText.UpdateName();
			DestroyableSingleton<AccountManager>.Instance.UpdateAccountInfoDisplays();
			this.Close();
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003A39 File Offset: 0x00001C39
	public void RandomizeName()
	{
		this.nameText.nameSource.SetText(DestroyableSingleton<AccountManager>.Instance.GetRandomName(), "");
	}

	// Token: 0x0400003B RID: 59
	public NameTextBehaviour nameText;

	// Token: 0x0400003C RID: 60
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400003D RID: 61
	public UiElement DefaultSelection;

	// Token: 0x0400003E RID: 62
	public List<UiElement> selectableObjects;
}
