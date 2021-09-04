using System;
using System.Collections;
using System.Text;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class FreeWeekendShower : MonoBehaviour
{
	// Token: 0x06000398 RID: 920 RVA: 0x00017F0A File Offset: 0x0001610A
	private void Start()
	{
		base.StartCoroutine(this.Check());
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00017F19 File Offset: 0x00016119
	private IEnumerator Check()
	{
		WaitForSeconds wait = new WaitForSeconds(1f);
		StringBuilder txt = new StringBuilder();
		for (;;)
		{
			txt.Length = 0;
			if (Constants.ShouldFlipSkeld())
			{
				txt.AppendLine("Happy April Fools! Enjoy ehT Dleks!");
			}
			if (Constants.ShouldFlipSkeld())
			{
				txt.AppendLine("Happy April Fools! Enjoy ehT Dleks!");
			}
			this.Output.Text = txt.ToString();
			yield return wait;
		}
	}

	// Token: 0x0400043E RID: 1086
	public TextRenderer Output;
}
