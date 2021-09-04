using System;
using UnityEngine;

// Token: 0x0200023B RID: 571
[RequireComponent(typeof(TextRenderer))]
public class PlatformTextTranslation : MonoBehaviour, ITranslatedText
{
	// Token: 0x06000D79 RID: 3449 RVA: 0x00051E40 File Offset: 0x00050040
	public void ResetText()
	{
		TextRenderer component = base.GetComponent<TextRenderer>();
		component.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.DefaultTargetText, Array.Empty<object>());
		component.RefreshMesh();
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00051E68 File Offset: 0x00050068
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
		this.ResetText();
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x00051E80 File Offset: 0x00050080
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

	// Token: 0x04000F24 RID: 3876
	public StringNames DefaultTargetText;

	// Token: 0x04000F25 RID: 3877
	public StringNames SwitchTargetText;
}
