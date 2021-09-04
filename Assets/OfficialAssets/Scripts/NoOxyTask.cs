using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class NoOxyTask : SabotageTask
{
	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0004FD02 File Offset: 0x0004DF02
	public override int TaskStep
	{
		get
		{
			return this.reactor.UserCount;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0004FD0F File Offset: 0x0004DF0F
	public override bool IsComplete
	{
		get
		{
			return this.isComplete;
		}
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0004FD18 File Offset: 0x0004DF18
	public override void Initialize()
	{
		this.targetNumber = IntRange.Next(0, 99999);
		ShipStatus instance = ShipStatus.Instance;
		this.reactor = (LifeSuppSystemType)instance.Systems[SystemTypes.LifeSupp];
		DestroyableSingleton<HudManager>.Instance.StartOxyFlash();
		base.SetupArrows();
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0004FD64 File Offset: 0x0004DF64
	private void FixedUpdate()
	{
		if (this.isComplete)
		{
			return;
		}
		if (!this.reactor.IsActive)
		{
			this.Complete();
			return;
		}
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			this.Arrows[i].gameObject.SetActive(!this.reactor.GetConsoleComplete(i));
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0004FDC2 File Offset: 0x0004DFC2
	public override bool ValidConsole(global::Console console)
	{
		return !this.reactor.GetConsoleComplete(console.ConsoleId) && console.TaskTypes.Contains(TaskTypes.RestoreOxy);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0004FDE6 File Offset: 0x0004DFE6
	public override void OnRemove()
	{
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0004FDE8 File Offset: 0x0004DFE8
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

	// Token: 0x06000D18 RID: 3352 RVA: 0x0004FE24 File Offset: 0x0004E024
	public override void AppendTaskText(StringBuilder sb)
	{
		this.even = !this.even;
		Color color = this.even ? Color.yellow : Color.red;
		if (this.reactor != null)
		{
			sb.Append(color.ToTextColor());
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(TaskTypes.RestoreOxy));
			sb.Append(" ");
			sb.Append(Mathf.CeilToInt(this.reactor.Countdown));
			sb.AppendLine(string.Format(" ({0}/{1})[]", this.reactor.UserCount, 2));
		}
		else
		{
			sb.AppendLine(color.ToTextColor() + "Oxygen depleting[]");
		}
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			try
			{
				this.Arrows[i].image.color = color;
			}
			catch
			{
			}
		}
	}

	// Token: 0x04000E7D RID: 3709
	private bool isComplete;

	// Token: 0x04000E7E RID: 3710
	private LifeSuppSystemType reactor;

	// Token: 0x04000E7F RID: 3711
	private bool even;

	// Token: 0x04000E80 RID: 3712
	public int targetNumber;
}
