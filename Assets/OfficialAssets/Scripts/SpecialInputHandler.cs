using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
public class SpecialInputHandler : MonoBehaviour
{
	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600092E RID: 2350 RVA: 0x0003C77D File Offset: 0x0003A97D
	// (set) Token: 0x0600092F RID: 2351 RVA: 0x0003C785 File Offset: 0x0003A985
	public bool disableVirtualCursor
	{
		get
		{
			return this._disableVirtualCursor;
		}
		set
		{
			if (this._disableVirtualCursor != value)
			{
				this._disableVirtualCursor = value;
				if (base.isActiveAndEnabled)
				{
					if (this._disableVirtualCursor)
					{
						SpecialInputHandler.disableVirtualCursorCount++;
						return;
					}
					SpecialInputHandler.disableVirtualCursorCount--;
				}
			}
		}
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0003C7C0 File Offset: 0x0003A9C0
	private void OnEnable()
	{
		SpecialInputHandler.count++;
		if (this.disableVirtualCursor)
		{
			SpecialInputHandler.disableVirtualCursorCount++;
		}
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0003C7E2 File Offset: 0x0003A9E2
	private void OnDisable()
	{
		SpecialInputHandler.count--;
		if (this.disableVirtualCursor)
		{
			SpecialInputHandler.disableVirtualCursorCount--;
		}
	}

	// Token: 0x04000AA4 RID: 2724
	public static int count;

	// Token: 0x04000AA5 RID: 2725
	public static int disableVirtualCursorCount;

	// Token: 0x04000AA6 RID: 2726
	[SerializeField]
	private bool _disableVirtualCursor;
}
