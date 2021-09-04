using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BC RID: 444
public class RegionTextMonitor : MonoBehaviour
{
	// Token: 0x06000A49 RID: 2633 RVA: 0x000426A2 File Offset: 0x000408A2
	private void Start()
	{
		base.StartCoroutine(this.GetRegionText());
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x000426B1 File Offset: 0x000408B1
	private IEnumerator GetRegionText()
	{
		while (DestroyableSingleton<ServerManager>.Instance.CurrentRegion == null)
		{
			yield return null;
		}
		base.GetComponent<TextRenderer>().Text = DestroyableSingleton<TranslationController>.Instance.GetStringWithDefault(DestroyableSingleton<ServerManager>.Instance.CurrentRegion.TranslateName, DestroyableSingleton<ServerManager>.Instance.CurrentRegion.Name, Array.Empty<object>());
		yield break;
	}
}
