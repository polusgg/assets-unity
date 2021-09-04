using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public static class Constants
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000DAFB File Offset: 0x0000BCFB
	internal static int GetBroadcastVersion()
	{
		return 50531650;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000DB02 File Offset: 0x0000BD02
	internal static int GetVersion(int year, int month, int day, int rev)
	{
		return year * 25000 + month * 1800 + day * 50 + rev;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000DB1A File Offset: 0x0000BD1A
	internal static byte[] GetBroadcastVersionBytes()
	{
		return BitConverter.GetBytes(Constants.GetBroadcastVersion());
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000DB26 File Offset: 0x0000BD26
	public static bool ShouldPlaySfx()
	{
		return !AmongUsClient.Instance || AmongUsClient.Instance.GameMode != GameModes.LocalGame || DetectHeadset.Detect();
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000DB48 File Offset: 0x0000BD48
	internal static bool ShouldFlipSkeld()
	{
		try
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = new DateTime(utcNow.Year, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			DateTime t2 = t.AddDays(1.0);
			if (utcNow >= t && utcNow <= t2)
			{
				return true;
			}
		}
		catch
		{
		}
		return false;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
	public static Platforms GetPlatformType()
	{
		return Platforms.StandaloneItch;
	}

	// Token: 0x040002A9 RID: 681
	public const string LocalNetAddress = "127.0.0.1";

	// Token: 0x040002AA RID: 682
	public const ushort GamePlayPort = 22023;

	// Token: 0x040002AB RID: 683
	public const ushort AnnouncementPort = 22024;

	// Token: 0x040002AC RID: 684
	public const string InfinitySymbol = "∞";

	// Token: 0x040002AD RID: 685
	public static readonly int ShipOnlyMask = LayerMask.GetMask(new string[]
	{
		"Ship"
	});

	// Token: 0x040002AE RID: 686
	public static readonly int ShipAndObjectsMask = LayerMask.GetMask(new string[]
	{
		"Ship",
		"Objects"
	});

	// Token: 0x040002AF RID: 687
	public static readonly int ShipAndAllObjectsMask = LayerMask.GetMask(new string[]
	{
		"Ship",
		"Objects",
		"ShortObjects"
	});

	// Token: 0x040002B0 RID: 688
	public static readonly int NotShipMask = ~LayerMask.GetMask(new string[]
	{
		"Ship"
	});

	// Token: 0x040002B1 RID: 689
	public static readonly int Usables = ~LayerMask.GetMask(new string[]
	{
		"Ship",
		"UI"
	});

	// Token: 0x040002B2 RID: 690
	public static readonly int PlayersOnlyMask = LayerMask.GetMask(new string[]
	{
		"Players",
		"Ghost"
	});

	// Token: 0x040002B3 RID: 691
	public static readonly int ShadowMask = LayerMask.GetMask(new string[]
	{
		"Shadow",
		"Objects",
		"IlluminatedBlocking"
	});

	// Token: 0x040002B4 RID: 692
	public static readonly int[] CompatVersions = new int[]
	{
		Constants.GetBroadcastVersion()
	};

	// Token: 0x040002B5 RID: 693
	public const int Year = 2021;

	// Token: 0x040002B6 RID: 694
	public const int Month = 3;

	// Token: 0x040002B7 RID: 695
	public const int Day = 25;

	// Token: 0x040002B8 RID: 696
	public const int Revision = 0;

	// Token: 0x040002B9 RID: 697
	public const int VisualRevision = 0;

	// Token: 0x040002BA RID: 698
	public const int PrivacyPolicyVersion = 1;
}
