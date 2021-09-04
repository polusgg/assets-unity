using System;
using TMPro;
using UnityEngine;

// Token: 0x0200023E RID: 574
[RequireComponent(typeof(TextMeshPro))]
public class TextTranslatorTMP : MonoBehaviour, ITranslatedText
{
	// Token: 0x06000D81 RID: 3457 RVA: 0x00051F90 File Offset: 0x00050190
	public void ResetText()
	{
		if (this.ResetOnlyWhenNoDefault && (this.defaultStr != null || this.defaultStr != ""))
		{
			return;
		}
		TextMeshPro component = base.GetComponent<TextMeshPro>();
		string text = DestroyableSingleton<TranslationController>.Instance.GetStringWithDefault(this.TargetText, this.defaultStr, Array.Empty<object>());
		if (this.ToUpper)
		{
			text = text.ToUpperInvariant();
		}
		component.text = text;
		component.ForceMeshUpdate(false, false);
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00051FFF File Offset: 0x000501FF
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
		this.ResetText();
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x00052018 File Offset: 0x00050218
	public void OnDestroy()
	{
		if (DestroyableSingleton<TranslationController>.InstanceExists)
		{
			try
			{
				DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Remove(this);
			}
			catch
			{
			}
		}
	}

	// Token: 0x0400117A RID: 4474
	public StringNames TargetText;

	// Token: 0x0400117B RID: 4475
	public string defaultStr;

	// Token: 0x0400117C RID: 4476
	public bool ToUpper;

	// Token: 0x0400117D RID: 4477
	public bool ResetOnlyWhenNoDefault;
}
