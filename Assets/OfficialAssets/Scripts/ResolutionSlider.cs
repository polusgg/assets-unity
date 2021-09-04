using System;
using System.Linq;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class ResolutionSlider : MonoBehaviour
{
	// Token: 0x06000B15 RID: 2837 RVA: 0x000460A0 File Offset: 0x000442A0
	public void OnEnable()
	{
		this.allResolutions = (from r in Screen.resolutions
		where r.height > 480
		select r).ToArray<Resolution>();
		this.targetResolution = Screen.currentResolution;
		this.targetFullscreen = Screen.fullScreen;
		this.targetIdx = this.allResolutions.IndexOf((Resolution e) => e.width == this.targetResolution.width && e.height == this.targetResolution.height);
		this.slider.Value = (float)this.targetIdx / ((float)this.allResolutions.Length - 1f);
		this.Display.Text = string.Format("{0}x{1}", this.targetResolution.width, this.targetResolution.height);
		this.Fullscreen.UpdateText(this.targetFullscreen);
		this.VSync.UpdateText(SaveManager.VSync);
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0004618C File Offset: 0x0004438C
	public void ToggleVSync()
	{
		SaveManager.VSync = !SaveManager.VSync;
		if (SaveManager.VSync)
		{
			QualitySettings.vSyncCount = 1;
		}
		else
		{
			QualitySettings.vSyncCount = 0;
		}
		this.VSync.UpdateText(SaveManager.VSync);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x000461C0 File Offset: 0x000443C0
	public void ToggleFullscreen()
	{
		this.targetFullscreen = !this.targetFullscreen;
		this.Fullscreen.UpdateText(this.targetFullscreen);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x000461E4 File Offset: 0x000443E4
	public void OnResChange()
	{
		int num = Mathf.RoundToInt((float)(this.allResolutions.Length - 1) * this.slider.Value);
		if (num != this.targetIdx)
		{
			this.targetIdx = num;
			this.targetResolution = this.allResolutions[num];
			this.Display.Text = string.Format("{0}x{1}", this.targetResolution.width, this.targetResolution.height);
		}
		this.slider.Value = (float)this.targetIdx / ((float)this.allResolutions.Length - 1f);
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00046287 File Offset: 0x00044487
	public void SaveChange()
	{
		ResolutionManager.SetResolution(this.targetResolution.width, this.targetResolution.height, this.targetFullscreen);
	}

	// Token: 0x04000C79 RID: 3193
	private int targetIdx;

	// Token: 0x04000C7A RID: 3194
	private Resolution targetResolution;

	// Token: 0x04000C7B RID: 3195
	private bool targetFullscreen;

	// Token: 0x04000C7C RID: 3196
	private Resolution[] allResolutions;

	// Token: 0x04000C7D RID: 3197
	public SlideBar slider;

	// Token: 0x04000C7E RID: 3198
	public ToggleButtonBehaviour Fullscreen;

	// Token: 0x04000C7F RID: 3199
	public ToggleButtonBehaviour VSync;

	// Token: 0x04000C80 RID: 3200
	public TextRenderer Display;
}
