using System;
using System.Collections;
//using Discord;
using InnerNet;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000082 RID: 130
public class DiscordManager : DestroyableSingleton<DiscordManager>
{
	// Token: 0x06000322 RID: 802 RVA: 0x00014A18 File Offset: 0x00012C18
	public void Start()
	{
		//if (DestroyableSingleton<DiscordManager>.Instance != this)
		//{
		//	return;
		//}
		//try
		//{
		//	this.presence = new Discord(477175586805252107L, 1UL);
		//	ActivityManager activityManager = this.presence.GetActivityManager();
		//	activityManager.RegisterSteam(945360U);
		//	activityManager.OnActivityJoin += new ActivityManager.ActivityJoinHandler(this.HandleJoinRequest);
		//	this.SetInMenus();
		//	SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode mode)
		//	{
		//		this.OnSceneChange(scene.name);
		//	};
		//}
		//catch
		//{
		//	Debug.LogWarning("Discord messed up");
		//}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x00014AA8 File Offset: 0x00012CA8
	private void OnSceneChange(string name)
	{
		if (name != null && (name == "MatchMaking" || name == "MMOnline" || name == "MainMenu"))
		{
			this.SetInMenus();
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00014ADC File Offset: 0x00012CDC
	public void FixedUpdate()
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//try
		//{
		//	this.presence.RunCallbacks();
		//}
		//catch (ResultException)
		//{
		//	this.presence.Dispose();
		//	this.presence = null;
		//}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00014B28 File Offset: 0x00012D28
	public void SetInMenus()
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//this.ClearPresence();
		//this.StartTime = null;
		//Activity activity = default(Activity);
		//activity.State = "In Menus";
		//activity.Assets.LargeImage = "icon";
		//this.presence.GetActivityManager().UpdateActivity(activity, delegate(Result r)
		//{
		//});
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00014BA8 File Offset: 0x00012DA8
	public void SetPlayingGame()
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//if (this.StartTime == null)
		//{
		//	this.StartTime = new DateTime?(DateTime.UtcNow);
		//}
		//Activity activity = default(Activity);
		//activity.State = "In Game";
		//activity.Details = "Playing";
		//activity.Assets.LargeImage = "icon";
		//activity.Timestamps.Start = DiscordManager.ToUnixTime(this.StartTime.Value);
		//this.presence.GetActivityManager().UpdateActivity(activity, delegate(Result r)
		//{
		//});
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00014C58 File Offset: 0x00012E58
	public void SetHowToPlay()
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//this.ClearPresence();
		//Activity activity = default(Activity);
		//activity.State = "In Freeplay";
		//activity.Assets.LargeImage = "icon";
		//this.presence.GetActivityManager().UpdateActivity(activity, delegate(Result r)
		//{
		//});
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00014CCC File Offset: 0x00012ECC
	public void SetInLobbyClient(int numPlayers, int gameId)
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//if (this.StartTime == null)
		//{
		//	this.StartTime = new DateTime?(DateTime.UtcNow);
		//}
		//string id = GameCode.IntToGameName(gameId);
		//this.ClearPresence();
		//Activity activity = default(Activity);
		//activity.State = "In Lobby";
		//activity.Assets.LargeImage = "icon";
		//activity.Timestamps.Start = DiscordManager.ToUnixTime(this.StartTime.Value);
		//activity.Party.Size.CurrentSize = numPlayers;
		//activity.Party.Size.MaxSize = 10;
		//activity.Party.Id = id;
		//this.presence.GetActivityManager().UpdateActivity(activity, delegate(Result r)
		//{
		//});
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00014DAF File Offset: 0x00012FAF
	private void ClearPresence()
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//this.presence.GetActivityManager().ClearActivity(delegate(Result r)
		//{
		//});
	}

	// Token: 0x0600032A RID: 810 RVA: 0x00014DEC File Offset: 0x00012FEC
	public void SetInLobbyHost(int numPlayers, int gameId)
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//if (this.StartTime == null)
		//{
		//	this.StartTime = new DateTime?(DateTime.UtcNow);
		//}
		//string text = GameCode.IntToGameName(gameId);
		//Activity activity = default(Activity);
		//activity.State = "In Lobby";
		//activity.Details = "Hosting a game";
		//activity.Assets.LargeImage = "icon";
		//activity.Assets.LargeText = "Ask to play!";
		//activity.Party.Size.CurrentSize = numPlayers;
		//activity.Party.Size.MaxSize = 10;
		//activity.Secrets.Join = "join" + text;
		//activity.Secrets.Match = "match" + text;
		//activity.Party.Id = text;
		//this.presence.GetActivityManager().UpdateActivity(activity, delegate(Result r)
		//{
		//});
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00014EF8 File Offset: 0x000130F8
	private void HandleJoinRequest(string joinSecret)
	{
		if (!joinSecret.StartsWith("join"))
		{
			Debug.LogWarning("Invalid join secret: " + joinSecret);
			return;
		}
		if (!AmongUsClient.Instance)
		{
			Debug.LogWarning("Missing AmongUsClient");
			return;
		}
		if (!DestroyableSingleton<DiscordManager>.InstanceExists)
		{
			Debug.LogWarning("Missing DiscordManager");
			return;
		}
		if (AmongUsClient.Instance.mode != MatchMakerModes.None)
		{
			Debug.LogWarning("Already connected");
			return;
		}
		AmongUsClient.Instance.GameMode = GameModes.OnlineGame;
		AmongUsClient.Instance.GameId = GameCode.GameNameToInt(joinSecret.Substring(4));
		AmongUsClient.Instance.SetEndpoint(DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress, 22023);
		AmongUsClient.Instance.MainMenuScene = "MMOnline";
		AmongUsClient.Instance.OnlineScene = "OnlineGame";
		DestroyableSingleton<DiscordManager>.Instance.StopAllCoroutines();
		AmongUsClient.Instance.Connect(MatchMakerModes.Client);
		DestroyableSingleton<DiscordManager>.Instance.StartCoroutine(DestroyableSingleton<DiscordManager>.Instance.CoJoinGame());
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00014FE5 File Offset: 0x000131E5
	private IEnumerator CoJoinGame()
	{
		yield return AmongUsClient.Instance.WaitForConnectionOrFail();
		if (AmongUsClient.Instance.ClientId < 0)
		{
			SceneManager.LoadScene("MMOnline");
		}
		yield break;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00014FED File Offset: 0x000131ED
	public void RequestRespondYes(long userId)
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		////this.presence.GetActivityManager().SendRequestReply(userId, 1, delegate(Result r)
		//{
		//});
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001502C File Offset: 0x0001322C
	public void RequestRespondNo(long userId)
	{
		//if (this.presence == null)
		//{
		//	return;
		//}
		//Debug.Log("Discord: responding no to Ask to Join request");
		////this.presence.GetActivityManager().SendRequestReply(userId, 0, delegate(Result r)
		////{
		////});
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001507D File Offset: 0x0001327D
	public override void OnDestroy()
	{
		//base.OnDestroy();
		//if (this.presence != null)
		//{
		//	this.presence.Dispose();
		//}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00015098 File Offset: 0x00013298
	private static long ToUnixTime(DateTime time)
	{
		return (long)(time - DiscordManager.epoch).TotalSeconds;
	}

	// Token: 0x040003A4 RID: 932
	private const long ClientId = 477175586805252107L;

	// Token: 0x040003A5 RID: 933
	[NonSerialized]
	//private Discord presence;

	// Token: 0x040003A6 RID: 934
	private DateTime? StartTime;

	// Token: 0x040003A7 RID: 935
	private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
