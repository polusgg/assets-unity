using System;

// Token: 0x02000232 RID: 562
public class WinningPlayerData
{
	// Token: 0x06000D64 RID: 3428 RVA: 0x00051549 File Offset: 0x0004F749
	public WinningPlayerData()
	{
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x00051554 File Offset: 0x0004F754
	public WinningPlayerData(GameData.PlayerInfo player)
	{
		this.IsYou = (player.Object == PlayerControl.LocalPlayer);
		this.Name = player.PlayerName;
		this.IsDead = (player.IsDead || player.Disconnected);
		this.IsImpostor = player.IsImpostor;
		this.ColorId = player.ColorId;
		this.SkinId = player.SkinId;
		this.PetId = player.PetId;
		this.HatId = player.HatId;
	}

	// Token: 0x04000EDA RID: 3802
	public string Name;

	// Token: 0x04000EDB RID: 3803
	public bool IsDead;

	// Token: 0x04000EDC RID: 3804
	public bool IsImpostor;

	// Token: 0x04000EDD RID: 3805
	public int ColorId;

	// Token: 0x04000EDE RID: 3806
	public uint SkinId;

	// Token: 0x04000EDF RID: 3807
	public uint HatId;

	// Token: 0x04000EE0 RID: 3808
	public uint PetId;

	// Token: 0x04000EE1 RID: 3809
	public bool IsYou;
}
