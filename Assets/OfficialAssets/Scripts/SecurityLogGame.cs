using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class SecurityLogGame : Minigame
{
	// Token: 0x060003F3 RID: 1011 RVA: 0x0001A357 File Offset: 0x00018557
	public void Awake()
	{
		base.SetupInput(false);
		this.Logger = ShipStatus.Instance.GetComponent<SecurityLogBehaviour>();
		if (!PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.RefreshScreen();
			return;
		}
		this.SabText.gameObject.SetActive(true);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0001A394 File Offset: 0x00018594
	public void Update()
	{
		if (!PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			if (this.SabText.isActiveAndEnabled)
			{
				this.SabText.gameObject.SetActive(false);
				this.RefreshScreen();
				return;
			}
			if (this.Logger.HasNew)
			{
				this.Logger.HasNew = false;
				this.RefreshScreen();
				return;
			}
		}
		else if (!this.SabText.isActiveAndEnabled)
		{
			this.EntryPool.ReclaimAll();
			this.SabText.gameObject.SetActive(true);
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0001A41C File Offset: 0x0001861C
	private void RefreshScreen()
	{
		this.EntryPool.ReclaimAll();
		int num = 0;
		for (int i = 0; i < this.Logger.LogEntries.Count; i++)
		{
			SecurityLogBehaviour.SecurityLogEntry securityLogEntry = this.Logger.LogEntries[i];
			LogEntryBubble logEntryBubble = this.EntryPool.Get<LogEntryBubble>();
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(securityLogEntry.PlayerId);
			if (playerById == null)
			{
				Debug.Log(string.Format("Couldn't find player {0} for log", securityLogEntry.PlayerId));
			}
			else
			{
				PlayerControl.SetPlayerMaterialColors(playerById.ColorId, logEntryBubble.HeadImage);
				string @string = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.LogNorth + (int)securityLogEntry.Location, Array.Empty<object>());
				string text = logEntryBubble.Text.Text;
				logEntryBubble.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SecLogEntry, new object[]
				{
					playerById.PlayerName,
					@string
				});
				if (!logEntryBubble.Text.Text.Equals(text))
				{
					logEntryBubble.Text.RefreshMesh();
				}
				logEntryBubble.Background.sprite = this.LocationBgs[(int)((byte)securityLogEntry.Location)];
				logEntryBubble.transform.localPosition = new Vector3(0f, (float)num * -this.EntryHeight, 0f);
				num++;
			}
		}
		float max = Mathf.Max(0f, (float)num * this.EntryHeight - this.ScreenHeight);
		float scrollPercY = this.scroller.GetScrollPercY();
		this.scroller.YBounds = new FloatRange(0f, max);
		if (scrollPercY > 0.95f)
		{
			this.scroller.ScrollPercentY(1f);
		}
	}

	// Token: 0x040004A3 RID: 1187
	private SecurityLogBehaviour Logger;

	// Token: 0x040004A4 RID: 1188
	public ObjectPoolBehavior EntryPool;

	// Token: 0x040004A5 RID: 1189
	public Scroller scroller;

	// Token: 0x040004A6 RID: 1190
	public float ScreenHeight = 4f;

	// Token: 0x040004A7 RID: 1191
	public float EntryHeight = 0.4f;

	// Token: 0x040004A8 RID: 1192
	public Sprite[] LocationBgs;

	// Token: 0x040004A9 RID: 1193
	public TextRenderer SabText;
}
