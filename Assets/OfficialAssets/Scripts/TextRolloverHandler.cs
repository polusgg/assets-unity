using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000DF RID: 223
public class TextRolloverHandler : MonoBehaviour
{
	// Token: 0x06000593 RID: 1427 RVA: 0x00024D7E File Offset: 0x00022F7E
	public void Start()
	{
		PassiveButton component = base.GetComponent<PassiveButton>();
		component.OnMouseOver.AddListener(new UnityAction(this.DoMouseOver));
		component.OnMouseOut.AddListener(new UnityAction(this.DoMouseOut));
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00024DB3 File Offset: 0x00022FB3
	public void DoMouseOver()
	{
		this.Target.Color = this.OverColor;
		this.Target.OutlineColor = this.OverOutlineColor;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00024DD7 File Offset: 0x00022FD7
	public void DoMouseOut()
	{
		this.Target.Color = this.OutColor;
		this.Target.OutlineColor = this.OutOutlineColor;
	}

	// Token: 0x0400062C RID: 1580
	public TextRenderer Target;

	// Token: 0x0400062D RID: 1581
	public Color OverColor = Color.green;

	// Token: 0x0400062E RID: 1582
	public Color OverOutlineColor = Color.white;

	// Token: 0x0400062F RID: 1583
	public Color OutColor = Color.white;

	// Token: 0x04000630 RID: 1584
	public Color OutOutlineColor = Color.white;
}
