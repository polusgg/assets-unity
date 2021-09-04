using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000221 RID: 545
public class ElectricTask : SabotageTask
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0004F898 File Offset: 0x0004DA98
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

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0004F8A5 File Offset: 0x0004DAA5
	public override bool IsComplete
	{
		get
		{
			return this.isComplete;
		}
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0004F8B0 File Offset: 0x0004DAB0
	public override void Initialize()
	{
		ShipStatus instance = ShipStatus.Instance;
		this.system = (SwitchSystem)instance.Systems[SystemTypes.Electrical];
		base.SetupArrows();
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0004F8E0 File Offset: 0x0004DAE0
	private void FixedUpdate()
	{
		if (this.isComplete)
		{
			return;
		}
		if (this.system.ExpectedSwitches == this.system.ActualSwitches)
		{
			this.Complete();
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0004F909 File Offset: 0x0004DB09
	public override bool ValidConsole(global::Console console)
	{
		return console.TaskTypes.Contains(TaskTypes.FixLights);
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0004F918 File Offset: 0x0004DB18
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

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0004F954 File Offset: 0x0004DB54
	public override void AppendTaskText(StringBuilder sb)
	{
		this.even = !this.even;
		Color color = this.even ? Color.yellow : Color.red;
		sb.Append(color.ToTextColor());
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(TaskTypes.FixLights));
		sb.AppendLine(" (%" + ((int)(this.system.Level * 100f)).ToString() + ")[]");
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			if (this.Arrows[i].isActiveAndEnabled)
			{
				this.Arrows[i].image.color = color;
			}
		}
	}

	// Token: 0x04000E73 RID: 3699
	private bool isComplete;

	// Token: 0x04000E74 RID: 3700
	private SwitchSystem system;

	// Token: 0x04000E75 RID: 3701
	private bool even;
}
