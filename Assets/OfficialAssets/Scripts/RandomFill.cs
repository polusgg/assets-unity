using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x0200005A RID: 90
public class RandomFill<T>
{
	// Token: 0x06000268 RID: 616 RVA: 0x00010521 File Offset: 0x0000E721
	public RandomFill()
	{
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00010529 File Offset: 0x0000E729
	public RandomFill(IEnumerable<T> set)
	{
		this.Set(set);
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00010538 File Offset: 0x0000E738
	public void Set(IEnumerable<T> values)
	{
		if (this.values == null)
		{
			this.values = values.ToArray<T>();
			this.values.Shuffle(0);
			this.idx = this.values.Length - 1;
		}
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0001056C File Offset: 0x0000E76C
	public T Get()
	{
		if (this.idx < 0)
		{
			this.values.Shuffle(0);
			this.idx = this.values.Length - 1;
		}
		T[] array = this.values;
		int num = this.idx;
		this.idx = num - 1;
		return array[num];
	}

	// Token: 0x040002FF RID: 767
	private T[] values;

	// Token: 0x04000300 RID: 768
	private int idx;
}
