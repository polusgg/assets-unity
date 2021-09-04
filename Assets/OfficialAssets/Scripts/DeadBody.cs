using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class DeadBody : MonoBehaviour
{
	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000958 RID: 2392 RVA: 0x0003D4AB File Offset: 0x0003B6AB
	public Vector2 TruePosition
	{
		get
		{
			return base.transform.position;// + this.myCollider.offset;
		}
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0003D4D4 File Offset: 0x0003B6D4
	public void OnClick()
	{
		if (this.Reported)
		{
			return;
		}
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		Vector2 truePosition2 = this.TruePosition;
		if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false))
		{
			this.Reported = true;
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(this.ParentId);
			PlayerControl.LocalPlayer.CmdReportDeadBody(playerById);
		}
	}

	// Token: 0x04000ACD RID: 2765
	public bool Reported;

	// Token: 0x04000ACE RID: 2766
	public short KillIdx;

	// Token: 0x04000ACF RID: 2767
	public byte ParentId;

	// Token: 0x04000AD0 RID: 2768
	public Collider2D myCollider;
}
