using System;
using System.Linq;
using Hazel;

// Token: 0x020001E4 RID: 484
public class AutoDoorsSystemType : ISystemType, IActivatable, RunTimer, IDoorSystem
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00048711 File Offset: 0x00046911
	public bool IsActive
	{
		get
		{
			return ShipStatus.Instance.AllDoors.Any((PlainDoor b) => !b.Open);
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00048741 File Offset: 0x00046941
	public bool IsDirty
	{
		get
		{
			return this.dirtyBits > 0U;
		}
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0004874C File Offset: 0x0004694C
	public void Detoriorate(float deltaTime)
	{
		for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
		{
			if (ShipStatus.Instance.AllDoors[i].DoUpdate(deltaTime))
			{
				this.dirtyBits |= 1U << i;
			}
		}
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00048797 File Offset: 0x00046997
	public void RepairDamage(PlayerControl player, byte amount)
	{
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0004879C File Offset: 0x0004699C
	public void Serialize(MessageWriter writer, bool initialState)
	{
		if (initialState)
		{
			for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
			{
				ShipStatus.Instance.AllDoors[i].Serialize(writer);
			}
			return;
		}
		writer.WritePacked(this.dirtyBits);
		for (int j = 0; j < ShipStatus.Instance.AllDoors.Length; j++)
		{
			if ((this.dirtyBits & 1U << j) != 0U)
			{
				ShipStatus.Instance.AllDoors[j].Serialize(writer);
			}
		}
		this.dirtyBits = 0U;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00048824 File Offset: 0x00046A24
	public void Deserialize(MessageReader reader, bool initialState)
	{
		if (initialState)
		{
			for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
			{
				ShipStatus.Instance.AllDoors[i].Deserialize(reader);
			}
			return;
		}
		uint num = reader.ReadPackedUInt32();
		for (int j = 0; j < ShipStatus.Instance.AllDoors.Length; j++)
		{
			if ((num & 1U << j) != 0U)
			{
				ShipStatus.Instance.AllDoors[j].Deserialize(reader);
			}
		}
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00048898 File Offset: 0x00046A98
	public void SetDoor(AutoOpenDoor door, bool open)
	{
		door.SetDoorway(open);
		this.dirtyBits |= 1U << ShipStatus.Instance.AllDoors.IndexOf(door);
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x000488C4 File Offset: 0x00046AC4
	public void CloseDoorsOfType(SystemTypes room)
	{
		for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
		{
			PlainDoor plainDoor = ShipStatus.Instance.AllDoors[i];
			if (plainDoor.Room == room)
			{
				plainDoor.SetDoorway(false);
				this.dirtyBits |= 1U << i;
			}
		}
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00048918 File Offset: 0x00046B18
	public float GetTimer(SystemTypes room)
	{
		for (int i = 0; i < ShipStatus.Instance.AllDoors.Length; i++)
		{
			PlainDoor plainDoor = ShipStatus.Instance.AllDoors[i];
			if (plainDoor.Room == room)
			{
				return ((AutoOpenDoor)plainDoor).CooldownTimer;
			}
		}
		return 0f;
	}

	// Token: 0x04000CDA RID: 3290
	private uint dirtyBits;
}
