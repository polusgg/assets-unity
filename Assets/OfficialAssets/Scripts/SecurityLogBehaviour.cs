using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class SecurityLogBehaviour : MonoBehaviour
{
	// Token: 0x06000BF6 RID: 3062 RVA: 0x0004A651 File Offset: 0x00048851
	public void LogPlayer(PlayerControl player, SecurityLogBehaviour.SecurityLogLocations location)
	{
		this.HasNew = true;
		this.LogEntries.Add(new SecurityLogBehaviour.SecurityLogEntry(player.PlayerId, location));
		if (this.LogEntries.Count > 20)
		{
			this.LogEntries.RemoveAt(0);
		}
	}

	// Token: 0x04000D53 RID: 3411
	public const byte ConsoleMask = 240;

	// Token: 0x04000D54 RID: 3412
	public const byte PlayerMask = 15;

	// Token: 0x04000D55 RID: 3413
	public Color[] BarColors = new Color[]
	{
		new Color32(33, 77, 173, 128),
		new Color32(173, 81, 16, 128),
		new Color32(16, 97, 8, 128)
	};

	// Token: 0x04000D56 RID: 3414
	public readonly List<SecurityLogBehaviour.SecurityLogEntry> LogEntries = new List<SecurityLogBehaviour.SecurityLogEntry>();

	// Token: 0x04000D57 RID: 3415
	public bool HasNew;

	// Token: 0x02000441 RID: 1089
	public enum SecurityLogLocations
	{
		// Token: 0x04001C1F RID: 7199
		North,
		// Token: 0x04001C20 RID: 7200
		Southeast,
		// Token: 0x04001C21 RID: 7201
		Southwest
	}

	// Token: 0x02000442 RID: 1090
	public struct SecurityLogEntry
	{
		// Token: 0x06001A2C RID: 6700 RVA: 0x000797F2 File Offset: 0x000779F2
		public SecurityLogEntry(byte playerId, SecurityLogBehaviour.SecurityLogLocations location)
		{
			this.PlayerId = playerId;
			this.Location = location;
		}

		// Token: 0x04001C22 RID: 7202
		public byte PlayerId;

		// Token: 0x04001C23 RID: 7203
		public SecurityLogBehaviour.SecurityLogLocations Location;
	}
}
