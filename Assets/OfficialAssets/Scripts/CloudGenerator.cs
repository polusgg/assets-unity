using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class CloudGenerator : MonoBehaviour
{
	// Token: 0x060003CA RID: 970 RVA: 0x00018FF0 File Offset: 0x000171F0
	public void Start()
	{
		Vector2[][] array = new Vector2[this.CloudImages.Length][];
		this.ExtentCache = new Vector2[this.CloudImages.Length];
		for (int i = 0; i < this.CloudImages.Length; i++)
		{
			Sprite sprite = this.CloudImages[i];
			array[i] = sprite.uv;
			this.ExtentCache[i] = sprite.bounds.extents;
		}
		this.clouds = new CloudGenerator.Cloud[this.NumClouds];
		this.verts = new Vector3[this.NumClouds * 4];
		Vector2[] array2 = new Vector2[this.NumClouds * 4];
		int[] array3 = new int[this.NumClouds * 6];
		this.SetDirection(this.Direction);
		MeshFilter component = base.GetComponent<MeshFilter>();
		this.mesh = new Mesh();
		this.mesh.MarkDynamic();
		component.mesh = this.mesh;
		Vector3 vector = default(Vector3);
		for (int j = 0; j < this.clouds.Length; j++)
		{
			CloudGenerator.Cloud cloud = this.clouds[j];
			int num = cloud.CloudIdx = this.CloudImages.RandomIdx<Sprite>();
			Vector2 vector2 = this.ExtentCache[num];
			Vector2[] array4 = array[num];
			float num2 = FloatRange.Next(-1f, 1f) * this.Length;
			float num3 = FloatRange.Next(-1f, 1f) * this.Width;
			float num4 = cloud.PositionX = num2 * this.NormDir.x + num3 * this.Tangent.x;
			float num5 = cloud.PositionY = num2 * this.NormDir.y + num3 * this.Tangent.y;
			cloud.Rate = this.Rates.Next();
			cloud.Size = this.Sizes.Next();
			cloud.FlipX = (float)(BoolRange.Next(0.5f) ? -1 : 1);
			if (this.Depth)
			{
				cloud.PositionZ = FloatRange.Next(0f, this.MaxDepth);
			}
			vector2 *= cloud.Size;
			this.clouds[j] = cloud;
			int num6 = j * 4;
			vector.x = num4 - vector2.x * cloud.FlipX;
			vector.y = num5 + vector2.y;
			vector.z = cloud.PositionZ;
			this.verts[num6] = vector;
			vector.x = num4 + vector2.x * cloud.FlipX;
			this.verts[num6 + 1] = vector;
			vector.x = num4 - vector2.x * cloud.FlipX;
			vector.y = num5 - vector2.y;
			this.verts[num6 + 2] = vector;
			vector.x = num4 + vector2.x * cloud.FlipX;
			this.verts[num6 + 3] = vector;
			array2[num6] = array4[0];
			array2[num6 + 1] = array4[1];
			array2[num6 + 2] = array4[2];
			array2[num6 + 3] = array4[3];
			int num7 = j * 6;
			array3[num7] = num6;
			array3[num7 + 1] = num6 + 1;
			array3[num7 + 2] = num6 + 2;
			array3[num7 + 3] = num6 + 2;
			array3[num7 + 4] = num6 + 1;
			array3[num7 + 5] = num6 + 3;
		}
		this.mesh.vertices = this.verts;
		this.mesh.uv = array2;
		this.mesh.SetIndices(array3, 0, 0);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000193CC File Offset: 0x000175CC
	private void Update()
	{
		float num = -0.99f * this.Length;
		Vector2 vector = this.Direction * Time.deltaTime;
		Vector3 vector2 = default(Vector3);
		for (int i = 0; i < this.clouds.Length; i++)
		{
			int num2 = i * 4;
			CloudGenerator.Cloud cloud = this.clouds[i];
			float num3 = cloud.PositionX;
			float num4 = cloud.PositionY;
			Vector2 vector3 = this.ExtentCache[cloud.CloudIdx];
			vector3 *= cloud.Size;
			float rate = cloud.Rate;
			num3 += rate * vector.x;
			num4 += rate * vector.y;
			if (this.OrthoDistance(num3, num4) > this.Length)
			{
				float num5 = FloatRange.Next(-1f, 1f) * this.Width;
				num3 = num * this.NormDir.x + num5 * this.Tangent.x;
				num4 = num * this.NormDir.y + num5 * this.Tangent.y;
				cloud.Rate = this.Rates.Next();
				cloud.Size = this.Sizes.Next();
				cloud.FlipX = (float)(BoolRange.Next(0.5f) ? -1 : 1);
				if (this.Depth)
				{
					cloud.PositionZ = FloatRange.Next(0f, this.MaxDepth);
				}
			}
			cloud.PositionX = num3;
			cloud.PositionY = num4;
			if (this.Depth)
			{
				num4 += (base.transform.position.y + this.ParallaxOffset) / (cloud.PositionZ * this.ParallaxStrength + 0.0001f);
				vector2.z = cloud.PositionZ;
			}
			this.clouds[i] = cloud;
			vector2.x = num3 - vector3.x * cloud.FlipX;
			vector2.y = num4 + vector3.y;
			this.verts[num2] = vector2;
			vector2.x = num3 + vector3.x * cloud.FlipX;
			this.verts[num2 + 1] = vector2;
			vector2.x = num3 - vector3.x * cloud.FlipX;
			vector2.y = num4 - vector3.y;
			this.verts[num2 + 2] = vector2;
			vector2.x = num3 + vector3.x * cloud.FlipX;
			this.verts[num2 + 3] = vector2;
		}
		this.mesh.vertices = this.verts;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00019688 File Offset: 0x00017888
	public void SetDirection(Vector2 dir)
	{
		this.Direction = dir;
		this.NormDir = this.Direction.normalized;
		this.Tangent = new Vector2(-this.NormDir.y, this.NormDir.x);
		this.tanLen = Mathf.Sqrt(this.Tangent.y * this.Tangent.y + this.Tangent.x * this.Tangent.x);
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00019709 File Offset: 0x00017909
	private float OrthoDistance(float pointx, float pointy)
	{
		return (this.Tangent.y * pointx - this.Tangent.x * pointy) / this.tanLen;
	}

	// Token: 0x04000462 RID: 1122
	public Sprite[] CloudImages;

	// Token: 0x04000463 RID: 1123
	private Vector2[] ExtentCache;

	// Token: 0x04000464 RID: 1124
	public int NumClouds = 500;

	// Token: 0x04000465 RID: 1125
	public float Length = 25f;

	// Token: 0x04000466 RID: 1126
	public float Width = 25f;

	// Token: 0x04000467 RID: 1127
	public Vector2 Direction = new Vector2(1f, 0f);

	// Token: 0x04000468 RID: 1128
	private Vector2 NormDir = new Vector2(1f, 0f);

	// Token: 0x04000469 RID: 1129
	private Vector2 Tangent = new Vector2(0f, 1f);

	// Token: 0x0400046A RID: 1130
	private float tanLen;

	// Token: 0x0400046B RID: 1131
	public FloatRange Rates = new FloatRange(0.25f, 1f);

	// Token: 0x0400046C RID: 1132
	public FloatRange Sizes = new FloatRange(0.75f, 1.25f);

	// Token: 0x0400046D RID: 1133
	public bool Depth;

	// Token: 0x0400046E RID: 1134
	public float MaxDepth = 4f;

	// Token: 0x0400046F RID: 1135
	public float ParallaxOffset;

	// Token: 0x04000470 RID: 1136
	public float ParallaxStrength = 1f;

	// Token: 0x04000471 RID: 1137
	[HideInInspector]
	private CloudGenerator.Cloud[] clouds;

	// Token: 0x04000472 RID: 1138
	[HideInInspector]
	private Vector3[] verts;

	// Token: 0x04000473 RID: 1139
	[HideInInspector]
	private Mesh mesh;

	// Token: 0x02000339 RID: 825
	private struct Cloud
	{
		// Token: 0x04001847 RID: 6215
		public int CloudIdx;

		// Token: 0x04001848 RID: 6216
		public float Rate;

		// Token: 0x04001849 RID: 6217
		public float Size;

		// Token: 0x0400184A RID: 6218
		public float FlipX;

		// Token: 0x0400184B RID: 6219
		public float PositionX;

		// Token: 0x0400184C RID: 6220
		public float PositionY;

		// Token: 0x0400184D RID: 6221
		public float PositionZ;
	}
}
