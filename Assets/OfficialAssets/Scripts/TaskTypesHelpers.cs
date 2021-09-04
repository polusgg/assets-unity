using System;
using System.Linq;

// Token: 0x0200022D RID: 557
public static class TaskTypesHelpers
{
	// Token: 0x04000ED6 RID: 3798
	public static readonly TaskTypes[] AllTypes = Enum.GetValues(typeof(TaskTypes)).Cast<TaskTypes>().ToArray<TaskTypes>();
}
