using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class CreateStoreButton : MonoBehaviour
{
	// Token: 0x060006BD RID: 1725 RVA: 0x0002B0A7 File Offset: 0x000292A7
	public void Click()
	{
		DestroyableSingleton<StoreMenu>.Instance.Open();
	}
}
