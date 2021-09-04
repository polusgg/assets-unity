using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using InnerNet;
using PowerTools;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class ShipStatus : InnerNetObject
{
	// Token: 0x06000BF8 RID: 3064 RVA: 0x0004A710 File Offset: 0x00048910
	public void RpcCloseDoorsOfType(SystemTypes type)
	{
        if (AmongUsClient.Instance.AmHost)
        {
            this.CloseDoorsOfType(type);
            return;
        }
        MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 27, Hazel.SendOption.Reliable, AmongUsClient.Instance.HostId);
        messageWriter.Write((byte)type);
        AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
    }

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0004A764 File Offset: 0x00048964
	public void RpcRepairSystem(SystemTypes systemType, int amount)
	{
		//if (AmongUsClient.Instance.AmHost)
		//{
		//	this.RepairSystem(systemType, PlayerControl.LocalPlayer, (byte)amount);
		//	return;
		//}
		//MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 28, 1, AmongUsClient.Instance.HostId);
		//messageWriter.Write((byte)systemType);
		//messageWriter.WriteNetObject(PlayerControl.LocalPlayer);
		//messageWriter.Write((byte)amount);
		//AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
	}

    // Token: 0x06000BFA RID: 3066 RVA: 0x0004A7D0 File Offset: 0x000489D0
    public override bool Serialize(MessageWriter writer, bool initialState)
    {
        bool result = false;
        short num = 0;
        while ((int)num < SystemTypeHelpers.AllTypes.Length)
        {
            SystemTypes systemTypes = SystemTypeHelpers.AllTypes[(int)num];
            ISystemType systemType;
            if (this.Systems.TryGetValue(systemTypes, out systemType) && (initialState || systemType.IsDirty))
            {
                result = true;
                writer.StartMessage((byte)systemTypes);
                systemType.Serialize(writer, initialState);
                writer.EndMessage();
            }
            num += 1;
        }
        return result;
    }

    // Token: 0x06000BFB RID: 3067 RVA: 0x0004A830 File Offset: 0x00048A30
    public override void Deserialize(MessageReader reader, bool initialState)
    {
        while (reader.Position < reader.Length)
        {
            MessageReader messageReader = reader.ReadMessage();
            SystemTypes tag = (SystemTypes)messageReader.Tag;
            ISystemType systemType;
            if (this.Systems.TryGetValue(tag, out systemType))
            {
                systemType.Deserialize(messageReader, initialState);
            }
        }
    }

    //// Token: 0x06000BFC RID: 3068 RVA: 0x0004A874 File Offset: 0x00048A74
    public override void HandleRpc(byte callId, MessageReader reader)
    {
        if (callId == 27)
        {
            this.CloseDoorsOfType((SystemTypes)reader.ReadByte());
            return;
        }
        if (callId != 28)
        {
            return;
        }
        this.RepairSystem((SystemTypes)reader.ReadByte(), reader.ReadNetObject<PlayerControl>(), reader.ReadByte());
    }

    // Token: 0x17000100 RID: 256
    // (get) Token: 0x06000BFD RID: 3069 RVA: 0x0004A8B3 File Offset: 0x00048AB3
    // (set) Token: 0x06000BFE RID: 3070 RVA: 0x0004A8BB File Offset: 0x00048ABB
    public IStepWatcher[] AllStepWatchers { get; private set; }

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0004A8C4 File Offset: 0x00048AC4
	// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0004A8CC File Offset: 0x00048ACC
	public PlainShipRoom[] AllRooms { get; private set; }

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0004A8D5 File Offset: 0x00048AD5
	// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0004A8DD File Offset: 0x00048ADD
	public Dictionary<SystemTypes, PlainShipRoom> FastRooms { get; private set; }

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0004A8E6 File Offset: 0x00048AE6
	// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0004A8EE File Offset: 0x00048AEE
	public Vent[] AllVents { get; private set; }

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0004A8F7 File Offset: 0x00048AF7
	// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0004A8FF File Offset: 0x00048AFF
	public bool BeginCalled { get; set; }

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0004A908 File Offset: 0x00048B08
	public override bool IsDirty
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0004A90C File Offset: 0x00048B0C
	protected virtual void OnEnable()
	{
		if (this.Systems != null)
		{
			return;
		}
		this.Systems = new Dictionary<SystemTypes, ISystemType>(ShipStatus.SystemTypeComparer.Instance)
		{
			{
				SystemTypes.Electrical,
				new SwitchSystem()
			},
			{
				SystemTypes.MedBay,
				new MedScanSystem()
			}
		};
		Camera main = Camera.main;
		main.backgroundColor = this.CameraColor;
		FollowerCamera component = main.GetComponent<FollowerCamera>();
		switch (this.Type)
		{
		case ShipStatus.MapType.Ship:
			this.Systems.Add(SystemTypes.Doors, new AutoDoorsSystemType());
			this.Systems.Add(SystemTypes.Comms, new HudOverrideSystemType());
			this.Systems.Add(SystemTypes.Security, new SecurityCameraSystemType());
			this.Systems.Add(SystemTypes.Reactor, new ReactorSystemType(30f, SystemTypes.Reactor));
			this.Systems.Add(SystemTypes.LifeSupp, new LifeSuppSystemType(30f));
			if (component)
			{
				component.shakeAmount = 0.02f;
				component.shakePeriod = 0.3f;
			}
			break;
		case ShipStatus.MapType.Hq:
			this.Systems.Add(SystemTypes.Comms, new HqHudSystemType());
			this.Systems.Add(SystemTypes.Reactor, new ReactorSystemType(45f, SystemTypes.Reactor));
			this.Systems.Add(SystemTypes.LifeSupp, new LifeSuppSystemType(45f));
			if (component)
			{
				component.shakeAmount = 0f;
				component.shakePeriod = 0f;
			}
			break;
		case ShipStatus.MapType.Pb:
			DestroyableSingleton<HudManager>.Instance.ShadowQuad.material.SetInt("_Mask", 7);
			this.Systems.Add(SystemTypes.Doors, new DoorsSystemType());
			this.Systems.Add(SystemTypes.Comms, new HudOverrideSystemType());
			this.Systems.Add(SystemTypes.Security, new SecurityCameraSystemType());
			this.Systems.Add(SystemTypes.Laboratory, new ReactorSystemType(60f, SystemTypes.Laboratory));
			if (component)
			{
				component.shakeAmount = 0f;
				component.shakePeriod = 0f;
			}
			break;
		}
		this.Systems.Add(SystemTypes.Sabotage, new SabotageSystemType((from i in this.Systems.Values
		where i is IActivatable
		select i).Cast<IActivatable>().ToArray<IActivatable>()));
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x0004AB40 File Offset: 0x00048D40
	public virtual void RepairGameOverSystems()
	{
		this.RepairSystem(SystemTypes.Laboratory, PlayerControl.LocalPlayer, 16);
		this.RepairSystem(SystemTypes.Reactor, PlayerControl.LocalPlayer, 16);
		this.RepairSystem(SystemTypes.LifeSupp, PlayerControl.LocalPlayer, 16);
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x0004AB70 File Offset: 0x00048D70
	private void Awake()
	{
		this.AllStepWatchers = (from s in base.GetComponentsInChildren<IStepWatcher>()
		orderby s.Priority descending
		select s).ToArray<IStepWatcher>();
		this.AllRooms = base.GetComponentsInChildren<PlainShipRoom>();
		this.FastRooms = (from p in this.AllRooms
		where p.RoomId > SystemTypes.Hallway
		select p).ToDictionary((PlainShipRoom d) => d.RoomId);
		this.AllCameras = base.GetComponentsInChildren<SurvCamera>();
		this.AllConsoles = base.GetComponentsInChildren<global::Console>();
		this.AllVents = base.GetComponentsInChildren<Vent>();
		this.AssignTaskIndexes();
		ShipStatus.Instance = this;
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x0004AC44 File Offset: 0x00048E44
	protected virtual void Start()
	{
		if (DestroyableSingleton<HudManager>.InstanceExists)
		{
			DestroyableSingleton<HudManager>.Instance.Chat.ForceClosed();
			DestroyableSingleton<HudManager>.Instance.Chat.SetVisible(false);
			DestroyableSingleton<HudManager>.Instance.GameSettings.gameObject.SetActive(false);
		}
		foreach (DeconSystem deconSystem in base.GetComponentsInChildren<DeconSystem>())
		{
			this.Systems.Add(deconSystem.TargetSystem, deconSystem);
		}
		foreach (PlayerControl playerControl in PlayerControl.AllPlayerControls)
		{
			playerControl.UpdatePlatformIcon();
		}
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x0004ACFC File Offset: 0x00048EFC
	public override void OnDestroy()
	{
		if (SoundManager.Instance)
		{
			SoundManager.Instance.StopAllSound();
		}
		base.OnDestroy();
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0004AD1C File Offset: 0x00048F1C
	public virtual void SpawnPlayer(PlayerControl player, int numPlayers, bool initialSpawn)
	{
		Vector2 vector = Vector2.up;
		vector = vector.Rotate((float)(player.PlayerId - 1) * (360f / (float)numPlayers));
		vector *= this.SpawnRadius;
		Vector2 position = (initialSpawn ? this.InitialSpawnCenter : this.MeetingSpawnCenter) + vector + new Vector2(0f, 0.3636f);
		player.NetTransform.SnapTo(position);
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0004AD90 File Offset: 0x00048F90
	public void StartShields()
	{
		for (int i = 0; i < this.ShieldsImages.Length; i++)
		{
			this.ShieldsImages[i].Play(this.ShieldsActive, 1f);
		}
		this.ShieldBorder.sprite = this.ShieldBorderOn;
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0004ADDC File Offset: 0x00048FDC
	public void FireWeapon()
	{
		//if (this.WeaponsImage && !this.WeaponsImage.IsPlaying(null))
		//{
		//	this.WeaponsImage.Play(this.WeaponFires[this.WeaponFireIdx], 1f);
		//	this.WeaponFireIdx = (this.WeaponFireIdx + 1) % this.WeaponFires.Length;
		//}
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0004AE38 File Offset: 0x00049038
	public NormalPlayerTask GetTaskById(byte idx)
	{
		NormalPlayerTask result;
		if ((result = this.CommonTasks.FirstOrDefault((NormalPlayerTask t) => t.Index == (int)idx)) == null)
		{
			result = (this.LongTasks.FirstOrDefault((NormalPlayerTask t) => t.Index == (int)idx) ?? this.NormalTasks.FirstOrDefault((NormalPlayerTask t) => t.Index == (int)idx));
		}
		return result;
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x0004AE9F File Offset: 0x0004909F
	public void OpenHatch()
	{
		//if (this.Hatch && !this.Hatch.IsPlaying(null))
		//{
		//	this.Hatch.Play(this.HatchActive, 1f);
		//	this.HatchParticles.Play();
		//}
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x0004AEDD File Offset: 0x000490DD
	public void CloseDoorsOfType(SystemTypes room)
	{
		((IDoorSystem)this.Systems[SystemTypes.Doors]).CloseDoorsOfType(room);
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0004AEF8 File Offset: 0x000490F8
	public void RepairSystem(SystemTypes systemType, PlayerControl player, byte amount)
	{
		ISystemType systemType2;
		if (this.Systems.TryGetValue(systemType, out systemType2))
		{
			systemType2.RepairDamage(player, amount);
		}
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0004AF20 File Offset: 0x00049120
	internal void SelectInfected()
	{
		List<GameData.PlayerInfo> list = (from pcd in GameData.Instance.AllPlayers
		where !pcd.Disconnected
		select pcd into pc
		where !pc.IsDead
		select pc).ToList<GameData.PlayerInfo>();
		int adjustedNumImpostors = PlayerControl.GameOptions.GetAdjustedNumImpostors(GameData.Instance.PlayerCount);
		GameData.PlayerInfo[] array = new GameData.PlayerInfo[Mathf.Min(list.Count, adjustedNumImpostors)];
		for (int i = 0; i < array.Length; i++)
		{
			int index = HashRandom.FastNext(list.Count);
			array[i] = list[index];
			list.RemoveAt(index);
		}
		PlayerControl.LocalPlayer.RpcSetInfected(array);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0004AFE8 File Offset: 0x000491E8
	private void AssignTaskIndexes()
	{
		int num = 0;
		for (int i = 0; i < this.CommonTasks.Length; i++)
		{
			this.CommonTasks[i].Index = num++;
		}
		for (int j = 0; j < this.LongTasks.Length; j++)
		{
			this.LongTasks[j].Index = num++;
		}
		for (int k = 0; k < this.NormalTasks.Length; k++)
		{
			this.NormalTasks[k].Index = num++;
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0004B066 File Offset: 0x00049266
	public virtual void OnMeetingCalled()
	{
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0004B068 File Offset: 0x00049268
	public virtual IEnumerator PrespawnStep()
	{
		yield break;
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x0004B070 File Offset: 0x00049270
	public void Begin()
	{
		this.numScans = 0;
		this.AssignTaskIndexes();
		GameOptionsData gameOptions = PlayerControl.GameOptions;
		List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
		HashSet<TaskTypes> hashSet = new HashSet<TaskTypes>();
		List<byte> list = new List<byte>(10);
		List<NormalPlayerTask> list2 = this.CommonTasks.ToList<NormalPlayerTask>();
		list2.Shuffle(0);
		int num = 0;
		this.AddTasksFromList(ref num, gameOptions.NumCommonTasks, list, hashSet, list2);
		for (int i = 0; i < gameOptions.NumCommonTasks; i++)
		{
			if (list2.Count == 0)
			{
				Debug.LogWarning("Not enough common tasks");
				break;
			}
			int index = list2.RandomIdx<NormalPlayerTask>();
			list.Add((byte)list2[index].Index);
			list2.RemoveAt(index);
		}
		List<NormalPlayerTask> list3 = this.LongTasks.ToList<NormalPlayerTask>();
		list3.Shuffle(0);
		List<NormalPlayerTask> list4 = this.NormalTasks.ToList<NormalPlayerTask>();
		list4.Shuffle(0);
		int num2 = 0;
		int num3 = 0;
		int count = gameOptions.NumShortTasks;
		if (gameOptions.NumCommonTasks + gameOptions.NumLongTasks + gameOptions.NumShortTasks == 0)
		{
			count = 1;
		}
		byte b = 0;
		while ((int)b < allPlayers.Count)
		{
			hashSet.Clear();
			list.RemoveRange(gameOptions.NumCommonTasks, list.Count - gameOptions.NumCommonTasks);
			this.AddTasksFromList(ref num2, gameOptions.NumLongTasks, list, hashSet, list3);
			this.AddTasksFromList(ref num3, count, list, hashSet, list4);
			GameData.PlayerInfo playerInfo = allPlayers[(int)b];
			if (playerInfo.Object && !playerInfo.Object.GetComponent<DummyBehaviour>().enabled)
			{
				byte[] taskTypeIds = list.ToArray();
				GameData.Instance.RpcSetTasks(playerInfo.PlayerId, taskTypeIds);
			}
			b += 1;
		}
		this.BeginCalled = true;
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0004B220 File Offset: 0x00049420
	private void AddTasksFromList(ref int start, int count, List<byte> tasks, HashSet<TaskTypes> usedTaskTypes, List<NormalPlayerTask> unusedTasks)
	{
		//int num = 0;
		//int num2 = 0;
		//Func<NormalPlayerTask, bool> <>9__0;
		//while (num2 < count && num++ != 1000)
		//{
		//	if (start >= unusedTasks.Count)
		//	{
		//		start = 0;
		//		unusedTasks.Shuffle(0);
		//		Func<NormalPlayerTask, bool> predicate;
		//		if ((predicate = <>9__0) == null)
		//		{
		//			predicate = (<>9__0 = ((NormalPlayerTask t) => usedTaskTypes.Contains(t.TaskType)));
		//		}
		//		if (unusedTasks.All(predicate))
		//		{
		//			Debug.Log("Not enough task types");
		//			usedTaskTypes.Clear();
		//		}
		//	}
		//	int num3 = start;
		//	start = num3 + 1;
		//	NormalPlayerTask normalPlayerTask = unusedTasks[num3];
		//	if (!usedTaskTypes.Add(normalPlayerTask.TaskType))
		//	{
		//		num2--;
		//	}
		//	else
		//	{
		//		tasks.Add((byte)normalPlayerTask.Index);
		//		if (!PlayerControl.GameOptions.VisualTasks && normalPlayerTask.TaskType == TaskTypes.SubmitScan)
		//		{
		//			num3 = this.numScans;
		//			this.numScans = num3 + 1;
		//			if (num3 > 1)
		//			{
		//				unusedTasks.Remove(normalPlayerTask);
		//			}
		//		}
		//	}
		//	num2++;
		//}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0004B324 File Offset: 0x00049524
	public void FixedUpdate()
	{
		if (!AmongUsClient.Instance)
		{
			return;
		}
		if (!PlayerControl.LocalPlayer)
		{
			return;
		}
		this.Timer += Time.fixedDeltaTime;
		this.EmergencyCooldown -= Time.fixedDeltaTime;
		if (GameData.Instance)
		{
			GameData.Instance.RecomputeTaskCounts();
		}
		if (AmongUsClient.Instance.AmHost && this.BeginCalled)
		{
			this.CheckEndCriteria();
		}
		if (AmongUsClient.Instance.AmClient)
		{
			for (int i = 0; i < SystemTypeHelpers.AllTypes.Length; i++)
			{
				SystemTypes key = SystemTypeHelpers.AllTypes[i];
				ISystemType systemType;
				if (this.Systems.TryGetValue(key, out systemType))
				{
					systemType.Detoriorate(Time.fixedDeltaTime);
				}
			}
		}
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0004B3E0 File Offset: 0x000495E0
	public virtual float CalculateLightRadius(GameData.PlayerInfo player)
	{
		if (player == null || player.IsDead)
		{
			return this.MaxLightRadius;
		}
		SwitchSystem switchSystem = (SwitchSystem)this.Systems[SystemTypes.Electrical];
		if (player.IsImpostor)
		{
			return this.MaxLightRadius * PlayerControl.GameOptions.ImpostorLightMod;
		}
		float num = (float)switchSystem.Value / 255f;
		return Mathf.Lerp(this.MinLightRadius, this.MaxLightRadius, num) * PlayerControl.GameOptions.CrewLightMod;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0004B458 File Offset: 0x00049658
	private void CheckEndCriteria()
	{
		if (!GameData.Instance)
		{
			return;
		}
		ISystemType systemType;
		if (this.Systems.TryGetValue(SystemTypes.LifeSupp, out systemType))
		{
			LifeSuppSystemType lifeSuppSystemType = (LifeSuppSystemType)systemType;
			if (lifeSuppSystemType.Countdown < 0f)
			{
				this.EndGameForSabotage();
				lifeSuppSystemType.Countdown = 10000f;
			}
		}
		ISystemType systemType2;
		if ((this.Systems.TryGetValue(SystemTypes.Reactor, out systemType2) || this.Systems.TryGetValue(SystemTypes.Laboratory, out systemType2)) && systemType2 is ICriticalSabotage)
		{
			ICriticalSabotage criticalSabotage = (ICriticalSabotage)systemType2;
			if (criticalSabotage.Countdown < 0f)
			{
				this.EndGameForSabotage();
				criticalSabotage.ClearSabotage();
			}
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < GameData.Instance.PlayerCount; i++)
		{
			GameData.PlayerInfo playerInfo = GameData.Instance.AllPlayers[i];
			if (!playerInfo.Disconnected)
			{
				if (playerInfo.IsImpostor)
				{
					num3++;
				}
				if (!playerInfo.IsDead)
				{
					if (playerInfo.IsImpostor)
					{
						num2++;
					}
					else
					{
						num++;
					}
				}
			}
		}
		if (num2 <= 0 && (!DestroyableSingleton<TutorialManager>.InstanceExists || num3 > 0))
		{
			if (!DestroyableSingleton<TutorialManager>.InstanceExists)
			{
				this.BeginCalled = false;
				ShipStatus.RpcEndGame(GameOverReason.HumansByVote, !SaveManager.BoughtNoAds);
				return;
			}
			if (!Minigame.Instance)
			{
				DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverImpostorDead, Array.Empty<object>()));
				ShipStatus.ReviveEveryone();
				return;
			}
		}
		else if (num <= num2)
		{
			if (!DestroyableSingleton<TutorialManager>.InstanceExists)
			{
				this.BeginCalled = false;
				GameOverReason endReason;
				switch (TempData.LastDeathReason)
				{
				case DeathReason.Exile:
					endReason = GameOverReason.ImpostorByVote;
					break;
				case DeathReason.Kill:
					endReason = GameOverReason.ImpostorByKill;
					break;
				default:
					endReason = GameOverReason.ImpostorByVote;
					break;
				}
				ShipStatus.RpcEndGame(endReason, !SaveManager.BoughtNoAds);
				return;
			}
			DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverImpostorKills, Array.Empty<object>()));
			ShipStatus.ReviveEveryone();
			return;
		}
		else if (!DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			if (GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks)
			{
				base.enabled = false;
				ShipStatus.RpcEndGame(GameOverReason.HumansByTask, !SaveManager.BoughtNoAds);
				return;
			}
		}
		else if (PlayerControl.LocalPlayer.myTasks.All((PlayerTask t) => t.IsComplete))
		{
			DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverTaskWin, Array.Empty<object>()));
			this.Begin();
		}
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0004B6B1 File Offset: 0x000498B1
	private void EndGameForSabotage()
	{
		if (!DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			this.BeginCalled = false;
			ShipStatus.RpcEndGame(GameOverReason.ImpostorBySabotage, !SaveManager.BoughtNoAds);
			return;
		}
		DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverSabotage, Array.Empty<object>()));
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0004B6F0 File Offset: 0x000498F0
	public bool IsGameOverDueToDeath()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < GameData.Instance.PlayerCount; i++)
		{
			GameData.PlayerInfo playerInfo = GameData.Instance.AllPlayers[i];
			if (!playerInfo.Disconnected)
			{
				if (playerInfo.IsImpostor)
				{
					num3++;
				}
				if (!playerInfo.IsDead)
				{
					if (playerInfo.IsImpostor)
					{
						num2++;
					}
					else
					{
						num++;
					}
				}
			}
		}
		return (num2 <= 0 && (!DestroyableSingleton<TutorialManager>.InstanceExists || num3 > 0)) || num <= num2;
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0004B774 File Offset: 0x00049974
	private static void RpcEndGame(GameOverReason endReason, bool showAd)
	{
		Debug.Log("Endgame for " + endReason.ToString());
		MessageWriter messageWriter = AmongUsClient.Instance.StartEndGame();
		messageWriter.Write((byte)endReason);
		messageWriter.Write(showAd);
		AmongUsClient.Instance.FinishEndGame(messageWriter);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0004B7C4 File Offset: 0x000499C4
	private static void ReviveEveryone()
	{
		for (int i = 0; i < GameData.Instance.PlayerCount; i++)
		{
			GameData.Instance.AllPlayers[i].Object.Revive();
		}
		//Object.FindObjectsOfType<DeadBody>().ForEach(delegate(DeadBody b)
		//{
		//	Object.Destroy(b.gameObject);
		//});
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0004B82C File Offset: 0x00049A2C
	public bool CheckTaskCompletion()
	{
		if (DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			if (PlayerControl.LocalPlayer.myTasks.All((PlayerTask t) => t.IsComplete))
			{
				DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverTaskWin, Array.Empty<object>()));
				this.Begin();
			}
			return false;
		}
		GameData.Instance.RecomputeTaskCounts();
		if (GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks)
		{
			this.BeginCalled = false;
			ShipStatus.RpcEndGame(GameOverReason.HumansByTask, !SaveManager.BoughtNoAds);
			return true;
		}
		return false;
	}

	// Token: 0x04000D58 RID: 3416
	public static ShipStatus Instance;

	// Token: 0x04000D59 RID: 3417
	public Color CameraColor = Color.black;

	// Token: 0x04000D5A RID: 3418
	public float MaxLightRadius = 5f;

	// Token: 0x04000D5B RID: 3419
	public float MinLightRadius = 1f;

	// Token: 0x04000D5C RID: 3420
	public float MapScale = 4.4f;

	// Token: 0x04000D5D RID: 3421
	public MapBehaviour MapPrefab;

	// Token: 0x04000D5E RID: 3422
	public ExileController ExileCutscenePrefab;

	// Token: 0x04000D5F RID: 3423
	public OverlayKillAnimation EmergencyOverlay;

	// Token: 0x04000D60 RID: 3424
	public OverlayKillAnimation ReportOverlay;

	// Token: 0x04000D61 RID: 3425
	public Vector2 InitialSpawnCenter;

	// Token: 0x04000D62 RID: 3426
	public Vector2 MeetingSpawnCenter;

	// Token: 0x04000D63 RID: 3427
	public Vector2 MeetingSpawnCenter2;

	// Token: 0x04000D64 RID: 3428
	public float SpawnRadius = 1.55f;

	// Token: 0x04000D65 RID: 3429
	public NormalPlayerTask[] CommonTasks;

	// Token: 0x04000D66 RID: 3430
	public NormalPlayerTask[] LongTasks;

	// Token: 0x04000D67 RID: 3431
	public NormalPlayerTask[] NormalTasks;

	// Token: 0x04000D68 RID: 3432
	public PlayerTask[] SpecialTasks;

	// Token: 0x04000D69 RID: 3433
	public Transform[] DummyLocations;

	// Token: 0x04000D6A RID: 3434
	public SurvCamera[] AllCameras;

	// Token: 0x04000D6B RID: 3435
	public PlainDoor[] AllDoors;

	// Token: 0x04000D6C RID: 3436
	public global::Console[] AllConsoles;

	// Token: 0x04000D6D RID: 3437
	public Dictionary<SystemTypes, ISystemType> Systems;

	// Token: 0x04000D6E RID: 3438
	public StringNames[] SystemNames;

	// Token: 0x04000D73 RID: 3443
	public AudioClip SabotageSound;

	// Token: 0x04000D74 RID: 3444
	public AnimationClip[] WeaponFires;

	// Token: 0x04000D75 RID: 3445
	public SpriteAnim WeaponsImage;

	// Token: 0x04000D76 RID: 3446
	public AudioClip[] VentMoveSounds;

	// Token: 0x04000D77 RID: 3447
	public AudioClip VentEnterSound;

	// Token: 0x04000D78 RID: 3448
	public AnimationClip HatchActive;

	// Token: 0x04000D79 RID: 3449
	public SpriteAnim Hatch;

	// Token: 0x04000D7A RID: 3450
	public ParticleSystem HatchParticles;

	// Token: 0x04000D7B RID: 3451
	public AnimationClip ShieldsActive;

	// Token: 0x04000D7C RID: 3452
	public SpriteAnim[] ShieldsImages;

	// Token: 0x04000D7D RID: 3453
	public SpriteRenderer ShieldBorder;

	// Token: 0x04000D7E RID: 3454
	public Sprite ShieldBorderOn;

	// Token: 0x04000D7F RID: 3455
	public MedScannerBehaviour MedScanner;

	// Token: 0x04000D80 RID: 3456
	private int WeaponFireIdx;

	// Token: 0x04000D81 RID: 3457
	public float Timer;

	// Token: 0x04000D82 RID: 3458
	public float EmergencyCooldown;

	// Token: 0x04000D83 RID: 3459
	public ShipStatus.MapType Type;

	// Token: 0x04000D85 RID: 3461
	private int numScans;

	// Token: 0x02000443 RID: 1091
	public enum MapType
	{
		// Token: 0x04001C25 RID: 7205
		Ship,
		// Token: 0x04001C26 RID: 7206
		Hq,
		// Token: 0x04001C27 RID: 7207
		Pb,
		
		Submarine = 5,
	}

	// Token: 0x02000444 RID: 1092
	public class SystemTypeComparer : IEqualityComparer<SystemTypes>
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x00079802 File Offset: 0x00077A02
		public bool Equals(SystemTypes x, SystemTypes y)
		{
			return x == y;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00079808 File Offset: 0x00077A08
		public int GetHashCode(SystemTypes obj)
		{
			return (int)obj;
		}

		// Token: 0x04001C28 RID: 7208
		public static readonly ShipStatus.SystemTypeComparer Instance = new ShipStatus.SystemTypeComparer();
	}
}
