using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class DetectTamper
{
	// Token: 0x06000318 RID: 792 RVA: 0x000145FF File Offset: 0x000127FF
	public static bool Detect()
	{
		return !Application.genuineCheckAvailable || Application.genuine;
	}
}
