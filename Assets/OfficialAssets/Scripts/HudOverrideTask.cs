using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000224 RID: 548
public class HudOverrideTask : SabotageTask, IHudOverrideTask
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0004FB90 File Offset: 0x0004DD90
	public override int TaskStep
	{
		get
		{
			if (!this.isComplete)
			{
				return 0;
			}
			return 1;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0004FB9D File Offset: 0x0004DD9D
	public override bool IsComplete
	{
		get
		{
			return this.isComplete;
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0004FBA8 File Offset: 0x0004DDA8
	public override void Initialize()
	{
		ShipStatus instance = ShipStatus.Instance;
		this.system = (instance.Systems[SystemTypes.Comms] as HudOverrideSystemType);
		base.SetupArrows();
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0004FBD9 File Offset: 0x0004DDD9
	private void FixedUpdate()
	{
		if (this.isComplete)
		{
			return;
		}
		if (!this.system.IsActive)
		{
			this.Complete();
		}
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0004FBF7 File Offset: 0x0004DDF7
	public override bool ValidConsole(global::Console console)
	{
		return console.TaskTypes.Contains(TaskTypes.FixComms);
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0004FC08 File Offset: 0x0004DE08
	public override void Complete()
	{
		this.isComplete = true;
		PlayerControl.LocalPlayer.RemoveTask(this);
		if (this.didContribute)
		{
			StatsManager instance = StatsManager.Instance;
			uint sabsFixed = instance.SabsFixed;
			instance.SabsFixed = sabsFixed + 1U;
		}
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0004FC44 File Offset: 0x0004DE44
	public override void AppendTaskText(StringBuilder sb)
	{
		this.even = !this.even;
		Color color = this.even ? Color.yellow : Color.red;
		sb.Append(color.ToTextColor());
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(TaskTypes.FixComms));
		sb.Append("[]");
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			this.Arrows[i].image.color = color;
		}
	}

	// Token: 0x04000E79 RID: 3705
	private bool isComplete;

	// Token: 0x04000E7A RID: 3706
	private HudOverrideSystemType system;

	// Token: 0x04000E7B RID: 3707
	private bool even;
}
