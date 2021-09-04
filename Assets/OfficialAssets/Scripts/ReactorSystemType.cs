using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;

// Token: 0x020001B9 RID: 441
public class ReactorSystemType : ISystemType, IActivatable, ICriticalSabotage
{
	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00041DE4 File Offset: 0x0003FFE4
	// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00041DEC File Offset: 0x0003FFEC
	public float Countdown { get; private set; } = 10000f;

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00041DF5 File Offset: 0x0003FFF5
	// (set) Token: 0x06000A2B RID: 2603 RVA: 0x00041DFD File Offset: 0x0003FFFD
	public bool IsDirty { get; private set; }

	// Token: 0x06000A2C RID: 2604 RVA: 0x00041E06 File Offset: 0x00040006
	public ReactorSystemType(float duration, SystemTypes system)
	{
		this.ReactorDuration = duration;
		this.system = system;
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00041E40 File Offset: 0x00040040
	public int UserCount
	{
		get
		{
			int num = 0;
			int num2 = 0;
			foreach (Tuple<byte, byte> tuple in this.UserConsolePairs)
			{
				int num3 = 1 << (int)tuple.Item2;
				if ((num3 & num2) == 0)
				{
					num++;
					num2 |= num3;
				}
			}
			return num;
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00041EB0 File Offset: 0x000400B0
	public bool GetConsoleComplete(int consoleId)
	{
		return this.UserConsolePairs.Any((Tuple<byte, byte> kvp) => (int)kvp.Item2 == consoleId);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00041EE1 File Offset: 0x000400E1
	public void ClearSabotage()
	{
		this.Countdown = 10000f;
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00041EEE File Offset: 0x000400EE
	public bool IsActive
	{
		get
		{
			return this.Countdown < 10000f;
		}
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00041F00 File Offset: 0x00040100
	public void RepairDamage(PlayerControl player, byte opCode)
	{
		int num = (int)(opCode & 3);
		if (opCode == 128 && !this.IsActive)
		{
			this.Countdown = this.ReactorDuration;
			this.UserConsolePairs.Clear();
		}
		else if (opCode == 16)
		{
			this.Countdown = 10000f;
		}
		else if (opCode.HasAnyBit(64))
		{
			this.UserConsolePairs.Add(new Tuple<byte, byte>(player.PlayerId, (byte)num));
			if (this.UserCount >= 2)
			{
				this.Countdown = 10000f;
			}
		}
		else if (opCode.HasAnyBit(32))
		{
			this.UserConsolePairs.Remove(new Tuple<byte, byte>(player.PlayerId, (byte)num));
		}
		this.IsDirty = true;
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00041FB0 File Offset: 0x000401B0
	public void Detoriorate(float deltaTime)
	{
		if (this.IsActive)
		{
			if (!PlayerTask.PlayerHasTaskOfType<ReactorTask>(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.AddSystemTask(this.system);
			}
			this.Countdown -= deltaTime;
			this.timer += deltaTime;
			if (this.timer > 2f)
			{
				this.timer = 0f;
				this.IsDirty = true;
				return;
			}
		}
		else
		{
			DestroyableSingleton<HudManager>.Instance.StopReactorFlash();
		}
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00042028 File Offset: 0x00040228
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write(this.Countdown);
		writer.WritePacked(this.UserConsolePairs.Count);
		foreach (Tuple<byte, byte> tuple in this.UserConsolePairs)
		{
			writer.Write(tuple.Item1);
			writer.Write(tuple.Item2);
		}
		this.IsDirty = initialState;
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x000420B0 File Offset: 0x000402B0
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.Countdown = reader.ReadSingle();
		this.UserConsolePairs.Clear();
		int num = reader.ReadPackedInt32();
		for (int i = 0; i < num; i++)
		{
			this.UserConsolePairs.Add(new Tuple<byte, byte>(reader.ReadByte(), reader.ReadByte()));
		}
	}

	// Token: 0x04000B9A RID: 2970
	private const float SyncRate = 2f;

	// Token: 0x04000B9B RID: 2971
	private float timer;

	// Token: 0x04000B9C RID: 2972
	public const byte StartCountdown = 128;

	// Token: 0x04000B9D RID: 2973
	public const byte AddUserOp = 64;

	// Token: 0x04000B9E RID: 2974
	public const byte RemoveUserOp = 32;

	// Token: 0x04000B9F RID: 2975
	public const byte ClearCountdown = 16;

	// Token: 0x04000BA0 RID: 2976
	public const float CountdownStopped = 10000f;

	// Token: 0x04000BA1 RID: 2977
	public readonly float ReactorDuration = 30f;

	// Token: 0x04000BA2 RID: 2978
	public const byte ConsoleIdMask = 3;

	// Token: 0x04000BA3 RID: 2979
	public const byte RequiredUserCount = 2;

	// Token: 0x04000BA5 RID: 2981
	private HashSet<Tuple<byte, byte>> UserConsolePairs = new HashSet<Tuple<byte, byte>>();

	// Token: 0x04000BA6 RID: 2982
	private SystemTypes system;
}
