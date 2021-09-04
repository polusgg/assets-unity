using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020001D3 RID: 467
public class OptionsMenuBehaviour : MonoBehaviour, ITranslatedText
{
	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00045CD8 File Offset: 0x00043ED8
	public bool IsOpen
	{
		get
		{
			return base.isActiveAndEnabled;
		}
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00045CE0 File Offset: 0x00043EE0
	public void OpenTabGroup(int index)
	{
		for (int i = 0; i < this.Tabs.Length; i++)
		{
			TabGroup tabGroup = this.Tabs[i];
			if (i == index)
			{
				tabGroup.Open();
			}
			else
			{
				tabGroup.Close();
			}
		}
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00045D1B File Offset: 0x00043F1B
	private void Update()
	{
		//if (Input.GetKeyUp(27))
		//{
		//	this.Close();
		//}
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00045D2C File Offset: 0x00043F2C
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
		this.ControllerSelectable.Clear();
		foreach (UiElement item in base.GetComponentsInChildren<UiElement>(true))
		{
			if (!this.IgnoreControllerSelection.Contains(item))
			{
				this.ControllerSelectable.Add(item);
			}
		}
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00045D88 File Offset: 0x00043F88
	public void OnDestroy()
	{
		if (DestroyableSingleton<TranslationController>.InstanceExists)
		{
			DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Remove(this);
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00045DA4 File Offset: 0x00043FA4
	public void ResetText()
	{
		this.JoystickButton.transform.parent.GetComponentInChildren<TextRenderer>().Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SettingsMouseMode, Array.Empty<object>());
		this.TouchButton.transform.parent.GetComponentInChildren<TextRenderer>().Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SettingsKeyboardMode, Array.Empty<object>());
		this.JoystickSizeSlider.gameObject.SetActive(false);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00045E18 File Offset: 0x00044018
	public void Open()
	{
		this.ResetText();
		if (base.gameObject.activeSelf)
		{
			if (this.Toggle)
			{
				base.GetComponent<TransitionOpen>().Close();
			}
			return;
		}
		this.OpenTabGroup(0);
		this.UpdateButtons();
		base.gameObject.SetActive(true);
		if (DestroyableSingleton<HudManager>.InstanceExists)
		{
			ConsoleJoystick.SetMode_MenuAdditive();
		}
		ControllerManager.Instance.OpenOverlayMenu("OptionsMenu", this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, true);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00045E93 File Offset: 0x00044093
	public void SetControlType(int i)
	{
		SaveManager.ControlMode = i;
		this.UpdateButtons();
		if (DestroyableSingleton<HudManager>.InstanceExists)
		{
			DestroyableSingleton<HudManager>.Instance.SetTouchType(i);
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00045EB3 File Offset: 0x000440B3
	public void UpdateJoystickSize()
	{
		SaveManager.JoystickSize = this.JoystickSizes.Lerp(this.JoystickSizeSlider.Value);
		if (DestroyableSingleton<HudManager>.InstanceExists)
		{
			DestroyableSingleton<HudManager>.Instance.SetJoystickSize(SaveManager.JoystickSize);
		}
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00045EE6 File Offset: 0x000440E6
	public void UpdateSfxVolume()
	{
		SaveManager.SfxVolume = this.SoundSlider.Value;
		SoundManager.Instance.ChangeSfxVolume(this.SoundSlider.Value);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00045F0D File Offset: 0x0004410D
	public void UpdateMusicVolume()
	{
		SaveManager.MusicVolume = this.MusicSlider.Value;
		SoundManager.Instance.ChangeMusicVolume(this.MusicSlider.Value);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00045F34 File Offset: 0x00044134
	public void OpenPrivacyPolicy()
	{
		Application.OpenURL("https://innersloth.com/privacy.php");
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00045F40 File Offset: 0x00044140
	public void TogglePersonalizedAd()
	{
		this.Close();
		Object.FindObjectOfType<MainMenuManager>().AdsPolicy.ForceShow();
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00045F57 File Offset: 0x00044157
	public void ToggleCensorChat()
	{
		SaveManager.CensorChat = !SaveManager.CensorChat;
		this.UpdateButtons();
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00045F6C File Offset: 0x0004416C
	public void UpdateButtons()
	{
		if (SaveManager.ControlMode == 0)
		{
			this.JoystickButton.color = new Color32(0, byte.MaxValue, 42, byte.MaxValue);
			this.TouchButton.color = Color.white;
			this.JoystickSizeSlider.enabled = true;
			this.JoystickSizeSlider.OnEnable();
		}
		else
		{
			this.JoystickButton.color = Color.white;
			this.TouchButton.color = new Color32(0, byte.MaxValue, 42, byte.MaxValue);
			this.JoystickSizeSlider.enabled = false;
			this.JoystickSizeSlider.OnDisable();
		}
		this.JoystickSizeSlider.Value = this.JoystickSizes.ReverseLerp(SaveManager.JoystickSize);
		this.SoundSlider.Value = SaveManager.SfxVolume;
		this.MusicSlider.Value = SaveManager.MusicVolume;
		this.CensorChatButton.UpdateText(SaveManager.CensorChat);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0004605F File Offset: 0x0004425F
	public void Close()
	{
		base.gameObject.SetActive(false);
		ControllerManager.Instance.CloseOverlayMenu("OptionsMenu");
	}

	// Token: 0x04000C67 RID: 3175
	public SpriteRenderer Background;

	// Token: 0x04000C68 RID: 3176
	public SpriteRenderer JoystickButton;

	// Token: 0x04000C69 RID: 3177
	public SpriteRenderer TouchButton;

	// Token: 0x04000C6A RID: 3178
	public SlideBar JoystickSizeSlider;

	// Token: 0x04000C6B RID: 3179
	public FloatRange JoystickSizes = new FloatRange(0.5f, 1.5f);

	// Token: 0x04000C6C RID: 3180
	public SlideBar SoundSlider;

	// Token: 0x04000C6D RID: 3181
	public SlideBar MusicSlider;

	// Token: 0x04000C6E RID: 3182
	public ToggleButtonBehaviour CensorChatButton;

	// Token: 0x04000C6F RID: 3183
	public bool Toggle = true;

	// Token: 0x04000C70 RID: 3184
	public TabGroup[] Tabs;

	// Token: 0x04000C71 RID: 3185
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000C72 RID: 3186
	public UiElement DefaultButtonSelected;

	// Token: 0x04000C73 RID: 3187
	public List<UiElement> ControllerSelectable;

	// Token: 0x04000C74 RID: 3188
	public List<UiElement> IgnoreControllerSelection;

	// Token: 0x04000C75 RID: 3189
	private bool saveMusic;

	// Token: 0x04000C76 RID: 3190
	private bool saveSFX;

	// Token: 0x04000C77 RID: 3191
	private float musicVolume;

	// Token: 0x04000C78 RID: 3192
	private float sfxVolume;
}
