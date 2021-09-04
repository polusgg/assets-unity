using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
[RequireComponent(typeof(SpriteRenderer))]
public class ImageTranslator : MonoBehaviour, ITranslatedText
{
	// Token: 0x06000D6F RID: 3439 RVA: 0x00051900 File Offset: 0x0004FB00
	public void ResetText()
	{
		Sprite image = DestroyableSingleton<TranslationController>.Instance.GetImage(this.TargetImage);
		if (image)
		{
			base.GetComponent<SpriteRenderer>().sprite = image;
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00051932 File Offset: 0x0004FB32
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
		this.ResetText();
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0005194A File Offset: 0x0004FB4A
	public void OnDestroy()
	{
		if (DestroyableSingleton<TranslationController>.InstanceExists)
		{
			DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Remove(this);
		}
	}

	// Token: 0x04000F1E RID: 3870
	public ImageNames TargetImage;
}
