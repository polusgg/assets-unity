using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006C RID: 108
[CreateAssetMenu]
public class FontExtensionData : ScriptableObject
{
	// Token: 0x060002BE RID: 702 RVA: 0x0001180C File Offset: 0x0000FA0C
	public void AdjustKernings(FontData target)
	{
		for (int i = 0; i < this.kernings.Count; i++)
		{
			KerningPair kerningPair = this.kernings[i];
			Dictionary<int, float> dictionary;
			if (target.kernings.TryGetValue((int)kerningPair.First, out dictionary))
			{
				float num;
				if (dictionary.TryGetValue((int)kerningPair.Second, out num))
				{
					dictionary[(int)kerningPair.Second] = num + (float)kerningPair.Pixels;
				}
				else
				{
					dictionary[(int)kerningPair.Second] = (float)kerningPair.Pixels;
				}
			}
			else
			{
				Dictionary<int, float> dictionary2 = new Dictionary<int, float>();
				dictionary2[(int)kerningPair.Second] = (float)kerningPair.Pixels;
				target.kernings[(int)kerningPair.First] = dictionary2;
			}
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x000118C4 File Offset: 0x0000FAC4
	public void AdjustOffsets(FontData target)
	{
		for (int i = 0; i < this.Offsets.Count; i++)
		{
			OffsetAdjustment offsetAdjustment = this.Offsets[i];
			int index;
			if (target.charMap.TryGetValue((int)offsetAdjustment.Char, out index))
			{
				Vector3 value = target.offsets[index];
				value.x += (float)offsetAdjustment.OffsetX;
				value.y += (float)offsetAdjustment.OffsetY;
				target.offsets[index] = value;
			}
		}
	}

	// Token: 0x04000336 RID: 822
	public string FontName;

	// Token: 0x04000337 RID: 823
	public List<KerningPair> kernings = new List<KerningPair>();

	// Token: 0x04000338 RID: 824
	public List<OffsetAdjustment> Offsets = new List<OffsetAdjustment>();
}
