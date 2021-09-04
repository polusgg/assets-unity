using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class EnableVSync : MonoBehaviour
{
	// Token: 0x0600089E RID: 2206 RVA: 0x00038713 File Offset: 0x00036913
	private void Awake()
	{
		QualitySettings.vSyncCount = 1;
	}
}
