using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RadioWaveBehaviour : MonoBehaviour
{
	// Token: 0x060001AB RID: 427 RVA: 0x0000D4FC File Offset: 0x0000B6FC
	public void Start()
	{
        this.mesh = new Mesh();
        base.GetComponent<MeshFilter>().mesh = this.mesh;
        this.mesh.MarkDynamic();
        this.vecs = new Vector3[this.NumPoints];
        int[] array = new int[this.NumPoints];
        for (int i = 0; i < this.vecs.Length; i++)
        {
            Vector3 vector = this.vecs[i];
            vector.x = this.Width.Lerp((float)i / (float)this.vecs.Length);
            vector.y = this.Height.Next();
            this.vecs[i] = vector;
            array[i] = i;
        }
        this.mesh.vertices = this.vecs;
        this.mesh.SetIndices(array, MeshTopology.LineStrip, 0);
    }

	// Token: 0x060001AC RID: 428 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
	public void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > this.TickRate)
		{
			this.timer = 0f;
			this.Tick++;
			for (int i = 0; i < this.vecs.Length - this.Skip; i++)
			{
				this.vecs[i].y = this.vecs[i + this.Skip].y;
			}
			if (this.Random)
			{
				for (int j = 1; j <= this.Skip; j++)
				{
					this.vecs[this.vecs.Length - j].y = this.Height.Next();
				}
			}
			else
			{
				float num = 1f - this.NoiseLevel;
				for (int k = 0; k < this.Skip; k++)
				{
					this.vecs[this.vecs.Length - 1 - this.Skip + k].y = Mathf.Sin(((float)this.Tick + (float)k / (float)this.Skip) * this.Frequency) * this.Height.max * num + this.Height.Next() * this.NoiseLevel;
				}
			}
			this.mesh.vertices = this.vecs;
		}
	}

	// Token: 0x04000287 RID: 647
	public int NumPoints = 128;

	// Token: 0x04000288 RID: 648
	public FloatRange Width;

	// Token: 0x04000289 RID: 649
	public FloatRange Height;

	// Token: 0x0400028A RID: 650
	private Mesh mesh;

	// Token: 0x0400028B RID: 651
	private Vector3[] vecs;

	// Token: 0x0400028C RID: 652
	public float TickRate = 0.1f;

	// Token: 0x0400028D RID: 653
	private float timer;

	// Token: 0x0400028E RID: 654
	public int Skip = 2;

	// Token: 0x0400028F RID: 655
	public float Frequency = 5f;

	// Token: 0x04000290 RID: 656
	private int Tick;

	// Token: 0x04000291 RID: 657
	public bool Random;

	// Token: 0x04000292 RID: 658
	[Range(0f, 1f)]
	public float NoiseLevel;
}
