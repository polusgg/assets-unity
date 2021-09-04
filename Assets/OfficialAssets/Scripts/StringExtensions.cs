using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public static class StringExtensions
{
	// Token: 0x060002B3 RID: 691 RVA: 0x00011520 File Offset: 0x0000F720
	public static string Lerp(string a, string b, float t)
	{
		int num = Mathf.Max(a.Length, b.Length);
		int num2 = (int)Mathf.Lerp(0f, (float)num, t);
		for (int i = 0; i < num; i++)
		{
			if (i < num2)
			{
				if (i < b.Length)
				{
					StringExtensions.buffer[i] = b[i];
				}
				else
				{
					StringExtensions.buffer[i] = ' ';
				}
			}
			else if (i < a.Length)
			{
				StringExtensions.buffer[i] = a[i];
			}
		}
		return new string(StringExtensions.buffer, 0, num);
	}

	// Token: 0x04000323 RID: 803
	private static char[] buffer = new char[256];
}
