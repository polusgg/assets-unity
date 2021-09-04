using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public abstract class PassiveUiElement : UiElement
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000574 RID: 1396 RVA: 0x000248ED File Offset: 0x00022AED
	public virtual bool HandleUp
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000575 RID: 1397 RVA: 0x000248F0 File Offset: 0x00022AF0
	public virtual bool HandleDown
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000576 RID: 1398 RVA: 0x000248F3 File Offset: 0x00022AF3
	public virtual bool HandleRepeat
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000577 RID: 1399 RVA: 0x000248F6 File Offset: 0x00022AF6
	public virtual bool HandleDrag
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000578 RID: 1400 RVA: 0x000248F9 File Offset: 0x00022AF9
	public virtual bool HandleOverOut
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x000248FC File Offset: 0x00022AFC
	public void Start()
	{
		DestroyableSingleton<PassiveButtonManager>.Instance.RegisterOne(this);
		if (this.Colliders == null || this.Colliders.Length == 0)
		{
			this.Colliders = base.GetComponents<Collider2D>();
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00024926 File Offset: 0x00022B26
	public void OnDestroy()
	{
		if (DestroyableSingleton<PassiveButtonManager>.InstanceExists)
		{
			DestroyableSingleton<PassiveButtonManager>.Instance.RemoveOne(this);
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0002493A File Offset: 0x00022B3A
	public virtual void ReceiveClickDown()
	{
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0002493C File Offset: 0x00022B3C
	public virtual void ReceiveRepeatDown()
	{
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0002493E File Offset: 0x00022B3E
	public virtual void ReceiveClickUp()
	{
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00024940 File Offset: 0x00022B40
	public virtual void ReceiveClickDrag(Vector2 dragDelta)
	{
	}

	// Token: 0x04000621 RID: 1569
	public Collider2D ClickMask;

	// Token: 0x04000622 RID: 1570
	public Collider2D[] Colliders;
}
