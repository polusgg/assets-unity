using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class AirshipUploadTask : NormalPlayerTask
{
	// Token: 0x06000112 RID: 274 RVA: 0x00006FE0 File Offset: 0x000051E0
	public override bool ValidConsole(global::Console console)
	{
		return (console.Room == this.StartAt && console.ValidTasks.Any((TaskSet set) => this.TaskType == set.taskType && set.taskStep.Contains(this.taskStep))) || (this.taskStep == 1 && console.TaskTypes.Contains(this.TaskType));
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00007034 File Offset: 0x00005234
	protected override void FixedUpdate()
	{
		if (this.Arrows[0].isActiveAndEnabled && PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.arrowSuspended = true;
			this.Arrows.SetAllGameObjectsActive(false);
			return;
		}
		if (this.arrowSuspended && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.arrowSuspended = false;
			this.Arrows.SetAllGameObjectsActive(true);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00007098 File Offset: 0x00005298
	public override void UpdateArrow()
	{
		if (this.taskStep == 0 || this.IsComplete || !base.Owner.AmOwner)
		{
			this.Arrows.SetAllGameObjectsActive(false);
			return;
		}
		if (PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.arrowSuspended = true;
		}
		else
		{
			this.Arrows.SetAllGameObjectsActive(true);
		}
		List<Vector2> list = base.FindValidConsolesPositions();
		int num = 0;
		while (num < list.Count && num < this.Arrows.Length)
		{
			this.Arrows[num].target = list[num];
			num++;
		}
		this.LocationDirty = true;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00007134 File Offset: 0x00005334
	public override void AppendTaskText(StringBuilder sb)
	{
		if (this.taskStep > 0)
		{
			if (this.IsComplete)
			{
				sb.Append("[00DD00FF]");
			}
			else
			{
				sb.Append("[FFFF00FF]");
			}
		}
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString((this.taskStep == 0) ? this.StartAt : SystemTypes.Outside));
		sb.Append(": ");
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString((this.taskStep == 0) ? StringNames.DownloadData : StringNames.UploadData, Array.Empty<object>()));
		sb.Append(" (");
		sb.Append(this.taskStep);
		sb.Append("/");
		sb.Append(this.MaxStep);
		sb.AppendLine(") []");
	}

	// Token: 0x04000123 RID: 291
	public ArrowBehaviour[] Arrows;
}
