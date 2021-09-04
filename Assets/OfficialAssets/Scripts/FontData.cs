using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000069 RID: 105
[Serializable]
public class FontData
{
	// Token: 0x060002BA RID: 698 RVA: 0x00011788 File Offset: 0x0000F988
	public float GetKerning(int last, int cur)
	{
		Dictionary<int, float> dictionary;
		float result;
		if (this.kernings.TryGetValue(last, out dictionary) && dictionary.TryGetValue(cur, out result))
		{
			return result;
		}
		return 0f;
	}

	// Token: 0x04000329 RID: 809
	public Vector2 TextureSize = new Vector2(256f, 256f);

	// Token: 0x0400032A RID: 810
	public List<Vector4> bounds = new List<Vector4>();

	// Token: 0x0400032B RID: 811
	public List<Vector3> offsets = new List<Vector3>();

	// Token: 0x0400032C RID: 812
	public List<Vector4> Channels = new List<Vector4>();

	// Token: 0x0400032D RID: 813
	public Dictionary<int, int> charMap;

	// Token: 0x0400032E RID: 814
	public float LineHeight;

	// Token: 0x0400032F RID: 815
	public Dictionary<int, Dictionary<int, float>> kernings;
}
