using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class AspectSize : MonoBehaviour
{
	// Token: 0x0600043D RID: 1085 RVA: 0x0001B5F8 File Offset: 0x000197F8
	public void OnEnable()
	{
		Camera main = Camera.main;
		float num = main.orthographicSize * main.aspect;
		float num2 = (this.Background ? this.Background : this.Renderer.sprite).bounds.size.x / 2f;
		float num3 = num / num2 * this.PercentWidth;
		if (num3 < 1f)
		{
			base.transform.localScale = new Vector3(num3, num3, num3);
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001B678 File Offset: 0x00019878
	public static float CalculateSize(Vector3 parentPos, Sprite sprite)
	{
		Camera main = Camera.main;
		float num = main.orthographicSize * main.aspect + parentPos.x;
		float x = sprite.bounds.size.x;
		float num2 = num / x * 0.98f;
		return Mathf.Min(1f, num2);
	}

	// Token: 0x040004FB RID: 1275
	public Sprite Background;

	// Token: 0x040004FC RID: 1276
	public SpriteRenderer Renderer;

	// Token: 0x040004FD RID: 1277
	public float PercentWidth = 0.95f;
}
