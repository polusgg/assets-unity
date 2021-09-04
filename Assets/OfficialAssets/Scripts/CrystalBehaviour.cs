using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class CrystalBehaviour : UiElement
{
	// Token: 0x06000649 RID: 1609 RVA: 0x00028444 File Offset: 0x00026644
	private void Update()
	{
		if (!this.TargetPosition)
		{
			return;
		}
		Vector3 localPosition = base.transform.localPosition;
		if ((double)Vector2.Distance(this.TargetPosition.localPosition, base.transform.localPosition) > 0.01)
		{
			localPosition = Vector3.Lerp(this.TargetPosition.localPosition, base.transform.localPosition, Time.deltaTime);
		}
		float num = Time.time * 0.35f;
		localPosition.x += (Mathf.PerlinNoise(num, this.PieceIndex * 100f) * 2f - 1f) * this.XFloatMag;
		localPosition.y += (Mathf.PerlinNoise(this.PieceIndex * 100f, num) * 2f - 1f) * 0.05f;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00028534 File Offset: 0x00026734
	public void Flash(float delay = 0f)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(CrystalBehaviour.Flash(this, delay));
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0002854A File Offset: 0x0002674A
	private static IEnumerator Flash(CrystalBehaviour c, float delay)
	{
		for (float time = 0f; time < delay; time += Time.deltaTime)
		{
			yield return null;
		}
		Color col = Color.clear;
		for (float time = 0f; time < 0.1f; time += Time.deltaTime)
		{
			float num = time / 0.1f;
			col.r = (col.g = (col.b = Mathf.Lerp(0f, 1f, num)));
			c.Renderer.material.SetColor("_AddColor", col);
			yield return null;
		}
		for (float time = 0f; time < 0.1f; time += Time.deltaTime)
		{
			float num2 = time / 0.1f;
			col.r = (col.g = (col.b = Mathf.Lerp(1f, 0f, num2)));
			c.Renderer.material.SetColor("_AddColor", col);
			yield return null;
		}
		col.r = (col.g = (col.b = 0f));
		c.Renderer.material.SetColor("_AddColor", col);
		yield break;
	}

	// Token: 0x040006FD RID: 1789
	public Transform TargetPosition;

	// Token: 0x040006FE RID: 1790
	public SpriteRenderer Renderer;

	// Token: 0x040006FF RID: 1791
	public BoxCollider2D Collider;

	// Token: 0x04000700 RID: 1792
	public bool CanMove = true;

	// Token: 0x04000701 RID: 1793
	public FloatRange Padding;

	// Token: 0x04000702 RID: 1794
	private const float Speed = 15f;

	// Token: 0x04000703 RID: 1795
	public float XFloatMag = 0.01f;

	// Token: 0x04000704 RID: 1796
	private const float FloatMag = 0.05f;

	// Token: 0x04000705 RID: 1797
	private const float FloatSpeed = 0.35f;

	// Token: 0x04000706 RID: 1798
	public float PieceIndex;
}
