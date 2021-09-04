using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class VirtualJoystick : MonoBehaviour, IVirtualJoystick
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060004BA RID: 1210 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
	// (set) Token: 0x060004BB RID: 1211 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
	public Vector2 Delta { get; private set; }

	// Token: 0x060004BC RID: 1212 RVA: 0x0001E104 File Offset: 0x0001C304
	protected virtual void FixedUpdate()
	{
		//this.myController.Update();
		//switch (this.myController.CheckDrag(this.Outer))
		//{
		//case DragState.TouchStart:
		//case DragState.Dragging:
		//{
		//	float num = this.OuterRadius - this.InnerRadius;
		//	Vector2 vector = this.myController.DragPosition - base.transform.position;
		//	float magnitude = vector.magnitude;
		//	Vector2 vector2;
		//	vector2..ctor(Mathf.Sqrt(Mathf.Abs(vector.x)) * Mathf.Sign(vector.x), Mathf.Sqrt(Mathf.Abs(vector.y)) * Mathf.Sign(vector.y));
		//	this.Delta = Vector2.ClampMagnitude(vector2 / this.OuterRadius, 1f);
		//	this.Inner.transform.localPosition = Vector3.ClampMagnitude(vector, num) + Vector3.back;
		//	return;
		//}
		//case DragState.Holding:
		//	break;
		//case DragState.Released:
		//	this.Delta = Vector2.zero;
		//	this.Inner.transform.localPosition = Vector3.back;
		//	break;
		//default:
		//	return;
		//}
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0001E220 File Offset: 0x0001C420
	public virtual void UpdateJoystick(FingerBehaviour finger, Vector2 velocity, bool syncFinger)
	{
		Vector3 vector = this.Inner.transform.localPosition;
		Vector3 vector2 = velocity.normalized * this.InnerRadius;
		vector2.z = vector.z;
		if (syncFinger)
		{
			vector = Vector3.Lerp(vector, vector2, Time.fixedDeltaTime * 5f);
			this.Inner.transform.localPosition = vector;
			vector = this.Inner.transform.position;
			vector.z = -26f;
			finger.transform.position = vector;
			return;
		}
		if (this.Inner.gameObject != finger.gameObject)
		{
			this.Inner.transform.localPosition = vector2;
		}
	}

	// Token: 0x04000586 RID: 1414
	public float InnerRadius = 0.64f;

	// Token: 0x04000587 RID: 1415
	public float OuterRadius = 1.28f;

	// Token: 0x04000588 RID: 1416
	public CircleCollider2D Outer;

	// Token: 0x04000589 RID: 1417
	public SpriteRenderer Inner;

	// Token: 0x0400058B RID: 1419
	private Controller myController = new Controller();
}
