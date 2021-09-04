using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class HeliSabotageSystem : MonoBehaviour, ISystemType, ICriticalSabotage, IActivatable
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000DC RID: 220 RVA: 0x00005B32 File Offset: 0x00003D32
	// (set) Token: 0x060000DD RID: 221 RVA: 0x00005B3A File Offset: 0x00003D3A
	public float Countdown { get; private set; } = 10000f;

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000DE RID: 222 RVA: 0x00005B43 File Offset: 0x00003D43
	public bool IsActive
	{
		get
		{
			return this.CompletedConsoles.Count < 2;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000DF RID: 223 RVA: 0x00005B53 File Offset: 0x00003D53
	public int UserCount
	{
		get
		{
			return this.CompletedConsoles.Count;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005B60 File Offset: 0x00003D60
	public float PercentActive
	{
		get
		{
			return this.Timer / 10f;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005B6E File Offset: 0x00003D6E
	// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005B76 File Offset: 0x00003D76
	public bool IsDirty { get; private set; }

	// Token: 0x060000E3 RID: 227 RVA: 0x00005B7F File Offset: 0x00003D7F
	public HeliSabotageSystem()
	{
		this.ClearSabotage();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00005BB9 File Offset: 0x00003DB9
	public void ClearSabotage()
	{
		this.Countdown = 10000f;
		this.CompletedConsoles.Add(0);
		this.CompletedConsoles.Add(1);
		this.IsDirty = true;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00005BE8 File Offset: 0x00003DE8
	public void Detoriorate(float deltaTime)
	{
		if (this.IsActive)
		{
			this.UpdateHeliSize();
			this.Countdown -= deltaTime;
			this.Timer -= deltaTime;
			if (this.Timer <= 0f)
			{
				this.TargetNumber = IntRange.Next(0, 99999);
				this.Timer = 10f;
				this.CompletedConsoles.Clear();
			}
			if (!PlayerTask.PlayerHasTaskOfType<ReactorTask>(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.AddSystemTask(SystemTypes.Reactor);
			}
			this.syncTimer -= deltaTime;
			if (this.syncTimer < 0f)
			{
				this.syncTimer = 2f;
				this.IsDirty = true;
				return;
			}
		}
		else
		{
			this.Helicopter.gameObject.SetActive(false);
			DestroyableSingleton<HudManager>.Instance.StopReactorFlash();
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00005CB8 File Offset: 0x00003EB8
	private void UpdateHeliSize()
	{
		float num = this.ScaleCurve.Evaluate(1f - this.Countdown / this.CharlesDuration);
		this.Helicopter.gameObject.SetActive(true);
		if (num > 0.8f)
		{
			num -= 0.8f;
			float num2 = Mathf.Lerp(0.05f, 1.2f, num);
			this.Helicopter.transform.localScale = new Vector3(num2, num2, num2);
			Vector3 localPosition = this.Helicopter.transform.localPosition;
			localPosition.y = Mathf.Lerp(2.17f, 1.5f, num);
			this.Helicopter.transform.localPosition = localPosition;
			return;
		}
		this.Helicopter.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		Vector3 localPosition2 = this.Helicopter.transform.localPosition;
		localPosition2.y = 2.17f;
		this.Helicopter.transform.localPosition = localPosition2;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00005DBC File Offset: 0x00003FBC
	internal bool IsConsoleActive(int consoleId)
	{
		return this.ActiveConsoles.Any((Tuple<byte, byte> s) => s.Item2 == (byte)consoleId);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00005DED File Offset: 0x00003FED
	internal bool IsConsoleOkay(int consoleId)
	{
		return this.CompletedConsoles.Contains((byte)consoleId);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00005DFC File Offset: 0x00003FFC
	public void RepairDamage(PlayerControl player, byte amount)
	{
		//byte b = amount & 15;
		//HeliSabotageSystem.Tags tags = (HeliSabotageSystem.Tags)(amount & 240);
		//if (tags <= HeliSabotageSystem.Tags.DeactiveBit)
		//{
		//	if (tags != HeliSabotageSystem.Tags.FixBit)
		//	{
		//		if (tags == HeliSabotageSystem.Tags.DeactiveBit)
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
		//else if (tags != HeliSabotageSystem.Tags.ActiveBit)
		//{
		//	if (tags == HeliSabotageSystem.Tags.DamageBit)
		//	{
		//		this.Timer = -1f;
		//		this.Countdown = this.CharlesDuration;
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

	// Token: 0x060000EA RID: 234 RVA: 0x00005EBC File Offset: 0x000040BC
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write(this.Countdown);
		writer.Write(this.Timer);
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

	// Token: 0x060000EB RID: 235 RVA: 0x00005FA4 File Offset: 0x000041A4
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.Countdown = reader.ReadSingle();
		this.Timer = reader.ReadSingle();
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

	// Token: 0x040000D9 RID: 217
	public SpriteRenderer Helicopter;

	// Token: 0x040000DA RID: 218
	public AnimationCurve ScaleCurve;

	// Token: 0x040000DB RID: 219
	public const byte TagMask = 240;

	// Token: 0x040000DC RID: 220
	public const byte IdMask = 15;

	// Token: 0x040000DD RID: 221
	private HashSet<Tuple<byte, byte>> ActiveConsoles = new HashSet<Tuple<byte, byte>>();

	// Token: 0x040000DE RID: 222
	private HashSet<byte> CompletedConsoles = new HashSet<byte>();

	// Token: 0x040000DF RID: 223
	private const float ActiveTime = 10f;

	// Token: 0x040000E0 RID: 224
	private float Timer;

	// Token: 0x040000E1 RID: 225
	private const float SyncRate = 2f;

	// Token: 0x040000E2 RID: 226
	private float syncTimer;

	// Token: 0x040000E3 RID: 227
	public const float CountdownStopped = 10000f;

	// Token: 0x040000E4 RID: 228
	public readonly float CharlesDuration = 90f;

	// Token: 0x040000E6 RID: 230
	public int TargetNumber;

	// Token: 0x020002CF RID: 719
	public enum Tags
	{
		// Token: 0x04001650 RID: 5712
		DamageBit = 128,
		// Token: 0x04001651 RID: 5713
		ActiveBit = 64,
		// Token: 0x04001652 RID: 5714
		DeactiveBit = 32,
		// Token: 0x04001653 RID: 5715
		FixBit = 16
	}
}
