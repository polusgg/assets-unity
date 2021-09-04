using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class MainMenuManager : MonoBehaviour
{
	// Token: 0x06000688 RID: 1672 RVA: 0x0002A33D File Offset: 0x0002853D
	public void Start()
	{
		ChatLanguageSet.Instance.Load();
		base.StartCoroutine(this.RunStartUp());
		QualitySettings.vSyncCount = (SaveManager.VSync ? 1 : 0);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002A366 File Offset: 0x00028566
	private IEnumerator RunStartUp()
	{
		yield return this.EOSLogin();
		yield return this.PrivacyPolicy.Show();
		base.StartCoroutine(this.Announcement.Init());
		yield return this.Announcement.Show();
		this.CheckAddOns();
		yield return null;
		ControllerManager.Instance.NewScene(base.name, null, this.DefaultButtonSelected, this.ControllerSelectable, false);
		ControllerManager.Instance.CurrentUiState.CurrentSelection = this.DefaultButtonSelected;
		yield break;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002A378 File Offset: 0x00028578
	private void CheckAddOns()
	{
		DateTime utcNow = DateTime.UtcNow;
		for (int i = 0; i < DestroyableSingleton<HatManager>.Instance.AllHats.Count; i++)
		{
			HatBehaviour hatBehaviour = DestroyableSingleton<HatManager>.Instance.AllHats[i];
			if (!hatBehaviour.ProdId.StartsWith("pet_") && (hatBehaviour.LimitedMonth == utcNow.Month || hatBehaviour.LimitedMonth == 0) && (hatBehaviour.LimitedYear == utcNow.Year || hatBehaviour.LimitedYear == 0) && !hatBehaviour.NotInStore && !HatManager.IsMapStuff(hatBehaviour.ProdId) && !SaveManager.GetPurchase(hatBehaviour.ProductId))
			{
				SaveManager.SetPurchased(hatBehaviour.ProductId);
			}
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0002A429 File Offset: 0x00028629
	public void ShowAnnouncementPopUp()
	{
		if (this.Announcement != null)
		{
			this.Announcement.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0002A44A File Offset: 0x0002864A
	public IEnumerator EOSLogin()
	{
		//EOSManager.Instance.LoginForKWS(true);
		//while (!EOSManager.Instance.FinishedLoginFlow())
		//{
		//	yield return null;
		//}
		yield break;
	}

	// Token: 0x04000764 RID: 1892
	public AdDataCollectScreen AdsPolicy;

	// Token: 0x04000765 RID: 1893
	public PrivacyPolicyScreen PrivacyPolicy;

	// Token: 0x04000766 RID: 1894
	public AnnouncementPopUp Announcement;

	// Token: 0x04000767 RID: 1895
	[Header("Console Controller Navigation")]
	public UiElement DefaultButtonSelected;

	// Token: 0x04000768 RID: 1896
	public List<UiElement> ControllerSelectable;

	// Token: 0x04000769 RID: 1897
	public List<PassiveButton> disableOnStartup = new List<PassiveButton>();
}
