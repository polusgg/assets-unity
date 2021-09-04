using System;
using System.Collections.Generic;

// Token: 0x0200022A RID: 554
public abstract class SabotageTask : PlayerTask
{
	// Token: 0x06000D4F RID: 3407 RVA: 0x00050F7D File Offset: 0x0004F17D
	public void MarkContributed()
	{
		this.didContribute = true;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00050F88 File Offset: 0x0004F188
	protected void SetupArrows()
	{
		if (base.Owner.AmOwner)
		{
			List<global::Console> list = base.FindConsoles();
			for (int i = 0; i < list.Count; i++)
			{
				int consoleId = list[i].ConsoleId;
				this.Arrows[consoleId].target = list[i].transform.position;
				this.Arrows[consoleId].gameObject.SetActive(true);
			}
			return;
		}
		for (int j = 0; j < this.Arrows.Length; j++)
		{
			this.Arrows[j].gameObject.SetActive(false);
		}
	}

	// Token: 0x04000E95 RID: 3733
	protected bool didContribute;

	// Token: 0x04000E96 RID: 3734
	public ArrowBehaviour[] Arrows;
}
