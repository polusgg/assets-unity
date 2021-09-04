using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class TaskPanelBehaviour : DestroyableSingleton<TaskPanelBehaviour>
{
	// Token: 0x06000CEB RID: 3307 RVA: 0x0004F4E4 File Offset: 0x0004D6E4
	private void Update()
	{
		//this.background.transform.localScale = new Vector3(this.TaskText.Width + 0.2f, this.TaskText.Height + 0.2f, 1f);
		//Vector3 vector = this.background.sprite.bounds.extents;
		//vector.y = -vector.y;
		//vector = vector.Mul(this.background.transform.localScale);
		//this.background.transform.localPosition = vector;
		//Vector3 vector2 = this.tab.sprite.bounds.extents;
		//vector2 = vector2.Mul(this.tab.transform.localScale);
		//vector2.y = -vector2.y;
		//vector2.x += vector.x * 2f;
		//this.tab.transform.localPosition = vector2;
		//this.ClosedPosition.y = (this.OpenPosition.y = 0.6f);
		//this.ClosedPosition.x = -this.background.sprite.bounds.size.x * this.background.transform.localScale.x;
		//if (this.open)
		//{
		//	this.timer = Mathf.Min(1f, this.timer + Time.deltaTime / this.Duration);
		//}
		//else
		//{
		//	this.timer = Mathf.Max(0f, this.timer - Time.deltaTime / this.Duration);
		//}
		//Vector3 relativePos;
		//relativePos..ctor(Mathf.SmoothStep(this.ClosedPosition.x, this.OpenPosition.x, this.timer), Mathf.SmoothStep(this.ClosedPosition.y, this.OpenPosition.y, this.timer), this.OpenPosition.z);
		//base.transform.localPosition = AspectPosition.ComputePosition(AspectPosition.EdgeAlignments.LeftTop, relativePos);
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0004F6F8 File Offset: 0x0004D8F8
	public void ToggleOpen()
	{
		this.open = !this.open;
	}

	// Token: 0x04000E6A RID: 3690
	public Vector3 OpenPosition;

	// Token: 0x04000E6B RID: 3691
	public Vector3 ClosedPosition;

	// Token: 0x04000E6C RID: 3692
	public SpriteRenderer background;

	// Token: 0x04000E6D RID: 3693
	public SpriteRenderer tab;

	// Token: 0x04000E6E RID: 3694
	public TextRenderer TaskText;

	// Token: 0x04000E6F RID: 3695
	public bool open;

	// Token: 0x04000E70 RID: 3696
	private float timer;

	// Token: 0x04000E71 RID: 3697
	public float Duration;
}
