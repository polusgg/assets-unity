using System;
using TMPro;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class ReportConfirmScreen : MonoBehaviour
{
	// Token: 0x06000451 RID: 1105 RVA: 0x0001BE11 File Offset: 0x0001A011
	public void Show(string playerName, int colorId)
	{
		this.NameText.text = playerName;
		PlayerControl.SetPlayerMaterialColors(colorId, this.PlayerIcon);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001BE37 File Offset: 0x0001A037
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000510 RID: 1296
	public TextMeshPro NameText;

	// Token: 0x04000511 RID: 1297
	public SpriteRenderer PlayerIcon;
}
