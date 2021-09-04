using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class AdjustSteeringGame : Minigame
{
	// Token: 0x06000109 RID: 265 RVA: 0x000066D0 File Offset: 0x000048D0
	public void HonkHorn()
	{
		SoundManager.Instance.PlaySoundImmediate(this.HornSound, false, 1f, 1f);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x000066F0 File Offset: 0x000048F0
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.TargetThrustY = AdjustSteeringGame.ThrustRange.Next();
		this.ThrustTarget.transform.localPosition = new Vector3(-3.25f, this.TargetThrustY);
		float num = AdjustSteeringGame.ThrustRange.NextMinDistance(this.TargetThrustY, 0.5f);
		this.Thrust.transform.localPosition = new Vector3(-2.15f, num, -1f);
		this.TargetSteeringRot = AdjustSteeringGame.SteeringRange.Next();
		this.SteeringTarget.transform.localEulerAngles = new Vector3(0f, 0f, this.TargetSteeringRot);
		float num2 = AdjustSteeringGame.SteeringRange.NextMinDistance(this.TargetSteeringRot, 15f);
		this.Steering.transform.localEulerAngles = new Vector3(0f, 0f, num2);
		base.SetupInput(true);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x000067DC File Offset: 0x000049DC
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.controller.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	//Player player = ReInput.players.GetPlayer(0);
		//	if (!this.thrustLocked)
		//	{
		//		float axisRaw = player.GetAxisRaw(14);
		//		Vector3 localPosition = this.Thrust.transform.localPosition;
		//		if (Mathf.Abs(axisRaw) > 0.1f)
		//		{
		//			localPosition.y += axisRaw * Time.deltaTime * 4f;
		//			localPosition.y = AdjustSteeringGame.ThrustRange.Clamp(localPosition.y);
		//			this.Thrust.transform.localPosition = localPosition;
		//			this.prevHadLeftInput = true;
		//		}
		//		else
		//		{
		//			if (this.prevHadLeftInput && Mathf.Abs(localPosition.y - this.TargetThrustY) < 0.075f)
		//			{
		//				this.thrustLocked = true;
		//				localPosition.y = this.TargetThrustY;
		//				this.Thrust.transform.localPosition = localPosition;
		//			}
		//			this.prevHadLeftInput = false;
		//		}
		//		if (!this.thrustLocked)
		//		{
		//			bool flag = Mathf.Abs(localPosition.y - this.TargetThrustY) < 0.075f;
		//			if (flag && !this.prevThrustWasGood)
		//			{
		//				VibrationManager.Vibrate(0.5f, 0f, 0.1f, VibrationManager.VibrationFalloff.None, null, false);
		//			}
		//			this.prevThrustWasGood = flag;
		//		}
		//	}
		//	if (!this.steeringLocked)
		//	{
		//		Vector3 localEulerAngles = this.Steering.transform.localEulerAngles;
		//		if (localEulerAngles.z > 180f)
		//		{
		//			localEulerAngles.z -= 360f;
		//		}
		//		Vector2 vector = player.GetAxis2DRaw(16, 17);
		//		Vector3 localPosition2 = this.Thrust.transform.localPosition;
		//		float magnitude = vector.magnitude;
		//		if (magnitude > 0.9f)
		//		{
		//			if (this.prevHadRightInput)
		//			{
		//				vector /= magnitude;
		//				float num = Vector2.SignedAngle(this.prevRightStickInput, vector);
		//				localEulerAngles.z += num * 0.035f;
		//				localEulerAngles.z = AdjustSteeringGame.SteeringRange.Clamp(localEulerAngles.z);
		//				this.Steering.transform.localEulerAngles = localEulerAngles;
		//			}
		//			this.prevRightStickInput = vector;
		//			this.prevHadRightInput = true;
		//		}
		//		else
		//		{
		//			if (this.prevHadRightInput && Mathf.Abs(localEulerAngles.z - this.TargetSteeringRot) < 5f)
		//			{
		//				this.steeringLocked = true;
		//				localEulerAngles.z = this.TargetSteeringRot;
		//				this.Steering.transform.localEulerAngles = localEulerAngles;
		//			}
		//			this.prevHadRightInput = false;
		//		}
		//		if (!this.steeringLocked)
		//		{
		//			bool flag2 = Mathf.Abs(localEulerAngles.z - this.TargetSteeringRot) < 5f;
		//			if (flag2 && !this.prevSteeringWasGood)
		//			{
		//				VibrationManager.Vibrate(0f, 0.5f, 0.1f, VibrationManager.VibrationFalloff.None, null, false);
		//			}
		//			this.prevSteeringWasGood = flag2;
		//		}
		//	}
		//}
		//else
		//{
		//	if (!this.thrustLocked)
		//	{
		//		Vector3 localPosition3 = this.Thrust.transform.localPosition;
		//		DragState dragState = this.controller.CheckDrag(this.Thrust);
		//		if (dragState != DragState.Dragging)
		//		{
		//			if (dragState == DragState.Released)
		//			{
		//				if (Mathf.Abs(localPosition3.y - this.TargetThrustY) < 0.075f)
		//				{
		//					this.thrustLocked = true;
		//					localPosition3.y = this.TargetThrustY;
		//					this.Thrust.transform.localPosition = localPosition3;
		//				}
		//			}
		//		}
		//		else
		//		{
		//			Vector2 vector2 = this.controller.DragPosition - base.transform.position;
		//			vector2.y = AdjustSteeringGame.ThrustRange.Clamp(vector2.y);
		//			localPosition3.y = vector2.y;
		//			this.Thrust.transform.localPosition = localPosition3;
		//		}
		//	}
		//	if (!this.steeringLocked)
		//	{
		//		Vector3 localEulerAngles2 = this.Steering.transform.localEulerAngles;
		//		if (localEulerAngles2.z > 180f)
		//		{
		//			localEulerAngles2.z -= 360f;
		//		}
		//		switch (this.controller.CheckDrag(this.Steering))
		//		{
		//		case DragState.TouchStart:
		//			this.startAngle = localEulerAngles2.z;
		//			break;
		//		case DragState.Dragging:
		//		{
		//			Vector2 vector3 = this.controller.DragPosition - this.controller.DragStartPosition;
		//			localEulerAngles2.z = AdjustSteeringGame.SteeringRange.Clamp(vector3.x * -15f + this.startAngle);
		//			this.Steering.transform.localEulerAngles = localEulerAngles2;
		//			break;
		//		}
		//		case DragState.Released:
		//			if (Mathf.Abs(localEulerAngles2.z - this.TargetSteeringRot) < 5f)
		//			{
		//				this.steeringLocked = true;
		//				localEulerAngles2.z = this.TargetSteeringRot;
		//				this.Steering.transform.localEulerAngles = localEulerAngles2;
		//			}
		//			break;
		//		}
		//	}
		//}
		//if (this.thrustLocked && this.steeringLocked)
		//{
		//	this.MyNormTask.NextStep();
		//	this.Close();
		//}
	}

	// Token: 0x040000FD RID: 253
	private const float ArrowOffset = -3.25f;

	// Token: 0x040000FE RID: 254
	private const float ThrustOffset = -2.15f;

	// Token: 0x040000FF RID: 255
	private static readonly FloatRange ThrustRange = new FloatRange(-1.84f, 1.84f);

	// Token: 0x04000100 RID: 256
	private static readonly FloatRange SteeringRange = new FloatRange(-30f, 30f);

	// Token: 0x04000101 RID: 257
	public Collider2D Thrust;

	// Token: 0x04000102 RID: 258
	public Collider2D Steering;

	// Token: 0x04000103 RID: 259
	public SpriteRenderer ThrustTarget;

	// Token: 0x04000104 RID: 260
	public SpriteRenderer SteeringTarget;

	// Token: 0x04000105 RID: 261
	private Controller controller = new Controller();

	// Token: 0x04000106 RID: 262
	private float TargetThrustY;

	// Token: 0x04000107 RID: 263
	private float TargetSteeringRot;

	// Token: 0x04000108 RID: 264
	private bool thrustLocked;

	// Token: 0x04000109 RID: 265
	private bool steeringLocked;

	// Token: 0x0400010A RID: 266
	private float startAngle;

	// Token: 0x0400010B RID: 267
	public AudioClip HornSound;

	// Token: 0x0400010C RID: 268
	private bool prevHadLeftInput;

	// Token: 0x0400010D RID: 269
	private bool prevHadRightInput;

	// Token: 0x0400010E RID: 270
	private Vector2 prevRightStickInput = Vector2.zero;

	// Token: 0x0400010F RID: 271
	private bool prevThrustWasGood;

	// Token: 0x04000110 RID: 272
	private bool prevSteeringWasGood;

	// Token: 0x04000111 RID: 273
	private const float rotationSensitivity = 0.035f;

	// Token: 0x04000112 RID: 274
	private const float hintVibrationIntensity = 0.5f;
}
