using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class NumberButton : PoolableBehavior
{
	// Token: 0x060000B8 RID: 184 RVA: 0x00004E60 File Offset: 0x00003060
	public void SetSelected(bool selected)
	{
		base.GetComponent<ButtonRolloverHandler>().OutColor = (this.Background.color = (selected ? Color.white : Color.black));
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00004E95 File Offset: 0x00003095
	public override void Reset()
	{
		FontCache.Instance.SetFont(this.Text, "Arial");
		base.Reset();
	}

	// Token: 0x040000AF RID: 175
	public TextRenderer Text;

	// Token: 0x040000B0 RID: 176
	public PassiveButton Button;

	// Token: 0x040000B1 RID: 177
	public SpriteRenderer Background;

	// Token: 0x040000B2 RID: 178
	public int monthNum = -1;
}
