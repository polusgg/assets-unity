using System;
using System.Collections.Generic;

// Token: 0x02000054 RID: 84
public static class MemSafeStringExtensions
{
	// Token: 0x0600024F RID: 591 RVA: 0x0000FA75 File Offset: 0x0000DC75
	public static void SafeSplit(this SubString subString, List<SubString> output, char delim)
	{
		subString.Source.SafeSplit(output, delim, subString.Start, subString.Length);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000FA90 File Offset: 0x0000DC90
	public static void SafeSplit(this string source, List<SubString> output, char delim)
	{
		source.SafeSplit(output, delim, 0, source.Length);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
	public static void SafeSplit(this string source, List<SubString> output, char delim, int start, int length)
	{
		output.Clear();
		int num = start;
		int num2 = start + length;
		for (int i = start; i < num2; i++)
		{
			if (source[i] == delim)
			{
				if (num != i)
				{
					output.Add(new SubString(source, num, i - num));
				}
				num = i + 1;
			}
		}
		if (num != num2)
		{
			output.Add(new SubString(source, num, num2 - num));
		}
	}
}
