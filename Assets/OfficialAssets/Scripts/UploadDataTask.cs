using System;
using System.Linq;
using System.Text;

// Token: 0x0200022F RID: 559
public class UploadDataTask : NormalPlayerTask
{
	// Token: 0x06000D58 RID: 3416 RVA: 0x00051218 File Offset: 0x0004F418
	public override bool ValidConsole(global::Console console)
	{
		return (console.Room == this.StartAt && console.ValidTasks.Any((TaskSet set) => this.TaskType == set.taskType && set.taskStep.Contains(this.taskStep))) || (this.taskStep == 1 && console.TaskTypes.Contains(this.TaskType));
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0005126C File Offset: 0x0004F46C
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
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString((this.taskStep == 0) ? this.StartAt : this.EndAt));
		sb.Append(": ");
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString((this.taskStep == 0) ? StringNames.DownloadData : StringNames.UploadData, Array.Empty<object>()));
		sb.Append(" (");
		sb.Append(this.taskStep);
		sb.Append("/");
		sb.Append(this.MaxStep);
		sb.AppendLine(") []");
	}

	// Token: 0x04000ED7 RID: 3799
	public SystemTypes EndAt = SystemTypes.Admin;
}
