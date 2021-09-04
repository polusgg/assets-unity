using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Hazel;
using Hazel.Udp;
using UnityEngine;

namespace InnerNet
{
	// Token: 0x0200029D RID: 669
	public class InnerNetServer : DestroyableSingleton<InnerNetServer>
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x000625F4 File Offset: 0x000607F4
		public override void OnDestroy()
		{
			this.StopServer();
			base.OnDestroy();
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00062604 File Offset: 0x00060804
		public void StartAsServer()
		{
			if (this.listener != null)
			{
				this.StopServer();
			}
			this.GameState = GameStates.NotStarted;
			this.listener = new UdpConnectionListener(new IPEndPoint(IPAddress.Any, this.Port), 0, delegate(string s)
			{
				Debug.LogError(s);
			});
			this.listener.NewConnection += this.OnServerConnect;
			this.listener.Start();
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00062684 File Offset: 0x00060884
		public void StartAsLocalServer()
		{
			if (this.listener != null)
			{
				this.StopServer();
			}
			this.GameState = GameStates.NotStarted;
			this.listener = new UdpConnectionListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), this.Port), 0, delegate(string s)
			{
				Debug.LogError(s);
			});
			this.listener.NewConnection += this.OnServerConnect;
			this.listener.Start();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00062708 File Offset: 0x00060908
		private void DebugString(string obj)
		{
			if (!string.IsNullOrWhiteSpace(obj))
			{
				Debug.LogError(obj);
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00062718 File Offset: 0x00060918
		public void StopServer()
		{
			this.HostId = -1;
			this.GameState = GameStates.Destroyed;
			if (this.listener != null)
			{
				this.listener.Dispose();
				this.listener = null;
			}
			List<InnerNetServer.Player> clients = this.Clients;
			lock (clients)
			{
				this.Clients.Clear();
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00062788 File Offset: 0x00060988
		public static bool IsCompatibleVersion(int version)
		{
			return Constants.CompatVersions.Contains(version);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00062798 File Offset: 0x00060998
		private void OnServerConnect(NewConnectionEventArgs evt)
		{
			MessageReader handshakeData = evt.HandshakeData;
			try
			{
				if (evt.HandshakeData.Length < 5)
				{
					InnerNetServer.SendIncorrectVersion(evt.Connection);
					return;
				}
				if (!InnerNetServer.IsCompatibleVersion(handshakeData.ReadInt32()))
				{
					InnerNetServer.SendIncorrectVersion(evt.Connection);
					return;
				}
			}
			finally
			{
				handshakeData.Recycle();
			}
			InnerNetServer.Player client = new InnerNetServer.Player(evt.Connection);
			Debug.Log(string.Format("Client {0} added: {1}", client.Id, evt.Connection.EndPoint));
			UdpConnection udpConnection = (UdpConnection)evt.Connection;
			udpConnection.KeepAliveInterval = 1500;
			udpConnection.DisconnectTimeout = 6000;
			udpConnection.ResendPingMultiplier = 1.5f;
			udpConnection.DataReceived += delegate(DataReceivedEventArgs e)
			{
				this.OnDataReceived(client, e);
			};
			udpConnection.Disconnected += delegate(object o, DisconnectedEventArgs e)
			{
				this.ClientDisconnect(client);
			};
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0006289C File Offset: 0x00060A9C
		private static void SendIncorrectVersion(Connection connection)
		{
			MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
			messageWriter.StartMessage(1);
			messageWriter.Write(5);
			messageWriter.EndMessage();
			connection.Send(messageWriter);
			messageWriter.Recycle();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000628D4 File Offset: 0x00060AD4
		private void Connection_DataRecievedRaw(byte[] data, int length)
		{
			Debug.Log("Server Got: " + string.Join(" ", (from b in data
			select b.ToString()).Take(length)));
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00062928 File Offset: 0x00060B28
		private void OnDataReceived(InnerNetServer.Player client, DataReceivedEventArgs evt)
		{
			MessageReader message = evt.Message;
			try
			{
				while (message.Position < message.Length)
				{
					this.HandleMessage(client, message.ReadMessage(), evt.SendOption);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("InnerNetServer::OnDataReceived Exception");
				Debug.LogException(ex, this);
			}
			finally
			{
				message.Recycle();
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00062998 File Offset: 0x00060B98
		private void HandleMessage(InnerNetServer.Player client, MessageReader reader, SendOption sendOption)
		{
			switch (reader.Tag)
			{
			case 0:
			{
				Debug.Log("Server got host game");
				MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
				messageWriter.StartMessage(0);
				messageWriter.Write(32);
				messageWriter.EndMessage();
				client.Connection.Send(messageWriter);
				messageWriter.Recycle();
				return;
			}
			case 1:
			{
				Debug.Log("Server got join game");
				if (reader.ReadInt32() == 32)
				{
					this.JoinGame(client);
					return;
				}
				MessageWriter messageWriter2 = MessageWriter.Get((SendOption)1);
				messageWriter2.StartMessage(1);
				messageWriter2.Write(3);
				messageWriter2.EndMessage();
				client.Connection.Send(messageWriter2);
				messageWriter2.Recycle();
				return;
			}
			case 2:
				if (reader.ReadInt32() == 32)
				{
					this.StartGame(reader, client);
					return;
				}
				break;
			case 3:
				if (reader.ReadInt32() == 32)
				{
					this.ClientDisconnect(client);
					return;
				}
				break;
			case 4:
			case 7:
			case 9:
			case 10:
				break;
			case 5:
				if (this.Clients.Contains(client))
				{
					if (reader.ReadInt32() == 32)
					{
						MessageWriter messageWriter3 = MessageWriter.Get(sendOption);
						messageWriter3.CopyFrom(reader);
						this.Broadcast(messageWriter3, client);
						messageWriter3.Recycle();
						return;
					}
				}
				else if (this.GameState == GameStates.Started)
				{
					Debug.Log("GameDataTo: Server didn't have client");
					client.Connection.Dispose();
					return;
				}
				break;
			case 6:
				if (this.Clients.Contains(client))
				{
					if (reader.ReadInt32() == 32)
					{
						int targetId = reader.ReadPackedInt32();
						MessageWriter messageWriter4 = MessageWriter.Get(sendOption);
						messageWriter4.CopyFrom(reader);
						this.SendTo(messageWriter4, targetId);
						messageWriter4.Recycle();
						return;
					}
				}
				else if (this.GameState == GameStates.Started)
				{
					Debug.Log("GameDataTo: Server didn't have client");
					client.Connection.Dispose();
					return;
				}
				break;
			case 8:
				if (reader.ReadInt32() == 32)
				{
					this.EndGame(reader, client);
					return;
				}
				break;
			case 11:
				if (reader.ReadInt32() == 32)
				{
					this.KickPlayer(reader.ReadPackedInt32(), reader.ReadBoolean());
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00062B80 File Offset: 0x00060D80
		private void KickPlayer(int targetId, bool ban)
		{
			List<InnerNetServer.Player> clients = this.Clients;
			lock (clients)
			{
				InnerNetServer.Player player = null;
				for (int i = 0; i < this.Clients.Count; i++)
				{
					if (this.Clients[i].Id == targetId)
					{
						player = this.Clients[i];
						break;
					}
				}
				if (player != null)
				{
					if (ban)
					{
						HashSet<string> obj = this.ipBans;
						lock (obj)
						{
							IPEndPoint endPoint = player.Connection.EndPoint;
							this.ipBans.Add(endPoint.Address.ToString());
						}
					}
					MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
					messageWriter.StartMessage(11);
					messageWriter.Write(32);
					messageWriter.WritePacked(targetId);
					messageWriter.Write(ban);
					messageWriter.EndMessage();
					this.Broadcast(messageWriter, null);
					messageWriter.Recycle();
				}
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00062C94 File Offset: 0x00060E94
		protected void JoinGame(InnerNetServer.Player client)
		{
			HashSet<string> obj = this.ipBans;
			lock (obj)
			{
				IPEndPoint endPoint = client.Connection.EndPoint;
				if (this.ipBans.Contains(endPoint.Address.ToString()))
				{
					MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
					messageWriter.StartMessage(1);
					messageWriter.Write(6);
					messageWriter.EndMessage();
					client.Connection.Send(messageWriter);
					messageWriter.Recycle();
					return;
				}
			}
			List<InnerNetServer.Player> clients = this.Clients;
			lock (clients)
			{
				switch (this.GameState)
				{
				case GameStates.NotStarted:
					this.HandleNewGameJoin(client);
					break;
				default:
				{
					MessageWriter messageWriter2 = MessageWriter.Get((SendOption)1);
					messageWriter2.StartMessage(1);
					messageWriter2.Write(2);
					messageWriter2.EndMessage();
					client.Connection.Send(messageWriter2);
					messageWriter2.Recycle();
					break;
				}
				case GameStates.Ended:
					this.HandleRejoin(client);
					break;
				}
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00062DB0 File Offset: 0x00060FB0
		private void HandleRejoin(InnerNetServer.Player client)
		{
			if (client.Id == this.HostId)
			{
				this.GameState = GameStates.NotStarted;
				this.HandleNewGameJoin(client);
				MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
				for (int i = 0; i < this.Clients.Count; i++)
				{
					InnerNetServer.Player player = this.Clients[i];
					if (player != client)
					{
						try
						{
							this.WriteJoinedMessage(player, messageWriter, true);
							player.Connection.Send(messageWriter);
						}
						catch (Exception ex)
						{
							Debug.LogError("InnerNetServer::HandleRejoin Exception: " + ex.Message);
							Debug.LogException(ex, this);
						}
					}
				}
				messageWriter.Recycle();
				return;
			}
			if (this.Clients.Count >= 9)
			{
				MessageWriter messageWriter2 = MessageWriter.Get((SendOption)1);
				messageWriter2.StartMessage(1);
				messageWriter2.Write(1);
				messageWriter2.EndMessage();
				client.Connection.Send(messageWriter2);
				messageWriter2.Recycle();
				return;
			}
			this.Clients.Add(client);
			client.LimboState = LimboStates.WaitingForHost;
			MessageWriter messageWriter3 = MessageWriter.Get((SendOption)1);
			try
			{
				messageWriter3.StartMessage(12);
				messageWriter3.Write(32);
				messageWriter3.Write(client.Id);
				messageWriter3.EndMessage();
				client.Connection.Send(messageWriter3);
				this.BroadcastJoinMessage(client, messageWriter3);
			}
			catch (Exception ex2)
			{
				Debug.LogError("InnerNetServer::HandleRejoin MessageWriter Exception: " + ex2.Message);
				Debug.LogException(ex2, this);
			}
			finally
			{
				messageWriter3.Recycle();
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00062F34 File Offset: 0x00061134
		private void HandleNewGameJoin(InnerNetServer.Player client)
		{
			if (this.Clients.Count >= 10)
			{
				MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
				try
				{
					messageWriter.StartMessage(1);
					messageWriter.Write(1);
					messageWriter.EndMessage();
					client.Connection.Send(messageWriter);
				}
				catch (Exception ex)
				{
					Debug.LogError("InnerNetServer::HandleNewGameJoin MessageWriter 1 Exception: " + ex.Message);
					Debug.LogException(ex, this);
				}
				finally
				{
					messageWriter.Recycle();
				}
				return;
			}
			this.Clients.Add(client);
			client.LimboState = LimboStates.PreSpawn;
			if (this.HostId == -1)
			{
				this.HostId = this.Clients[0].Id;
			}
			if (this.HostId == client.Id)
			{
				client.LimboState = LimboStates.NotLimbo;
			}
			MessageWriter messageWriter2 = MessageWriter.Get((SendOption)1);
			try
			{
				this.WriteJoinedMessage(client, messageWriter2, true);
				client.Connection.Send(messageWriter2);
				this.BroadcastJoinMessage(client, messageWriter2);
			}
			catch (Exception ex2)
			{
				Debug.LogError("InnerNetServer::HandleNewGameJoin MessageWriter 2 Exception: " + ex2.Message);
				Debug.LogException(ex2, this);
			}
			finally
			{
				messageWriter2.Recycle();
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00063068 File Offset: 0x00061268
		private void EndGame(MessageReader message, InnerNetServer.Player source)
		{
			if (source.Id == this.HostId)
			{
				this.GameState = GameStates.Ended;
				MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
				messageWriter.CopyFrom(message);
				this.Broadcast(messageWriter, null);
				messageWriter.Recycle();
				List<InnerNetServer.Player> clients = this.Clients;
				lock (clients)
				{
					this.Clients.Clear();
					return;
				}
			}
			Debug.LogWarning("Reset request rejected from: " + source.Id.ToString());
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000630F8 File Offset: 0x000612F8
		private void StartGame(MessageReader message, InnerNetServer.Player source)
		{
			this.GameState = GameStates.Started;
			MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
			messageWriter.CopyFrom(message);
			this.Broadcast(messageWriter, null);
			messageWriter.Recycle();
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00063128 File Offset: 0x00061328
		private void ClientDisconnect(InnerNetServer.Player client)
		{
			Debug.Log("Server DC client " + client.Id.ToString());
			MessageWriter messageWriter = MessageWriter.Get((SendOption)1);
			messageWriter.StartMessage(4);
			messageWriter.Write(32);
			messageWriter.Write(client.Id);
			messageWriter.Write(this.HostId);
			messageWriter.Write(0);
			messageWriter.EndMessage();
			this.Broadcast(messageWriter, null);
			messageWriter.Recycle();
			List<InnerNetServer.Player> clients = this.Clients;
			lock (clients)
			{
				this.Clients.Remove(client);
				client.Connection.Dispose();
				if (this.Clients.Count > 0)
				{
					this.HostId = this.Clients[0].Id;
				}
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00063204 File Offset: 0x00061404
		protected void SendTo(MessageWriter msg, int targetId)
		{
			List<InnerNetServer.Player> clients = this.Clients;
			lock (clients)
			{
				for (int i = 0; i < this.Clients.Count; i++)
				{
					InnerNetServer.Player player = this.Clients[i];
					if (player.Id == targetId)
					{
						try
						{
							player.Connection.Send(msg);
							break;
						}
						catch (Exception ex)
						{
							Debug.LogError("InnerNetServer::SendTo Exception");
							Debug.LogException(ex, this);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00063298 File Offset: 0x00061498
		protected void Broadcast(MessageWriter msg, InnerNetServer.Player source)
		{
			List<InnerNetServer.Player> clients = this.Clients;
			lock (clients)
			{
				for (int i = 0; i < this.Clients.Count; i++)
				{
					InnerNetServer.Player player = this.Clients[i];
					if (player != source)
					{
						try
						{
							player.Connection.Send(msg);
						}
						catch (Exception ex)
						{
							Debug.LogError("InnerNetServer::Broadcast Exception");
							Debug.LogException(ex, this);
						}
					}
				}
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00063328 File Offset: 0x00061528
		private void BroadcastJoinMessage(InnerNetServer.Player client, MessageWriter msg)
		{
			msg.Clear((SendOption)1);
			msg.StartMessage(1);
			msg.Write(32);
			msg.Write(client.Id);
			msg.Write(this.HostId);
			msg.EndMessage();
			this.Broadcast(msg, client);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00063368 File Offset: 0x00061568
		private void WriteJoinedMessage(InnerNetServer.Player client, MessageWriter msg, bool clear)
		{
			if (clear)
			{
				msg.Clear((SendOption)1);
			}
			msg.StartMessage(7);
			msg.Write(32);
			msg.Write(client.Id);
			msg.Write(this.HostId);
			msg.WritePacked(this.Clients.Count - 1);
			for (int i = 0; i < this.Clients.Count; i++)
			{
				InnerNetServer.Player player = this.Clients[i];
				if (player != client)
				{
					msg.WritePacked(player.Id);
				}
			}
			msg.EndMessage();
		}

		// Token: 0x04001576 RID: 5494
		public const int MaxPlayers = 10;

		// Token: 0x04001577 RID: 5495
		public const int LocalGameId = 32;

		// Token: 0x04001578 RID: 5496
		private const int InvalidHost = -1;

		// Token: 0x04001579 RID: 5497
		private int HostId = -1;

		// Token: 0x0400157A RID: 5498
		public HashSet<string> ipBans = new HashSet<string>();

		// Token: 0x0400157B RID: 5499
		public int Port = 22023;

		// Token: 0x0400157C RID: 5500
		[SerializeField]
		private GameStates GameState;

		// Token: 0x0400157D RID: 5501
		private UdpConnectionListener listener;

		// Token: 0x0400157E RID: 5502
		private List<InnerNetServer.Player> Clients = new List<InnerNetServer.Player>();

		// Token: 0x020004AD RID: 1197
		protected class Player
		{
			// Token: 0x06001C1E RID: 7198 RVA: 0x0007E15C File Offset: 0x0007C35C
			public Player(Connection connection)
			{
				this.Id = Interlocked.Increment(ref InnerNetServer.Player.IdCount);
				this.Connection = connection;
			}

			// Token: 0x04001D95 RID: 7573
			private static int IdCount = 1;

			// Token: 0x04001D96 RID: 7574
			public int Id;

			// Token: 0x04001D97 RID: 7575
			public Connection Connection;

			// Token: 0x04001D98 RID: 7576
			public LimboStates LimboState;
		}
	}
}
