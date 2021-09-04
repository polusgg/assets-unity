using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class GameOptionsMenu : MonoBehaviour
{
	// Token: 0x06000AE5 RID: 2789 RVA: 0x00045168 File Offset: 0x00043368
	public void Start()
	{
		this.Children = base.GetComponentsInChildren<OptionBehaviour>();
		this.cachedData = PlayerControl.GameOptions;
		for (int i = 0; i < this.Children.Length; i++)
		{
			OptionBehaviour optionBehaviour = this.Children[i];
			optionBehaviour.OnValueChanged = new Action<OptionBehaviour>(this.ValueChanged);
			if (AmongUsClient.Instance && !AmongUsClient.Instance.AmHost)
			{
				optionBehaviour.SetAsPlayer();
			}
		}
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x000451D8 File Offset: 0x000433D8
	public void Update()
	{
		if (this.cachedData != PlayerControl.GameOptions)
		{
			this.cachedData = PlayerControl.GameOptions;
			this.RefreshChildren();
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x000451F8 File Offset: 0x000433F8
	private void RefreshChildren()
	{
		for (int i = 0; i < this.Children.Length; i++)
		{
			OptionBehaviour optionBehaviour = this.Children[i];
			optionBehaviour.enabled = false;
			optionBehaviour.enabled = true;
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00045230 File Offset: 0x00043430
	public void ValueChanged(OptionBehaviour option)
	{
		if (!AmongUsClient.Instance || !AmongUsClient.Instance.AmHost)
		{
			return;
		}
		if (option.Title == StringNames.GameRecommendedSettings)
		{
			if (this.cachedData.isDefaults)
			{
				this.cachedData.isDefaults = false;
			}
			else
			{
				this.cachedData.SetRecommendations(GameData.Instance.PlayerCount, AmongUsClient.Instance.GameMode);
			}
			this.RefreshChildren();
		}
		else
		{
			GameOptionsData gameOptions = PlayerControl.GameOptions;
			StringNames title = option.Title;
			switch (title)
			{
			case StringNames.GameMapName:
				gameOptions.MapId = (byte)option.GetInt();
				break;
			case StringNames.GameNumImpostors:
				gameOptions.NumImpostors = option.GetInt();
				break;
			case StringNames.GameNumMeetings:
				gameOptions.NumEmergencyMeetings = option.GetInt();
				break;
			case StringNames.GameDiscussTime:
				gameOptions.DiscussionTime = option.GetInt();
				break;
			case StringNames.GameVotingTime:
				gameOptions.VotingTime = option.GetInt();
				break;
			case StringNames.GamePlayerSpeed:
				gameOptions.PlayerSpeedMod = option.GetFloat();
				break;
			case StringNames.GameCrewLight:
				gameOptions.CrewLightMod = option.GetFloat();
				break;
			case StringNames.GameImpostorLight:
				gameOptions.ImpostorLightMod = option.GetFloat();
				break;
			case StringNames.GameKillCooldown:
				gameOptions.KillCooldown = option.GetFloat();
				break;
			case StringNames.GameKillDistance:
				gameOptions.KillDistance = option.GetInt();
				break;
			case StringNames.GameCommonTasks:
				gameOptions.NumCommonTasks = option.GetInt();
				break;
			case StringNames.GameLongTasks:
				gameOptions.NumLongTasks = option.GetInt();
				break;
			case StringNames.GameShortTasks:
				gameOptions.NumShortTasks = option.GetInt();
				break;
			default:
				if (title != StringNames.GameEmergencyCooldown)
				{
					switch (title)
					{
					case StringNames.GameConfirmImpostor:
						gameOptions.ConfirmImpostor = option.GetBool();
						goto IL_226;
					case StringNames.GameVisualTasks:
						gameOptions.VisualTasks = option.GetBool();
						goto IL_226;
					case StringNames.GameAnonymousVotes:
						gameOptions.AnonymousVotes = option.GetBool();
						goto IL_226;
					case StringNames.GameTaskBarMode:
						gameOptions.TaskBarMode = (TaskBarMode)option.GetInt();
						goto IL_226;
					}
					Debug.Log("Ono, unrecognized setting: " + option.Title.ToString());
				}
				else
				{
					gameOptions.EmergencyCooldown = option.GetInt();
				}
				break;
			}
			IL_226:
			if (gameOptions.isDefaults && option.Title != StringNames.GameMapName)
			{
				gameOptions.isDefaults = false;
				this.RefreshChildren();
			}
		}
		if (PlayerControl.LocalPlayer)
		{
			PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);
		}
	}

	// Token: 0x04000C45 RID: 3141
	private GameOptionsData cachedData;

	// Token: 0x04000C46 RID: 3142
	public GameObject ResetButton;

	// Token: 0x04000C47 RID: 3143
	private OptionBehaviour[] Children;
}
