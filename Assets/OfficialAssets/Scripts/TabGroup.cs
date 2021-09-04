using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class TabGroup : MonoBehaviour
{
	// Token: 0x06000714 RID: 1812 RVA: 0x0002D188 File Offset: 0x0002B388
	internal void Close()
	{
		//Color color;
		//color..ctor(0.3f, 0.3f, 0.3f);
		//this.Button.color = color;
		//this.Rollover.OutColor = color;
		//this.Content.SetActive(false);
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0002D1CF File Offset: 0x0002B3CF
	internal void Open()
	{
		this.Button.color = Color.white;
		this.Rollover.OutColor = Color.white;
		this.Content.SetActive(true);
	}

	// Token: 0x040007FA RID: 2042
	public SpriteRenderer Button;

	// Token: 0x040007FB RID: 2043
	public ButtonRolloverHandler Rollover;

	// Token: 0x040007FC RID: 2044
	public GameObject Content;
}
