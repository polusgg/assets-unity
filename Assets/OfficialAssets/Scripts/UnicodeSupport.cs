using System;
using System.Text;

// Token: 0x020001A0 RID: 416
public static class UnicodeSupport
{
	// Token: 0x0600093E RID: 2366 RVA: 0x0003C8D8 File Offset: 0x0003AAD8
	public static string FilterUnsupportedCharacters(TextRenderer tr, FontData data)
	{
		UnicodeSupport.sb.Clear();
		for (int i = 0; i < tr.Text.Length; i++)
		{
			if (tr.Text[i] > ' ' && !data.charMap.ContainsKey((int)tr.Text[i]))
			{
				UnicodeSupport.sb.Append('□');
			}
			else
			{
				UnicodeSupport.sb.Append(tr.Text[i]);
			}
		}
		return UnicodeSupport.sb.ToString();
	}

	// Token: 0x04000AAE RID: 2734
	private static StringBuilder sb = new StringBuilder();
}
