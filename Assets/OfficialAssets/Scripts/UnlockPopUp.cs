using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class UnlockPopUp : MonoBehaviour
{
	// Token: 0x06000DB4 RID: 3508 RVA: 0x0005305F File Offset: 0x0005125F
	public IEnumerator Show()
	{
		DateTime utcNow = DateTime.UtcNow;
		if ((utcNow.DayOfYear < 350 && utcNow.DayOfYear > 4) || SaveManager.GetPurchase("hats_newyears2018"))
		{
			yield break;
		}
		base.gameObject.SetActive(true);
		SaveManager.SetPurchased("hats_newyears2018");
		while (base.isActiveAndEnabled)
		{
			yield return null;
		}
		yield break;
	}
}
