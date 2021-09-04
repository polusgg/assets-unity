using System;
using System.Collections.Generic;
using Hazel;

// Token: 0x020001B7 RID: 439
public class LifeSuppSystemType : ISystemType, IActivatable
{
	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00041869 File Offset: 0x0003FA69
	// (set) Token: 0x06000A1A RID: 2586 RVA: 0x00041871 File Offset: 0x0003FA71
	public bool IsDirty { get; private set; }

	// Token: 0x06000A1B RID: 2587 RVA: 0x0004187A File Offset: 0x0003FA7A
	public LifeSuppSystemType(float duration)
	{
		this.LifeSuppDuration = duration;
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000418AA File Offset: 0x0003FAAA
	public int UserCount
	{
		get
		{
			return this.CompletedConsoles.Count;
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x000418B7 File Offset: 0x0003FAB7
	public bool GetConsoleComplete(int consoleId)
	{
		return this.CompletedConsoles.Contains(consoleId);
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000418C5 File Offset: 0x0003FAC5
	public bool IsActive
	{
		get
		{
			return this.Countdown < 10000f;
		}
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x000418D4 File Offset: 0x0003FAD4
	public void RepairDamage(PlayerControl player, byte opCode)
	{
		int item = (int)(opCode & 3);
		if (opCode == 128 && !this.IsActive)
		{
			this.Countdown = this.LifeSuppDuration;
			this.CompletedConsoles.Clear();
		}
		else if (opCode == 16)
		{
			this.Countdown = 10000f;
		}
		else if (opCode.HasAnyBit(64))
		{
			this.CompletedConsoles.Add(item);
		}
		this.IsDirty = true;
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00041940 File Offset: 0x0003FB40
	public void Detoriorate(float deltaTime)
	{
		if (this.IsActive)
		{
			if (DestroyableSingleton<HudManager>.Instance.OxyFlash == null)
			{
				PlayerControl.LocalPlayer.AddSystemTask(SystemTypes.LifeSupp);
			}
			this.Countdown -= deltaTime;
			if (this.UserCount >= 2)
			{
				this.Countdown = 10000f;
				this.IsDirty = true;
				return;
			}
			this.timer += deltaTime;
			if (this.timer > 2f)
			{
				this.timer = 0f;
				this.IsDirty = true;
				return;
			}
		}
		else if (DestroyableSingleton<HudManager>.Instance.OxyFlash != null)
		{
			DestroyableSingleton<HudManager>.Instance.StopOxyFlash();
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x000419DC File Offset: 0x0003FBDC
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write(this.Countdown);
		writer.WritePacked(this.CompletedConsoles.Count);
		foreach (int num in this.CompletedConsoles)
		{
			writer.WritePacked(num);
		}
		this.IsDirty = initialState;
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00041A54 File Offset: 0x0003FC54
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.Countdown = reader.ReadSingle();
		if (reader.Position < reader.Length)
		{
			this.CompletedConsoles.Clear();
			int num = reader.ReadPackedInt32();
			for (int i = 0; i < num; i++)
			{
				this.CompletedConsoles.Add(reader.ReadPackedInt32());
			}
		}
	}

	// Token: 0x04000B85 RID: 2949
	private const float SyncRate = 2f;

	// Token: 0x04000B86 RID: 2950
	private float timer;

	// Token: 0x04000B87 RID: 2951
	public const byte StartCountdown = 128;

	// Token: 0x04000B88 RID: 2952
	public const byte AddUserOp = 64;

	// Token: 0x04000B89 RID: 2953
	public const byte ClearCountdown = 16;

	// Token: 0x04000B8A RID: 2954
	public const float CountdownStopped = 10000f;

	// Token: 0x04000B8B RID: 2955
	public readonly float LifeSuppDuration = 45f;

	// Token: 0x04000B8C RID: 2956
	public const byte ConsoleIdMask = 3;

	// Token: 0x04000B8D RID: 2957
	public const byte RequiredUserCount = 2;

	// Token: 0x04000B8E RID: 2958
	public float Countdown = 10000f;

	// Token: 0x04000B8F RID: 2959
	private HashSet<int> CompletedConsoles = new HashSet<int>();
}
