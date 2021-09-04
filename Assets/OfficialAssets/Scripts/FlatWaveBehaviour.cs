using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FlatWaveBehaviour : MonoBehaviour
{
	// Token: 0x06000356 RID: 854 RVA: 0x00015E10 File Offset: 0x00014010
	public void Start()
	{
		//this.mesh = new Mesh();
		//base.GetComponent<MeshFilter>().mesh = this.mesh;
		//this.mesh.MarkDynamic();
		//this.vecs = new Vector3[this.NumPoints];
		//int[] array = new int[this.NumPoints];
		//for (int i = 0; i < this.vecs.Length; i++)
		//{
		//	Vector3 vector = this.vecs[i];
		//	vector.x = this.Width.Lerp((float)i / (float)this.vecs.Length);
		//	vector.y = this.Center;
		//	if (BoolRange.Next(this.NoiseP))
		//	{
		//		vector.y += this.Delta.Next();
		//	}
		//	this.vecs[i] = vector;
		//	array[i] = i;
		//}
		//this.mesh.vertices = this.vecs;
		//this.mesh.SetIndices(array, 4, 0);
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00015F00 File Offset: 0x00014100
	public void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > this.TickRate)
		{
			this.timer = 0f;
			for (int i = 0; i < this.vecs.Length - this.Skip; i++)
			{
				this.vecs[i].y = this.vecs[i + this.Skip].y;
			}
			for (int j = 1; j <= this.Skip; j++)
			{
				this.vecs[this.vecs.Length - j].y = this.Center;
				if (BoolRange.Next(this.NoiseP))
				{
					Vector3[] array = this.vecs;
					int num = this.vecs.Length - j;
					array[num].y = array[num].y + this.Delta.Next();
				}
			}
			this.mesh.vertices = this.vecs;
		}
	}

	// Token: 0x040003D9 RID: 985
	public int NumPoints = 128;

	// Token: 0x040003DA RID: 986
	public FloatRange Width;

	// Token: 0x040003DB RID: 987
	public FloatRange Delta;

	// Token: 0x040003DC RID: 988
	public float Center;

	// Token: 0x040003DD RID: 989
	private Mesh mesh;

	// Token: 0x040003DE RID: 990
	private Vector3[] vecs;

	// Token: 0x040003DF RID: 991
	public float TickRate = 0.1f;

	// Token: 0x040003E0 RID: 992
	private float timer;

	// Token: 0x040003E1 RID: 993
	public int Skip = 3;

	// Token: 0x040003E2 RID: 994
	[Range(0f, 1f)]
	public float NoiseP = 0.5f;
}
