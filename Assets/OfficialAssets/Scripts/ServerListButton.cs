using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class ServerListButton : PoolableBehavior
{
	// Token: 0x0600073F RID: 1855 RVA: 0x0002DED0 File Offset: 0x0002C0D0
	public void SetSelected(bool selected)
	{
		base.GetComponent<ButtonRolloverHandler>().OutColor = (this.Background.color = (selected ? Color.white : Color.black));
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0002DF05 File Offset: 0x0002C105
	public override void Reset()
	{
		FontCache.Instance.SetFont(this.Text, "Arial");
		base.Reset();
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0002DF22 File Offset: 0x0002C122
	public void SetTextTranslationId(StringNames id, string defaultStr)
	{
		this.textTranslator.TargetText = id;
		this.textTranslator.defaultStr = defaultStr;
	}

	// Token: 0x04000831 RID: 2097
	public TextRenderer Text;

	// Token: 0x04000832 RID: 2098
	public PassiveButton Button;

	// Token: 0x04000833 RID: 2099
	public SpriteRenderer Background;

	// Token: 0x04000834 RID: 2100
	public TextTranslator textTranslator;
}
