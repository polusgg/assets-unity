using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class VersionShower : MonoBehaviour
{
	// Token: 0x060002DE RID: 734 RVA: 0x00013110 File Offset: 0x00011310
	public void Start()
	{
		string str = "v" + Application.version;
		str += "i";
		if (!DetectTamper.Detect())
		{
			str += "h";
		}
		this.text.Text = str;
		Screen.sleepTimeout = -1;
	}

	// Token: 0x0400035A RID: 858
	public TextRenderer text;
}
