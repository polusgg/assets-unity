using System;
using System.Collections;
using InnerNet;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000248 RID: 584
public class TutorialManager : DestroyableSingleton<TutorialManager>
{
	// Token: 0x06000DAA RID: 3498 RVA: 0x00052F73 File Offset: 0x00051173
	public override void Awake()
	{
		base.Awake();
		StatsManager.Instance = new TutorialStatsManager();
		base.StartCoroutine(this.RunTutorial());
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x00052F92 File Offset: 0x00051192
	public override void OnDestroy()
	{
		StatsManager.Instance = new StatsManager();
		base.OnDestroy();
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x00052FA4 File Offset: 0x000511A4
	private IEnumerator RunTutorial()
	{
		while (!ShipStatus.Instance)
		{
			yield return null;
		}
		ShipStatus.Instance.Timer = 15f;
		while (!PlayerControl.LocalPlayer)
		{
			yield return null;
		}
		if (DestroyableSingleton<DiscordManager>.InstanceExists)
		{
			DestroyableSingleton<DiscordManager>.Instance.SetHowToPlay();
		}
		PlayerControl.GameOptions = new GameOptionsData
		{
			NumImpostors = 0,
			DiscussionTime = 0,
			NumEmergencyMeetings = 9
		};
		PlayerControl.LocalPlayer.RpcSetInfected(new GameData.PlayerInfo[0]);
		for (int i = 0; i < ShipStatus.Instance.DummyLocations.Length; i++)
		{
			PlayerControl playerControl = Object.Instantiate<PlayerControl>(this.PlayerPrefab);
			playerControl.PlayerId = (byte)GameData.Instance.GetAvailableId();
			GameData.PlayerInfo playerInfo = GameData.Instance.AddPlayer(playerControl);
			AmongUsClient.Instance.Spawn(playerControl, -2, SpawnFlags.None);
			playerInfo.dontCensorName = true;
			playerControl.isDummy = true;
			playerControl.transform.position = ShipStatus.Instance.DummyLocations[i].position;
			playerControl.GetComponent<DummyBehaviour>().enabled = true;
			playerControl.NetTransform.enabled = false;
			playerControl.SetName(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Dummy, Array.Empty<object>()) + " " + (i + 1).ToString(), true);
			byte b = (byte)((i < (int)SaveManager.BodyColor) ? i : (i + 1));
			playerControl.SetColor((int)b);
			playerControl.SetHat(0U, (int)b);
			playerControl.SetSkin(0U);
			playerControl.SetPet(0U);
			GameData.Instance.RpcSetTasks(playerControl.PlayerId, new byte[0]);
		}
		ShipStatus.Instance.Begin();
		yield break;
	}

	// Token: 0x040011B8 RID: 4536
	public PlayerControl PlayerPrefab;
}
