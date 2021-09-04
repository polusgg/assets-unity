using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
[RequireComponent(typeof(TextRenderer))]
public class TextTranslator : MonoBehaviour, ITranslatedText
{
	// Token: 0x06000D7D RID: 3453 RVA: 0x00051EC4 File Offset: 0x000500C4
	public void ResetText()
	{
		if (this.ResetOnlyWhenNoDefault && (this.defaultStr != null || this.defaultStr != ""))
		{
			return;
		}
		TextRenderer component = base.GetComponent<TextRenderer>();
		string text = DestroyableSingleton<TranslationController>.Instance.GetStringWithDefault(this.TargetText, this.defaultStr, Array.Empty<object>());
		if (this.ToUpper)
		{
			text = text.ToUpperInvariant();
		}
		component.Text = text;
		component.RefreshMesh();
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00051F31 File Offset: 0x00050131
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
		this.ResetText();
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x00051F4C File Offset: 0x0005014C
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

	// Token: 0x04001176 RID: 4470
	public StringNames TargetText;

	// Token: 0x04001177 RID: 4471
	public string defaultStr;

	// Token: 0x04001178 RID: 4472
	public bool ToUpper;

	// Token: 0x04001179 RID: 4473
	public bool ResetOnlyWhenNoDefault;
}
