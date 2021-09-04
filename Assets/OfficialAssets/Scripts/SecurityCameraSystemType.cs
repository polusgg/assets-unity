using System;
using System.Collections.Generic;
using Hazel;

// Token: 0x020001C5 RID: 453
public class SecurityCameraSystemType : ISystemType
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00043A56 File Offset: 0x00041C56
	public bool InUse
	{
		get
		{
			return this.PlayersUsing.Count > 0;
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00043A66 File Offset: 0x00041C66
	// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x00043A6E File Offset: 0x00041C6E
	public bool IsDirty { get; private set; }

	// Token: 0x06000AB3 RID: 2739 RVA: 0x00043A78 File Offset: 0x00041C78
	public void Detoriorate(float deltaTime)
	{
		if (GameData.Instance)
		{
			foreach (byte b in this.PlayersUsing)
			{
				GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(b);
				if (playerById == null || playerById.Disconnected)
				{
					this.ToRemove.Add(b);
				}
			}
			if (this.ToRemove.Count > 0)
			{
				this.PlayersUsing.ExceptWith(this.ToRemove);
				this.ToRemove.Clear();
				this.UpdateCameras();
				this.IsDirty = true;
			}
		}
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x00043B30 File Offset: 0x00041D30
	public void RepairDamage(PlayerControl player, byte amount)
	{
		if (amount == 1)
		{
			this.PlayersUsing.Add(player.PlayerId);
		}
		else
		{
			this.PlayersUsing.Remove(player.PlayerId);
		}
		this.IsDirty = true;
		this.UpdateCameras();
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x00043B6C File Offset: 0x00041D6C
	private void UpdateCameras()
	{
		for (int i = 0; i < ShipStatus.Instance.AllCameras.Length; i++)
		{
			ShipStatus.Instance.AllCameras[i].SetAnimation(this.InUse);
		}
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x00043BA8 File Offset: 0x00041DA8
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.WritePacked(this.PlayersUsing.Count);
		foreach (byte b in this.PlayersUsing)
		{
			writer.Write(b);
		}
		this.IsDirty = initialState;
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00043C14 File Offset: 0x00041E14
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.PlayersUsing.Clear();
		int num = reader.ReadPackedInt32();
		for (int i = 0; i < num; i++)
		{
			this.PlayersUsing.Add(reader.ReadByte());
		}
		this.UpdateCameras();
	}

	// Token: 0x04000C04 RID: 3076
	public const byte IncrementOp = 1;

	// Token: 0x04000C05 RID: 3077
	public const byte DecrementOp = 2;

	// Token: 0x04000C06 RID: 3078
	private HashSet<byte> PlayersUsing = new HashSet<byte>();

	// Token: 0x04000C08 RID: 3080
	private HashSet<byte> ToRemove = new HashSet<byte>();
}
