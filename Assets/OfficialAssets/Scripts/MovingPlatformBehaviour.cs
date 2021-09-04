using System;
using System.Collections;
using Hazel;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class MovingPlatformBehaviour : MonoBehaviour, ISystemType
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000616B File Offset: 0x0000436B
	public bool InUse
	{
		get
		{
			return this.Target;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006178 File Offset: 0x00004378
	// (set) Token: 0x060000F5 RID: 245 RVA: 0x00006180 File Offset: 0x00004380
	public bool IsDirty { get; private set; }

	// Token: 0x060000F6 RID: 246 RVA: 0x00006189 File Offset: 0x00004389
	public void Use()
	{
		PlayerControl.LocalPlayer.RpcUsePlatform();
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00006198 File Offset: 0x00004398
	public void Use(PlayerControl player)
	{
		Vector3 vector = base.transform.position - player.transform.position;
		if (this.Target || vector.magnitude > 3f)
		{
			return;
		}
		this.IsDirty = true;
		base.StartCoroutine(this.UsePlatform(player));
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x000061F2 File Offset: 0x000043F2
	public void Start()
	{
		this.SetSide(true);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000061FB File Offset: 0x000043FB
	private void SetSide(bool isLeft)
	{
		this.IsLeft = isLeft;
		base.transform.localPosition = (this.IsLeft ? this.LeftPosition : this.RightPosition);
		this.IsDirty = true;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000622C File Offset: 0x0000442C
	private void SetTarget(uint playerNetId, bool isLeft)
	{
		if (this.Target)
		{
			this.MeetingCalled();
		}
		PlayerControl playerControl = AmongUsClient.Instance.FindObjectByNetId<PlayerControl>(playerNetId);
		if (!playerControl)
		{
			this.SetSide(isLeft);
			return;
		}
		base.StartCoroutine(this.UsePlatform(playerControl));
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00006276 File Offset: 0x00004476
	private IEnumerator UsePlatform(PlayerControl target)
	{
		//this.Target = target;
		//target.MyPhysics.ResetMoveState(true);
		//if (target.AmOwner)
		//{
		//	PlayerControl.HideCursorTemporarily();
		//}
		//target.Collider.enabled = false;
		//target.moveable = false;
		//target.NetTransform.enabled = false;
		//Vector3 vector = this.IsLeft ? this.LeftUsePosition : this.RightUsePosition;
		//Vector3 vector2 = (!this.IsLeft) ? this.LeftUsePosition : this.RightUsePosition;
		//Vector3 sourcePos = this.IsLeft ? this.LeftPosition : this.RightPosition;
		//Vector3 targetPos = (!this.IsLeft) ? this.LeftPosition : this.RightPosition;
		//Vector3 vector3 = base.transform.parent.TransformPoint(vector);
		//Vector3 worldUseTargetPos = base.transform.parent.TransformPoint(vector2);
		//Vector3 worldSourcePos = base.transform.parent.TransformPoint(sourcePos);
		//Vector3 worldTargetPos = base.transform.parent.TransformPoint(targetPos);
		//yield return target.MyPhysics.WalkPlayerTo(vector3, 0.01f, 1f);
		//yield return target.MyPhysics.WalkPlayerTo(worldSourcePos, 0.01f, 1f);
		//yield return Effects.Wait(0.1f);
		//target.MyPhysics.enabled = false;
		//worldSourcePos -= target.Collider.offset;
		//worldTargetPos -= target.Collider.offset;
		//if (Constants.ShouldPlaySfx())
		//{
		//	SoundManager.Instance.PlayDynamicSound("PlatformMoving", this.MovingSound, true, new DynamicSound.GetDynamicsFunction(this.SoundDynamics), true);
		//}
		//this.IsLeft = !this.IsLeft;
		//yield return Effects.All(new IEnumerator[]
		//{
		//	Effects.Slide2D(base.transform, sourcePos, targetPos, target.MyPhysics.Speed),
		//	Effects.Slide2DWorld(target.transform, worldSourcePos, worldTargetPos, target.MyPhysics.Speed)
		//});
		//if (Constants.ShouldPlaySfx())
		//{
		//	SoundManager.Instance.StopNamedSound("PlatformMoving");
		//}
		//target.MyPhysics.enabled = true;
		//yield return target.MyPhysics.WalkPlayerTo(worldUseTargetPos, 0.01f, 1f);
		//target.CurrentPet.transform.position = target.transform.position;
		//yield return Effects.Wait(0.1f);
		//target.Collider.enabled = true;
		//target.moveable = true;
		//target.NetTransform.enabled = true;
		//this.Target = null;
		yield break;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000628C File Offset: 0x0000448C
	private void SoundDynamics(AudioSource source, float dt)
	{
		if (!PlayerControl.LocalPlayer)
		{
			source.volume = 0f;
			return;
		}
		Vector2 vector = base.transform.position;
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		float num = Vector2.Distance(vector, truePosition);
		if (num > 6f)
		{
			source.volume = 0f;
			return;
		}
		float num2 = 1f - num / 6f;
		source.volume = Mathf.Lerp(source.volume, num2, dt);
		VibrationManager.Vibrate(0.15f, vector, 6f);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000631C File Offset: 0x0000451C
	public void MeetingCalled()
	{
		base.StopAllCoroutines();
		VibrationManager.ClearAllVibration();
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.StopNamedSound("PlatformMoving");
		}
		if (this.Target)
		{
			this.Target.MyPhysics.enabled = true;
			this.Target.Collider.enabled = true;
			this.Target.moveable = true;
			this.Target.NetTransform.enabled = true;
			this.Target = null;
		}
		this.SetSide(this.IsLeft);
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000063A9 File Offset: 0x000045A9
	public void Detoriorate(float deltaTime)
	{
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000063AB File Offset: 0x000045AB
	public void RepairDamage(PlayerControl player, byte amount)
	{
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000063B0 File Offset: 0x000045B0
	public void Serialize(MessageWriter writer, bool initialState)
	{
		this.useId += 1;
		writer.Write(this.useId);
		PlayerControl target = this.Target;
		writer.Write((target != null) ? target.NetId : uint.MaxValue);
		writer.Write(this.IsLeft);
		this.IsDirty = initialState;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00006404 File Offset: 0x00004604
	public void Deserialize(MessageReader reader, bool initialState)
	{
		if (initialState)
		{
			this.useId = reader.ReadByte();
			this.SetTarget(reader.ReadUInt32(), reader.ReadBoolean());
			return;
		}
		byte newSid = reader.ReadByte();
		if (NetHelpers.SidGreaterThan(newSid, this.useId))
		{
			this.useId = newSid;
			this.SetTarget(reader.ReadUInt32(), reader.ReadBoolean());
		}
	}

	// Token: 0x040000EE RID: 238
	public Vector3 LeftPosition;

	// Token: 0x040000EF RID: 239
	public Vector3 RightPosition;

	// Token: 0x040000F0 RID: 240
	public Vector3 LeftUsePosition;

	// Token: 0x040000F1 RID: 241
	public Vector3 RightUsePosition;

	// Token: 0x040000F2 RID: 242
	public AudioClip MovingSound;

	// Token: 0x040000F3 RID: 243
	private bool IsLeft;

	// Token: 0x040000F4 RID: 244
	private PlayerControl Target;

	// Token: 0x040000F5 RID: 245
	private byte useId;
}
