using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class PoolableBehavior : MonoBehaviour
{
	// Token: 0x06000265 RID: 613 RVA: 0x0001050A File Offset: 0x0000E70A
	public virtual void Reset()
	{
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0001050C File Offset: 0x0000E70C
	public void Awake()
	{
		this.OwnerPool = DefaultPool.Instance;
	}

	// Token: 0x040002FE RID: 766
	[HideInInspector]
	public IObjectPool OwnerPool;
}
