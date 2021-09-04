using System;
using Hazel;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InnerNet
{
	// Token: 0x02000298 RID: 664
	public abstract class InnerNetObject : MonoBehaviour, IComparable<InnerNetObject>
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00062503 File Offset: 0x00060703
		public virtual bool IsDirty
		{
			get
			{
				return this.DirtyBits > 0U;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0006250E File Offset: 0x0006070E
		public bool AmOwner
		{
			get
			{
				return this.OwnerId == AmongUsClient.Instance.ClientId;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00062522 File Offset: 0x00060722
		public void Despawn()
		{
			AmongUsClient.Instance.Despawn(this);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0006253A File Offset: 0x0006073A
		public virtual void OnDestroy()
		{
			if (AmongUsClient.Instance && this.NetId != 4294967295U)
			{
				if (this.DespawnOnDestroy && this.AmOwner)
				{
					AmongUsClient.Instance.Despawn(this);
					return;
				}
				AmongUsClient.Instance.RemoveNetObject(this);
			}
		}

		// Token: 0x060012D3 RID: 4819
		public abstract void HandleRpc(byte callId, MessageReader reader);

		// Token: 0x060012D4 RID: 4820
		public abstract bool Serialize(MessageWriter writer, bool initialState);

		// Token: 0x060012D5 RID: 4821
		public abstract void Deserialize(MessageReader reader, bool initialState);

		// Token: 0x060012D6 RID: 4822 RVA: 0x00062578 File Offset: 0x00060778
		public int CompareTo(InnerNetObject other)
		{
			if (this.NetId > other.NetId)
			{
				return 1;
			}
			if (this.NetId < other.NetId)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0006259B File Offset: 0x0006079B
		protected bool IsDirtyBitSet(int idx)
		{
			return (this.DirtyBits & 1U << idx) > 0U;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000625AD File Offset: 0x000607AD
		protected void ClearDirtyBits()
		{
			this.DirtyBits = 0U;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000625B6 File Offset: 0x000607B6
		protected void SetDirtyBit(uint val)
		{
			this.DirtyBits |= val;
		}

		// Token: 0x0400155E RID: 5470
		public uint SpawnId;

		// Token: 0x0400155F RID: 5471
		public uint NetId;

		// Token: 0x04001560 RID: 5472
		protected uint DirtyBits;

		// Token: 0x04001561 RID: 5473
		public SpawnFlags SpawnFlags;

		// Token: 0x04001562 RID: 5474
		public SendOption sendMode = (SendOption)1;

		// Token: 0x04001563 RID: 5475
		public int OwnerId;

		// Token: 0x04001564 RID: 5476
		protected bool DespawnOnDestroy = true;
	}
}
