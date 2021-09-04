using System;
using System.Linq;
using System.Text;

// Token: 0x02000220 RID: 544
public class DivertPowerTask : NormalPlayerTask
{
	// Token: 0x06000CEE RID: 3310 RVA: 0x0004F714 File Offset: 0x0004D914
	public override bool ValidConsole(global::Console console)
	{
		return (console.Room == this.TargetSystem && console.ValidTasks.Any((TaskSet set) => set.Contains(this))) || (this.taskStep == 0 && console.TaskTypes.Contains(this.TaskType));
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0004F768 File Offset: 0x0004D968
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
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.StartAt));
			sb.Append(": ");
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DivertPowerTo, new object[]
			{
				DestroyableSingleton<TranslationController>.Instance.GetString(this.TargetSystem)
			}));
		}
		else
		{
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.TargetSystem));
			sb.Append(": ");
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.AcceptDivertedPower, Array.Empty<object>()));
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

	// Token: 0x04000E72 RID: 3698
	public SystemTypes TargetSystem;
}
