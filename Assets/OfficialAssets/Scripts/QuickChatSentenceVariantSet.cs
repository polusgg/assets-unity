using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class QuickChatSentenceVariantSet
{
	// Token: 0x060008F7 RID: 2295 RVA: 0x0003A6E0 File Offset: 0x000388E0
	public QuickChatSentenceVariant GetMatchingVariant(List<StringNames> currentKeys)
	{
		foreach (QuickChatSentenceVariant quickChatSentenceVariant in this.variants)
		{
			if (quickChatSentenceVariant.ShouldUse(currentKeys))
			{
				return quickChatSentenceVariant;
			}
		}
		return null;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x0003A73C File Offset: 0x0003893C
	public void AddVariant(StringNames[] keys, string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			Debug.LogWarning("Attempting to add variant for " + this.baseToken.ToString() + " with an empty string");
			return;
		}
		QuickChatSentenceVariant item = new QuickChatSentenceVariant(keys, value);
		this.variants.Add(item);
	}

	// Token: 0x04000A62 RID: 2658
	public StringNames baseToken;

	// Token: 0x04000A63 RID: 2659
	private List<QuickChatSentenceVariant> variants = new List<QuickChatSentenceVariant>();
}
