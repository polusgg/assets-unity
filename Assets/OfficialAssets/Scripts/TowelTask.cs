using System;
using System.Linq;
using System.Text;

// Token: 0x0200022E RID: 558
public class TowelTask : NormalPlayerTask
{
	// Token: 0x06000D55 RID: 3413 RVA: 0x00051080 File Offset: 0x0004F280
	public override bool ValidConsole(global::Console console)
	{
		if (this.TaskType == TaskTypes.PickUpTowels && console.TaskTypes.Contains(TaskTypes.PickUpTowels))
		{
			if (this.Data.IndexOf((byte b) => (int)b == console.ConsoleId) != -1)
			{
				return true;
			}
			if (this.Data.All((byte b) => b == 250) && console.ConsoleId == 255)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00051118 File Offset: 0x0004F318
	public override void AppendTaskText(StringBuilder sb)
	{
		int num = this.Data.Count((byte b) => b == 250);
		bool flag = num > 0;
		if (flag)
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
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.StartAt));
		sb.Append(": ");
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.TaskType));
		if (num < this.Data.Length)
		{
			sb.Append(" (");
			sb.Append(num);
			sb.Append("/");
			sb.Append(this.Data.Length);
			sb.Append(")");
		}
		if (flag)
		{
			sb.Append("[]");
		}
		sb.AppendLine();
	}
}
