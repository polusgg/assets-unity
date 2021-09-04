using System;
using System.Linq;
using System.Text;

// Token: 0x02000231 RID: 561
public class WeatherNodeTask : NormalPlayerTask
{
	// Token: 0x06000D5F RID: 3423 RVA: 0x000513B4 File Offset: 0x0004F5B4
	public override bool ValidConsole(global::Console console)
	{
		return (this.taskStep == 0 && console.ConsoleId == this.NodeId && console.TaskTypes.Contains(this.TaskType)) || console.ValidTasks.Any((TaskSet t) => t.Contains(this));
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00051403 File Offset: 0x0004F603
	public override Minigame GetMinigamePrefab()
	{
		if (this.taskStep == 0)
		{
			return this.MinigamePrefab;
		}
		return this.Stage2Prefab;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0005141C File Offset: 0x0004F61C
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
		if (this.taskStep == 0)
		{
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(WeatherSwitchGame.ControlNames[this.NodeId], Array.Empty<object>()));
			sb.Append(": ");
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.FixWeatherNode, Array.Empty<object>()));
		}
		else
		{
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.StartAt));
			sb.Append(": ");
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.FixWeatherNode, Array.Empty<object>()));
		}
		sb.Append(" (");
		sb.Append(this.taskStep);
		sb.Append("/");
		sb.Append(this.MaxStep);
		sb.AppendLine(")");
		if (this.taskStep > 0)
		{
			sb.Append("[]");
		}
	}

	// Token: 0x04000ED8 RID: 3800
	public int NodeId;

	// Token: 0x04000ED9 RID: 3801
	public Minigame Stage2Prefab;
}
