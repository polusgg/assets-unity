using System;
using System.Collections;
using System.Net;
using Hazel;
using Hazel.Udp;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class AnnouncementPopUp : MonoBehaviour
{
	// Token: 0x0600067C RID: 1660 RVA: 0x0002A0B7 File Offset: 0x000282B7
	private static bool IsSuccess(AnnouncementPopUp.AnnounceState state)
	{
		return state == AnnouncementPopUp.AnnounceState.Success || state == AnnouncementPopUp.AnnounceState.Cached;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002A0C3 File Offset: 0x000282C3
	private void OnEnable()
	{
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0002A0DB File Offset: 0x000282DB
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0002A0ED File Offset: 0x000282ED
	public void EnableAnnouncement()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0002A0FB File Offset: 0x000282FB
	public IEnumerator Init()
	{
		if (this.AskedForUpdate != AnnouncementPopUp.AnnounceState.Fetching)
		{
			yield break;
		}
		yield return DestroyableSingleton<ServerManager>.Instance.WaitForServers();
		Debug.Log("Requesting announcement from: " + DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress);
		this.connection = new UnityUdpClientConnection(new IPEndPoint(IPAddress.Parse(DestroyableSingleton<ServerManager>.Instance.OnlineNetAddress), 22024), 0);
		this.connection.ResendTimeout = 1000;
		this.connection.ResendPingMultiplier = 1f;
		this.connection.DataReceived += this.Connection_DataReceived;
		this.connection.Disconnected += this.Connection_Disconnected;
		try
		{
			Announcement lastAnnouncement = SaveManager.LastAnnouncement;
			MessageWriter messageWriter = MessageWriter.Get(0);
			messageWriter.WritePacked(2U);
			messageWriter.WritePacked(lastAnnouncement.Id);
			messageWriter.WritePacked(SaveManager.LastLanguage);
			this.connection.ConnectAsync(messageWriter.ToByteArray(true));
			messageWriter.Recycle();
		}
		catch
		{
			this.AskedForUpdate = AnnouncementPopUp.AnnounceState.Failed;
		}
		yield return this.Show();
		yield break;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0002A10A File Offset: 0x0002830A
	private void Connection_Disconnected(object sender, DisconnectedEventArgs e)
	{
		Debug.Log("Announcement failed: " + e.Reason);
		this.AskedForUpdate = AnnouncementPopUp.AnnounceState.Failed;
		this.connection.Dispose();
		this.connection = null;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0002A13C File Offset: 0x0002833C
	public void FixedUpdate()
	{
		UnityUdpClientConnection unityUdpClientConnection = this.connection;
		if (unityUdpClientConnection != null)
		{
			unityUdpClientConnection.FixedUpdate();
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002A15C File Offset: 0x0002835C
	private void Connection_DataReceived(DataReceivedEventArgs e)
	{
		MessageReader message = e.Message;
		try
		{
			while (message.Position < message.Length)
			{
				MessageReader messageReader = message.ReadMessage();
				switch (messageReader.Tag)
				{
				case 0:
					this.AskedForUpdate = AnnouncementPopUp.AnnounceState.Cached;
					break;
				case 1:
					this.announcementUpdate = default(Announcement);
					this.announcementUpdate.DateFetched = DateTime.UtcNow;
					this.announcementUpdate.Id = messageReader.ReadPackedUInt32();
					this.announcementUpdate.AnnounceText = messageReader.ReadString();
					this.AskedForUpdate = ((this.announcementUpdate.Id == 0U) ? AnnouncementPopUp.AnnounceState.Cached : AnnouncementPopUp.AnnounceState.Success);
					break;
				case 4:
					try
					{
						int num = (int)messageReader.ReadByte();
						for (int i = 0; i < num; i++)
						{
							ChatLanguageSet.Instance.Languages[messageReader.ReadString()] = messageReader.ReadUInt32();
						}
					}
					catch (Exception ex)
					{
						Debug.Log("Error while loading languages: " + ex.Message);
					}
					try
					{
						ChatLanguageSet.Instance.Save();
					}
					catch
					{
					}
					break;
				}
			}
		}
		finally
		{
			message.Recycle();
		}
		try
		{
			this.connection.Dispose();
			this.connection = null;
		}
		catch
		{
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0002A2FC File Offset: 0x000284FC
	public IEnumerator Show()
	{
		float timer = 0f;
		while (this.AskedForUpdate == AnnouncementPopUp.AnnounceState.Fetching && timer < 6f)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		if (!AnnouncementPopUp.IsSuccess(this.AskedForUpdate))
		{
			Announcement lastAnnouncement = SaveManager.LastAnnouncement;
			if (lastAnnouncement.Id == 0U)
			{
				this.AnnounceText.Text = "Couldn't get announcement.";
			}
			else
			{
				this.AnnounceText.Text = "Couldn't get announcement. Last Known:\r\n" + lastAnnouncement.AnnounceText;
			}
		}
		else if (this.announcementUpdate.Id != SaveManager.LastAnnouncement.Id)
		{
			if (this.AskedForUpdate != AnnouncementPopUp.AnnounceState.Cached)
			{
				base.gameObject.SetActive(true);
			}
			if (this.announcementUpdate.Id == 0U)
			{
				this.announcementUpdate = SaveManager.LastAnnouncement;
				this.announcementUpdate.DateFetched = DateTime.UtcNow;
			}
			SaveManager.LastAnnouncement = this.announcementUpdate;
			this.AnnounceText.Text = this.announcementUpdate.AnnounceText;
		}
		while (base.gameObject.activeSelf)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0002A30B File Offset: 0x0002850B
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0002A319 File Offset: 0x00028519
	private void OnDestroy()
	{
		if (this.connection != null)
		{
			this.connection.Dispose();
			this.connection = null;
		}
	}

	// Token: 0x0400075D RID: 1885
	public const uint AnnouncementVersion = 2U;

	// Token: 0x0400075E RID: 1886
	private UnityUdpClientConnection connection;

	// Token: 0x0400075F RID: 1887
	private AnnouncementPopUp.AnnounceState AskedForUpdate;

	// Token: 0x04000760 RID: 1888
	public TextRenderer AnnounceText;

	// Token: 0x04000761 RID: 1889
	private Announcement announcementUpdate;

	// Token: 0x04000762 RID: 1890
	public GameObject ConnectIcon;

	// Token: 0x04000763 RID: 1891
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x02000398 RID: 920
	private enum AnnounceState
	{
		// Token: 0x040019CE RID: 6606
		Fetching,
		// Token: 0x040019CF RID: 6607
		Failed,
		// Token: 0x040019D0 RID: 6608
		Success,
		// Token: 0x040019D1 RID: 6609
		Cached
	}
}
