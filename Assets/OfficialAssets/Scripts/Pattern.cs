using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x0200005C RID: 92
public class Pattern
{
	// Token: 0x06000272 RID: 626 RVA: 0x00010750 File Offset: 0x0000E950
	public string FollowPattern(Random randy, Dictionary<string, WordGroup> words)
	{
		bool flag = false;
		bool flag2 = true;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (char c in this.pattern)
		{
			if (c == '^')
			{
				flag = true;
			}
			else if (char.IsUpper(c))
			{
				WordGroup wordGroup;
				if (words.TryGetValue(c.ToString(), out wordGroup))
				{
					string text2 = wordGroup[randy.Next(wordGroup.Count)];
					if (flag2)
					{
						flag2 = flag;
						stringBuilder.AppendFormat("{0}{1}", text2.Substring(0, 1).ToUpperInvariant(), text2.Substring(1).ToLowerInvariant());
					}
					else
					{
						stringBuilder.Append(text2);
					}
				}
			}
			else
			{
				if (flag2)
				{
					flag2 = flag;
					stringBuilder.Append(char.ToUpperInvariant(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
				if (char.IsWhiteSpace(c))
				{
					flag2 = true;
				}
			}
		}
		return stringBuilder.Replace(" Of", " of").Replace(" The", " the").ToString();
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00010856 File Offset: 0x0000EA56
	public static Pattern Parse(string line)
	{
		return new Pattern
		{
			pattern = line
		};
	}

	// Token: 0x04000306 RID: 774
	private string pattern;
}
