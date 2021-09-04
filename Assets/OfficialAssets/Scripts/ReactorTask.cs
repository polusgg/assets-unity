using System;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class ReactorTask : SabotageTask
{
	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00050DAE File Offset: 0x0004EFAE
	public override int TaskStep
	{
		get
		{
			return this.reactor.UserCount;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00050DBB File Offset: 0x0004EFBB
	public override bool IsComplete
	{
		get
		{
			return this.isComplete;
		}
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x00050DC3 File Offset: 0x0004EFC3
	public override void Initialize()
	{
		base.SetupArrows();
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x00050DCC File Offset: 0x0004EFCC
	public void Awake()
	{
		this.reactor = (ICriticalSabotage)ShipStatus.Instance.Systems[this.StartAt];
		DestroyableSingleton<HudManager>.Instance.StartReactorFlash();
		ReactorShipRoom reactorShipRoom = ShipStatus.Instance.AllRooms.FirstOrDefault((PlainShipRoom r) => r.RoomId == this.StartAt) as ReactorShipRoom;
		if (reactorShipRoom != null)
		{
			reactorShipRoom.StartMeltdown();
		}
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x00050E33 File Offset: 0x0004F033
	private void FixedUpdate()
	{
		if (this.isComplete)
		{
			return;
		}
		if (!this.reactor.IsActive)
		{
			this.Complete();
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00050E51 File Offset: 0x0004F051
	public override bool ValidConsole(global::Console console)
	{
		return console.TaskTypes.Contains(this.TaskType);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00050E64 File Offset: 0x0004F064
	public override void OnRemove()
	{
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00050E68 File Offset: 0x0004F068
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

	// Token: 0x06000D4C RID: 3404 RVA: 0x00050EA4 File Offset: 0x0004F0A4
	public override void AppendTaskText(StringBuilder sb)
	{
		this.even = !this.even;
		Color color = this.even ? Color.yellow : Color.red;
		sb.Append(color.ToTextColor());
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.TaskType));
		sb.Append(" ");
		sb.Append((int)this.reactor.Countdown);
		sb.AppendLine(string.Format(" ({0}/{1})[]", this.reactor.UserCount, 2));
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			this.Arrows[i].image.color = color;
		}
	}

	// Token: 0x04000E92 RID: 3730
	private bool isComplete;

	// Token: 0x04000E93 RID: 3731
	private ICriticalSabotage reactor;

	// Token: 0x04000E94 RID: 3732
	private bool even;
}
