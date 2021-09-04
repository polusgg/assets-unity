using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class PingTracker : MonoBehaviour
{
	// Token: 0x060007AB RID: 1963 RVA: 0x0003128C File Offset: 0x0002F48C
	private void Update()
	{
		if (AmongUsClient.Instance)
		{
			if (AmongUsClient.Instance.GameMode == GameModes.FreePlay)
			{
				base.gameObject.SetActive(false);
			}
			this.text.Text = string.Format("Ping: {0} ms", AmongUsClient.Instance.Ping);
		}
	}

	// Token: 0x040008B7 RID: 2231
	public TextRenderer text;
}
