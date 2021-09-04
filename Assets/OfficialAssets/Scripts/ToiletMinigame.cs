using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class ToiletMinigame : Minigame
{
	// Token: 0x0600017F RID: 383 RVA: 0x0000BAC7 File Offset: 0x00009CC7
	public override void Begin(PlayerTask task)
	{
		this.plungerSource = SoundManager.Instance.GetNamedAudioSource("plungerSource");
		base.Begin(task);
		base.SetupInput(true);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000BAEC File Offset: 0x00009CEC
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None || this.pressure >= 1f)
		//{
		//	return;
		//}
		//this.pressure -= Time.deltaTime * this.plungeScale / 2f;
		//if (this.pressure < 0f)
		//{
		//	this.pressure = 0f;
		//}
		//this.controller.Update();
		//Vector3 localPosition = this.Stick.transform.localPosition;
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	float axisRaw = ReInput.players.GetPlayer(0).GetAxisRaw(14);
		//	if (axisRaw < 0f)
		//	{
		//		if (this.controllerStickPos > 1f + axisRaw)
		//		{
		//			this.controllerStickPos = Mathf.Lerp(this.controllerStickPos, 1f + axisRaw, Time.deltaTime * 30f);
		//		}
		//	}
		//	else if (axisRaw > 0f)
		//	{
		//		if (this.controllerStickPos < axisRaw)
		//		{
		//			this.controllerStickPos = Mathf.Lerp(this.controllerStickPos, axisRaw, Time.deltaTime * 30f);
		//		}
		//	}
		//	else
		//	{
		//		this.controllerStickPos = Mathf.Lerp(this.controllerStickPos, 1f, Time.deltaTime);
		//	}
		//	localPosition.y = this.StickRange.Lerp(this.controllerStickPos);
		//	this.Stick.transform.localPosition = localPosition;
		//	if (this.lastY > localPosition.y)
		//	{
		//		this.pressure += (this.lastY - localPosition.y) * this.plungeScale;
		//		if (Constants.ShouldPlaySfx() && !this.plungerSource.isPlaying)
		//		{
		//			this.plungerSource.clip = this.plungeSounds.Random<AudioClip>();
		//			this.plungerSource.Play();
		//			VibrationManager.Vibrate(0.3f, 0.3f, 0.2f, VibrationManager.VibrationFalloff.Linear, null, false);
		//		}
		//	}
		//	this.lastY = localPosition.y;
		//}
		//else
		//{
		//	switch (this.controller.CheckDrag(this.Stick))
		//	{
		//	case DragState.TouchStart:
		//		this.lastY = localPosition.y;
		//		break;
		//	case DragState.Dragging:
		//		localPosition.y = this.StickRange.Clamp(this.StickRange.max + (this.controller.DragPosition.y - this.controller.DragStartPosition.y));
		//		this.Stick.transform.localPosition = localPosition;
		//		if (this.lastY > localPosition.y)
		//		{
		//			this.pressure += (this.lastY - localPosition.y) * this.plungeScale;
		//			if (Constants.ShouldPlaySfx() && !this.plungerSource.isPlaying)
		//			{
		//				this.plungerSource.clip = this.plungeSounds.Random<AudioClip>();
		//				this.plungerSource.Play();
		//			}
		//		}
		//		this.lastY = localPosition.y;
		//		break;
		//	case DragState.Released:
		//		localPosition.y = Mathf.Lerp(localPosition.y, this.StickRange.max, Time.deltaTime);
		//		this.Stick.transform.localPosition = localPosition;
		//		break;
		//	}
		//}
		//this.Plunger.sprite = ((localPosition.y < -0.75f) ? this.PlungerDown : this.PlungerUp);
		//this.Needle.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, -230f, this.pressure));
		//if (this.pressure >= 1f)
		//{
		//	base.StartCoroutine(this.Finish());
		//}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000BE64 File Offset: 0x0000A064
	private IEnumerator Finish()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.flushSound, false, 1f);
		}
		VibrationManager.Vibrate(2.5f, 2.5f, 0f, VibrationManager.VibrationFalloff.None, this.flushSound, false);
		this.MyNormTask.NextStep();
		yield return Effects.All(new IEnumerator[]
		{
			Effects.Shake(this.Pipes.transform, 0.65f, 0.05f, true),
			Effects.Rotate2D(this.Needle.transform, 230f, 0f, 0.6f)
		});
		this.Close();
		yield break;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000BE73 File Offset: 0x0000A073
	public override void Close()
	{
		SoundManager.Instance.StopNamedSound("plungerSource");
		base.Close();
	}

	// Token: 0x04000219 RID: 537
	public SpriteRenderer Needle;

	// Token: 0x0400021A RID: 538
	public SpriteRenderer Pipes;

	// Token: 0x0400021B RID: 539
	public const float StickDown = -0.75f;

	// Token: 0x0400021C RID: 540
	public FloatRange StickRange = new FloatRange(-0.85f, -0.4f);

	// Token: 0x0400021D RID: 541
	public Collider2D Stick;

	// Token: 0x0400021E RID: 542
	public SpriteRenderer Plunger;

	// Token: 0x0400021F RID: 543
	public Sprite PlungerUp;

	// Token: 0x04000220 RID: 544
	public Sprite PlungerDown;

	// Token: 0x04000221 RID: 545
	private float pressure;

	// Token: 0x04000222 RID: 546
	public Controller controller = new Controller();

	// Token: 0x04000223 RID: 547
	public float lastY;

	// Token: 0x04000224 RID: 548
	public float plungeScale = 0.5f;

	// Token: 0x04000225 RID: 549
	public AudioClip flushSound;

	// Token: 0x04000226 RID: 550
	public AudioClip[] plungeSounds;

	// Token: 0x04000227 RID: 551
	private AudioSource plungerSource;

	// Token: 0x04000228 RID: 552
	private float controllerStickPos = 1f;

	// Token: 0x04000229 RID: 553
	private const float controllerPlungeSpeed = 30f;
}
