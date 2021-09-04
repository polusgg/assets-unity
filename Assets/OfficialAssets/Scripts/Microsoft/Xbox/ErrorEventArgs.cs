using System;

namespace Microsoft.Xbox
{
	// Token: 0x020002A6 RID: 678
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0006347E File Offset: 0x0006167E
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x00063486 File Offset: 0x00061686
		public string ErrorCode { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0006348F File Offset: 0x0006168F
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00063497 File Offset: 0x00061697
		public string ErrorMessage { get; private set; }

		// Token: 0x060012FF RID: 4863 RVA: 0x000634A0 File Offset: 0x000616A0
		public ErrorEventArgs(string errorCode, string errorMessage)
		{
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}
	}
}
