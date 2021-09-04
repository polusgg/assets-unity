using System;

// Token: 0x020001B2 RID: 434
public class RingBuffer<T>
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060009FC RID: 2556 RVA: 0x00040F34 File Offset: 0x0003F134
	// (set) Token: 0x060009FD RID: 2557 RVA: 0x00040F3C File Offset: 0x0003F13C
	public int Count { get; private set; }

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060009FE RID: 2558 RVA: 0x00040F45 File Offset: 0x0003F145
	public int Capacity
	{
		get
		{
			return this.Data.Length;
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00040F4F File Offset: 0x0003F14F
	public RingBuffer(int size)
	{
		this.Data = new T[size];
	}

	// Token: 0x170000A8 RID: 168
	public T this[int i]
	{
		get
		{
			int num = (this.startIdx + i) % this.Data.Length;
			return this.Data[num];
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00040F8F File Offset: 0x0003F18F
	public T First()
	{
		return this.Data[this.startIdx];
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00040FA4 File Offset: 0x0003F1A4
	public T Last()
	{
		int num = (this.startIdx + this.Count - 1) % this.Data.Length;
		return this.Data[num];
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00040FD8 File Offset: 0x0003F1D8
	public void Add(T item)
	{
		int num = (this.startIdx + this.Count) % this.Data.Length;
		this.Data[num] = item;
		if (this.Count < this.Data.Length)
		{
			int count = this.Count;
			this.Count = count + 1;
			return;
		}
		this.startIdx = (this.startIdx + 1) % this.Data.Length;
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00041044 File Offset: 0x0003F244
	public T RemoveFirst()
	{
		if (this.Count == 0)
		{
			throw new InvalidOperationException("Data is empty");
		}
		T result = this.Data[this.startIdx];
		this.startIdx = (this.startIdx + 1) % this.Data.Length;
		int count = this.Count;
		this.Count = count - 1;
		return result;
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0004109C File Offset: 0x0003F29C
	public void Clear()
	{
		this.startIdx = 0;
		this.Count = 0;
	}

	// Token: 0x04000B5A RID: 2906
	private readonly T[] Data;

	// Token: 0x04000B5B RID: 2907
	private int startIdx;
}
