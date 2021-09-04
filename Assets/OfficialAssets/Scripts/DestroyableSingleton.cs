using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200004C RID: 76
public class DestroyableSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000E692 File Offset: 0x0000C892
	public static bool InstanceExists
	{
		get
		{
			return DestroyableSingleton<T>._instance;
		}
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
	public virtual void Awake()
	{
		if (!DestroyableSingleton<T>._instance)
		{
			DestroyableSingleton<T>._instance = (this as T);
			if (this.DontDestroy)
			{
				Object.DontDestroyOnLoad(base.gameObject);
				return;
			}
		}
		else if (DestroyableSingleton<T>._instance != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000E704 File Offset: 0x0000C904
	public static T Instance
	{
		get
		{
			if (!DestroyableSingleton<T>._instance)
			{
				DestroyableSingleton<T>._instance = Object.FindObjectOfType<T>();
				if (!DestroyableSingleton<T>._instance)
				{
					DestroyableSingleton<T>._instance = new GameObject().AddComponent<T>();
				}
			}
			return DestroyableSingleton<T>._instance;
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000E751 File Offset: 0x0000C951
	public virtual void OnDestroy()
	{
		if (!this.DontDestroy)
		{
			DestroyableSingleton<T>._instance = default(T);
		}
	}

	// Token: 0x040002D6 RID: 726
	private static T _instance;

	// Token: 0x040002D7 RID: 727
	public bool DontDestroy;
}
