using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;

// Token: 0x020001EE RID: 494
internal class HqHudSystemType : ISystemType, IActivatable
{
	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000B9F RID: 2975 RVA: 0x000495C0 File Offset: 0x000477C0
	public bool IsActive
	{
		get
		{
			return this.CompletedConsoles.Count < 2;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000495D0 File Offset: 0x000477D0
	public float NumComplete
	{
		get
		{
			return (float)this.CompletedConsoles.Count;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000495DE File Offset: 0x000477DE
	public float PercentActive
	{
		get
		{
			return this.Timer / 10f;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x000495EC File Offset: 0x000477EC
	// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x000495F4 File Offset: 0x000477F4
	public bool IsDirty { get; private set; }

	// Token: 0x06000BA4 RID: 2980 RVA: 0x000495FD File Offset: 0x000477FD
	public HqHudSystemType()
	{
		this.CompletedConsoles.Add(0);
		this.CompletedConsoles.Add(1);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00049638 File Offset: 0x00047838
	public void Detoriorate(float deltaTime)
	{
		if (this.IsActive)
		{
			this.Timer -= deltaTime;
			if (this.Timer <= 0f)
			{
				this.TargetNumber = IntRange.Next(0, 99999);
				this.Timer = 10f;
				this.CompletedConsoles.Clear();
			}
			if (!PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.AddSystemTask(SystemTypes.Comms);
			}
		}
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x000496A8 File Offset: 0x000478A8
	internal bool IsConsoleActive(int consoleId)
	{
		return this.ActiveConsoles.Any((Tuple<byte, byte> s) => s.Item2 == (byte)consoleId);
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x000496D9 File Offset: 0x000478D9
	internal bool IsConsoleOkay(int consoleId)
	{
		return this.CompletedConsoles.Contains((byte)consoleId);
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x000496E8 File Offset: 0x000478E8
	public void RepairDamage(PlayerControl player, byte amount)
	{
		//byte b = amount & 15;
		//HqHudSystemType.Tags tags = (HqHudSystemType.Tags)(amount & 240);
		//if (tags <= HqHudSystemType.Tags.DeactiveBit)
		//{
		//	if (tags != HqHudSystemType.Tags.FixBit)
		//	{
		//		if (tags == HqHudSystemType.Tags.DeactiveBit)
		//		{
		//			this.ActiveConsoles.Remove(new Tuple<byte, byte>(player.PlayerId, b));
		//		}
		//	}
		//	else
		//	{
		//		this.Timer = 10f;
		//		this.CompletedConsoles.Add(b);
		//	}
		//}
		//else if (tags != HqHudSystemType.Tags.ActiveBit)
		//{
		//	if (tags == HqHudSystemType.Tags.DamageBit)
		//	{
		//		this.Timer = -1f;
		//		this.CompletedConsoles.Clear();
		//		this.ActiveConsoles.Clear();
		//	}
		//}
		//else
		//{
		//	this.ActiveConsoles.Add(new Tuple<byte, byte>(player.PlayerId, b));
		//}
		//this.IsDirty = true;
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00049798 File Offset: 0x00047998
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.WritePacked(this.ActiveConsoles.Count);
		foreach (Tuple<byte, byte> tuple in this.ActiveConsoles)
		{
			writer.Write(tuple.Item1);
			writer.Write(tuple.Item2);
		}
		writer.WritePacked(this.CompletedConsoles.Count);
		foreach (byte b in this.CompletedConsoles)
		{
			writer.Write(b);
		}
		this.IsDirty = initialState;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00049868 File Offset: 0x00047A68
	public void Deserialize(MessageReader reader, bool initialState)
	{
		int num = reader.ReadPackedInt32();
		this.ActiveConsoles.Clear();
		for (int i = 0; i < num; i++)
		{
			this.ActiveConsoles.Add(new Tuple<byte, byte>(reader.ReadByte(), reader.ReadByte()));
		}
		int num2 = reader.ReadPackedInt32();
		this.CompletedConsoles.Clear();
		for (int j = 0; j < num2; j++)
		{
			this.CompletedConsoles.Add(reader.ReadByte());
		}
	}

	// Token: 0x04000D09 RID: 3337
	public const byte TagMask = 240;

	// Token: 0x04000D0A RID: 3338
	public const byte IdMask = 15;

	// Token: 0x04000D0B RID: 3339
	private HashSet<Tuple<byte, byte>> ActiveConsoles = new HashSet<Tuple<byte, byte>>();

	// Token: 0x04000D0C RID: 3340
	private HashSet<byte> CompletedConsoles = new HashSet<byte>();

	// Token: 0x04000D0D RID: 3341
	private const float ActiveTime = 10f;

	// Token: 0x04000D0E RID: 3342
	private float Timer;

	// Token: 0x04000D0F RID: 3343
	public int TargetNumber;

	// Token: 0x02000438 RID: 1080
	public enum Tags
	{
		// Token: 0x04001C00 RID: 7168
		DamageBit = 128,
		// Token: 0x04001C01 RID: 7169
		ActiveBit = 64,
		// Token: 0x04001C02 RID: 7170
		DeactiveBit = 32,
		// Token: 0x04001C03 RID: 7171
		FixBit = 16
	}
}
