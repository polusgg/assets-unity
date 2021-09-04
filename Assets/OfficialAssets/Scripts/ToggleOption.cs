using System;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class ToggleOption : OptionBehaviour
{
	// Token: 0x06000D6A RID: 3434 RVA: 0x000517CC File Offset: 0x0004F9CC
	public void OnEnable()
	{
		this.TitleText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.Title, Array.Empty<object>());
		GameOptionsData gameOptions = PlayerControl.GameOptions;
		StringNames title = this.Title;
		if (title != StringNames.GameRecommendedSettings)
		{
			switch (title)
			{
			case StringNames.GameConfirmImpostor:
				this.CheckMark.enabled = gameOptions.ConfirmImpostor;
				return;
			case StringNames.GameVisualTasks:
				this.CheckMark.enabled = gameOptions.VisualTasks;
				return;
			case StringNames.GameAnonymousVotes:
				this.CheckMark.enabled = gameOptions.AnonymousVotes;
				return;
			}
			Debug.Log("Ono, unrecognized setting: " + this.Title.ToString());
			return;
		}
		this.CheckMark.enabled = gameOptions.isDefaults;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00051894 File Offset: 0x0004FA94
	private void FixedUpdate()
	{
		bool @bool = this.GetBool();
		if (this.oldValue != @bool)
		{
			this.oldValue = @bool;
			this.CheckMark.enabled = @bool;
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x000518C4 File Offset: 0x0004FAC4
	public void Toggle()
	{
		this.CheckMark.enabled = !this.CheckMark.enabled;
		this.OnValueChanged(this);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x000518EB File Offset: 0x0004FAEB
	public override bool GetBool()
	{
		return this.CheckMark.enabled;
	}

	// Token: 0x04000EF4 RID: 3828
	public TextRenderer TitleText;

	// Token: 0x04000EF5 RID: 3829
	public SpriteRenderer CheckMark;

	// Token: 0x04000EF6 RID: 3830
	private bool oldValue;
}
