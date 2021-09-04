using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class TranslationController : DestroyableSingleton<TranslationController>
{
	// Token: 0x06000D88 RID: 3464 RVA: 0x000520B9 File Offset: 0x000502B9
	public override void Awake()
	{
		base.Awake();
		if (DestroyableSingleton<TranslationController>.Instance == this)
		{
			this.CurrentLanguage = new LanguageUnit(this.Languages[(int)SaveManager.LastLanguage]);
		}
		this.FallbackLanguage = new LanguageUnit(this.FallbackLanguageImageSet);
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x000520F8 File Offset: 0x000502F8
	public void SetLanguage(TranslatedImageSet lang)
	{
		int num = this.Languages.IndexOf(lang);
		Debug.Log("Set language to " + num.ToString());
		SaveManager.LastLanguage = (uint)num;
		this.CurrentLanguage = new LanguageUnit(this.Languages[num]);
		for (int i = 0; i < this.ActiveTexts.Count; i++)
		{
			this.ActiveTexts[i].ResetText();
		}
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00052168 File Offset: 0x00050368
	public Sprite GetImage(ImageNames id)
	{
		return this.CurrentLanguage.GetImage(id);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00052176 File Offset: 0x00050376
	public string GetString(StringNames id, params object[] parts)
	{
		return this.CurrentLanguage.GetString(id, "", parts);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0005218A File Offset: 0x0005038A
	public string GetStringWithDefault(StringNames id, string defaultStr, params object[] parts)
	{
		return this.CurrentLanguage.GetString(id, defaultStr, parts);
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0005219C File Offset: 0x0005039C
	public string GetString(SystemTypes room)
	{
		StringNames stringNames = TranslationController.SystemTypesToStringNames[(int)room];
		if (stringNames == StringNames.ExitButton)
		{
			return "STRMISS";
		}
		return this.GetString(stringNames, Array.Empty<object>());
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000521C6 File Offset: 0x000503C6
	public string GetString(TaskTypes task)
	{
		return this.GetString(TranslationController.TaskTypesToStringNames[(int)((byte)task)], Array.Empty<object>());
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x000521DC File Offset: 0x000503DC
	public string GetMonthStringViaNumber(int monthNum)
	{
		StringNames id = StringNames.January + monthNum - 1;
		return DestroyableSingleton<TranslationController>.Instance.GetString(id, Array.Empty<object>());
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x00052203 File Offset: 0x00050403
	public string GetFITBVariant(StringNames id, List<StringNames> entryIDs)
	{
		return this.CurrentLanguage.GetFITBVariant(id, entryIDs);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x00052214 File Offset: 0x00050414
	internal static uint SelectDefaultLanguage()
	{
		//try
		//{
		//	SystemLanguage systemLanguage = Application.systemLanguage;
		//	if (systemLanguage <= 23)
		//	{
		//		switch (systemLanguage)
		//		{
		//		case 9:
		//			return 6U;
		//		case 10:
		//			return 0U;
		//		case 11:
		//		case 12:
		//		case 13:
		//			break;
		//		case 14:
		//			return 8U;
		//		case 15:
		//			return 9U;
		//		default:
		//			switch (systemLanguage)
		//			{
		//			case 21:
		//				return 10U;
		//			case 22:
		//				return 11U;
		//			case 23:
		//				return 4U;
		//			}
		//			break;
		//		}
		//	}
		//	else
		//	{
		//		if (systemLanguage == 28)
		//		{
		//			return 3U;
		//		}
		//		if (systemLanguage == 30)
		//		{
		//			return 5U;
		//		}
		//		if (systemLanguage == 34)
		//		{
		//			return 1U;
		//		}
		//	}
		//	return 0U;
		//}
		//catch
		//{
		//}
		return 0U;
	}

	// Token: 0x04001191 RID: 4497
	private static readonly StringNames[] SystemTypesToStringNames = SystemTypeHelpers.AllTypes.Select(delegate(SystemTypes t)
	{
		StringNames result;
		Enum.TryParse<StringNames>(t.ToString(), out result);
		return result;
	}).ToArray<StringNames>();

	// Token: 0x04001192 RID: 4498
	private static readonly StringNames[] TaskTypesToStringNames = TaskTypesHelpers.AllTypes.Select(delegate(TaskTypes t)
	{
		StringNames result;
		Enum.TryParse<StringNames>(t.ToString(), out result);
		return result;
	}).ToArray<StringNames>();

	// Token: 0x04001193 RID: 4499
	public TranslatedImageSet[] Languages;

	// Token: 0x04001194 RID: 4500
	public LanguageUnit CurrentLanguage;

	// Token: 0x04001195 RID: 4501
	public LanguageUnit FallbackLanguage;

	// Token: 0x04001196 RID: 4502
	public TranslatedImageSet FallbackLanguageImageSet;

	// Token: 0x04001197 RID: 4503
	public List<ITranslatedText> ActiveTexts = new List<ITranslatedText>();
}
