using System;
using System.Collections.Generic;
using Hazel;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class MedScanSystem : ISystemType
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000774 RID: 1908 RVA: 0x0002F6B3 File Offset: 0x0002D8B3
	// (set) Token: 0x06000775 RID: 1909 RVA: 0x0002F6BB File Offset: 0x0002D8BB
	public byte CurrentUser { get; private set; } = byte.MaxValue;

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000776 RID: 1910 RVA: 0x0002F6C4 File Offset: 0x0002D8C4
	// (set) Token: 0x06000777 RID: 1911 RVA: 0x0002F6CC File Offset: 0x0002D8CC
	public bool IsDirty { get; private set; }

	// Token: 0x06000778 RID: 1912 RVA: 0x0002F6D8 File Offset: 0x0002D8D8
	public void Detoriorate(float deltaTime)
	{
		if (this.UsersList.Count > 0)
		{
			if (this.CurrentUser != this.UsersList[0])
			{
				if (this.CurrentUser != 255)
				{
					Debug.Log("Released scanner from: " + this.CurrentUser.ToString());
				}
				this.CurrentUser = this.UsersList[0];
				Debug.Log("Acquired scanner for: " + this.CurrentUser.ToString());
				this.IsDirty = true;
				return;
			}
		}
		else if (this.CurrentUser != 255)
		{
			Debug.Log("Released scanner from: " + this.CurrentUser.ToString());
			this.CurrentUser = byte.MaxValue;
			this.IsDirty = true;
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
	public void RepairDamage(PlayerControl player, byte data)
	{
		//byte playerId = data & 31;
		//if ((data & 128) != 0)
		//{
		//	if (!this.UsersList.Contains(playerId))
		//	{
		//		Debug.Log("Added to queue: " + playerId.ToString());
		//		this.UsersList.Add(playerId);
		//	}
		//}
		//else if ((data & 64) != 0)
		//{
		//	Debug.Log("Removed from queue: " + playerId.ToString());
		//	this.UsersList.RemoveAll((byte v) => v == playerId);
		//}
		//this.IsDirty = true;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0002F850 File Offset: 0x0002DA50
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.WritePacked(this.UsersList.Count);
		for (int i = 0; i < this.UsersList.Count; i++)
		{
			writer.Write(this.UsersList[i]);
		}
		this.IsDirty = initialState;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.UsersList.Clear();
		int num = reader.ReadPackedInt32();
		for (int i = 0; i < num; i++)
		{
			this.UsersList.Add(reader.ReadByte());
		}
	}

	// Token: 0x0400086D RID: 2157
	public const byte Request = 128;

	// Token: 0x0400086E RID: 2158
	public const byte Release = 64;

	// Token: 0x0400086F RID: 2159
	public const byte NumMask = 31;

	// Token: 0x04000870 RID: 2160
	public const byte NoPlayer = 255;

	// Token: 0x04000871 RID: 2161
	public List<byte> UsersList = new List<byte>();
}
