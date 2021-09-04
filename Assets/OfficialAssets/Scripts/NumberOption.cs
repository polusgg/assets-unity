using System;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class NumberOption : OptionBehaviour
{
	// Token: 0x06000AF8 RID: 2808 RVA: 0x000459A0 File Offset: 0x00043BA0
	public void OnEnable()
	{
		this.TitleText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(this.Title, Array.Empty<object>());
		this.FixedUpdate();
		GameOptionsData gameOptions = PlayerControl.GameOptions;
		StringNames title = this.Title;
		switch (title)
		{
		case StringNames.GameNumImpostors:
			this.Value = (float)gameOptions.NumImpostors;
			return;
		case StringNames.GameNumMeetings:
			this.Value = (float)gameOptions.NumEmergencyMeetings;
			return;
		case StringNames.GameDiscussTime:
			this.Value = (float)gameOptions.DiscussionTime;
			return;
		case StringNames.GameVotingTime:
			this.Value = (float)gameOptions.VotingTime;
			return;
		case StringNames.GamePlayerSpeed:
			this.Value = gameOptions.PlayerSpeedMod;
			return;
		case StringNames.GameCrewLight:
			this.Value = gameOptions.CrewLightMod;
			return;
		case StringNames.GameImpostorLight:
			this.Value = gameOptions.ImpostorLightMod;
			return;
		case StringNames.GameKillCooldown:
			this.Value = gameOptions.KillCooldown;
			return;
		case StringNames.GameKillDistance:
			break;
		case StringNames.GameCommonTasks:
			this.Value = (float)gameOptions.NumCommonTasks;
			return;
		case StringNames.GameLongTasks:
			this.Value = (float)gameOptions.NumLongTasks;
			return;
		case StringNames.GameShortTasks:
			this.Value = (float)gameOptions.NumShortTasks;
			return;
		default:
			if (title == StringNames.GameEmergencyCooldown)
			{
				this.Value = (float)gameOptions.EmergencyCooldown;
				return;
			}
			break;
		}
		Debug.Log("Ono, unrecognized setting: " + this.Title.ToString());
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00045AF0 File Offset: 0x00043CF0
	private void FixedUpdate()
	{
		if (this.oldValue != this.Value)
		{
			this.oldValue = this.Value;
			if (this.ZeroIsInfinity && Mathf.Abs(this.Value) < 0.0001f)
			{
				this.ValueText.Text = "∞";
				return;
			}
			if (this.SuffixType == NumberSuffixes.None)
			{
				this.ValueText.Text = this.Value.ToString(this.FormatString);
				return;
			}
			if (this.SuffixType == NumberSuffixes.Multiplier)
			{
				this.ValueText.Text = this.Value.ToString(this.FormatString) + "x";
				return;
			}
			this.ValueText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameSecondsAbbrev, new object[]
			{
				this.Value.ToString(this.FormatString)
			});
		}
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x00045BCE File Offset: 0x00043DCE
	public void Increase()
	{
		this.Value = this.ValidRange.Clamp(this.Value + this.Increment);
		this.OnValueChanged(this);
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00045BFA File Offset: 0x00043DFA
	public void Decrease()
	{
		this.Value = this.ValidRange.Clamp(this.Value - this.Increment);
		this.OnValueChanged(this);
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x00045C26 File Offset: 0x00043E26
	public override float GetFloat()
	{
		return this.Value;
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00045C2E File Offset: 0x00043E2E
	public override int GetInt()
	{
		return (int)this.Value;
	}

	// Token: 0x04000C5C RID: 3164
	public TextRenderer TitleText;

	// Token: 0x04000C5D RID: 3165
	public TextRenderer ValueText;

	// Token: 0x04000C5E RID: 3166
	public float Value = 1f;

	// Token: 0x04000C5F RID: 3167
	private float oldValue = float.MaxValue;

	// Token: 0x04000C60 RID: 3168
	public float Increment;

	// Token: 0x04000C61 RID: 3169
	public FloatRange ValidRange = new FloatRange(0f, 2f);

	// Token: 0x04000C62 RID: 3170
	public string FormatString = "0.0";

	// Token: 0x04000C63 RID: 3171
	public bool ZeroIsInfinity;

	// Token: 0x04000C64 RID: 3172
	public NumberSuffixes SuffixType = NumberSuffixes.Multiplier;
}
