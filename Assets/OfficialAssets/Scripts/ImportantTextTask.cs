using System;
using System.Text;

// Token: 0x02000225 RID: 549
public class ImportantTextTask : PlayerTask
{
	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0004FCCF File Offset: 0x0004DECF
	public override int TaskStep
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0004FCD2 File Offset: 0x0004DED2
	public override bool IsComplete
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0004FCD5 File Offset: 0x0004DED5
	public override void Initialize()
	{
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0004FCD7 File Offset: 0x0004DED7
	public override bool ValidConsole(global::Console console)
	{
		return false;
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0004FCDA File Offset: 0x0004DEDA
	public override void Complete()
	{
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0004FCDC File Offset: 0x0004DEDC
	public override void AppendTaskText(StringBuilder sb)
	{
		sb.AppendLine("[FF0000FF]" + this.Text + "[]");
	}

	// Token: 0x04000E7C RID: 3708
	public string Text;
}
