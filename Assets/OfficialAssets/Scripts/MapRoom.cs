using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class MapRoom : MonoBehaviour
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
	// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
	public InfectedOverlay Parent { get; set; }

	// Token: 0x060004E9 RID: 1257 RVA: 0x0001F201 File Offset: 0x0001D401
	public void Start()
	{
		if (this.door)
		{
			this.door.SetCooldownNormalizedUvs();
		}
		if (this.special)
		{
			this.special.SetCooldownNormalizedUvs();
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0001F234 File Offset: 0x0001D434
	public void OOBUpdate()
	{
		if (this.door && ShipStatus.Instance)
		{
			float timer = ((RunTimer)ShipStatus.Instance.Systems[SystemTypes.Doors]).GetTimer(this.room);
			float num = this.Parent.CanUseDoors ? (timer / 30f) : 1f;
			this.door.material.SetFloat("_Percent", num);
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0001F2AE File Offset: 0x0001D4AE
	internal void SetSpecialActive(float perc)
	{
		if (this.special)
		{
			this.special.material.SetFloat("_Percent", perc);
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0001F2D3 File Offset: 0x0001D4D3
	public void SabotageReactor()
	{
		if (!this.Parent.CanUseSpecial)
		{
			return;
		}
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Sabotage, 3);
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0001F2F0 File Offset: 0x0001D4F0
	public void SabotageComms()
	{
		if (!this.Parent.CanUseSpecial)
		{
			return;
		}
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Sabotage, 14);
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0001F30E File Offset: 0x0001D50E
	public void SabotageOxygen()
	{
		if (!this.Parent.CanUseSpecial)
		{
			return;
		}
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Sabotage, 8);
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0001F32B File Offset: 0x0001D52B
	public void SabotageLights()
	{
		if (!this.Parent.CanUseSpecial)
		{
			return;
		}
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Sabotage, 7);
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001F348 File Offset: 0x0001D548
	public void SabotageSeismic()
	{
		if (!this.Parent.CanUseSpecial)
		{
			return;
		}
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Sabotage, 21);
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001F368 File Offset: 0x0001D568
	public void SabotageDoors()
	{
		if (!this.Parent.CanUseDoors)
		{
			return;
		}
		if (((RunTimer)ShipStatus.Instance.Systems[SystemTypes.Doors]).GetTimer(this.room) > 0f)
		{
			return;
		}
		ShipStatus.Instance.RpcCloseDoorsOfType(this.room);
	}

	// Token: 0x040005AF RID: 1455
	public SystemTypes room;

	// Token: 0x040005B0 RID: 1456
	public SpriteRenderer door;

	// Token: 0x040005B1 RID: 1457
	public SpriteRenderer special;
}
