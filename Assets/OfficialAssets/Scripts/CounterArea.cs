using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class CounterArea : MonoBehaviour
{
	// Token: 0x06000B59 RID: 2905 RVA: 0x000483D4 File Offset: 0x000465D4
	public void UpdateCount(int cnt)
	{
		bool flag = this.myIcons.Count != cnt;
		while (this.myIcons.Count < cnt)
		{
			PoolableBehavior item = this.pool.Get<PoolableBehavior>();
			this.myIcons.Add(item);
		}
		while (this.myIcons.Count > cnt)
		{
			PoolableBehavior poolableBehavior = this.myIcons[this.myIcons.Count - 1];
			this.myIcons.RemoveAt(this.myIcons.Count - 1);
			poolableBehavior.OwnerPool.Reclaim(poolableBehavior);
		}
		if (flag)
		{
			for (int i = 0; i < this.myIcons.Count; i++)
			{
				int num = i % 5;
				int num2 = i / 5;
				float num3 = (float)(Mathf.Min(cnt - num2 * 5, 5) - 1) * this.XOffset / -2f;
				this.myIcons[i].transform.position = base.transform.position + new Vector3(num3 + (float)num * this.XOffset, (float)num2 * this.YOffset, -1f);
			}
		}
	}

	// Token: 0x04000CCF RID: 3279
	public SystemTypes RoomType;

	// Token: 0x04000CD0 RID: 3280
	public ObjectPoolBehavior pool;

	// Token: 0x04000CD1 RID: 3281
	private List<PoolableBehavior> myIcons = new List<PoolableBehavior>();

	// Token: 0x04000CD2 RID: 3282
	public float XOffset = 0.3f;

	// Token: 0x04000CD3 RID: 3283
	public float YOffset = 0.3f;

	// Token: 0x04000CD4 RID: 3284
	public int MaxWidth = 5;
}
