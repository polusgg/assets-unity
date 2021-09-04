using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000223 RID: 547
public class HqHudOverrideTask : SabotageTask, IHudOverrideTask
{
	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0004FA12 File Offset: 0x0004DC12
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

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0004FA1F File Offset: 0x0004DC1F
	public override bool IsComplete
	{
		get
		{
			return this.isComplete;
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0004FA28 File Offset: 0x0004DC28
	public override void Initialize()
	{
		ShipStatus instance = ShipStatus.Instance;
		this.system = (instance.Systems[SystemTypes.Comms] as HqHudSystemType);
		base.SetupArrows();
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0004FA59 File Offset: 0x0004DC59
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

	// Token: 0x06000CFE RID: 3326 RVA: 0x0004FA77 File Offset: 0x0004DC77
	public override bool ValidConsole(global::Console console)
	{
		return console.TaskTypes.Contains(TaskTypes.FixComms);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0004FA88 File Offset: 0x0004DC88
	public override void Complete()
	{
		SecurityLogBehaviour component = ShipStatus.Instance.GetComponent<SecurityLogBehaviour>();
		if (component)
		{
			component.LogEntries.Clear();
		}
		this.isComplete = true;
		PlayerControl.LocalPlayer.RemoveTask(this);
		if (this.didContribute)
		{
			StatsManager instance = StatsManager.Instance;
			uint sabsFixed = instance.SabsFixed;
			instance.SabsFixed = sabsFixed + 1U;
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0004FAE4 File Offset: 0x0004DCE4
	public override void AppendTaskText(StringBuilder sb)
	{
		this.even = !this.even;
		Color color = this.even ? Color.yellow : Color.red;
		sb.Append(color.ToTextColor());
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(TaskTypes.FixComms));
		sb.Append(string.Format(" ({0}/2)", this.system.NumComplete));
		sb.Append("[]");
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			this.Arrows[i].image.color = color;
		}
	}

	// Token: 0x04000E76 RID: 3702
	private bool isComplete;

	// Token: 0x04000E77 RID: 3703
	private HqHudSystemType system;

	// Token: 0x04000E78 RID: 3704
	private bool even;
}
