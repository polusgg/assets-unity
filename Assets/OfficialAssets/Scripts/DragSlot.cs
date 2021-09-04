using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class DragSlot : MonoBehaviour
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B610 File Offset: 0x00009810
	public Vector3 TargetPosition
	{
		get
		{
			return base.transform.position + this.Offset;
		}
	}

	// Token: 0x04000207 RID: 519
	public Vector3 Offset;

	// Token: 0x04000208 RID: 520
	public Behaviour Occupant;
}
