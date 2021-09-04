using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class WaterWheelGame : Minigame
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x0600080B RID: 2059 RVA: 0x00034411 File Offset: 0x00032611
	// (set) Token: 0x0600080C RID: 2060 RVA: 0x00034420 File Offset: 0x00032620
	private float Water
	{
		get
		{
			return this.WaterLevel.Value;
		}
		set
		{
			float num = Mathf.Clamp(value, 0f, 1f);
			this.WaterLevel.Value = num;
			this.MyNormTask.Data[base.ConsoleId] = (byte)(num * 255f);
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00034464 File Offset: 0x00032664
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.WaterLevel.Value = (float)this.MyNormTask.Data[base.ConsoleId] / 255f;
		if (Constants.ShouldPlaySfx())
		{
			this.fillSound = SoundManager.Instance.PlaySound(this.FillStart, false, 1f);
		}
		base.SetupInput(true);
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x000344C8 File Offset: 0x000326C8
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.Water += Time.deltaTime * this.Rate;
		//if (this.fillSound && !this.fillSound.isPlaying)
		//{
		//	this.fillSound = SoundManager.Instance.PlaySound(this.FillLoop, true, 1f);
		//}
		//if (this.fillSound)
		//{
		//	this.fillSound.volume = Mathf.Lerp(0f, 1f, this.Rate * 5f);
		//	this.fillSound.pitch = Mathf.Lerp(0.75f, 1.25f, this.Water);
		//}
		//Vector2 vector = ReInput.players.GetPlayer(0).GetAxis2DRaw(13, 14);
		//if (vector.magnitude > 0.9f)
		//{
		//	vector = vector.normalized;
		//	if (this.hadPrev)
		//	{
		//		float num = this.prevStickInput.AngleSigned(vector);
		//		if (num > 180f)
		//		{
		//			num -= 360f;
		//		}
		//		if (num < -180f)
		//		{
		//			num += 360f;
		//		}
		//		num /= (float)this.WheelScale;
		//		Vector3 localEulerAngles = this.Wheel.transform.localEulerAngles;
		//		float z = localEulerAngles.z;
		//		float num2 = Mathf.Clamp(localEulerAngles.z + num, 0.0001f, 358.99f);
		//		if (Mathf.Abs(localEulerAngles.z - num2) > 1f && Constants.ShouldPlaySfx())
		//		{
		//			AudioSource audioSource = SoundManager.Instance.PlaySound(this.WheelTurn, false, 1f);
		//			if (audioSource.timeSamples == 0)
		//			{
		//				audioSource.pitch = FloatRange.Next(0.9f, 1.1f);
		//			}
		//		}
		//		localEulerAngles.z = num2;
		//		this.Wheel.transform.localEulerAngles = localEulerAngles;
		//		this.Rate += num / (float)(360 * this.WheelScale);
		//		float num3 = Mathf.Abs(z - num2) * 0.01f;
		//		VibrationManager.Vibrate(num3, num3, 0.02f, VibrationManager.VibrationFalloff.None, null, false);
		//	}
		//	this.prevStickInput = vector;
		//	this.hadPrev = true;
		//}
		//else
		//{
		//	this.hadPrev = false;
		//}
		//if (this.grabbed)
		//{
		//	Controller controller = DestroyableSingleton<PassiveButtonManager>.Instance.controller;
		//	Vector2 vector2 = this.Wheel.transform.position;
		//	float num4 = (controller.DragStartPosition - vector2).AngleSigned(controller.DragPosition - vector2);
		//	if (num4 > 180f)
		//	{
		//		num4 -= 360f;
		//	}
		//	if (num4 < -180f)
		//	{
		//		num4 += 360f;
		//	}
		//	num4 /= (float)this.WheelScale;
		//	Vector3 localEulerAngles2 = this.Wheel.transform.localEulerAngles;
		//	float num5 = Mathf.Clamp(localEulerAngles2.z + num4, 0.0001f, 358.99f);
		//	if (Mathf.Abs(localEulerAngles2.z - num5) > 1f && Constants.ShouldPlaySfx())
		//	{
		//		AudioSource audioSource2 = SoundManager.Instance.PlaySound(this.WheelTurn, false, 1f);
		//		if (audioSource2.timeSamples == 0)
		//		{
		//			audioSource2.pitch = FloatRange.Next(0.9f, 1.1f);
		//		}
		//	}
		//	localEulerAngles2.z = num5;
		//	this.Wheel.transform.localEulerAngles = localEulerAngles2;
		//	this.Rate += num4 / (float)(360 * this.WheelScale);
		//	controller.ResetDragPosition();
		//}
		//else if (this.WaterLevel.Value >= 1f)
		//{
		//	this.MyNormTask.NextStep();
		//	base.StartCoroutine(base.CoStartClose(0.75f));
		//}
		//Vector3 localPosition = this.Watertop.transform.localPosition;
		//localPosition.y = this.WaterLevel.TopY;
		//this.Watertop.transform.localPosition = localPosition;
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00034884 File Offset: 0x00032A84
	public void Grab()
	{
		this.grabbed = !this.grabbed;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00034895 File Offset: 0x00032A95
	public override void Close()
	{
		if (this.fillSound)
		{
			this.fillSound.Stop();
		}
		base.Close();
	}

	// Token: 0x04000972 RID: 2418
	public SpriteRenderer Wheel;

	// Token: 0x04000973 RID: 2419
	public VerticalSpriteGauge WaterLevel;

	// Token: 0x04000974 RID: 2420
	public SpriteRenderer Watertop;

	// Token: 0x04000975 RID: 2421
	public int WheelScale = 4;

	// Token: 0x04000976 RID: 2422
	public AudioClip FillStart;

	// Token: 0x04000977 RID: 2423
	public AudioClip FillLoop;

	// Token: 0x04000978 RID: 2424
	public AudioClip WheelTurn;

	// Token: 0x04000979 RID: 2425
	private float Rate = 0.01f;

	// Token: 0x0400097A RID: 2426
	private AudioSource fillSound;

	// Token: 0x0400097B RID: 2427
	private Vector2 prevStickInput = Vector2.zero;

	// Token: 0x0400097C RID: 2428
	private bool hadPrev;

	// Token: 0x0400097D RID: 2429
	private bool grabbed;
}
