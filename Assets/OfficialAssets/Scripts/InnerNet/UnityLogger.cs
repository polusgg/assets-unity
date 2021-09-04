using System;
using UnityEngine;

namespace InnerNet
{
	// Token: 0x0200029C RID: 668
	public class UnityLogger : Hazel.ILogger
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x000625DC File Offset: 0x000607DC
		public void WriteError(string msg)
		{
			Debug.LogError(msg);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000625E4 File Offset: 0x000607E4
		public void WriteInfo(string msg)
		{
			Debug.Log(msg);
		}

        public void WriteVerbose(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
