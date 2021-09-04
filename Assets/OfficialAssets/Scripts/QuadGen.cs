using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class QuadGen : MonoBehaviour
{
	// Token: 0x06000A17 RID: 2583 RVA: 0x000416A8 File Offset: 0x0003F8A8
	[ContextMenu("Generate")]
	public void Generate()
	{
		Mesh mesh = base.GetComponent<MeshFilter>().mesh = new Mesh();
		int num = this.WidthSubdivisions + 2;
		int num2 = this.HeightSubdivisions + 2;
		Vector3[] array = new Vector3[num * num2];
		Vector2[] array2 = new Vector2[num * num2];
		Vector3 vector = default(Vector3);
		Vector2 vector2 = default(Vector2);
		for (int i = 0; i < num2; i++)
		{
			vector.y = this.Height * (0.5f - (float)i / ((float)this.HeightSubdivisions + 1f));
			for (int j = 0; j < num; j++)
			{
				vector.x = this.Width * (0.5f - (float)j / ((float)this.WidthSubdivisions + 1f));
				int num3 = j + i * num;
				array[num3] = vector;
				vector2.y = Mathf.Lerp(-1f, 1f, Mathf.PerlinNoise((float)j * this.PerlinFreqX, (float)i));
				array2[num3] = vector2;
			}
		}
		List<int> list = new List<int>();
		for (int k = 0; k < array.Length; k++)
		{
			if ((k + 1) % num != 0)
			{
				if (k + num >= array.Length)
				{
					break;
				}
				list.Add(k);
				list.Add(k + num);
				list.Add(k + num + 1);
				list.Add(k);
				list.Add(k + num + 1);
				list.Add(k + 1);
			}
		}
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.SetIndices(list.ToArray(), 0, 0);
	}

	// Token: 0x04000B80 RID: 2944
	public float Width = 1f;

	// Token: 0x04000B81 RID: 2945
	public float Height = 1f;

	// Token: 0x04000B82 RID: 2946
	public int WidthSubdivisions;

	// Token: 0x04000B83 RID: 2947
	public int HeightSubdivisions;

	// Token: 0x04000B84 RID: 2948
	public float PerlinFreqX = 10f;
}
