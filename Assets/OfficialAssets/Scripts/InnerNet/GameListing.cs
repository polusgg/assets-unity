using System;
using Hazel;

namespace InnerNet
{
	// Token: 0x02000297 RID: 663
	[Serializable]
	public struct GameListing
	{
		// Token: 0x060012CD RID: 4813 RVA: 0x00062400 File Offset: 0x00060600
		public static GameListing Deserialize(MessageReader reader)
		{
			GameListing result = default(GameListing);
			result.GameId = reader.ReadInt32();
			result.HostName = reader.ReadString();
			result.PlayerCount = reader.ReadByte();
			result.Age = reader.ReadPackedInt32();
			GameOptionsData gameOptionsData = GameOptionsData.Deserialize(reader);
			result.MapId = gameOptionsData.MapId;
			result.NumImpostors = gameOptionsData.NumImpostors;
			result.MaxPlayers = gameOptionsData.MaxPlayers;
			return result;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00062478 File Offset: 0x00060678
		public static GameListing DeserializeV2(MessageReader reader)
		{
			return new GameListing
			{
				IP = reader.ReadUInt32(),
				Port = reader.ReadUInt16(),
				GameId = reader.ReadInt32(),
				HostName = reader.ReadString(),
				PlayerCount = reader.ReadByte(),
				Age = reader.ReadPackedInt32(),
				MapId = reader.ReadByte(),
				NumImpostors = (int)reader.ReadByte(),
				MaxPlayers = (int)reader.ReadByte()
			};
		}

		// Token: 0x04001555 RID: 5461
		public uint IP;

		// Token: 0x04001556 RID: 5462
		public ushort Port;

		// Token: 0x04001557 RID: 5463
		public int GameId;

		// Token: 0x04001558 RID: 5464
		public byte PlayerCount;

		// Token: 0x04001559 RID: 5465
		public string HostName;

		// Token: 0x0400155A RID: 5466
		public int Age;

		// Token: 0x0400155B RID: 5467
		public int MaxPlayers;

		// Token: 0x0400155C RID: 5468
		public int NumImpostors;

		// Token: 0x0400155D RID: 5469
		public byte MapId;
	}
}
