using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using InnerNet;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public static class SaveManager
{
	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0004277B File Offset: 0x0004097B
	// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00042787 File Offset: 0x00040987
	public static Announcement LastAnnouncement
	{
		get
		{
			SaveManager.LoadAnnouncement();
			return SaveManager.lastAnnounce;
		}
		set
		{
			SaveManager.lastAnnounce = value;
			SaveManager.SaveAnnouncement(false);
		}
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00042795 File Offset: 0x00040995
	private static string GetPrefsName()
	{
		if (!string.IsNullOrEmpty(SaveManager.epicAccountId))
		{
			return SaveManager.epicAccountId + "_playerPrefs";
		}
		return "playerPrefs";
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000427B8 File Offset: 0x000409B8
	public static bool BoughtNoAds
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x000427BB File Offset: 0x000409BB
	public static bool GetPurchase(string key)
	{
		SaveManager.LoadSecureData();
		return SaveManager.purchases.Contains(key);
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x000427CD File Offset: 0x000409CD
	public static void ClearPurchased(string key)
	{
		SaveManager.LoadSecureData();
		SaveManager.purchases.Remove(key);
		SaveManager.SaveSecureData(false);
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x000427E6 File Offset: 0x000409E6
	public static void SetPurchased(string key)
	{
		SaveManager.LoadSecureData();
		SaveManager.purchases.Add(key ?? "null");
		if (key == "bought_ads")
		{
			SaveManager.ShowAdsScreen = ShowAdsState.Purchased;
		}
		SaveManager.SaveSecureData(false);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0004281C File Offset: 0x00040A1C
	public static void SaveLocalDoB(int year, int mo, int day)
	{
		SaveManager.LoadSecureData();
		SaveManager.dobInfo = string.Concat(new string[]
		{
			year.ToString(),
			"-",
			mo.ToString().PadLeft(2, '0'),
			"-",
			day.ToString().PadLeft(2, '0')
		});
		string item = "";
		foreach (string text in SaveManager.purchases)
		{
			if (text.Split(new char[]
			{
				'-'
			}).Length == 3)
			{
				item = text;
			}
		}
		SaveManager.purchases.Remove(item);
		SaveManager.purchases.Add(SaveManager.dobInfo);
		SaveManager.SaveSecureData(false);
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x000428FC File Offset: 0x00040AFC
	public static bool GetLocalDoB()
	{
		SaveManager.LoadSecureData();
		if (string.IsNullOrEmpty(SaveManager.dobInfo))
		{
			return false;
		}
		string[] array = SaveManager.dobInfo.Split(new char[]
		{
			'-'
		});
		if (array.Length != 3)
		{
			return false;
		}
		int num;
		if (!int.TryParse(array[0], out num))
		{
			return false;
		}
		int num2;
		if (!int.TryParse(array[1], out num2))
		{
			return false;
		}
		int num3;
		if (!int.TryParse(array[2], out num3))
		{
			return false;
		}
		SaveManager.BirthDateYear = num;
		SaveManager.BirthDateMonth = num2;
		SaveManager.BirthDateDay = num3;
		return true;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00042978 File Offset: 0x00040B78
	private static void LoadSecureData()
	{
		if (!SaveManager.purchaseFile.Loaded)
		{
			try
			{
				SaveManager.purchaseFile.LoadData(delegate(BinaryReader reader)
				{
					while (reader.BaseStream.Position < reader.BaseStream.Length)
					{
						string text = reader.ReadString();
						if (text.Split(new char[]
						{
							'-'
						}).Length == 3)
						{
							SaveManager.dobInfo = text;
						}
						else
						{
							SaveManager.purchases.Add(text);
						}
					}
				});
			}
			catch (NullReferenceException)
			{
			}
			catch (Exception ex)
			{
				string str = "Deleted corrupt secure file outer: ";
				Exception ex2 = ex;
				Debug.Log(str + ((ex2 != null) ? ex2.ToString() : null));
				SaveManager.purchaseFile.Delete();
			}
		}
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00042A04 File Offset: 0x00040C04
	public static void SaveSecureData(bool saveNow = false)
	{
		SaveManager.purchaseFile.SaveData(new object[]
		{
			SaveManager.purchases
		});
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00042A1E File Offset: 0x00040C1E
	// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00042A2B File Offset: 0x00040C2B
	public static bool VSync
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.vsync;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.vsync = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00042A3F File Offset: 0x00040C3F
	// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00042A59 File Offset: 0x00040C59
	public static QuickChatModes ChatModeType
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			if (SaveManager.chatModeType == 0)
			{
				SaveManager.chatModeType = 1;
			}
			return (QuickChatModes)SaveManager.chatModeType;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.chatModeType = (int)value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00042A6D File Offset: 0x00040C6D
	// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00042A7A File Offset: 0x00040C7A
	public static bool CensorChat
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.censorChat;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.censorChat = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00042A8E File Offset: 0x00040C8E
	// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00042A9B File Offset: 0x00040C9B
	public static ShowAdsState ShowAdsScreen
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return (ShowAdsState)SaveManager.showAdsScreen;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.showAdsScreen = (byte)value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00042AAF File Offset: 0x00040CAF
	// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00042ABC File Offset: 0x00040CBC
	public static int AcceptedPrivacyPolicy
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.privacyPolicyVersion;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.privacyPolicyVersion = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00042AD0 File Offset: 0x00040CD0
	// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00042ADD File Offset: 0x00040CDD
	public static bool IsGuest
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.isGuest;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.isGuest = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00042AF1 File Offset: 0x00040CF1
	// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00042AFE File Offset: 0x00040CFE
	public static bool HasLoggedIn
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.hasLoggedIn;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.hasLoggedIn = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00042B12 File Offset: 0x00040D12
	// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00042B1F File Offset: 0x00040D1F
	public static int BirthDateMonth
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.birthDateMonth;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.birthDateMonth = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00042B33 File Offset: 0x00040D33
	// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00042B40 File Offset: 0x00040D40
	public static int BirthDateDay
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.birthDateDay;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.birthDateDay = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00042B54 File Offset: 0x00040D54
	// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00042B61 File Offset: 0x00040D61
	public static int BirthDateYear
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.birthDateYear;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.birthDateYear = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00042B75 File Offset: 0x00040D75
	// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00042B82 File Offset: 0x00040D82
	public static string BirthDateSetDate
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.birthDateSetDate;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.birthDateSetDate = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00042B96 File Offset: 0x00040D96
	// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00042BA3 File Offset: 0x00040DA3
	public static string AuthenticatedEpicId
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.epicAccountId;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.epicAccountId = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00042BB7 File Offset: 0x00040DB7
	// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00042BC4 File Offset: 0x00040DC4
	public static bool ShowMinPlayerWarning
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.showMinPlayerWarning;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.showMinPlayerWarning = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00042BD8 File Offset: 0x00040DD8
	// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00042BE5 File Offset: 0x00040DE5
	public static bool ShowOnlineHelp
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.showOnlineHelp;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.showOnlineHelp = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00042BF9 File Offset: 0x00040DF9
	// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00042C0D File Offset: 0x00040E0D
	public static float SfxVolume
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return (float)SaveManager.sfxVolume / 255f;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.sfxVolume = (byte)(value * 255f);
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00042C28 File Offset: 0x00040E28
	// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00042C3C File Offset: 0x00040E3C
	public static float MusicVolume
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return (float)SaveManager.musicVolume / 255f;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.musicVolume = (byte)(value * 255f);
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00042C57 File Offset: 0x00040E57
	// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00042C64 File Offset: 0x00040E64
	public static int ControlMode
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.touchConfig;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.touchConfig = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00042C78 File Offset: 0x00040E78
	// (set) Token: 0x06000A7F RID: 2687 RVA: 0x00042C85 File Offset: 0x00040E85
	public static float JoystickSize
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.joyStickSize;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.joyStickSize = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00042C99 File Offset: 0x00040E99
	private static string GetDefaultName()
	{
		return DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EnterName, Array.Empty<object>());
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00042CB0 File Offset: 0x00040EB0
	// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00042CF6 File Offset: 0x00040EF6
	public static string PlayerName
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			string text = string.IsNullOrWhiteSpace(SaveManager.lastPlayerName) ? DestroyableSingleton<AccountManager>.Instance.GetRandomName() : SaveManager.lastPlayerName;
			if (text.Length > 10)
			{
				return text.Substring(0, 10);
			}
			return text;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.lastPlayerName = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00042D0A File Offset: 0x00040F0A
	// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00042D17 File Offset: 0x00040F17
	public static string GuardianEmail
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.guardianEmail;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.guardianEmail = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00042D2B File Offset: 0x00040F2B
	// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00042D38 File Offset: 0x00040F38
	public static uint LastPet
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.lastPet;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.lastPet = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00042D4C File Offset: 0x00040F4C
	// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00042D59 File Offset: 0x00040F59
	public static uint LastHat
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.lastHat;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.lastHat = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00042D6D File Offset: 0x00040F6D
	// (set) Token: 0x06000A8A RID: 2698 RVA: 0x00042D7A File Offset: 0x00040F7A
	public static uint LastSkin
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return SaveManager.lastSkin;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.lastSkin = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00042D8E File Offset: 0x00040F8E
	// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00042DAE File Offset: 0x00040FAE
	public static uint LastLanguage
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			if (SaveManager.lastLanguage > 13U)
			{
				SaveManager.lastLanguage = TranslationController.SelectDefaultLanguage();
			}
			return SaveManager.lastLanguage;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.lastLanguage = value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00042DC2 File Offset: 0x00040FC2
	// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00042DD0 File Offset: 0x00040FD0
	public static byte BodyColor
	{
		get
		{
			SaveManager.LoadPlayerPrefs(false);
			return (byte)SaveManager.colorConfig;
		}
		set
		{
			SaveManager.LoadPlayerPrefs(false);
			SaveManager.colorConfig = (uint)value;
			SaveManager.SavePlayerPrefs(false);
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00042DE4 File Offset: 0x00040FE4
	// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00042E42 File Offset: 0x00041042
	public static GameOptionsData GameHostOptions
	{
		get
		{
			if (SaveManager.hostOptionsData == null)
			{
				SaveManager.hostOptionsData = SaveManager.LoadGameOptions("gameHostOptions");
			}
			SaveManager.hostOptionsData.NumImpostors = Mathf.Clamp(SaveManager.hostOptionsData.NumImpostors, 1, 3);
			SaveManager.hostOptionsData.KillDistance = Mathf.Clamp(SaveManager.hostOptionsData.KillDistance, 0, 2);
			return SaveManager.hostOptionsData;
		}
		set
		{
			SaveManager.hostOptionsData = value;
			SaveManager.SaveGameOptions(SaveManager.hostOptionsData, "gameHostOptions", false);
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00042E5A File Offset: 0x0004105A
	// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00042E77 File Offset: 0x00041077
	public static GameOptionsData GameSearchOptions
	{
		get
		{
			if (SaveManager.searchOptionsData == null)
			{
				SaveManager.searchOptionsData = SaveManager.LoadGameOptions("gameSearchOptions");
			}
			return SaveManager.searchOptionsData;
		}
		set
		{
			SaveManager.searchOptionsData = value;
			SaveManager.SaveGameOptions(SaveManager.searchOptionsData, "gameSearchOptions", false);
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00042E90 File Offset: 0x00041090
	private static GameOptionsData LoadGameOptions(string filename)
	{
		string path = Path.Combine(PlatformPaths.persistentDataPath, filename);
		if (File.Exists(path))
		{
			using (FileStream fileStream = File.OpenRead(path))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					return GameOptionsData.Deserialize(binaryReader) ?? new GameOptionsData();
				}
			}
		}
		return new GameOptionsData();
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x00042F08 File Offset: 0x00041108
	public static void SaveGameOptions(GameOptionsData data, string filename, bool saveNow = false)
	{
		string path = Path.Combine(PlatformPaths.persistentDataPath, filename);
		Debug.Log("SaveGameOptions");
		FileIO.WriteAllBytes(path, data.ToBytes(4));
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00042F2C File Offset: 0x0004112C
	private static void LoadAnnouncement()
	{
		if (SaveManager.loadedAnnounce)
		{
			return;
		}
		SaveManager.loadedAnnounce = true;
		string path = Path.Combine(PlatformPaths.persistentDataPath, "announcement");
		if (FileIO.Exists(path))
		{
			string[] array = FileIO.ReadAllText(path).Split(new char[1]);
			if (array.Length == 3)
			{
				Announcement announcement = default(Announcement);
				SaveManager.TryGetUint(array, 0, out announcement.Id, 0U);
				announcement.AnnounceText = array[1];
				SaveManager.TryGetDateTime(array, 2, out announcement.DateFetched);
				SaveManager.lastAnnounce = announcement;
				return;
			}
			SaveManager.lastAnnounce = default(Announcement);
		}
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x00042FB8 File Offset: 0x000411B8
	public static void SaveAnnouncement(bool saveNow = false)
	{
		string path = Path.Combine(PlatformPaths.persistentDataPath, "announcement");
		Debug.Log("SaveAnnouncements");
		FileIO.WriteAllText(path, string.Join("\0", new object[]
		{
			SaveManager.lastAnnounce.Id,
			SaveManager.lastAnnounce.AnnounceText,
			SaveManager.lastAnnounce.DateFetched.ToString(DateTimeFormatInfo.InvariantInfo)
		}));
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0004302C File Offset: 0x0004122C
	public static void LoadPlayerPrefs(bool overrideLoad = false)
	{
		if (!overrideLoad && SaveManager.loaded)
		{
			return;
		}
		SaveManager.loaded = true;
		string path = Path.Combine(PlatformPaths.persistentDataPath, SaveManager.GetPrefsName());
		if (FileIO.Exists(path))
		{
			string[] array = FileIO.ReadAllText(path).Split(new char[]
			{
				','
			});
			SaveManager.lastPlayerName = array[0];
			if (array.Length > 1)
			{
				int.TryParse(array[1], out SaveManager.touchConfig);
			}
			if (array.Length <= 2 || !uint.TryParse(array[2], out SaveManager.colorConfig))
			{
				SaveManager.colorConfig = (uint)Palette.PlayerColors.RandomIdx<Color32>();
			}
			SaveManager.TryGetBool(array, 8, out SaveManager.showMinPlayerWarning, false);
			SaveManager.TryGetBool(array, 9, out SaveManager.showOnlineHelp, false);
			SaveManager.TryGetUint(array, 10, out SaveManager.lastHat, 0U);
			SaveManager.TryGetByte(array, 11, out SaveManager.sfxVolume);
			SaveManager.TryGetByte(array, 12, out SaveManager.musicVolume);
			SaveManager.TryGetFloat(array, 13, out SaveManager.joyStickSize, 1f);
			SaveManager.TryGetUint(array, 15, out SaveManager.lastSkin, 0U);
			SaveManager.TryGetUint(array, 16, out SaveManager.lastPet, 0U);
			SaveManager.TryGetBool(array, 17, out SaveManager.censorChat, true);
			SaveManager.TryGetUint(array, 18, out SaveManager.lastLanguage, uint.MaxValue);
			SaveManager.TryGetBool(array, 19, out SaveManager.vsync, false);
			SaveManager.TryGetByte(array, 20, out SaveManager.showAdsScreen);
			SaveManager.TryGetInt(array, 21, out SaveManager.privacyPolicyVersion);
			if (array.Length > 22)
			{
				SaveManager.epicAccountId = array[22];
			}
			if (array.Length > 25)
			{
				SaveManager.TryGetInt(array, 23, out SaveManager.birthDateMonth);
				SaveManager.TryGetInt(array, 24, out SaveManager.birthDateDay);
				SaveManager.TryGetInt(array, 25, out SaveManager.birthDateYear);
			}
			if (array.Length > 26)
			{
				SaveManager.birthDateSetDate = array[26];
			}
			SaveManager.TryGetInt(array, 27, out SaveManager.chatModeType);
			SaveManager.TryGetBool(array, 28, out SaveManager.isGuest, false);
			if (array.Length > 29)
			{
				SaveManager.guardianEmail = array[29];
			}
			SaveManager.TryGetBool(array, 30, out SaveManager.hasLoggedIn, false);
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x000431F8 File Offset: 0x000413F8
	public static void SavePlayerPrefs(bool saveNow = false)
	{
		SaveManager.LoadPlayerPrefs(false);
		string path = Path.Combine(PlatformPaths.persistentDataPath, SaveManager.GetPrefsName());
		try
		{
			FileIO.WriteAllText(path, string.Join(",", new object[]
			{
				SaveManager.lastPlayerName ?? string.Empty,
				SaveManager.touchConfig,
				SaveManager.colorConfig,
				1,
				false,
				false,
				false,
				0,
				SaveManager.showMinPlayerWarning,
				SaveManager.showOnlineHelp,
				SaveManager.lastHat,
				SaveManager.sfxVolume,
				SaveManager.musicVolume,
				SaveManager.joyStickSize.ToString(CultureInfo.InvariantCulture),
				0L,
				SaveManager.lastSkin,
				SaveManager.lastPet,
				SaveManager.censorChat,
				SaveManager.lastLanguage,
				SaveManager.vsync,
				SaveManager.showAdsScreen,
				SaveManager.privacyPolicyVersion,
				SaveManager.epicAccountId,
				SaveManager.birthDateMonth,
				SaveManager.birthDateDay,
				SaveManager.birthDateYear,
				SaveManager.birthDateSetDate,
				SaveManager.chatModeType,
				SaveManager.isGuest,
				SaveManager.guardianEmail,
				SaveManager.hasLoggedIn
			}));
		}
		catch (IOException ex)
		{
			Debug.LogError(ex.Message);
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06000A99 RID: 2713 RVA: 0x000433E8 File Offset: 0x000415E8
	// (set) Token: 0x06000A9A RID: 2714 RVA: 0x000433F4 File Offset: 0x000415F4
	public static string[] QuickChatFavorites
	{
		get
		{
			SaveManager.LoadQuickChatFavorites();
			return SaveManager.quickChatFavorites;
		}
		set
		{
			SaveManager.quickChatFavorites = value;
			SaveManager.SaveQuickChatFavorites(false);
		}
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x00043404 File Offset: 0x00041604
	public static void LoadQuickChatFavorites()
	{
		if (SaveManager.loadedQCFavorites)
		{
			return;
		}
		SaveManager.loadedQCFavorites = true;
		string path = Path.Combine(PlatformPaths.persistentDataPath, "quickChatFavorites");
		if (FileIO.Exists(path))
		{
			SaveManager.quickChatFavorites = FileIO.ReadAllText(path).Split(new char[]
			{
				'\n'
			});
			return;
		}
		if (SaveManager.quickChatFavorites == null)
		{
			SaveManager.quickChatFavorites = new string[20];
		}
		if (SaveManager.quickChatFavorites[0] == null)
		{
			for (int i = 0; i < 20; i++)
			{
				SaveManager.quickChatFavorites[i] = "";
			}
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00043488 File Offset: 0x00041688
	public static void SaveQuickChatFavorites(bool saveNow = false)
	{
		Debug.LogError("SaveQuickChatFavorites");
		FileIO.WriteAllText(Path.Combine(PlatformPaths.persistentDataPath, "quickChatFavorites"), string.Join("\n", SaveManager.quickChatFavorites));
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x000434B7 File Offset: 0x000416B7
	private static void TryGetBool(string[] parts, int index, out bool value, bool @default = false)
	{
		value = @default;
		if (index < parts.Length)
		{
			bool.TryParse(parts[index], out value);
		}
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000434CC File Offset: 0x000416CC
	private static void TryGetByte(string[] parts, int index, out byte value)
	{
		value = 0;
		if (index < parts.Length)
		{
			byte.TryParse(parts[index], out value);
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x000434E1 File Offset: 0x000416E1
	private static void TryGetFloat(string[] parts, int index, out float value, float @default = 0f)
	{
		value = @default;
		if (index < parts.Length)
		{
			float.TryParse(parts[index], NumberStyles.Number, CultureInfo.InvariantCulture, out value);
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x000434FD File Offset: 0x000416FD
	private static void TryGetDateTime(string[] parts, int index, out DateTime value)
	{
		value = default(DateTime);
		if (index < parts.Length)
		{
			DateTime.TryParse(parts[index], DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeUniversal, out value);
		}
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0004351D File Offset: 0x0004171D
	private static void TryGetInt(string[] parts, int index, out int value)
	{
		value = 0;
		if (index < parts.Length)
		{
			int.TryParse(parts[index], out value);
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00043532 File Offset: 0x00041732
	private static void TryGetUint(string[] parts, int index, out uint value, uint @default = 0U)
	{
		value = @default;
		if (index < parts.Length)
		{
			uint.TryParse(parts[index], out value);
		}
	}

	// Token: 0x04000BD5 RID: 3029
	private static bool loaded;

	// Token: 0x04000BD6 RID: 3030
	private static bool loadedStats;

	// Token: 0x04000BD7 RID: 3031
	private static bool loadedAnnounce;

	// Token: 0x04000BD8 RID: 3032
	private static bool loadedQCFavorites;

	// Token: 0x04000BD9 RID: 3033
	private static string lastPlayerName;

	// Token: 0x04000BDA RID: 3034
	private static byte sfxVolume = byte.MaxValue;

	// Token: 0x04000BDB RID: 3035
	private static byte musicVolume = byte.MaxValue;

	// Token: 0x04000BDC RID: 3036
	private static bool showMinPlayerWarning = true;

	// Token: 0x04000BDD RID: 3037
	private static bool showOnlineHelp = true;

	// Token: 0x04000BDE RID: 3038
	private static byte showAdsScreen = 0;

	// Token: 0x04000BDF RID: 3039
	private static int privacyPolicyVersion = 0;

	// Token: 0x04000BE0 RID: 3040
	private static int birthDateDay = 1;

	// Token: 0x04000BE1 RID: 3041
	private static int birthDateMonth = 1;

	// Token: 0x04000BE2 RID: 3042
	private static int birthDateYear = 2021;

	// Token: 0x04000BE3 RID: 3043
	private static string birthDateSetDate = "";

	// Token: 0x04000BE4 RID: 3044
	private static string epicAccountId = "";

	// Token: 0x04000BE5 RID: 3045
	private static bool vsync = false;

	// Token: 0x04000BE6 RID: 3046
	private static bool censorChat = true;

	// Token: 0x04000BE7 RID: 3047
	private static int chatModeType = 0;

	// Token: 0x04000BE8 RID: 3048
	private static bool isGuest = false;

	// Token: 0x04000BE9 RID: 3049
	private static bool hasLoggedIn = false;

	// Token: 0x04000BEA RID: 3050
	private static string guardianEmail = "";

	// Token: 0x04000BEB RID: 3051
	private static int touchConfig = 1;

	// Token: 0x04000BEC RID: 3052
	private static float joyStickSize = 1f;

	// Token: 0x04000BED RID: 3053
	private static uint colorConfig;

	// Token: 0x04000BEE RID: 3054
	private static uint lastPet;

	// Token: 0x04000BEF RID: 3055
	private static uint lastHat;

	// Token: 0x04000BF0 RID: 3056
	private static uint lastSkin;

	// Token: 0x04000BF1 RID: 3057
	private static uint lastLanguage = uint.MaxValue;

	// Token: 0x04000BF2 RID: 3058
	private static GameOptionsData hostOptionsData;

	// Token: 0x04000BF3 RID: 3059
	private static GameOptionsData searchOptionsData;

	// Token: 0x04000BF4 RID: 3060
	private static Announcement lastAnnounce;

	// Token: 0x04000BF5 RID: 3061
	public const int quickChatFavoriteSlots = 20;

	// Token: 0x04000BF6 RID: 3062
	private static string[] quickChatFavorites = new string[20];

	// Token: 0x04000BF7 RID: 3063
	private static SecureDataFile purchaseFile = new SecureDataFile(Path.Combine(PlatformPaths.persistentDataPath, "secureNew"));

	// Token: 0x04000BF8 RID: 3064
	private static HashSet<string> purchases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

	// Token: 0x04000BF9 RID: 3065
	private static string dobInfo = "";
}
