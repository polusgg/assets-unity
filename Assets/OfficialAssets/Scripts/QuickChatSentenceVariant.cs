using System;
using System.Collections.Generic;

// Token: 0x0200018D RID: 397
public class QuickChatSentenceVariant
{
	// Token: 0x060008FA RID: 2298 RVA: 0x0003A7A0 File Offset: 0x000389A0
	public QuickChatSentenceVariant(StringNames[] Keys, string Value)
	{
		this.requiredKeysInSlots = new StringNames[Keys.Length - 1];
		for (int i = 0; i < this.requiredKeysInSlots.Length; i++)
		{
			this.requiredKeysInSlots[i] = Keys[i + 1];
		}
		this.value = Value;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0003A7EC File Offset: 0x000389EC
	public bool ShouldUse(List<StringNames> currentKeys)
	{
		if (this.requiredKeysInSlots.Length != currentKeys.Count)
		{
			return false;
		}
		for (int i = 0; i < this.requiredKeysInSlots.Length; i++)
		{
			if (this.requiredKeysInSlots[i] != StringNames.ANY && this.requiredKeysInSlots[i] != currentKeys[i] && (this.requiredKeysInSlots[i] != StringNames.QCCrewMe || currentKeys[i] != StringNames.QCCrewI))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000A64 RID: 2660
	private StringNames[] requiredKeysInSlots;

	// Token: 0x04000A65 RID: 2661
	public string value;
}
