using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000121 RID: 289
public class PlayerTab : MonoBehaviour
{
	// Token: 0x06000707 RID: 1799 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
	public void OnEnable()
	{
		PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		this.HatImage.SetHat(SaveManager.LastHat, PlayerControl.LocalPlayer.Data.ColorId);
		PlayerControl.SetSkinImage(SaveManager.LastSkin, this.SkinImage);
		PlayerControl.SetPetImage(SaveManager.LastPet, PlayerControl.LocalPlayer.Data.ColorId, this.PetImage);
		float num = (float)Palette.PlayerColors.Length / 3f;
		for (int i = 0; i < Palette.PlayerColors.Length; i++)
		{
			float num2 = this.XRange.Lerp((float)(i % 3) / 2f);
			float num3 = this.YRange.Lerp(1f - (float)(i / 3) / num);
			ColorChip colorChip = Object.Instantiate<ColorChip>(this.ColorTabPrefab);
			colorChip.transform.SetParent(base.transform);
			colorChip.transform.localPosition = new Vector3(num2, num3, -1f);
			int j = i;
			colorChip.Button.OnClick.AddListener(delegate()
			{
				this.SelectColor(j);
			});
			colorChip.Inner.color = Palette.PlayerColors[i];
			this.ColorChips.Add(colorChip);
		}
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0002CC50 File Offset: 0x0002AE50
	public void OnDisable()
	{
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			Object.Destroy(this.ColorChips[i].gameObject);
		}
		this.ColorChips.Clear();
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0002CC94 File Offset: 0x0002AE94
	public void Update()
	{
		this.UpdateAvailableColors();
		for (int i = 0; i < this.ColorChips.Count; i++)
		{
			this.ColorChips[i].InUseForeground.SetActive(!this.AvailableColors.Contains(i));
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0002CCE2 File Offset: 0x0002AEE2
	private void SelectColor(int colorId)
	{
		this.UpdateAvailableColors();
		if (this.AvailableColors.Remove(colorId))
		{
			SaveManager.BodyColor = (byte)colorId;
			if (PlayerControl.LocalPlayer)
			{
				PlayerControl.LocalPlayer.CmdCheckColor((byte)colorId);
			}
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0002CD18 File Offset: 0x0002AF18
	public void UpdateAvailableColors()
	{
		PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer.Data.ColorId, this.DemoImage);
		PlayerControl.SetPetImage(SaveManager.LastPet, PlayerControl.LocalPlayer.Data.ColorId, this.PetImage);
		for (int i = 0; i < Palette.PlayerColors.Length; i++)
		{
			this.AvailableColors.Add(i);
		}
		if (GameData.Instance)
		{
			List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
			for (int j = 0; j < allPlayers.Count; j++)
			{
				GameData.PlayerInfo playerInfo = allPlayers[j];
				this.AvailableColors.Remove(playerInfo.ColorId);
			}
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0002CDBF File Offset: 0x0002AFBF
	public ColorChip GetDefaultSelectable()
	{
		return this.ColorChips[PlayerControl.LocalPlayer.Data.ColorId];
	}

	// Token: 0x040007E5 RID: 2021
	public ColorChip ColorTabPrefab;

	// Token: 0x040007E6 RID: 2022
	public SpriteRenderer DemoImage;

	// Token: 0x040007E7 RID: 2023
	public HatParent HatImage;

	// Token: 0x040007E8 RID: 2024
	public SpriteRenderer SkinImage;

	// Token: 0x040007E9 RID: 2025
	public SpriteRenderer PetImage;

	// Token: 0x040007EA RID: 2026
	public FloatRange XRange = new FloatRange(1.5f, 3f);

	// Token: 0x040007EB RID: 2027
	public FloatRange YRange = new FloatRange(-1f, -3f);

	// Token: 0x040007EC RID: 2028
	private HashSet<int> AvailableColors = new HashSet<int>();

	// Token: 0x040007ED RID: 2029
	[HideInInspector]
	public List<ColorChip> ColorChips = new List<ColorChip>();

	// Token: 0x040007EE RID: 2030
	private const int Columns = 3;
}
