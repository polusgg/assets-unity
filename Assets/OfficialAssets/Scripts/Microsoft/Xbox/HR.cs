using System;

namespace Microsoft.Xbox
{
	// Token: 0x020002A4 RID: 676
	internal class HR
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0006345F File Offset: 0x0006165F
		internal static bool SUCCEEDED(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00063468 File Offset: 0x00061668
		internal static bool FAILED(int hr)
		{
			return hr < 0;
		}
	}
}
