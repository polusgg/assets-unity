using System;
using System.Collections;
using System.Net.Sockets;
using Hazel.Udp;
using UnityEngine;

namespace InnerNet
{
	// Token: 0x02000292 RID: 658
	public class InnerDiscover : DestroyableSingleton<InnerDiscover>
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06001263 RID: 4707 RVA: 0x00060004 File Offset: 0x0005E204
		// (remove) Token: 0x06001264 RID: 4708 RVA: 0x0006003C File Offset: 0x0005E23C
		public event Action<BroadcastPacket> OnPacketGet;

		// Token: 0x06001265 RID: 4709 RVA: 0x00060074 File Offset: 0x0005E274
		public void StartAsServer(string data)
		{
			bool flag = this.sender == null;
			if (flag)
			{
				this.sender = new UdpBroadcaster(this.Port, new Action<string>(Debug.LogError));
			}
			this.sender.SetData(data);
			if (flag)
			{
				base.StartCoroutine(this.RunServer());
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000600C5 File Offset: 0x0005E2C5
		private IEnumerator RunServer()
		{
			while (this.sender != null)
			{
				this.sender.Broadcast();
				for (float timer = 0f; timer < this.Interval; timer += Time.deltaTime)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000600D4 File Offset: 0x0005E2D4
		public void StopServer()
		{
			if (this.sender != null)
			{
				this.sender.Dispose();
				this.sender = null;
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000600F0 File Offset: 0x0005E2F0
		public void StartAsClient()
		{
			if (this.listener == null)
			{
				try
				{
					this.listener = new UdpBroadcastListener(this.Port, new Action<string>(Debug.LogError));
					this.listener.StartListen();
					base.StartCoroutine(this.RunClient());
				}
				catch (SocketException ex)
				{
					Debug.LogError("InnerDiscover::StartAsClient SocketException");
					Debug.LogException(ex, this);
					AmongUsClient.Instance.LastDisconnectReason = DisconnectReasons.Custom;
					AmongUsClient.Instance.LastCustomDisconnect = "Couldn't start local network listener. You may need to restart Among Us.";
					DestroyableSingleton<DisconnectPopup>.Instance.Show();
				}
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00060184 File Offset: 0x0005E384
		private IEnumerator RunClient()
		{
			while (this.listener != null)
			{
				this.listener.StartListen();
				BroadcastPacket[] packets = this.listener.GetPackets();
				for (int i = 0; i < packets.Length; i++)
				{
					Action<BroadcastPacket> onPacketGet = this.OnPacketGet;
					if (onPacketGet != null)
					{
						onPacketGet(packets[i]);
					}
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00060193 File Offset: 0x0005E393
		public void StopClient()
		{
			if (this.listener != null)
			{
				this.listener.Dispose();
				this.listener = null;
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000601AF File Offset: 0x0005E3AF
		public override void OnDestroy()
		{
			this.StopServer();
			this.StopClient();
			base.OnDestroy();
		}

		// Token: 0x04001508 RID: 5384
		private UdpBroadcastListener listener;

		// Token: 0x04001509 RID: 5385
		private UdpBroadcaster sender;

		// Token: 0x0400150A RID: 5386
		public int Port = 47777;

		// Token: 0x0400150B RID: 5387
		public float Interval = 1f;
	}
}
