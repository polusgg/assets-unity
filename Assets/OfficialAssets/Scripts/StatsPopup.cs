using System;
using System.Text;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class StatsPopup : MonoBehaviour
{
	// Token: 0x06000CD6 RID: 3286 RVA: 0x0004E720 File Offset: 0x0004C920
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0004E734 File Offset: 0x0004C934
	private void OnEnable()
	{
		StringBuilder stringBuilder = new StringBuilder(1024);
		StringBuilder stringBuilder2 = new StringBuilder(256);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsBodiesReported, StatsManager.Instance.BodiesReported);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsEmergenciesCalled, StatsManager.Instance.EmergenciesCalled);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsTasksCompleted, StatsManager.Instance.TasksCompleted);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsAllTasksCompleted, StatsManager.Instance.CompletedAllTasks);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsSabotagesFixed, StatsManager.Instance.SabsFixed);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsImpostorKills, StatsManager.Instance.ImpostorKills);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsTimesMurdered, StatsManager.Instance.TimesMurdered);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsTimesEjected, StatsManager.Instance.TimesEjected);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsCrewmateStreak, StatsManager.Instance.CrewmateStreak);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsGamesImpostor, StatsManager.Instance.TimesImpostor);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsGamesCrewmate, StatsManager.Instance.TimesCrewmate);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsGamesStarted, StatsManager.Instance.GamesStarted);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsGamesFinished, StatsManager.Instance.GamesFinished);
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsImpostorVoteWins, StatsManager.Instance.GetWinReason(GameOverReason.ImpostorByVote));
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsImpostorKillsWins, StatsManager.Instance.GetWinReason(GameOverReason.ImpostorByKill));
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsImpostorSabotageWins, StatsManager.Instance.GetWinReason(GameOverReason.ImpostorBySabotage));
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsCrewmateVoteWins, StatsManager.Instance.GetWinReason(GameOverReason.HumansByVote));
		StatsPopup.AppendStat(stringBuilder, stringBuilder2, StringNames.StatsCrewmateTaskWins, StatsManager.Instance.GetWinReason(GameOverReason.HumansByTask));
		this.StatsText.Text = stringBuilder.ToString();
		this.NumbersText.Text = stringBuilder2.ToString();
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton);
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0004E97A File Offset: 0x0004CB7A
	private static void AppendStat(StringBuilder str, StringBuilder strNums, StringNames statName, object stat)
	{
		str.AppendLine(DestroyableSingleton<TranslationController>.Instance.GetString(statName, Array.Empty<object>()));
		strNums.Append(stat);
		strNums.AppendLine();
	}

	// Token: 0x04000E42 RID: 3650
	public TextRenderer StatsText;

	// Token: 0x04000E43 RID: 3651
	public TextRenderer NumbersText;

	// Token: 0x04000E44 RID: 3652
	[Header("Console Controller Navigation")]
	public UiElement BackButton;
}
