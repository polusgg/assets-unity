using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class VertLineBehaviour : MonoBehaviour
{
	// Token: 0x17000083 RID: 131
	// (set) Token: 0x06000801 RID: 2049 RVA: 0x00033D42 File Offset: 0x00031F42
	public Color color
	{
		set
		{
			this.rend.material.SetColor("_Color", value);
		}
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00033D5C File Offset: 0x00031F5C
	public void Awake()
	{
		this.rend = base.GetComponent<MeshRenderer>();
		this.mesh = new Mesh();
		base.GetComponent<MeshFilter>().mesh = this.mesh;
		this.mesh.MarkDynamic();
		this.vecs = new Vector3[this.NumPoints];
		Vector2[] array = new Vector2[this.NumPoints];
		int[] array2 = new int[this.NumPoints];
		float num = (float)(this.vecs.Length - 1);
		for (int i = 0; i < this.vecs.Length; i++)
		{
			this.vecs[i].y = this.Height.Lerp((float)i / num);
			array2[i] = i;
			array[i] = new Vector2(0.5f, (float)i / num);
		}
		this.mesh.vertices = this.vecs;
		this.mesh.uv = array;
		this.mesh.SetIndices(array2, MeshTopology.LineStrip, 0);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00033E48 File Offset: 0x00032048
	public void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.Duration)
		{
			this.timer = 0f;
		}
		float num = this.timer / this.Duration;
		this.mesh.vertices = this.vecs;
		this.rend.material.SetFloat("_Fade", num);
	}

	[ContextMenu("SetAlive")]
	public void SetAlive()
	{
		this.color = Color.white;
		int num = this.vecs.Length;
		int num2 = this.vecs.Length / (this.beats + this.beatPadding);
		for (int i = 0; i < this.beats; i++)
		{
			int num3 = (int)((float)i * ((float)this.vecs.Length / (float)this.beats)) + this.Offset;
			for (int j = 0; j < num2; j++)
			{
				int num4 = (j + num3) % this.vecs.Length;
				Vector3 vector = this.vecs[num4];
				float num5 = (float)j / (float)num2;
				if ((double)num5 < 0.3)
				{
					float num6 = num5 / 0.3f;
					vector.x = this.Width.Lerp(Mathf.Lerp(0.5f, 0f, num6));
				}
				else if (num5 < 0.6f)
				{
					float num7 = (num5 - 0.3f) / 0.3f;
					vector.x = this.Width.Lerp(Mathf.Lerp(0f, 1f, num7));
				}
				else
				{
					float num8 = (num5 - 0.6f) / 0.3f;
					vector.x = this.Width.Lerp(Mathf.Lerp(1f, 0.5f, num8));
				}
				this.vecs[num4] = vector;
			}
		}
		this.mesh.vertices = this.vecs;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00034028 File Offset: 0x00032228
	public void SetDead()
	{
		this.color = Color.red;
		float num = (float)(this.vecs.Length - 1);
		for (int i = 0; i < this.vecs.Length; i++)
		{
			Vector3 vector = this.vecs[i];
			vector.x = this.Width.Lerp(0.5f);
			vector.y = this.Height.Lerp((float)i / num);
			this.vecs[i] = vector;
		}
		this.mesh.vertices = this.vecs;
	}

	// Token: 0x0400095B RID: 2395
	public int NumPoints = 128;

	// Token: 0x0400095C RID: 2396
	public FloatRange Width;

	// Token: 0x0400095D RID: 2397
	public FloatRange Height;

	// Token: 0x0400095E RID: 2398
	private Mesh mesh;

	// Token: 0x0400095F RID: 2399
	private MeshRenderer rend;

	// Token: 0x04000960 RID: 2400
	private Vector3[] vecs;

	// Token: 0x04000961 RID: 2401
	public float Duration = 4f;

	// Token: 0x04000962 RID: 2402
	private float timer;

	// Token: 0x04000963 RID: 2403
	public int Offset = 25;

	// Token: 0x04000964 RID: 2404
	public int beats = 4;

	// Token: 0x04000965 RID: 2405
	public int beatPadding = 5;
}
