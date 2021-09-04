using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
public class MedScannerBehaviour : MonoBehaviour
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000772 RID: 1906 RVA: 0x0002F693 File Offset: 0x0002D893
	public Vector3 Position
	{
		get
		{
			return base.transform.position + this.Offset;
		}
	}

	// Token: 0x0400086C RID: 2156
	public Vector3 Offset;
}
