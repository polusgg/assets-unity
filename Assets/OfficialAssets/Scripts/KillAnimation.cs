using System;
using System.Collections;
using PowerTools;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class KillAnimation : MonoBehaviour
{
	// Token: 0x06000985 RID: 2437 RVA: 0x0003DBB7 File Offset: 0x0003BDB7
	public IEnumerator CoPerformKill(PlayerControl source, PlayerControl target)
	{
		//FollowerCamera cam = Camera.main.GetComponent<FollowerCamera>();
		//bool isParticipant = PlayerControl.LocalPlayer == source || PlayerControl.LocalPlayer == target;
		//PlayerPhysics sourcePhys = source.MyPhysics;
		//KillAnimation.SetMovement(source, false);
		//KillAnimation.SetMovement(target, false);
		//DeadBody deadBody = Object.Instantiate<DeadBody>(this.bodyPrefab);
		//deadBody.enabled = false;
		//deadBody.ParentId = target.PlayerId;
		//target.SetPlayerMaterialColors(deadBody.GetComponent<Renderer>());
		//Vector3 vector = target.transform.position + this.BodyOffset;
		//vector.z = vector.y / 1000f;
		//deadBody.transform.position = vector;
		//if (isParticipant)
		//{
		//	cam.Locked = true;
		//	ConsoleJoystick.SetMode_Task();
		//	if (PlayerControl.LocalPlayer.AmOwner)
		//	{
		//		PlayerControl.LocalPlayer.MyPhysics.inputHandler.enabled = true;
		//	}
		//}
		//target.Die(DeathReason.Kill);
		//SpriteAnim sourceAnim = source.GetComponent<SpriteAnim>();
		//yield return new WaitForAnimationFinish(sourceAnim, this.BlurAnim);
		//source.NetTransform.SnapTo(target.transform.position);
		//sourceAnim.Play(sourcePhys.IdleAnim, 1f);
		//KillAnimation.SetMovement(source, true);
		//KillAnimation.SetMovement(target, true);
		//deadBody.enabled = true;
		//if (isParticipant)
		//{
		//	cam.Locked = false;
		//}
		yield break;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0003DBD4 File Offset: 0x0003BDD4
	public static void SetMovement(PlayerControl source, bool canMove)
	{
		source.moveable = canMove;
		source.MyPhysics.ResetMoveState(false);
		source.NetTransform.enabled = canMove;
		source.MyPhysics.enabled = canMove;
		source.NetTransform.Halt();
	}

	// Token: 0x04000AF6 RID: 2806
	public AnimationClip BlurAnim;

	// Token: 0x04000AF7 RID: 2807
	public DeadBody bodyPrefab;

	// Token: 0x04000AF8 RID: 2808
	public Vector3 BodyOffset;
}
