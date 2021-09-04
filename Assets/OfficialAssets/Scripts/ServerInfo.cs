using System;
using System.IO;
using System.Net;

// Token: 0x020000FB RID: 251
public class ServerInfo
{
	// Token: 0x06000643 RID: 1603 RVA: 0x00028342 File Offset: 0x00026542
	public ServerInfo(string name, string ip, ushort port)
	{
		this.Name = name;
		this.Ip = ip;
		this.Port = port;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0002836A File Offset: 0x0002656A
	public void Serialize(BinaryWriter writer)
	{
		writer.Write(this.Name);
#pragma warning disable 618
		writer.Write((uint)IPAddress.Parse(this.Ip).Address);
#pragma warning restore 618
		writer.Write(this.Port);
		writer.Write(this.ConnectionFailures);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x000283A7 File Offset: 0x000265A7
	public static ServerInfo Deserialize(BinaryReader reader)
	{
		return new ServerInfo(reader.ReadString(), new IPAddress((long)((ulong)reader.ReadUInt32())).ToString(), reader.ReadUInt16())
		{
			ConnectionFailures = reader.ReadInt32()
		};
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x000283D7 File Offset: 0x000265D7
	public override string ToString()
	{
		return string.Format("{0}: {1}:{2}", this.Name, this.Ip, this.Port);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x000283FA File Offset: 0x000265FA
	public override int GetHashCode()
	{
		return this.Ip.GetHashCode();
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00028408 File Offset: 0x00026608
	public override bool Equals(object obj)
	{
		ServerInfo serverInfo = obj as ServerInfo;
		return serverInfo != null && serverInfo.Ip == this.Ip && serverInfo.Port == this.Port;
	}

	// Token: 0x040006F8 RID: 1784
	public readonly string Name = "Custom";

	// Token: 0x040006F9 RID: 1785
	public readonly string Ip;

	// Token: 0x040006FA RID: 1786
	public readonly ushort Port;

	// Token: 0x040006FB RID: 1787
	public int Players;

	// Token: 0x040006FC RID: 1788
	public int ConnectionFailures;
}
