using System;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class BuildDefineConditionalObject : MonoBehaviour
{
	// Token: 0x06000841 RID: 2113 RVA: 0x00035D7C File Offset: 0x00033F7C
	private void Awake()
	{
		base.gameObject.SetActive(this.isDefined);
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00035D8F File Offset: 0x00033F8F
	private void OnEnable()
	{
		if (!this.isDefined)
		{
			base.gameObject.SetActive(this.isDefined);
		}
	}

	// Token: 0x040009B8 RID: 2488
	public bool isDefined;
}
