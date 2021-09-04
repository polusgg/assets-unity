using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class Scroller : PassiveUiElement
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000580 RID: 1408 RVA: 0x0002494A File Offset: 0x00022B4A
	public override bool HandleUp
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000581 RID: 1409 RVA: 0x0002494D File Offset: 0x00022B4D
	public override bool HandleDown
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000582 RID: 1410 RVA: 0x00024950 File Offset: 0x00022B50
	public override bool HandleDrag
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000583 RID: 1411 RVA: 0x00024953 File Offset: 0x00022B53
	public override bool HandleOverOut
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000584 RID: 1412 RVA: 0x00024956 File Offset: 0x00022B56
	public bool AtTop
	{
		get
		{
			return this.Inner.localPosition.y <= this.YBounds.min + 0.25f;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000585 RID: 1413 RVA: 0x0002497E File Offset: 0x00022B7E
	public bool AtBottom
	{
		get
		{
			return this.Inner.localPosition.y >= this.YBounds.max - 0.25f;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000586 RID: 1414 RVA: 0x000249A6 File Offset: 0x00022BA6
	public Collider2D Hitbox
	{
		get
		{
			return this.Colliders[0];
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x000249B0 File Offset: 0x00022BB0
	public void FixedUpdate()
	{
		if (!this.Inner)
		{
			return;
		}
		Vector2 mouseScrollDelta = Input.mouseScrollDelta;
		if (mouseScrollDelta.y != 0f)
		{
			mouseScrollDelta.y = -mouseScrollDelta.y;
			this.ScrollRelative(mouseScrollDelta);
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x000249F4 File Offset: 0x00022BF4
	public void Update()
	{
		if (!this.active && this.velocity.sqrMagnitude > 0.01f)
		{
			this.velocity = Vector2.ClampMagnitude(this.velocity, this.velocity.magnitude - 10f * Time.deltaTime);
			this.ScrollRelative(this.velocity * Time.deltaTime);
		}
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00024A5C File Offset: 0x00022C5C
	public void ScrollDown()
	{
		Collider2D collider2D = this.Colliders.First<Collider2D>();
		float num = collider2D.bounds.max.y - collider2D.bounds.min.y;
		this.ScrollRelative(new Vector2(0f, num * 0.75f));
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00024AB4 File Offset: 0x00022CB4
	public void ScrollUp()
	{
		Collider2D collider2D = this.Colliders.First<Collider2D>();
		float num = collider2D.bounds.max.y - collider2D.bounds.min.y;
		this.ScrollRelative(new Vector2(0f, num * -0.75f));
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00024B0C File Offset: 0x00022D0C
	public float GetScrollPercY()
	{
		if ((double)this.YBounds.Width < 0.0001)
		{
			return 1f;
		}
		Vector3 localPosition = this.Inner.transform.localPosition;
		return this.YBounds.ReverseLerp(localPosition.y);
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00024B58 File Offset: 0x00022D58
	public void ScrollPercentY(float p)
	{
		Vector3 localPosition = this.Inner.transform.localPosition;
		localPosition.y = this.YBounds.Lerp(p);
		this.Inner.transform.localPosition = localPosition;
		this.UpdateScrollBars(localPosition);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00024BA1 File Offset: 0x00022DA1
	public override void ReceiveClickDown()
	{
		this.active = true;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00024BAA File Offset: 0x00022DAA
	public override void ReceiveClickUp()
	{
		this.active = false;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00024BB3 File Offset: 0x00022DB3
	public override void ReceiveClickDrag(Vector2 dragDelta)
	{
		this.velocity = dragDelta / Time.deltaTime * 0.9f;
		this.ScrollRelative(dragDelta);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00024BD8 File Offset: 0x00022DD8
	public void ScrollRelative(Vector2 dragDelta)
	{
		if (dragDelta.magnitude < 0.05f)
		{
			return;
		}
		if (!this.allowX)
		{
			dragDelta.x = 0f;
		}
		if (!this.allowY)
		{
			dragDelta.y = 0f;
		}
		Vector3 vector = this.Inner.transform.localPosition + (Vector3) dragDelta;
		vector.x = this.XBounds.Clamp(vector.x);
		int childCount = this.Inner.transform.childCount;
		float num = Mathf.Max(this.YBounds.min, this.YBounds.max);
		vector.y = Mathf.Clamp(vector.y, this.YBounds.min, num);
		this.Inner.transform.localPosition = vector;
		this.UpdateScrollBars(vector);
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00024CB4 File Offset: 0x00022EB4
	private void UpdateScrollBars(Vector3 pos)
	{
		if (this.ScrollerY)
		{
			if (this.YBounds.min == this.YBounds.max)
			{
				this.ScrollerY.enabled = false;
				return;
			}
			this.ScrollerY.enabled = true;
			float num = this.YBounds.ReverseLerp(pos.y);
			Vector3 localPosition = this.ScrollerY.transform.localPosition;
			localPosition.y = this.ScrollerYRange.Lerp(1f - num);
			this.ScrollerY.transform.localPosition = localPosition;
		}
	}

	// Token: 0x04000623 RID: 1571
	public Transform Inner;

	// Token: 0x04000624 RID: 1572
	public bool allowX;

	// Token: 0x04000625 RID: 1573
	public FloatRange XBounds = new FloatRange(-10f, 10f);

	// Token: 0x04000626 RID: 1574
	public bool allowY;

	// Token: 0x04000627 RID: 1575
	public FloatRange YBounds = new FloatRange(-10f, 10f);

	// Token: 0x04000628 RID: 1576
	public FloatRange ScrollerYRange;

	// Token: 0x04000629 RID: 1577
	public SpriteRenderer ScrollerY;

	// Token: 0x0400062A RID: 1578
	private Vector2 velocity;

	// Token: 0x0400062B RID: 1579
	private bool active;
}
