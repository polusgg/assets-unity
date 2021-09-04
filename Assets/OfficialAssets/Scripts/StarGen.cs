using System;
using UnityEngine;

// Token: 0x02000216 RID: 534
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class StarGen : MonoBehaviour
{
	// Token: 0x06000C9A RID: 3226 RVA: 0x0004D79C File Offset: 0x0004B99C
	public void Start()
	{
		this.stars = new StarGen.Stars[this.NumStars];
		this.verts = new Vector3[this.NumStars * 4];
		Vector2[] array = new Vector2[this.NumStars * 4];
		int[] array2 = new int[this.NumStars * 6];
		this.SetDirection(this.Direction);
		MeshFilter component = base.GetComponent<MeshFilter>();
		this.mesh = new Mesh();
		this.mesh.MarkDynamic();
		component.mesh = this.mesh;
		Vector3 vector = default(Vector3);
		Vector2 vector2 = default(Vector2);
		for (int i = 0; i < this.stars.Length; i++)
		{
			StarGen.Stars stars = this.stars[i];
			float num = FloatRange.Next(-1f, 1f) * this.Length;
			float num2 = FloatRange.Next(-1f, 1f) * this.Width;
			float num3 = stars.PositionX = num * this.NormDir.x + num2 * this.Tangent.x;
			float num4 = stars.PositionY = num * this.NormDir.y + num2 * this.Tangent.y;
			float num5 = FloatRange.Next(0.01f, 0.05f);
			stars.Size = num5;
			stars.Rate = this.Rates.Next();
			this.stars[i] = stars;
			int num6 = i * 4;
			vector.x = num3 - num5;
			vector.y = num4 + num5;
			this.verts[num6] = vector;
			vector.y = num4 - num5;
			this.verts[num6 + 1] = vector;
			vector.x = num3 + num5;
			this.verts[num6 + 2] = vector;
			vector.y = num4 + num5;
			this.verts[num6 + 3] = vector;
			vector2.x = -1f;
			vector2.y = 1f;
			array[num6] = vector2;
			vector2.y = -1f;
			array[num6 + 1] = vector2;
			vector2.x = 1f;
			array[num6 + 2] = vector2;
			vector2.y = 1f;
			array[num6 + 3] = vector2;
			int num7 = i * 6;
			array2[num7] = num6;
			array2[num7 + 1] = num6 + 1;
			array2[num7 + 2] = num6 + 2;
			array2[num7 + 3] = num6 + 2;
			array2[num7 + 4] = num6;
			array2[num7 + 5] = num6 + 3;
		}
		this.mesh.vertices = this.verts;
		this.mesh.uv = array;
		this.mesh.SetIndices(array2, 0, 0);
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0004DA68 File Offset: 0x0004BC68
	private void Update()
	{
		float num = -0.99f * this.Length;
		Vector2 vector = this.Direction * Time.deltaTime;
		for (int i = 0; i < this.stars.Length; i++)
		{
			StarGen.Stars stars = this.stars[i];
			float size = stars.Size;
			float num2 = stars.PositionX;
			float num3 = stars.PositionY;
			float num4 = stars.Rate * (size / 0.05f);
			num2 += num4 * vector.x;
			num3 += num4 * vector.y;
			if (this.OrthoDistance(num2, num3) > this.Length)
			{
				float num5 = FloatRange.Next(-1f, 1f) * this.Width;
				num2 = num * this.NormDir.x + num5 * this.Tangent.x;
				num3 = num * this.NormDir.y + num5 * this.Tangent.y;
				this.stars[i].Rate = this.Rates.Next();
			}
			stars.PositionX = num2;
			stars.PositionY = num3;
			this.stars[i] = stars;
			int num6 = i * 4;
			float x = num2 - size;
			float x2 = num2 + size;
			float y = num3 + size;
			float y2 = num3 - size;
			this.verts[num6].x = x;
			this.verts[num6].y = y;
			this.verts[num6 + 1].x = x;
			this.verts[num6 + 1].y = y2;
			this.verts[num6 + 2].x = x2;
			this.verts[num6 + 2].y = y2;
			this.verts[num6 + 3].x = x2;
			this.verts[num6 + 3].y = y;
		}
		this.mesh.vertices = this.verts;
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x0004DC80 File Offset: 0x0004BE80
	public void SetDirection(Vector2 dir)
	{
		this.Direction = dir;
		if (this.Direction.sqrMagnitude < 0.0001f)
		{
			this.NormDir = Vector2.left;
			this.Tangent = Vector2.up;
			this.tanLen = 1f;
			return;
		}
		this.NormDir = this.Direction.normalized;
		this.Tangent = new Vector2(-this.NormDir.y, this.NormDir.x);
		this.tanLen = Mathf.Sqrt(this.Tangent.y * this.Tangent.y + this.Tangent.x * this.Tangent.x);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x0004DD38 File Offset: 0x0004BF38
	public void RegenPositions()
	{
		if (this.stars == null)
		{
			return;
		}
		for (int i = 0; i < this.stars.Length; i++)
		{
			float num = FloatRange.Next(-1f, 1f) * this.Length;
			float num2 = FloatRange.Next(-1f, 1f) * this.Width;
			this.stars[i].PositionX = num * this.NormDir.x + num2 * this.Tangent.x;
			this.stars[i].PositionY = num * this.NormDir.y + num2 * this.Tangent.y;
		}
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0004DDED File Offset: 0x0004BFED
	private float OrthoDistance(float pointx, float pointy)
	{
		return (this.Tangent.y * pointx - this.Tangent.x * pointy) / this.tanLen;
	}

	// Token: 0x04000E19 RID: 3609
	private const float MaxStarRadius = 0.05f;

	// Token: 0x04000E1A RID: 3610
	public int NumStars = 500;

	// Token: 0x04000E1B RID: 3611
	public float Length = 25f;

	// Token: 0x04000E1C RID: 3612
	public float Width = 25f;

	// Token: 0x04000E1D RID: 3613
	public Vector2 Direction = new Vector2(1f, 0f);

	// Token: 0x04000E1E RID: 3614
	private Vector2 NormDir = new Vector2(1f, 0f);

	// Token: 0x04000E1F RID: 3615
	private Vector2 Tangent = new Vector2(0f, 1f);

	// Token: 0x04000E20 RID: 3616
	private float tanLen;

	// Token: 0x04000E21 RID: 3617
	public FloatRange Rates = new FloatRange(0.25f, 1f);

	// Token: 0x04000E22 RID: 3618
	[HideInInspector]
	private StarGen.Stars[] stars;

	// Token: 0x04000E23 RID: 3619
	[HideInInspector]
	private Vector3[] verts;

	// Token: 0x04000E24 RID: 3620
	[HideInInspector]
	private Mesh mesh;

	// Token: 0x0200044C RID: 1100
	[Serializable]
	private struct Stars
	{
		// Token: 0x04001C41 RID: 7233
		public float Size;

		// Token: 0x04001C42 RID: 7234
		public float Rate;

		// Token: 0x04001C43 RID: 7235
		public float PositionX;

		// Token: 0x04001C44 RID: 7236
		public float PositionY;
	}
}
