using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class ToggleButtonBehaviour : MonoBehaviour, ITranslatedText
{
	// Token: 0x060005E7 RID: 1511 RVA: 0x00026B48 File Offset: 0x00024D48
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00026B5A File Offset: 0x00024D5A
	public void OnDestroy()
	{
		if (DestroyableSingleton<TranslationController>.InstanceExists)
		{
			DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Remove(this);
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00026B74 File Offset: 0x00024D74
	public void ResetText()
	{
		this.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.BaseText, Array.Empty<object>()) + ": " + DestroyableSingleton<TranslationController>.Instance.GetString(this.onState ? StringNames.SettingsOn : StringNames.SettingsOff, Array.Empty<object>());
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00026BC8 File Offset: 0x00024DC8
	public void UpdateText(bool on)
	{
		this.onState = on;
		Color color = on ? new Color(0f, 1f, 0.16470589f, 1f) : Color.white;
		this.Background.color = color;
		this.ResetText();
		if (this.Rollover)
		{
			this.Rollover.ChangeOutColor(color);
		}
	}

	// Token: 0x040006A0 RID: 1696
	public StringNames BaseText;

	// Token: 0x040006A1 RID: 1697
	public TextRenderer Text;

	// Token: 0x040006A2 RID: 1698
	public SpriteRenderer Background;

	// Token: 0x040006A3 RID: 1699
	public ButtonRolloverHandler Rollover;

	// Token: 0x040006A4 RID: 1700
	private bool onState;
}
