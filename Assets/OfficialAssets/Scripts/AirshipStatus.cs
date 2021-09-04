using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class AirshipStatus : ShipStatus
{
	// Token: 0x060000C5 RID: 197 RVA: 0x00005380 File Offset: 0x00003580
	protected override void OnEnable()
	{
		if (this.Systems != null)
		{
			return;
		}
		this.Ladders = base.GetComponentsInChildren<Ladder>();
		ElectricalDoors componentInChildren = base.GetComponentInChildren<ElectricalDoors>();
		if (AmongUsClient.Instance.AmHost)
		{
			componentInChildren.Initialize();
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
			},
			{
				SystemTypes.Doors,
				new DoorsSystemType()
			},
			{
				SystemTypes.Comms,
				new HudOverrideSystemType()
			},
			{
				SystemTypes.GapRoom,
				base.GetComponentInChildren<MovingPlatformBehaviour>()
			},
			{
				SystemTypes.Reactor,
				base.GetComponentInChildren<HeliSabotageSystem>()
			},
			{
				SystemTypes.Decontamination,
				componentInChildren
			},
			{
				SystemTypes.Decontamination2,
				new AutoDoorsSystemType()
			},
			{
				SystemTypes.Security,
				new SecurityCameraSystemType()
			}
		};
		Camera main = Camera.main;
		main.backgroundColor = this.CameraColor;
		FollowerCamera component = main.GetComponent<FollowerCamera>();
		component.shakeAmount = 0f;
		component.shakePeriod = 0f;
		this.Systems.Add(SystemTypes.Sabotage, new SabotageSystemType((from i in this.Systems.Values
		where i is IActivatable
		select i).Cast<IActivatable>().ToArray<IActivatable>()));
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000054B2 File Offset: 0x000036B2
	public override void RepairGameOverSystems()
	{
		(ShipStatus.Instance.Systems[SystemTypes.Reactor] as HeliSabotageSystem).ClearSabotage();
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000054D0 File Offset: 0x000036D0
	public override float CalculateLightRadius(GameData.PlayerInfo player)
	{
		float num = base.CalculateLightRadius(player);
		if (!player.IsImpostor)
		{
			foreach (LightAffector lightAffector in this.LightAffectors)
			{
				if (player.Object && player.Object.Collider.IsTouching(lightAffector.Hitbox))
				{
					num *= lightAffector.Multiplier;
				}
			}
		}
		return num;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00005535 File Offset: 0x00003735
	public override void OnMeetingCalled()
	{
		this.GapPlatform.MeetingCalled();
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005542 File Offset: 0x00003742
	public override void SpawnPlayer(PlayerControl player, int numPlayers, bool initialSpawn)
	{
		if (DestroyableSingleton<TutorialManager>.InstanceExists)
		{
			player.NetTransform.SnapTo(new Vector2(-0.66f, -0.5f));
			return;
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00005566 File Offset: 0x00003766
	public override IEnumerator PrespawnStep()
	{
		//SpawnInMinigame spawnInMinigame = Object.Instantiate<SpawnInMinigame>(this.SpawnInGame);
		//spawnInMinigame.transform.SetParent(Camera.main.transform, false);
		//spawnInMinigame.transform.localPosition = new Vector3(0f, 0f, -600f);
		//spawnInMinigame.Begin(null);
		//yield return spawnInMinigame.WaitForFinish();
		yield break;
	}

	// Token: 0x040000C1 RID: 193
	public Ladder[] Ladders;

	// Token: 0x040000C2 RID: 194
	public SpawnInMinigame SpawnInGame;

	// Token: 0x040000C3 RID: 195
	public MovingPlatformBehaviour GapPlatform;

	// Token: 0x040000C4 RID: 196
	public ParticleSystem ShowerParticles;

	// Token: 0x040000C5 RID: 197
	public LightAffector[] LightAffectors;
}
