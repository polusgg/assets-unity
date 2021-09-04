using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = System.Random;

// Token: 0x0200005B RID: 91
public class RandomNameGenerator : MonoBehaviour
{
	// Token: 0x0600026C RID: 620 RVA: 0x000105BA File Offset: 0x0000E7BA
	private void Awake()
	{
		this.Parse(new StringReader(this.PatternConfig.text));
		this.UsablePatterns = this.Patterns;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x000105E0 File Offset: 0x0000E7E0
	public string GetName()
	{
		if (this.UsablePatterns.Count<Pattern>() == 0)
		{
			this.Reset();
		}
		Pattern pattern = this.UsablePatterns.Random<Pattern>();
		this.UsablePatterns = from p in this.UsablePatterns
		where p != pattern
		select p;
		return pattern.FollowPattern(RandomNameGenerator.randy, this.WordGroups);
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0001064A File Offset: 0x0000E84A
	public void Reset()
	{
		this.UsablePatterns = this.Patterns;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00010658 File Offset: 0x0000E858
	public void Parse(TextReader reader)
	{
		string text;
		while ((text = reader.ReadLine()) != null)
		{
			if (text.StartsWith("["))
			{
				string[] array = text.Split(new char[]
				{
					' ',
					'[',
					']'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					if (array.Any((string w) => w.Equals("words", StringComparison.OrdinalIgnoreCase)))
					{
						string key = array[0];
						WordGroup value = WordGroup.Parse(reader);
						this.WordGroups.Add(key, value);
						continue;
					}
				}
				if (array.Length == 1 && array[0].Equals("patterns", StringComparison.OrdinalIgnoreCase))
				{
					while (!(text = reader.ReadLine()).IsNullOrWhiteSpace())
					{
						this.Patterns.Add(Pattern.Parse(text));
					}
				}
			}
		}
	}

	// Token: 0x04000301 RID: 769
	public TextAsset PatternConfig;

	// Token: 0x04000302 RID: 770
	private List<Pattern> Patterns = new List<Pattern>();

	// Token: 0x04000303 RID: 771
	private Dictionary<string, WordGroup> WordGroups = new Dictionary<string, WordGroup>(StringComparer.OrdinalIgnoreCase);

	// Token: 0x04000304 RID: 772
	private static Random randy = new Random();

	// Token: 0x04000305 RID: 773
	private IEnumerable<Pattern> UsablePatterns;
}
