using System;

// Token: 0x02000053 RID: 83
public struct SubString
{
	// Token: 0x0600024A RID: 586 RVA: 0x0000F92C File Offset: 0x0000DB2C
	public SubString(string source, int start, int length)
	{
		this.Source = source;
		this.Start = start;
		this.Length = length;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000F943 File Offset: 0x0000DB43
	public override string ToString()
	{
		return this.Source.Substring(this.Start, this.Length);
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000F95C File Offset: 0x0000DB5C
	public int GetKvpValue()
	{
		int num = this.Start + this.Length;
		for (int i = this.Start; i < num; i++)
		{
			if (this.Source[i] == '=')
			{
				i++;
				return new SubString(this.Source, i, num - i).ToInt();
			}
		}
		throw new InvalidCastException();
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000F9BC File Offset: 0x0000DBBC
	public int ToInt()
	{
		int num = 0;
		int num2 = this.Start + this.Length;
		bool flag = false;
		for (int i = this.Start; i < num2; i++)
		{
			char c = this.Source[i];
			if (c == '-')
			{
				flag = true;
			}
			else if (c >= '0' && c <= '9')
			{
				int num3 = (int)(c - '0');
				num = 10 * num + num3;
			}
		}
		if (!flag)
		{
			return num;
		}
		return -num;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000FA28 File Offset: 0x0000DC28
	public bool StartsWith(string v)
	{
		if (v.Length > this.Length)
		{
			return false;
		}
		for (int i = 0; i < v.Length; i++)
		{
			if (this.Source[i + this.Start] != v[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x040002DE RID: 734
	public readonly int Start;

	// Token: 0x040002DF RID: 735
	public readonly int Length;

	// Token: 0x040002E0 RID: 736
	public readonly string Source;
}
