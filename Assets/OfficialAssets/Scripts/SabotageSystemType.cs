using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;

// Token: 0x020001FD RID: 509
public class SabotageSystemType : ISystemType
{
	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0004A422 File Offset: 0x00048622
	// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0004A42A File Offset: 0x0004862A
	public float Timer { get; set; }

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0004A433 File Offset: 0x00048633
	public float PercentCool
	{
		get
		{
			return this.Timer / 30f;
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000BED RID: 3053 RVA: 0x0004A441 File Offset: 0x00048641
	public bool AnyActive
	{
		get
		{
			return this.specials.Any((IActivatable s) => s.IsActive);
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0004A46D File Offset: 0x0004866D
	// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0004A475 File Offset: 0x00048675
	public bool IsDirty { get; private set; }

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0004A480 File Offset: 0x00048680
	public SabotageSystemType(IActivatable[] specials)
	{
		this.specials = new List<IActivatable>(specials);
		this.specials.RemoveAll((IActivatable d) => d is IDoorSystem);
		this.specials.Add(this.dummy);
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x0004A4E6 File Offset: 0x000486E6
	public void Detoriorate(float deltaTime)
	{
		this.dummy.timer -= deltaTime;
		if (this.Timer > 0f && !this.AnyActive)
		{
			this.Timer -= deltaTime;
			this.IsDirty = true;
		}
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x0004A525 File Offset: 0x00048725
	public void ForceSabTime(float t)
	{
		this.dummy.timer = t;
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x0004A534 File Offset: 0x00048734
	public void RepairDamage(PlayerControl player, byte amount)
	{
		this.IsDirty = true;
		if (this.Timer > 0f)
		{
			return;
		}
		if (MeetingHud.Instance)
		{
			return;
		}
		if (AmongUsClient.Instance.AmHost)
		{
			if (amount <= 7)
			{
				if (amount != 3)
				{
					if (amount == 7)
					{
						byte b = 4;
						for (int i = 0; i < 5; i++)
						{
							if (BoolRange.Next(0.5f))
							{
								b |= (byte)(1 << i);
							}
						}
						ShipStatus.Instance.RpcRepairSystem(SystemTypes.Electrical, (int)(b | 128));
					}
				}
				else
				{
					ShipStatus.Instance.RepairSystem(SystemTypes.Reactor, player, 128);
				}
			}
			else if (amount != 8)
			{
				if (amount != 14)
				{
					if (amount == 21)
					{
						ShipStatus.Instance.RepairSystem(SystemTypes.Laboratory, player, 128);
					}
				}
				else
				{
					ShipStatus.Instance.RepairSystem(SystemTypes.Comms, player, 128);
				}
			}
			else
			{
				ShipStatus.Instance.RepairSystem(SystemTypes.LifeSupp, player, 128);
			}
		}
		this.Timer = 30f;
		this.IsDirty = true;
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x0004A62E File Offset: 0x0004882E
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write(this.Timer);
		this.IsDirty = initialState;
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x0004A643 File Offset: 0x00048843
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.Timer = reader.ReadSingle();
	}

	// Token: 0x04000D4E RID: 3406
	public const float SpecialSabDelay = 30f;

	// Token: 0x04000D50 RID: 3408
	private List<IActivatable> specials;

	// Token: 0x04000D52 RID: 3410
	private SabotageSystemType.DummySab dummy = new SabotageSystemType.DummySab();

	// Token: 0x0200043F RID: 1087
	public class DummySab : IActivatable
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000797B4 File Offset: 0x000779B4
		public bool IsActive
		{
			get
			{
				return this.timer > 0f;
			}
		}

		// Token: 0x04001C1A RID: 7194
		public float timer;
	}
}
