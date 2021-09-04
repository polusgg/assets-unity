using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public static class ResolutionManager
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060002A2 RID: 674 RVA: 0x00010DCC File Offset: 0x0000EFCC
	// (remove) Token: 0x060002A3 RID: 675 RVA: 0x00010E00 File Offset: 0x0000F000
	public static event Action<float> ResolutionChanged;

	// Token: 0x060002A4 RID: 676 RVA: 0x00010E33 File Offset: 0x0000F033
	public static void SetResolution(int width, int height, bool fullscreen)
	{
		Action<float> resolutionChanged = ResolutionManager.ResolutionChanged;
		if (resolutionChanged != null)
		{
			resolutionChanged((float)width / (float)height);
		}
		Screen.SetResolution(width, height, fullscreen);
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x00010E54 File Offset: 0x0000F054
	public static void ToggleFullscreen()
	{
		bool flag = !Screen.fullScreen;
		int width;
		int height;
		if (flag)
		{
			Resolution[] resolutions = Screen.resolutions;
			Resolution resolution = resolutions[0];
			for (int i = 0; i < resolutions.Length; i++)
			{
				if (resolution.height < resolutions[i].height)
				{
					resolution = resolutions[i];
				}
			}
			width = resolution.width;
			height = resolution.height;
		}
		else
		{
			width = 711;
			height = 400;
		}
		ResolutionManager.SetResolution(width, height, flag);
	}
}
