using System;

// Token: 0x02000075 RID: 117
public class XXHash
{
	// Token: 0x060002E4 RID: 740 RVA: 0x000131DD File Offset: 0x000113DD
	public XXHash(int seed)
	{
		this.seed = (uint)seed;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x000131EC File Offset: 0x000113EC
	public uint GetHash(byte[] buf)
	{
		int i = 0;
		int num = buf.Length;
		uint num3;
		if (num >= 16)
		{
			int num2 = num - 16;
			uint value = this.seed + 2654435761U + 2246822519U;
			uint value2 = this.seed + 2246822519U;
			uint value3 = this.seed;
			uint value4 = this.seed - 2654435761U;
			do
			{
				value = XXHash.CalcSubHash(value, buf, i);
				i += 4;
				value2 = XXHash.CalcSubHash(value2, buf, i);
				i += 4;
				value3 = XXHash.CalcSubHash(value3, buf, i);
				i += 4;
				value4 = XXHash.CalcSubHash(value4, buf, i);
				i += 4;
			}
			while (i <= num2);
			num3 = XXHash.RotateLeft(value, 1) + XXHash.RotateLeft(value2, 7) + XXHash.RotateLeft(value3, 12) + XXHash.RotateLeft(value4, 18);
		}
		else
		{
			num3 = this.seed + 374761393U;
		}
		num3 += (uint)num;
		while (i <= num - 4)
		{
			num3 += BitConverter.ToUInt32(buf, i) * 3266489917U;
			num3 = XXHash.RotateLeft(num3, 17) * 668265263U;
			i += 4;
		}
		while (i < num)
		{
			num3 += (uint)buf[i] * 374761393U;
			num3 = XXHash.RotateLeft(num3, 11) * 2654435761U;
			i++;
		}
		num3 ^= num3 >> 15;
		num3 *= 2246822519U;
		num3 ^= num3 >> 13;
		num3 *= 3266489917U;
		return num3 ^ num3 >> 16;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00013334 File Offset: 0x00011534
	public uint GetHash(params uint[] buf)
	{
		int i = 0;
		int num = buf.Length;
		uint num3;
		if (num >= 4)
		{
			int num2 = num - 4;
			uint value = this.seed + 2654435761U + 2246822519U;
			uint value2 = this.seed + 2246822519U;
			uint value3 = this.seed;
			uint value4 = this.seed - 2654435761U;
			do
			{
				value = XXHash.CalcSubHash(value, buf[i]);
				i++;
				value2 = XXHash.CalcSubHash(value2, buf[i]);
				i++;
				value3 = XXHash.CalcSubHash(value3, buf[i]);
				i++;
				value4 = XXHash.CalcSubHash(value4, buf[i]);
				i++;
			}
			while (i <= num2);
			num3 = XXHash.RotateLeft(value, 1) + XXHash.RotateLeft(value2, 7) + XXHash.RotateLeft(value3, 12) + XXHash.RotateLeft(value4, 18);
		}
		else
		{
			num3 = this.seed + 374761393U;
		}
		num3 += (uint)(num * 4);
		while (i < num)
		{
			num3 += buf[i] * 3266489917U;
			num3 = XXHash.RotateLeft(num3, 17) * 668265263U;
			i++;
		}
		num3 ^= num3 >> 15;
		num3 *= 2246822519U;
		num3 ^= num3 >> 13;
		num3 *= 3266489917U;
		return num3 ^ num3 >> 16;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00013454 File Offset: 0x00011654
	public uint GetHash(params int[] buf)
	{
		int i = 0;
		int num = buf.Length;
		uint num3;
		if (num >= 4)
		{
			int num2 = num - 4;
			uint value = this.seed + 2654435761U + 2246822519U;
			uint value2 = this.seed + 2246822519U;
			uint value3 = this.seed;
			uint value4 = this.seed - 2654435761U;
			do
			{
				value = XXHash.CalcSubHash(value, (uint)buf[i]);
				i++;
				value2 = XXHash.CalcSubHash(value2, (uint)buf[i]);
				i++;
				value3 = XXHash.CalcSubHash(value3, (uint)buf[i]);
				i++;
				value4 = XXHash.CalcSubHash(value4, (uint)buf[i]);
				i++;
			}
			while (i <= num2);
			num3 = XXHash.RotateLeft(value, 1) + XXHash.RotateLeft(value2, 7) + XXHash.RotateLeft(value3, 12) + XXHash.RotateLeft(value4, 18);
		}
		else
		{
			num3 = this.seed + 374761393U;
		}
		num3 += (uint)(num * 4);
		while (i < num)
		{
			num3 += (uint)(buf[i] * -1028477379);
			num3 = XXHash.RotateLeft(num3, 17) * 668265263U;
			i++;
		}
		num3 ^= num3 >> 15;
		num3 *= 2246822519U;
		num3 ^= num3 >> 13;
		num3 *= 3266489917U;
		return num3 ^ num3 >> 16;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00013574 File Offset: 0x00011774
	public uint GetHash(int buf)
	{
		uint num = XXHash.RotateLeft(this.seed + 374761393U + 4U + (uint)(buf * -1028477379), 17) * 668265263U;
		uint num2 = (num ^ num >> 15) * 2246822519U;
		uint num3 = (num2 ^ num2 >> 13) * 3266489917U;
		return num3 ^ num3 >> 16;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x000135B4 File Offset: 0x000117B4
	private static uint CalcSubHash(uint value, byte[] buf, int index)
	{
		uint num = BitConverter.ToUInt32(buf, index);
		value += num * 2246822519U;
		value = XXHash.RotateLeft(value, 13);
		value *= 2654435761U;
		return value;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x000135E8 File Offset: 0x000117E8
	private static uint CalcSubHash(uint value, uint read_value)
	{
		value += read_value * 2246822519U;
		value = XXHash.RotateLeft(value, 13);
		value *= 2654435761U;
		return value;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00013609 File Offset: 0x00011809
	private static uint RotateLeft(uint value, int count)
	{
		return value << count | value >> 32 - count;
	}

	// Token: 0x0400035E RID: 862
	private uint seed;

	// Token: 0x0400035F RID: 863
	private const uint PRIME32_1 = 2654435761U;

	// Token: 0x04000360 RID: 864
	private const uint PRIME32_2 = 2246822519U;

	// Token: 0x04000361 RID: 865
	private const uint PRIME32_3 = 3266489917U;

	// Token: 0x04000362 RID: 866
	private const uint PRIME32_4 = 668265263U;

	// Token: 0x04000363 RID: 867
	private const uint PRIME32_5 = 374761393U;
}
