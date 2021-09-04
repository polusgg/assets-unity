using System;
using System.IO;
//using Beebyte.Obfuscator;

// Token: 0x020000FA RID: 250
//[Skip]
public class StaticRegionInfo : IRegionInfo
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x0600063A RID: 1594 RVA: 0x000281FB File Offset: 0x000263FB
	public string Name { get; }

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x0600063B RID: 1595 RVA: 0x00028203 File Offset: 0x00026403
	public string PingServer { get; }

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x0002820B File Offset: 0x0002640B
	public ServerInfo[] Servers { get; }

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x0600063D RID: 1597 RVA: 0x00028213 File Offset: 0x00026413
	public StringNames TranslateName { get; }

	// Token: 0x0600063E RID: 1598 RVA: 0x0002821B File Offset: 0x0002641B
	public StaticRegionInfo(string name, StringNames translateName, string ping, ServerInfo[] servers)
	{
		this.Name = name;
		this.PingServer = ping;
		this.Servers = servers;
		this.TranslateName = translateName;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00028240 File Offset: 0x00026440
	public static StaticRegionInfo Deserialize(BinaryReader reader)
	{
		string name = reader.ReadString();
		string ping = reader.ReadString();
		ServerInfo[] array = new ServerInfo[reader.ReadInt32()];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = ServerInfo.Deserialize(reader);
		}
		return new StaticRegionInfo(name, StringNames.NoTranslation, ping, array);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002828C File Offset: 0x0002648C
	public IRegionInfo Duplicate()
	{
		ServerInfo[] array = new ServerInfo[this.Servers.Length];
		for (int i = 0; i < array.Length; i++)
		{
			ServerInfo serverInfo = this.Servers[i];
			array[i] = new ServerInfo(serverInfo.Name, serverInfo.Ip, serverInfo.Port)
			{
				ConnectionFailures = serverInfo.ConnectionFailures,
				Players = serverInfo.Players
			};
		}
		return new StaticRegionInfo(this.Name, this.TranslateName, this.PingServer, array);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00028308 File Offset: 0x00026508
	public override int GetHashCode()
	{
		return this.Name.GetHashCode();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00028318 File Offset: 0x00026518
	public override bool Equals(object obj)
	{
		IRegionInfo regionInfo = obj as IRegionInfo;
		return regionInfo != null && regionInfo.Name.Equals(this.Name);
	}
}
