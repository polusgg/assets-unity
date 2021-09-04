using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200021D RID: 541
public class GarbageBehaviour : MonoBehaviour
{
	// Token: 0x06000CE0 RID: 3296 RVA: 0x0004F141 File Offset: 0x0004D341
	public void FixedUpdate()
	{
		if (base.transform.localPosition.y < -3.49f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
