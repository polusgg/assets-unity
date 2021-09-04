using System;
using System.Linq;

namespace InnerNet
{
	// Token: 0x02000290 RID: 656
	public static class GameCode
	{
		// Token: 0x0600125D RID: 4701 RVA: 0x0005FCFC File Offset: 0x0005DEFC
		public static string IntToGameName(int gameId)
		{
			if (gameId < -1)
			{
				return GameCode.IntToGameNameV2(gameId);
			}
			char[] array = new char[]
			{
				(char)(gameId & 255),
				(char)(gameId >> 8 & 255),
				(char)(gameId >> 16 & 255),
				(char)(gameId >> 24 & 255)
			};
			if (array.Any((char c) => c < 'A' || c > 'z'))
			{
				return null;
			}
			return new string(array);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0005FD80 File Offset: 0x0005DF80
		public static int GameNameToInt(string gameId)
		{
			if (gameId.Length == 6)
			{
				return GameCode.GameNameToIntV2(gameId);
			}
			if (gameId.Length != 4)
			{
				return -1;
			}
			gameId = gameId.ToUpperInvariant();
			if (gameId.Any((char c) => c < 'A' || c > 'Z'))
			{
				return 0;
			}
			return (int)(gameId[0] | (int)gameId[1] << 8 | (int)gameId[2] << 16 | (int)gameId[3] << 24);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0005FE00 File Offset: 0x0005E000
		public static int CreateGameId(int sn, int gn)
		{
			return int.MinValue | (gn & 1023) | (sn & 1048575) << 10;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0005FE1C File Offset: 0x0005E01C
		private static string IntToGameNameV2(int gameId)
		{
			char[] array = new char[6];
			int num = gameId & 1023;
			int num2 = (int)((uint)gameId >> 10 & 1048575U);
			array[0] = GameCode.V2[num % 26];
			array[1] = GameCode.V2[num / 26];
			array[2] = GameCode.V2[num2 % 26];
			array[3] = GameCode.V2[num2 / 26 % 26];
			array[4] = GameCode.V2[num2 / 676 % 26];
			array[5] = GameCode.V2[num2 / 17576 % 26];
			if (array.Any((char c) => c < 'A' || c > 'z'))
			{
				return null;
			}
			return new string(array);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0005FEE8 File Offset: 0x0005E0E8
		private static int GameNameToIntV2(string gameId)
		{
			if (gameId.Length != 6)
			{
				return -1;
			}
			gameId = gameId.ToUpperInvariant();
			if (gameId.Any((char c) => c < 'A' || c > 'z'))
			{
				return 0;
			}
			int gn = GameCode.V2Map[(int)(gameId[0] - 'A')] + GameCode.V2Map[(int)(gameId[1] - 'A')] * 26;
			return GameCode.CreateGameId(GameCode.V2Map[(int)(gameId[2] - 'A')] + GameCode.V2Map[(int)(gameId[3] - 'A')] * 26 + GameCode.V2Map[(int)(gameId[4] - 'A')] * 676 + GameCode.V2Map[(int)(gameId[5] - 'A')] * 17576, gn);
		}

		// Token: 0x040014F7 RID: 5367
		public const int V2Flag = -2147483648;

		// Token: 0x040014F8 RID: 5368
		public const int MaxGameNumber = 676;

		// Token: 0x040014F9 RID: 5369
		public static readonly int GameCodeV2MinVersion = Constants.GetVersion(2020, 9, 6, 0);

		// Token: 0x040014FA RID: 5370
		private static readonly string V2 = "QWXRTYLPESDFGHUJKZOCVBINMA";

		// Token: 0x040014FB RID: 5371
		private static readonly int[] V2Map = (from v in Enumerable.Range(65, 26)
		select GameCode.V2.IndexOf((char)v)).ToArray<int>();
	}
}
