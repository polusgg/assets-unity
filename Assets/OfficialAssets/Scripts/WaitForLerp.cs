using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class WaitForLerp : IEnumerator
{
	// Token: 0x060002E0 RID: 736 RVA: 0x00013166 File Offset: 0x00011366
	public WaitForLerp(float seconds, Action<float> act)
	{
		this.duration = seconds;
		this.act = act;
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001317C File Offset: 0x0001137C
	public object Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00013180 File Offset: 0x00011380
	public bool MoveNext()
	{
		this.timer = Mathf.Min(this.timer + Time.deltaTime, this.duration);
		this.act(this.timer / this.duration);
		return this.timer < this.duration;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x000131D0 File Offset: 0x000113D0
	public void Reset()
	{
		this.timer = 0f;
	}

	// Token: 0x0400035B RID: 859
	private float duration;

	// Token: 0x0400035C RID: 860
	private float timer;

	// Token: 0x0400035D RID: 861
	private Action<float> act;
}
