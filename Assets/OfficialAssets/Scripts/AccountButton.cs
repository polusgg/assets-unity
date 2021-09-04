using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AccountButton : PoolableBehavior
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void SetSelected(bool selected)
	{
		base.GetComponent<ButtonRolloverHandler>().OutColor = (this.Background.color = (selected ? Color.white : Color.black));
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002085 File Offset: 0x00000285
	public override void Reset()
	{
		FontCache.Instance.SetFont(this.Text, "Arial");
		base.Reset();
	}

	// Token: 0x04000001 RID: 1
	public TextRenderer Text;

	// Token: 0x04000002 RID: 2
	public PassiveButton Button;

	// Token: 0x04000003 RID: 3
	public SpriteRenderer Background;
}
