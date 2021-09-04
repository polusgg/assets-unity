using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x0200005D RID: 93
public class WordGroup : List<string>
{
	// Token: 0x06000275 RID: 629 RVA: 0x0001086C File Offset: 0x0000EA6C
	public static WordGroup Parse(TextReader reader)
	{
		WordGroup wordGroup = new WordGroup();
		string text;
		while (!(text = reader.ReadLine()).IsNullOrWhiteSpace())
		{
			wordGroup.AddRange(text.Trim().Split(new char[]
			{
				','
			}));
		}
		return wordGroup;
	}
}
