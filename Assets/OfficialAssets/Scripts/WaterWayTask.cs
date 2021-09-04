using System;
using System.Linq;

// Token: 0x02000230 RID: 560
public class WaterWayTask : NormalPlayerTask
{
	// Token: 0x06000D5C RID: 3420 RVA: 0x0005136B File Offset: 0x0004F56B
	public override void Initialize()
	{
		base.Initialize();
		this.Data = new byte[3];
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0005137F File Offset: 0x0004F57F
	public override bool ValidConsole(global::Console console)
	{
		return console.TaskTypes.Contains(this.TaskType) && this.Data[console.ConsoleId] < byte.MaxValue;
	}
}
