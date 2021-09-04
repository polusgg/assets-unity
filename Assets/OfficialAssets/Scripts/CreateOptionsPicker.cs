using System;
using InnerNet;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class CreateOptionsPicker : MonoBehaviour
{
	// Token: 0x06000AC7 RID: 2759 RVA: 0x000440A0 File Offset: 0x000422A0
	public void Start()
	{
		for (int i = 0; i < this.MapButtons.Length; i++)
		{
			if (i < AmongUsClient.Instance.ShipPrefabs.Count)
			{
				this.MapButtons[i].gameObject.SetActive(true);
			}
			else
			{
				this.MapButtons[i].gameObject.SetActive(false);
			}
		}
		GameOptionsData targetOptions = this.GetTargetOptions();
		this.UpdateImpostorsButtons(targetOptions.NumImpostors);
		this.UpdateMaxPlayersButtons(targetOptions);
		this.UpdateLanguageButton((uint)targetOptions.Keywords);
		this.UpdateMapButtons((int)targetOptions.MapId);
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0004412C File Offset: 0x0004232C
	public GameOptionsData GetTargetOptions()
	{
		if (this.mode == SettingsMode.Host)
		{
			return SaveManager.GameHostOptions;
		}
		GameOptionsData gameSearchOptions = SaveManager.GameSearchOptions;
		if (gameSearchOptions.MapId == 0)
		{
			gameSearchOptions.ToggleMapFilter(0);
			SaveManager.GameSearchOptions = gameSearchOptions;
		}
		return gameSearchOptions;
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00044163 File Offset: 0x00042363
	private void SetTargetOptions(GameOptionsData data)
	{
		if (this.mode == SettingsMode.Host)
		{
			SaveManager.GameHostOptions = data;
			return;
		}
		SaveManager.GameSearchOptions = data;
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0004417C File Offset: 0x0004237C
	public void SetMaxPlayersButtons(int maxPlayers)
	{
		GameOptionsData targetOptions = this.GetTargetOptions();
		if (maxPlayers < GameOptionsData.MinPlayers[targetOptions.NumImpostors])
		{
			return;
		}
		targetOptions.MaxPlayers = maxPlayers;
		this.SetTargetOptions(targetOptions);
		if (DestroyableSingleton<FindAGameManager>.InstanceExists)
		{
			DestroyableSingleton<FindAGameManager>.Instance.ResetTimer();
		}
		this.UpdateMaxPlayersButtons(targetOptions);
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x000441C8 File Offset: 0x000423C8
	private void UpdateMaxPlayersButtons(GameOptionsData opts)
	{
		if (this.CrewArea)
		{
			this.CrewArea.SetCrewSize(opts.MaxPlayers, opts.NumImpostors);
		}
		for (int i = 0; i < this.MaxPlayerButtons.Length; i++)
		{
			SpriteRenderer spriteRenderer = this.MaxPlayerButtons[i];
			spriteRenderer.enabled = (spriteRenderer.name == opts.MaxPlayers.ToString());
			spriteRenderer.GetComponentInChildren<TextRenderer>().Color = ((int.Parse(spriteRenderer.name) < GameOptionsData.MinPlayers[opts.NumImpostors]) ? Palette.DisabledGrey : Color.white);
		}
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00044264 File Offset: 0x00042464
	public void SetImpostorButtons(int numImpostors)
	{
		GameOptionsData targetOptions = this.GetTargetOptions();
		targetOptions.NumImpostors = numImpostors;
		this.SetTargetOptions(targetOptions);
		this.SetMaxPlayersButtons(Mathf.Max(targetOptions.MaxPlayers, GameOptionsData.MinPlayers[numImpostors]));
		this.UpdateImpostorsButtons(numImpostors);
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x000442A8 File Offset: 0x000424A8
	private void UpdateImpostorsButtons(int numImpostors)
	{
		for (int i = 0; i < this.ImpostorButtons.Length; i++)
		{
			SpriteRenderer spriteRenderer = this.ImpostorButtons[i];
			spriteRenderer.enabled = (spriteRenderer.name == numImpostors.ToString());
		}
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x000442E8 File Offset: 0x000424E8
	public void SetMap(int mapid)
	{
		GameOptionsData targetOptions = this.GetTargetOptions();
		if (this.mode == SettingsMode.Host)
		{
			targetOptions.MapId = (byte)mapid;
		}
		else
		{
			targetOptions.ToggleMapFilter((byte)mapid);
		}
		this.SetTargetOptions(targetOptions);
		if (DestroyableSingleton<FindAGameManager>.InstanceExists)
		{
			DestroyableSingleton<FindAGameManager>.Instance.ResetTimer();
		}
		this.UpdateMapButtons(mapid);
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00044338 File Offset: 0x00042538
	private void UpdateMapButtons(int mapid)
	{
		if (this.mode == SettingsMode.Host)
		{
			if (this.CrewArea)
			{
				this.CrewArea.SetMap(mapid);
			}
			for (int i = 0; i < this.MapButtons.Length; i++)
			{
				SpriteRenderer spriteRenderer = this.MapButtons[i];
				spriteRenderer.color = ((spriteRenderer.name == mapid.ToString()) ? Color.white : Palette.Black);
			}
		}
		else
		{
			GameOptionsData targetOptions = this.GetTargetOptions();
			for (int j = 0; j < this.MapButtons.Length; j++)
			{
				SpriteRenderer spriteRenderer2 = this.MapButtons[j];
				spriteRenderer2.color = (targetOptions.FilterContainsMap(byte.Parse(spriteRenderer2.name)) ? Color.white : Palette.DisabledGrey);
			}
		}
		if (Constants.ShouldFlipSkeld())
		{
			this.MapButtons[0].flipX = true;
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00044408 File Offset: 0x00042608
	public void SetLanguageFilter(uint keyword)
	{
		GameOptionsData targetOptions = this.GetTargetOptions();
		targetOptions.Keywords = (GameKeywords)keyword;
		this.SetTargetOptions(targetOptions);
		if (DestroyableSingleton<FindAGameManager>.InstanceExists)
		{
			DestroyableSingleton<FindAGameManager>.Instance.ResetTimer();
		}
		this.UpdateLanguageButton(keyword);
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x00044444 File Offset: 0x00042644
	private void UpdateLanguageButton(uint flag)
	{
		if (ChatLanguageSet.Instance.GetString(flag) == "Other")
		{
			this.LanguageButton.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.OtherLanguage, Array.Empty<object>());
			return;
		}
		this.LanguageButton.Text = ChatLanguageSet.Instance.GetString(flag);
	}

	// Token: 0x04000C1A RID: 3098
	public SpriteRenderer[] MaxPlayerButtons;

	// Token: 0x04000C1B RID: 3099
	public SpriteRenderer[] ImpostorButtons;

	// Token: 0x04000C1C RID: 3100
	public TextRenderer LanguageButton;

	// Token: 0x04000C1D RID: 3101
	public SpriteRenderer[] MapButtons;

	// Token: 0x04000C1E RID: 3102
	public SettingsMode mode;

	// Token: 0x04000C1F RID: 3103
	public CrewVisualizer CrewArea;
}
