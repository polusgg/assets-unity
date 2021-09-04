using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class AlphaBlink : MonoBehaviour
{
	// Token: 0x060001D3 RID: 467 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
	public void SetColor(Color c)
	{
		this.Start();
		this.baseColor = c;
		this.Update();
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000E30D File Offset: 0x0000C50D
	private void Start()
	{
		this.mesh = base.GetComponent<MeshRenderer>();
		this.rend = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000E328 File Offset: 0x0000C528
	public void Update()
	{
		float num = Time.time % this.Period / this.Period;
		num = (float)((num < this.Ratio) ? 1 : 0);
		if (this.rend)
		{
			this.rend.color = new Color(this.baseColor.r, this.baseColor.g, this.baseColor.b, this.AlphaRange.Lerp(num));
		}
		if (this.mesh)
		{
			this.mesh.material.SetColor("_Color", new Color(this.baseColor.r, this.baseColor.g, this.baseColor.b, this.AlphaRange.Lerp(num)));
		}
	}

	// Token: 0x040002C8 RID: 712
	public float Period = 1f;

	// Token: 0x040002C9 RID: 713
	public float Ratio = 0.5f;

	// Token: 0x040002CA RID: 714
	private SpriteRenderer rend;

	// Token: 0x040002CB RID: 715
	private MeshRenderer mesh;

	// Token: 0x040002CC RID: 716
	public FloatRange AlphaRange = new FloatRange(0.2f, 0.5f);

	// Token: 0x040002CD RID: 717
	public Color baseColor = Color.white;
}
