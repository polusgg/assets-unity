using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000068 RID: 104
public class FontCache : MonoBehaviour
{
	// Token: 0x060002B5 RID: 693 RVA: 0x000115B6 File Offset: 0x0000F7B6
	public void OnEnable()
	{
		if (!FontCache.Instance)
		{
			FontCache.Instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		if (FontCache.Instance != null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x000115F0 File Offset: 0x0000F7F0
	public void SetFont(TextRenderer self, string name)
	{
		if (self.FontData.name == name)
		{
			return;
		}
		for (int i = 0; i < this.DefaultFonts.Count; i++)
		{
			if (this.DefaultFonts[i].name == name)
			{
				MeshRenderer component = self.GetComponent<MeshRenderer>();
				Material material = component.material;
				self.FontData = this.DefaultFonts[i];
				component.sharedMaterial = this.DefaultFontMaterials[i];
				component.material.SetColor("_OutlineColor", material.GetColor("_OutlineColor"));
				component.material.SetInt("_Mask", material.GetInt("_Mask"));
				return;
			}
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x000116B0 File Offset: 0x0000F8B0
	public FontData LoadFont(TextAsset dataSrc)
	{
		if (this.cache == null)
		{
			this.cache = new Dictionary<string, FontData>();
		}
		FontData fontData;
		if (this.cache.TryGetValue(dataSrc.name, out fontData))
		{
			return fontData;
		}
		int num = this.extraData.FindIndex((FontExtensionData ed) => ed.FontName.Equals(dataSrc.name, StringComparison.OrdinalIgnoreCase));
		FontExtensionData eData = null;
		if (num >= 0)
		{
			eData = this.extraData[num];
		}
		fontData = FontCache.LoadFontUncached(dataSrc, eData);
		this.cache[dataSrc.name] = fontData;
		return fontData;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00011749 File Offset: 0x0000F949
	public static FontData LoadFontUncached(TextAsset dataSrc, FontExtensionData eData = null)
	{
		return FontLoader.FromBinary(dataSrc, eData);
	}

	// Token: 0x04000324 RID: 804
	public static FontCache Instance;

	// Token: 0x04000325 RID: 805
	private Dictionary<string, FontData> cache = new Dictionary<string, FontData>();

	// Token: 0x04000326 RID: 806
	public List<FontExtensionData> extraData = new List<FontExtensionData>();

	// Token: 0x04000327 RID: 807
	public List<TextAsset> DefaultFonts = new List<TextAsset>();

	// Token: 0x04000328 RID: 808
	public List<Material> DefaultFontMaterials = new List<Material>();
}
