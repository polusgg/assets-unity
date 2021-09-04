using System;

// Token: 0x02000052 RID: 82
public class SubStringReader
{
	// Token: 0x06000248 RID: 584 RVA: 0x0000F84F File Offset: 0x0000DA4F
	public SubStringReader(string source)
	{
		this.Source = source;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000F860 File Offset: 0x0000DA60
	public SubString ReadLine()
	{
		int position = this.Position;
		if (position >= this.Source.Length)
		{
			return default(SubString);
		}
		int num = this.Position;
		int i = position;
		while (i < this.Source.Length)
		{
			char c = this.Source[i];
			if (c == '\r')
			{
				num = i - 1;
				this.Position = i + 1;
				if (i + 1 < this.Source.Length && this.Source[i + 1] == '\n')
				{
					this.Position = i + 2;
					break;
				}
				break;
			}
			else
			{
				if (c == '\n')
				{
					num = i - 1;
					this.Position = i + 1;
					break;
				}
				this.Position++;
				i++;
			}
		}
		return new SubString(this.Source, position, num - position);
	}

	// Token: 0x040002DC RID: 732
	private readonly string Source;

	// Token: 0x040002DD RID: 733
	private int Position;
}
