using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.CoreScripts;
//using Beebyte.Obfuscator;
using Hazel;
using InnerNet;
using PowerTools;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020001AF RID: 431
//[SkipRename]
public class PlayerControl : InnerNetObject
{
	// Token: 0x06000999 RID: 2457 RVA: 0x0003E370 File Offset: 0x0003C570
	public void RpcSetScanner(bool value)
	{
		//byte b = this.scannerCount + 1;
		//this.scannerCount = b;
		//byte b2 = b;
		//if (AmongUsClient.Instance.AmClient)
		//{
		//	this.SetScanner(value, b2);
		//}
		//if (!PlayerControl.GameOptions.VisualTasks)
		//{
		//	return;
		//}
		//MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 15, 1);
		//messageWriter.Write(value);
		//messageWriter.Write(b2);
		//messageWriter.EndMessage();
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
	public void RpcUsePlatform()
	{
		if (AmongUsClient.Instance.AmHost)
		{
			AirshipStatus airshipStatus = ShipStatus.Instance as AirshipStatus;
			if (airshipStatus)
			{
				airshipStatus.GapPlatform.Use(this);
				return;
			}
		}
		else
		{
			AmongUsClient.Instance.StartRpc(this.NetId, 32, SendOption.Reliable).EndMessage();
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0003E429 File Offset: 0x0003C629
	public void RpcPlayAnimation(byte animType)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.PlayAnimation(animType);
		}
		if (!PlayerControl.GameOptions.VisualTasks)
		{
			return;
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 0, 0);
		messageWriter.Write(animType);
		messageWriter.EndMessage();
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0003E46C File Offset: 0x0003C66C
	public void RpcSetStartCounter(int secondsLeft)
	{
		int lastStartCounter = this.LastStartCounter;
		this.LastStartCounter = lastStartCounter + 1;
		int num = lastStartCounter;
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 18, SendOption.Reliable);
		messageWriter.WritePacked(num);
		messageWriter.Write((sbyte)secondsLeft);
		messageWriter.EndMessage();
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0003E4B2 File Offset: 0x0003C6B2
	public void RpcCompleteTask(uint idx)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.CompleteTask(idx);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 1, SendOption.Reliable);
		messageWriter.WritePacked(idx);
		messageWriter.EndMessage();
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0003E4E8 File Offset: 0x0003C6E8
	public void RpcSyncSettings(GameOptionsData gameOptions)
	{
		if (!AmongUsClient.Instance.AmHost || DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			return;
		}
		PlayerControl.GameOptions = gameOptions;
		SaveManager.GameHostOptions = gameOptions;
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 2, SendOption.Reliable);
		messageWriter.WriteBytesAndSize(gameOptions.ToBytes(4));
		messageWriter.EndMessage();
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0003E53C File Offset: 0x0003C73C
	public void RpcSetInfected(GameData.PlayerInfo[] infected)
	{
		byte[] array = (from p in infected
		select p.PlayerId).ToArray<byte>();
		if (AmongUsClient.Instance.AmClient)
		{
			this.SetInfected(array);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 3, SendOption.Reliable);
		messageWriter.WriteBytesAndSize(array);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0003E5A8 File Offset: 0x0003C7A8
	public void CmdCheckName(string name)
	{
		if (AmongUsClient.Instance.AmHost)
		{
			this.CheckName(name);
			return;
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 5, SendOption.Reliable, AmongUsClient.Instance.HostId);
		messageWriter.Write(name);
		AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0003E5F8 File Offset: 0x0003C7F8
	public void RpcSetSkin(uint skinId)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.SetSkin(skinId);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 10, SendOption.Reliable);
		messageWriter.WritePacked(skinId);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0003E62C File Offset: 0x0003C82C
	public void RpcSetHat(uint hatId)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			GameData.PlayerInfo data = this.Data;
			int colorId = (data != null) ? data.ColorId : 0;
			this.SetHat(hatId, colorId);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 9, SendOption.Reliable);
		messageWriter.WritePacked(hatId);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0003E67F File Offset: 0x0003C87F
	public void RpcSetPet(uint petId)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.SetPet(petId);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 17, SendOption.Reliable);
		messageWriter.WritePacked(petId);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0003E6B3 File Offset: 0x0003C8B3
	public void RpcSetName(string name)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.SetName(name, false);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 6, SendOption.Reliable);
		messageWriter.Write(name);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
	public void CmdCheckColor(byte bodyColor)
	{
		if (AmongUsClient.Instance.AmHost)
		{
			this.CheckColor(bodyColor);
			return;
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 7, SendOption.Reliable, AmongUsClient.Instance.HostId);
		messageWriter.Write(bodyColor);
		AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0003E738 File Offset: 0x0003C938
	public void RpcSetColor(byte bodyColor)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.SetColor((int)bodyColor);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 8, SendOption.Reliable);
		messageWriter.Write(bodyColor);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0003E76C File Offset: 0x0003C96C
	public bool RpcSendChat(string chatText)
	{
		if (string.IsNullOrWhiteSpace(chatText))
		{
			return false;
		}
		if (AmongUsClient.Instance.AmClient && DestroyableSingleton<HudManager>.Instance)
		{
			DestroyableSingleton<HudManager>.Instance.Chat.AddChat(this, chatText);
		}
		if (chatText.IndexOf("who", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			DestroyableSingleton<Telemetry>.Instance.SendWho();
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 13, SendOption.Reliable);
		messageWriter.Write(chatText);
		messageWriter.EndMessage();
		return true;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0003E7E8 File Offset: 0x0003C9E8
	public void RpcSendChatNote(byte srcPlayerId, ChatNoteTypes noteType)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(srcPlayerId);
			DestroyableSingleton<HudManager>.Instance.Chat.AddChatNote(playerById, noteType);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 16, SendOption.Reliable);
		messageWriter.Write(srcPlayerId);
		messageWriter.Write((byte)noteType);
		messageWriter.EndMessage();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0003E848 File Offset: 0x0003CA48
	public void CmdReportDeadBody(GameData.PlayerInfo target)
	{
		if (AmongUsClient.Instance.AmHost)
		{
			this.ReportDeadBody(target);
			return;
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 11, SendOption.Reliable);
		messageWriter.Write((target != null) ? target.PlayerId : byte.MaxValue);
		messageWriter.EndMessage();
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0003E898 File Offset: 0x0003CA98
	public void RpcStartMeeting(GameData.PlayerInfo info)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			base.StartCoroutine(this.CoStartMeeting(info));
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 14, SendOption.Reliable, -1);
		messageWriter.Write((info != null) ? info.PlayerId : byte.MaxValue);
		AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
	public void RpcMurderPlayer(PlayerControl target)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.MurderPlayer(target);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 12, SendOption.Reliable, -1);
		messageWriter.WriteNetObject(target);
		AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0003E93F File Offset: 0x0003CB3F
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		if (initialState)
		{
			writer.Write(this.isNew);
		}
		writer.Write(this.PlayerId);
		return true;
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0003E95D File Offset: 0x0003CB5D
	public override void Deserialize(MessageReader reader, bool initialState)
	{
		if (initialState)
		{
			this.isNew = reader.ReadBoolean();
		}
		this.PlayerId = reader.ReadByte();
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0003E97C File Offset: 0x0003CB7C
	public override void HandleRpc(byte callId, MessageReader reader)
	{
		switch (callId)
		{
		case 0:
			this.PlayAnimation(reader.ReadByte());
			return;
		case 1:
			this.CompleteTask(reader.ReadPackedUInt32());
			return;
		case 2:
			PlayerControl.GameOptions = GameOptionsData.FromBytes(reader.ReadBytesAndSize());
			return;
		case 3:
			this.SetInfected(reader.ReadBytesAndSize());
			return;
		case 4:
			this.Exiled();
			return;
		case 5:
			this.CheckName(reader.ReadString());
			return;
		case 6:
			this.SetName(reader.ReadString(), false);
			return;
		case 7:
			this.CheckColor(reader.ReadByte());
			return;
		case 8:
			this.SetColor((int)reader.ReadByte());
			return;
		case 9:
		{
			GameData.PlayerInfo data = this.Data;
			int colorId = (data != null) ? data.ColorId : 0;
			this.SetHat(reader.ReadPackedUInt32(), colorId);
			return;
		}
		case 10:
			this.SetSkin(reader.ReadPackedUInt32());
			return;
		case 11:
		{
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(reader.ReadByte());
			this.ReportDeadBody(playerById);
			return;
		}
		case 12:
		{
			PlayerControl target = reader.ReadNetObject<PlayerControl>();
			this.MurderPlayer(target);
			return;
		}
		case 13:
		{
			string chatText = reader.ReadString();
			if (DestroyableSingleton<HudManager>.Instance)
			{
				DestroyableSingleton<HudManager>.Instance.Chat.AddChat(this, chatText);
				return;
			}
			break;
		}
		case 14:
		{
			GameData.PlayerInfo playerById2 = GameData.Instance.GetPlayerById(reader.ReadByte());
			base.StartCoroutine(this.CoStartMeeting(playerById2));
			return;
		}
		case 15:
			this.SetScanner(reader.ReadBoolean(), reader.ReadByte());
			break;
		case 16:
		{
			GameData.PlayerInfo playerById3 = GameData.Instance.GetPlayerById(reader.ReadByte());
			DestroyableSingleton<HudManager>.Instance.Chat.AddChatNote(playerById3, (ChatNoteTypes)reader.ReadByte());
			return;
		}
		case 17:
			this.SetPet(reader.ReadPackedUInt32());
			return;
		case 18:
		{
			int num = reader.ReadPackedInt32();
			sbyte startCounter = reader.ReadSByte();
			if (DestroyableSingleton<GameStartManager>.InstanceExists && this.LastStartCounter < num)
			{
				this.LastStartCounter = num;
				DestroyableSingleton<GameStartManager>.Instance.SetStartCounter(startCounter);
				return;
			}
			break;
		}
		case 19:
		case 20:
		case 21:
		case 22:
		case 23:
		case 24:
		case 25:
		case 26:
		case 27:
		case 28:
		case 29:
		case 30:
		case 31:
			break;
		case 32:
			if (AmongUsClient.Instance.AmHost)
			{
				AirshipStatus airshipStatus = ShipStatus.Instance as AirshipStatus;
				if (airshipStatus)
				{
					airshipStatus.GapPlatform.Use(this);
					base.SetDirtyBit(4096U);
					return;
				}
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060009AF RID: 2479 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
	public bool CanMove
	{
		get
		{
			return this.moveable && !Minigame.Instance && (!DestroyableSingleton<HudManager>.InstanceExists || (!DestroyableSingleton<HudManager>.Instance.Chat.IsOpen && !DestroyableSingleton<HudManager>.Instance.KillOverlay.IsOpen && !DestroyableSingleton<HudManager>.Instance.GameMenu.IsOpen)) && (!ControllerManager.Instance || !ControllerManager.Instance.IsUiControllerActive) && (!MapBehaviour.Instance || !MapBehaviour.Instance.IsOpenStopped) && !MeetingHud.Instance && !CustomPlayerMenu.Instance && !ExileController.Instance && !IntroCutscene.Instance;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0003ECB7 File Offset: 0x0003CEB7
	public GameData.PlayerInfo Data
	{
		get
		{
			if (this._cachedData == null)
			{
				if (!GameData.Instance)
				{
					return null;
				}
				this._cachedData = GameData.Instance.GetPlayerById(this.PlayerId);
			}
			return this._cachedData;
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0003ECEC File Offset: 0x0003CEEC
	public void SetKillTimer(float time)
	{
		if (PlayerControl.GameOptions.KillCooldown <= 0f)
		{
			return;
		}
		this.killTimer = Mathf.Clamp(time, 0f, PlayerControl.GameOptions.KillCooldown);
		DestroyableSingleton<HudManager>.Instance.KillButton.SetCoolDown(this.killTimer, PlayerControl.GameOptions.KillCooldown);
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0003ED45 File Offset: 0x0003CF45
	// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0003ED54 File Offset: 0x0003CF54
	public bool Visible
	{
		get
		{
			return this.myRend.enabled;
		}
		set
		{
			this.myRend.enabled = value;
			this.MyPhysics.Skin.Visible = value;
			this.HatRenderer.gameObject.SetActive(value);
			if (this.CurrentPet)
			{
				// this.CurrentPet.Visible = value;
			}
			this.nameText.gameObject.SetActive(value);
		}
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0003EDBC File Offset: 0x0003CFBC
	private void Awake()
	{
		this.myRend = base.GetComponent<SpriteRenderer>();
		this.MyPhysics = base.GetComponent<PlayerPhysics>();
		this.NetTransform = base.GetComponent<CustomNetworkTransform>();
		this.Collider = base.GetComponent<Collider2D>();
		if (!this.notRealPlayer)
		{
			PlayerControl.AllPlayerControls.Add(this);
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0003EE0C File Offset: 0x0003D00C
	private IEnumerator Start()
	{
		while (this.PlayerId == 255)
		{
			yield return null;
		}
		this.RemainingEmergencies = PlayerControl.GameOptions.NumEmergencyMeetings;
		if (base.AmOwner)
		{
			this.myLight = UnityEngine.Object.Instantiate<LightSource>(this.LightPrefab);
			this.myLight.transform.SetParent(base.transform);
			this.myLight.transform.localPosition = this.Collider.offset;
			PlayerControl.LocalPlayer = this;
			Camera.main.GetComponent<FollowerCamera>().SetTarget(this);
			this.SetName(SaveManager.PlayerName, false);
			this.SetColor((int)SaveManager.BodyColor);
			if (Application.targetFrameRate > 30)
			{
				this.MyPhysics.EnableInterpolation();
			}
			this.CmdCheckName(SaveManager.PlayerName);
			this.CmdCheckColor(SaveManager.BodyColor);
			this.RpcSetPet(SaveManager.LastPet);
			this.RpcSetHat(SaveManager.LastHat);
			this.RpcSetSkin(SaveManager.LastSkin);
			yield return null;
			this.UpdatePlatformIcon();
		}
		else
		{
			base.StartCoroutine(this.ClientInitialize());
		}
		if (this.isNew)
		{
			this.isNew = false;
			base.StartCoroutine(this.MyPhysics.CoSpawnPlayer(LobbyBehaviour.Instance));
		}
		yield break;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0003EE1B File Offset: 0x0003D01B
	private IEnumerator ClientInitialize()
	{
		this.Visible = false;
		while (!GameData.Instance || this.Data == null || this.Data.IsIncomplete)
		{
			yield return null;
		}
		this.SetName(this.Data.PlayerName, this.isDummy);
		this.SetColor(this.Data.ColorId);
		this.SetHat(this.Data.HatId, this.Data.ColorId);
		this.SetSkin(this.Data.SkinId);
		this.SetPet(this.Data.PetId);
		this.Visible = true;
		yield return null;
		this.UpdatePlatformIcon();
		yield break;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0003EE2A File Offset: 0x0003D02A
	public override void OnDestroy()
	{
		if (this.CurrentPet)
		{
            UnityEngine.Object.Destroy(this.CurrentPet.gameObject);
		}
		if (!this.notRealPlayer)
		{
			PlayerControl.AllPlayerControls.Remove(this);
		}
		base.OnDestroy();
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0003EE64 File Offset: 0x0003D064
	private void FixedUpdate()
	{
		if (!GameData.Instance)
		{
			return;
		}
		GameData.PlayerInfo data = this.Data;
		if (data == null)
		{
			return;
		}
		if (data.IsDead && PlayerControl.LocalPlayer && PlayerControl.LocalPlayer.Data != null)
		{
			this.Visible = PlayerControl.LocalPlayer.Data.IsDead;
		}
		if (base.AmOwner)
		{
			if (ShipStatus.Instance)
			{
				this.myLight.LightRadius = ShipStatus.Instance.CalculateLightRadius(data);
			}
			if (data.IsImpostor && this.CanMove && !data.IsDead)
			{
				this.SetKillTimer(this.killTimer - Time.fixedDeltaTime);
				PlayerControl target = this.FindClosestTarget();
				DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(target);
			}
			else
			{
				DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
			}
			if (this.CanMove || this.inVent)
			{
				this.newItemsInRange.Clear();
				bool flag = (PlayerControl.GameOptions.GhostsDoTasks || !data.IsDead) && (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) && this.CanMove;
				Vector2 truePosition = this.GetTruePosition();
				int num = Physics2D.OverlapCircleNonAlloc(truePosition, this.MaxReportDistance, this.hitBuffer, Constants.Usables);
				IUsable usable = null;
				float num2 = float.MaxValue;
				bool flag2 = false;
				for (int i = 0; i < num; i++)
				{
					Collider2D collider2D = this.hitBuffer[i];
					IUsable usable2;
					if (!this.cache.TryGetValue(collider2D, out usable2))
					{
						usable2 = (this.cache[collider2D] = collider2D.GetComponent<IUsable>());
					}
					if (usable2 != null && (flag || this.inVent))
					{
						bool flag3;
						bool flag4;
						float num3 = usable2.CanUse(data, out flag3, out flag4);
						if (flag3 || flag4)
						{
							this.newItemsInRange.Add(usable2);
						}
						if (flag3 && num3 < num2)
						{
							num2 = num3;
							usable = usable2;
						}
					}
					if (flag && !data.IsDead && !flag2 && collider2D.tag == "DeadBody")
					{
						DeadBody component = collider2D.GetComponent<DeadBody>();
						if (component.enabled && Vector2.Distance(truePosition, component.TruePosition) <= this.MaxReportDistance && !PhysicsHelpers.AnythingBetween(truePosition, component.TruePosition, Constants.ShipAndObjectsMask, false))
						{
							flag2 = true;
						}
					}
				}
				for (int l = this.itemsInRange.Count - 1; l > -1; l--)
				{
					IUsable item = this.itemsInRange[l];
					int num4 = this.newItemsInRange.FindIndex((IUsable j) => j == item);
					if (num4 == -1)
					{
						item.SetOutline(false, false);
						this.itemsInRange.RemoveAt(l);
					}
					else
					{
						this.newItemsInRange.RemoveAt(num4);
						item.SetOutline(true, usable == item);
					}
				}
				for (int k = 0; k < this.newItemsInRange.Count; k++)
				{
					IUsable usable3 = this.newItemsInRange[k];
					usable3.SetOutline(true, usable == usable3);
					this.itemsInRange.Add(usable3);
				}
				this.closest = usable;
				DestroyableSingleton<HudManager>.Instance.UseButton.SetTarget(usable);
				DestroyableSingleton<HudManager>.Instance.ReportButton.SetActive(flag2);
				return;
			}
			this.closest = null;
			DestroyableSingleton<HudManager>.Instance.UseButton.SetTarget(null);
			DestroyableSingleton<HudManager>.Instance.ReportButton.SetActive(false);
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0003F1E5 File Offset: 0x0003D3E5
	public void UseClosest()
	{
		if (this.closest != null)
		{
			this.closest.Use();
		}
		this.closest = null;
		DestroyableSingleton<HudManager>.Instance.UseButton.SetTarget(null);
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0003F214 File Offset: 0x0003D414
	public void ReportClosest()
	{
		if (AmongUsClient.Instance.IsGameOver)
		{
			return;
		}
		if (PlayerControl.LocalPlayer.Data.IsDead)
		{
			return;
		}
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(this.GetTruePosition(), this.MaxReportDistance, Constants.PlayersOnlyMask))
		{
			if (!(collider2D.tag != "DeadBody"))
			{
				DeadBody component = collider2D.GetComponent<DeadBody>();
				if (component && !component.Reported)
				{
					component.OnClick();
					if (component.Reported)
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
	public void PlayStepSound()
	{
		if (!Constants.ShouldPlaySfx())
		{
			return;
		}
		if (PlayerControl.LocalPlayer != this)
		{
			return;
		}
		if (LobbyBehaviour.Instance)
		{
			for (int i = 0; i < LobbyBehaviour.Instance.AllRooms.Length; i++)
			{
				SoundGroup soundGroup = LobbyBehaviour.Instance.AllRooms[i].MakeFootstep(this);
				if (soundGroup)
				{
					AudioClip clip = soundGroup.Random();
					this.FootSteps.clip = clip;
					this.FootSteps.Play();
					break;
				}
			}
		}
		if (!ShipStatus.Instance)
		{
			return;
		}
		for (int j = 0; j < ShipStatus.Instance.AllStepWatchers.Length; j++)
		{
			SoundGroup soundGroup2 = ShipStatus.Instance.AllStepWatchers[j].MakeFootstep(this);
			if (soundGroup2)
			{
				AudioClip clip2 = soundGroup2.Random();
				this.FootSteps.clip = clip2;
				this.FootSteps.Play();
				return;
			}
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0003F384 File Offset: 0x0003D584
	private void SetScanner(bool on, byte cnt)
	{
		if (cnt < this.scannerCount)
		{
			return;
		}
		this.scannerCount = cnt;
		for (int i = 0; i < this.ScannerAnims.Length; i++)
		{
			SpriteAnim spriteAnim = this.ScannerAnims[i];
			if (on && !this.Data.IsDead)
			{
				spriteAnim.gameObject.SetActive(true);
				spriteAnim.Play(null, 1f);
				this.ScannersImages[i].flipX = !this.myRend.flipX;
			}
			else
			{
				if (spriteAnim.isActiveAndEnabled)
				{
					spriteAnim.Stop();
				}
				spriteAnim.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0003F41D File Offset: 0x0003D61D
	public Vector2 GetTruePosition()
	{
		return base.transform.position;// + this.Collider.offset;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0003F440 File Offset: 0x0003D640
	private PlayerControl FindClosestTarget()
	{
		PlayerControl result = null;
		float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
		if (!ShipStatus.Instance)
		{
			return null;
		}
		Vector2 truePosition = this.GetTruePosition();
		List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
		for (int i = 0; i < allPlayers.Count; i++)
		{
			GameData.PlayerInfo playerInfo = allPlayers[i];
			if (!playerInfo.Disconnected && playerInfo.PlayerId != this.PlayerId && !playerInfo.IsDead && !playerInfo.IsImpostor)
			{
				PlayerControl @object = playerInfo.Object;
				if (@object && @object.Collider.enabled)
				{
					Vector2 vector = @object.GetTruePosition() - truePosition;
					float magnitude = vector.magnitude;
					if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
					{
						result = @object;
						num = magnitude;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0003F530 File Offset: 0x0003D730
	public void SetTasks(List<GameData.TaskInfo> tasks)
	{
		base.StartCoroutine(this.CoSetTasks(tasks));
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0003F540 File Offset: 0x0003D740
	private IEnumerator CoSetTasks(List<GameData.TaskInfo> tasks)
	{
		while (!ShipStatus.Instance)
		{
			yield return null;
		}
		if (base.AmOwner)
		{
			DestroyableSingleton<HudManager>.Instance.TaskStuff.SetActive(true);
			StatsManager instance = StatsManager.Instance;
			uint num = instance.GamesStarted;
			instance.GamesStarted = num + 1U;
			if (this.Data.IsImpostor)
			{
				StatsManager instance2 = StatsManager.Instance;
				num = instance2.TimesImpostor;
				instance2.TimesImpostor = num + 1U;
				StatsManager.Instance.CrewmateStreak = 0U;
			}
			else
			{
				StatsManager instance3 = StatsManager.Instance;
				num = instance3.TimesCrewmate;
				instance3.TimesCrewmate = num + 1U;
				StatsManager instance4 = StatsManager.Instance;
				num = instance4.CrewmateStreak;
				instance4.CrewmateStreak = num + 1U;
				DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
			}
		}
		this.myTasks.DestroyAll<PlayerTask>();
		if (this.Data.IsImpostor)
		{
			ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
			importantTextTask.transform.SetParent(PlayerControl.LocalPlayer.transform, false);
			importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ImpostorTask, Array.Empty<object>()) + "\r\n[FFFFFFFF]" + DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.FakeTasks, Array.Empty<object>());
			this.myTasks.Insert(0, importantTextTask);
		}
		for (int i = 0; i < tasks.Count; i++)
		{
			GameData.TaskInfo taskInfo = tasks[i];
			NormalPlayerTask normalPlayerTask = Object.Instantiate<NormalPlayerTask>(ShipStatus.Instance.GetTaskById(taskInfo.TypeId), base.transform);
			normalPlayerTask.Id = taskInfo.Id;
			normalPlayerTask.Owner = this;
			normalPlayerTask.Initialize();
			this.myTasks.Add(normalPlayerTask);
		}
		yield break;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0003F558 File Offset: 0x0003D758
	public void AddSystemTask(SystemTypes system)
	{
		PlayerTask playerTask;
		if (system <= SystemTypes.Electrical)
		{
			if (system != SystemTypes.Reactor)
			{
				if (system != SystemTypes.Electrical)
				{
					return;
				}
				playerTask = ShipStatus.Instance.SpecialTasks[1];
			}
			else
			{
				playerTask = ShipStatus.Instance.SpecialTasks[0];
			}
		}
		else if (system != SystemTypes.LifeSupp)
		{
			if (system != SystemTypes.Comms)
			{
				if (system != SystemTypes.Laboratory)
				{
					return;
				}
				playerTask = ShipStatus.Instance.SpecialTasks[4];
			}
			else
			{
				playerTask = ShipStatus.Instance.SpecialTasks[2];
			}
		}
		else
		{
			playerTask = ShipStatus.Instance.SpecialTasks[3];
		}
		PlayerControl localPlayer = PlayerControl.LocalPlayer;
		PlayerTask playerTask2 = Object.Instantiate<PlayerTask>(playerTask, localPlayer.transform);
		playerTask2.Id = 255U;
		playerTask2.Owner = localPlayer;
		playerTask2.Initialize();
		localPlayer.myTasks.Add(playerTask2);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0003F604 File Offset: 0x0003D804
	public void RemoveTask(PlayerTask task)
	{
		task.OnRemove();
		this.myTasks.Remove(task);
		GameData.Instance.TutOnlyRemoveTask(this.PlayerId, task.Id);
		DestroyableSingleton<HudManager>.Instance.UseButton.SetTarget(null);
		Object.Destroy(task.gameObject);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003F658 File Offset: 0x0003D858
	private void ClearTasks()
	{
		for (int i = 0; i < this.myTasks.Count; i++)
		{
			PlayerTask playerTask = this.myTasks[i];
			playerTask.OnRemove();
			Object.Destroy(playerTask.gameObject);
		}
		this.myTasks.Clear();
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0003F6A4 File Offset: 0x0003D8A4
	public void RemoveInfected()
	{
		GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(this.PlayerId);
		if (playerById.IsImpostor)
		{
			playerById.Object.nameText.Color = Color.white;
			playerById.IsImpostor = false;
			this.myTasks.RemoveAt(0);
			DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003F708 File Offset: 0x0003D908
	public void Die(DeathReason reason)
	{
		if (!DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			StatsManager.Instance.LastGameStarted = DateTime.MinValue;
			StatsManager instance = StatsManager.Instance;
			float banPoints = instance.BanPoints;
			instance.BanPoints = banPoints - 1f;
		}
		TempData.LastDeathReason = reason;
		if (this.CurrentPet)
		{
			// this.CurrentPet.SetMourning();
		}
		this.Data.IsDead = true;
		base.gameObject.layer = LayerMask.NameToLayer("Ghost");
		this.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
		if (base.AmOwner)
		{
			DestroyableSingleton<HudManager>.Instance.Chat.SetVisible(true);
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0003F7B8 File Offset: 0x0003D9B8
	public void Revive()
	{
		this.Data.IsDead = false;
		base.gameObject.layer = LayerMask.NameToLayer("Players");
		this.MyPhysics.ResetMoveState(true);
		if (this.CurrentPet)
		{
			this.CurrentPet.Source = this;
		}
		this.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 4);
		if (base.AmOwner)
		{
			DestroyableSingleton<HudManager>.Instance.ShadowQuad.gameObject.SetActive(true);
			DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(this.Data.IsImpostor);
			DestroyableSingleton<HudManager>.Instance.Chat.ForceClosed();
			DestroyableSingleton<HudManager>.Instance.Chat.SetVisible(false);
		}
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0003F884 File Offset: 0x0003DA84
	public void PlayAnimation(byte animType)
	{
		if (animType == 1)
		{
			ShipStatus.Instance.StartShields();
			return;
		}
		if (animType == 6)
		{
			ShipStatus.Instance.FireWeapon();
			return;
		}
		if (animType - 9 > 1)
		{
			return;
		}
		ShipStatus.Instance.OpenHatch();
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0003F8C4 File Offset: 0x0003DAC4
	public void CompleteTask(uint idx)
	{
		PlayerTask playerTask = this.myTasks.Find((PlayerTask p) => p.Id == idx);
		if (playerTask)
		{
			GameData.Instance.CompleteTask(this, idx);
			playerTask.Complete();
			DestroyableSingleton<Telemetry>.Instance.WriteCompleteTask(this.PlayerId, playerTask.TaskType);
			return;
		}
		Debug.LogWarning(this.PlayerId.ToString() + ": Server didn't have task: " + idx.ToString());
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0003F954 File Offset: 0x0003DB54
	private void SetInfected(byte[] infected)
	{
		if (!DestroyableSingleton<TutorialManager>.InstanceExists && this.infectedSet)
		{
			return;
		}
		this.infectedSet = true;
		StatsManager instance = StatsManager.Instance;
		float banPoints = instance.BanPoints;
		instance.BanPoints = banPoints + 1f;
		StatsManager.Instance.LastGameStarted = DateTime.UtcNow;
		for (int i = 0; i < infected.Length; i++)
		{
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(infected[i]);
			if (playerById != null)
			{
				playerById.IsImpostor = true;
			}
		}
		DestroyableSingleton<HudManager>.Instance.MapButton.gameObject.SetActive(true);
		DestroyableSingleton<HudManager>.Instance.ReportButton.gameObject.SetActive(true);
		DestroyableSingleton<HudManager>.Instance.UseButton.gameObject.SetActive(true);
		PlayerControl.LocalPlayer.RemainingEmergencies = PlayerControl.GameOptions.NumEmergencyMeetings;
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		if (data.IsImpostor)
		{
			ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
			importantTextTask.transform.SetParent(PlayerControl.LocalPlayer.transform, false);
			importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ImpostorTask, Array.Empty<object>()) + "\r\n[FFFFFFFF]" + DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.FakeTasks, Array.Empty<object>());
			this.myTasks.Insert(0, importantTextTask);
			DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(true);
			PlayerControl.LocalPlayer.SetKillTimer(10f);
			for (int j = 0; j < infected.Length; j++)
			{
				GameData.PlayerInfo playerById2 = GameData.Instance.GetPlayerById(infected[j]);
				if (playerById2 != null)
				{
					playerById2.Object.nameText.Color = Palette.ImpostorRed;
				}
			}
		}
		if (!DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			List<PlayerControl> yourTeam;
			if (data.IsImpostor)
			{
				yourTeam = (from pcd in GameData.Instance.AllPlayers
				where !pcd.Disconnected
				where pcd.IsImpostor
				select pcd.Object).OrderBy(delegate(PlayerControl pc)
				{
					if (!(pc == PlayerControl.LocalPlayer))
					{
						return 1;
					}
					return 0;
				}).ToList<PlayerControl>();
			}
			else
			{
				yourTeam = (from pcd in GameData.Instance.AllPlayers
				where !pcd.Disconnected
				select pcd.Object).OrderBy(delegate(PlayerControl pc)
				{
					if (!(pc == PlayerControl.LocalPlayer))
					{
						return 1;
					}
					return 0;
				}).ToList<PlayerControl>();
			}
			base.StopAllCoroutines();
			DestroyableSingleton<HudManager>.Instance.StartCoroutine(DestroyableSingleton<HudManager>.Instance.CoShowIntro(yourTeam));
		}
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0003FC4C File Offset: 0x0003DE4C
	public void Exiled()
	{
		this.Die(DeathReason.Exile);
		if (base.AmOwner)
		{
			StatsManager instance = StatsManager.Instance;
			uint timesEjected = instance.TimesEjected;
			instance.TimesEjected = timesEjected + 1U;
			DestroyableSingleton<HudManager>.Instance.ShadowQuad.gameObject.SetActive(false);
			ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
			importantTextTask.transform.SetParent(base.transform, false);
			if (this.Data.IsImpostor)
			{
				this.ClearTasks();
				importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostImpostor, Array.Empty<object>());
			}
			else if (!PlayerControl.GameOptions.GhostsDoTasks)
			{
				this.ClearTasks();
				importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostIgnoreTasks, Array.Empty<object>());
			}
			else
			{
				importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostDoTasks, Array.Empty<object>());
			}
			this.myTasks.Insert(0, importantTextTask);
		}
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0003FD30 File Offset: 0x0003DF30
	public void CheckName(string name)
	{
		List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
		bool flag = allPlayers.Any((GameData.PlayerInfo i) => i.PlayerId != this.PlayerId && i.PlayerName.Equals(name, StringComparison.OrdinalIgnoreCase));
		if (flag)
		{
			for (int k = 1; k < 100; k++)
			{
				string text = name + " " + k.ToString();
				flag = false;
				for (int j = 0; j < allPlayers.Count; j++)
				{
					if (allPlayers[j].PlayerId != this.PlayerId && allPlayers[j].PlayerName.Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					name = text;
					break;
				}
			}
		}
		this.RpcSetName(name);
		GameData.Instance.UpdateName(this.PlayerId, name, false);
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0003FE10 File Offset: 0x0003E010
	public void SetName(string name, bool dontCensor = false)
	{
		if (GameData.Instance)
		{
			GameData.Instance.UpdateName(this.PlayerId, name, dontCensor);
		}
		base.gameObject.name = name;
		if (name == "")
		{
			this.nameText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.PlayerName, Array.Empty<object>());
		}
		else
		{
			this.nameText.Text = name;
			this.textTranslator.defaultStr = name;
		}
		this.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 4);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0003FEA8 File Offset: 0x0003E0A8
	public void CheckColor(byte bodyColor)
	{
		List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
		int num = 0;
		while (num++ < 100 && ((int)bodyColor >= Palette.PlayerColors.Length || allPlayers.Any((GameData.PlayerInfo p) => !p.Disconnected && p.PlayerId != this.PlayerId && p.ColorId == (int)bodyColor)))
		{
			bodyColor = (byte)((int)(bodyColor + 1) % Palette.PlayerColors.Length);
		}
		this.RpcSetColor(bodyColor);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0003FF28 File Offset: 0x0003E128
	public void SetHatAlpha(float a)
	{
		Color white = Color.white;
		white.a = a;
		this.HatRenderer.color = white;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0003FF50 File Offset: 0x0003E150
	public void SetColor(int bodyColor)
	{
		if (GameData.Instance)
		{
			GameData.Instance.UpdateColor(this.PlayerId, bodyColor);
		}
		if (this.myRend == null)
		{
			base.GetComponent<SpriteRenderer>();
		}
		PlayerControl.SetPlayerMaterialColors(bodyColor, this.myRend);
		this.HatRenderer.SetColor(bodyColor);
		if (this.CurrentPet)
		{
			PlayerControl.SetPlayerMaterialColors(bodyColor, this.CurrentPet.rend);
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0003FFBF File Offset: 0x0003E1BF
	public void SetSkin(uint skinId)
	{
		if (GameData.Instance)
		{
			GameData.Instance.UpdateSkin(this.PlayerId, skinId);
		}
		this.MyPhysics.SetSkin(skinId);
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0003FFEC File Offset: 0x0003E1EC
	public void SetHat(uint hatId, int colorId)
	{
		if (hatId == 4294967295U)
		{
			return;
		}
		if (GameData.Instance)
		{
			GameData.Instance.UpdateHat(this.PlayerId, hatId);
		}
		this.HatRenderer.SetHat(hatId, colorId);
		this.nameText.transform.localPosition = new Vector3(0f, (hatId == 0U) ? 0.7f : 1.05f, -0.5f);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x00040058 File Offset: 0x0003E258
	public void SetPet(uint petId)
	{
		if (this.CurrentPet)
		{
			Object.Destroy(this.CurrentPet.gameObject);
		}
		this.CurrentPet = Object.Instantiate<PetBehaviour>(DestroyableSingleton<HatManager>.Instance.GetPetById(petId));
		this.CurrentPet.transform.position = base.transform.position;
		this.CurrentPet.Source = this;
		GameData.PlayerInfo data = this.Data;
		if (this.Data != null)
		{
			GameData.Instance.UpdatePet(this.PlayerId, petId);
			this.Data.PetId = petId;
			PlayerControl.SetPlayerMaterialColors(this.Data.ColorId, this.CurrentPet.rend);
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00040106 File Offset: 0x0003E306
	public static void SetPetImage(uint petId, int colorId, SpriteRenderer target)
	{
		if (!DestroyableSingleton<HatManager>.InstanceExists)
		{
			return;
		}
		PlayerControl.SetPetImage(DestroyableSingleton<HatManager>.Instance.GetPetById(petId), colorId, target);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00040122 File Offset: 0x0003E322
	public static void SetPetImage(PetBehaviour pet, int colorId, SpriteRenderer target)
	{
		target.sprite = pet.rend.sprite;
		if (target != pet.rend)
		{
			target.material = new Material(pet.rend.sharedMaterial);
			PlayerControl.SetPlayerMaterialColors(colorId, target);
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00040160 File Offset: 0x0003E360
	public static void SetSkinImage(uint skinId, SpriteRenderer target)
	{
		if (!DestroyableSingleton<HatManager>.InstanceExists)
		{
			return;
		}
		PlayerControl.SetSkinImage(DestroyableSingleton<HatManager>.Instance.GetSkinById(skinId), target);
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0004017B File Offset: 0x0003E37B
	public static void SetSkinImage(SkinData skin, SpriteRenderer target)
	{
		target.sprite = skin.IdleFrame;
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0004018C File Offset: 0x0003E38C
	private void ReportDeadBody(GameData.PlayerInfo target)
	{
		if (AmongUsClient.Instance.IsGameOver)
		{
			return;
		}
		if (MeetingHud.Instance)
		{
			return;
		}
		if (target == null && PlayerControl.LocalPlayer.myTasks.Any(new Func<PlayerTask, bool>(PlayerTask.TaskIsEmergency)))
		{
			return;
		}
		if (this.Data.IsDead)
		{
			return;
		}
		MeetingRoomManager.Instance.AssignSelf(this, target);
		if (!AmongUsClient.Instance.AmHost)
		{
			return;
		}
		if (ShipStatus.Instance.CheckTaskCompletion())
		{
			return;
		}
		DestroyableSingleton<HudManager>.Instance.OpenMeetingRoom(this);
		this.RpcStartMeeting(target);
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0004021A File Offset: 0x0003E41A
	public IEnumerator CoStartMeeting(GameData.PlayerInfo target)
	{
		DestroyableSingleton<Telemetry>.Instance.WriteMeetingStarted(target == null);
		while (!MeetingHud.Instance)
		{
			yield return null;
		}
		MeetingRoomManager.Instance.RemoveSelf();
		for (int i = 0; i < PlayerControl.AllPlayerControls.Count; i++)
		{
			PlayerControl playerControl = PlayerControl.AllPlayerControls[i];
			if (!playerControl.GetComponent<DummyBehaviour>().enabled)
			{
				playerControl.MyPhysics.ExitAllVents();
				ShipStatus.Instance.SpawnPlayer(playerControl, GameData.Instance.PlayerCount, false);
			}
			playerControl.NetTransform.enabled = true;
			playerControl.MyPhysics.ResetMoveState(true);
		}
		if (base.AmOwner)
		{
			if (target != null)
			{
				StatsManager instance = StatsManager.Instance;
				uint num = instance.BodiesReported;
				instance.BodiesReported = num + 1U;
			}
			else
			{
				this.RemainingEmergencies--;
				StatsManager instance2 = StatsManager.Instance;
				uint num = instance2.EmergenciesCalled;
				instance2.EmergenciesCalled = num + 1U;
			}
		}
		if (MapBehaviour.Instance)
		{
			MapBehaviour.Instance.Close();
		}
		if (Minigame.Instance)
		{
			Minigame.Instance.ForceClose();
		}
		ShipStatus.Instance.OnMeetingCalled();
		KillAnimation.SetMovement(this, true);
		MeetingHud.Instance.StartCoroutine(MeetingHud.Instance.CoIntro(this.Data, target));
		DeadBody[] array = Object.FindObjectsOfType<DeadBody>();
		for (int j = 0; j < array.Length; j++)
		{
			Object.Destroy(array[j].gameObject);
		}
		yield break;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00040230 File Offset: 0x0003E430
	public void MurderPlayer(PlayerControl target)
	{
		if (AmongUsClient.Instance.IsGameOver)
		{
			return;
		}
		if (!target || this.Data.IsDead || !this.Data.IsImpostor || this.Data.Disconnected)
		{
			int num = target ? ((int)target.PlayerId) : -1;
			Debug.LogWarning(string.Format("Bad kill from {0} to {1}", this.PlayerId, num));
			return;
		}
		GameData.PlayerInfo data = target.Data;
		if (data == null || data.IsDead)
		{
			Debug.LogWarning("Missing target data for kill");
			return;
		}
		if (base.AmOwner)
		{
			StatsManager instance = StatsManager.Instance;
			uint num2 = instance.ImpostorKills;
			instance.ImpostorKills = num2 + 1U;
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.KillSfx, false, 0.8f);
			}
			this.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
		}
		DestroyableSingleton<Telemetry>.Instance.WriteMurder();
		target.gameObject.layer = LayerMask.NameToLayer("Ghost");
		if (target.AmOwner)
		{
			StatsManager instance2 = StatsManager.Instance;
			uint num2 = instance2.TimesMurdered;
			instance2.TimesMurdered = num2 + 1U;
			if (Minigame.Instance)
			{
				try
				{
					Minigame.Instance.Close();
					Minigame.Instance.Close();
				}
				catch
				{
				}
			}
			DestroyableSingleton<HudManager>.Instance.KillOverlay.ShowOne(this.Data, data);
			DestroyableSingleton<HudManager>.Instance.ShadowQuad.gameObject.SetActive(false);
			target.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
			target.RpcSetScanner(false);
			ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
			importantTextTask.transform.SetParent(base.transform, false);
			if (!PlayerControl.GameOptions.GhostsDoTasks)
			{
				target.ClearTasks();
				importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostIgnoreTasks, Array.Empty<object>());
			}
			else
			{
				importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostDoTasks, Array.Empty<object>());
			}
			target.myTasks.Insert(0, importantTextTask);
		}
		this.MyPhysics.StartCoroutine(this.KillAnimations.Random<KillAnimation>().CoPerformKill(this, target));
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00040460 File Offset: 0x0003E660
	public void SetPlayerMaterialColors(Renderer rend)
	{
		GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(this.PlayerId);
		PlayerControl.SetPlayerMaterialColors((playerById != null) ? playerById.ColorId : 0, rend);
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00040484 File Offset: 0x0003E684
	public static void SetPlayerMaterialColors(int colorId, Renderer rend)
	{
		if (!rend || colorId < 0 || colorId >= Palette.PlayerColors.Length)
		{
			return;
		}
		rend.material.SetColor("_BackColor", Palette.ShadowColors[colorId]);
		rend.material.SetColor("_BodyColor", Palette.PlayerColors[colorId]);
		rend.material.SetColor("_VisorColor", Palette.VisorColor);
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x00040504 File Offset: 0x0003E704
	public static void SetPlayerMaterialColors(Color color, Renderer rend)
	{
		if (!rend)
		{
			return;
		}
		rend.material.SetColor("_BackColor", color);
		rend.material.SetColor("_BodyColor", color);
		rend.material.SetColor("_VisorColor", Palette.VisorColor);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00040556 File Offset: 0x0003E756
	public static void HideCursorTemporarily()
	{
		if (PlayerControl.LocalPlayer.AmOwner)
		{
			PlayerControl.LocalPlayer.MyPhysics.inputHandler.enabled = true;
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0004057C File Offset: 0x0003E77C
	public void SetAppearanceFromSaveData()
	{
		this.MyPhysics.ResetMoveState(true);
		this.SetName(SaveManager.PlayerName, false);
		this.SetColor((int)SaveManager.BodyColor);
		this.SetHat(SaveManager.LastHat, (int)SaveManager.BodyColor);
		this.SetSkin(SaveManager.LastSkin);
		this.SetPet(SaveManager.LastPet);
		SpriteAnimNodeSync[] componentsInChildren = base.GetComponentsInChildren<SpriteAnimNodeSync>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].LateUpdate();
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x000405F0 File Offset: 0x0003E7F0
	public void UpdatePlatformIcon()
	{
		//PlatformIdentifierIcon componentInChildren = base.GetComponentInChildren<PlatformIdentifierIcon>();
		//ClientData client = AmongUsClient.Instance.GetClient(this.OwnerId);
		//if (client != null && client.platformID != -1)
		//{
		//	componentInChildren.SetIcon(client.platformID);
		//}
	}

	// Token: 0x04000B20 RID: 2848
	private int LastStartCounter;

	// Token: 0x04000B21 RID: 2849
	public byte PlayerId = byte.MaxValue;

	// Token: 0x04000B22 RID: 2850
	public float MaxReportDistance = 5f;

	// Token: 0x04000B23 RID: 2851
	public bool moveable = true;

	// Token: 0x04000B24 RID: 2852
	public bool inVent;

	// Token: 0x04000B25 RID: 2853
	public static PlayerControl LocalPlayer;

	// Token: 0x04000B26 RID: 2854
	private GameData.PlayerInfo _cachedData;

	// Token: 0x04000B27 RID: 2855
	public AudioSource FootSteps;

	// Token: 0x04000B28 RID: 2856
	public AudioClip KillSfx;

	// Token: 0x04000B29 RID: 2857
	public KillAnimation[] KillAnimations;

	// Token: 0x04000B2A RID: 2858
	[SerializeField]
	private float killTimer;

	// Token: 0x04000B2B RID: 2859
	public int RemainingEmergencies;

	// Token: 0x04000B2C RID: 2860
	public TextRenderer nameText;

	// Token: 0x04000B2D RID: 2861
	public LightSource LightPrefab;

	// Token: 0x04000B2E RID: 2862
	private LightSource myLight;

	// Token: 0x04000B2F RID: 2863
	public TextTranslator textTranslator;

	// Token: 0x04000B30 RID: 2864
	[HideInInspector]
	public Collider2D Collider;

	// Token: 0x04000B31 RID: 2865
	[HideInInspector]
	public PlayerPhysics MyPhysics;

	// Token: 0x04000B32 RID: 2866
	[HideInInspector]
	public CustomNetworkTransform NetTransform;

	// Token: 0x04000B33 RID: 2867
	public PetBehaviour CurrentPet;

	// Token: 0x04000B34 RID: 2868
	public HatParent HatRenderer;

	// Token: 0x04000B35 RID: 2869
	private SpriteRenderer myRend;

	// Token: 0x04000B36 RID: 2870
	private Collider2D[] hitBuffer = new Collider2D[20];

	// Token: 0x04000B37 RID: 2871
	public static GameOptionsData GameOptions = new GameOptionsData();

	// Token: 0x04000B38 RID: 2872
	public List<PlayerTask> myTasks = new List<PlayerTask>();

	// Token: 0x04000B39 RID: 2873
	public SpriteAnim[] ScannerAnims;

	// Token: 0x04000B3A RID: 2874
	public SpriteRenderer[] ScannersImages;

	// Token: 0x04000B3B RID: 2875
	private IUsable closest;

	// Token: 0x04000B3C RID: 2876
	private bool isNew = true;

	// Token: 0x04000B3D RID: 2877
	public bool isDummy;

	// Token: 0x04000B3E RID: 2878
	public bool notRealPlayer;

	// Token: 0x04000B3F RID: 2879
	public static List<PlayerControl> AllPlayerControls = new List<PlayerControl>();

	// Token: 0x04000B40 RID: 2880
	private Dictionary<Collider2D, IUsable> cache = new Dictionary<Collider2D, IUsable>(PlayerControl.ColliderComparer.Instance);

	// Token: 0x04000B41 RID: 2881
	private List<IUsable> itemsInRange = new List<IUsable>();

	// Token: 0x04000B42 RID: 2882
	private List<IUsable> newItemsInRange = new List<IUsable>();

	// Token: 0x04000B43 RID: 2883
	private byte scannerCount;

	// Token: 0x04000B44 RID: 2884
	private bool infectedSet;

	// Token: 0x0200040C RID: 1036
	public class ColliderComparer : IEqualityComparer<Collider2D>
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x00076B5E File Offset: 0x00074D5E
		public bool Equals(Collider2D x, Collider2D y)
		{
			return x == y;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00076B67 File Offset: 0x00074D67
		public int GetHashCode(Collider2D obj)
		{
			return obj.GetInstanceID();
		}

		// Token: 0x04001B78 RID: 7032
		public static readonly PlayerControl.ColliderComparer Instance = new PlayerControl.ColliderComparer();
	}

	// Token: 0x0200040D RID: 1037
	public class UsableComparer : IEqualityComparer<IUsable>
	{
		// Token: 0x0600194A RID: 6474 RVA: 0x00076B83 File Offset: 0x00074D83
		public bool Equals(IUsable x, IUsable y)
		{
			return x == y;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00076B89 File Offset: 0x00074D89
		public int GetHashCode(IUsable obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x04001B79 RID: 7033
		public static readonly PlayerControl.UsableComparer Instance = new PlayerControl.UsableComparer();
	}
}
