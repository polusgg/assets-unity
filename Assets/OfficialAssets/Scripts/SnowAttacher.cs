using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000145 RID: 325
public class SnowAttacher : MonoBehaviour
{
	// Token: 0x060007B2 RID: 1970 RVA: 0x000314AD File Offset: 0x0002F6AD
	public void Start()
	{
		Object.Instantiate<GameObject>(this.SnowPrefab, DestroyableSingleton<HudManager>.Instance.transform).transform.localPosition = new Vector3(0f, 3f, 0f);
	}

	// Token: 0x040008B9 RID: 2233
	public GameObject SnowPrefab;
}
