using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class ConditionalHide : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x0000DA4C File Offset: 0x0000BC4C
	private void Awake()
	{
		//for (int i = 0; i < this.HideForPlatforms.Length; i++)
		//{
		//	if (this.HideForPlatforms[i] == 2)
		//	{
		//		base.gameObject.SetActive(false);
		//	}
		//}
		//for (int j = 0; j < this.OnlyShowForPlatforms.Length; j++)
		//{
		//	int num = this.OnlyShowForPlatforms[j];
		//	bool flag = false;
		//	if (num == 2)
		//	{
		//		flag = true;
		//	}
		//	if (flag)
		//	{
		//		base.gameObject.SetActive(true);
		//		return;
		//	}
		//	base.gameObject.SetActive(false);
		//}
	}

	// Token: 0x0400029D RID: 669
	public RuntimePlatform[] HideForPlatforms = new RuntimePlatform[]
	{
        (RuntimePlatform)2
	};

	// Token: 0x0400029E RID: 670
	public RuntimePlatform[] OnlyShowForPlatforms = new RuntimePlatform[0];
}
