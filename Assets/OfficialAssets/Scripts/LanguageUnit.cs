using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class LanguageUnit
{
	// Token: 0x06000D73 RID: 3443 RVA: 0x0005196C File Offset: 0x0004FB6C
	public LanguageUnit(TranslatedImageSet locSet)
	{
		foreach (ImageData imageData in locSet.Images)
		{
			this.AllImages.Add(imageData.Name, imageData.Sprite);
		}
		using (StringReader stringReader = new StringReader(locSet.Data.text))
		{
			for (string text = stringReader.ReadLine(); text != null; text = stringReader.ReadLine())
			{
				if (text.Length != 0)
				{
					int num = text.IndexOf('\t');
					if (num < 0)
					{
						Debug.LogWarning("Couldn't parse: " + text);
					}
					else
					{
						string text2 = text.Substring(0, num);
						string text3 = this.UnescapeCodes(text, num + 1);
						if (text2.Length != 0 || text3.Length != 0)
						{
							StringNames stringNames;
							if (!Enum.TryParse<StringNames>(text2, out stringNames))
							{
								if (text2[0] == 'Q' && text2[1] == 'C')
								{
									this.HandleQCSentenceVariant(text2, text3);
								}
							}
							else if (this.AllStrings.ContainsKey(stringNames))
							{
								Debug.LogWarning(string.Format("Duplicate translation for {0}: '{1}' and '{2}'", stringNames, text3, this.AllStrings[stringNames]));
							}
							else
							{
								this.AllStrings.Add(stringNames, text3);
							}
						}
					}
				}
			}
		}
		foreach (object obj in Enum.GetValues(typeof(StringNames)))
		{
			StringNames stringNames2 = (StringNames)obj;
			if (!this.AllStrings.ContainsKey(stringNames2))
			{
				if (locSet.name == "English")
				{
					string[] array = new string[6];
					array[0] = "No Translation for ";
					array[1] = stringNames2.ToString();
					array[2] = " (";
					int num2 = 3;
					int i = (int)stringNames2;
					array[num2] = i.ToString();
					array[4] = ") in language: ";
					array[5] = locSet.name;
					Debug.LogWarning(string.Concat(array));
				}
				else
				{
					Debug.LogWarning("No Translation for " + stringNames2.ToString() + " in language: " + locSet.name);
				}
			}
		}
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00051BF4 File Offset: 0x0004FDF4
	public string UnescapeCodes(string src, int startAt)
	{
		this.builder.Clear();
		for (int i = startAt; i < src.Length; i++)
		{
			char c = src[i];
			if (c == '\\')
			{
				char c2 = src[++i];
				if (c2 != 'n')
				{
					if (c2 == 't')
					{
						this.builder.Append('\t');
					}
				}
				else
				{
					this.builder.Append('\n');
				}
			}
			else
			{
				this.builder.Append(c);
			}
		}
		return this.builder.ToString();
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00051C7C File Offset: 0x0004FE7C
	public string GetString(StringNames stringId, string defaultStr, params object[] parts)
	{
		if (defaultStr == null)
		{
			defaultStr = "";
		}
		string text;
		if (this.AllStrings.TryGetValue(stringId, out text))
		{
			if (text.IsNullOrWhiteSpace() && DestroyableSingleton<TranslationController>.Instance.FallbackLanguage != null && DestroyableSingleton<TranslationController>.Instance.FallbackLanguage.AllStrings.TryGetValue(stringId, out text))
			{
				if (parts.Length != 0)
				{
					return string.Format(text, parts);
				}
				return text;
			}
			else
			{
				if (stringId == StringNames.NoTranslation && defaultStr != "")
				{
					text = defaultStr;
				}
				if (parts.Length != 0)
				{
					return string.Format(text, parts);
				}
				return text;
			}
		}
		else if (DestroyableSingleton<TranslationController>.Instance.FallbackLanguage != null && DestroyableSingleton<TranslationController>.Instance.FallbackLanguage.AllStrings.TryGetValue(stringId, out text))
		{
			if (stringId == StringNames.NoTranslation && defaultStr != "")
			{
				text = defaultStr;
			}
			if (parts.Length != 0)
			{
				return string.Format(text, parts);
			}
			return text;
		}
		else
		{
			if (stringId == StringNames.NoTranslation && defaultStr != "")
			{
				return defaultStr;
			}
			return "STRMISS";
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00051D6C File Offset: 0x0004FF6C
	public Sprite GetImage(ImageNames id)
	{
		Sprite result;
		this.AllImages.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00051D8C File Offset: 0x0004FF8C
	public void HandleQCSentenceVariant(string id, string value)
	{
		string[] array = id.Split(new char[]
		{
			'_'
		});
		StringNames[] array2 = new StringNames[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			StringNames stringNames;
			if (!Enum.TryParse<StringNames>(array[i], out stringNames))
			{
				return;
			}
			array2[i] = stringNames;
		}
		QuickChatSentenceVariantSet quickChatSentenceVariantSet;
		if (!this.AllQuickChatVariantSets.TryGetValue(array2[0], out quickChatSentenceVariantSet))
		{
			quickChatSentenceVariantSet = new QuickChatSentenceVariantSet();
			quickChatSentenceVariantSet.baseToken = array2[0];
			this.AllQuickChatVariantSets.Add(quickChatSentenceVariantSet.baseToken, quickChatSentenceVariantSet);
		}
		quickChatSentenceVariantSet.AddVariant(array2, value);
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00051E10 File Offset: 0x00050010
	public string GetFITBVariant(StringNames id, List<StringNames> entryIDs)
	{
		QuickChatSentenceVariantSet quickChatSentenceVariantSet;
		if (this.AllQuickChatVariantSets.TryGetValue(id, out quickChatSentenceVariantSet))
		{
			QuickChatSentenceVariant matchingVariant = quickChatSentenceVariantSet.GetMatchingVariant(entryIDs);
			if (matchingVariant != null)
			{
				return matchingVariant.value;
			}
		}
		return null;
	}

	// Token: 0x04000F1F RID: 3871
	public bool IsEnglish;

	// Token: 0x04000F20 RID: 3872
	private Dictionary<StringNames, string> AllStrings = new Dictionary<StringNames, string>();

	// Token: 0x04000F21 RID: 3873
	private Dictionary<ImageNames, Sprite> AllImages = new Dictionary<ImageNames, Sprite>();

	// Token: 0x04000F22 RID: 3874
	private Dictionary<StringNames, QuickChatSentenceVariantSet> AllQuickChatVariantSets = new Dictionary<StringNames, QuickChatSentenceVariantSet>();

	// Token: 0x04000F23 RID: 3875
	private StringBuilder builder = new StringBuilder(512);
}
