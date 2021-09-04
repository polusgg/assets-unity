using System;
using Hazel;

// Token: 0x0200008D RID: 141
public class SwitchSystem : ISystemType, IActivatable
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x0600035D RID: 861 RVA: 0x00016431 File Offset: 0x00014631
	// (set) Token: 0x0600035E RID: 862 RVA: 0x00016439 File Offset: 0x00014639
	public bool IsDirty { get; private set; }

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600035F RID: 863 RVA: 0x00016442 File Offset: 0x00014642
	public float Level
	{
		get
		{
			return (float)this.Value / 255f;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000360 RID: 864 RVA: 0x00016451 File Offset: 0x00014651
	public bool IsActive
	{
		get
		{
			return this.ExpectedSwitches != this.ActualSwitches;
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00016464 File Offset: 0x00014664
	public SwitchSystem()
	{
		Random random = new Random();
		this.ExpectedSwitches = (byte)(random.Next() & 31);
		this.ActualSwitches = this.ExpectedSwitches;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x000164B0 File Offset: 0x000146B0
	public void Detoriorate(float deltaTime)
	{
		this.timer += deltaTime;
		if (this.timer >= this.DetoriorationTime)
		{
			this.timer = 0f;
			if (this.ExpectedSwitches != this.ActualSwitches)
			{
				if (this.Value > 0)
				{
					this.Value = (byte)Math.Max((int)(this.Value - 3), 0);
				}
				if (!SwitchSystem.HasTask<ElectricTask>())
				{
					PlayerControl.LocalPlayer.AddSystemTask(SystemTypes.Electrical);
					return;
				}
			}
			else if (this.Value < 255)
			{
				this.Value = (byte)Math.Min((int)(this.Value + 3), 255);
			}
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00016549 File Offset: 0x00014749
	public void RepairDamage(PlayerControl player, byte amount)
	{
		//if (amount.HasBit(128))
		//{
  //          (void)(this.ActualSwitches ^= (amount & 31));
		//}
		//else
		//{
		//	this.ActualSwitches ^= (byte)(1 << (int)amount);
		//}
		//this.IsDirty = true;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00016589 File Offset: 0x00014789
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write(this.ExpectedSwitches);
		writer.Write(this.ActualSwitches);
		writer.Write(this.Value);
		this.IsDirty = initialState;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x000165B6 File Offset: 0x000147B6
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.ExpectedSwitches = reader.ReadByte();
		this.ActualSwitches = reader.ReadByte();
		this.Value = reader.ReadByte();
	}

	// Token: 0x06000366 RID: 870 RVA: 0x000165DC File Offset: 0x000147DC
	protected static bool HasTask<T>()
	{
		for (int i = PlayerControl.LocalPlayer.myTasks.Count - 1; i > 0; i--)
		{
			if (PlayerControl.LocalPlayer.myTasks[i] is T)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040003F0 RID: 1008
	public const byte MaxValue = 255;

	// Token: 0x040003F1 RID: 1009
	public const int NumSwitches = 5;

	// Token: 0x040003F2 RID: 1010
	public const byte DamageSystem = 128;

	// Token: 0x040003F3 RID: 1011
	public const byte SwitchesMask = 31;

	// Token: 0x040003F4 RID: 1012
	public float DetoriorationTime = 0.03f;

	// Token: 0x040003F5 RID: 1013
	public byte Value = byte.MaxValue;

	// Token: 0x040003F6 RID: 1014
	private float timer;

	// Token: 0x040003F7 RID: 1015
	public byte ExpectedSwitches;

	// Token: 0x040003F8 RID: 1016
	public byte ActualSwitches;
}
