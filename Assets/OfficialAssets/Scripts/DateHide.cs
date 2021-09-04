using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class DateHide : MonoBehaviour
{
	// Token: 0x06000314 RID: 788 RVA: 0x00014570 File Offset: 0x00012770
	private void Awake()
	{
		try
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = new DateTime(utcNow.Year, this.MonthStart, this.DayStart);
			DateTime t2 = new DateTime(utcNow.Year, this.MonthEnd, this.DayEnd);
			if (t <= utcNow && utcNow <= t2)
			{
				return;
			}
		}
		catch
		{
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400039E RID: 926
	public int MonthStart;

	// Token: 0x0400039F RID: 927
	public int DayStart;

	// Token: 0x040003A0 RID: 928
	public int MonthEnd;

	// Token: 0x040003A1 RID: 929
	public int DayEnd;
}
