using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200004B RID: 75
public class DefaultPool : IObjectPool
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060001DB RID: 475 RVA: 0x0000E571 File Offset: 0x0000C771
	public override int InUse
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060001DC RID: 476 RVA: 0x0000E574 File Offset: 0x0000C774
	public override int NotInUse
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060001DD RID: 477 RVA: 0x0000E577 File Offset: 0x0000C777
	public static bool InstanceExists
	{
		get
		{
			return DefaultPool._instance;
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060001DE RID: 478 RVA: 0x0000E584 File Offset: 0x0000C784
	public static DefaultPool Instance
	{
		get
		{
			object @lock = DefaultPool._lock;
			DefaultPool instance;
			lock (@lock)
			{
				if (DefaultPool._instance == null)
				{
					DefaultPool._instance = Object.FindObjectOfType<DefaultPool>();
					if (Object.FindObjectsOfType<DefaultPool>().Length > 1)
					{
						Debug.LogError("[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopening the scene might fix it.");
						return DefaultPool._instance;
					}
					if (DefaultPool._instance == null)
					{
						GameObject gameObject = new GameObject();
						DefaultPool._instance = gameObject.AddComponent<DefaultPool>();
						gameObject.name = "(singleton) DefaultPool";
					}
				}
				instance = DefaultPool._instance;
			}
			return instance;
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000E620 File Offset: 0x0000C820
	public void OnDestroy()
	{
		object @lock = DefaultPool._lock;
		lock (@lock)
		{
			DefaultPool._instance = null;
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000E660 File Offset: 0x0000C860
	public override T Get<T>()
	{
		throw new NotImplementedException();
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000E667 File Offset: 0x0000C867
	public override void Reclaim(PoolableBehavior obj)
	{
		Debug.Log("Default Pool: Destroying this thing.");
		Object.Destroy(obj.gameObject);
	}

	// Token: 0x040002D4 RID: 724
	private static DefaultPool _instance;

	// Token: 0x040002D5 RID: 725
	private static object _lock = new object();
}
