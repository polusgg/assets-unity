using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class AdDataCollectScreen : MonoBehaviour
{
	// Token: 0x06000670 RID: 1648 RVA: 0x00029F89 File Offset: 0x00028189
	public void ForceShow()
	{
		SaveManager.ShowAdsScreen = ShowAdsState.NotAccepted;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00029F9D File Offset: 0x0002819D
	public IEnumerator Show()
	{
		if (SaveManager.ShowAdsScreen == ShowAdsState.NotAccepted && !SaveManager.BoughtNoAds)
		{
			base.gameObject.SetActive(true);
			while (base.gameObject.activeSelf)
			{
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00029FAC File Offset: 0x000281AC
	public void Update()
	{
		if (SaveManager.BoughtNoAds)
		{
			SaveManager.ShowAdsScreen = ShowAdsState.Purchased;
			this.Close();
		}
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00029FC1 File Offset: 0x000281C1
	public void Close()
	{
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00029FCE File Offset: 0x000281CE
	public void SetPersonalized()
	{
		if (SaveManager.ShowAdsScreen != ShowAdsState.Purchased)
		{
			SaveManager.ShowAdsScreen = ShowAdsState.Personalized;
		}
		this.Close();
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00029FE4 File Offset: 0x000281E4
	public void SetNonPersonalized()
	{
		if (SaveManager.ShowAdsScreen != ShowAdsState.Purchased)
		{
			SaveManager.ShowAdsScreen = ShowAdsState.NonPersonalized;
		}
		this.Close();
	}
}
