using System;
using System.Collections.Generic;
using System.Linq;
//using Beebyte.Obfuscator;
using Hazel;
using InnerNet;
using UnityEngine;

// Token: 0x0200009B RID: 155
//[SkipRename]
public class GameData : InnerNetObject, IDisconnectHandler
{
	// Token: 0x060003A0 RID: 928 RVA: 0x00018048 File Offset: 0x00016248
	public void RpcSetTasks(byte playerId, byte[] taskTypeIds)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.SetTasks(playerId, taskTypeIds);
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 29, SendOption.Reliable);
		messageWriter.Write(playerId);
		messageWriter.WriteBytesAndSize(taskTypeIds);
		messageWriter.EndMessage();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00018084 File Offset: 0x00016284
	public static bool ContainsBadChars(string name, bool chat)
	{
		int i = 0;
		while (i < name.Length)
		{
			char c = name[i];
			if (c <= ')')
			{
				if (c == '#')
				{
					return true;
				}
				if (c == '\'' || c == ')')
				{
					goto IL_3D;
				}
			}
			else if (c <= ':')
			{
				if (c == '-' || c == ':')
				{
					goto IL_3D;
				}
			}
			else
			{
				if (c == '[')
				{
					return true;
				}
				if (c == '\\')
				{
					goto IL_3D;
				}
			}
			IL_44:
			i++;
			continue;
			IL_3D:
			if (!chat)
			{
				return true;
			}
			goto IL_44;
		}
		return false;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x000180E4 File Offset: 0x000162E4
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		if (initialState)
		{
			writer.WritePacked(this.AllPlayers.Count);
			for (int i = 0; i < this.AllPlayers.Count; i++)
			{
				GameData.PlayerInfo playerInfo = this.AllPlayers[i];
				writer.Write(playerInfo.PlayerId);
				playerInfo.Serialize(writer);
			}
		}
		else
		{
			for (int j = 0; j < this.AllPlayers.Count; j++)
			{
				GameData.PlayerInfo playerInfo2 = this.AllPlayers[j];
				if (base.IsDirtyBitSet((int)playerInfo2.PlayerId))
				{
					writer.StartMessage(playerInfo2.PlayerId);
					playerInfo2.Serialize(writer);
					writer.EndMessage();
				}
			}
			base.ClearDirtyBits();
		}
		return true;
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00018190 File Offset: 0x00016390
	public override void Deserialize(MessageReader reader, bool initialState)
	{
		if (initialState)
		{
			int num = reader.ReadPackedInt32();
			for (int i = 0; i < num; i++)
			{
				GameData.PlayerInfo playerInfo = new GameData.PlayerInfo(reader.ReadByte());
				playerInfo.Deserialize(reader);
				this.AllPlayers.Add(playerInfo);
			}
		}
		else
		{
			while (reader.Position < reader.Length)
			{
				MessageReader messageReader = reader.ReadMessage();
				GameData.PlayerInfo playerInfo2 = this.GetPlayerById(messageReader.Tag);
				if (playerInfo2 != null)
				{
					playerInfo2.Deserialize(messageReader);
				}
				else
				{
					playerInfo2 = new GameData.PlayerInfo(messageReader.Tag);
					playerInfo2.Deserialize(messageReader);
					this.AllPlayers.Add(playerInfo2);
				}
			}
		}
		this.RecomputeTaskCounts();
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0001822D File Offset: 0x0001642D
	public override void HandleRpc(byte callId, MessageReader reader)
	{
		if (callId == 29)
		{
			this.SetTasks(reader.ReadByte(), reader.ReadBytesAndSize());
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060003A5 RID: 933 RVA: 0x00018246 File Offset: 0x00016446
	public int PlayerCount
	{
		get
		{
			return this.AllPlayers.Count;
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00018254 File Offset: 0x00016454
	public void Awake()
	{
		if (GameData.Instance && GameData.Instance != this)
		{
			Debug.Log("Destroying dupe GameData");
            UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GameData.Instance = this;
		if (AmongUsClient.Instance)
		{
			AmongUsClient.Instance.DisconnectHandlers.AddUnique(this);
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000182B2 File Offset: 0x000164B2
	internal void SetDirty()
	{
		base.SetDirtyBit(uint.MaxValue);
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000182BC File Offset: 0x000164BC
	public GameData.PlayerInfo GetHost()
	{
		ClientData host = AmongUsClient.Instance.GetHost();
		if (host != null && host.Character)
		{
			return host.Character.Data;
		}
		return null;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000182F4 File Offset: 0x000164F4
	public sbyte GetAvailableId()
	{
		sbyte i;
		sbyte j;
		for (i = 0; i < 10; i = (sbyte)(j + (sbyte)1))
		{
			if (!this.AllPlayers.Any((GameData.PlayerInfo p) => p.PlayerId == (byte)i))
			{
				return i;
			}
			j = i;
		}
		return -1;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001834C File Offset: 0x0001654C
	public GameData.PlayerInfo GetPlayerById(byte id)
	{
		if (id == 255)
		{
			return null;
		}
		for (int i = 0; i < this.AllPlayers.Count; i++)
		{
			if (this.AllPlayers[i].PlayerId == id)
			{
				return this.AllPlayers[i];
			}
		}
		return null;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001839C File Offset: 0x0001659C
	public void UpdateName(byte playerId, string name, bool dontCensor = false)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		if (playerById != null)
		{
			playerById.dontCensorName = dontCensor;
			playerById.PlayerName = name;
		}
		base.SetDirtyBit(1U << (int)playerId);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000183D0 File Offset: 0x000165D0
	public void UpdateColor(byte playerId, int color)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		if (playerById != null)
		{
			playerById.ColorId = color;
		}
		base.SetDirtyBit(1U << (int)playerId);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000183FC File Offset: 0x000165FC
	public void UpdateHat(byte playerId, uint hat)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		if (playerById != null)
		{
			playerById.HatId = hat;
		}
		base.SetDirtyBit(1U << (int)playerId);
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00018428 File Offset: 0x00016628
	public void UpdatePet(byte playerId, uint petId)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		if (playerById != null)
		{
			playerById.PetId = petId;
		}
		base.SetDirtyBit(1U << (int)playerId);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00018454 File Offset: 0x00016654
	public void UpdateSkin(byte playerId, uint skin)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		if (playerById != null)
		{
			playerById.SkinId = skin;
		}
		base.SetDirtyBit(1U << (int)playerId);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00018480 File Offset: 0x00016680
	public GameData.PlayerInfo AddPlayer(PlayerControl pc)
	{
		GameData.PlayerInfo playerInfo = new GameData.PlayerInfo(pc);
		this.AllPlayers.Add(playerInfo);
		base.SetDirtyBit(1U << (int)pc.PlayerId);
		return playerInfo;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x000184B4 File Offset: 0x000166B4
	public bool RemovePlayer(byte playerId)
	{
		for (int i = 0; i < this.AllPlayers.Count; i++)
		{
			if (this.AllPlayers[i].PlayerId == playerId)
			{
				this.SetDirty();
				this.AllPlayers.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00018500 File Offset: 0x00016700
	public void RecomputeTaskCounts()
	{
		this.TotalTasks = 0;
		this.CompletedTasks = 0;
		for (int i = 0; i < this.AllPlayers.Count; i++)
		{
			GameData.PlayerInfo playerInfo = this.AllPlayers[i];
			if (!playerInfo.Disconnected && playerInfo.Tasks != null && playerInfo.Object && (PlayerControl.GameOptions.GhostsDoTasks || !playerInfo.IsDead) && !playerInfo.IsImpostor)
			{
				for (int j = 0; j < playerInfo.Tasks.Count; j++)
				{
					this.TotalTasks++;
					if (playerInfo.Tasks[j].Complete)
					{
						this.CompletedTasks++;
					}
				}
			}
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x000185C4 File Offset: 0x000167C4
	public void TutOnlyRemoveTask(byte playerId, uint taskId)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		GameData.TaskInfo item = playerById.FindTaskById(taskId);
		playerById.Tasks.Remove(item);
		this.RecomputeTaskCounts();
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x000185F4 File Offset: 0x000167F4
	public uint TutOnlyAddTask(byte playerId)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		uint num = (from d in playerById.Tasks
		select d.Id).Max<uint>() + 1U;
		playerById.Tasks.Add(new GameData.TaskInfo
		{
			Id = num
		});
		this.TotalTasks++;
		return num;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00018660 File Offset: 0x00016860
	private void SetTasks(byte playerId, byte[] taskTypeIds)
	{
		GameData.PlayerInfo playerById = this.GetPlayerById(playerId);
		if (playerById == null)
		{
			Debug.Log("Could not set tasks for player id: " + playerId.ToString());
			return;
		}
		if (playerById.Disconnected)
		{
			return;
		}
		if (!playerById.Object)
		{
			Debug.Log("Could not set tasks for player (" + playerById.PlayerName + "): " + playerId.ToString());
			return;
		}
		playerById.Tasks = new List<GameData.TaskInfo>(taskTypeIds.Length);
		for (int i = 0; i < taskTypeIds.Length; i++)
		{
			playerById.Tasks.Add(new GameData.TaskInfo(taskTypeIds[i], (uint)i));
			playerById.Tasks[i].Id = (uint)i;
		}
		playerById.Object.SetTasks(playerById.Tasks);
		base.SetDirtyBit(1U << (int)playerById.PlayerId);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001872C File Offset: 0x0001692C
	public void CompleteTask(PlayerControl pc, uint taskId)
	{
		GameData.TaskInfo taskInfo = pc.Data.FindTaskById(taskId);
		if (taskInfo == null)
		{
			Debug.LogWarning("Couldn't find task: " + taskId.ToString());
			return;
		}
		if (!taskInfo.Complete)
		{
			taskInfo.Complete = true;
			this.CompletedTasks++;
			return;
		}
		Debug.LogWarning("Double complete task: " + taskId.ToString());
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00018794 File Offset: 0x00016994
	public void HandleDisconnect(PlayerControl player, DisconnectReasons reason)
	{
		if (!player)
		{
			return;
		}
		GameData.PlayerInfo playerById = this.GetPlayerById(player.PlayerId);
		if (playerById == null)
		{
			return;
		}
		if (AmongUsClient.Instance.IsGameStarted)
		{
			if (!playerById.Disconnected)
			{
				playerById.Disconnected = true;
				TempData.LastDeathReason = DeathReason.Disconnect;
				this.ShowNotification(playerById.PlayerName, reason);
			}
		}
		else if (this.RemovePlayer(player.PlayerId))
		{
			this.ShowNotification(playerById.PlayerName, reason);
		}
		this.RecomputeTaskCounts();
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001880C File Offset: 0x00016A0C
	private void ShowNotification(string playerName, DisconnectReasons reason)
	{
		if (string.IsNullOrEmpty(playerName))
		{
			return;
		}
		if (reason <= DisconnectReasons.Banned)
		{
			if (reason == DisconnectReasons.ExitGame)
			{
				DestroyableSingleton<HudManager>.Instance.Notifier.AddItem(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.UserLeftGame, new object[]
				{
					playerName
				}));
				return;
			}
			if (reason == DisconnectReasons.Banned)
			{
				GameData.PlayerInfo data = AmongUsClient.Instance.GetHost().Character.Data;
				DestroyableSingleton<HudManager>.Instance.Notifier.AddItem(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.PlayerWasBannedBy, new object[]
				{
					playerName,
					data.PlayerName
				}));
				return;
			}
		}
		else if (reason == DisconnectReasons.Kicked)
		{
			GameData.PlayerInfo data2 = AmongUsClient.Instance.GetHost().Character.Data;
			DestroyableSingleton<HudManager>.Instance.Notifier.AddItem(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.PlayerWasKickedBy, new object[]
			{
				playerName,
				data2.PlayerName
			}));
			return;
		}
		DestroyableSingleton<HudManager>.Instance.Notifier.AddItem(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.LeftGameError, new object[]
		{
			playerName
		}));
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0001891C File Offset: 0x00016B1C
	public void HandleDisconnect()
	{
		if (!AmongUsClient.Instance.IsGameStarted)
		{
			for (int i = this.AllPlayers.Count - 1; i >= 0; i--)
			{
				if (!this.AllPlayers[i].Object)
				{
					this.AllPlayers.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x04000445 RID: 1093
	public static GameData Instance;

	// Token: 0x04000446 RID: 1094
	public List<GameData.PlayerInfo> AllPlayers = new List<GameData.PlayerInfo>();

	// Token: 0x04000447 RID: 1095
	public int TotalTasks;

	// Token: 0x04000448 RID: 1096
	public int CompletedTasks;

	// Token: 0x04000449 RID: 1097
	public const byte InvalidPlayerId = 255;

	// Token: 0x0400044A RID: 1098
	public const byte DisconnectedPlayerId = 254;

	// Token: 0x02000334 RID: 820
	public class TaskInfo
	{
		// Token: 0x060015D7 RID: 5591 RVA: 0x0006C02F File Offset: 0x0006A22F
		public TaskInfo()
		{
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0006C037 File Offset: 0x0006A237
		public TaskInfo(byte typeId, uint id)
		{
			this.Id = id;
			this.TypeId = typeId;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0006C04D File Offset: 0x0006A24D
		public void Serialize(MessageWriter writer)
		{
			writer.WritePacked(this.Id);
			writer.Write(this.Complete);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0006C067 File Offset: 0x0006A267
		public void Deserialize(MessageReader reader)
		{
			this.Id = reader.ReadPackedUInt32();
			this.Complete = reader.ReadBoolean();
		}

		// Token: 0x04001831 RID: 6193
		public uint Id;

		// Token: 0x04001832 RID: 6194
		public byte TypeId;

		// Token: 0x04001833 RID: 6195
		public bool Complete;
	}

	// Token: 0x02000335 RID: 821
	public class PlayerInfo
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0006C081 File Offset: 0x0006A281
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x0006C089 File Offset: 0x0006A289
		public string PlayerName
		{
			get
			{
				return this._playerName;
			}
			set
			{
				this._playerName = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0006C092 File Offset: 0x0006A292
		public bool IsIncomplete
		{
			get
			{
				return string.IsNullOrEmpty(this.PlayerName) || this.ColorId == -1 || this.HatId == uint.MaxValue || this.PetId == uint.MaxValue || this.SkinId == uint.MaxValue;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0006C0C7 File Offset: 0x0006A2C7
		public PlayerControl Object
		{
			get
			{
				if (!this._object)
				{
					this._object = PlayerControl.AllPlayerControls.FirstOrDefault((PlayerControl p) => p.PlayerId == this.PlayerId);
				}
				return this._object;
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0006C0F8 File Offset: 0x0006A2F8
		public PlayerInfo(byte playerId)
		{
			this.PlayerId = playerId;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0006C12E File Offset: 0x0006A32E
		public PlayerInfo(PlayerControl pc) : this(pc.PlayerId)
		{
			this._object = pc;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0006C144 File Offset: 0x0006A344
		public void Serialize(MessageWriter writer)
		{
			writer.Write(this.PlayerName);
			writer.WritePacked(this.ColorId);
			writer.WritePacked(this.HatId);
			writer.WritePacked(this.PetId);
			writer.WritePacked(this.SkinId);
			byte b = 0;
			if (this.Disconnected)
			{
				b |= 1;
			}
			if (this.IsImpostor)
			{
				b |= 2;
			}
			if (this.IsDead)
			{
				b |= 4;
			}
			writer.Write(b);
			if (this.Tasks != null)
			{
				writer.Write((byte)this.Tasks.Count);
				for (int i = 0; i < this.Tasks.Count; i++)
				{
					this.Tasks[i].Serialize(writer);
				}
				return;
			}
			writer.Write(0);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0006C208 File Offset: 0x0006A408
		public void Deserialize(MessageReader reader)
		{
			this.PlayerName = reader.ReadString();
			this.ColorId = reader.ReadPackedInt32();
			this.HatId = reader.ReadPackedUInt32();
			this.PetId = reader.ReadPackedUInt32();
			this.SkinId = reader.ReadPackedUInt32();
			byte b = reader.ReadByte();
			this.Disconnected = ((b & 1) > 0);
			this.IsImpostor = ((b & 2) > 0);
			this.IsDead = ((b & 4) > 0);
			byte b2 = reader.ReadByte();
			this.Tasks = new List<GameData.TaskInfo>((int)b2);
			for (int i = 0; i < (int)b2; i++)
			{
				this.Tasks.Add(new GameData.TaskInfo());
				this.Tasks[i].Deserialize(reader);
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0006C2C0 File Offset: 0x0006A4C0
		public GameData.TaskInfo FindTaskById(uint taskId)
		{
			for (int i = 0; i < this.Tasks.Count; i++)
			{
				if (this.Tasks[i].Id == taskId)
				{
					return this.Tasks[i];
				}
			}
			return null;
		}

		// Token: 0x04001834 RID: 6196
		public readonly byte PlayerId;

		// Token: 0x04001835 RID: 6197
		private string _playerName = string.Empty;

		// Token: 0x04001836 RID: 6198
		public bool dontCensorName;

		// Token: 0x04001837 RID: 6199
		public int ColorId = -1;

		// Token: 0x04001838 RID: 6200
		public uint HatId = uint.MaxValue;

		// Token: 0x04001839 RID: 6201
		public uint PetId = uint.MaxValue;

		// Token: 0x0400183A RID: 6202
		public uint SkinId = uint.MaxValue;

		// Token: 0x0400183B RID: 6203
		public bool Disconnected;

		// Token: 0x0400183C RID: 6204
		public List<GameData.TaskInfo> Tasks;

		// Token: 0x0400183D RID: 6205
		public bool IsImpostor;

		// Token: 0x0400183E RID: 6206
		public bool IsDead;

		// Token: 0x0400183F RID: 6207
		private PlayerControl _object;
	}
}
