﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SpriteParticle : MonoBehaviour
{
	// Token: 0x060002AF RID: 687 RVA: 0x00010FF8 File Offset: 0x0000F1F8
	public void OnDrawGizmos()
	{
		if (this.Particles == null || this.Sprites == null)
		{
			return;
		}
		if (this.Sprites.Length == 0)
		{
			return;
		}
		Sprite sprite = this.Sprites[0];
		for (int i = 0; i < this.Particles.Length; i++)
		{
			ParticleInfo particleInfo = this.Particles[i];
			Gizmos.DrawCube(particleInfo.Position + base.transform.position, new Vector3(sprite.bounds.size.x * particleInfo.Scale, sprite.bounds.size.y * particleInfo.Scale, sprite.bounds.size.x * particleInfo.Scale));
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x000110BC File Offset: 0x0000F2BC
	public void Start()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		this.mesh = new Mesh();
		this.mesh.MarkDynamic();
		component.mesh = this.mesh;
		this.TriangleCache = new ushort[this.Sprites.Length][];
		for (int i = 0; i < this.Sprites.Length; i++)
		{
			Sprite sprite = this.Sprites[i];
			Vector2[] vertices = sprite.vertices;
			this.TriangleCache[i] = sprite.triangles;
			this.VertCache[i] = vertices;
			this.UvCache[i] = sprite.uv;
			if (this.MaxVerts < vertices.Length)
			{
				this.MaxVerts = vertices.Length;
			}
		}
		this.verts = new Vector3[this.Particles.Length * this.MaxVerts];
		this.uvs = new Vector2[this.verts.Length];
		for (int j = 0; j < this.Particles.Length; j++)
		{
			int num = j * this.MaxVerts;
			int num2 = (int)this.Particles[j].Timer;
			Sprite sprite2 = this.Sprites[num2];
			Vector2[] vertices2 = sprite2.vertices;
			Vector2[] uv = sprite2.uv;
			for (int k = 0; k < sprite2.vertices.Length; k++)
			{
				int num3 = num + k;
				this.verts[num3].x = vertices2[k].x * this.Particles[j].Scale + this.Particles[j].Position.x;
				this.verts[num3].y = vertices2[k].y * this.Particles[j].Scale + this.Particles[j].Position.y;
				this.uvs[num3] = uv[k];
			}
			ushort[] triangles = sprite2.triangles;
			for (int l = 0; l < triangles.Length; l++)
			{
				this.tris.Add((int)triangles[l]);
			}
		}
		this.mesh.vertices = this.verts;
		this.mesh.uv = this.uvs;
		this.mesh.SetTriangles(this.tris, 0);
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00011318 File Offset: 0x0000F518
	public void Update()
	{
		float num = Time.deltaTime * 24f;
		this.tris.Clear();
		for (int i = 0; i < this.Particles.Length; i++)
		{
			float num2 = this.Particles[i].Timer + num;
			if (num2 >= (float)this.Sprites.Length)
			{
				num2 %= 24f;
			}
			this.Particles[i].Timer = num2;
			int num3 = i * this.MaxVerts;
			int num4 = (int)this.Particles[i].Timer;
			Vector2[] array = this.VertCache[num4];
			Vector2[] array2 = this.UvCache[num4];
			for (int j = 0; j < array.Length; j++)
			{
				int num5 = num3 + j;
				this.verts[num5].x = array[j].x * this.Particles[i].Scale + this.Particles[i].Position.x;
				this.verts[num5].y = array[j].y * this.Particles[i].Scale + this.Particles[i].Position.y;
				this.uvs[num5] = array2[j];
			}
			ushort[] array3 = this.TriangleCache[num4];
			for (int k = 0; k < array3.Length; k++)
			{
				this.tris.Add((int)array3[k] + num3);
			}
		}
		this.mesh.vertices = this.verts;
		this.mesh.uv = this.uvs;
		this.mesh.SetTriangles(this.tris, 0);
	}

	// Token: 0x04000318 RID: 792
	private const float FrameRate = 24f;

	// Token: 0x04000319 RID: 793
	public Sprite[] Sprites;

	// Token: 0x0400031A RID: 794
	public ParticleInfo[] Particles;

	// Token: 0x0400031B RID: 795
	public ushort[][] TriangleCache;

	// Token: 0x0400031C RID: 796
	private Vector3[] verts;

	// Token: 0x0400031D RID: 797
	private Vector2[] uvs;

	// Token: 0x0400031E RID: 798
	private List<int> tris = new List<int>();

	// Token: 0x0400031F RID: 799
	private Mesh mesh;

	// Token: 0x04000320 RID: 800
	private int MaxVerts;

	// Token: 0x04000321 RID: 801
	private Dictionary<int, Vector2[]> VertCache = new Dictionary<int, Vector2[]>();

	// Token: 0x04000322 RID: 802
	private Dictionary<int, Vector2[]> UvCache = new Dictionary<int, Vector2[]>();
}
