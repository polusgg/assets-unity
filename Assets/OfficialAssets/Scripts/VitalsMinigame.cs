using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class VitalsMinigame : Minigame
{
	// Token: 0x06000807 RID: 2055 RVA: 0x000340EC File Offset: 0x000322EC
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//DeadBody[] source = Object.FindObjectsOfType<DeadBody>();
		//this.vitals = new VitalsPanel[GameData.Instance.AllPlayers.Count];
		//for (int i = 0; i < this.vitals.Length; i++)
		//{
		//	GameData.PlayerInfo player = GameData.Instance.AllPlayers[i];
		//	VitalsPanel vitalsPanel = Object.Instantiate<VitalsPanel>(this.PanelPrefab, base.transform);
		//	vitalsPanel.transform.localPosition = new Vector3((float)i * 0.6f + -2.7f, 0.2f, -1f);
		//	PlayerControl.SetPlayerMaterialColors(player.ColorId, vitalsPanel.PlayerImage);
		//	vitalsPanel.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(Palette.ShortColorNames[player.ColorId], Array.Empty<object>());
		//	if (player.Disconnected || (player.IsDead && !source.Any((DeadBody b) => b.ParentId == player.PlayerId)))
		//	{
		//		vitalsPanel.IsDiscon = true;
		//		vitalsPanel.Background.sprite = this.VitalBgDiscon;
		//		vitalsPanel.Cardio.gameObject.SetActive(false);
		//	}
		//	else if (player.IsDead)
		//	{
		//		vitalsPanel.IsDead = true;
		//		vitalsPanel.Background.sprite = this.VitalBgDead;
		//		vitalsPanel.Cardio.SetDead();
		//	}
		//	else
		//	{
		//		vitalsPanel.Cardio.Offset = IntRange.Next(0, 64);
		//		vitalsPanel.Cardio.beats = this.BeatRange.Next();
		//		vitalsPanel.Cardio.SetAlive();
		//	}
		//	this.vitals[i] = vitalsPanel;
		//}
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00034298 File Offset: 0x00032498
	private void Update()
	{
		if (this.SabText.isActiveAndEnabled && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.SabText.gameObject.SetActive(false);
			for (int i = 0; i < this.vitals.Length; i++)
			{
				this.vitals[i].gameObject.SetActive(true);
			}
		}
		else if (!this.SabText.isActiveAndEnabled && PlayerTask.PlayerHasTaskOfType<HudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.SabText.gameObject.SetActive(true);
			for (int j = 0; j < this.vitals.Length; j++)
			{
				this.vitals[j].gameObject.SetActive(false);
			}
		}
		for (int k = 0; k < this.vitals.Length; k++)
		{
			VitalsPanel vitalsPanel = this.vitals[k];
			GameData.PlayerInfo playerInfo = GameData.Instance.AllPlayers[k];
			if (playerInfo.Disconnected && !vitalsPanel.IsDiscon)
			{
				vitalsPanel.IsDead = false;
				vitalsPanel.IsDiscon = true;
				vitalsPanel.Background.sprite = this.VitalBgDiscon;
				vitalsPanel.Cardio.gameObject.SetActive(false);
			}
			else if (playerInfo.IsDead && !vitalsPanel.IsDead && !vitalsPanel.IsDiscon)
			{
				vitalsPanel.IsDiscon = false;
				vitalsPanel.IsDead = true;
				vitalsPanel.Background.sprite = this.VitalBgDead;
				vitalsPanel.Cardio.SetDead();
			}
		}
	}

	// Token: 0x04000966 RID: 2406
	public VitalsPanel PanelPrefab;

	// Token: 0x04000967 RID: 2407
	public Sprite VitalBgDead;

	// Token: 0x04000968 RID: 2408
	public Sprite VitalBgDiscon;

	// Token: 0x04000969 RID: 2409
	private VitalsPanel[] vitals;

	// Token: 0x0400096A RID: 2410
	public IntRange BeatRange;

	// Token: 0x0400096B RID: 2411
	public TextRenderer SabText;
}
