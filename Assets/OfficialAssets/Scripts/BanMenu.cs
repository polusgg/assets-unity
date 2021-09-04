using System;
using System.Collections.Generic;
using System.Linq;
using InnerNet;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020000B4 RID: 180
public class BanMenu : MonoBehaviour
{
	// Token: 0x06000448 RID: 1096 RVA: 0x0001B7A0 File Offset: 0x000199A0
	public void SetVisible(bool show)
	{
		show &= (PlayerControl.LocalPlayer && PlayerControl.LocalPlayer.Data != null && !PlayerControl.LocalPlayer.Data.IsDead);
		this.BanButton.gameObject.SetActive(AmongUsClient.Instance.CanBan());
		this.KickButton.gameObject.SetActive(AmongUsClient.Instance.CanKick());
		base.GetComponent<SpriteRenderer>().enabled = show;
		base.GetComponent<PassiveButton>().enabled = show;
		this.hotkeyGlyph.SetActive(show);
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0001B838 File Offset: 0x00019A38
	private void Update()
	{
		if (!AmongUsClient.Instance)
		{
			return;
		}
		for (int i = 0; i < AmongUsClient.Instance.allClients.Count; i++)
		{
			try
			{
				ClientData client = AmongUsClient.Instance.allClients[i];
				if (client == null)
				{
					break;
				}
				int[] source;
				if (VoteBanSystem.Instance.HasMyVote(client.Id) && VoteBanSystem.Instance.Votes.TryGetValue(client.Id, out source))
				{
					int num = source.Count((int c) => c != 0);
					BanButton banButton = this.allButtons.FirstOrDefault((BanButton b) => b.TargetClientId == client.Id);
					if (banButton && banButton.numVotes != num)
					{
						banButton.SetVotes(num);
					}
				}
			}
			catch
			{
				break;
			}
		}
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0001B940 File Offset: 0x00019B40
	public void Show()
	{
		if (this.ContentParent.activeSelf)
		{
			this.Hide();
			return;
		}
		this.selectedClientId = -1;
		this.KickButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
		this.BanButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
		this.ReportButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
		this.ContentParent.SetActive(true);
		int num = 0;
		if (AmongUsClient.Instance)
		{
			List<ClientData> allClients = AmongUsClient.Instance.allClients;
			for (int i = 0; i < allClients.Count; i++)
			{
				ClientData clientData = allClients[i];
				if (clientData.Id != AmongUsClient.Instance.ClientId && clientData.Character)
				{
					GameData.PlayerInfo data = clientData.Character.Data;
					if (!string.IsNullOrWhiteSpace(data.PlayerName))
					{
						BanButton banButton = Object.Instantiate<BanButton>(this.BanButtonPrefab, this.ContentParent.transform);
						banButton.transform.localPosition = new Vector3(-0.2f, -0.15f - 0.4f * (float)num, -1f);
						banButton.Parent = this;
						banButton.NameText.Text = data.PlayerName;
						banButton.TargetClientId = clientData.Id;
						banButton.Unselect();
						this.allButtons.Add(banButton);
						this.ControllerSelectable.AddUnique(banButton.GetComponent<UiElement>());
						num++;
					}
				}
			}
		}
		float y = -0.15f - 0.4f * (float)num - 0.1f;
		this.KickButton.transform.SetLocalY(y);
		this.BanButton.transform.SetLocalY(y);
		this.ReportButton.transform.SetLocalY(y);
		float num2 = 0.3f + (float)(num + 1) * 0.4f;
		this.Background.size = new Vector2(3.6f, num2);
		this.Background.GetComponent<BoxCollider2D>().size = new Vector2(3.6f, num2);
		this.Background.transform.localPosition = new Vector3(-0.4f, -num2 / 2f + 0.15f, 0.1f);
		ConsoleJoystick.SetMode_Menu();
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x0001BBA0 File Offset: 0x00019DA0
	public void Hide()
	{
		this.selectedClientId = -1;
		this.ContentParent.SetActive(false);
		for (int i = 0; i < this.allButtons.Count; i++)
		{
			Object.Destroy(this.allButtons[i].gameObject);
		}
		this.allButtons.Clear();
		ConsoleJoystick.SetMode_QuickChat();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0001BC0C File Offset: 0x00019E0C
	public void Select(int clientId)
	{
		if (VoteBanSystem.Instance.HasMyVote(clientId))
		{
			return;
		}
		this.selectedClientId = clientId;
		ClientData client = AmongUsClient.Instance.GetClient(clientId);
		if (client == null)
		{
			this.KickButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
			this.BanButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
			this.ReportButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
		}
		else
		{
			this.KickButton.GetComponent<ButtonRolloverHandler>().SetEnabledColors();
			this.BanButton.GetComponent<ButtonRolloverHandler>().SetEnabledColors();
			if (client.HasBeenReported)
			{
				this.ReportButton.GetComponent<ButtonRolloverHandler>().SetEnabledColors();
			}
			else
			{
				this.ReportButton.GetComponent<ButtonRolloverHandler>().SetDisabledColors();
			}
		}
		for (int i = 0; i < this.allButtons.Count; i++)
		{
			BanButton banButton = this.allButtons[i];
			if (banButton.TargetClientId != clientId)
			{
				banButton.Unselect();
			}
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001BCEC File Offset: 0x00019EEC
	public void Kick(bool ban)
	{
		if (this.selectedClientId >= 0)
		{
			if (AmongUsClient.Instance.CanBan())
			{
				AmongUsClient.Instance.KickPlayer(this.selectedClientId, ban);
				this.Hide();
			}
			else
			{
				VoteBanSystem.Instance.CmdAddVote(this.selectedClientId);
			}
		}
		this.Select(-1);
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0001BD40 File Offset: 0x00019F40
	public void PickReportReason()
	{
		ClientData client = AmongUsClient.Instance.GetClient(this.selectedClientId);
		if (client == null || client.HasBeenReported || !client.Character)
		{
			return;
		}
		GameData.PlayerInfo data = client.Character.Data;
		this.ReportReason.Show(((data != null) ? data.PlayerName : null) ?? "???", (data != null) ? data.ColorId : 0);
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0001BDB0 File Offset: 0x00019FB0
	public void ReportPlayer(ReportReasons reason)
	{
		ClientData client = AmongUsClient.Instance.GetClient(this.selectedClientId);
		if (client != null && !client.HasBeenReported)
		{
			AmongUsClient.Instance.ReportPlayer(this.selectedClientId, reason);
			this.Hide();
		}
		this.Select(-1);
	}

	// Token: 0x04000503 RID: 1283
	public BanButton BanButtonPrefab;

	// Token: 0x04000504 RID: 1284
	public SpriteRenderer Background;

	// Token: 0x04000505 RID: 1285
	public SpriteRenderer BanButton;

	// Token: 0x04000506 RID: 1286
	public SpriteRenderer KickButton;

	// Token: 0x04000507 RID: 1287
	public SpriteRenderer ReportButton;

	// Token: 0x04000508 RID: 1288
	public GameObject hotkeyGlyph;

	// Token: 0x04000509 RID: 1289
	public GameObject ContentParent;

	// Token: 0x0400050A RID: 1290
	public ReportReasonScreen ReportReason;

	// Token: 0x0400050B RID: 1291
	public int selectedClientId = -1;

	// Token: 0x0400050C RID: 1292
	[HideInInspector]
	public List<BanButton> allButtons = new List<BanButton>();

	// Token: 0x0400050D RID: 1293
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400050E RID: 1294
	public UiElement DefaultButtonSelected;

	// Token: 0x0400050F RID: 1295
	public List<UiElement> ControllerSelectable;
}
