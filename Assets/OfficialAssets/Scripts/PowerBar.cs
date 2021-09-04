using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200002E RID: 46
public class PowerBar : MonoBehaviour
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000144 RID: 324 RVA: 0x00008F19 File Offset: 0x00007119
	public int NumSegments
	{
		get
		{
			return this.numberGreen + this.numberRed + this.numberYellow;
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00008F30 File Offset: 0x00007130
	public void Awake()
	{
		this.Segments = new SpriteRenderer[this.NumSegments];
		float num = this.Width / (float)(this.NumSegments - 1);
		float num2 = this.Width / -2f;
		for (int i = 0; i < this.Segments.Length; i++)
		{
			SpriteRenderer spriteRenderer = this.Segments[i] = Object.Instantiate<SpriteRenderer>(this.SegmentPrefab, base.transform);
			spriteRenderer.transform.localPosition = new Vector3(num2 + (float)i * num, 0f, 0.0001f);
			if (i < this.numberGreen)
			{
				spriteRenderer.sprite = this.greenImage;
			}
			else if (i < this.numberGreen + this.numberYellow)
			{
				spriteRenderer.sprite = this.yellowImage;
			}
			else
			{
				spriteRenderer.sprite = this.redImage;
			}
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00009005 File Offset: 0x00007205
	public void SetValue(float value)
	{
		this.value = value;
		this.Update();
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00009014 File Offset: 0x00007214
	public void Update()
	{
		for (int i = 0; i < this.Segments.Length; i++)
		{
			SpriteRenderer spriteRenderer = this.Segments[i];
			if ((float)i < this.value * (float)this.NumSegments)
			{
				if (i < this.numberGreen)
				{
					spriteRenderer.sprite = this.greenImage;
				}
				else if (i < this.numberGreen + this.numberYellow)
				{
					spriteRenderer.sprite = this.yellowImage;
				}
				else
				{
					spriteRenderer.sprite = this.redImage;
				}
				spriteRenderer.color = Color.white;
			}
			else
			{
				if (i < this.numberGreen)
				{
					spriteRenderer.sprite = this.greenEmptyImage;
				}
				else if (i < this.numberGreen + this.numberYellow)
				{
					spriteRenderer.sprite = this.yellowEmptyImage;
				}
				else
				{
					spriteRenderer.sprite = this.redEmptyImage;
				}
				spriteRenderer.color = Color.gray;
			}
		}
	}

	// Token: 0x04000195 RID: 405
	public SpriteRenderer SegmentPrefab;

	// Token: 0x04000196 RID: 406
	public Sprite greenImage;

	// Token: 0x04000197 RID: 407
	public Sprite yellowImage;

	// Token: 0x04000198 RID: 408
	public Sprite redImage;

	// Token: 0x04000199 RID: 409
	public Sprite greenEmptyImage;

	// Token: 0x0400019A RID: 410
	public Sprite yellowEmptyImage;

	// Token: 0x0400019B RID: 411
	public Sprite redEmptyImage;

	// Token: 0x0400019C RID: 412
	public int numberGreen = 11;

	// Token: 0x0400019D RID: 413
	public int numberYellow = 6;

	// Token: 0x0400019E RID: 414
	public int numberRed = 3;

	// Token: 0x0400019F RID: 415
	public float Width;

	// Token: 0x040001A0 RID: 416
	private float value = 0.5f;

	// Token: 0x040001A1 RID: 417
	private SpriteRenderer[] Segments;
}
