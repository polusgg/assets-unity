using System;
using System.IO;
using System.Text;
using Hazel;
using InnerNet;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class GameOptionsData
{
	// Token: 0x06000AD3 RID: 2771 RVA: 0x000444A8 File Offset: 0x000426A8
	public void ToggleMapFilter(byte newId)
	{
		byte b = (byte)(((int)this.MapId ^ 1 << (int)newId) & 23);
		if (b != 0)
		{
			this.MapId = b;
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x000444D4 File Offset: 0x000426D4
	public bool FilterContainsMap(byte newId)
	{
		int num = 1 << (int)newId;
		return ((int)this.MapId & num) == num;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x000444F4 File Offset: 0x000426F4
	public GameOptionsData()
	{
		//try
		//{
		//	SystemLanguage systemLanguage = Application.systemLanguage;
		//	if (systemLanguage <= 23)
		//	{
		//		if (systemLanguage == 1)
		//		{
		//			this.Keywords = GameKeywords.Arabic;
		//			goto IL_128;
		//		}
		//		if (systemLanguage == 23)
		//		{
		//			this.Keywords = GameKeywords.Korean;
		//			goto IL_128;
		//		}
		//	}
		//	else
		//	{
		//		switch (systemLanguage)
		//		{
		//		case 27:
		//			this.Keywords = GameKeywords.Polish;
		//			goto IL_128;
		//		case 28:
		//			this.Keywords = GameKeywords.Portuguese;
		//			goto IL_128;
		//		case 29:
		//			break;
		//		case 30:
		//			this.Keywords = GameKeywords.Russian;
		//			goto IL_128;
		//		default:
		//			if (systemLanguage == 34)
		//			{
		//				this.Keywords = GameKeywords.Spanish;
		//				goto IL_128;
		//			}
		//			break;
		//		}
		//	}
		//	this.Keywords = GameKeywords.Other;
		//	IL_128:;
		//}
		//catch
		//{
		//}
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00044640 File Offset: 0x00042840
	public void SetRecommendations(int numPlayers, GameModes modes)
	{
		numPlayers = Mathf.Clamp(numPlayers, 4, 10);
		this.PlayerSpeedMod = 1f;
		this.CrewLightMod = 1f;
		this.ImpostorLightMod = 1.5f;
		this.KillCooldown = (float)GameOptionsData.RecommendedKillCooldown[numPlayers];
		this.NumCommonTasks = 1;
		this.NumLongTasks = 1;
		this.NumShortTasks = 2;
		this.NumEmergencyMeetings = 1;
		if (modes != GameModes.OnlineGame)
		{
			this.NumImpostors = GameOptionsData.RecommendedImpostors[numPlayers];
		}
		this.KillDistance = 1;
		this.DiscussionTime = 15;
		this.VotingTime = 120;
		this.isDefaults = true;
		this.ConfirmImpostor = true;
		this.VisualTasks = true;
		this.EmergencyCooldown = ((modes == GameModes.OnlineGame) ? 15 : 0);
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x000446F0 File Offset: 0x000428F0
	public void Serialize(BinaryWriter writer, byte version)
	{
		writer.Write(version);
		writer.Write((byte)this.MaxPlayers);
		writer.Write((uint)this.Keywords);
		writer.Write(this.MapId);
		writer.Write(this.PlayerSpeedMod);
		writer.Write(this.CrewLightMod);
		writer.Write(this.ImpostorLightMod);
		writer.Write(this.KillCooldown);
		writer.Write((byte)this.NumCommonTasks);
		writer.Write((byte)this.NumLongTasks);
		writer.Write((byte)this.NumShortTasks);
		writer.Write(this.NumEmergencyMeetings);
		writer.Write((byte)this.NumImpostors);
		writer.Write((byte)this.KillDistance);
		writer.Write(this.DiscussionTime);
		writer.Write(this.VotingTime);
		writer.Write(this.isDefaults);
		if (version > 1)
		{
			writer.Write((byte)this.EmergencyCooldown);
		}
		if (version > 2)
		{
			writer.Write(this.ConfirmImpostor);
			writer.Write(this.VisualTasks);
		}
		if (version > 3)
		{
			writer.Write(this.AnonymousVotes);
			writer.Write((byte)this.TaskBarMode);
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00044814 File Offset: 0x00042A14
	public static GameOptionsData Deserialize(BinaryReader reader)
	{
		try
		{
			byte b = reader.ReadByte();
			GameOptionsData gameOptionsData = new GameOptionsData();
			gameOptionsData.MaxPlayers = (int)reader.ReadByte();
			gameOptionsData.Keywords = (GameKeywords)reader.ReadUInt32();
			gameOptionsData.MapId = reader.ReadByte();
			gameOptionsData.PlayerSpeedMod = reader.ReadSingle();
			gameOptionsData.CrewLightMod = reader.ReadSingle();
			gameOptionsData.ImpostorLightMod = reader.ReadSingle();
			gameOptionsData.KillCooldown = reader.ReadSingle();
			gameOptionsData.NumCommonTasks = (int)reader.ReadByte();
			gameOptionsData.NumLongTasks = (int)reader.ReadByte();
			gameOptionsData.NumShortTasks = (int)reader.ReadByte();
			gameOptionsData.NumEmergencyMeetings = reader.ReadInt32();
			gameOptionsData.NumImpostors = (int)reader.ReadByte();
			gameOptionsData.KillDistance = (int)reader.ReadByte();
			gameOptionsData.DiscussionTime = reader.ReadInt32();
			gameOptionsData.VotingTime = reader.ReadInt32();
			gameOptionsData.isDefaults = reader.ReadBoolean();
			try
			{
				if (b > 1)
				{
					gameOptionsData.EmergencyCooldown = (int)reader.ReadByte();
				}
				if (b > 2)
				{
					gameOptionsData.ConfirmImpostor = reader.ReadBoolean();
					gameOptionsData.VisualTasks = reader.ReadBoolean();
				}
				if (b > 3)
				{
					gameOptionsData.AnonymousVotes = reader.ReadBoolean();
					gameOptionsData.TaskBarMode = (TaskBarMode)reader.ReadByte();
				}
			}
			catch
			{
			}
			return gameOptionsData;
		}
		catch
		{
		}
		return null;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0004497C File Offset: 0x00042B7C
	public static GameOptionsData Deserialize(MessageReader reader)
	{
		try
		{
			byte b = reader.ReadByte();
			GameOptionsData gameOptionsData = new GameOptionsData();
			gameOptionsData.MaxPlayers = (int)reader.ReadByte();
			gameOptionsData.Keywords = (GameKeywords)reader.ReadUInt32();
			gameOptionsData.MapId = reader.ReadByte();
			gameOptionsData.PlayerSpeedMod = reader.ReadSingle();
			gameOptionsData.CrewLightMod = reader.ReadSingle();
			gameOptionsData.ImpostorLightMod = reader.ReadSingle();
			gameOptionsData.KillCooldown = reader.ReadSingle();
			gameOptionsData.NumCommonTasks = (int)reader.ReadByte();
			gameOptionsData.NumLongTasks = (int)reader.ReadByte();
			gameOptionsData.NumShortTasks = (int)reader.ReadByte();
			gameOptionsData.NumEmergencyMeetings = reader.ReadInt32();
			gameOptionsData.NumImpostors = (int)reader.ReadByte();
			gameOptionsData.KillDistance = (int)reader.ReadByte();
			gameOptionsData.DiscussionTime = reader.ReadInt32();
			gameOptionsData.VotingTime = reader.ReadInt32();
			gameOptionsData.isDefaults = reader.ReadBoolean();
			try
			{
				if (b > 1)
				{
					gameOptionsData.EmergencyCooldown = (int)reader.ReadByte();
				}
				if (b > 2)
				{
					gameOptionsData.ConfirmImpostor = reader.ReadBoolean();
					gameOptionsData.VisualTasks = reader.ReadBoolean();
				}
				if (b > 3)
				{
					gameOptionsData.AnonymousVotes = reader.ReadBoolean();
					gameOptionsData.TaskBarMode = (TaskBarMode)reader.ReadByte();
				}
			}
			catch
			{
			}
			return gameOptionsData;
		}
		catch
		{
		}
		return null;
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00044AE4 File Offset: 0x00042CE4
	public byte[] ToBytes(byte version)
	{
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				this.Serialize(binaryWriter, version);
				binaryWriter.Flush();
				memoryStream.Position = 0L;
				result = memoryStream.ToArray();
			}
		}
		return result;
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00044B50 File Offset: 0x00042D50
	public static GameOptionsData FromBytes(byte[] bytes)
	{
		GameOptionsData result;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			using (BinaryReader binaryReader = new BinaryReader(memoryStream))
			{
				result = (GameOptionsData.Deserialize(binaryReader) ?? new GameOptionsData());
			}
		}
		return result;
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00044BB0 File Offset: 0x00042DB0
	public override string ToString()
	{
		return this.ToHudString(10);
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00044BBC File Offset: 0x00042DBC
	public string ToHudString(int numPlayers)
	{
		numPlayers = Mathf.Clamp(numPlayers, 0, 10);
		this.settings.Length = 0;
		try
		{
			this.settings.AppendLine(DestroyableSingleton<TranslationController>.Instance.GetString(this.isDefaults ? StringNames.GameRecommendedSettings : StringNames.GameCustomSettings, Array.Empty<object>()));
			int num = GameOptionsData.MaxImpostors[numPlayers];
			string value = (this.MapId == 0 && Constants.ShouldFlipSkeld()) ? "Dleks" : GameOptionsData.MapNames[(int)this.MapId];
			this.AppendItem(this.settings, StringNames.GameMapName, value);
			this.settings.Append(string.Format("{0}: {1}", DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameNumImpostors, Array.Empty<object>()), this.NumImpostors));
			if (this.NumImpostors > num)
			{
				this.settings.Append(string.Format(" ({0}: {1})", DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Limit, Array.Empty<object>()), num));
			}
			this.settings.AppendLine();
			this.AppendItem(this.settings, StringNames.GameConfirmImpostor, this.ConfirmImpostor);
			this.AppendItem(this.settings, StringNames.GameNumMeetings, this.NumEmergencyMeetings);
			this.AppendItem(this.settings, StringNames.GameAnonymousVotes, this.AnonymousVotes);
			this.AppendItem(this.settings, StringNames.GameEmergencyCooldown, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameSecondsAbbrev, new object[]
			{
				this.EmergencyCooldown
			}));
			this.AppendItem(this.settings, StringNames.GameDiscussTime, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameSecondsAbbrev, new object[]
			{
				this.DiscussionTime
			}));
			if (this.VotingTime > 0)
			{
				this.AppendItem(this.settings, StringNames.GameVotingTime, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameSecondsAbbrev, new object[]
				{
					this.VotingTime
				}));
			}
			else
			{
				this.AppendItem(this.settings, StringNames.GameVotingTime, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameSecondsAbbrev, new object[]
				{
					"∞"
				}));
			}
			this.AppendItem(this.settings, StringNames.GamePlayerSpeed, this.PlayerSpeedMod, "x");
			this.AppendItem(this.settings, StringNames.GameCrewLight, this.CrewLightMod, "x");
			this.AppendItem(this.settings, StringNames.GameImpostorLight, this.ImpostorLightMod, "x");
			this.AppendItem(this.settings, StringNames.GameKillCooldown, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameSecondsAbbrev, new object[]
			{
				this.KillCooldown
			}));
			this.AppendItem(this.settings, StringNames.GameKillDistance, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SettingShort + this.KillDistance, Array.Empty<object>()));
			this.AppendItem(this.settings, StringNames.GameTaskBarMode, DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SettingNormalTaskMode + (int)this.TaskBarMode, Array.Empty<object>()));
			this.AppendItem(this.settings, StringNames.GameVisualTasks, this.VisualTasks);
			this.AppendItem(this.settings, StringNames.GameCommonTasks, this.NumCommonTasks);
			this.AppendItem(this.settings, StringNames.GameLongTasks, this.NumLongTasks);
			this.AppendItem(this.settings, StringNames.GameShortTasks, this.NumShortTasks);
		}
		catch
		{
		}
		return this.settings.ToString();
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00044F48 File Offset: 0x00043148
	private void AppendItem(StringBuilder settings, StringNames stringName, bool value)
	{
		settings.Append(DestroyableSingleton<TranslationController>.Instance.GetString(stringName, Array.Empty<object>()));
		settings.Append(": ");
		settings.AppendLine(DestroyableSingleton<TranslationController>.Instance.GetString(value ? StringNames.SettingsOn : StringNames.SettingsOff, Array.Empty<object>()));
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00044F97 File Offset: 0x00043197
	private void AppendItem(StringBuilder settings, StringNames stringName, float value, string secs)
	{
		settings.Append(DestroyableSingleton<TranslationController>.Instance.GetString(stringName, Array.Empty<object>()));
		settings.Append(": ");
		settings.Append(value);
		settings.AppendLine(secs);
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00044FCD File Offset: 0x000431CD
	private void AppendItem(StringBuilder settings, StringNames stringName, int value, string secs)
	{
		settings.Append(DestroyableSingleton<TranslationController>.Instance.GetString(stringName, Array.Empty<object>()));
		settings.Append(": ");
		settings.Append(value);
		settings.AppendLine(secs);
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00045003 File Offset: 0x00043203
	private void AppendItem(StringBuilder settings, StringNames stringName, string value)
	{
		settings.Append(DestroyableSingleton<TranslationController>.Instance.GetString(stringName, Array.Empty<object>()));
		settings.Append(": ");
		settings.AppendLine(value);
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00045030 File Offset: 0x00043230
	private void AppendItem(StringBuilder settings, StringNames stringName, int value)
	{
		settings.Append(DestroyableSingleton<TranslationController>.Instance.GetString(stringName, Array.Empty<object>()));
		settings.Append(": ");
		settings.Append(value);
		settings.AppendLine();
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00045064 File Offset: 0x00043264
	public int GetAdjustedNumImpostors(int playerCount)
	{
		int numImpostors = PlayerControl.GameOptions.NumImpostors;
		int num = GameOptionsData.MaxImpostors[GameData.Instance.PlayerCount];
		return Mathf.Clamp(numImpostors, 1, num);
	}

	// Token: 0x04000C20 RID: 3104
	private const byte SkeldBit = 1;

	// Token: 0x04000C21 RID: 3105
	private const byte MiraBit = 2;

	// Token: 0x04000C22 RID: 3106
	private const byte PolusBit = 4;

	// Token: 0x04000C23 RID: 3107
	private const byte AirshipBit = 16;

	// Token: 0x04000C24 RID: 3108
	private const byte MapMask = 23;

	// Token: 0x04000C25 RID: 3109
	public const byte ServerVersion = 2;

	// Token: 0x04000C26 RID: 3110
	public const byte NewestVersion = 4;

	// Token: 0x04000C27 RID: 3111
	public static readonly string[] MapNames = new string[]
	{
		"The Skeld",
		"MIRA HQ",
		"Polus",
		"dlekS ehT",
		"Airship"
	};

	// Token: 0x04000C28 RID: 3112
	public static readonly float[] KillDistances = new float[]
	{
		1f,
		1.8f,
		2.5f
	};

	// Token: 0x04000C29 RID: 3113
	public static readonly string[] KillDistanceStrings = new string[]
	{
		"Short",
		"Normal",
		"Long"
	};

	// Token: 0x04000C2A RID: 3114
	public int MaxPlayers = 10;

	// Token: 0x04000C2B RID: 3115
	public GameKeywords Keywords = GameKeywords.Other;

	// Token: 0x04000C2C RID: 3116
	public byte MapId;

	// Token: 0x04000C2D RID: 3117
	public float PlayerSpeedMod = 1f;

	// Token: 0x04000C2E RID: 3118
	public float CrewLightMod = 1f;

	// Token: 0x04000C2F RID: 3119
	public float ImpostorLightMod = 1.5f;

	// Token: 0x04000C30 RID: 3120
	public float KillCooldown = 15f;

	// Token: 0x04000C31 RID: 3121
	public int NumCommonTasks = 1;

	// Token: 0x04000C32 RID: 3122
	public int NumLongTasks = 1;

	// Token: 0x04000C33 RID: 3123
	public int NumShortTasks = 2;

	// Token: 0x04000C34 RID: 3124
	public int NumEmergencyMeetings = 1;

	// Token: 0x04000C35 RID: 3125
	public int EmergencyCooldown = 15;

	// Token: 0x04000C36 RID: 3126
	public int NumImpostors = 1;

	// Token: 0x04000C37 RID: 3127
	public bool GhostsDoTasks = true;

	// Token: 0x04000C38 RID: 3128
	public int KillDistance = 1;

	// Token: 0x04000C39 RID: 3129
	public int DiscussionTime = 15;

	// Token: 0x04000C3A RID: 3130
	public int VotingTime = 120;

	// Token: 0x04000C3B RID: 3131
	public bool ConfirmImpostor = true;

	// Token: 0x04000C3C RID: 3132
	public bool VisualTasks = true;

	// Token: 0x04000C3D RID: 3133
	public bool AnonymousVotes;

	// Token: 0x04000C3E RID: 3134
	public TaskBarMode TaskBarMode;

	// Token: 0x04000C3F RID: 3135
	public bool isDefaults = true;

	// Token: 0x04000C40 RID: 3136
	private static readonly int[] RecommendedKillCooldown = new int[]
	{
		0,
		0,
		0,
		0,
		45,
		30,
		15,
		35,
		30,
		25,
		20
	};

	// Token: 0x04000C41 RID: 3137
	private static readonly int[] RecommendedImpostors = new int[]
	{
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		2,
		2,
		2,
		2
	};

	// Token: 0x04000C42 RID: 3138
	private static readonly int[] MaxImpostors = new int[]
	{
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		2,
		2,
		3,
		3
	};

	// Token: 0x04000C43 RID: 3139
	public static readonly int[] MinPlayers = new int[]
	{
		4,
		4,
		7,
		9
	};

	// Token: 0x04000C44 RID: 3140
	private StringBuilder settings = new StringBuilder(2048);
}
