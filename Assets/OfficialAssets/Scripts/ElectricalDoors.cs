using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class ElectricalDoors : MonoBehaviour, ISystemType
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000594C File Offset: 0x00003B4C
	// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005954 File Offset: 0x00003B54
	public bool IsDirty { get; private set; }

	// Token: 0x060000D6 RID: 214 RVA: 0x00005960 File Offset: 0x00003B60
	[ContextMenu("Shuffle Doors")]
	public void Initialize()
	{
		HashSet<ElectricalDoors.DoorSet> hashSet = new HashSet<ElectricalDoors.DoorSet>();
		StaticDoor[] doors = this.Doors;
		for (int i = 0; i < doors.Length; i++)
		{
			doors[i].SetOpen(false);
		}
		ElectricalDoors.DoorSet room = this.Rooms[0];
		int num = 0;
		while (hashSet.Count < this.Rooms.Length && num++ < 10000)
		{
			StaticDoor door = room.Doors.Random<StaticDoor>();
			ElectricalDoors.DoorSet doorSet = this.Rooms.First((ElectricalDoors.DoorSet r) => r != room && r.Doors.Contains(door));
			if (hashSet.Add(doorSet))
			{
				door.SetOpen(true);
			}
			if (BoolRange.Next(0.5f))
			{
				hashSet.Add(room);
				room = doorSet;
			}
		}
		bool flag = BoolRange.Next(0.5f);
		this.LeftExits.Doors[0].SetOpen(flag);
		this.LeftExits.Doors[1].SetOpen(!flag);
		this.IsDirty = true;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00005A98 File Offset: 0x00003C98
	public void Detoriorate(float deltaTime)
	{
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00005A9A File Offset: 0x00003C9A
	public void RepairDamage(PlayerControl player, byte amount)
	{
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00005A9C File Offset: 0x00003C9C
	public void Serialize(MessageWriter writer, bool initialState)
	{
		uint num = 0U;
		for (int i = 0; i < this.Doors.Length; i++)
		{
			num |= (this.Doors[i].IsOpen ? 1U : 0U) << i;
		}
		writer.Write(num);
		this.IsDirty = initialState;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00005AE8 File Offset: 0x00003CE8
	public void Deserialize(MessageReader reader, bool initialState)
	{
		uint num = reader.ReadUInt32();
		for (int i = 0; i < this.Doors.Length; i++)
		{
			this.Doors[i].SetOpen(((ulong)num & (ulong)(1L << (i & 31))) > 0UL);
		}
	}

	// Token: 0x040000D5 RID: 213
	public StaticDoor[] Doors;

	// Token: 0x040000D6 RID: 214
	public ElectricalDoors.DoorSet[] Rooms;

	// Token: 0x040000D7 RID: 215
	public ElectricalDoors.DoorSet LeftExits;

	// Token: 0x020002CC RID: 716
	[Serializable]
	public class DoorSet
	{
		// Token: 0x060013C6 RID: 5062 RVA: 0x00065B23 File Offset: 0x00063D23
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0400164A RID: 5706
		public string Name;

		// Token: 0x0400164B RID: 5707
		public StaticDoor[] Doors;
	}
}
