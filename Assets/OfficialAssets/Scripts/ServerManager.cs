using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Beebyte.Obfuscator;
using Hazel;
using Hazel.UPnP;
//using Newtonsoft.Json;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class ServerManager : DestroyableSingleton<ServerManager>
{
	// Token: 0x17000065 RID: 101
	// (get) Token: 0x0600061F RID: 1567 RVA: 0x00027BD6 File Offset: 0x00025DD6
	// (set) Token: 0x06000620 RID: 1568 RVA: 0x00027BDE File Offset: 0x00025DDE
	public IRegionInfo CurrentRegion { get; private set; }

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x00027BE7 File Offset: 0x00025DE7
	// (set) Token: 0x06000622 RID: 1570 RVA: 0x00027BEF File Offset: 0x00025DEF
	public ServerInfo CurrentServer { get; private set; }

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000623 RID: 1571 RVA: 0x00027BF8 File Offset: 0x00025DF8
	// (set) Token: 0x06000624 RID: 1572 RVA: 0x00027C00 File Offset: 0x00025E00
	public IRegionInfo[] AvailableRegions { get; private set; } = ServerManager.DefaultRegions;

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000625 RID: 1573 RVA: 0x00027C09 File Offset: 0x00025E09
	private ServerInfo[] AvailableServers
	{
		get
		{
			return this.CurrentRegion.Servers;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000626 RID: 1574 RVA: 0x00027C16 File Offset: 0x00025E16
	public string OnlineNetAddress
	{
		get
		{
			return this.CurrentServer.Ip;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000627 RID: 1575 RVA: 0x00027C23 File Offset: 0x00025E23
	public ushort OnlineNetPort
	{
		get
		{
			return this.CurrentServer.Port;
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00027C30 File Offset: 0x00025E30
	public override void Awake()
	{
		base.Awake();
		if (DestroyableSingleton<ServerManager>.Instance != this)
		{
			return;
		}
		this.serverInfoFileOld = Path.Combine(PlatformPaths.persistentDataPath, "regionInfo.dat");
		this.serverInfoFileJson = Path.Combine(PlatformPaths.persistentDataPath, "regionInfo.json");
		this.LoadServers();
		Task.Run(new Action(this.HandleUpnp));
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00027C94 File Offset: 0x00025E94
	private void HandleUpnp()
	{
		try
		{
			using (UPnPHelper upnPHelper = new UPnPHelper(NullLogger.Instance))
			{
				try
				{
					upnPHelper.DeleteForwardingRule(22024);
				}
				catch
				{
				}
				try
				{
					upnPHelper.DeleteForwardingRule(22023);
				}
				catch
				{
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00027D14 File Offset: 0x00025F14
	[ContextMenu("Reselect Server")]
	internal void ReselectServer()
	{
		this.CurrentRegion = (this.CurrentRegion ?? ServerManager.DefaultRegions[0].Duplicate());
		if (this.AvailableServers.All((ServerInfo s) => s.Players == 0))
		{
			this.AvailableServers.Shuffle(0);
		}
		this.CurrentServer = (from s in this.AvailableServers
		orderby s.ConnectionFailures, s.Players
		select s).First<ServerInfo>();
		Debug.Log(string.Format("Selected server: {0}", this.CurrentServer));
		this.state = ServerManager.UpdateState.Success;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00027DEA File Offset: 0x00025FEA
	public IEnumerator ReselectRegionFromDefaults()
	{
		this.AvailableRegions = ServerManager.DefaultRegions;
		ServerManager.PingWrapper[] pings = new ServerManager.PingWrapper[ServerManager.DefaultRegions.Length];
		for (int i = 0; i < pings.Length; i++)
		{
			IRegionInfo regionInfo = ServerManager.DefaultRegions[i];
			pings[i] = new ServerManager.PingWrapper(regionInfo, new Ping(regionInfo.PingServer));
		}
		for (;;)
		{
			if (pings.Any((ServerManager.PingWrapper p) => p.Ping.isDone && p.Ping.time > 0))
			{
				break;
			}
			yield return null;
		}
		IRegionInfo regionInfo2 = ServerManager.DefaultRegions.First<IRegionInfo>();
		int num = int.MaxValue;
		foreach (ServerManager.PingWrapper pingWrapper in pings)
		{
			if (pingWrapper.Ping.isDone && pingWrapper.Ping.time > 0)
			{
				Debug.Log("Ping time: " + pingWrapper.Region.Name + " @ " + pingWrapper.Ping.time.ToString());
				if (pingWrapper.Ping.time < num)
				{
					regionInfo2 = pingWrapper.Region;
					num = pingWrapper.Ping.time;
				}
			}
			pingWrapper.Ping.DestroyPing();
		}
		this.CurrentRegion = regionInfo2.Duplicate();
		this.ReselectServer();
		this.SaveServers();
		yield break;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00027DF9 File Offset: 0x00025FF9
	public IEnumerator WaitForServers()
	{
		while (this.state == ServerManager.UpdateState.Connecting)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00027E08 File Offset: 0x00026008
	internal void SetRegion(IRegionInfo region)
	{
		this.CurrentRegion = region;
		this.ReselectServer();
		this.SaveServers();
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00027E20 File Offset: 0x00026020
	public void SaveServers()
	{
		try
		{
			ServerManager.JsonServerData jsonServerData = default(ServerManager.JsonServerData);
			jsonServerData.CurrentRegionIdx = this.AvailableRegions.IndexOf((IRegionInfo r) => r.Name == this.CurrentRegion.Name);
			jsonServerData.Regions = this.AvailableRegions;
			//FileIO.WriteAllText(this.serverInfoFileJson, JsonConvert.SerializeObject(jsonServerData, new JsonSerializerSettings
			//{
			//	TypeNameHandling = 4
			//}));
		}
		catch
		{
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x00027E98 File Offset: 0x00026098
	public void LoadServers()
	{
		if (FileIO.Exists(this.serverInfoFileOld))
		{
			this.LoadServersOld();
			FileIO.Delete(this.serverInfoFileOld);
			this.SaveServers();
			return;
		}
		if (FileIO.Exists(this.serverInfoFileJson))
		{
			try
			{
				//ServerManager.JsonServerData jsonServerData = JsonConvert.DeserializeObject<ServerManager.JsonServerData>(FileIO.ReadAllText(this.serverInfoFileJson), new JsonSerializerSettings
				//{
				//	TypeNameHandling = 4
				//});
				//this.AvailableRegions = jsonServerData.Regions;
				//this.CurrentRegion = this.AvailableRegions[jsonServerData.CurrentRegionIdx.Wrap(this.AvailableRegions.Length)];
				//this.CurrentServer = this.CurrentRegion.Servers.Random<ServerInfo>();
				//this.state = ServerManager.UpdateState.Success;
				return;
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("Couldn't load regions: {0}", ex));
				Debug.LogException(ex, this);
				base.StartCoroutine(this.ReselectRegionFromDefaults());
				return;
			}
		}
		base.StartCoroutine(this.ReselectRegionFromDefaults());
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00027F88 File Offset: 0x00026188
	public void LoadServersOld()
	{
		if (File.Exists(this.serverInfoFileOld))
		{
			try
			{
				using (FileStream fileStream = File.OpenRead(this.serverInfoFileOld))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						int num = binaryReader.ReadInt32();
						this.CurrentRegion = StaticRegionInfo.Deserialize(binaryReader);
						this.CurrentServer = this.CurrentRegion.Servers[num];
						Debug.Log(string.Format("Loaded server: {0}", this.CurrentServer));
						this.AvailableRegions = ServerManager.DefaultRegions.Append(this.CurrentRegion).ToArray<IRegionInfo>();
						this.state = ServerManager.UpdateState.Success;
					}
				}
				return;
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("Couldn't load regions: {0}", ex));
				Debug.LogException(ex, this);
				base.StartCoroutine(this.ReselectRegionFromDefaults());
				return;
			}
		}
		base.StartCoroutine(this.ReselectRegionFromDefaults());
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0002808C File Offset: 0x0002628C
	internal bool TrackServerFailure(string networkAddress)
	{
		ServerInfo srv = this.AvailableServers.FirstOrDefault((ServerInfo s) => s.Ip == networkAddress);
		if (srv != null)
		{
			srv.ConnectionFailures++;
			ServerInfo serverInfo = (from s in this.AvailableServers
			orderby s.Players
			select s).FirstOrDefault((ServerInfo s) => s.ConnectionFailures < srv.ConnectionFailures);
			if (serverInfo != null)
			{
				this.CurrentServer = serverInfo;
				AmongUsClient.Instance.SetEndpoint(serverInfo.Ip, serverInfo.Port);
				Debug.Log("Attempting another server: " + serverInfo.Name);
				return true;
			}
		}
		return false;
	}

	// Token: 0x040006ED RID: 1773
	public static readonly IRegionInfo[] DefaultRegions = new IRegionInfo[]
	{
		new DnsRegionInfo("na.mm.among.us", "North America", StringNames.ServerNA, "50.116.1.42", 22023),
		new DnsRegionInfo("eu.mm.among.us", "Europe", StringNames.ServerEU, "172.105.251.170", 22023),
		new DnsRegionInfo("as.mm.among.us", "Asia", StringNames.ServerAS, "139.162.111.196", 22023)
	};

	// Token: 0x040006F1 RID: 1777
	private string serverInfoFileOld;

	// Token: 0x040006F2 RID: 1778
	private string serverInfoFileJson;

	// Token: 0x040006F3 RID: 1779
	private ServerManager.UpdateState state;

	// Token: 0x02000386 RID: 902
	private enum UpdateState
	{
		// Token: 0x0400198C RID: 6540
		Connecting,
		// Token: 0x0400198D RID: 6541
		Failed,
		// Token: 0x0400198E RID: 6542
		Success
	}
	 
	// Token: 0x02000387 RID: 903
	//[Skip]
	//[JsonObject]
	private struct JsonServerData
	{
		// Token: 0x0400198F RID: 6543
		public int CurrentRegionIdx;

		// Token: 0x04001990 RID: 6544
		public IRegionInfo[] Regions;
	}

	// Token: 0x02000388 RID: 904
	private struct PingWrapper
	{
		// Token: 0x0600174E RID: 5966 RVA: 0x00070698 File Offset: 0x0006E898
		public PingWrapper(IRegionInfo region, Ping ping)
		{
			this.Region = region;
			this.Ping = ping;
		}

		// Token: 0x04001991 RID: 6545
		public IRegionInfo Region;

		// Token: 0x04001992 RID: 6546
		public Ping Ping;
	}
}
