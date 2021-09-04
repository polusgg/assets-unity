using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Token: 0x02000060 RID: 96
[Serializable]
public class FloatRange
{
	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000283 RID: 643 RVA: 0x00010993 File Offset: 0x0000EB93
	// (set) Token: 0x06000284 RID: 644 RVA: 0x0001099B File Offset: 0x0000EB9B
	public float Last { get; private set; }

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000285 RID: 645 RVA: 0x000109A4 File Offset: 0x0000EBA4
	public float Width
	{
		get
		{
			return this.max - this.min;
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x000109B3 File Offset: 0x0000EBB3
	public FloatRange(float min, float max)
	{
		this.min = min;
		this.max = max;
	}

	// Token: 0x06000287 RID: 647 RVA: 0x000109C9 File Offset: 0x0000EBC9
	public float ChangeRange(float y, float min, float max)
	{
		return Mathf.Lerp(min, max, (y - this.min) / this.Width);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x000109E1 File Offset: 0x0000EBE1
	public float Clamp(float value)
	{
		return Mathf.Clamp(value, this.min, this.max);
	}

	// Token: 0x06000289 RID: 649 RVA: 0x000109F5 File Offset: 0x0000EBF5
	public bool Contains(float t)
	{
		return this.min <= t && this.max >= t;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00010A10 File Offset: 0x0000EC10
	public float CubicLerp(float v)
	{
		if (this.min >= this.max)
		{
			return this.min;
		}
		v = Mathf.Clamp(0f, 1f, v);
		return v * v * v * (this.max - this.min) + this.min;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00010A5E File Offset: 0x0000EC5E
	public float EitherOr()
	{
		if (Random.value <= 0.5f)
		{
			return this.max;
		}
		return this.min;
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00010A79 File Offset: 0x0000EC79
	public float LerpUnclamped(float v)
	{
		return Mathf.LerpUnclamped(this.min, this.max, v);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00010A8D File Offset: 0x0000EC8D
	public float Lerp(float v)
	{
		return Mathf.Lerp(this.min, this.max, v);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00010AA1 File Offset: 0x0000ECA1
	public float ExpOutLerp(float v)
	{
		return this.Lerp(1f - Mathf.Pow(2f, -10f * v));
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00010AC0 File Offset: 0x0000ECC0
	public static float ExpOutLerp(float v, float min, float max)
	{
		return Mathf.Lerp(min, max, 1f - Mathf.Pow(2f, -10f * v));
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00010AE0 File Offset: 0x0000ECE0
	public static float Next(float min, float max)
	{
		return Random.Range(min, max);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00010AEC File Offset: 0x0000ECEC
	public float Next()
	{
		return this.Last = Random.Range(this.min, this.max);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00010B14 File Offset: 0x0000ED14
	public float NextMinDistance(float center, float minDistance)
	{
		float num = Mathf.Abs(this.min - center);
		float num2 = Mathf.Abs(this.max - center);
		bool flag = num > minDistance;
		bool flag2 = num2 > minDistance;
		bool flag3;
		if (flag2 && flag2)
		{
			flag3 = BoolRange.Next(0.5f);
		}
		else if (!flag && !flag2)
		{
			flag3 = (num > num2);
			minDistance = num * 0.9f;
		}
		else
		{
			flag3 = flag;
		}
		if (flag3)
		{
			return this.Last = Random.Range(this.min, center - minDistance);
		}
		return this.Last = Random.Range(center + minDistance, this.max);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00010BAE File Offset: 0x0000EDAE
	public IEnumerable<float> Range(int numStops)
	{
		float num;
		for (float i = 0f; i <= (float)numStops; i = num)
		{
			yield return Mathf.Lerp(this.min, this.max, i / (float)numStops);
			num = i + 1f;
		}
		yield break;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00010BC5 File Offset: 0x0000EDC5
	public IEnumerable<float> RandomRange(int numStops)
	{
		float num;
		for (float i = 0f; i <= (float)numStops; i = num)
		{
			yield return this.Next();
			num = i + 1f;
		}
		yield break;
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00010BDC File Offset: 0x0000EDDC
	internal float ReverseLerp(float t)
	{
		return Mathf.Clamp((t - this.min) / this.Width, 0f, 1f);
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00010BFC File Offset: 0x0000EDFC
	public static float ReverseLerp(float t, float min, float max)
	{
		float num = max - min;
		return Mathf.Clamp((t - min) / num, 0f, 1f);
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00010C21 File Offset: 0x0000EE21
	public IEnumerable<float> SpreadToEdges(int stops)
	{
		return FloatRange.SpreadToEdges(this.min, this.max, stops);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00010C35 File Offset: 0x0000EE35
	public IEnumerable<float> SpreadEvenly(int stops)
	{
		return FloatRange.SpreadEvenly(this.min, this.max, stops);
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00010C49 File Offset: 0x0000EE49
	public static IEnumerable<float> SpreadToEdges(float min, float max, int stops)
	{
		if (stops == 1)
		{
			yield break;
		}
		int num;
		for (int i = 0; i < stops; i = num)
		{
			yield return Mathf.Lerp(min, max, (float)i / ((float)stops - 1f));
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00010C67 File Offset: 0x0000EE67
	public static IEnumerable<float> SpreadEvenly(float min, float max, int stops)
	{
		float step = 1f / ((float)stops + 1f);
		for (float i = step; i < 1f; i += step)
		{
			yield return Mathf.Lerp(min, max, i);
		}
		yield break;
	}

	// Token: 0x04000309 RID: 777
	public float min;

	// Token: 0x0400030A RID: 778
	public float max;
}
