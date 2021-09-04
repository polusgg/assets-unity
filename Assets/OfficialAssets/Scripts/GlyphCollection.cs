using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;

// Token: 0x0200017A RID: 378
[CreateAssetMenu(menuName = "PEW/GlyphCollection")]
public class GlyphCollection : ScriptableObject
{
	// Token: 0x060008A9 RID: 2217 RVA: 0x0003886C File Offset: 0x00036A6C
	public void Initialize()
	{
		this.glyphDict = new Dictionary<string, GlyphCollection.GlyphMap>();
		foreach (GlyphCollection.GlyphMap glyphMap in this.glyphMaps)
		{
			this.glyphDict[glyphMap.elementIdentifier.ToLower()] = glyphMap;
			if (!string.IsNullOrEmpty(glyphMap.alternateElementIdentifier))
			{
				this.glyphDict[glyphMap.alternateElementIdentifier.ToLower()] = glyphMap;
			}
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060008AA RID: 2218 RVA: 0x00038900 File Offset: 0x00036B00
	private static string GlyphPath
	{
		get
		{
			return "Glyphs/XboxGlyphs";
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00038908 File Offset: 0x00036B08
	public static Sprite FindGlyph(int actionName, out GlyphCollection.ErrorCode error)
	{
		//if (!GlyphCollection.defaultGlyphCollection)
		//{
		//	GlyphCollection.defaultGlyphCollection = Resources.Load<GlyphCollection>(GlyphCollection.GlyphPath);
		//	if (!GlyphCollection.defaultGlyphCollection)
		//	{
		//		error = GlyphCollection.ErrorCode.NoGlyphFound;
		//		return null;
		//	}
		//	GlyphCollection.defaultGlyphCollection.Initialize();
		//}
		//Player player = ReInput.players.GetPlayer(0);
		//Controller controller = player.controllers.GetLastActiveController();
		//if (controller == null)
		//{
		//	foreach (Controller controller2 in player.controllers.Controllers)
		//	{
		//		Debug.LogError(controller2.name);
		//		if (controller2 is Joystick)
		//		{
		//			controller = controller2;
		//			break;
		//		}
		//	}
		//}
		//if (controller == null)
		//{
		//	Debug.LogError("GlyphCollection.FindGlyph: ...No controller, I guess?");
		//	error = GlyphCollection.ErrorCode.NoController;
		//	return null;
		//}
		//GlyphCollection glyphCollection = GlyphCollection.defaultGlyphCollection;
		//GlyphCollection glyphCollection2;
		//if (GlyphCollection.otherGlyphCollections.TryGetValue(controller.name, out glyphCollection2))
		//{
		//	glyphCollection = glyphCollection2;
		//}
		//else if (!GlyphCollection.controllersWithNoValidGlyphCollection.Contains(controller.name))
		//{
		//	glyphCollection2 = GlyphControllerMapCollection.Instance.TryGetGlyphCollectionForController(controller.name);
		//	if (glyphCollection2)
		//	{
		//		glyphCollection = glyphCollection2;
		//		GlyphCollection.otherGlyphCollections.Add(controller.name, glyphCollection2);
		//		Debug.Log("Found valid glyph collection for " + controller.name);
		//	}
		//	else
		//	{
		//		GlyphCollection.controllersWithNoValidGlyphCollection.Add(controller.name);
		//		Debug.Log("No valid glyph collection for " + controller.name + ", using default");
		//	}
		//}
		//int elementMapsWithAction = player.controllers.maps.GetElementMapsWithAction(actionName, false, GlyphCollection.mapResults);
		//if (elementMapsWithAction <= 0)
		//{
		//	string str = "GlyphCollection.FindGlyph: No elements bound to action ";
		//	RewiredConstsEnum.Action action = (RewiredConstsEnum.Action)actionName;
		//	Debug.LogError(str + action.ToString());
		//	error = GlyphCollection.ErrorCode.NoElementsBoundToAction;
		//	return null;
		//}
		//ActionElementMap actionElementMap = GlyphCollection.mapResults[0];
		//if (elementMapsWithAction > 1)
		//{
		//	for (int i = 1; i < elementMapsWithAction; i++)
		//	{
		//		if (GlyphCollection.mapResults[i].elementType == null)
		//		{
		//			actionElementMap = GlyphCollection.mapResults[i];
		//		}
		//	}
		//}
		//GlyphCollection.GlyphMap glyphMap = null;
		//if (glyphCollection.glyphDict.TryGetValue(actionElementMap.elementIdentifierName.ToLower(), out glyphMap))
		//{
		//	error = GlyphCollection.ErrorCode.None;
		//	return glyphMap.glyph;
		//}
		//Debug.LogError("GlyphCollection.FindGlyph: GlyphCollection didn't have a glyph for element " + actionElementMap.elementIdentifierName);
		error = GlyphCollection.ErrorCode.NoGlyphFound;
		return null;
	}

	// Token: 0x04000A00 RID: 2560
	public string controllerType;

	// Token: 0x04000A01 RID: 2561
	public List<GlyphCollection.GlyphMap> glyphMaps = new List<GlyphCollection.GlyphMap>();

	// Token: 0x04000A02 RID: 2562
	public Dictionary<string, GlyphCollection.GlyphMap> glyphDict;

	// Token: 0x04000A03 RID: 2563
	//private static List<ActionElementMap> mapResults = new List<ActionElementMap>();

	// Token: 0x04000A04 RID: 2564
	private static GlyphCollection defaultGlyphCollection;

	// Token: 0x04000A05 RID: 2565
	private static Dictionary<string, GlyphCollection> otherGlyphCollections = new Dictionary<string, GlyphCollection>();

	// Token: 0x04000A06 RID: 2566
	private static HashSet<string> controllersWithNoValidGlyphCollection = new HashSet<string>();

	// Token: 0x020003E6 RID: 998
	[Serializable]
	public class GlyphMap
	{
		// Token: 0x04001AE1 RID: 6881
		public string elementIdentifier;

		// Token: 0x04001AE2 RID: 6882
		public string alternateElementIdentifier;

		// Token: 0x04001AE3 RID: 6883
		public Sprite glyph;
	}

	// Token: 0x020003E7 RID: 999
	public enum ErrorCode
	{
		// Token: 0x04001AE5 RID: 6885
		None,
		// Token: 0x04001AE6 RID: 6886
		NoController,
		// Token: 0x04001AE7 RID: 6887
		NoGlyphFound,
		// Token: 0x04001AE8 RID: 6888
		NoElementsBoundToAction
	}
}
