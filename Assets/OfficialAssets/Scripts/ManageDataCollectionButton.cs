using System;
using UnityEngine;
using UnityEngine.Analytics;

// Token: 0x0200010A RID: 266
public class ManageDataCollectionButton : MonoBehaviour
{
	// Token: 0x06000692 RID: 1682 RVA: 0x0002A4A1 File Offset: 0x000286A1
	public void ManageData()
	{
		//DataPrivacy.FetchPrivacyUrl(new Action<string>(Application.OpenURL), new Action<string>(this.ShowPopup));
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0002A4C0 File Offset: 0x000286C0
	private void ShowPopup(string error)
	{
		if (this.PopupPrefab)
		{
			GenericPopup genericPopup = UnityEngine.Object.Instantiate<GenericPopup>(this.PopupPrefab);
			genericPopup.TextArea.Text = error;
			genericPopup.transform.SetWorldZ(base.transform.position.z - 1f);
		}
	}

	// Token: 0x0400076B RID: 1899
	public GenericPopup PopupPrefab;
}
