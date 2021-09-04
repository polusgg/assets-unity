using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Hazel;
using Hazel.Udp;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace InnerNet
{
	// Token: 0x02000294 RID: 660
	public abstract class InnerNetClient : MonoBehaviour
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x000601E4 File Offset: 0x0005E3E4
		private void FixedUpdate()
		{
			//if (this.connection != null)
			//{
			//	this.connection.FixedUpdate();
			//}
			//if (this.mode == MatchMakerModes.None || this.Streams == null)
			//{
			//	this.timer = 0f;
			//	return;
			//}
			//this.timer += Time.fixedDeltaTime;
			//if (this.timer < this.MinSendInterval)
			//{
			//	return;
			//}
			//this.timer = 0f;
			//List<InnerNetObject> obj = this.allObjects;
			//lock (obj)
			//{
			//	for (int i = 0; i < this.allObjects.Count; i++)
			//	{
			//		InnerNetObject innerNetObject = this.allObjects[i];
			//		if (innerNetObject && innerNetObject.IsDirty && (innerNetObject.AmOwner || (innerNetObject.OwnerId == -2 && this.AmHost)))
			//		{
			//			MessageWriter messageWriter = this.Streams[innerNetObject.sendMode];
			//			messageWriter.StartMessage(1);
			//			messageWriter.WritePacked(innerNetObject.NetId);
			//			if (innerNetObject.Serialize(messageWriter, false))
			//			{
			//				messageWriter.EndMessage();
			//			}
			//			else
			//			{
			//				messageWriter.CancelMessage();
			//			}
			//		}
			//	}
			//}
			//for (int j = 0; j < this.Streams.Length; j++)
			//{
			//	MessageWriter messageWriter2 = this.Streams[j];
			//	if (messageWriter2.HasBytes(7))
			//	{
			//		messageWriter2.EndMessage();
			//		this.SendOrDisconnect(messageWriter2);
			//		messageWriter2.Clear((byte)j);
			//		messageWriter2.StartMessage(5);
			//		messageWriter2.Write(this.GameId);
			//	}
			//}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00060364 File Offset: 0x0005E564
		public T FindObjectByNetId<T>(uint netId) where T : InnerNetObject
		{
			InnerNetObject innerNetObject;
			if (this.allObjectsFast.TryGetValue(netId, out innerNetObject))
			{
				return (T)((object)innerNetObject);
			}
			return default(T);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00060394 File Offset: 0x0005E594
		public MessageWriter StartRpcImmediately(uint targetNetId, byte callId, SendOption option, int targetClientId = -1)
		{
			MessageWriter messageWriter = MessageWriter.Get(option);
			if (targetClientId < 0)
			{
				messageWriter.StartMessage(5);
				messageWriter.Write(this.GameId);
			}
			else
			{
				messageWriter.StartMessage(6);
				messageWriter.Write(this.GameId);
				messageWriter.WritePacked(targetClientId);
			}
			messageWriter.StartMessage(2);
			messageWriter.WritePacked(targetNetId);
			messageWriter.Write(callId);
			return messageWriter;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000603F3 File Offset: 0x0005E5F3
		public void FinishRpcImmediately(MessageWriter msg)
		{
			msg.EndMessage();
			msg.EndMessage();
			this.SendOrDisconnect(msg);
			msg.Recycle();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00060410 File Offset: 0x0005E610
		public void SendSelfClientInfoToAll()
		{
			//MessageWriter messageWriter = MessageWriter.Get(SendOption.Reliable);
			//messageWriter.StartMessage(5);
			//messageWriter.Write(this.GameId);
			//messageWriter.StartMessage(205);
			//messageWriter.WritePacked(this.ClientId);
			//messageWriter.WritePacked(Application.platform);
			//messageWriter.EndMessage();
			//messageWriter.EndMessage();
			//this.SendOrDisconnect(messageWriter);
			//messageWriter.Recycle();
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00060474 File Offset: 0x0005E674
		public void PS4_AskIfRoomExists()
		{
			//Debug.LogError("PS4_AskIfRoomExists");
			//MessageWriter messageWriter = MessageWriter.Get(1);
			//messageWriter.StartMessage(5);
			//messageWriter.Write(this.GameId);
			//messageWriter.StartMessage(206);
			//messageWriter.WritePacked(this.ClientId);
			//messageWriter.Write(1);
			//messageWriter.EndMessage();
			//messageWriter.EndMessage();
			//this.SendOrDisconnect(messageWriter);
			//messageWriter.Recycle();
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000604DC File Offset: 0x0005E6DC
		public void PS4_ReplyWithMyRoom()
		{
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000604DE File Offset: 0x0005E6DE
		public void SendRpc(uint targetNetId, byte callId, SendOption option = SendOption.Reliable)
		{
			this.StartRpc(targetNetId, callId, option).EndMessage();
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000604EE File Offset: 0x0005E6EE
		public MessageWriter StartRpc(uint targetNetId, byte callId, SendOption option = SendOption.Reliable)
		{
			//MessageWriter messageWriter = this.Streams[option];
			//messageWriter.StartMessage(2);
			//messageWriter.WritePacked(targetNetId);
			//messageWriter.Write(callId);
			//return messageWriter;
			return new MessageWriter(1);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0006050D File Offset: 0x0005E70D
		private void SendSceneChange(string sceneName)
		{
			this.InOnlineScene = string.Equals(sceneName, "OnlineGame");
			if (!this.AmConnected)
			{
				return;
			}
			Debug.Log("Changed To " + sceneName);
			base.StartCoroutine(this.CoSendSceneChange(sceneName));
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00060547 File Offset: 0x0005E747
		private IEnumerator CoSendSceneChange(string sceneName)
		{
			List<InnerNetObject> obj = this.allObjects;
			lock (obj)
			{
				for (int i = this.allObjects.Count - 1; i > -1; i--)
				{
					if (!this.allObjects[i])
					{
						this.allObjects.RemoveAt(i);
					}
				}
				goto IL_9C;
			}
			IL_85:
			yield return null;
			IL_9C:
			if (this.AmConnected && this.ClientId < 0)
			{
				goto IL_85;
			}
			if (!this.AmConnected)
			{
				yield break;
			}
			ClientData clientData = this.FindClientById(this.ClientId);
			if (clientData != null)
			{
				Debug.Log(string.Format("Self changed scene: {0} {1}", this.ClientId, sceneName));
				yield return this.CoOnPlayerChangedScene(clientData, sceneName);
			}
			else
			{
				Debug.Log(string.Format("Couldn't find self in clients: {0}: ", this.ClientId) + sceneName);
			}
			if (!this.AmHost && this.connection.State == ConnectionState.Connected)
			{
				//MessageWriter messageWriter = MessageWriter.Get(1);
				//messageWriter.StartMessage(5);
				//messageWriter.Write(this.GameId);
				//messageWriter.StartMessage(6);
				//messageWriter.WritePacked(this.ClientId);
				//messageWriter.Write(sceneName);
				//messageWriter.EndMessage();
				//messageWriter.EndMessage();
				//this.SendOrDisconnect(messageWriter);
				//messageWriter.Recycle();
			}
			yield break;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00060560 File Offset: 0x0005E760
		public void Spawn(InnerNetObject netObjParent, int ownerId = -2, SpawnFlags flags = SpawnFlags.None)
		{
			if (this.AmHost)
			{
				ownerId = ((ownerId == -3) ? this.ClientId : ownerId);
				MessageWriter msg = this.Streams[1];
				this.WriteSpawnMessage(netObjParent, ownerId, flags, msg);
				return;
			}
			if (!this.AmClient)
			{
				return;
			}
			Debug.LogError("Tried to spawn while not host:" + ((netObjParent != null) ? netObjParent.ToString() : null));
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000605C0 File Offset: 0x0005E7C0
		private void WriteSpawnMessage(InnerNetObject netObjParent, int ownerId, SpawnFlags flags, MessageWriter msg)
		{
			msg.StartMessage(4);
			msg.WritePacked(netObjParent.SpawnId);
			msg.WritePacked(ownerId);
			msg.Write((byte)flags);
			InnerNetObject[] componentsInChildren = netObjParent.GetComponentsInChildren<InnerNetObject>();
			msg.WritePacked(componentsInChildren.Length);
			foreach (InnerNetObject innerNetObject in componentsInChildren)
			{
				innerNetObject.OwnerId = ownerId;
				innerNetObject.SpawnFlags = flags;
				if (innerNetObject.NetId == 0U)
				{
					InnerNetObject innerNetObject2 = innerNetObject;
					uint netIdCnt = this.NetIdCnt;
					this.NetIdCnt = netIdCnt + 1U;
					innerNetObject2.NetId = netIdCnt;
					this.allObjects.Add(innerNetObject);
					this.allObjectsFast.Add(innerNetObject.NetId, innerNetObject);
				}
				msg.WritePacked(innerNetObject.NetId);
				msg.StartMessage(1);
				innerNetObject.Serialize(msg, true);
				msg.EndMessage();
			}
			msg.EndMessage();
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00060690 File Offset: 0x0005E890
		public void Despawn(InnerNetObject objToDespawn)
		{
			if (objToDespawn.NetId < 1U)
			{
				Debug.LogError("Tried to net destroy: " + ((objToDespawn != null) ? objToDespawn.ToString() : null));
				return;
			}
			MessageWriter messageWriter = this.Streams[1];
			messageWriter.StartMessage(5);
			messageWriter.WritePacked(objToDespawn.NetId);
			messageWriter.EndMessage();
			this.RemoveNetObject(objToDespawn);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000606EC File Offset: 0x0005E8EC
		private bool AddNetObject(InnerNetObject obj)
		{
			uint num = obj.NetId + 1U;
			if (num > this.NetIdCnt)
			{
				this.NetIdCnt = num;
			}
			if (!this.allObjectsFast.ContainsKey(obj.NetId))
			{
				this.allObjects.Add(obj);
				this.allObjectsFast.Add(obj.NetId, obj);
				return true;
			}
			return false;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00060748 File Offset: 0x0005E948
		public void RemoveNetObject(InnerNetObject obj)
		{
			int num = this.allObjects.IndexOf(obj);
			if (num > -1)
			{
				this.allObjects.RemoveAt(num);
			}
			this.allObjectsFast.Remove(obj.NetId);
			obj.NetId = uint.MaxValue;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0006078C File Offset: 0x0005E98C
		public void RemoveUnownedObjects()
		{
			HashSet<int> hashSet = new HashSet<int>();
			hashSet.Add(-2);
			List<ClientData> obj = this.allClients;
			lock (obj)
			{
				for (int i = 0; i < this.allClients.Count; i++)
				{
					ClientData clientData = this.allClients[i];
					if (clientData.Character)
					{
						hashSet.Add(clientData.Id);
					}
				}
			}
			List<InnerNetObject> obj2 = this.allObjects;
			lock (obj2)
			{
				for (int j = this.allObjects.Count - 1; j > -1; j--)
				{
					InnerNetObject innerNetObject = this.allObjects[j];
					if (!innerNetObject)
					{
						this.allObjects.RemoveAt(j);
					}
					else if (!hashSet.Contains(innerNetObject.OwnerId))
					{
						innerNetObject.OwnerId = this.ClientId;
						Object.Destroy(innerNetObject.gameObject);
					}
				}
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000608AC File Offset: 0x0005EAAC
		private void HandleGameData(MessageReader parentReader)
		{
			try
			{
				while (parentReader.Position < parentReader.Length)
				{
					MessageReader messageReader = parentReader.ReadMessageAsNewBuffer();
					MessageReader reader = messageReader;
					int num = this.msgNum;
					this.msgNum = num + 1;
					base.StartCoroutine(this.HandleGameDataInner(reader, num));
				}
			}
			finally
			{
				parentReader.Recycle();
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00060908 File Offset: 0x0005EB08
		private IEnumerator HandleGameDataInner(MessageReader reader, int msgNum)
		{
			int cnt = 0;
			reader.Position = 0;
			byte tag = reader.Tag;
			switch (tag)
			{
			case 1:
				try
				{
					InnerNetObject innerNetObject;
					for (;;)
					{
						uint num = reader.ReadPackedUInt32();
						if (this.allObjectsFast.TryGetValue(num, out innerNetObject))
						{
							break;
						}
						if (this.DestroyedObjects.Contains(num))
						{
							goto IL_148;
						}
						Debug.LogWarning("Stored data for " + num.ToString());
						int num2 = cnt;
						cnt = num2 + 1;
						if (num2 > 10)
						{
							goto Block_15;
						}
						reader.Position = 0;
						yield return Effects.Wait(0.1f);
					}
					innerNetObject.Deserialize(reader, false);
					goto IL_148;
					Block_15:
					yield break;
				}
				finally
				{
					reader.Recycle();
				}
				IL_148:
				goto IL_4A6;
			case 2:
				try
				{
					byte b;
					InnerNetObject innerNetObject2;
					for (;;)
					{
						uint num3;
						try
						{
							num3 = reader.ReadPackedUInt32();
						}
						catch (Exception ex)
						{
							Debug.LogError(string.Format("Error in {0} try {1}, Pos:{2}/{3}: {4}", new object[]
							{
								msgNum,
								cnt,
								reader.Position,
								reader.Length,
								ex
							}));
							Debug.LogException(ex, this);
							throw;
						}
						b = reader.ReadByte();
						if (this.allObjectsFast.TryGetValue(num3, out innerNetObject2))
						{
							break;
						}
						if (num3 == 4294967295U || this.DestroyedObjects.Contains(num3))
						{
							goto IL_2AA;
						}
						Debug.LogWarning(string.Format("Stored Msg {0} RPC {1} for ", msgNum, (RpcCalls)b) + num3.ToString());
						int num2 = cnt;
						cnt = num2 + 1;
						if (num2 > 10)
						{
							goto Block_22;
						}
						reader.Position = 0;
						yield return Effects.Wait(0.1f);
					}
					innerNetObject2.HandleRpc(b, reader);
					goto IL_2AA;
					Block_22:
					yield break;
				}
				finally
				{
					reader.Recycle();
				}
				IL_2AA:
				goto IL_4A6;
			case 3:
				goto IL_416;
			case 4:
				base.StartCoroutine(this.CoHandleSpawn(reader));
				goto IL_4A6;
			case 5:
				try
				{
					uint num4 = reader.ReadPackedUInt32();
					this.DestroyedObjects.Add(num4);
					InnerNetObject innerNetObject3 = this.FindObjectByNetId<InnerNetObject>(num4);
					if (innerNetObject3 && !innerNetObject3.AmOwner)
					{
						this.RemoveNetObject(innerNetObject3);
						Object.Destroy(innerNetObject3.gameObject);
					}
					yield break;
				}
				finally
				{
					reader.Recycle();
				}
			case 6:
				break;
			case 7:
				try
				{
					ClientData clientData = this.FindClientById(reader.ReadPackedInt32());
					if (clientData != null)
					{
						clientData.IsReady = true;
					}
					yield break;
				}
				finally
				{
					reader.Recycle();
				}
			// goto IL_3CA;
			default:
				if (tag == 205)
				{
					goto IL_3CA;
				}
				if (tag != 206)
				{
					goto IL_416;
				}
				goto IL_3DC;
			}
			int num5 = reader.ReadPackedInt32();
			ClientData clientData2 = this.FindClientById(num5);
			string text = reader.ReadString();
			if (clientData2 != null && !string.IsNullOrWhiteSpace(text))
			{
				base.StartCoroutine(this.CoOnPlayerChangedScene(clientData2, text));
				goto IL_4A6;
			}
			Debug.Log(string.Format("Couldn't find client {0} to change scene to {1}", num5, text));
			reader.Recycle();
			goto IL_4A6;
			IL_3CA:
			try
			{
				yield break;
			}
			finally
			{
				reader.Recycle();
			}
			IL_3DC:
			try
			{
				Debug.Log("Client " + reader.ReadPackedUInt32().ToString() + " asked if a PS4 room exists, but we're not on PS4 so we don't care");
				yield break;
			}
			finally
			{
				reader.Recycle();
			}
			IL_416:
			Debug.Log(string.Format("Bad tag {0} at {1}+{2}={3}:  ", new object[]
			{
				reader.Tag,
				reader.Offset,
				reader.Position,
				reader.Length
			}) + string.Join<byte>(" ", reader.Buffer.Take(128)));
			reader.Recycle();
			IL_4A6:
			yield break;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00060925 File Offset: 0x0005EB25
		private IEnumerator CoHandleSpawn(MessageReader reader)
		{
			uint spawnId = reader.ReadPackedUInt32();
			if ((ulong)spawnId < (ulong)((long)this.SpawnableObjects.Length))
			{
				int ownerId = reader.ReadPackedInt32();
				ClientData ownerClient = this.FindClientById(ownerId);
				int frames = 0;
				while (frames < 1000 && ownerId > 0 && ownerClient == null)
				{
					Debug.LogWarning("Delay spawn for unowned " + spawnId.ToString());
					yield return null;
					ownerClient = this.FindClientById(ownerId);
					int num = frames + 1;
					frames = num;
				}
				if (ownerId > 0 && ownerClient == null)
				{
					yield break;
				}
				InnerNetObject innerNetObject = this.NonAddressableSpawnableObjects.FirstOrDefault((InnerNetObject f) => f.SpawnId == spawnId);
				InnerNetObject innerNetObject2;
				if (innerNetObject)
				{
					innerNetObject2 = Object.Instantiate<InnerNetObject>(innerNetObject);
				}
				else
				{
					AsyncOperationHandle<GameObject> spawnPrefab = this.SpawnableObjects[(int)spawnId].InstantiateAsync(null, false);
					yield return spawnPrefab;
					innerNetObject2 = spawnPrefab.Result.GetComponent<InnerNetObject>();
					spawnPrefab = default(AsyncOperationHandle<GameObject>);
				}
				innerNetObject2.SpawnFlags = (SpawnFlags)reader.ReadByte();
				if ((innerNetObject2.SpawnFlags & SpawnFlags.IsClientCharacter) != SpawnFlags.None)
				{
					if (!ownerClient.Character)
					{
						ownerClient.InScene = true;
						ownerClient.Character = (innerNetObject2 as PlayerControl);
					}
					else if (innerNetObject2)
					{
						Debug.LogWarning(string.Format("Double spawn character: {0} already has {1}", ownerClient.Id, ownerClient.Character.NetId));
						Object.Destroy(innerNetObject2.gameObject);
						yield break;
					}
				}
				int num2 = reader.ReadPackedInt32();
				InnerNetObject[] componentsInChildren = innerNetObject2.GetComponentsInChildren<InnerNetObject>();
				if (num2 != componentsInChildren.Length)
				{
					Debug.LogError(string.Format("Children didn't match for spawnable {0} ({1}): {2} != {3}", new object[]
					{
						spawnId,
						innerNetObject2,
						num2,
						componentsInChildren.Length
					}));
					Object.Destroy(innerNetObject2.gameObject);
					yield break;
				}
				for (int i = 0; i < num2; i++)
				{
					InnerNetObject innerNetObject3 = componentsInChildren[i];
					innerNetObject3.NetId = reader.ReadPackedUInt32();
					innerNetObject3.OwnerId = ownerId;
					if (this.DestroyedObjects.Contains(innerNetObject3.NetId))
					{
						innerNetObject2.NetId = uint.MaxValue;
						Object.Destroy(innerNetObject2.gameObject);
						break;
					}
					if (!this.AddNetObject(innerNetObject3))
					{
						innerNetObject2.NetId = uint.MaxValue;
						Object.Destroy(innerNetObject2.gameObject);
						break;
					}
					MessageReader messageReader = reader.ReadMessage();
					if (messageReader.Length > 0)
					{
						innerNetObject3.Deserialize(messageReader, true);
					}
				}
				ownerClient = null;
			}
			else
			{
				Debug.LogError("Couldn't find spawnable prefab: " + spawnId.ToString());
			}
			yield break;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0006093B File Offset: 0x0005EB3B
		public void SetEndpoint(string addr, ushort port)
		{
			this.networkAddress = addr;
			this.networkPort = (int)port;
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0006094B File Offset: 0x0005EB4B
		public bool AmConnected
		{
			get
			{
				return this.connection != null;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00060956 File Offset: 0x0005EB56
		public int Ping
		{
			get
			{
				if (this.connection == null)
				{
					return 0;
				}
				return (int)this.connection.AveragePingMs;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0006096E File Offset: 0x0005EB6E
		public int BytesSent
		{
			get
			{
				if (this.connection == null)
				{
					return 0;
				}
				return (int)this.connection.Statistics.TotalBytesSent;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0006098B File Offset: 0x0005EB8B
		public int BytesGot
		{
			get
			{
				if (this.connection == null)
				{
					return 0;
				}
				return (int)this.connection.Statistics.TotalBytesReceived;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x000609A8 File Offset: 0x0005EBA8
		public int Resends
		{
			get
			{
				if (this.connection == null)
				{
					return 0;
				}
				return this.connection.Statistics.MessagesResent;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000609C4 File Offset: 0x0005EBC4
		public bool AmHost
		{
			get
			{
				return this.HostId == this.ClientId;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x000609D4 File Offset: 0x0005EBD4
		public bool AmClient
		{
			get
			{
				return this.ClientId > 0;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000609DF File Offset: 0x0005EBDF
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x000609E7 File Offset: 0x0005EBE7
		public bool IsGamePublic { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000609F0 File Offset: 0x0005EBF0
		public bool IsGameStarted
		{
			get
			{
				return this.GameState == InnerNetClient.GameStates.Started;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000609FB File Offset: 0x0005EBFB
		public bool IsGameOver
		{
			get
			{
				return this.GameState == InnerNetClient.GameStates.Ended;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00060A06 File Offset: 0x0005EC06
		public virtual void Start()
		{
			SceneManager.activeSceneChanged += delegate(Scene oldScene, Scene scene)
			{
				this.SendSceneChange(scene.name);
			};
			this.ClientId = -1;
			this.GameId = 32;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00060A28 File Offset: 0x0005EC28
		private void SendOrDisconnect(MessageWriter msg)
		{
			try
			{
				this.connection.Send(msg);
			}
			catch (Exception ex)
			{
				Debug.Log("Failed to send message: " + ex.Message);
				Debug.LogException(ex, this);
				this.EnqueueDisconnect(DisconnectReasons.Error, "Failed to send message: " + ex.Message);
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00060A8C File Offset: 0x0005EC8C
		public ClientData GetHost()
		{
			List<ClientData> obj = this.allClients;
			lock (obj)
			{
				for (int i = 0; i < this.allClients.Count; i++)
				{
					ClientData clientData = this.allClients[i];
					if (clientData.Id == this.HostId)
					{
						return clientData;
					}
				}
			}
			return null;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00060B00 File Offset: 0x0005ED00
		public int GetClientIdFromCharacter(InnerNetObject character)
		{
			if (!character)
			{
				return -1;
			}
			List<ClientData> obj = this.allClients;
			lock (obj)
			{
				for (int i = 0; i < this.allClients.Count; i++)
				{
					ClientData clientData = this.allClients[i];
					if (clientData.Character == character)
					{
						return clientData.Id;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00060B84 File Offset: 0x0005ED84
		public virtual void OnDestroy()
		{
			if (this.AmConnected)
			{
				this.DisconnectInternal(DisconnectReasons.Destroy, null);
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00060B97 File Offset: 0x0005ED97
		public IEnumerator CoConnect()
		{
			//if (this.AmConnected)
			//{
			//	yield break;
			//}
			//for (;;)
			//{
			//	string ipAddr = this.networkAddress;
			//	DestroyableSingleton<DisconnectPopup>.Instance.Close();
			//	this.LastDisconnectReason = DisconnectReasons.ExitGame;
			//	this.NetIdCnt = 1U;
			//	this.DestroyedObjects.Clear();
			//	if (this.GameMode == GameModes.OnlineGame)
			//	{
			//		if (EOSManager.Instance.ClientId == null)
			//		{
			//			break;
			//		}
			//		yield return DestroyableSingleton<AuthManager>.Instance.CoConnect(ipAddr, (ushort)(this.networkPort + 2));
			//		yield return DestroyableSingleton<AuthManager>.Instance.CoWaitForNonce(5f);
			//	}
			//	IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Parse(ipAddr), this.networkPort);
			//	this.connection = new UnityUdpClientConnection(ipendPoint, 0);
			//	this.connection.KeepAliveInterval = 1000;
			//	this.connection.DisconnectTimeout = 7500;
			//	this.connection.ResendPingMultiplier = 1.2f;
			//	this.connection.DataReceived += this.OnMessageReceived;
			//	this.connection.Disconnected += this.OnDisconnect;
			//	this.connection.ConnectAsync(this.GetConnectionData());
			//	while (this.connection != null && this.connection.State == 1)
			//	{
			//		yield return null;
			//	}
			//	if ((this.connection != null && this.connection.State == 2) || this.LastDisconnectReason == DisconnectReasons.IncorrectVersion || this.LastDisconnectReason == DisconnectReasons.InvalidName)
			//	{
			//		goto IL_20C;
			//	}
			//	Debug.Log(string.Format("Failed to connected to: {0}:{1}", ipAddr, this.networkPort));
			//	if (!DestroyableSingleton<ServerManager>.Instance.TrackServerFailure(ipAddr))
			//	{
			//		goto IL_20C;
			//	}
			//	this.DisconnectInternal(DisconnectReasons.NewConnection, null);
			//}
			//this.EnqueueDisconnect(DisconnectReasons.NotAuthorized, null);
			//yield break;
			//IL_20C:
			yield break;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00060BA6 File Offset: 0x0005EDA6
		private void Connection_DataReceivedRaw(byte[] data)
		{
			Debug.Log("Client Got: " + string.Join(" ", from b in data
			select b.ToString()));
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00060BE8 File Offset: 0x0005EDE8
		private void Connection_DataSentRaw(byte[] data, int length)
		{
			Debug.Log("Client Sent: " + string.Join(" ", (from b in data
			select b.ToString()).ToArray<string>(), 0, length));
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00060C3A File Offset: 0x0005EE3A
		public void Connect(MatchMakerModes mode)
		{
			base.StartCoroutine(this.CoConnect(mode));
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00060C4A File Offset: 0x0005EE4A
		private IEnumerator CoConnect(MatchMakerModes mode)
		{
			if (this.mode != MatchMakerModes.None)
			{
				this.DisconnectInternal(DisconnectReasons.NewConnection, null);
				yield return Effects.Wait(0.1f);
			}
			this.mode = mode;
			yield return this.CoConnect();
			if (this.connection == null || this.connection.State != ConnectionState.Connected)
			{
				yield break;
			}
			MatchMakerModes matchMakerModes = this.mode;
			if (matchMakerModes == MatchMakerModes.Client)
			{
				this.JoinGame();
				yield return this.WaitWithTimeout(() => this.ClientId >= 0, "Failed to join game. Try again later.", 15);
				bool amConnected = this.AmConnected;
				yield break;
			}
			if (matchMakerModes != MatchMakerModes.HostAndClient)
			{
				yield break;
			}
			this.GameId = 0;
			PlayerControl.GameOptions = SaveManager.GameHostOptions;
			this.HostGame(PlayerControl.GameOptions);
			yield return this.WaitWithTimeout(() => this.GameId != 0, "Failed to create a game. Try again later.", 15);
			if (!this.AmConnected)
			{
				yield break;
			}
			this.JoinGame();
			yield return this.WaitWithTimeout(() => this.ClientId >= 0, "Failed to join game after creating it. Try again later.", 15);
			bool amConnected2 = this.AmConnected;
			yield break;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00060C60 File Offset: 0x0005EE60
		public IEnumerator WaitForConnectionOrFail()
		{
			while (this.AmConnected)
			{
				switch (this.mode)
				{
				case MatchMakerModes.None:
					goto IL_5F;
				case MatchMakerModes.Client:
					if (this.ClientId >= 0)
					{
						yield break;
					}
					break;
				case MatchMakerModes.HostAndClient:
					if (this.GameId != 0 && this.ClientId >= 0)
					{
						yield break;
					}
					break;
				default:
					goto IL_5F;
				}
				yield return null;
				continue;
				IL_5F:
				yield break;
			}
			yield break;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00060C6F File Offset: 0x0005EE6F
		private IEnumerator WaitWithTimeout(Func<bool> success, string errorMessage, int durationSeconds = 15)
		{
			bool failed = true;
			for (float timer = 0f; timer < (float)durationSeconds; timer += Time.deltaTime)
			{
				if (success())
				{
					failed = false;
					break;
				}
				if (!this.AmConnected)
				{
					yield break;
				}
				yield return null;
			}
			if (failed && errorMessage != null)
			{
				this.LastCustomDisconnect = errorMessage;
				this.EnqueueDisconnect(DisconnectReasons.Custom, "Couldn't connect");
			}
			yield break;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00060C94 File Offset: 0x0005EE94
		public void Update()
		{
			//if (Input.GetKeyDown(13) && (Input.GetKey(308) || Input.GetKey(307)))
			//{
			//	ResolutionManager.ToggleFullscreen();
			//}
			//this.TempQueue.Clear();
			//List<Action> obj = this.Dispatcher;
			//lock (obj)
			//{
			//	this.TempQueue.AddAll(this.Dispatcher);
			//	this.Dispatcher.Clear();
			//}
			//for (int i = 0; i < this.TempQueue.Count; i++)
			//{
			//	Action action = this.TempQueue[i];
			//	try
			//	{
			//		action();
			//	}
			//	catch (Exception ex)
			//	{
			//		Debug.LogError("InnerNetClient::Update Exception 1");
			//		Debug.LogException(ex, this);
			//	}
			//}
			//if (this.InOnlineScene)
			//{
			//	this.TempQueue.Clear();
			//	obj = this.PreSpawnDispatcher;
			//	lock (obj)
			//	{
			//		this.TempQueue.AddAll(this.PreSpawnDispatcher);
			//		this.PreSpawnDispatcher.Clear();
			//	}
			//	for (int j = 0; j < this.TempQueue.Count; j++)
			//	{
			//		Action action2 = this.TempQueue[j];
			//		try
			//		{
			//			action2();
			//		}
			//		catch (Exception ex2)
			//		{
			//			Debug.LogError("InnerNetClient::Update Exception 2");
			//			Debug.LogException(ex2, this);
			//		}
			//	}
			//}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00060E10 File Offset: 0x0005F010
		private void OnDisconnect(object sender, DisconnectedEventArgs e)
		{
			MessageReader message = e.Message;
			if (message != null && message.Position < message.Length)
			{
				if (message.ReadByte() == 1)
				{
					MessageReader messageReader = message.ReadMessage();
					DisconnectReasons disconnectReasons = (DisconnectReasons)messageReader.ReadByte();
					if (disconnectReasons == DisconnectReasons.Custom)
					{
						this.LastCustomDisconnect = messageReader.ReadString();
						if (string.IsNullOrWhiteSpace(this.LastCustomDisconnect))
						{
							this.LastCustomDisconnect = "The server disconnected you without a specific error message.";
						}
					}
					this.EnqueueDisconnect(disconnectReasons, null);
					return;
				}
				this.LastCustomDisconnect = "Forcibly disconnected from the server:\r\n\r\n" + (e.Reason ?? "Null");
				this.EnqueueDisconnect(DisconnectReasons.Custom, null);
				return;
			}
			else
			{
				if (e.Reason == null)
				{
					this.LastCustomDisconnect = "You disconnected from the server.\r\n\r\nNull";
					this.EnqueueDisconnect(DisconnectReasons.Custom, e.Reason);
					return;
				}
				if (!e.Reason.Contains("The remote sent a"))
				{
					this.LastCustomDisconnect = "You disconnected from the server.\r\n\r\n" + e.Reason;
					this.EnqueueDisconnect(DisconnectReasons.Custom, e.Reason);
					return;
				}
				this.EnqueueDisconnect(DisconnectReasons.Error, e.Reason);
				return;
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00060F0A File Offset: 0x0005F10A
		public void HandleDisconnect(DisconnectReasons reason, string stringReason = null)
		{
			if (AmongUsClient.Instance.GameMode == GameModes.FreePlay)
			{
				return;
			}
			base.StopAllCoroutines();
			this.DisconnectInternal(reason, stringReason);
			this.OnDisconnected();
			this.GameId = -1;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00060F38 File Offset: 0x0005F138
		protected void EnqueueDisconnect(DisconnectReasons reason, string stringReason = null)
		{
			UnityUdpClientConnection unityUdpClientConnection = this.connection;
			List<Action> dispatcher = this.Dispatcher;
			lock (dispatcher)
			{
				this.Dispatcher.Add(delegate
				{
					this.HandleDisconnect(reason, stringReason);
				});
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00060FAC File Offset: 0x0005F1AC
		protected void DisconnectInternal(DisconnectReasons reason, string stringReason = null)
		{
			Debug.Log(string.Format("Client DC because {0}: {1}", reason, stringReason ?? "null"));
			if (reason != DisconnectReasons.NewConnection && reason != DisconnectReasons.FocusLostBackground)
			{
				this.LastDisconnectReason = reason;
				if (reason != DisconnectReasons.ExitGame && DestroyableSingleton<DisconnectPopup>.InstanceExists)
				{
					DestroyableSingleton<DisconnectPopup>.Instance.Show();
				}
			}
			if (this.mode == MatchMakerModes.HostAndClient)
			{
				this.GameId = 0;
			}
			if (this.mode == MatchMakerModes.Client || this.mode == MatchMakerModes.HostAndClient)
			{
				this.ClientId = -1;
			}
			this.mode = MatchMakerModes.None;
			this.GameState = InnerNetClient.GameStates.NotJoined;
			UnityUdpClientConnection unityUdpClientConnection = Interlocked.Exchange<UnityUdpClientConnection>(ref this.connection, null);
			if (unityUdpClientConnection != null)
			{
				try
				{
					unityUdpClientConnection.Dispose();
				}
				catch (Exception ex)
				{
					Debug.LogError("InnerNetClient::DisconnectInternal Exception");
					Debug.LogException(ex, this);
				}
			}
			if (DestroyableSingleton<InnerNetServer>.InstanceExists)
			{
				DestroyableSingleton<InnerNetServer>.Instance.StopServer();
			}
			List<Action> obj = this.Dispatcher;
			lock (obj)
			{
				this.Dispatcher.Clear();
			}
			obj = this.PreSpawnDispatcher;
			lock (obj)
			{
				this.PreSpawnDispatcher.Clear();
			}
			if (reason != DisconnectReasons.Error)
			{
				this.TempQueue.Clear();
			}
			this.allObjects.Clear();
			this.allClients.Clear();
			this.allObjectsFast.Clear();
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00061120 File Offset: 0x0005F320
		public void HostGame(GameOptionsData settings)
		{
			//this.IsGamePublic = false;
			//MessageWriter messageWriter = MessageWriter.Get(1);
			//messageWriter.StartMessage(0);
			//messageWriter.WriteBytesAndSize(settings.ToBytes(2));
			//messageWriter.Write((byte)SaveManager.ChatModeType);
			//messageWriter.EndMessage();
			//this.SendOrDisconnect(messageWriter);
			//messageWriter.Recycle();
			//Debug.Log("Client requesting new game.");
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00061178 File Offset: 0x0005F378
		public void ReportPlayer(int clientId, ReportReasons reason)
		{
			//ClientData client = this.GetClient(clientId);
			//if (client != null)
			//{
			//	client.HasBeenReported = true;
			//}
			//MessageWriter messageWriter = MessageWriter.Get(1);
			//messageWriter.StartMessage(17);
			//messageWriter.Write(this.GameId);
			//messageWriter.WritePacked(clientId);
			//messageWriter.Write((byte)reason);
			//messageWriter.EndMessage();
			//this.SendOrDisconnect(messageWriter);
			//messageWriter.Recycle();
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000611D4 File Offset: 0x0005F3D4
		public void JoinGame()
		{
			//this.ClientId = -1;
			//if (!this.AmConnected)
			//{
			//	this.LastCustomDisconnect = "Disconnected before joining game.";
			//	this.HandleDisconnect(DisconnectReasons.Custom, null);
			//	return;
			//}
			//if (this.Streams == null)
			//{
			//	this.Streams = new MessageWriter[2];
			//	for (int i = 0; i < this.Streams.Length; i++)
			//	{
			//		this.Streams[i] = MessageWriter.Get((byte)i);
			//	}
			//}
			//for (int j = 0; j < this.Streams.Length; j++)
			//{
			//	MessageWriter messageWriter = this.Streams[j];
			//	messageWriter.Clear((byte)j);
			//	messageWriter.StartMessage(5);
			//	messageWriter.Write(this.GameId);
			//}
			//Debug.Log(string.Format("Client joining game: {0}/{1}", this.GameId, GameCode.IntToGameName(this.GameId)));
			//MessageWriter messageWriter2 = MessageWriter.Get(1);
			//messageWriter2.StartMessage(1);
			//messageWriter2.Write(this.GameId);
			//messageWriter2.EndMessage();
			//this.SendOrDisconnect(messageWriter2);
			//messageWriter2.Recycle();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000612C1 File Offset: 0x0005F4C1
		public bool CanBan()
		{
			return this.AmHost && !this.IsGameStarted;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000612D6 File Offset: 0x0005F4D6
		public bool CanKick()
		{
			return this.IsGameStarted || this.AmHost;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000612E8 File Offset: 0x0005F4E8
		public void KickPlayer(int clientId, bool ban)
		{
			//if (!this.AmHost)
			//{
			//	return;
			//}
			//MessageWriter messageWriter = MessageWriter.Get(1);
			//messageWriter.StartMessage(11);
			//messageWriter.Write(this.GameId);
			//messageWriter.WritePacked(clientId);
			//messageWriter.Write(ban);
			//messageWriter.EndMessage();
			//this.SendOrDisconnect(messageWriter);
			//messageWriter.Recycle();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0006133A File Offset: 0x0005F53A
		public MessageWriter StartEndGame()
		{
			//MessageWriter messageWriter = MessageWriter.Get(1);
			//messageWriter.StartMessage(8);
			//messageWriter.Write(this.GameId);
			//return messageWriter;

			return new MessageWriter(1);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00061355 File Offset: 0x0005F555
		public void FinishEndGame(MessageWriter msg)
		{
			msg.EndMessage();
			this.SendOrDisconnect(msg);
			msg.Recycle();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0006136C File Offset: 0x0005F56C
		protected void SendLateRejection(int targetId, DisconnectReasons reason)
		{
			//MessageWriter messageWriter = MessageWriter.Get(1);
			//messageWriter.StartMessage(4);
			//messageWriter.Write(this.GameId);
			//messageWriter.WritePacked(targetId);
			//messageWriter.Write((byte)reason);
			//messageWriter.EndMessage();
			//this.SendOrDisconnect(messageWriter);
			//messageWriter.Recycle();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000613B8 File Offset: 0x0005F5B8
		protected void SendClientReady()
		{
			if (!this.AmHost)
			{
				MessageWriter messageWriter = MessageWriter.Get(SendOption.Reliable);
				messageWriter.StartMessage(5);
				messageWriter.Write(this.GameId);
				messageWriter.StartMessage(7);
				messageWriter.WritePacked(this.ClientId);
				messageWriter.EndMessage();
				messageWriter.EndMessage();
				this.SendOrDisconnect(messageWriter);
				messageWriter.Recycle();
				return;
			}
			ClientData clientData = this.FindClientById(this.ClientId);
			if (clientData == null)
			{
				this.HandleDisconnect(DisconnectReasons.Error, "Couldn't find self as host");
				return;
			}
			clientData.IsReady = true;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006143C File Offset: 0x0005F63C
		protected void SendStartGame()
		{
			MessageWriter messageWriter = MessageWriter.Get(SendOption.Reliable);
			messageWriter.StartMessage(2);
			messageWriter.Write(this.GameId);
			messageWriter.EndMessage();
			this.SendOrDisconnect(messageWriter);
			messageWriter.Recycle();
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00061478 File Offset: 0x0005F678
		public void RequestGameList(bool includePrivate, GameOptionsData settings)
		{
			MessageWriter messageWriter = MessageWriter.Get(SendOption.Reliable);
			messageWriter.StartMessage(16);
			messageWriter.WritePacked(2);
			messageWriter.WriteBytesAndSize(settings.ToBytes(2));
			messageWriter.Write((byte)SaveManager.ChatModeType);
			messageWriter.EndMessage();
			this.SendOrDisconnect(messageWriter);
			messageWriter.Recycle();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000614C8 File Offset: 0x0005F6C8
		public void ChangeGamePublic(bool isPublic)
		{
			if (this.AmHost)
			{
				MessageWriter messageWriter = MessageWriter.Get(SendOption.Reliable);
				messageWriter.StartMessage(10);
				messageWriter.Write(this.GameId);
				messageWriter.Write(1);
				messageWriter.Write(isPublic);
				messageWriter.EndMessage();
				this.SendOrDisconnect(messageWriter);
				messageWriter.Recycle();
				this.IsGamePublic = isPublic;
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00061520 File Offset: 0x0005F720
		private void OnMessageReceived(DataReceivedEventArgs e)
		{
			MessageReader message = e.Message;
			try
			{
				while (message.Position < message.Length)
				{
					this.HandleMessage(message.ReadMessage(), e.SendOption);
				}
			}
			finally
			{
				message.Recycle();
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00061570 File Offset: 0x0005F770
		private void HandleMessage(MessageReader reader, SendOption sendOption)
		{
			List<Action> obj;
			switch (reader.Tag)
			{
			case 0:
				break;
			case 1:
				goto IL_37B;
			case 2:
				this.GameState = InnerNetClient.GameStates.Started;
				obj = this.Dispatcher;
				lock (obj)
				{
					this.Dispatcher.Add(delegate
					{
						base.StartCoroutine(this.CoStartGame());
					});
					return;
				}
			// goto IL_752;
			case 3:
			{
				DisconnectReasons reason3 = DisconnectReasons.ServerRequest;
				if (reader.Position < reader.Length)
				{
					reason3 = (DisconnectReasons)reader.ReadByte();
				}
				this.EnqueueDisconnect(reason3, null);
				return;
			}
			case 4:
				goto IL_12B;
			case 5:
			case 6:
			{
				int num = reader.ReadInt32();
				if (this.GameId != num)
				{
					return;
				}
				if (reader.Tag == 6)
				{
					int num2 = reader.ReadPackedInt32();
					if (this.ClientId != num2)
					{
						Debug.LogError(string.Format("Got data meant for {0}", num2));
						return;
					}
				}
				if (this.InOnlineScene)
				{
					MessageReader subReader = MessageReader.Get(reader);
					obj = this.Dispatcher;
					lock (obj)
					{
						this.Dispatcher.Add(delegate
						{
							this.HandleGameData(subReader);
						});
						return;
					}
				}
				#pragma warning disable CS0472
				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				if (sendOption != null)
				#pragma warning restore CS0472
				{
					MessageReader subReader = MessageReader.Get(reader);
					obj = this.PreSpawnDispatcher;
					lock (obj)
					{
						this.PreSpawnDispatcher.Add(delegate
						{
							this.HandleGameData(subReader);
						});
						return;
					}
		//			goto IL_599;
				}
				return;
			}
			case 7:
				goto IL_2BB;
			case 8:
			{
				int num3 = reader.ReadInt32();
				if (this.GameId == num3 && this.GameState != InnerNetClient.GameStates.Ended)
				{
					this.GameState = InnerNetClient.GameStates.Ended;
					List<ClientData> obj2 = this.allClients;
					lock (obj2)
					{
						this.allClients.Clear();
					}
					GameOverReason reason = (GameOverReason)reader.ReadByte();
					bool showAd = reader.ReadBoolean();
					obj = this.Dispatcher;
					lock (obj)
					{
						this.Dispatcher.Add(delegate
						{
							this.OnGameEnd(reason, showAd);
						});
						return;
					}
		//			goto IL_263;
				}
				return;
			}
			case 9:
			case 15:
				goto IL_80D;
			case 10:
				goto IL_69D;
			case 11:
				goto IL_752;
			case 12:
				goto IL_263;
			case 13:
			{
				uint address = reader.ReadUInt32();
				ushort port = reader.ReadUInt16();
				obj = this.Dispatcher;
				lock (obj)
				{
					this.Dispatcher.Add(delegate
					{
						AmongUsClient.Instance.SetEndpoint(InnerNetClient.AddressToString(address), port);
						Debug.Log(string.Format("Redirected to: {0}:{1}", this.networkAddress, this.networkPort));
						this.StopAllCoroutines();
						this.Connect(this.mode);
					});
					return;
				}
	//			goto IL_80D;
			}
			case 14:
				return;
			case 16:
				goto IL_599;
			case 17:
			{
				int clientId = reader.ReadPackedInt32();
				ReportReasons reason = (ReportReasons)reader.ReadInt32();
				ReportOutcome outcome = (ReportOutcome)reader.ReadByte();
				string playerName = reader.ReadString();
				obj = this.Dispatcher;
				lock (obj)
				{
					this.Dispatcher.Add(delegate
					{
						this.OnReportedPlayer(outcome, clientId, playerName, reason);
					});
					return;
				}
	//			break;
			}
			default:
				goto IL_80D;
			}
			this.GameId = reader.ReadInt32();
			Debug.Log("Client hosting game: " + GameCode.IntToGameName(this.GameId));
			obj = this.Dispatcher;
			lock (obj)
			{
				this.Dispatcher.Add(delegate
				{
					this.OnGameCreated(GameCode.IntToGameName(this.GameId));
				});
				return;
			}
			IL_12B:
			int num4 = reader.ReadInt32();
			if (this.GameId == num4)
			{
				int playerIdThatLeft = reader.ReadInt32();
				int hostId = reader.ReadInt32();
				DisconnectReasons reason2 = (DisconnectReasons)reader.ReadByte();
				if (!this.AmHost)
				{
					this.HostId = hostId;
					if (this.AmHost)
					{
						obj = this.Dispatcher;
						lock (obj)
						{
							this.Dispatcher.Add(delegate
							{
								this.OnBecomeHost();
							});
						}
					}
				}
				this.RemovePlayer(playerIdThatLeft, reason2);
				return;
			}
			return;
			IL_263:
			int num5 = reader.ReadInt32();
			if (this.GameId != num5)
			{
				return;
			}
			this.ClientId = reader.ReadInt32();
			obj = this.Dispatcher;
			lock (obj)
			{
				this.Dispatcher.Add(delegate
				{
					this.OnWaitForHost(GameCode.IntToGameName(this.GameId));
				});
				return;
			}
			IL_2BB:
			int num6 = reader.ReadInt32();
			if (this.GameId != num6 || this.GameState == InnerNetClient.GameStates.Joined)
			{
				return;
			}
			this.GameState = InnerNetClient.GameStates.Joined;
			this.ClientId = reader.ReadInt32();
			ClientData myClient = this.GetOrCreateClient(this.ClientId);
			this.HostId = reader.ReadInt32();
			int num7 = reader.ReadPackedInt32();
			for (int i = 0; i < num7; i++)
			{
				this.GetOrCreateClient(reader.ReadPackedInt32());
			}
			obj = this.Dispatcher;
			lock (obj)
			{
				this.Dispatcher.Add(delegate
				{
					this.OnGameJoined(GameCode.IntToGameName(this.GameId), myClient);
				});
				return;
			}
			IL_37B:
			int num8 = reader.ReadInt32();
			DisconnectReasons disconnectReasons = (DisconnectReasons)num8;
			if (InnerNetClient.disconnectReasons.Contains(disconnectReasons))
			{
				if (disconnectReasons == DisconnectReasons.Custom)
				{
					this.LastCustomDisconnect = reader.ReadString();
				}
				this.GameId = -1;
				this.EnqueueDisconnect(disconnectReasons, null);
				return;
			}
			if (this.GameId == num8)
			{
				int num9 = reader.ReadInt32();
				bool amHost = this.AmHost;
				this.HostId = reader.ReadInt32();
				ClientData client = this.GetOrCreateClient(num9);
				Debug.Log(string.Format("Player {0} joined", num9));
				obj = this.Dispatcher;
				lock (obj)
				{
					this.Dispatcher.Add(delegate
					{
						this.OnPlayerJoined(client);
					});
				}
				if (!this.AmHost || amHost)
				{
					return;
				}
				obj = this.Dispatcher;
				lock (obj)
				{
					this.Dispatcher.Add(delegate
					{
						this.OnBecomeHost();
					});
					return;
				}
			}
			this.EnqueueDisconnect(DisconnectReasons.IncorrectGame, null);
			return;
			IL_599:
			InnerNetClient.TotalGameData totals = new InnerNetClient.TotalGameData();
			List<GameListing> output = new List<GameListing>();
			while (reader.Position < reader.Length)
			{
				MessageReader messageReader = reader.ReadMessage();
				byte tag = messageReader.Tag;
				if (tag != 0)
				{
					if (tag == 1)
					{
						totals.PerMapTotals = new int[3];
						for (int j = 0; j < totals.PerMapTotals.Length; j++)
						{
							totals.PerMapTotals[j] = messageReader.ReadInt32();
						}
					}
				}
				else
				{
					while (messageReader.Position < messageReader.Length)
					{
						GameListing item = GameListing.DeserializeV2(messageReader.ReadMessage());
						output.Add(item);
					}
				}
			}
			obj = this.Dispatcher;
			lock (obj)
			{
				this.Dispatcher.Add(delegate
				{
					this.OnGetGameList(totals, output);
				});
				return;
			}
			IL_69D:
			int num10 = reader.ReadInt32();
			if (this.GameId != num10)
			{
				return;
			}
			if (reader.ReadByte() == 1)
			{
				this.IsGamePublic = reader.ReadBoolean();
				string str = "Alter Public = ";
				bool flag = this.IsGamePublic;
				Debug.Log(str + flag.ToString());
				return;
			}
			Debug.Log("Alter unknown");
			return;
			IL_752:
			int num11 = reader.ReadInt32();
			if (this.GameId != num11 || reader.ReadPackedInt32() != this.ClientId)
			{
				return;
			}
			bool flag2 = reader.ReadBoolean();
			if (reader.Position < reader.Length)
			{
				this.EnqueueDisconnect((DisconnectReasons)reader.ReadByte(), null);
				return;
			}
			this.EnqueueDisconnect(flag2 ? DisconnectReasons.Banned : DisconnectReasons.Kicked, null);
			return;
			IL_80D:
			Debug.Log(string.Format("Bad tag {0} at {1}+{2}={3}:  ", new object[]
			{
				reader.Tag,
				reader.Offset,
				reader.Position,
				reader.Length
			}) + string.Join<byte>(" ", reader.Buffer.Skip(reader.Offset).Take(reader.Length)));
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00061EB0 File Offset: 0x000600B0
		public static string AddressToString(uint address)
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				(byte)address,
				(byte)(address >> 8),
				(byte)(address >> 16),
				(byte)(address >> 24)
			});
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00061F00 File Offset: 0x00060100
		private ClientData GetOrCreateClient(int clientId)
		{
			List<ClientData> obj = this.allClients;
			ClientData clientData;
			lock (obj)
			{
				clientData = this.allClients.FirstOrDefault((ClientData c) => c.Id == clientId);
				if (clientData == null)
				{
					clientData = new ClientData(clientId);
					if (clientId == this.ClientId)
					{
						//clientData.platformID = Application.platform;
					}
					this.allClients.Add(clientData);
				}
			}
			return clientData;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00061F94 File Offset: 0x00060194
		public ClientData GetClient(int clientId)
		{
			List<ClientData> obj = this.allClients;
			ClientData result;
			lock (obj)
			{
				result = this.allClients.FirstOrDefault((ClientData c) => c.Id == clientId);
			}
			return result;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00061FF4 File Offset: 0x000601F4
		private void RemovePlayer(int playerIdThatLeft, DisconnectReasons reason)
		{
			ClientData client = null;
			List<ClientData> obj = this.allClients;
			lock (obj)
			{
				for (int i = 0; i < this.allClients.Count; i++)
				{
					ClientData clientData = this.allClients[i];
					if (clientData.Id == playerIdThatLeft)
					{
						client = clientData;
						this.allClients.RemoveAt(i);
						break;
					}
				}
			}
			if (client != null)
			{
				List<Action> dispatcher = this.Dispatcher;
				lock (dispatcher)
				{
					this.Dispatcher.Add(delegate
					{
						this.OnPlayerLeft(client, reason);
					});
				}
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000620D8 File Offset: 0x000602D8
		protected virtual void OnApplicationPause(bool pause)
		{
			this.appPaused = pause;
			if (!pause)
			{
				Debug.Log("Resumed Game");
				if (this.AmHost)
				{
					this.RemoveUnownedObjects();
					return;
				}
			}
			else if (this.GameState != InnerNetClient.GameStates.Ended && this.AmConnected)
			{
				Debug.Log("Lost focus during game");
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.WaitToDisconnect));
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00062138 File Offset: 0x00060338
		private void WaitToDisconnect(object state)
		{
			int num = 0;
			while (num < 30 && this.appPaused)
			{
				Thread.Sleep(1000);
				num++;
			}
			if (this.appPaused && this.GameState != InnerNetClient.GameStates.Ended && this.AmConnected)
			{
				this.DisconnectInternal(DisconnectReasons.FocusLostBackground, null);
				this.EnqueueDisconnect(DisconnectReasons.FocusLost, null);
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0006219C File Offset: 0x0006039C
		protected void SendInitialData(int clientId)
		{
			MessageWriter messageWriter = MessageWriter.Get(SendOption.Reliable);
			messageWriter.StartMessage(6);
			messageWriter.Write(this.GameId);
			messageWriter.WritePacked(clientId);
			List<InnerNetObject> obj = this.allObjects;
			lock (obj)
			{
				HashSet<GameObject> hashSet = new HashSet<GameObject>();
				for (int i = 0; i < this.allObjects.Count; i++)
				{
					InnerNetObject innerNetObject = this.allObjects[i];
					if (innerNetObject && hashSet.Add(innerNetObject.gameObject))
					{
						this.WriteSpawnMessage(innerNetObject, innerNetObject.OwnerId, innerNetObject.SpawnFlags, messageWriter);
					}
				}
			}
			messageWriter.EndMessage();
			this.SendOrDisconnect(messageWriter);
			messageWriter.Recycle();
		}

		// Token: 0x060012B4 RID: 4788
		protected abstract void OnGameCreated(string gameIdString);

		// Token: 0x060012B5 RID: 4789
		protected abstract void OnGameJoined(string gameIdString, ClientData client);

		// Token: 0x060012B6 RID: 4790
		protected abstract void OnWaitForHost(string gameIdString);

		// Token: 0x060012B7 RID: 4791
		protected abstract IEnumerator CoStartGame();

		// Token: 0x060012B8 RID: 4792
		protected abstract void OnGameEnd(GameOverReason reason, bool showAd);

		// Token: 0x060012B9 RID: 4793
		protected abstract void OnBecomeHost();

		// Token: 0x060012BA RID: 4794
		protected abstract void OnPlayerJoined(ClientData client);

		// Token: 0x060012BB RID: 4795
		protected abstract IEnumerator CoOnPlayerChangedScene(ClientData client, string targetScene);

		// Token: 0x060012BC RID: 4796
		protected abstract void OnPlayerLeft(ClientData client, DisconnectReasons reason);

		// Token: 0x060012BD RID: 4797
		protected abstract void OnReportedPlayer(ReportOutcome outcome, int clientId, string playerName, ReportReasons reason);

		// Token: 0x060012BE RID: 4798
		protected abstract void OnDisconnected();

		// Token: 0x060012BF RID: 4799
		protected abstract void OnGetGameList(InnerNetClient.TotalGameData totalGames, List<GameListing> availableGames);

		// Token: 0x060012C0 RID: 4800
		protected abstract byte[] GetConnectionData();

		// Token: 0x060012C1 RID: 4801 RVA: 0x00062268 File Offset: 0x00060468
		protected ClientData FindClientById(int id)
		{
			if (id < 0)
			{
				return null;
			}
			List<ClientData> obj = this.allClients;
			ClientData result;
			lock (obj)
			{
				for (int i = 0; i < this.allClients.Count; i++)
				{
					ClientData clientData = this.allClients[i];
					if (clientData.Id == id)
					{
						return clientData;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x04001510 RID: 5392
		public const int CurrentClient = -3;

		// Token: 0x04001511 RID: 5393
		public const int HostInherit = -2;

		// Token: 0x04001512 RID: 5394
		internal const byte DataFlag = 1;

		// Token: 0x04001513 RID: 5395
		internal const byte RpcFlag = 2;

		// Token: 0x04001514 RID: 5396
		internal const byte SpawnFlag = 4;

		// Token: 0x04001515 RID: 5397
		internal const byte DespawnFlag = 5;

		// Token: 0x04001516 RID: 5398
		internal const byte SceneChangeFlag = 6;

		// Token: 0x04001517 RID: 5399
		internal const byte ReadyFlag = 7;

		// Token: 0x04001518 RID: 5400
		internal const byte ChangeSettingsFlag = 8;

		// Token: 0x04001519 RID: 5401
		internal const byte ConsoleDeclareClientPlatformFlag = 205;

		// Token: 0x0400151A RID: 5402
		internal const byte PS4RoomRequest = 206;

		// Token: 0x0400151B RID: 5403
		internal const byte PS4RoomRequest_DoesRoomExist = 1;

		// Token: 0x0400151C RID: 5404
		internal const byte PS4RoomRequest_DoesRoomExistReply = 2;

		// Token: 0x0400151D RID: 5405
		public float MinSendInterval = 0.1f;

		// Token: 0x0400151E RID: 5406
		private uint NetIdCnt = 1U;

		// Token: 0x0400151F RID: 5407
		private float timer;

		// Token: 0x04001520 RID: 5408
		public AssetReference[] SpawnableObjects;

		// Token: 0x04001521 RID: 5409
		public InnerNetObject[] NonAddressableSpawnableObjects;

		// Token: 0x04001522 RID: 5410
		private bool InOnlineScene;

		// Token: 0x04001523 RID: 5411
		private HashSet<uint> DestroyedObjects = new HashSet<uint>();

		// Token: 0x04001524 RID: 5412
		public List<InnerNetObject> allObjects = new List<InnerNetObject>();

		// Token: 0x04001525 RID: 5413
		private Dictionary<uint, InnerNetObject> allObjectsFast = new Dictionary<uint, InnerNetObject>();

		// Token: 0x04001526 RID: 5414
		private MessageWriter[] Streams;

		// Token: 0x04001527 RID: 5415
		private int msgNum;

		// Token: 0x04001528 RID: 5416
		private static readonly DisconnectReasons[] disconnectReasons = new DisconnectReasons[]
		{
			DisconnectReasons.Error,
			DisconnectReasons.GameFull,
			DisconnectReasons.GameStarted,
			DisconnectReasons.GameNotFound,
			DisconnectReasons.IncorrectVersion,
			DisconnectReasons.Banned,
			DisconnectReasons.Kicked,
			DisconnectReasons.Hacking,
			DisconnectReasons.ServerFull,
			DisconnectReasons.Custom
		};

		// Token: 0x04001529 RID: 5417
		private const int SecondsDelayBeforeDisconnect = 30;

		// Token: 0x0400152A RID: 5418
		public const int NoClientId = -1;

		// Token: 0x0400152B RID: 5419
		private string networkAddress = "127.0.0.1";

		// Token: 0x0400152C RID: 5420
		private int networkPort;

		// Token: 0x0400152D RID: 5421
		private UnityUdpClientConnection connection;

		// Token: 0x0400152E RID: 5422
		public MatchMakerModes mode;

		// Token: 0x0400152F RID: 5423
		public GameModes GameMode;

		// Token: 0x04001530 RID: 5424
		public int GameId = 32;

		// Token: 0x04001531 RID: 5425
		public int HostId;

		// Token: 0x04001532 RID: 5426
		public int ClientId = -1;

		// Token: 0x04001533 RID: 5427
		public List<ClientData> allClients = new List<ClientData>();

		// Token: 0x04001534 RID: 5428
		public DisconnectReasons LastDisconnectReason;

		// Token: 0x04001535 RID: 5429
		public string LastCustomDisconnect;

		// Token: 0x04001536 RID: 5430
		private readonly List<Action> PreSpawnDispatcher = new List<Action>();

		// Token: 0x04001537 RID: 5431
		private readonly List<Action> Dispatcher = new List<Action>();

		// Token: 0x04001539 RID: 5433
		public InnerNetClient.GameStates GameState;

		// Token: 0x0400153A RID: 5434
		private List<Action> TempQueue = new List<Action>();

		// Token: 0x0400153B RID: 5435
		private volatile bool appPaused;

		// Token: 0x02000496 RID: 1174
		public enum GameStates
		{
			// Token: 0x04001D48 RID: 7496
			NotJoined,
			// Token: 0x04001D49 RID: 7497
			Joined,
			// Token: 0x04001D4A RID: 7498
			Started,
			// Token: 0x04001D4B RID: 7499
			Ended
		}

		// Token: 0x02000497 RID: 1175
		public class TotalGameData
		{
			// Token: 0x04001D4C RID: 7500
			public int[] PerMapTotals;
		}
	}
}
