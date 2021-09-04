using System;
using System.IO;
using UnityEngine;

// Token: 0x020001BD RID: 445
public class ResSetter : MonoBehaviour
{
	// Token: 0x06000A4C RID: 2636 RVA: 0x000426C8 File Offset: 0x000408C8
	public void Start()
	{
		Screen.SetResolution(this.Width, this.Height, false);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000426DC File Offset: 0x000408DC
	public void Update()
	{
		//if (Input.GetKeyDown(115))
		//{
		//	Directory.CreateDirectory("C:\\AmongUsSS");
		//	string format = "C:\\AmongUsSS\\Screenshot-{0}.png";
		//	int num = this.cnt;
		//	this.cnt = num + 1;
		//	ScreenCapture.CaptureScreenshot(string.Format(format, num));
		//}
	}

	// Token: 0x04000BC8 RID: 3016
	public int Width = 1438;

	// Token: 0x04000BC9 RID: 3017
	public int Height = 810;

	// Token: 0x04000BCA RID: 3018
	private int cnt;
}
