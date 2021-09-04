using System;
using System.Collections.Generic;
using Hazel;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public class DoorsSystemType : ISystemType, IActivatable, RunTimer, IDoorSystem
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000489F9 File Offset: 0x00046BF9
	public bool IsActive
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000489FC File Offset: 0x00046BFC
	// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00048A04 File Offset: 0x00046C04
	public bool IsDirty { get; private set; }

	// Token: 0x06000B78 RID: 2936 RVA: 0x00048A10 File Offset: 0x00046C10
	public void Detoriorate(float deltaTime)
	{
		for (int i = 0; i < SystemTypeHelpers.AllTypes.Length; i++)
		{
			SystemTypes key = SystemTypeHelpers.AllTypes[i];
			float num;
			if (this.timers.TryGetValue(key, out num))
			{
				this.timers[key] = Mathf.Clamp(num - deltaTime, 0f, 30f);
			}
		}
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00048A68 File Offset: 0x00046C68
	public void RepairDamage(PlayerControl player, byte amount)
	{
		int num = (int)(amount & 31);
		int num2 = (int)(amount & 192);
		if (num2 != 64)
		{
			if (num2 != 128)
			{
			}
		}
		else if (num < ShipStatus.Instance.AllDoors.Length)
		{
			ShipStatus.Instance.AllDoors[num].SetDoorway(true);
		}
		else
		{
			Debug.LogWarning(string.Format("Couldn't find door {0}", num));
		}
		this.IsDirty = true;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00048AD0 File Offset: 0x00046CD0
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write((byte)this.timers.Count);
		foreach (KeyValuePair<SystemTypes, float> keyValuePair in this.timers)
		{
			writer.Write((byte)keyValuePair.Key);
			writer.Write(keyValuePair.Value);
		}
		for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
		{
			ShipStatus.Instance.AllDoors[i].Serialize(writer);
		}
		this.IsDirty = initialState;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00048B78 File Offset: 0x00046D78
	public void Deserialize(MessageReader reader, bool initialState)
	{
		int num = (int)reader.ReadByte();
		for (int i = 0; i < num; i++)
		{
			SystemTypes key = (SystemTypes)reader.ReadByte();
			float value = reader.ReadSingle();
			this.timers[key] = value;
		}
		for (int j = 0; j < ShipStatus.Instance.AllDoors.Length; j++)
		{
			ShipStatus.Instance.AllDoors[j].Deserialize(reader);
		}
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00048BE4 File Offset: 0x00046DE4
	public void CloseDoorsOfType(SystemTypes room)
	{
		this.timers[room] = 30f;
		for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
		{
			PlainDoor plainDoor = ShipStatus.Instance.AllDoors[i];
			if (plainDoor.Room == room)
			{
				plainDoor.SetDoorway(false);
			}
		}
		this.IsDirty = true;
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00048C40 File Offset: 0x00046E40
	public virtual float GetTimer(SystemTypes room)
	{
		float result;
		if (this.timers.TryGetValue(room, out result))
		{
			return result;
		}
		return 0f;
	}

	// Token: 0x04000CDF RID: 3295
	public const byte CloseDoors = 128;

	// Token: 0x04000CE0 RID: 3296
	public const byte OpenDoor = 64;

	// Token: 0x04000CE1 RID: 3297
	private const byte ActionMask = 192;

	// Token: 0x04000CE2 RID: 3298
	private const byte IdMask = 31;

	// Token: 0x04000CE3 RID: 3299
	private Dictionary<SystemTypes, float> timers = new Dictionary<SystemTypes, float>();
}
