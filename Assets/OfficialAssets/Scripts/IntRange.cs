using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Token: 0x0200005F RID: 95
[Serializable]
public class IntRange
{
	// Token: 0x06000279 RID: 633 RVA: 0x000108CA File Offset: 0x0000EACA
	public IntRange()
	{
	}

	// Token: 0x0600027A RID: 634 RVA: 0x000108D2 File Offset: 0x0000EAD2
	public IntRange(int min, int max)
	{
		this.min = min;
		this.max = max;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x000108E8 File Offset: 0x0000EAE8
	public int Next()
	{
		return Random.Range(this.min, this.max);
	}

	// Token: 0x0600027C RID: 636 RVA: 0x000108FB File Offset: 0x0000EAFB
	public bool Contains(int value)
	{
		return this.min <= value && this.max >= value;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00010914 File Offset: 0x0000EB14
	public static int Next(int max)
	{
		return (int)(Random.value * (float)max);
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0001091F File Offset: 0x0000EB1F
	internal static int Next(int min, int max)
	{
		return Random.Range(min, max);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00010928 File Offset: 0x0000EB28
	internal static byte NextByte(byte min, byte max)
	{
		return (byte)Random.Range((int)min, (int)max);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00010934 File Offset: 0x0000EB34
	public static void FillRandom(sbyte min, sbyte max, sbyte[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (sbyte)IntRange.Next((int)min, (int)max);
		}
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0001095A File Offset: 0x0000EB5A
	public static int RandomSign()
	{
		if (!BoolRange.Next(0.5f))
		{
			return -1;
		}
		return 1;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0001096C File Offset: 0x0000EB6C
	public static void FillRandomRange(sbyte[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (sbyte)i;
		}
		array.Shuffle(0);
	}

	// Token: 0x04000307 RID: 775
	public int min;

	// Token: 0x04000308 RID: 776
	public int max;
}
