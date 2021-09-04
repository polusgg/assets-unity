using System;
using System.Collections.Generic;
using Hazel;
using InnerNet;

// Token: 0x020000B7 RID: 183
public class VoteBanSystem : InnerNetObject
{
	// Token: 0x06000459 RID: 1113 RVA: 0x0001BF80 File Offset: 0x0001A180
	public void Awake()
	{
		VoteBanSystem.Instance = this;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0001BF88 File Offset: 0x0001A188
	public void CmdAddVote(int clientId)
	{
		//this.AddVote(AmongUsClient.Instance.ClientId, clientId);
		//MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 26, 1);
		//messageWriter.Write(AmongUsClient.Instance.ClientId);
		//messageWriter.Write(clientId);
		//messageWriter.EndMessage();
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
	private void AddVote(int srcClient, int clientId)
	{
		int[] array;
		if (!this.Votes.TryGetValue(clientId, out array))
		{
			array = (this.Votes[clientId] = new int[3]);
		}
		int num = -1;
		for (int i = 0; i < array.Length; i++)
		{
			int num2 = array[i];
			if (num2 == srcClient)
			{
				break;
			}
			if (num2 == 0)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			array[num] = srcClient;
			base.SetDirtyBit(1U);
			if (num == array.Length - 1)
			{
				AmongUsClient.Instance.KickPlayer(clientId, false);
			}
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0001C050 File Offset: 0x0001A250
	public bool HasMyVote(int clientId)
	{
		int[] array;
		return this.Votes.TryGetValue(clientId, out array) && Array.IndexOf<int>(array, AmongUsClient.Instance.ClientId) != -1;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0001C088 File Offset: 0x0001A288
	public override void HandleRpc(byte callId, MessageReader reader)
	{
		if (callId == 26)
		{
			int srcClient = reader.ReadInt32();
			int clientId = reader.ReadInt32();
			this.AddVote(srcClient, clientId);
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write((byte)this.Votes.Count);
		foreach (KeyValuePair<int, int[]> keyValuePair in this.Votes)
		{
			writer.Write(keyValuePair.Key);
			for (int i = 0; i < 3; i++)
			{
				writer.WritePacked(keyValuePair.Value[i]);
			}
		}
		base.ClearDirtyBits();
		return true;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001C140 File Offset: 0x0001A340
	public override void Deserialize(MessageReader reader, bool initialState)
	{
		int num = (int)reader.ReadByte();
		for (int i = 0; i < num; i++)
		{
			int key = reader.ReadInt32();
			int[] array;
			if (!this.Votes.TryGetValue(key, out array))
			{
				array = (this.Votes[key] = new int[3]);
			}
			for (int j = 0; j < 3; j++)
			{
				array[j] = reader.ReadPackedInt32();
			}
		}
	}

	// Token: 0x0400051A RID: 1306
	public static VoteBanSystem Instance;

	// Token: 0x0400051B RID: 1307
	public Dictionary<int, int[]> Votes = new Dictionary<int, int[]>();
}
