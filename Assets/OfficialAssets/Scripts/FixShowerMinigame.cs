using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class FixShowerMinigame : Minigame
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600013D RID: 317 RVA: 0x00008BD7 File Offset: 0x00006DD7
	private float Power
	{
		get
		{
			return Mathf.Sin(-1.5707964f + this.powerTime * 4f) / 2f + 0.5f;
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00008BFC File Offset: 0x00006DFC
	public void Start()
	{
		this.showerPos = BitConverter.ToSingle(this.MyNormTask.Data, 0);
		this.showerHead.transform.localEulerAngles = new Vector3(0f, 0f, this.showerAngles.Lerp(this.showerPos));
		this.powerBar.gameObject.SetActive(false);
		this.mallet.enabled = false;
		base.SetupInput(true);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00008C74 File Offset: 0x00006E74
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//if (this.animating)
		//{
		//	return;
		//}
		//this.controller.Update();
		//if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	bool flag = this.showerPos < 0.5f;
		//	if (this.leftGlyph.activeSelf != flag)
		//	{
		//		this.leftGlyph.SetActive(flag);
		//	}
		//	if (this.rightGlyph.activeSelf == flag)
		//	{
		//		this.rightGlyph.SetActive(!flag);
		//	}
		//	bool button = player.GetButton(flag ? 24 : 21);
		//	if (button)
		//	{
		//		if (!this.prevButtonHeld)
		//		{
		//			this.powerBar.gameObject.SetActive(true);
		//			this.powerTime = 0f;
		//			this.powerBar.SetValue(0f);
		//		}
		//		else
		//		{
		//			this.powerTime += Time.deltaTime;
		//			this.powerBar.SetValue(this.Power);
		//		}
		//	}
		//	else if (this.prevButtonHeld)
		//	{
		//		this.powerBar.gameObject.SetActive(false);
		//		base.StartCoroutine(this.Bash(this.Power * 0.32f + 0.04f));
		//	}
		//	this.prevButtonHeld = button;
		//	return;
		//}
		//switch (this.controller.CheckDrag(this.showerHead))
		//{
		//case DragState.TouchStart:
		//	this.powerBar.gameObject.SetActive(true);
		//	this.powerTime = 0f;
		//	this.powerBar.SetValue(0f);
		//	return;
		//case DragState.Holding:
		//case DragState.Dragging:
		//	this.powerTime += Time.deltaTime;
		//	this.powerBar.SetValue(this.Power);
		//	return;
		//case DragState.Released:
		//	this.powerBar.gameObject.SetActive(false);
		//	base.StartCoroutine(this.Bash(this.Power * 0.32f + 0.04f));
		//	return;
		//default:
		//	return;
		//}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00008E51 File Offset: 0x00007051
	public IEnumerator Bash(float power)
	{
		this.animating = true;
		this.mallet.transform.localEulerAngles = Vector3.zero;
		if ((double)this.showerPos < 0.5)
		{
			this.mallet.flipX = false;
			this.mallet.transform.localPosition = new Vector3(-2.5f, -0.4f, 0f);
		}
		else
		{
			this.mallet.flipX = true;
			this.mallet.transform.localPosition = new Vector3(2.5f, -0.4f, 0f);
		}
		this.mallet.enabled = true;
		yield return Effects.Wait(0.05f);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.swingSound, false, 1f);
		}
		yield return Effects.Lerp(0.15f, delegate(float t)
		{
			float num = this.hammerAngles.Lerp(this.hammerAnim.Evaluate(t));
			if (this.mallet.flipX)
			{
				num = -num;
			}
			this.mallet.transform.localEulerAngles = new Vector3(0f, 0f, num);
		});
		if (this.showerPos < 0.5f)
		{
			VibrationManager.Vibrate(power, 0f, 0.3f, VibrationManager.VibrationFalloff.Linear, null, false);
		}
		else
		{
			VibrationManager.Vibrate(0f, power, 0.3f, VibrationManager.VibrationFalloff.Linear, null, false);
		}
		if ((double)this.showerPos > 0.5)
		{
			power = -power;
		}
		this.showerPos += power;
		this.showerHead.transform.localEulerAngles = new Vector3(0f, 0f, this.showerAngles.Lerp(this.showerPos));
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.bashSounds.Random<AudioClip>(), false, 1f);
		}
		yield return Effects.Wait(0.05f);
		this.mallet.enabled = false;
		if (Mathf.Abs(this.showerPos - 0.5f) < 0.07f)
		{
			this.MyNormTask.NextStep();
			yield return base.CoStartClose(0.75f);
		}
		this.animating = false;
		yield break;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00008E67 File Offset: 0x00007067
	public override void Close()
	{
		this.MyNormTask.Data = BitConverter.GetBytes(this.showerPos);
		base.Close();
	}

	// Token: 0x04000183 RID: 387
	private const float PowerRatio = 0.32f;

	// Token: 0x04000184 RID: 388
	private const float BasePower = 0.04f;

	// Token: 0x04000185 RID: 389
	private const float CompleteTolerance = 0.07f;

	// Token: 0x04000186 RID: 390
	private FloatRange hammerAngles = new FloatRange(0f, -45f);

	// Token: 0x04000187 RID: 391
	private FloatRange showerAngles = new FloatRange(-30f, 30f);

	// Token: 0x04000188 RID: 392
	private float showerPos;

	// Token: 0x04000189 RID: 393
	public SpriteRenderer mallet;

	// Token: 0x0400018A RID: 394
	public Collider2D showerHead;

	// Token: 0x0400018B RID: 395
	public AnimationCurve hammerAnim;

	// Token: 0x0400018C RID: 396
	private Controller controller = new Controller();

	// Token: 0x0400018D RID: 397
	private float powerTime;

	// Token: 0x0400018E RID: 398
	public PowerBar powerBar;

	// Token: 0x0400018F RID: 399
	public AudioClip[] bashSounds;

	// Token: 0x04000190 RID: 400
	public AudioClip swingSound;

	// Token: 0x04000191 RID: 401
	public GameObject leftGlyph;

	// Token: 0x04000192 RID: 402
	public GameObject rightGlyph;

	// Token: 0x04000193 RID: 403
	private bool prevButtonHeld;

	// Token: 0x04000194 RID: 404
	private bool animating;
}
