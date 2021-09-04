using System;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class SafeMinigame : Minigame
{
	// Token: 0x06000168 RID: 360 RVA: 0x0000A702 File Offset: 0x00008902
	public void OnEnable()
	{
		this.Begin(null);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000A70C File Offset: 0x0000890C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.Spinner.GetComponent<SpriteRenderer>().color = Palette.DisabledGrey;
		do
		{
			for (int i = 0; i < this.combo.Length; i++)
			{
				this.combo[i] = IntRange.Next(8) + 1;
			}
		}
		while (this.combo[0] == this.combo[1] || this.combo[1] == this.combo[2]);
		Vector3 eulerAngles = this.Tumbler.transform.eulerAngles;
		eulerAngles.z = new FloatRange(0f, 360f).NextMinDistance((float)(this.combo[0] * 45), 45f);
		this.Tumbler.transform.eulerAngles = eulerAngles;
		this.combo[1] = -this.combo[1];
		this.UpdateComboInstructions();
		this.loopSound = SoundManager.Instance.GetNamedAudioSource("spinnerLoop");
		this.loopSound.volume = 0f;
		this.loopSound.pitch = 0.5f;
		this.loopSound.loop = false;
		this.loopSound.clip = this.SpinnerFreeSound;
		base.SetupInput(true);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000A83C File Offset: 0x00008A3C
	private void UpdateComboInstructions()
	{
		int num = this.latched.LastIndexOf((bool b) => b) + 2;
		this.ComboText.Text = string.Join<int>(" - ", from c in this.combo.Take(num)
		select Mathf.Abs(c) - 1);
		for (int i = 0; i < this.Arrows.Length; i++)
		{
			this.Arrows[i].enabled = (i < num);
		}
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000A8E0 File Offset: 0x00008AE0
	private void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.controller.Update();
		//Player player = ReInput.players.GetPlayer(0);
		//float delta = 0f;
		//bool flag = this.latched.All((bool b) => b);
		//if (!flag)
		//{
		//	int num = this.latched.IndexOf((bool b) => !b);
		//	int num2 = this.combo[num] * 45;
		//	Vector3 eulerAngles = this.Tumbler.transform.eulerAngles;
		//	if (eulerAngles.z < 0f)
		//	{
		//		eulerAngles.z += 360f;
		//	}
		//	bool flag2 = false;
		//	if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//	{
		//		Vector2 vector = player.GetAxis2DRaw(13, 14);
		//		float magnitude = vector.magnitude;
		//		if (magnitude > 0.9f)
		//		{
		//			vector /= magnitude;
		//			if (!this.prevHadLeftInput)
		//			{
		//				this.lastTumDir = 0f;
		//				this.leftStickStartAngle = Mathf.Atan2(vector.y, vector.x) - eulerAngles.z * 0.017453292f;
		//			}
		//			else
		//			{
		//				delta = Vector2.SignedAngle(this.prevLeftInput, vector) * 0.017453292f;
		//				eulerAngles.z = (Mathf.Atan2(vector.y, vector.x) - this.leftStickStartAngle) * 57.29578f;
		//				this.Tumbler.transform.eulerAngles = eulerAngles;
		//				flag2 = true;
		//			}
		//			this.prevLeftInput = vector;
		//			this.prevHadLeftInput = true;
		//		}
		//		else
		//		{
		//			if (this.prevHadLeftInput && num == this.combo.Length - 1)
		//			{
		//				if (this.AngleNear(eulerAngles.z + 45f, this.lastTumDir, (float)num2, 7f))
		//				{
		//					this.latched[num] = true;
		//				}
		//				this.UpdateComboInstructions();
		//			}
		//			this.prevHadLeftInput = false;
		//		}
		//	}
		//	else
		//	{
		//		switch (this.controller.CheckDrag(this.Tumbler))
		//		{
		//		case DragState.TouchStart:
		//		{
		//			this.lastTumDir = 0f;
		//			Vector2 vector2 = this.controller.DragPosition - this.Tumbler.transform.position;
		//			vector2.Normalize();
		//			this.leftStickStartAngle = Mathf.Atan2(vector2.y, vector2.x) - eulerAngles.z * 0.017453292f;
		//			this.lastMouseVec = vector2;
		//			break;
		//		}
		//		case DragState.Dragging:
		//		{
		//			Vector2 vector3 = this.controller.DragPosition - this.Tumbler.transform.position;
		//			vector3.Normalize();
		//			delta = Vector2.SignedAngle(this.lastMouseVec, vector3) * 0.017453292f;
		//			eulerAngles.z = (Mathf.Atan2(vector3.y, vector3.x) - this.leftStickStartAngle) * 57.29578f;
		//			flag2 = true;
		//			this.Tumbler.transform.eulerAngles = eulerAngles;
		//			this.lastMouseVec = vector3;
		//			break;
		//		}
		//		case DragState.Released:
		//			if (num == this.combo.Length - 1)
		//			{
		//				if (this.AngleNear(eulerAngles.z + 45f, this.lastTumDir, (float)num2, 7f))
		//				{
		//					this.latched[num] = true;
		//				}
		//				this.UpdateComboInstructions();
		//			}
		//			break;
		//		}
		//	}
		//	if (flag2)
		//	{
		//		this.CheckTumblr(delta, eulerAngles.z, num, num2);
		//	}
		//	if (this.latched.All((bool b) => b))
		//	{
		//		this.spinDel = 0f;
		//		this.controller.ClearTouch();
		//		this.TumblerBehind.color = Palette.DisabledGrey;
		//		this.Tumbler.GetComponent<SpriteRenderer>().color = Palette.DisabledGrey;
		//		this.Spinner.GetComponent<SpriteRenderer>().color = Color.white;
		//	}
		//}
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Vector2 vector4 = player.GetAxis2DRaw(16, 17);
		//	float magnitude2 = vector4.magnitude;
		//	if (magnitude2 > 0.9f)
		//	{
		//		vector4 /= magnitude2;
		//		if (!this.prevHadRightInput)
		//		{
		//			this.spinTime = 0f;
		//		}
		//		else
		//		{
		//			this.spinTime += Time.deltaTime;
		//			Vector2 vector5 = this.Spinner.transform.position;
		//			(this.lastMouseVec - vector5).WheelAngle(this.controller.DragPosition - vector5);
		//			this.lastMouseVec = this.controller.DragPosition;
		//			float num3 = this.spinVel;
		//			float num4 = Vector2.SignedAngle(this.prevRightInput, vector4);
		//			this.spinVel += num4 / (3f * this.spinTime);
		//			if (this.spinVel > this.TopSpinRate)
		//			{
		//				this.spinVel = this.TopSpinRate;
		//			}
		//			if (this.spinVel < -this.TopSpinRate)
		//			{
		//				this.spinVel = -this.TopSpinRate;
		//			}
		//			if (Mathf.Sign(num3) != Mathf.Sign(this.spinVel) && Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.SpinnerStartSound, false, 1f);
		//			}
		//		}
		//		this.prevRightInput = vector4;
		//		this.prevHadRightInput = true;
		//	}
		//	else
		//	{
		//		this.prevHadRightInput = false;
		//	}
		//}
		//else
		//{
		//	DragState dragState = this.controller.CheckDrag(this.Spinner);
		//	if (dragState != DragState.TouchStart)
		//	{
		//		if (dragState == DragState.Dragging)
		//		{
		//			this.spinTime += Time.deltaTime;
		//			Vector2 vector6 = this.Spinner.transform.position;
		//			float num5 = (this.lastMouseVec - vector6).WheelAngle(this.controller.DragPosition - vector6);
		//			this.lastMouseVec = this.controller.DragPosition;
		//			float num6 = this.spinVel;
		//			this.spinVel += num5 / (2.5f * this.spinTime);
		//			if (this.spinVel > this.TopSpinRate)
		//			{
		//				this.spinVel = this.TopSpinRate;
		//			}
		//			if (this.spinVel < -this.TopSpinRate)
		//			{
		//				this.spinVel = -this.TopSpinRate;
		//			}
		//			if (Mathf.Sign(num6) != Mathf.Sign(this.spinVel) && Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.SpinnerStartSound, false, 1f);
		//			}
		//		}
		//	}
		//	else
		//	{
		//		this.lastMouseVec = this.controller.DragPosition;
		//		this.spinTime = 0f;
		//	}
		//}
		//if (Constants.ShouldPlaySfx())
		//{
		//	float num7 = Mathf.Min(1f, Mathf.Abs(this.spinVel / 60f));
		//	float volume = num7 * num7 * num7;
		//	this.loopSound.volume = volume;
		//	float num8 = Mathf.Lerp(0.5f, 1.25f, num7);
		//	this.loopSound.pitch = Mathf.Lerp(this.loopSound.pitch, num8, Time.deltaTime);
		//}
		//float num9 = this.Spinner.transform.localEulerAngles.z - 180f;
		//float num10 = this.spinVel * Time.deltaTime;
		//this.Spinner.transform.Rotate(0f, 0f, num10);
		//float num11 = this.Spinner.transform.localEulerAngles.z - 180f;
		//if (Constants.ShouldPlaySfx() && Mathf.Sign(num9) != Mathf.Sign(num11))
		//{
		//	this.loopSound.Play();
		//}
		//this.spinDel += num10;
		//this.spinVel = Mathf.Lerp(this.spinVel, 0f, Time.deltaTime);
		//if (flag && Mathf.Abs(this.spinDel) > 720f)
		//{
		//	if (Constants.ShouldPlaySfx())
		//	{
		//		SoundManager.Instance.PlaySound(this.SpinnerStopSound, false, 1f);
		//	}
		//	this.MyNormTask.NextStep();
		//	this.Close();
		//}
	}

	// Token: 0x0600016C RID: 364 RVA: 0x0000B0C8 File Offset: 0x000092C8
	private void CheckTumblr(float delta, float tumRotZ, int unlatched, int expected)
	{
		float num = this.lastTumDir;
		float num2 = -Mathf.Sign(delta);
		if (num2 != 0f && num != num2)
		{
			this.reversalBuffer += delta;
			if (Mathf.Abs(this.reversalBuffer) > 0.15f)
			{
				if (Constants.ShouldPlaySfx())
				{
					SoundManager.Instance.PlaySound(this.DialTurnSound, false, 1f);
				}
				this.lastTumDir = -Mathf.Sign(delta);
				this.reversalBuffer = 0f;
			}
		}
		if (num != 0f && this.lastTumDir != num)
		{
			if (this.AngleNear(tumRotZ + 45f, num, (float)expected, 7f))
			{
				if (Constants.ShouldPlaySfx())
				{
					SoundManager.Instance.PlaySound(this.DialGoodSound, false, 1f);
				}
				this.latched[unlatched] = true;
			}
			else
			{
				this.latched = new bool[3];
				this.vibration = new bool[3];
			}
			this.UpdateComboInstructions();
			return;
		}
		if (this.AngleNear(tumRotZ + 45f, num, (float)expected, 5f))
		{
			this.latched[unlatched] = true;
			this.UpdateComboInstructions();
			this.latched[unlatched] = false;
			if (this.AngleNear(tumRotZ + 45f, num, (float)expected, 7f))
			{
				this.reversalBuffer = 0.15f * -num2;
				if (!this.vibration[unlatched])
				{
					VibrationManager.Vibrate(0.5f, 0.5f, 0.2f, VibrationManager.VibrationFalloff.Linear, null, false);
					this.vibration[unlatched] = true;
				}
			}
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000B237 File Offset: 0x00009437
	public override void Close()
	{
		SoundManager.Instance.StopNamedSound("spinnerLoop");
		base.Close();
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000B250 File Offset: 0x00009450
	private bool AngleNear(float actual, float dir, float expected, float Threshold)
	{
		if (actual < 0f)
		{
			actual += 360f;
		}
		if (Mathf.Sign(dir) != Mathf.Sign(expected))
		{
			return false;
		}
		expected = Mathf.Abs(expected);
		if (actual < 90f && expected > 270f)
		{
			actual += 360f;
		}
		if (expected < 90f && actual > 270f)
		{
			expected += 360f;
		}
		return actual >= expected - Threshold && actual <= expected + Threshold;
	}

	// Token: 0x040001DF RID: 479
	private const float LowTumblerThreshold = 5f;

	// Token: 0x040001E0 RID: 480
	private const float HighTumblerThreshold = 7f;

	// Token: 0x040001E1 RID: 481
	private const float ReverseDirThreshold = 0.15f;

	// Token: 0x040001E2 RID: 482
	public TextRenderer ComboText;

	// Token: 0x040001E3 RID: 483
	public Collider2D Tumbler;

	// Token: 0x040001E4 RID: 484
	public SpriteRenderer TumblerBehind;

	// Token: 0x040001E5 RID: 485
	public Collider2D Spinner;

	// Token: 0x040001E6 RID: 486
	public SpriteRenderer[] Arrows;

	// Token: 0x040001E7 RID: 487
	private int[] combo = new int[3];

	// Token: 0x040001E8 RID: 488
	private bool[] latched = new bool[3];

	// Token: 0x040001E9 RID: 489
	private bool[] vibration = new bool[3];

	// Token: 0x040001EA RID: 490
	private Controller controller = new Controller();

	// Token: 0x040001EB RID: 491
	private float lastTumDir;

	// Token: 0x040001EC RID: 492
	private Vector2 lastMouseVec;

	// Token: 0x040001ED RID: 493
	private float spinTime;

	// Token: 0x040001EE RID: 494
	private float spinDel;

	// Token: 0x040001EF RID: 495
	private float spinVel;

	// Token: 0x040001F0 RID: 496
	private float reversalBuffer;

	// Token: 0x040001F1 RID: 497
	public AudioClip DialTurnSound;

	// Token: 0x040001F2 RID: 498
	public AudioClip DialGoodSound;

	// Token: 0x040001F3 RID: 499
	public AudioClip SpinnerStartSound;

	// Token: 0x040001F4 RID: 500
	public AudioClip SpinnerFreeSound;

	// Token: 0x040001F5 RID: 501
	public AudioClip SpinnerStopSound;

	// Token: 0x040001F6 RID: 502
	private AudioSource loopSound;

	// Token: 0x040001F7 RID: 503
	private bool prevHadLeftInput;

	// Token: 0x040001F8 RID: 504
	private bool prevHadRightInput;

	// Token: 0x040001F9 RID: 505
	private Vector2 prevLeftInput = Vector2.zero;

	// Token: 0x040001FA RID: 506
	private Vector2 prevRightInput = Vector2.zero;

	// Token: 0x040001FB RID: 507
	private float leftStickStartAngle;

	// Token: 0x040001FC RID: 508
	public float TopSpinRate = 360f;
}
