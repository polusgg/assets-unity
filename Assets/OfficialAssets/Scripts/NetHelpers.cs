using System;

// Token: 0x020000F6 RID: 246
public static class NetHelpers
{
	// Token: 0x0600061D RID: 1565 RVA: 0x00027B6C File Offset: 0x00025D6C
	public static bool SidGreaterThan(ushort newSid, ushort prevSid)
	{
		//ushort num = prevSid + 32767;
		//if (prevSid < num)
		//{
		//	return newSid > prevSid && newSid <= num;
		//}
		//return newSid > prevSid || newSid <= num;
		return false;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x00027BA4 File Offset: 0x00025DA4
	public static bool SidGreaterThan(byte newSid, byte prevSid)
	{
		//byte b = prevSid + 127;
		//if (prevSid < b)
		//{
		//	return newSid > prevSid && newSid <= b;
		//}
		//return newSid > prevSid || newSid <= b;

		return false;
	}
}
