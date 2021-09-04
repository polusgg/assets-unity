using System;
using Hazel;

// Token: 0x020001F2 RID: 498
internal class HudOverrideSystemType : ISystemType, IActivatable
{
	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x000498E0 File Offset: 0x00047AE0
	// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x000498E8 File Offset: 0x00047AE8
	public bool IsActive { get; private set; }

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x000498F1 File Offset: 0x00047AF1
	// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x000498F9 File Offset: 0x00047AF9
	public bool IsDirty { get; private set; }

	// Token: 0x06000BB5 RID: 2997 RVA: 0x00049902 File Offset: 0x00047B02
	public void Detoriorate(float deltaTime)
	{
		if (this.IsActive && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			PlayerControl.LocalPlayer.AddSystemTask(SystemTypes.Comms);
		}
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00049924 File Offset: 0x00047B24
	public void RepairDamage(PlayerControl player, byte amount)
	{
		if ((amount & 128) > 0)
		{
			this.IsActive = true;
		}
		else
		{
			this.IsActive = false;
		}
		this.IsDirty = true;
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x00049949 File Offset: 0x00047B49
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write(this.IsActive);
		this.IsDirty = initialState;
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0004995E File Offset: 0x00047B5E
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.IsActive = reader.ReadBoolean();
	}

	// Token: 0x04000D11 RID: 3345
	public const byte DamageBit = 128;

	// Token: 0x04000D12 RID: 3346
	public const byte TaskMask = 127;
}
