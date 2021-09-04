using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017B RID: 379
[CreateAssetMenu(menuName = "PEW/GlyphControllerMapCollection")]
public class GlyphControllerMapCollection : ScriptableObject
{
	// Token: 0x060008AE RID: 2222 RVA: 0x00038B7C File Offset: 0x00036D7C
	public void Initialize()
	{
		this.nameToGlyphCollectionDict = new Dictionary<string, string>();
		foreach (GlyphControllerMapCollection.GlyphControllerMap glyphControllerMap in this.nameToGlyphCollectionList)
		{
			this.nameToGlyphCollectionDict.Add(glyphControllerMap.controllerName, glyphControllerMap.glyphCollectionPath);
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060008AF RID: 2223 RVA: 0x00038BEC File Offset: 0x00036DEC
	public static GlyphControllerMapCollection Instance
	{
		get
		{
			if (!GlyphControllerMapCollection._instance)
			{
				GlyphControllerMapCollection._instance = Resources.Load<GlyphControllerMapCollection>("ControllerGlyphMapAsset");
				GlyphControllerMapCollection._instance.Initialize();
			}
			return GlyphControllerMapCollection._instance;
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00038C18 File Offset: 0x00036E18
	public GlyphCollection TryGetGlyphCollectionForController(string controllerName)
	{
		string text;
		if (this.nameToGlyphCollectionDict.TryGetValue(controllerName, out text))
		{
			GlyphCollection glyphCollection = Resources.Load<GlyphCollection>(text);
			if (glyphCollection)
			{
				glyphCollection.Initialize();
				return glyphCollection;
			}
		}
		return null;
	}

	// Token: 0x04000A07 RID: 2567
	public List<GlyphControllerMapCollection.GlyphControllerMap> nameToGlyphCollectionList = new List<GlyphControllerMapCollection.GlyphControllerMap>();

	// Token: 0x04000A08 RID: 2568
	private Dictionary<string, string> nameToGlyphCollectionDict;

	// Token: 0x04000A09 RID: 2569
	private static GlyphControllerMapCollection _instance;

	// Token: 0x020003E8 RID: 1000
	[Serializable]
	public class GlyphControllerMap
	{
		// Token: 0x04001AE9 RID: 6889
		public string controllerName;

		// Token: 0x04001AEA RID: 6890
		public string glyphCollectionPath;
	}
}
