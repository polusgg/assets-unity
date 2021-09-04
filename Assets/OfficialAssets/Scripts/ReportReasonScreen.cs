using System;
using InnerNet;
using TMPro;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class ReportReasonScreen : MonoBehaviour
{
	// Token: 0x06000454 RID: 1108 RVA: 0x0001BE4D File Offset: 0x0001A04D
	public void Show(string playerName, int colorId)
	{
		this.playerName = playerName;
		this.colorId = colorId;
		this.NameText.text = playerName;
		PlayerControl.SetPlayerMaterialColors(colorId, this.PlayerIcon);
		this.SelectReason(-1);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001BE88 File Offset: 0x0001A088
	public void SelectReason(int reason)
	{
		ReportReasons? reportReasons = this.currentReason;
		ReportReasons reportReasons2 = ReportReasons.InappropriateName;
		if (!(reportReasons.GetValueOrDefault() < reportReasons2 & reportReasons != null))
		{
			reportReasons = this.currentReason;
			reportReasons2 = ReportReasons.Harassment_Misconduct;
			if (!(reportReasons.GetValueOrDefault() > reportReasons2 & reportReasons != null))
			{
				this.currentReason = new ReportReasons?((ReportReasons)reason);
				goto IL_54;
			}
		}
		this.currentReason = null;
		IL_54:
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			this.Buttons[i].ChangeOutColor((i == reason) ? Color.green : Color.white);
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001BF1C File Offset: 0x0001A11C
	public void Submit()
	{
		if (this.currentReason == null)
		{
			return;
		}
		this.Parent.ReportPlayer(this.currentReason.Value);
		this.Hide();
		this.ConfirmScreen.Show(this.playerName, this.colorId);
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001BF6A File Offset: 0x0001A16A
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000512 RID: 1298
	public BanMenu Parent;

	// Token: 0x04000513 RID: 1299
	public ButtonRolloverHandler[] Buttons;

	// Token: 0x04000514 RID: 1300
	public TextMeshPro NameText;

	// Token: 0x04000515 RID: 1301
	public SpriteRenderer PlayerIcon;

	// Token: 0x04000516 RID: 1302
	private ReportReasons? currentReason;

	// Token: 0x04000517 RID: 1303
	public ReportConfirmScreen ConfirmScreen;

	// Token: 0x04000518 RID: 1304
	private string playerName;

	// Token: 0x04000519 RID: 1305
	private int colorId;
}
