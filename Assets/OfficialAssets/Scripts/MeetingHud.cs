using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using InnerNet;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020000D3 RID: 211
public class MeetingHud : InnerNetObject, IDisconnectHandler
{
	// Token: 0x06000508 RID: 1288 RVA: 0x00022066 File Offset: 0x00020266
	public void RpcClose()
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.Close();
		}
		AmongUsClient.Instance.SendRpc(this.NetId, 22, (SendOption)1);
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00022090 File Offset: 0x00020290
	public void CmdCastVote(byte playerId, sbyte suspectIdx)
	{
		if (AmongUsClient.Instance.AmHost)
		{
			this.CastVote(playerId, suspectIdx);
			return;
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 24, (SendOption)1, AmongUsClient.Instance.HostId);
		messageWriter.Write(playerId);
		messageWriter.Write(suspectIdx);
		AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x000220EC File Offset: 0x000202EC
	private void RpcVotingComplete(byte[] states, GameData.PlayerInfo exiled, bool tie)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.VotingComplete(states, exiled, tie);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 23, (SendOption)1);
		messageWriter.WriteBytesAndSize(states);
		messageWriter.Write((exiled != null) ? exiled.PlayerId : byte.MaxValue);
		messageWriter.Write(tie);
		messageWriter.EndMessage();
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0002214C File Offset: 0x0002034C
	private void RpcClearVote(int clientId)
	{
		if (AmongUsClient.Instance.ClientId == clientId)
		{
			this.ClearVote();
			return;
		}
		MessageWriter msg = AmongUsClient.Instance.StartRpcImmediately(this.NetId, 25, (SendOption)1, clientId);
		AmongUsClient.Instance.FinishRpcImmediately(msg);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00022190 File Offset: 0x00020390
	public override void HandleRpc(byte callId, MessageReader reader)
	{
		switch (callId)
		{
		case 22:
			this.Close();
			return;
		case 23:
		{
			byte[] states = reader.ReadBytesAndSize();
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(reader.ReadByte());
			bool tie = reader.ReadBoolean();
			this.VotingComplete(states, playerById, tie);
			return;
		}
		case 24:
		{
			byte srcPlayerId = reader.ReadByte();
			sbyte suspectPlayerId = reader.ReadSByte();
			this.CastVote(srcPlayerId, suspectPlayerId);
			return;
		}
		case 25:
			this.ClearVote();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0002220A File Offset: 0x0002040A
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0002221C File Offset: 0x0002041C
	private void Awake()
	{
		if (!MeetingHud.Instance)
		{
			MeetingHud.Instance = this;
			return;
		}
		if (MeetingHud.Instance != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0002224C File Offset: 0x0002044C
	private void Start()
	{
		DestroyableSingleton<HudManager>.Instance.Chat.gameObject.SetActive(true);
		DestroyableSingleton<HudManager>.Instance.Chat.SetPosition(this);
		DestroyableSingleton<HudManager>.Instance.StopOxyFlash();
		DestroyableSingleton<HudManager>.Instance.StopReactorFlash();
		this.SkipVoteButton.SetTargetPlayerId(-1);
		this.SkipVoteButton.Parent = this;
		Camera.main.GetComponent<FollowerCamera>().Locked = true;
		if (PlayerControl.LocalPlayer.Data.IsDead)
		{
			this.SetForegroundForDead();
		}
		AmongUsClient.Instance.DisconnectHandlers.AddUnique(this);
		foreach (PlayerVoteArea playerVoteArea in this.playerStates)
		{
			this.ControllerSelectable.Add(playerVoteArea.PlayerButton);
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, null, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00022329 File Offset: 0x00020529
	private void SetForegroundForDead()
	{
		this.amDead = true;
		this.SkipVoteButton.gameObject.SetActive(false);
		this.Glass.sprite = this.CrackedGlass;
		this.Glass.color = Color.white;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00022364 File Offset: 0x00020564
	public void Update()
	{
		this.discussionTimer += Time.deltaTime;
		this.UpdateButtons();
		switch (this.state)
		{
		case MeetingHud.VoteStates.Discussion:
		{
			if (this.discussionTimer < (float)PlayerControl.GameOptions.DiscussionTime)
			{
				float num = (float)PlayerControl.GameOptions.DiscussionTime - this.discussionTimer;
				this.TimerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MeetingVotingBegins, new object[]
				{
					Mathf.CeilToInt(num)
				});
				for (int i = 0; i < this.playerStates.Length; i++)
				{
					this.playerStates[i].SetDisabled();
				}
				this.SkipVoteButton.SetDisabled();
				return;
			}
			this.state = MeetingHud.VoteStates.NotVoted;
			bool active = PlayerControl.GameOptions.VotingTime > 0;
			this.TimerText.gameObject.SetActive(active);
			for (int j = 0; j < this.playerStates.Length; j++)
			{
				this.playerStates[j].SetEnabled();
			}
			this.SkipVoteButton.SetEnabled();
			return;
		}
		case MeetingHud.VoteStates.NotVoted:
		case MeetingHud.VoteStates.Voted:
			if (PlayerControl.GameOptions.VotingTime > 0)
			{
				float num2 = this.discussionTimer - (float)PlayerControl.GameOptions.DiscussionTime;
				float num3 = Mathf.Max(0f, (float)PlayerControl.GameOptions.VotingTime - num2);
				this.TimerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MeetingVotingEnds, new object[]
				{
					Mathf.CeilToInt(num3)
				});
				if (this.state == MeetingHud.VoteStates.NotVoted && Mathf.CeilToInt(num3) <= this.lastSecond)
				{
					this.lastSecond--;
					base.StartCoroutine(Effects.PulseColor(this.TimerText, Color.red, Color.white, 0.25f));
					SoundManager.Instance.PlaySound(this.VoteEndingSound, false, 1f).pitch = Mathf.Lerp(1.5f, 0.8f, (float)this.lastSecond / 10f);
				}
				if (AmongUsClient.Instance.AmHost && num2 >= (float)PlayerControl.GameOptions.VotingTime)
				{
					this.ForceSkipAll();
					return;
				}
			}
			break;
		case MeetingHud.VoteStates.Results:
			if (AmongUsClient.Instance.GameMode == GameModes.OnlineGame)
			{
				float num4 = this.discussionTimer - this.resultsStartedAt;
				float num5 = Mathf.Max(0f, 5f - num4);
				this.TimerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MeetingProceeds, new object[]
				{
					Mathf.CeilToInt(num5)
				});
				if (AmongUsClient.Instance.AmHost && num5 <= 0f)
				{
					this.HandleProceed();
				}
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0002260B File Offset: 0x0002080B
	public IEnumerator CoIntro(GameData.PlayerInfo reporter, GameData.PlayerInfo targetPlayer)
	{
		if (DestroyableSingleton<HudManager>.InstanceExists)
		{
			DestroyableSingleton<HudManager>.Instance.Chat.ForceClosed();
			base.transform.SetParent(DestroyableSingleton<HudManager>.Instance.transform);
			base.transform.localPosition = new Vector3(0f, -10f, 5f);
			DestroyableSingleton<HudManager>.Instance.SetHudActive(false);
		}
		OverlayKillAnimation killAnimPrefab = (targetPlayer == null) ? ShipStatus.Instance.EmergencyOverlay : ShipStatus.Instance.ReportOverlay;
		DestroyableSingleton<HudManager>.Instance.KillOverlay.ShowOne(killAnimPrefab, reporter, targetPlayer);
		yield return DestroyableSingleton<HudManager>.Instance.KillOverlay.WaitForFinish();
		yield return Effects.Slide2D(base.transform, new Vector2(0f, -10f), new Vector2(0f, 0f), 0.25f);
		this.TitleText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MeetingWhoIsTitle, Array.Empty<object>());
		if (!PlayerControl.LocalPlayer.Data.IsDead)
		{
			yield return DestroyableSingleton<HudManager>.Instance.ShowEmblem(false);
		}
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			base.StartCoroutine(this.playerStates[i].CoAnimateOverlay());
		}
		yield break;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00022628 File Offset: 0x00020828
	private IEnumerator CoStartCutscene()
	{
		yield return DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.clear, Color.black, 1f);
		ExileController exileController = Object.Instantiate<ExileController>(ShipStatus.Instance.ExileCutscenePrefab);
		exileController.transform.SetParent(DestroyableSingleton<HudManager>.Instance.transform, false);
		exileController.transform.localPosition = new Vector3(0f, 0f, -60f);
		exileController.Begin(this.exiledPlayer, this.wasTie);
		this.DespawnOnDestroy = false;
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00022637 File Offset: 0x00020837
	public void ServerStart(byte reporter)
	{
		this.reporterId = reporter;
		this.PopulateButtons(reporter);
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00022648 File Offset: 0x00020848
	public void Close()
	{
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		DestroyableSingleton<HudManager>.Instance.Chat.SetPosition(null);
		DestroyableSingleton<HudManager>.Instance.Chat.SetVisible(data.IsDead);
		base.StartCoroutine(this.CoStartCutscene());
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00022694 File Offset: 0x00020894
	private void VotingComplete(byte[] states, GameData.PlayerInfo exiled, bool tie)
	{
		if (this.state == MeetingHud.VoteStates.Results)
		{
			return;
		}
		this.state = MeetingHud.VoteStates.Results;
		this.resultsStartedAt = this.discussionTimer;
		this.exiledPlayer = exiled;
		this.wasTie = tie;
		this.SkipVoteButton.gameObject.SetActive(false);
		this.SkippedVoting.gameObject.SetActive(true);
		AmongUsClient.Instance.DisconnectHandlers.Remove(this);
		this.PopulateResults(states);
		this.SetupProceedButton();
		if (DestroyableSingleton<HudManager>.Instance.Chat.IsOpen)
		{
			DestroyableSingleton<HudManager>.Instance.Chat.ForceClosed();
			ControllerManager.Instance.CloseOverlayMenu(DestroyableSingleton<HudManager>.Instance.Chat.name);
		}
		ControllerManager.Instance.CloseOverlayMenu(base.name);
		ControllerManager.Instance.OpenOverlayMenu(base.name, null, this.ProceedButtonUi);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0002276C File Offset: 0x0002096C
	public bool Select(int suspectStateIdx)
	{
		if (this.discussionTimer < (float)PlayerControl.GameOptions.DiscussionTime)
		{
			return false;
		}
		if (PlayerControl.LocalPlayer.Data.IsDead)
		{
			return false;
		}
		SoundManager.Instance.PlaySound(this.VoteSound, false, 1f).volume = 0.8f;
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			PlayerVoteArea playerVoteArea = this.playerStates[i];
			if (suspectStateIdx != (int)playerVoteArea.TargetPlayerId)
			{
				playerVoteArea.ClearButtons();
			}
		}
		if (suspectStateIdx != -1)
		{
			this.SkipVoteButton.ClearButtons();
		}
		return true;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x000227FC File Offset: 0x000209FC
	public void Confirm(sbyte suspectStateIdx)
	{
		if (PlayerControl.LocalPlayer.Data.IsDead)
		{
			return;
		}
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			PlayerVoteArea playerVoteArea = this.playerStates[i];
			playerVoteArea.ClearButtons();
			playerVoteArea.voteComplete = true;
		}
		this.SkipVoteButton.ClearButtons();
		this.SkipVoteButton.voteComplete = true;
		this.SkipVoteButton.gameObject.SetActive(false);
		MeetingHud.VoteStates voteStates = this.state;
		if (voteStates != MeetingHud.VoteStates.NotVoted)
		{
			return;
		}
		this.state = MeetingHud.VoteStates.Voted;
		this.CmdCastVote(PlayerControl.LocalPlayer.PlayerId, suspectStateIdx);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00022894 File Offset: 0x00020A94
	public void HandleDisconnect(PlayerControl pc, DisconnectReasons reason)
	{
		if (!AmongUsClient.Instance.AmHost)
		{
			return;
		}
		if (this.playerStates == null)
		{
			return;
		}
		if (!pc)
		{
			return;
		}
		if (!GameData.Instance)
		{
			return;
		}
		int num = this.playerStates.IndexOf((PlayerVoteArea pv) => pv.TargetPlayerId == (sbyte)pc.PlayerId);
		PlayerVoteArea playerVoteArea = this.playerStates[num];
		playerVoteArea.isDead = true;
		playerVoteArea.Overlay.gameObject.SetActive(true);
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			PlayerVoteArea playerVoteArea2 = this.playerStates[i];
			if (!playerVoteArea2.isDead && playerVoteArea2.didVote && playerVoteArea2.votedFor == (sbyte)pc.PlayerId)
			{
				playerVoteArea2.UnsetVote();
				base.SetDirtyBit(1U << i);
				GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById((byte)playerVoteArea2.TargetPlayerId);
				if (playerById != null)
				{
					int clientIdFromCharacter = AmongUsClient.Instance.GetClientIdFromCharacter(playerById.Object);
					if (clientIdFromCharacter != -1)
					{
						this.RpcClearVote(clientIdFromCharacter);
					}
				}
			}
		}
		base.SetDirtyBit(1U << num);
		this.CheckForEndVoting();
		if (this.state == MeetingHud.VoteStates.Results)
		{
			this.SetupProceedButton();
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x000229C2 File Offset: 0x00020BC2
	public void HandleDisconnect()
	{
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000229C4 File Offset: 0x00020BC4
	private void ForceSkipAll()
	{
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			PlayerVoteArea playerVoteArea = this.playerStates[i];
			if (!playerVoteArea.didVote)
			{
				playerVoteArea.didVote = true;
				playerVoteArea.votedFor = -2;
				base.SetDirtyBit(1U << i);
			}
		}
		this.CheckForEndVoting();
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00022A18 File Offset: 0x00020C18
	public void CastVote(byte srcPlayerId, sbyte suspectPlayerId)
	{
		int num = this.playerStates.IndexOf((PlayerVoteArea pv) => pv.TargetPlayerId == (sbyte)srcPlayerId);
		PlayerVoteArea playerVoteArea = this.playerStates[num];
		if (!playerVoteArea.isDead && !playerVoteArea.didVote)
		{
			if (PlayerControl.LocalPlayer.PlayerId == srcPlayerId || AmongUsClient.Instance.GameMode != GameModes.LocalGame)
			{
				SoundManager.Instance.PlaySound(this.VoteLockinSound, false, 1f);
			}
			playerVoteArea.SetVote(suspectPlayerId);
			base.SetDirtyBit(1U << num);
			this.CheckForEndVoting();
			PlayerControl.LocalPlayer.RpcSendChatNote(srcPlayerId, ChatNoteTypes.DidVote);
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00022AC4 File Offset: 0x00020CC4
	public void ClearVote()
	{
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			this.playerStates[i].voteComplete = false;
		}
		this.SkipVoteButton.voteComplete = false;
		this.SkipVoteButton.gameObject.SetActive(true);
		this.state = MeetingHud.VoteStates.NotVoted;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00022B18 File Offset: 0x00020D18
	private void CheckForEndVoting()
	{
		if (this.playerStates.All((PlayerVoteArea ps) => ps.isDead || ps.didVote))
		{
			byte[] self = this.CalculateVotes();
			bool tie;
			int maxIdx = self.IndexOfMax((byte p) => (int)p, out tie) - 1;
			GameData.PlayerInfo exiled = GameData.Instance.AllPlayers.FirstOrDefault((GameData.PlayerInfo v) => (int)v.PlayerId == maxIdx);
			byte[] array = new byte[10];
			for (int i = 0; i < this.playerStates.Length; i++)
			{
				PlayerVoteArea playerVoteArea = this.playerStates[i];
				array[(int)playerVoteArea.TargetPlayerId] = playerVoteArea.GetState();
			}
			this.RpcVotingComplete(array, exiled, tie);
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00022BF4 File Offset: 0x00020DF4
	private byte[] CalculateVotes()
	{
		byte[] array = new byte[11];
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			PlayerVoteArea playerVoteArea = this.playerStates[i];
			if (playerVoteArea.didVote)
			{
				int num = (int)(playerVoteArea.votedFor + 1);
				if (num >= 0 && num < array.Length)
				{
					byte[] array2 = array;
					int num2 = num;
					array2[num2] += 1;
				}
			}
		}
		return array;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00022C50 File Offset: 0x00020E50
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		if (this.playerStates == null)
		{
			return false;
		}
		if (initialState)
		{
			for (int i = 0; i < this.playerStates.Length; i++)
			{
				this.playerStates[i].Serialize(writer);
			}
		}
		else
		{
			writer.WritePacked(this.DirtyBits);
			for (int j = 0; j < this.playerStates.Length; j++)
			{
				if (base.IsDirtyBitSet(j))
				{
					this.playerStates[j].Serialize(writer);
				}
			}
		}
		base.ClearDirtyBits();
		return true;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00022CCC File Offset: 0x00020ECC
	public override void Deserialize(MessageReader reader, bool initialState)
	{
		if (initialState)
		{
			MeetingHud.Instance = this;
			this.PopulateButtons(0);
			for (int i = 0; i < this.playerStates.Length; i++)
			{
				PlayerVoteArea playerVoteArea = this.playerStates[i];
				playerVoteArea.Deserialize(reader);
				if (playerVoteArea.didReport)
				{
					this.reporterId = (byte)playerVoteArea.TargetPlayerId;
				}
			}
			return;
		}
		uint num = reader.ReadPackedUInt32();
		for (int j = 0; j < this.playerStates.Length; j++)
		{
			if ((num & 1U << j) != 0U)
			{
				this.playerStates[j].Deserialize(reader);
			}
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00022D54 File Offset: 0x00020F54
	public void HandleProceed()
	{
		if (!AmongUsClient.Instance.AmHost)
		{
			base.StartCoroutine(Effects.SwayX(this.HostIcon.transform, 0.75f, 0.25f));
			return;
		}
		if (this.state != MeetingHud.VoteStates.Results)
		{
			return;
		}
		this.state = MeetingHud.VoteStates.Proceeding;
		this.RpcClose();
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00022DA8 File Offset: 0x00020FA8
	private void SetupProceedButton()
	{
		if (AmongUsClient.Instance.GameMode != GameModes.OnlineGame)
		{
			this.TimerText.gameObject.SetActive(false);
			this.ProceedButton.gameObject.SetActive(true);
			this.HostIcon.gameObject.SetActive(true);
			GameData.PlayerInfo host = GameData.Instance.GetHost();
			if (host != null)
			{
				PlayerControl.SetPlayerMaterialColors(host.ColorId, this.HostIcon);
				return;
			}
			this.HostIcon.enabled = false;
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00022E24 File Offset: 0x00021024
	private void PopulateResults(byte[] states)
	{
		this.TitleText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MeetingVotingResults, Array.Empty<object>());
		int num = 0;
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			PlayerVoteArea playerVoteArea = this.playerStates[i];
			playerVoteArea.ClearForResults();
			int num2 = 0;
			for (int j = 0; j < this.playerStates.Length; j++)
			{
				PlayerVoteArea playerVoteArea2 = this.playerStates[j];
				byte self = states[(int)playerVoteArea2.TargetPlayerId];
				if (!self.HasAnyBit(128))
				{
					GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById((byte)playerVoteArea2.TargetPlayerId);
					int votedFor = (int)PlayerVoteArea.GetVotedFor(self);
					if (votedFor == (int)playerVoteArea.TargetPlayerId)
					{
						SpriteRenderer spriteRenderer = Object.Instantiate<SpriteRenderer>(this.PlayerVotePrefab);
						if (PlayerControl.GameOptions.AnonymousVotes)
						{
							PlayerControl.SetPlayerMaterialColors(Palette.DisabledGrey, spriteRenderer);
						}
						else
						{
							PlayerControl.SetPlayerMaterialColors(playerById.ColorId, spriteRenderer);
						}
						spriteRenderer.transform.SetParent(playerVoteArea.transform);
						spriteRenderer.transform.localPosition = this.CounterOrigin + new Vector3(this.CounterOffsets.x * (float)num2, 0f, 0f);
						spriteRenderer.transform.localScale = Vector3.zero;
						base.StartCoroutine(Effects.Bloop((float)num2 * 0.5f, spriteRenderer.transform, 1f, 0.5f));
						num2++;
					}
					else if (i == 0 && votedFor == -1)
					{
						SpriteRenderer spriteRenderer2 = Object.Instantiate<SpriteRenderer>(this.PlayerVotePrefab);
						if (PlayerControl.GameOptions.AnonymousVotes)
						{
							PlayerControl.SetPlayerMaterialColors(Palette.DisabledGrey, spriteRenderer2);
						}
						else
						{
							PlayerControl.SetPlayerMaterialColors(playerById.ColorId, spriteRenderer2);
						}
						spriteRenderer2.transform.SetParent(this.SkippedVoting.transform);
						spriteRenderer2.transform.localPosition = this.CounterOrigin + new Vector3(this.CounterOffsets.x * (float)num, 0f, 0f);
						spriteRenderer2.transform.localScale = Vector3.zero;
						base.StartCoroutine(Effects.Bloop((float)num * 0.5f, spriteRenderer2.transform, 1f, 0.5f));
						num++;
					}
				}
			}
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0002306C File Offset: 0x0002126C
	private void UpdateButtons()
	{
		if (PlayerControl.LocalPlayer.Data.IsDead && !this.amDead)
		{
			this.SetForegroundForDead();
		}
		if (AmongUsClient.Instance.AmHost)
		{
			for (int i = 0; i < this.playerStates.Length; i++)
			{
				PlayerVoteArea playerVoteArea = this.playerStates[i];
				GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById((byte)playerVoteArea.TargetPlayerId);
				if (playerById == null)
				{
					playerVoteArea.SetDisabled();
				}
				else
				{
					bool flag = playerById.Disconnected || playerById.IsDead;
					if (flag != playerVoteArea.isDead)
					{
						playerVoteArea.SetDead(playerById.PlayerId == PlayerControl.LocalPlayer.PlayerId, this.reporterId == playerById.PlayerId, flag);
						base.SetDirtyBit(1U << i);
					}
				}
			}
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00023134 File Offset: 0x00021334
	private void PopulateButtons(byte reporter)
	{
		this.playerStates = new PlayerVoteArea[GameData.Instance.PlayerCount];
		for (int i = 0; i < this.playerStates.Length; i++)
		{
			GameData.PlayerInfo playerInfo = GameData.Instance.AllPlayers[i];
			PlayerVoteArea playerVoteArea = this.playerStates[i] = this.CreateButton(playerInfo);
			playerVoteArea.Parent = this;
			playerVoteArea.SetTargetPlayerId((sbyte)playerInfo.PlayerId);
			playerVoteArea.SetDead(playerInfo.PlayerId == PlayerControl.LocalPlayer.PlayerId, reporter == playerInfo.PlayerId, playerInfo.Disconnected || playerInfo.IsDead);
		}
		foreach (PlayerVoteArea playerVoteArea2 in this.playerStates)
		{
			ControllerManager.Instance.AddSelectableUiElement(playerVoteArea2.PlayerButton, false);
		}
		this.SortButtons();
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00023208 File Offset: 0x00021408
	private void SortButtons()
	{
		PlayerVoteArea[] array = this.playerStates.OrderBy(delegate(PlayerVoteArea p)
		{
			if (!p.isDead)
			{
				return 0;
			}
			return 50;
		}).ThenBy((PlayerVoteArea p) => p.TargetPlayerId).ToArray<PlayerVoteArea>();
		for (int i = 0; i < array.Length; i++)
		{
			int num = i % 2;
			int num2 = i / 2;
			array[i].transform.localPosition = this.VoteOrigin + new Vector3(this.VoteButtonOffsets.x * (float)num, this.VoteButtonOffsets.y * (float)num2, -0.9f - (float)num2 * 0.01f);
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000232C8 File Offset: 0x000214C8
	private PlayerVoteArea CreateButton(GameData.PlayerInfo playerInfo)
	{
		PlayerVoteArea playerVoteArea = Object.Instantiate<PlayerVoteArea>(this.PlayerButtonPrefab, this.ButtonParent.transform);
		playerVoteArea.PlayerIcon.SetFlipX(true);
		PlayerControl.SetPlayerMaterialColors(playerInfo.ColorId, playerVoteArea.PlayerIcon.Body);
		playerVoteArea.PlayerIcon.HatSlot.SetHat(playerInfo.HatId, playerInfo.ColorId);
		DestroyableSingleton<HatManager>.Instance.SetSkin(playerVoteArea.PlayerIcon.SkinSlot, playerInfo.SkinId);
		playerVoteArea.NameText.Text = playerInfo.PlayerName;
		bool flag = PlayerControl.LocalPlayer.Data.IsImpostor && playerInfo.IsImpostor;
		playerVoteArea.NameText.Color = (flag ? Palette.ImpostorRed : Color.white);
		playerVoteArea.transform.localScale = Vector3.one;
		return playerVoteArea;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0002339C File Offset: 0x0002159C
	public bool DidVote(byte playerId)
	{
		return this.playerStates.First((PlayerVoteArea p) => p.TargetPlayerId == (sbyte)playerId).didVote;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x000233D4 File Offset: 0x000215D4
	public int GetVotesRemaining()
	{
		int result;
		try
		{
			result = this.playerStates.Count((PlayerVoteArea ps) => !ps.isDead && !ps.didVote);
		}
		catch
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x040005BC RID: 1468
	private const float ResultsTime = 5f;

	// Token: 0x040005BD RID: 1469
	private const float Depth = 5f;

	// Token: 0x040005BE RID: 1470
	public static MeetingHud Instance;

	// Token: 0x040005BF RID: 1471
	public Transform ButtonParent;

	// Token: 0x040005C0 RID: 1472
	public TextRenderer TitleText;

	// Token: 0x040005C1 RID: 1473
	public Vector3 VoteOrigin = new Vector3(-3.6f, 1.75f);

	// Token: 0x040005C2 RID: 1474
	public Vector3 VoteButtonOffsets = new Vector2(3.6f, -0.91f);

	// Token: 0x040005C3 RID: 1475
	private Vector3 CounterOrigin = new Vector2(0.5f, -0.13f);

	// Token: 0x040005C4 RID: 1476
	private Vector3 CounterOffsets = new Vector2(0.3f, 0f);

	// Token: 0x040005C5 RID: 1477
	public PlayerVoteArea SkipVoteButton;

	// Token: 0x040005C6 RID: 1478
	private PlayerVoteArea[] playerStates = new PlayerVoteArea[0];

	// Token: 0x040005C7 RID: 1479
	public PlayerVoteArea PlayerButtonPrefab;

	// Token: 0x040005C8 RID: 1480
	public SpriteRenderer PlayerVotePrefab;

	// Token: 0x040005C9 RID: 1481
	public Sprite CrackedGlass;

	// Token: 0x040005CA RID: 1482
	public SpriteRenderer Glass;

	// Token: 0x040005CB RID: 1483
	public PassiveButton ProceedButton;

	// Token: 0x040005CC RID: 1484
	public AudioClip VoteSound;

	// Token: 0x040005CD RID: 1485
	public AudioClip VoteLockinSound;

	// Token: 0x040005CE RID: 1486
	public AudioClip VoteEndingSound;

	// Token: 0x040005CF RID: 1487
	private MeetingHud.VoteStates state;

	// Token: 0x040005D0 RID: 1488
	public SpriteRenderer SkippedVoting;

	// Token: 0x040005D1 RID: 1489
	public SpriteRenderer HostIcon;

	// Token: 0x040005D2 RID: 1490
	public Sprite KillBackground;

	// Token: 0x040005D3 RID: 1491
	private GameData.PlayerInfo exiledPlayer;

	// Token: 0x040005D4 RID: 1492
	private bool wasTie;

	// Token: 0x040005D5 RID: 1493
	public TextRenderer TimerText;

	// Token: 0x040005D6 RID: 1494
	public float discussionTimer;

	// Token: 0x040005D7 RID: 1495
	private byte reporterId;

	// Token: 0x040005D8 RID: 1496
	[Header("Console Controller Navigation")]
	public UiElement DefaultButtonSelected;

	// Token: 0x040005D9 RID: 1497
	public UiElement ProceedButtonUi;

	// Token: 0x040005DA RID: 1498
	public List<UiElement> ControllerSelectable;

	// Token: 0x040005DB RID: 1499
	private bool amDead;

	// Token: 0x040005DC RID: 1500
	private float resultsStartedAt;

	// Token: 0x040005DD RID: 1501
	private int lastSecond = 10;

	// Token: 0x02000367 RID: 871
	public enum VoteStates
	{
		// Token: 0x04001922 RID: 6434
		Discussion,
		// Token: 0x04001923 RID: 6435
		NotVoted,
		// Token: 0x04001924 RID: 6436
		Voted,
		// Token: 0x04001925 RID: 6437
		Results,
		// Token: 0x04001926 RID: 6438
		Proceeding
	}
}
