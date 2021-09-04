using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class PolusShipStatus : ShipStatus
{
	// Token: 0x060007AF RID: 1967 RVA: 0x00031310 File Offset: 0x0002F510
	protected override void OnEnable()
	{
		if (this.Systems != null)
		{
			return;
		}
		this.Systems = new Dictionary<SystemTypes, ISystemType>(ShipStatus.SystemTypeComparer.Instance)
		{
			{
				SystemTypes.Electrical,
				new SwitchSystem()
			},
			{
				SystemTypes.MedBay,
				new MedScanSystem()
			}
		};
		Camera main = Camera.main;
		main.backgroundColor = this.CameraColor;
		FollowerCamera component = main.GetComponent<FollowerCamera>();
		DestroyableSingleton<HudManager>.Instance.ShadowQuad.material.SetInt("_Mask", 7);
		this.Systems.Add(SystemTypes.Doors, new DoorsSystemType());
		this.Systems.Add(SystemTypes.Comms, new HudOverrideSystemType());
		this.Systems.Add(SystemTypes.Security, new SecurityCameraSystemType());
		this.Systems.Add(SystemTypes.Laboratory, new ReactorSystemType(60f, SystemTypes.Laboratory));
		if (component)
		{
			component.shakeAmount = 0f;
			component.shakePeriod = 0f;
		}
		this.Systems.Add(SystemTypes.Sabotage, new SabotageSystemType((from i in this.Systems.Values
		where i is IActivatable
		select i).Cast<IActivatable>().ToArray<IActivatable>()));
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00031438 File Offset: 0x0002F638
	public override void SpawnPlayer(PlayerControl player, int numPlayers, bool initialSpawn)
	{
		if (initialSpawn)
		{
			base.SpawnPlayer(player, numPlayers, initialSpawn);
			return;
		}
		Vector2 position;
		if (player.PlayerId < 5)
		{
			position = this.MeetingSpawnCenter + Vector2.right * (float)player.PlayerId;
		}
		else
		{
			position = this.MeetingSpawnCenter2 + Vector2.right * (float)(player.PlayerId - 5);
		}
		player.NetTransform.SnapTo(position);
	}
}
