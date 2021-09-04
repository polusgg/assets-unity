using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class AlphaPulse : MonoBehaviour
{
	// Token: 0x060001D7 RID: 471 RVA: 0x0000E434 File Offset: 0x0000C634
	public void SetColor(Color c)
	{
		this.Start();
		this.baseColor = c;
		this.Update();
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000E449 File Offset: 0x0000C649
	private void Start()
	{
		this.mesh = base.GetComponent<MeshRenderer>();
		this.rend = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000E464 File Offset: 0x0000C664
	public void Update()
	{
		float v = Mathf.Abs(Mathf.Cos((this.Offset + Time.time) * 3.1415927f / this.Duration));
		if (this.rend)
		{
			this.rend.color = new Color(this.baseColor.r, this.baseColor.g, this.baseColor.b, this.AlphaRange.Lerp(v));
		}
		if (this.mesh)
		{
			this.mesh.material.SetColor("_Color", new Color(this.baseColor.r, this.baseColor.g, this.baseColor.b, this.AlphaRange.Lerp(v)));
		}
	}

	// Token: 0x040002CE RID: 718
	public float Offset = 1f;

	// Token: 0x040002CF RID: 719
	public float Duration = 2.5f;

	// Token: 0x040002D0 RID: 720
	private SpriteRenderer rend;

	// Token: 0x040002D1 RID: 721
	private MeshRenderer mesh;

	// Token: 0x040002D2 RID: 722
	public FloatRange AlphaRange = new FloatRange(0.2f, 0.5f);

	// Token: 0x040002D3 RID: 723
	public Color baseColor = Color.white;
}
