using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000070 RID: 112
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TextRenderer : MonoBehaviour
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x00011D78 File Offset: 0x0000FF78
	// (set) Token: 0x060002CA RID: 714 RVA: 0x00011D80 File Offset: 0x0000FF80
	public float Width { get; private set; }

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060002CB RID: 715 RVA: 0x00011D89 File Offset: 0x0000FF89
	// (set) Token: 0x060002CC RID: 716 RVA: 0x00011D91 File Offset: 0x0000FF91
	public float Height { get; private set; }

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060002CD RID: 717 RVA: 0x00011D9A File Offset: 0x0000FF9A
	public Vector3 CursorPos
	{
		get
		{
			return new Vector3(this.cursorLocation.x / 100f * this.scale, this.cursorLocation.y / 100f * this.scale, -0.001f);
		}
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00011DD8 File Offset: 0x0000FFD8
	public void Start()
	{
		this.render = base.GetComponent<MeshRenderer>();
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (!component.mesh)
		{
			this.mesh = new Mesh();
			this.mesh.name = "Text" + base.name;
			component.mesh = this.mesh;
			this.render.material.SetColor("_OutlineColor", this.OutlineColor);
		}
		else
		{
			this.mesh = component.mesh;
		}
		if (Mathf.Approximately(base.transform.localPosition.z, 0f))
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -0.3f);
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00011EB4 File Offset: 0x000100B4
	[ContextMenu("Generate Mesh")]
	public void GenerateMesh()
	{
		this.render = base.GetComponent<MeshRenderer>();
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (!component.sharedMesh)
		{
			this.mesh = new Mesh();
			this.mesh.name = "Text" + base.name;
			component.mesh = this.mesh;
		}
		else
		{
			this.mesh = component.sharedMesh;
		}
		this.lastText = null;
		this.lastOutlineColor = this.OutlineColor;
		this.Update();
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00011F3C File Offset: 0x0001013C
	private void Update()
	{
		if (this.lastOutlineColor != this.OutlineColor)
		{
			this.lastOutlineColor = this.OutlineColor;
			this.render.material.SetColor("_OutlineColor", this.OutlineColor);
		}
		if (this.lastText != this.Text || this.lastColor != this.Color)
		{
			this.RefreshMesh();
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00011FB0 File Offset: 0x000101B0
	public void RefreshMesh()
	{

        if (!this.render || !this.mesh)
        {
            this.Start();
        }
        if (this.Text != null)
        {
            if (this.FontData.name != "Japanese")
            {
                if (this.Text.Any((char c) => (c >= '぀' && c <= 'ヿ') || (c >= '一' && c <= '鿆') || (c >= '！' && c <= '￮')))
                {
                    FontCache.Instance.SetFont(this, "Japanese");
                    goto IL_19A;
                }
            }
            if (this.FontData.name != "Korean")
            {
                if (this.Text.Any((char c) => (c >= '㄰' && c <= '㆏') || (c >= '가' && c <= '힣')))
                {
                    FontCache.Instance.SetFont(this, "Korean");
                    goto IL_19A;
                }
            }
            if (this.FontData.name.Equals("Japanese") || this.FontData.name.Equals("Korean"))
            {
                if (this.Text.All((char c) => c < 'ӟ'))
                {
                    FontCache.Instance.SetFont(this, "Arial");
                    goto IL_19A;
                }
            }
            if (!this.FontData.name.Contains("Arial"))
            {
                if (this.Text.Any((char c) => c > 'Ͽ' && c < 'ӟ'))
                {
                    FontCache.Instance.SetFont(this, "Arial");
                }
            }
        }
    IL_19A:
        FontData fontData = FontCache.Instance.LoadFont(this.FontData);
        this.lastText = this.Text;
        this.lastColor = this.Color;
        float num = this.scale;
        float num2 = 0f;
        if (this.minLineHeight > 0f)
        {
            float num3 = fontData.bounds[0].w / 100f;
            num = Mathf.Max(num, this.minLineHeight / num3);
        }
        if (this.scaleToFit)
        {
            num = Mathf.Min(num, this.maxWidth / this.GetMaxWidth(fontData, this.lastText));
            num2 = fontData.LineHeight * (num - this.scale);
        }
        else if (this.maxWidth > 0f)
        {
            this.lastText = (this.Text = TextRenderer.WrapText(fontData, this.lastText, this.maxWidth));
        }
        List<Vector3> list = new List<Vector3>(this.lastText.Length * 4);
        List<Vector2> list2 = new List<Vector2>(this.lastText.Length * 4);
        List<Vector4> list3 = new List<Vector4>(this.lastText.Length * 4);
        List<Color> list4 = new List<Color>(this.lastText.Length * 4);
        int[] array = new int[this.lastText.Length * 6];
        this.Width = 0f;
        this.cursorLocation.x = (this.cursorLocation.y = 0f);
        int num4 = -1;
        Vector2 from = default(Vector2);
        string text = null;
        int lineStart = 0;
        int num5 = 0;
        Color item = this.Color;
        int? num6 = null;
        int i = 0;
        while (i < this.lastText.Length)
        {
            int num7 = (int)this.lastText[i];
            if (num7 == 91)
            {
                if (num4 != 91)
                {
                    if (this.lastText[i + 1] == '[')
                    {
                        goto IL_5AC;
                    }
                    num6 = new int?(0);
                    num4 = num7;
                }
            }
            else
            {
                if (num6 == null)
                {
                    goto IL_5AC;
                }
                if (num7 == 93)
                {
                    if (num4 != 91)
                    {
                        int? num8 = num6;
                        byte b = (byte)((num8 != null) ? new int?(num8.GetValueOrDefault() >> 24 & 255) : null).Value;
                        int? num9 = num6;
                        byte b2 = (byte)((num9 != null) ? new int?(num9.GetValueOrDefault() >> 16 & 255) : null).Value;
                        int? num10 = num6;
                        item = new Color32(b, b2, (byte)((num10 != null) ? new int?(num10.GetValueOrDefault() >> 8 & 255) : null).Value, (byte)(num6 & 255).Value);
                        item.a *= this.Color.a;
                    }
                    else
                    {
                        item = this.Color;
                    }
                    num6 = null;
                    if (text != null)
                    {
                        TextLink textLink = Object.Instantiate<TextLink>(this.textLinkPrefab, base.transform);
                        textLink.transform.localScale = Vector3.one;
                        Vector3 vector = list.Last<Vector3>();
                        textLink.Set(from, vector, text);
                        text = null;
                    }
                }
                else if (num7 == 104)
                {
                    int num11 = this.lastText.IndexOf(']', i);
                    text = this.lastText.Substring(i, num11 - i);
                    from = list[list.Count - 2];
                    item = new Color(0.5f, 0.5f, 1f);
                    num6 = null;
                    i = num11;
                }
                else
                {
                    num6 = (num6 << 4 | this.CharToInt(num7));
                }
                num4 = num7;
            }
        IL_9B5:
            i++;
            continue;
        IL_5AC:
            if (num7 == 13)
            {
                goto IL_9B5;
            }
            if (num7 == 10)
            {
                if (this.Centered)
                {
                    this.CenterVerts(list, this.cursorLocation.x, lineStart, num);
                }
                else if (this.RightAligned)
                {
                    this.RightAlignVerts(list, this.cursorLocation.x, lineStart, num);
                }
                bool flag = this.cursorLocation.x == 0f;
                this.cursorLocation.x = 0f;
                if (flag)
                {
                    this.cursorLocation.y = this.cursorLocation.y - fontData.LineHeight / 2f;
                }
                else
                {
                    this.cursorLocation.y = this.cursorLocation.y - fontData.LineHeight;
                }
                lineStart = list.Count;
                goto IL_9B5;
            }
            if (num7 == 9)
            {
                float num12 = this.cursorLocation.x / 100f;
                num12 = Mathf.Ceil(num12 / this.TabWidth + 0.0001f) * this.TabWidth;
                this.cursorLocation.x = num12 * 100f;
                goto IL_9B5;
            }
            int index;
            if (!fontData.charMap.TryGetValue(num7, out index))
            {
                Debug.Log("Missing char :" + num7.ToString());
                num7 = -1;
                index = fontData.charMap[-1];
            }
            Vector4 vector2 = fontData.bounds[index];
            Vector2 textureSize = fontData.TextureSize;
            Vector3 vector3 = fontData.offsets[index];
            float kerning = fontData.GetKerning(num4, num7);
            float num13 = this.cursorLocation.x + vector3.x + kerning;
            float num14 = this.cursorLocation.y - vector3.y + num2;
            list.Add(new Vector3(num13, num14 - vector2.w) / 100f * num);
            list.Add(new Vector3(num13, num14) / 100f * num);
            list.Add(new Vector3(num13 + vector2.z, num14) / 100f * num);
            list.Add(new Vector3(num13 + vector2.z, num14 - vector2.w) / 100f * num);
            list4.Add(item);
            list4.Add(item);
            list4.Add(item);
            list4.Add(item);
            list2.Add(new Vector2(vector2.x / textureSize.x, 1f - (vector2.y + vector2.w) / textureSize.y));
            list2.Add(new Vector2(vector2.x / textureSize.x, 1f - vector2.y / textureSize.y));
            list2.Add(new Vector2((vector2.x + vector2.z) / textureSize.x, 1f - vector2.y / textureSize.y));
            list2.Add(new Vector2((vector2.x + vector2.z) / textureSize.x, 1f - (vector2.y + vector2.w) / textureSize.y));
            Vector4 item2 = fontData.Channels[index];
            list3.Add(item2);
            list3.Add(item2);
            list3.Add(item2);
            list3.Add(item2);
            array[num5 * 6] = num5 * 4;
            array[num5 * 6 + 1] = num5 * 4 + 1;
            array[num5 * 6 + 2] = num5 * 4 + 2;
            array[num5 * 6 + 3] = num5 * 4;
            array[num5 * 6 + 4] = num5 * 4 + 2;
            array[num5 * 6 + 5] = num5 * 4 + 3;
            this.cursorLocation.x = this.cursorLocation.x + (vector3.z + kerning);
            float num15 = this.cursorLocation.x / 100f * num;
            if (this.Width < num15)
            {
                this.Width = num15;
            }
            num4 = num7;
            num5++;
            goto IL_9B5;
        }
        if (this.Centered)
        {
            this.CenterVerts(list, this.cursorLocation.x, lineStart, num);
            this.cursorLocation.x = this.cursorLocation.x / 2f;
            this.Width /= 2f;
        }
        else if (this.RightAligned)
        {
            this.RightAlignVerts(list, this.cursorLocation.x, lineStart, num);
        }
        this.Height = -(this.cursorLocation.y - fontData.LineHeight) / 100f * num;
        this.mesh.Clear();
        if (list.Count > 0)
        {
            this.mesh.SetVertices(list);
            this.mesh.SetColors(list4);
            this.mesh.SetUVs(0, list2);
            this.mesh.SetUVs(1, list3);
            this.mesh.SetIndices(array, 0, 0);
            if (this.trackCharacterPosition)
            {
                this.characterPosData = new Vector4[list.Count];
                int num16 = 0;
                int j = 0;
                while (j < list.Count)
                {
                    int index2 = j;
                    int index3 = j + 2;
                    Vector4 vector4 = default(Vector4);
                    Vector3 vector5 = list[index2];
                    Vector3 vector6 = list[index3] - vector5;
                    vector4.x = vector5.x;
                    vector4.y = vector5.y;
                    vector4.z = vector6.x;
                    vector4.w = vector6.y;
                    this.characterPosData[num16] = vector4;
                    j += 4;
                    num16++;
                }
            }
        }
    }

	// Token: 0x060002D2 RID: 722 RVA: 0x00012B1C File Offset: 0x00010D1C
	private float GetMaxWidth(FontData data, string lastText)
	{
		float num = 0f;
		float num2 = 0f;
		int last = -1;
		bool flag = false;
		int num3 = 0;
		int num4 = 0;
		while (num4 < lastText.Length && num3++ <= 1000)
		{
			int num5 = (int)lastText[num4];
			if (num5 == 91)
			{
				flag = true;
				goto IL_4D;
			}
			if (num5 != 93)
			{
				goto IL_4D;
			}
			flag = false;
			IL_100:
			num4++;
			continue;
			IL_4D:
			if (flag || num5 == 13)
			{
				goto IL_100;
			}
			if (num5 == 10)
			{
				last = -1;
				num2 = 0f;
				goto IL_100;
			}
			if (num5 == 9)
			{
				num2 = Mathf.Ceil(num2 / 100f / 0.5f) * 0.5f * 100f;
				goto IL_100;
			}
			int index;
			if (!data.charMap.TryGetValue(num5, out index))
			{
				Debug.Log("Missing char :" + num5.ToString());
				num5 = -1;
				index = data.charMap[-1];
			}
			Vector3 vector = data.offsets[index];
			num2 += vector.z + data.GetKerning(last, num5);
			if (num2 > num)
			{
				num = num2;
			}
			last = num5;
			goto IL_100;
		}
		return num / 100f;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00012C44 File Offset: 0x00010E44
	private void RightAlignVerts(List<Vector3> verts, float baseX, int lineStart, float scale)
	{
		for (int i = lineStart; i < verts.Count; i++)
		{
			Vector3 value = verts[i];
			value.x -= baseX / 100f * scale;
			verts[i] = value;
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00012C88 File Offset: 0x00010E88
	private void CenterVerts(List<Vector3> verts, float baseX, int lineStart, float scale)
	{
		for (int i = lineStart; i < verts.Count; i++)
		{
			Vector3 value = verts[i];
			value.x -= baseX / 200f * scale;
			verts[i] = value;
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00012CCC File Offset: 0x00010ECC
	private int CharToInt(int c)
	{
		if (c < 65)
		{
			return c - 48;
		}
		if (c < 97)
		{
			return 10 + (c - 65);
		}
		return 10 + (c - 97);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00012CFC File Offset: 0x00010EFC
	public static string WrapText(FontData data, string displayTxt, float maxWidth)
	{
		float num = 0f;
		int num2 = -1;
		int last = -1;
		bool flag = false;
		int num3 = 0;
		int num4 = 0;
		while (num4 < displayTxt.Length && num3++ <= 1000)
		{
			int num5 = (int)displayTxt[num4];
			if (num5 == 91)
			{
				flag = true;
				goto IL_49;
			}
			if (num5 != 93)
			{
				goto IL_49;
			}
			flag = false;
			IL_155:
			num4++;
			continue;
			IL_49:
			if (flag || num5 == 13)
			{
				goto IL_155;
			}
			if (num5 == 10)
			{
				num2 = -1;
				last = -1;
				num = 0f;
				goto IL_155;
			}
			if (num5 == 9)
			{
				num = Mathf.Ceil(num / 100f / 0.5f) * 0.5f * 100f;
				goto IL_155;
			}
			int index;
			if (!data.charMap.TryGetValue(num5, out index))
			{
				Debug.Log("Missing char :" + num5.ToString());
				num5 = -1;
				index = data.charMap[-1];
			}
			if (num5 == 32)
			{
				num2 = num4;
			}
			Vector3 vector = data.offsets[index];
			num += vector.z + data.GetKerning(last, num5);
			if (num > maxWidth * 100f)
			{
				if (num2 != -1)
				{
					displayTxt = displayTxt.Substring(0, num2) + "\n" + displayTxt.Substring(num2 + 1);
					num4 = num2;
				}
				else
				{
					displayTxt = displayTxt.Substring(0, num4) + "\n" + displayTxt.Substring(num4);
				}
				num2 = -1;
				num = 0f;
			}
			last = num5;
			goto IL_155;
		}
		return displayTxt;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00012E74 File Offset: 0x00011074
	public bool GetWordPosition(string str, out Vector3 bottomLeft, out Vector3 topRight)
	{
		int num = this.Text.IndexOf(str);
		if (num != -1)
		{
			if (!this.trackCharacterPosition)
			{
				this.trackCharacterPosition = true;
				this.RefreshMesh();
			}
			float z = base.transform.localPosition.z;
			Vector4 vector = this.characterPosData[num];
			Vector4 vector2 = this.characterPosData[num + str.Length - 1];
			bottomLeft = new Vector3(vector.x, vector.y, z);
			topRight = new Vector3(vector2.x + vector2.z, vector2.y + vector2.w, z);
			return true;
		}
		bottomLeft = Vector3.zero;
		topRight = Vector3.zero;
		return false;
	}

	// Token: 0x04000342 RID: 834
	public TextAsset FontData;

	// Token: 0x04000343 RID: 835
	public float scale = 1f;

	// Token: 0x04000344 RID: 836
	public float TabWidth = 0.5f;

	// Token: 0x04000345 RID: 837
	public bool Centered;

	// Token: 0x04000346 RID: 838
	public bool RightAligned;

	// Token: 0x04000347 RID: 839
	public TextLink textLinkPrefab;

	// Token: 0x04000348 RID: 840
	[HideInInspector]
	private Mesh mesh;

	// Token: 0x04000349 RID: 841
	[HideInInspector]
	public MeshRenderer render;

	// Token: 0x0400034A RID: 842
	[Multiline]
	public string Text;

	// Token: 0x0400034B RID: 843
	private string lastText;

	// Token: 0x0400034C RID: 844
	public Color Color = Color.white;

	// Token: 0x0400034D RID: 845
	private Color lastColor = Color.white;

	// Token: 0x0400034E RID: 846
	public Color OutlineColor = Color.black;

	// Token: 0x0400034F RID: 847
	private Color lastOutlineColor = Color.white;

	// Token: 0x04000350 RID: 848
	public float maxWidth = -1f;

	// Token: 0x04000351 RID: 849
	public float minLineHeight = -1f;

	// Token: 0x04000352 RID: 850
	public bool scaleToFit;

	// Token: 0x04000355 RID: 853
	public bool paragraphSpacing;

	// Token: 0x04000356 RID: 854
	public bool trackCharacterPosition;

	// Token: 0x04000357 RID: 855
	[HideInInspector]
	public Vector4[] characterPosData;

	// Token: 0x04000358 RID: 856
	private Vector2 cursorLocation;
}
