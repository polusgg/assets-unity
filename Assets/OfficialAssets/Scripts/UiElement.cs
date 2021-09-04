using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000E0 RID: 224
public abstract class UiElement : MonoBehaviour
{
	// Token: 0x06000597 RID: 1431 RVA: 0x00024E2F File Offset: 0x0002302F
	public virtual void ReceiveMouseOut()
	{
		this.OnMouseOut.Invoke();
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00024E3C File Offset: 0x0002303C
	public virtual void ReceiveMouseOver()
	{
		this.OnMouseOver.Invoke();
	}

	// Token: 0x04000631 RID: 1585
	public UnityEvent OnMouseOver;

	// Token: 0x04000632 RID: 1586
	public UnityEvent OnMouseOut;
}
