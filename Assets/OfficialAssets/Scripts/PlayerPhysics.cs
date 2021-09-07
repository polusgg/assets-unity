using System;
using System.Collections;
using System.Linq;
using Hazel;
using InnerNet;
using PowerTools;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public class PlayerPhysics : InnerNetObject
{
	// Token: 0x060009E2 RID: 2530 RVA: 0x000406BC File Offset: 0x0003E8BC
	public void RpcClimbLadder(Ladder source)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			this.ClimbLadder(source, (byte)(this.lastClimbLadderSid + 1));
		}
		else
		{
			this.lastClimbLadderSid += 1;
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 31, (SendOption)1);
		messageWriter.Write(source.Id);
		messageWriter.Write(this.lastClimbLadderSid);
		messageWriter.EndMessage();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x00040726 File Offset: 0x0003E926
	public void RpcEnterVent(int id)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			base.StartCoroutine(this.CoEnterVent(id));
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 19, (SendOption)1);
		messageWriter.WritePacked(id);
		messageWriter.EndMessage();
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00040761 File Offset: 0x0003E961
	public void RpcExitVent(int id)
	{
		if (AmongUsClient.Instance.AmClient)
		{
			base.StartCoroutine(this.CoExitVent(id));
		}
		MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 20, (SendOption)1);
		messageWriter.WritePacked(id);
		messageWriter.EndMessage();
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0004079C File Offset: 0x0003E99C
	public override void HandleRpc(byte callId, MessageReader reader)
	{
		if (callId != 19)
		{
			if (callId != 20)
			{
				if (callId == 31)
				{
					AirshipStatus airshipStatus = (AirshipStatus)ShipStatus.Instance;
					byte ladderId = reader.ReadByte();
					byte climbLadderSid = reader.ReadByte();
					this.ClimbLadder(airshipStatus.Ladders.First((Ladder f) => f.Id == ladderId), climbLadderSid);
					return;
				}
			}
			else
			{
				int id = reader.ReadPackedInt32();
				base.StartCoroutine(this.CoExitVent(id));
			}
			return;
		}
		int id2 = reader.ReadPackedInt32();
		base.StartCoroutine(this.CoEnterVent(id2));
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0004082E File Offset: 0x0003EA2E
	public float TrueSpeed
	{
		get
		{
			return this.Speed * PlayerControl.GameOptions.PlayerSpeedMod;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00040841 File Offset: 0x0003EA41
	public float TrueGhostSpeed
	{
		get
		{
			return this.GhostSpeed * PlayerControl.GameOptions.PlayerSpeedMod;
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x00040854 File Offset: 0x0003EA54
	public void Awake()
	{
		this.body = base.GetComponent<Rigidbody2D>();
		this.Animator = base.GetComponent<SpriteAnim>();
		this.rend = base.GetComponent<SpriteRenderer>();
		this.myPlayer = base.GetComponent<PlayerControl>();
		if (this.inputHandler == null)
		{
			this.inputHandler = base.gameObject.AddComponent<SpecialInputHandler>();
			this.inputHandler.disableVirtualCursor = true;
			this.inputHandler.enabled = false;
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x000408C8 File Offset: 0x0003EAC8
	public void EnableInterpolation()
	{
		this.body.interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000408D8 File Offset: 0x0003EAD8
	private void FixedUpdate()
	{
		GameData.PlayerInfo data = this.myPlayer.Data;
		bool flag = data != null && data.IsDead;
		this.HandleAnimation(flag);
		if (base.AmOwner && this.myPlayer.CanMove && GameData.Instance)
		{
			this.body.velocity = DestroyableSingleton<HudManager>.Instance.joystick.Delta * (flag ? this.TrueGhostSpeed : this.TrueSpeed);
		}
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x00040958 File Offset: 0x0003EB58
	private void LateUpdate()
	{
		Vector3 position = base.transform.position;
		position.z = position.y / 1000f;
		base.transform.position = position;
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x00040990 File Offset: 0x0003EB90
	public Vector3 Vec2ToPosition(Vector2 pos)
	{
		return new Vector3(pos.x, pos.y, pos.y / 1000f);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x000409B0 File Offset: 0x0003EBB0
	public void SetSkin(uint skinId)
	{
		this.Skin.SetSkin(skinId, this.rend.flipX);
		if (this.Animator.IsPlaying(this.SpawnAnim))
		{
			this.Skin.SetSpawn(this.Animator.Time);
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00040A00 File Offset: 0x0003EC00
	private void StartClimb(bool down)
	{
		this.rend.flipX = false;
		this.Skin.Flipped = false;
		this.Animator.Play(down ? this.ClimbDownAnim : this.ClimbAnim, 1f);
		this.Animator.Time = 0f;
		this.Skin.SetClimb(down);
		this.myPlayer.HatRenderer.SetClimbAnim();
		// this.myPlayer.CurrentPet.Visible = false;
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00040A83 File Offset: 0x0003EC83
	private void ClimbLadder(Ladder source, byte climbLadderSid)
	{
		if (!NetHelpers.SidGreaterThan(climbLadderSid, this.lastClimbLadderSid))
		{
			return;
		}
		this.lastClimbLadderSid = climbLadderSid;
		this.ResetMoveState(true);
		base.StartCoroutine(this.CoClimbLadder(source, climbLadderSid));
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00040AB1 File Offset: 0x0003ECB1
	private IEnumerator CoClimbLadder(Ladder source, byte climbLadderSid)
	{
		this.myPlayer.Collider.enabled = false;
		this.myPlayer.moveable = false;
		this.myPlayer.NetTransform.enabled = false;
		if (this.myPlayer.AmOwner)
		{
			this.myPlayer.MyPhysics.inputHandler.enabled = true;
		}
		yield return this.WalkPlayerTo(source.transform.position, 0.001f, 1f);
		yield return Effects.Wait(0.1f);
		this.StartClimb(source.IsTop);
		this.myPlayer.FootSteps.clip = source.UseSound;
		this.myPlayer.FootSteps.loop = true;
		this.myPlayer.FootSteps.Play();
		yield return this.WalkPlayerTo(source.Destination.transform.position, 0.001f, (float)(source.IsTop ? 2 : 1));
		this.myPlayer.CurrentPet.transform.position = this.myPlayer.transform.position;
		this.ResetAnimState();
		yield return Effects.Wait(0.1f);
		this.myPlayer.Collider.enabled = true;
		this.myPlayer.moveable = true;
		this.myPlayer.NetTransform.enabled = true;
		yield break;
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00040AC8 File Offset: 0x0003ECC8
	public void ResetMoveState(bool stopCoroutines = true)
	{
		if (stopCoroutines)
		{
			this.myPlayer.StopAllCoroutines();
			base.StopAllCoroutines();
			if (this.inputHandler && this.inputHandler.enabled)
			{
				this.inputHandler.enabled = false;
			}
		}
		base.enabled = true;
		this.myPlayer.inVent = false;
		this.myPlayer.Visible = true;
		GameData.PlayerInfo data = this.myPlayer.Data;
		this.myPlayer.Collider.enabled = (data == null || !data.IsDead);
		this.ResetAnimState();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00040B60 File Offset: 0x0003ED60
	public void ResetAnimState()
	{
		this.myPlayer.FootSteps.Stop();
		this.myPlayer.FootSteps.loop = false;
		this.myPlayer.HatRenderer.SetIdleAnim();
		GameData.PlayerInfo data = this.myPlayer.Data;
		if (data != null)
		{
			this.myPlayer.HatRenderer.SetColor(this.myPlayer.Data.ColorId);
		}
		if (data == null || !data.IsDead)
		{
			this.Skin.SetIdle(this.rend.flipX);
			this.Animator.Play(this.IdleAnim, 1f);
			this.myPlayer.Visible = true;
			this.myPlayer.SetHatAlpha(1f);
			return;
		}
		this.Skin.SetGhost();
		this.Animator.Play(this.GhostIdleAnim, 1f);
		this.myPlayer.SetHatAlpha(0.5f);
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00040C54 File Offset: 0x0003EE54
	private void HandleAnimation(bool amDead)
	{
		if (this.Animator.IsPlaying(this.SpawnAnim))
		{
			return;
		}
		if (!GameData.Instance)
		{
			return;
		}
		Vector2 velocity = this.body.velocity;
		AnimationClip currentAnimation = this.Animator.GetCurrentAnimation();
		if (currentAnimation == this.ClimbAnim || currentAnimation == this.ClimbDownAnim)
		{
			return;
		}
		if (!amDead)
		{
			if (velocity.sqrMagnitude >= 0.05f)
			{
				bool flipX = this.rend.flipX;
				if (velocity.x < -0.01f)
				{
					this.rend.flipX = true;
				}
				else if (velocity.x > 0.01f)
				{
					this.rend.flipX = false;
				}
				if (currentAnimation != this.RunAnim || flipX != this.rend.flipX)
				{
					this.Animator.Play(this.RunAnim, 1f);
					this.Animator.Time = 0.45833334f;
					this.Skin.SetRun(this.rend.flipX);
				}
			}
			else if (currentAnimation == this.RunAnim || currentAnimation == this.SpawnAnim || !currentAnimation)
			{
				this.Skin.SetIdle(this.rend.flipX);
				this.Animator.Play(this.IdleAnim, 1f);
				this.myPlayer.SetHatAlpha(1f);
			}
		}
		else
		{
			this.Skin.SetGhost();
			if (currentAnimation != this.GhostIdleAnim)
			{
				this.Animator.Play(this.GhostIdleAnim, 1f);
				this.myPlayer.SetHatAlpha(0.5f);
			}
			if (velocity.x < -0.01f)
			{
				this.rend.flipX = true;
			}
			else if (velocity.x > 0.01f)
			{
				this.rend.flipX = false;
			}
		}
		this.Skin.Flipped = this.rend.flipX;
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00040E5C File Offset: 0x0003F05C
	public IEnumerator CoSpawnPlayer(LobbyBehaviour lobby)
	{
		//if (!lobby)
		//{
		//	yield break;
		//}
		//if (this.myPlayer.AmOwner)
		//{
		//	this.inputHandler.enabled = true;
		//}
		//Vector3 spawnPos = this.Vec2ToPosition(lobby.SpawnPositions[(int)this.myPlayer.PlayerId % lobby.SpawnPositions.Length]);
		//this.myPlayer.nameText.gameObject.SetActive(false);
		//this.myPlayer.Collider.enabled = false;
		//KillAnimation.SetMovement(this.myPlayer, false);
		//yield return new WaitForFixedUpdate();
		//bool amFlipped = this.myPlayer.PlayerId > 4;
		//this.myPlayer.GetComponent<SpriteRenderer>().flipX = amFlipped;
		//this.myPlayer.transform.position = spawnPos;
		//SoundManager.Instance.PlaySound(lobby.SpawnSound, false, 1f).volume = 0.75f;
		//this.Skin.SetSpawn(0f);
		//this.Skin.Flipped = amFlipped;
		//yield return new WaitForAnimationFinish(this.Animator, this.SpawnAnim);
		//base.transform.position = spawnPos + new Vector3(amFlipped ? -0.3f : 0.3f, -0.24f);
		//this.ResetMoveState(false);
		//Vector2 vector = (-spawnPos).normalized;
		//yield return this.WalkPlayerTo(spawnPos + vector, 0.01f, 1f);
		//this.myPlayer.Collider.enabled = true;
		//KillAnimation.SetMovement(this.myPlayer, true);
		//this.myPlayer.nameText.gameObject.SetActive(true);
		//if (this.myPlayer.AmOwner)
		//{
		//	this.inputHandler.enabled = false;
		//}
		yield break;
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00040E74 File Offset: 0x0003F074
	public void ExitAllVents()
	{
		ConsoleJoystick.SetMode_Gameplay();
		Vent.currentVent = null;
		this.ResetMoveState(true);
		this.myPlayer.moveable = true;
		Vent[] allVents = ShipStatus.Instance.AllVents;
		for (int i = 0; i < allVents.Length; i++)
		{
			allVents[i].SetButtons(false);
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00040EC1 File Offset: 0x0003F0C1
	private IEnumerator CoEnterVent(int id)
	{
		Vent vent = ShipStatus.Instance.AllVents.First((Vent v) => v.Id == id);
		if (this.myPlayer.AmOwner)
		{
			this.inputHandler.enabled = true;
		}
		this.myPlayer.moveable = false;
		yield return this.WalkPlayerTo(vent.transform.position + vent.Offset, 0.01f, 1f);
		vent.EnterVent(this.myPlayer);
		this.Skin.SetEnterVent(this.rend.flipX);
		yield return new WaitForAnimationFinish(this.Animator, this.EnterVentAnim);
		this.Skin.SetIdle(this.rend.flipX);
		this.Animator.Play(this.IdleAnim, 1f);
		this.myPlayer.Visible = false;
		this.myPlayer.inVent = true;
		if (this.myPlayer.AmOwner)
		{
			this.inputHandler.enabled = false;
		}
		yield break;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00040ED7 File Offset: 0x0003F0D7
	private IEnumerator CoExitVent(int id)
	{
		Vent vent = ShipStatus.Instance.AllVents.First((Vent v) => v.Id == id);
		if (this.myPlayer.AmOwner)
		{
			this.inputHandler.enabled = true;
		}
		this.myPlayer.Visible = true;
		this.myPlayer.inVent = false;
		vent.ExitVent(this.myPlayer);
		this.Skin.SetExitVent(this.rend.flipX);
		yield return new WaitForAnimationFinish(this.Animator, this.ExitVentAnim);
		this.Skin.SetIdle(this.rend.flipX);
		this.Animator.Play(this.IdleAnim, 1f);
		this.myPlayer.moveable = true;
		if (this.myPlayer.AmOwner)
		{
			this.inputHandler.enabled = false;
		}
		yield break;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00040EED File Offset: 0x0003F0ED
	public IEnumerator WalkPlayerTo(Vector2 worldPos, float tolerance = 0.01f, float speedMul = 1f)
	{
		//worldPos -= this.myPlayer.Collider.offset;
		//Rigidbody2D body = this.body;
		//Vector2 del = worldPos - base.transform.position;
		//while (del.sqrMagnitude > tolerance)
		//{
		//	float num = Mathf.Clamp(del.magnitude * 2f, 0.05f, 1f);
		//	body.velocity = del.normalized * this.Speed * num * speedMul;
		//	yield return null;
		//	if (body.velocity.magnitude < 0.005f && (double)del.sqrMagnitude < 0.1)
		//	{
		//		break;
		//	}
		//	del = worldPos - base.transform.position;
		//}
		//del = default(Vector2);
		//body.velocity = Vector2.zero;
		yield break;
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00040F11 File Offset: 0x0003F111
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		return false;
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00040F14 File Offset: 0x0003F114
	public override void Deserialize(MessageReader reader, bool initialState)
	{
	}

	// Token: 0x04000B49 RID: 2889
	private byte lastClimbLadderSid;

	// Token: 0x04000B4A RID: 2890
	public float Speed = 4.5f;

	// Token: 0x04000B4B RID: 2891
	public float GhostSpeed = 3f;

	// Token: 0x04000B4C RID: 2892
	[HideInInspector]
	private Rigidbody2D body;

	// Token: 0x04000B4D RID: 2893
	[HideInInspector]
	private SpriteAnim Animator;

	// Token: 0x04000B4E RID: 2894
	[HideInInspector]
	private SpriteRenderer rend;

	// Token: 0x04000B4F RID: 2895
	[HideInInspector]
	private PlayerControl myPlayer;

	// Token: 0x04000B50 RID: 2896
	public AnimationClip RunAnim;

	// Token: 0x04000B51 RID: 2897
	public AnimationClip IdleAnim;

	// Token: 0x04000B52 RID: 2898
	public AnimationClip GhostIdleAnim;

	// Token: 0x04000B53 RID: 2899
	public AnimationClip EnterVentAnim;

	// Token: 0x04000B54 RID: 2900
	public AnimationClip ExitVentAnim;

	// Token: 0x04000B55 RID: 2901
	public AnimationClip SpawnAnim;

	// Token: 0x04000B56 RID: 2902
	public AnimationClip ClimbAnim;

	// Token: 0x04000B57 RID: 2903
	public AnimationClip ClimbDownAnim;

	// Token: 0x04000B58 RID: 2904
	public SkinLayer Skin;

	// Token: 0x04000B59 RID: 2905
	[NonSerialized]
	public SpecialInputHandler inputHandler;
}
