using System;

// Token: 0x02000050 RID: 80
public static class HashRandom
{
	// Token: 0x0600023E RID: 574 RVA: 0x0000F7C7 File Offset: 0x0000D9C7
	public static uint Next()
	{
		return HashRandom.src.GetHash(HashRandom.cnt++);
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
	public static int FastNext(int maxInt)
	{
		return (int)((ulong)HashRandom.Next() % (ulong)((long)maxInt));
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000F7EC File Offset: 0x0000D9EC
	public static int Next(int maxInt)
	{
		uint num = (uint)(-1 / maxInt);
		uint num2 = num * (uint)maxInt;
		uint num3;
		do
		{
			num3 = HashRandom.Next();
		}
		while (num3 > num2);
		return (int)(num3 / num);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000F810 File Offset: 0x0000DA10
	public static int Next(int minInt, int maxInt)
	{
		return HashRandom.Next(maxInt - minInt) + minInt;
	}

	// Token: 0x040002DA RID: 730
	private static XXHash src = new XXHash((int)DateTime.UtcNow.Ticks);

	// Token: 0x040002DB RID: 731
	private static int cnt = 0;
}
