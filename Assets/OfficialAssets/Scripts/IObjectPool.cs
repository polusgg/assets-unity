using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public abstract class IObjectPool : MonoBehaviour
{
	// Token: 0x06000243 RID: 579
	public abstract T Get<T>() where T : PoolableBehavior;

	// Token: 0x06000244 RID: 580
	public abstract void Reclaim(PoolableBehavior obj);

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000245 RID: 581
	public abstract int InUse { get; }

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000246 RID: 582
	public abstract int NotInUse { get; }
}
