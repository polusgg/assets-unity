using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class TwitterLink : MonoBehaviour
{
	// Token: 0x060002DC RID: 732 RVA: 0x000130EF File Offset: 0x000112EF
	public void Click()
	{
		Application.OpenURL(this.LinkUrl);
	}

	// Token: 0x04000359 RID: 857
	public string LinkUrl = "https://www.twitter.com/InnerslothDevs";
}
