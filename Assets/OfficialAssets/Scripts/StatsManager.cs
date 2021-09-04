using System;
using System.IO;
using UnityEngine;

// Token: 0x0200021A RID: 538
public class StatsManager
{
	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0004E042 File Offset: 0x0004C242
	// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0004E050 File Offset: 0x0004C250
	public uint BodiesReported
	{
		get
		{
			this.LoadStats();
			return this.bodiesReported;
		}
		set
		{
			this.LoadStats();
			this.bodiesReported = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0004E065 File Offset: 0x0004C265
	// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0004E073 File Offset: 0x0004C273
	public uint EmergenciesCalled
	{
		get
		{
			this.LoadStats();
			return this.emergenciesCalls;
		}
		set
		{
			this.LoadStats();
			this.emergenciesCalls = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0004E088 File Offset: 0x0004C288
	// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0004E096 File Offset: 0x0004C296
	public uint TasksCompleted
	{
		get
		{
			this.LoadStats();
			return this.tasksCompleted;
		}
		set
		{
			this.LoadStats();
			this.tasksCompleted = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0004E0AB File Offset: 0x0004C2AB
	// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0004E0B9 File Offset: 0x0004C2B9
	public uint CompletedAllTasks
	{
		get
		{
			this.LoadStats();
			return this.completedAllTasks;
		}
		set
		{
			this.LoadStats();
			this.completedAllTasks = value;
			this.SaveStats();
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0004E0CE File Offset: 0x0004C2CE
	// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0004E0DC File Offset: 0x0004C2DC
	public uint SabsFixed
	{
		get
		{
			this.LoadStats();
			return this.sabsFixed;
		}
		set
		{
			this.LoadStats();
			this.sabsFixed = value;
			this.SaveStats();
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0004E0F1 File Offset: 0x0004C2F1
	// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0004E0FF File Offset: 0x0004C2FF
	public uint ImpostorKills
	{
		get
		{
			this.LoadStats();
			return this.impostorKills;
		}
		set
		{
			this.LoadStats();
			this.impostorKills = value;
			this.SaveStats();
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0004E114 File Offset: 0x0004C314
	// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0004E122 File Offset: 0x0004C322
	public uint TimesMurdered
	{
		get
		{
			this.LoadStats();
			return this.timesMurdered;
		}
		set
		{
			this.LoadStats();
			this.timesMurdered = value;
			this.SaveStats();
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0004E137 File Offset: 0x0004C337
	// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0004E145 File Offset: 0x0004C345
	public uint TimesEjected
	{
		get
		{
			this.LoadStats();
			return this.timesEjected;
		}
		set
		{
			this.LoadStats();
			this.timesEjected = value;
			this.SaveStats();
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0004E15A File Offset: 0x0004C35A
	// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0004E168 File Offset: 0x0004C368
	public uint CrewmateStreak
	{
		get
		{
			this.LoadStats();
			return this.crewmateStreak;
		}
		set
		{
			this.LoadStats();
			this.crewmateStreak = value;
			this.SaveStats();
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0004E17D File Offset: 0x0004C37D
	// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0004E18B File Offset: 0x0004C38B
	public uint TimesImpostor
	{
		get
		{
			this.LoadStats();
			return this.timesImpostor;
		}
		set
		{
			this.LoadStats();
			this.timesImpostor = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
	// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x0004E1AE File Offset: 0x0004C3AE
	public uint TimesCrewmate
	{
		get
		{
			this.LoadStats();
			return this.timesCrewmate;
		}
		set
		{
			this.LoadStats();
			this.timesCrewmate = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0004E1C3 File Offset: 0x0004C3C3
	// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0004E1D1 File Offset: 0x0004C3D1
	public uint GamesStarted
	{
		get
		{
			this.LoadStats();
			return this.gamesStarted;
		}
		set
		{
			this.LoadStats();
			this.gamesStarted = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0004E1E6 File Offset: 0x0004C3E6
	// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0004E1F4 File Offset: 0x0004C3F4
	public uint GamesFinished
	{
		get
		{
			this.LoadStats();
			return this.gamesFinished;
		}
		set
		{
			this.LoadStats();
			this.gamesFinished = value;
			this.SaveStats();
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0004E209 File Offset: 0x0004C409
	// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0004E217 File Offset: 0x0004C417
	public float BanPoints
	{
		get
		{
			this.LoadStats();
			return this.banPoints;
		}
		set
		{
			this.LoadStats();
			this.banPoints = Mathf.Max(0f, value);
			this.SaveStats();
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0004E236 File Offset: 0x0004C436
	// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0004E249 File Offset: 0x0004C449
	public DateTime LastGameStarted
	{
		get
		{
			this.LoadStats();
			return new DateTime(this.lastGameStarted);
		}
		set
		{
			this.LoadStats();
			this.lastGameStarted = value.Ticks;
			this.SaveStats();
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0004E264 File Offset: 0x0004C464
	public float BanMinutes
	{
		get
		{
			return Mathf.Max(this.BanPoints - 2f, 0f) * 5f;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0004E282 File Offset: 0x0004C482
	public bool AmBanned
	{
		get
		{
			return this.BanMinutesLeft > 0;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0004E290 File Offset: 0x0004C490
	public int BanMinutesLeft
	{
		get
		{
			TimeSpan timeSpan = DateTime.UtcNow - this.LastGameStarted;
			int num = Mathf.CeilToInt(this.BanMinutes - (float)timeSpan.TotalMinutes);
			if (num > 1440 || timeSpan.TotalDays > 1.0)
			{
				this.BanPoints = 0f;
				return 0;
			}
			return num;
		}
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0004E2EB File Offset: 0x0004C4EB
	public void AddDrawReason(GameOverReason reason)
	{
		this.LoadStats();
		this.DrawReasons[(int)reason] += 1U;
		this.SaveStats();
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0004E30A File Offset: 0x0004C50A
	public void AddWinReason(GameOverReason reason)
	{
		this.LoadStats();
		this.WinReasons[(int)reason] += 1U;
		this.SaveStats();
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0004E329 File Offset: 0x0004C529
	public uint GetWinReason(GameOverReason reason)
	{
		this.LoadStats();
		return this.WinReasons[(int)reason];
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0004E339 File Offset: 0x0004C539
	public void AddLoseReason(GameOverReason reason)
	{
		this.LoadStats();
		this.LoseReasons[(int)reason] += 1U;
		this.SaveStats();
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0004E358 File Offset: 0x0004C558
	public uint GetLoseReason(GameOverReason reason)
	{
		this.LoadStats();
		return this.LoseReasons[(int)reason];
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0004E368 File Offset: 0x0004C568
	protected virtual void LoadStats()
	{
		if (this.loadedStats)
		{
			return;
		}
		this.loadedStats = true;
		Debug.Log("LoadStats");
		string path = Path.Combine(PlatformPaths.persistentDataPath, "playerStats2");
		if (FileIO.Exists(path))
		{
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
				{
					byte b = binaryReader.ReadByte();
					this.bodiesReported = binaryReader.ReadUInt32();
					this.emergenciesCalls = binaryReader.ReadUInt32();
					this.tasksCompleted = binaryReader.ReadUInt32();
					this.completedAllTasks = binaryReader.ReadUInt32();
					this.sabsFixed = binaryReader.ReadUInt32();
					this.impostorKills = binaryReader.ReadUInt32();
					this.timesMurdered = binaryReader.ReadUInt32();
					this.timesEjected = binaryReader.ReadUInt32();
					this.crewmateStreak = binaryReader.ReadUInt32();
					this.timesImpostor = binaryReader.ReadUInt32();
					this.timesCrewmate = binaryReader.ReadUInt32();
					this.gamesStarted = binaryReader.ReadUInt32();
					this.gamesFinished = binaryReader.ReadUInt32();
					for (int i = 0; i < this.WinReasons.Length; i++)
					{
						this.WinReasons[i] = binaryReader.ReadUInt32();
					}
					for (int j = 0; j < this.LoseReasons.Length; j++)
					{
						this.LoseReasons[j] = binaryReader.ReadUInt32();
					}
					if (b > 1)
					{
						for (int k = 0; k < this.DrawReasons.Length; k++)
						{
							this.DrawReasons[k] = binaryReader.ReadUInt32();
						}
					}
					if (b > 2)
					{
						this.banPoints = binaryReader.ReadSingle();
						this.lastGameStarted = binaryReader.ReadInt64();
					}
				}
			}
			catch
			{
				Debug.LogError("Deleting corrupted stats file");
				File.Delete(path);
			}
		}
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0004E53C File Offset: 0x0004C73C
	protected virtual void SaveStats()
	{
		Debug.Log("SaveStats");
		try
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(Path.Combine(PlatformPaths.persistentDataPath, "playerStats2"))))
			{
				binaryWriter.Write(3);
				binaryWriter.Write(this.bodiesReported);
				binaryWriter.Write(this.emergenciesCalls);
				binaryWriter.Write(this.tasksCompleted);
				binaryWriter.Write(this.completedAllTasks);
				binaryWriter.Write(this.sabsFixed);
				binaryWriter.Write(this.impostorKills);
				binaryWriter.Write(this.timesMurdered);
				binaryWriter.Write(this.timesEjected);
				binaryWriter.Write(this.crewmateStreak);
				binaryWriter.Write(this.timesImpostor);
				binaryWriter.Write(this.timesCrewmate);
				binaryWriter.Write(this.gamesStarted);
				binaryWriter.Write(this.gamesFinished);
				for (int i = 0; i < this.WinReasons.Length; i++)
				{
					binaryWriter.Write(this.WinReasons[i]);
				}
				for (int j = 0; j < this.LoseReasons.Length; j++)
				{
					binaryWriter.Write(this.LoseReasons[j]);
				}
				for (int k = 0; k < this.DrawReasons.Length; k++)
				{
					binaryWriter.Write(this.DrawReasons[k]);
				}
				binaryWriter.Write(this.banPoints);
				binaryWriter.Write(this.lastGameStarted);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Failed to write out stats: " + ex.Message);
		}
	}

	// Token: 0x04000E2B RID: 3627
	public static StatsManager Instance = new StatsManager();

	// Token: 0x04000E2C RID: 3628
	private const byte StatsVersion = 3;

	// Token: 0x04000E2D RID: 3629
	private bool loadedStats;

	// Token: 0x04000E2E RID: 3630
	private uint bodiesReported;

	// Token: 0x04000E2F RID: 3631
	private uint emergenciesCalls;

	// Token: 0x04000E30 RID: 3632
	private uint tasksCompleted;

	// Token: 0x04000E31 RID: 3633
	private uint completedAllTasks;

	// Token: 0x04000E32 RID: 3634
	private uint sabsFixed;

	// Token: 0x04000E33 RID: 3635
	private uint impostorKills;

	// Token: 0x04000E34 RID: 3636
	private uint timesMurdered;

	// Token: 0x04000E35 RID: 3637
	private uint timesEjected;

	// Token: 0x04000E36 RID: 3638
	private uint crewmateStreak;

	// Token: 0x04000E37 RID: 3639
	private uint timesImpostor;

	// Token: 0x04000E38 RID: 3640
	private uint timesCrewmate;

	// Token: 0x04000E39 RID: 3641
	private uint gamesStarted;

	// Token: 0x04000E3A RID: 3642
	private uint gamesFinished;

	// Token: 0x04000E3B RID: 3643
	private float banPoints;

	// Token: 0x04000E3C RID: 3644
	private long lastGameStarted;

	// Token: 0x04000E3D RID: 3645
	private const int PointsUntilBanStarts = 2;

	// Token: 0x04000E3E RID: 3646
	private const int MinutesPerBanPoint = 5;

	// Token: 0x04000E3F RID: 3647
	private uint[] WinReasons = new uint[7];

	// Token: 0x04000E40 RID: 3648
	private uint[] DrawReasons = new uint[7];

	// Token: 0x04000E41 RID: 3649
	private uint[] LoseReasons = new uint[7];
}
