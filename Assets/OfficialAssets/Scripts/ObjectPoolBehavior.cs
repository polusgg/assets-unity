using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000056 RID: 86
public class ObjectPoolBehavior : IObjectPool
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000254 RID: 596 RVA: 0x0000FB5E File Offset: 0x0000DD5E
	public override int InUse
	{
		get
		{
			return this.activeChildren.Count;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000255 RID: 597 RVA: 0x0000FB6B File Offset: 0x0000DD6B
	public override int NotInUse
	{
		get
		{
			return this.inactiveChildren.Count;
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000FB78 File Offset: 0x0000DD78
	public virtual void Awake()
	{
		if (this.AutoInit)
		{
			this.InitPool(this.Prefab);
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000FB90 File Offset: 0x0000DD90
	public void InitPool(PoolableBehavior prefab)
	{
		this.AutoInit = false;
		for (int i = 0; i < this.poolSize; i++)
		{
			this.CreateOneInactive(prefab);
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000FBBC File Offset: 0x0000DDBC
	private void CreateOneInactive(PoolableBehavior prefab)
	{
		PoolableBehavior poolableBehavior = Object.Instantiate<PoolableBehavior>(prefab);
		poolableBehavior.transform.SetParent(base.transform);
		poolableBehavior.gameObject.SetActive(false);
		poolableBehavior.OwnerPool = this;
		this.inactiveChildren.Add(poolableBehavior);
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000FC00 File Offset: 0x0000DE00
	public void ReclaimOldest()
	{
		if (this.activeChildren.Count > 0)
		{
			this.Reclaim(this.activeChildren[0]);
			return;
		}
		this.InitPool(this.Prefab);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000FC30 File Offset: 0x0000DE30
	public void ReclaimAll()
	{
		foreach (PoolableBehavior obj in this.activeChildren.ToArray())
		{
			this.Reclaim(obj);
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000FC64 File Offset: 0x0000DE64
	public override T Get<T>()
	{
		List<PoolableBehavior> obj = this.inactiveChildren;
		PoolableBehavior poolableBehavior;
		lock (obj)
		{
			if (this.inactiveChildren.Count == 0)
			{
				if (this.activeChildren.Count == 0)
				{
					this.InitPool(this.Prefab);
				}
				else
				{
					this.CreateOneInactive(this.Prefab);
				}
			}
			poolableBehavior = this.inactiveChildren[this.inactiveChildren.Count - 1];
			this.inactiveChildren.RemoveAt(this.inactiveChildren.Count - 1);
			this.activeChildren.Add(poolableBehavior);
		}
		if (this.DetachOnGet)
		{
			poolableBehavior.transform.SetParent(null, false);
		}
		poolableBehavior.gameObject.SetActive(true);
		poolableBehavior.Reset();
		return poolableBehavior as T;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000FD44 File Offset: 0x0000DF44
	public override void Reclaim(PoolableBehavior obj)
	{
		if (!this)
		{
			DefaultPool.Instance.Reclaim(obj);
			return;
		}
		obj.gameObject.SetActive(false);
		obj.transform.SetParent(base.transform);
		List<PoolableBehavior> obj2 = this.inactiveChildren;
		lock (obj2)
		{
			if (this.activeChildren.Remove(obj))
			{
				this.inactiveChildren.Add(obj);
			}
			else if (this.inactiveChildren.Contains(obj))
			{
				Debug.Log("ObjectPoolBehavior: :| Something was reclaimed without being gotten");
			}
			else
			{
				Debug.Log("ObjectPoolBehavior: Destroying this thing I don't own");
				Object.Destroy(obj.gameObject);
			}
		}
	}

	// Token: 0x040002E1 RID: 737
	public int poolSize = 20;

	// Token: 0x040002E2 RID: 738
	[SerializeField]
	private List<PoolableBehavior> inactiveChildren = new List<PoolableBehavior>();

	// Token: 0x040002E3 RID: 739
	[SerializeField]
	public List<PoolableBehavior> activeChildren = new List<PoolableBehavior>();

	// Token: 0x040002E4 RID: 740
	public PoolableBehavior Prefab;

	// Token: 0x040002E5 RID: 741
	public bool AutoInit;

	// Token: 0x040002E6 RID: 742
	public bool DetachOnGet;
}
