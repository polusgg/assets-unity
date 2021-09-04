using System;
using System.Collections.Generic;
using InnerNet;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.CoreScripts
{
	// Token: 0x020002A9 RID: 681
	public class Telemetry : DestroyableSingleton<Telemetry>
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x000636B0 File Offset: 0x000618B0
		public void Init()
		{
			this.gameStarted = true;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000636BC File Offset: 0x000618BC
		public void StartGame(bool isHost, int playerCount, int impostorCount, GameModes gameMode, uint timesImpostor, uint gamesPlayed, uint crewStreak)
		{
			this.amHost = isHost;
			this.timeStarted = DateTime.UtcNow;
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"Platform",
					Application.platform
				},
				{
					"TimesImpostor",
					timesImpostor
				},
				{
					"CrewStreak",
					crewStreak
				},
				{
					"GamesPlayed",
					gamesPlayed
				},
				{
					"GameMode",
					gameMode
				}
			};
			if (this.amHost)
			{
				dictionary.Add("PlayerCount", playerCount);
				dictionary.Add("InfectedCount", impostorCount);
			}
			Analytics.CustomEvent("StartGame", dictionary);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00063778 File Offset: 0x00061978
		public void StartGameCosmetics(int colorId, uint hatId, uint skinId, uint petId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"Color",
					DestroyableSingleton<TranslationController>.Instance.GetString(Telemetry.ColorNames[colorId], Array.Empty<object>())
				},
				{
					"Hat",
					DestroyableSingleton<HatManager>.Instance.GetHatById(hatId).name
				},
				{
					"Skin",
					DestroyableSingleton<HatManager>.Instance.GetSkinById(skinId).name
				},
				{
					"Pet",
					DestroyableSingleton<HatManager>.Instance.GetPetById(petId).name
				}
			};
			Analytics.CustomEvent("StartGameCosmetics", dictionary);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0006380A File Offset: 0x00061A0A
		public void WriteMeetingStarted(bool isEmergency)
		{
			if (!this.amHost)
			{
				return;
			}
			Analytics.CustomEvent("MeetingStarted", new Dictionary<string, object>
			{
				{
					"IsEmergency",
					isEmergency
				}
			});
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00063836 File Offset: 0x00061A36
		public void WriteMeetingEnded(float duration)
		{
			if (!this.amHost)
			{
				return;
			}
			Analytics.CustomEvent("MeetingEnded", new Dictionary<string, object>
			{
				{
					"IsEmergency",
					duration
				}
			});
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00063862 File Offset: 0x00061A62
		public void WritePosition(byte playerNum, Vector2 worldPos)
		{
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00063864 File Offset: 0x00061A64
		public void WriteMurder()
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("Murder");
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0006387A File Offset: 0x00061A7A
		public void WriteSabotageUsed(SystemTypes systemType)
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("SabotageUsed", new Dictionary<string, object>
			{
				{
					"SystemType",
					systemType
				}
			});
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000638A6 File Offset: 0x00061AA6
		public void WriteUse(byte playerNum, TaskTypes taskType, Vector3 worldPos)
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("ConsoleUsed", new Dictionary<string, object>
			{
				{
					"TaskType",
					taskType
				}
			});
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000638D2 File Offset: 0x00061AD2
		public void WriteCompleteTask(byte playerNum, TaskTypes taskType)
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("TaskComplete", new Dictionary<string, object>
			{
				{
					"TaskType",
					taskType
				}
			});
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000638FE File Offset: 0x00061AFE
		internal void WriteDisconnect(DisconnectReasons reason)
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("Disconnect", new Dictionary<string, object>
			{
				{
					"Reason",
					reason
				}
			});
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0006392C File Offset: 0x00061B2C
		public void EndGame(GameOverReason endReason)
		{
			if (!this.gameStarted)
			{
				return;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"Reason",
					endReason
				}
			};
			if (this.amHost)
			{
				dictionary.Add("DurationSec", (DateTime.UtcNow - this.timeStarted).TotalSeconds);
			}
			Analytics.CustomEvent("EndGame", dictionary);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00063995 File Offset: 0x00061B95
		public void SendWho()
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("SentWho");
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000639AC File Offset: 0x00061BAC
		public void WonGame(int colorId, uint hatId)
		{
			if (!this.gameStarted)
			{
				return;
			}
			Analytics.CustomEvent("WonGame", new Dictionary<string, object>
			{
				{
					"Color",
					Telemetry.ColorNames[colorId]
				},
				{
					"Hat",
					DestroyableSingleton<HatManager>.Instance.GetHatById(hatId).name
				}
			});
		}

		// Token: 0x040015AB RID: 5547
		private static readonly StringNames[] ColorNames = new StringNames[]
		{
			StringNames.ColorRed,
			StringNames.ColorBlue,
			StringNames.ColorGreen,
			StringNames.ColorPink,
			StringNames.ColorOrange,
			StringNames.ColorYellow,
			StringNames.ColorBlack,
			StringNames.ColorWhite,
			StringNames.ColorPurple,
			StringNames.ColorBrown,
			StringNames.ColorCyan,
			StringNames.ColorLime
		};

		// Token: 0x040015AC RID: 5548
		private bool amHost;

		// Token: 0x040015AD RID: 5549
		private bool gameStarted;

		// Token: 0x040015AE RID: 5550
		private DateTime timeStarted;
	}
}
