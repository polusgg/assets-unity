using System;
using System.Collections.Generic;

namespace InnerNet
{
	// Token: 0x0200028F RID: 655
	public static class CryptoHelpers
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0005FC90 File Offset: 0x0005DE90
		public static byte[] DecodePEM(string pemData)
		{
			List<byte> list = new List<byte>();
			pemData = pemData.Replace("\r", "");
			foreach (string text in pemData.Split(new char[]
			{
				'\n'
			}))
			{
				if (!text.StartsWith("-----"))
				{
					byte[] collection = Convert.FromBase64String(text);
					list.AddRange(collection);
				}
			}
			return list.ToArray();
		}
	}
}
