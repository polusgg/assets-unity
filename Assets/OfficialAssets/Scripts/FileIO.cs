using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000179 RID: 377
public static class FileIO
{
	// Token: 0x060008A0 RID: 2208 RVA: 0x00038723 File Offset: 0x00036923
	public static string GetPlayerName()
	{
		return null;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00038728 File Offset: 0x00036928
	public static string FilterText(string input, string inputCompo = "")
	{
		char c = ' ';
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Clear();
		foreach (char c2 in input)
		{
			if (c != ' ' || c2 != ' ')
			{
				if (c2 != '\r')
				{
				}
				if (c2 == '\b')
				{
					stringBuilder.Length = Mathf.Max(stringBuilder.Length - 1, 0);
				}
				if (FileIO.IsCharAllowed(c2))
				{
					stringBuilder.Append(c2);
					c = c2;
				}
			}
		}
		stringBuilder.Length = Mathf.Min(stringBuilder.Length, 10);
		input = stringBuilder.ToString();
		return input;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x000387B8 File Offset: 0x000369B8
	public static bool IsCharAllowed(char i)
	{
		return i == ' ' || (i >= 'A' && i <= 'Z') || (i >= 'a' && i <= 'z') || (i >= '0' && i <= '9') || (i >= 'À' && i <= 'ÿ') || (i >= 'Ѐ' && i <= 'џ') || (i >= '぀' && i <= '㆟') || (i >= 'ⱡ' && i <= '힣');
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00038839 File Offset: 0x00036A39
	public static bool Exists(string path)
	{
		return File.Exists(path);
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00038841 File Offset: 0x00036A41
	public static string ReadAllText(string path)
	{
		return File.ReadAllText(path);
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00038849 File Offset: 0x00036A49
	public static byte[] ReadAllBytes(string path)
	{
		return File.ReadAllBytes(path);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00038851 File Offset: 0x00036A51
	public static void WriteAllText(string path, string contents)
	{
		File.WriteAllText(path, contents);
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0003885A File Offset: 0x00036A5A
	public static void WriteAllBytes(string path, byte[] bytes)
	{
		File.WriteAllBytes(path, bytes);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00038863 File Offset: 0x00036A63
	public static void Delete(string path)
	{
		File.Delete(path);
	}
}
