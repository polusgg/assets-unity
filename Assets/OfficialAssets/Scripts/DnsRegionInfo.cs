using System;
using System.Net;
//using Beebyte.Obfuscator;
//using Newtonsoft.Json;

// Token: 0x020000F4 RID: 244
//[Skip]
public class DnsRegionInfo : IRegionInfo
{
	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600060E RID: 1550 RVA: 0x00027722 File Offset: 0x00025922
	public string Name { get; }

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600060F RID: 1551 RVA: 0x0002772A File Offset: 0x0002592A
	//[JsonIgnore]
	public string PingServer
	{
		get
		{
			return this.Servers.Random<ServerInfo>().Ip;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000610 RID: 1552 RVA: 0x0002773C File Offset: 0x0002593C
	//[JsonIgnore]
	public ServerInfo[] Servers
	{
		get
		{
			if (this.cachedServers == null)
			{
				this.PopulateServers();
			}
			return this.cachedServers;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x00027752 File Offset: 0x00025952
	public StringNames TranslateName { get; }

	// Token: 0x06000612 RID: 1554 RVA: 0x0002775C File Offset: 0x0002595C
	public DnsRegionInfo(string fqdn, string name, StringNames translateName, string defaultIp, ushort port)
	{
		if (port == 0)
		{
			port = 22023;
		}
		this.Fqdn = fqdn;
		this.Name = name;
		this.TranslateName = translateName;
		this.DefaultIp = defaultIp;
		this.Port = port;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x000277AC File Offset: 0x000259AC
	private void PopulateServers()
	{
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(this.Fqdn);
			ServerInfo[] array = new ServerInfo[hostAddresses.Length];
			for (int i = 0; i < hostAddresses.Length; i++)
			{
				array[i] = new ServerInfo(string.Format("{0}-{1}", this.Name, i), hostAddresses[i].ToString(), this.Port);
			}
			this.cachedServers = array;
		}
		catch
		{
			this.cachedServers = new ServerInfo[]
			{
				new ServerInfo(this.Name ?? "", this.DefaultIp, 22023)
			};
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00027854 File Offset: 0x00025A54
	private DnsRegionInfo(string fqdn, string name, StringNames translateName, ServerInfo[] servers)
	{
		this.Fqdn = fqdn;
		this.Name = name;
		this.TranslateName = translateName;
		this.cachedServers = servers;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00027884 File Offset: 0x00025A84
	public IRegionInfo Duplicate()
	{
		return new DnsRegionInfo(this.Fqdn, this.Name, this.TranslateName, this.Servers);
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000278A3 File Offset: 0x00025AA3
	public override int GetHashCode()
	{
		return this.Name.GetHashCode();
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x000278B0 File Offset: 0x00025AB0
	public override bool Equals(object obj)
	{
		IRegionInfo regionInfo = obj as IRegionInfo;
		return regionInfo != null && regionInfo.Name.Equals(this.Name);
	}

	// Token: 0x040006C6 RID: 1734
	public readonly string Fqdn;

	// Token: 0x040006C7 RID: 1735
	public readonly string DefaultIp;

	// Token: 0x040006C8 RID: 1736
	public readonly ushort Port = 22023;

	// Token: 0x040006C9 RID: 1737
	private ServerInfo[] cachedServers;
}
