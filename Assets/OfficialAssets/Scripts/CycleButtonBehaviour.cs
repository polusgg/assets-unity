using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class CycleButtonBehaviour : MonoBehaviour, ITranslatedText
{
	// Token: 0x0600047E RID: 1150 RVA: 0x0001D1FA File Offset: 0x0001B3FA
	public void Start()
	{
		DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Add(this);
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0001D20C File Offset: 0x0001B40C
	public void OnDestroy()
	{
		if (DestroyableSingleton<TranslationController>.InstanceExists)
		{
			DestroyableSingleton<TranslationController>.Instance.ActiveTexts.Remove(this);
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0001D226 File Offset: 0x0001B426
	public void ResetText()
	{
		this.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.options[this.curSelection], Array.Empty<object>());
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0001D24F File Offset: 0x0001B44F
	public void UpdateText(int selection)
	{
		this.curSelection = selection;
		this.ResetText();
	}

	// Token: 0x04000548 RID: 1352
	public StringNames[] options;

	// Token: 0x04000549 RID: 1353
	public StringNames BaseText;

	// Token: 0x0400054A RID: 1354
	public TextRenderer Text;

	// Token: 0x0400054B RID: 1355
	public SpriteRenderer Background;

	// Token: 0x0400054C RID: 1356
	public ButtonRolloverHandler Rollover;

	// Token: 0x0400054D RID: 1357
	private int curSelection;
}
