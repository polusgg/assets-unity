using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class DialBehaviour : MonoBehaviour
{
	// Token: 0x060001A8 RID: 424 RVA: 0x0000D41C File Offset: 0x0000B61C
	public void Update()
	{
		//this.Engaged = false;
		//this.myController.Update();
		//if (this.myController.CheckDrag(this.collider) == DragState.Dragging)
		//{
		//	Vector2 vector = this.myController.DragPosition - base.transform.position;
		//	float num = Vector2.up.AngleSigned(vector);
		//	if (num < -180f)
		//	{
		//		num += 360f;
		//	}
		//	num = this.DialRange.Clamp(num);
		//	this.SetValue(num);
		//	this.Engaged = true;
		//}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
	public void SetValue(float angle)
	{
		//this.Value = angle;
		//Vector3 localEulerAngles;
		//localEulerAngles..ctor(0f, 0f, angle);
		//this.DialTrans.localEulerAngles = localEulerAngles;
		//this.DialShadTrans.localEulerAngles = localEulerAngles;
	}

	// Token: 0x04000280 RID: 640
	public FloatRange DialRange;

	// Token: 0x04000281 RID: 641
	public Collider2D collider;

	// Token: 0x04000282 RID: 642
	public Controller myController = new Controller();

	// Token: 0x04000283 RID: 643
	public float Value;

	// Token: 0x04000284 RID: 644
	public bool Engaged;

	// Token: 0x04000285 RID: 645
	public Transform DialTrans;

	// Token: 0x04000286 RID: 646
	public Transform DialShadTrans;
}
