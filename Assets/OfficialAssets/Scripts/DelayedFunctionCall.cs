using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000175 RID: 373
public class DelayedFunctionCall : MonoBehaviour
{
	// Token: 0x06000891 RID: 2193 RVA: 0x0003836D File Offset: 0x0003656D
	private void Update()
	{
		if (this.t >= this.delayDuration)
		{
			base.enabled = false;
			if (this.onTimerElapsed != null)
			{
				this.onTimerElapsed.Invoke();
			}
		}
		this.t += Time.deltaTime;
	}

	// Token: 0x040009F7 RID: 2551
	public UnityEvent onTimerElapsed;

	// Token: 0x040009F8 RID: 2552
	public float delayDuration = 1f;

	// Token: 0x040009F9 RID: 2553
	private float t;
}
