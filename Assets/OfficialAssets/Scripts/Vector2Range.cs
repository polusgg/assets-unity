using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Token: 0x02000061 RID: 97
[Serializable]
public struct Vector2Range
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x0600029B RID: 667 RVA: 0x00010C85 File Offset: 0x0000EE85
	public float Width
	{
		get
		{
			return this.max.x - this.min.x;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600029C RID: 668 RVA: 0x00010C9E File Offset: 0x0000EE9E
	public float Height
	{
		get
		{
			return this.max.y - this.min.y;
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x00010CB7 File Offset: 0x0000EEB7
	public Vector2Range(Vector2 min, Vector2 max)
	{
		this.min = min;
		this.max = max;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x00010CC8 File Offset: 0x0000EEC8
	public void LerpUnclamped(ref Vector3 output, float t, float z)
	{
		output.Set(Mathf.LerpUnclamped(this.min.x, this.max.x, t), Mathf.LerpUnclamped(this.min.y, this.max.y, t), z);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00010D14 File Offset: 0x0000EF14
	public void Lerp(ref Vector3 output, float t, float z)
	{
		output.Set(Mathf.Lerp(this.min.x, this.max.x, t), Mathf.Lerp(this.min.y, this.max.y, t), z);
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00010D60 File Offset: 0x0000EF60
	public Vector2 Next()
	{
		return new Vector2(Random.Range(this.min.x, this.max.x), Random.Range(this.min.y, this.max.y));
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00010DA0 File Offset: 0x0000EFA0
	public static Vector2 NextEdge()
	{
		float num = 6.2831855f * Random.value;
		float num2 = Mathf.Cos(num);
		float num3 = Mathf.Sin(num);
		return new Vector2(num2, num3);
	}

	// Token: 0x0400030C RID: 780
	public Vector2 min;

	// Token: 0x0400030D RID: 781
	public Vector2 max;
}
