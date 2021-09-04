using System;
//using Beebyte.Obfuscator;

// Token: 0x020000F9 RID: 249
//[Skip]
public interface IRegionInfo
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000635 RID: 1589
	string Name { get; }

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000636 RID: 1590
	string PingServer { get; }

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000637 RID: 1591
	ServerInfo[] Servers { get; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000638 RID: 1592
	StringNames TranslateName { get; }

	// Token: 0x06000639 RID: 1593
	IRegionInfo Duplicate();
}
