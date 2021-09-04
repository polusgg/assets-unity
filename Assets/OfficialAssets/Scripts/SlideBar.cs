using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200020C RID: 524
public class SlideBar : PassiveUiElement
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0004C652 File Offset: 0x0004A852
	public override bool HandleDrag
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0004C658 File Offset: 0x0004A858
	public void OnEnable()
	{
		if (this.Title)
		{
			this.Title.Color = Color.white;
		}
		if (this.Bar)
		{
			this.Bar.color = Color.white;
		}
		this.Dot.color = Color.white;
		this.UpdateValue();
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
	public void OnDisable()
	{
		if (this.Title)
		{
			this.Title.Color = Color.gray;
		}
		if (this.Bar)
		{
			this.Bar.color = Color.gray;
		}
		this.Dot.color = Color.gray;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0004C710 File Offset: 0x0004A910
	public override void ReceiveClickDrag(Vector2 dragDelta)
	{
		//Vector3 localPosition = this.Dot.transform.localPosition;
		//Vector2 vector = DestroyableSingleton<PassiveButtonManager>.Instance.controller.DragPosition - this.Bar.transform.position;
		//if (this.Vertical)
		//{
		//	localPosition.y = this.Range.Clamp(vector.y);
		//	this.Value = this.Range.ReverseLerp(localPosition.y);
		//}
		//else
		//{
		//	localPosition.x = this.Range.Clamp(vector.x);
		//	this.Value = this.Range.ReverseLerp(localPosition.x);
		//}
		//this.UpdateValue();
		//this.OnValueChange.Invoke();
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
	public void UpdateValue()
	{
		Vector3 localPosition = this.Dot.transform.localPosition;
		if (this.Vertical)
		{
			localPosition.y = this.Range.Lerp(this.Value);
		}
		else
		{
			localPosition.x = this.Range.Lerp(this.Value);
		}
		this.Dot.transform.localPosition = localPosition;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0004C83D File Offset: 0x0004AA3D
	public void SetValue(float newValue)
	{
		this.Value = newValue;
		this.UpdateValue();
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0004C84C File Offset: 0x0004AA4C
	public void ControllerIncrease()
	{
		float num = this.Value + this.sliderSegmentIncrement;
		this.Value = Mathf.Clamp(num, 0f, 1f);
		Debug.Log("ControllerIncrease: " + this.Value.ToString());
		this.UpdateValue();
		this.OnValueChange.Invoke();
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0004C8A8 File Offset: 0x0004AAA8
	public void ControllerDecrease()
	{
		float num = this.Value - this.sliderSegmentIncrement;
		this.Value = Mathf.Clamp(num, 0f, 1f);
		Debug.Log("ControllerDecrease: " + this.Value.ToString());
		this.UpdateValue();
		this.OnValueChange.Invoke();
	}

	// Token: 0x04000DE4 RID: 3556
	[Space(20f)]
	public TextRenderer Title;

	// Token: 0x04000DE5 RID: 3557
	public SpriteRenderer Bar;

	// Token: 0x04000DE6 RID: 3558
	public SpriteRenderer Dot;

	// Token: 0x04000DE7 RID: 3559
	public FloatRange Range;

	// Token: 0x04000DE8 RID: 3560
	public bool Vertical;

	// Token: 0x04000DE9 RID: 3561
	public float Value;

	// Token: 0x04000DEA RID: 3562
	public UnityEvent OnValueChange;

	// Token: 0x04000DEB RID: 3563
	private float sliderSegmentIncrement = 0.1f;
}
