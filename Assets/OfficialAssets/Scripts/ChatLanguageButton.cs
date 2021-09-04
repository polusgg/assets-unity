using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class ChatLanguageButton : PoolableBehavior
{
	// Token: 0x060006AA RID: 1706 RVA: 0x0002ACD0 File Offset: 0x00028ED0
	public void SetSelected(bool selected)
	{
		base.GetComponent<ButtonRolloverHandler>().OutColor = (this.Background.color = (selected ? Color.white : Color.black));
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0002AD05 File Offset: 0x00028F05
	public override void Reset()
	{
		FontCache.Instance.SetFont(this.Text, "Arial");
		base.Reset();
	}

	// Token: 0x04000777 RID: 1911
	public TextRenderer Text;

	// Token: 0x04000778 RID: 1912
	public PassiveButton Button;

	// Token: 0x04000779 RID: 1913
	public SpriteRenderer Background;
}
