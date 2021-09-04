using System;
using System.Linq;

// Token: 0x02000207 RID: 519
public static class SystemTypeHelpers
{
	// Token: 0x04000DD0 RID: 3536
	public static readonly SystemTypes[] AllTypes = Enum.GetValues(typeof(SystemTypes)).Cast<SystemTypes>().ToArray<SystemTypes>();
}
