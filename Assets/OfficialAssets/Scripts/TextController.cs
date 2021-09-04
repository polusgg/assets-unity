using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TextController : MonoBehaviour
{
	// Token: 0x060005E5 RID: 1509 RVA: 0x00026798 File Offset: 0x00024998
	public void Update()
	{
		//if (!this.rend)
		//{
		//	this.rend = base.GetComponent<MeshRenderer>();
		//}
		//if (string.IsNullOrEmpty(this.Text))
		//{
		//	this.rend.enabled = false;
		//	return;
		//}
		//if (this.displaying == null || this.displaying.GetHashCode() != this.Text.GetHashCode() || this.Color != this.lastColor)
		//{
		//	int num = 0;
		//	int num2 = 0;
		//	int num3 = 1;
		//	for (int i = 0; i < this.Text.Length; i++)
		//	{
		//		if (this.Text[i] == '\n')
		//		{
		//			num2 = 0;
		//			num3++;
		//		}
		//		else
		//		{
		//			num2++;
		//			if (num2 > num)
		//			{
		//				num = num2;
		//			}
		//		}
		//	}
		//	if (!this.texture || !this.colorTexture)
		//	{
		//		if (!this.texture)
		//		{
		//			this.texture = new Texture2D(num, num3, 5, false);
		//			this.texture.filterMode = 0;
		//			this.texture.wrapMode = 1;
		//		}
		//		if (!this.colorTexture)
		//		{
		//			this.colorTexture = new Texture2D(num, num3, 5, false);
		//			this.colorTexture.filterMode = 0;
		//			this.colorTexture.wrapMode = 1;
		//		}
		//	}
		//	else if (this.texture.width != num || this.texture.height != num3)
		//	{
		//		this.texture.Resize(num, num3, 5, false);
		//		this.colorTexture.Resize(num, num3, 5, false);
		//	}
		//	Color[] array = new Color[num * num3];
		//	array.SetAll(this.Color);
		//	this.colorTexture.SetPixels(array);
		//	array.SetAll(new Color(0.125f, 0f, 0f));
		//	this.texture.SetPixels(array);
		//	int num4 = 0;
		//	int num5 = this.texture.height - 1;
		//	Color color = this.Color;
		//	for (int j = 0; j < this.Text.Length; j++)
		//	{
		//		char c = this.Text[j];
		//		if (c != '\r')
		//		{
		//			if (c == '\n')
		//			{
		//				num4 = 0;
		//				num5--;
		//			}
		//			else
		//			{
		//				this.texture.SetPixel(num4, num5, new Color((float)c / 256f, 0f, 0f));
		//				this.colorTexture.SetPixel(num4, num5, color);
		//				num4++;
		//			}
		//		}
		//	}
		//	this.texture.Apply(false, false);
		//	this.colorTexture.Apply(false, false);
		//	this.rend.enabled = true;
		//	this.rend.material.SetTexture("_InputTex", this.texture);
		//	this.rend.material.SetTexture("_ColorTex", this.colorTexture);
		//	this._scale = float.NegativeInfinity;
		//	this.displaying = this.Text;
		//	this.lastColor = this.Color;
		//}
		//if (this._scale != this.Scale)
		//{
		//	this._scale = this.Scale;
		//	base.transform.localScale = new Vector3((float)this.texture.width, (float)this.texture.height, 1f) * this.Scale;
		//	if (this.topAligned)
		//	{
		//		base.transform.localPosition = this.Offset + new Vector3((float)this.texture.width * this.Scale / 2f, (float)(-(float)this.texture.height) * this.Scale / 2f, 0f);
		//	}
		//}
	}

	// Token: 0x04000695 RID: 1685
	public float Scale = 1f;

	// Token: 0x04000696 RID: 1686
	[Multiline]
	public string Text;

	// Token: 0x04000697 RID: 1687
	private string displaying;

	// Token: 0x04000698 RID: 1688
	[HideInInspector]
	private Texture2D texture;

	// Token: 0x04000699 RID: 1689
	[HideInInspector]
	private Texture2D colorTexture;

	// Token: 0x0400069A RID: 1690
	private MeshRenderer rend;

	// Token: 0x0400069B RID: 1691
	private float _scale = float.NegativeInfinity;

	// Token: 0x0400069C RID: 1692
	public Color Color = Color.white;

	// Token: 0x0400069D RID: 1693
	private Color lastColor;

	// Token: 0x0400069E RID: 1694
	public Vector3 Offset;

	// Token: 0x0400069F RID: 1695
	public bool topAligned;
}
