using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class ElecLeverGame : Minigame
{
	// Token: 0x0600012A RID: 298 RVA: 0x0000820C File Offset: 0x0000640C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.NumberImage.sprite = this.NumberIcons[this.MyNormTask.Data.IndexOf((byte b) => (int)b == base.ConsoleId)];
		this.ResetLights();
		base.SetupInput(true);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000825C File Offset: 0x0000645C
	private void ResetLights()
	{
		int taskStep = this.MyNormTask.TaskStep;
		for (int i = 0; i < this.Lights.Length; i++)
		{
			this.Lights[i].sprite = ((i < taskStep) ? this.LightOn : this.LightOff);
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x000082A8 File Offset: 0x000064A8
	private void SetLights(bool on)
	{
		byte b = this.MyNormTask.Data[0];
		for (int i = 0; i < this.Lights.Length; i++)
		{
			this.Lights[i].sprite = (on ? this.LightOn : this.LightOff);
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000082F4 File Offset: 0x000064F4
	private void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.controller.Update();
		//Vector3 localPosition = this.Handle.transform.localPosition;
		//float num = this.HandleRange.ReverseLerp(localPosition.y);
		//if (!this.finished)
		//{
		//	if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//	{
		//		Player player = ReInput.players.GetPlayer(0);
		//		float num2 = player.GetAxisRaw(14);
		//		float num3 = player.GetAxisRaw(17);
		//		num2 = Mathf.Clamp01(-num2);
		//		num3 = Mathf.Clamp01(-num3);
		//		Vector2 vector = 0.15f * new Vector2((num2 > 0.7f) ? num2 : 0f, (num3 > 0.7f) ? num3 : 0f);
		//		int num4 = 0;
		//		if (num2 > 0.7f)
		//		{
		//			num4++;
		//		}
		//		if (num3 > 0.7f)
		//		{
		//			num4++;
		//		}
		//		switch (num4)
		//		{
		//		case 0:
		//		{
		//			Vector3 localScale = this.Bars.transform.localScale;
		//			localScale.y = 1f;
		//			this.Handle.transform.localScale = localScale;
		//			localPosition.y = Mathf.Lerp(localPosition.y, this.HandleRange.max, num + Time.deltaTime * 15f);
		//			break;
		//		}
		//		case 1:
		//		{
		//			float num5 = Mathf.Max(num2, num3);
		//			if (num > 0.9f)
		//			{
		//				num -= num5 * Time.deltaTime;
		//				if (num < 0.9f)
		//				{
		//					num = 0.9f;
		//				}
		//				else
		//				{
		//					VibrationManager.Vibrate(vector.x, vector.y, 1E-05f, VibrationManager.VibrationFalloff.None, null, false);
		//				}
		//				localPosition.y = this.HandleRange.Lerp(num);
		//				this.Handle.transform.localPosition = localPosition;
		//				Vector3 localScale2 = this.Bars.transform.localScale;
		//				localScale2.y = this.HandleRange.ChangeRange(localPosition.y, 0.25f, 1f);
		//				this.Handle.transform.localScale = localScale2;
		//			}
		//			break;
		//		}
		//		case 2:
		//		{
		//			float num6 = (num2 + num3) * 0.5f;
		//			if (num > 0.7f)
		//			{
		//				num -= num6 * Time.deltaTime;
		//				localPosition.y = this.HandleRange.Lerp(num);
		//				VibrationManager.Vibrate(vector.x, vector.y, 1E-05f, VibrationManager.VibrationFalloff.None, null, false);
		//				this.Handle.transform.localPosition = localPosition;
		//				Vector3 localScale3 = this.Bars.transform.localScale;
		//				localScale3.y = this.HandleRange.ChangeRange(localPosition.y, 0.25f, 1f);
		//				this.Handle.transform.localScale = localScale3;
		//			}
		//			else
		//			{
		//				Vector3 localScale4 = this.Bars.transform.localScale;
		//				localScale4.y = -1f;
		//				this.Handle.transform.localScale = localScale4;
		//				VibrationManager.Vibrate(0.5f, 0.5f, 0.2f, VibrationManager.VibrationFalloff.Linear, null, false);
		//				localPosition.y = this.HandleRange.min;
		//				this.finished = true;
		//				base.StartCoroutine(this.FinishUp());
		//			}
		//			break;
		//		}
		//		}
		//	}
		//	else
		//	{
		//		DragState dragState = this.controller.CheckDrag(this.Handle);
		//		if (dragState != DragState.NoTouch)
		//		{
		//			if (dragState == DragState.Dragging)
		//			{
		//				if (num > 0.7f)
		//				{
		//					Vector2 vector2 = this.controller.DragPosition - base.transform.position;
		//					float num7 = this.HandleRange.ReverseLerp(this.HandleRange.Clamp(vector2.y));
		//					localPosition.y = this.HandleRange.Lerp(num7 / 2f + 0.5f);
		//					this.Handle.transform.localPosition = localPosition;
		//					Vector3 localScale5 = this.Bars.transform.localScale;
		//					localScale5.y = this.HandleRange.ChangeRange(localPosition.y, 0.25f, 1f);
		//					this.Handle.transform.localScale = localScale5;
		//				}
		//				else
		//				{
		//					Vector3 localScale6 = this.Bars.transform.localScale;
		//					localScale6.y = -1f;
		//					this.Handle.transform.localScale = localScale6;
		//					localPosition.y = this.HandleRange.min;
		//				}
		//			}
		//		}
		//		else if (num > 0.7f)
		//		{
		//			Vector3 localScale7 = this.Bars.transform.localScale;
		//			localScale7.y = 1f;
		//			this.Handle.transform.localScale = localScale7;
		//			localPosition.y = Mathf.Lerp(localPosition.y, this.HandleRange.max, num + Time.deltaTime * 15f);
		//		}
		//		else
		//		{
		//			this.finished = true;
		//			base.StartCoroutine(this.FinishUp());
		//		}
		//	}
		//}
		//this.Handle.transform.localPosition = localPosition;
		//Vector3 localScale8 = this.Bars.transform.localScale;
		//localScale8.y = this.HandleRange.ChangeRange(localPosition.y, -1f, 1f);
		//this.Bars.transform.localScale = localScale8;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00008819 File Offset: 0x00006A19
	private IEnumerator FinishUp()
	{
		if ((int)this.MyNormTask.Data[this.MyNormTask.taskStep] == base.ConsoleId)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.correctSound, false, 1f);
			}
			this.Lights[this.MyNormTask.taskStep].sprite = this.LightOn;
			NormalPlayerTask myNormTask = this.MyNormTask;
			if (myNormTask != null)
			{
				myNormTask.NextStep();
			}
			yield return Effects.Wait(0.25f);
			this.Close();
		}
		else
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.incorrectSound, false, 1f);
			}
			this.SetLights(false);
			yield return Effects.Wait(0.1f);
			Vector3 localScale = this.Bars.transform.localScale;
			localScale.y = 1f;
			this.Handle.transform.localScale = localScale;
			Vector3 localPosition = this.Handle.transform.localPosition;
			localPosition.y = this.HandleRange.max;
			this.Handle.transform.localPosition = localPosition;
			Vector3 localScale2 = this.Bars.transform.localScale;
			localScale2.y = this.HandleRange.ChangeRange(localPosition.y, -1f, 1f);
			this.Bars.transform.localScale = localScale2;
			this.SetLights(true);
			yield return Effects.Wait(0.5f);
			this.ResetLights();
			yield return Effects.Wait(0.25f);
			this.Close();
		}
		yield break;
	}

	// Token: 0x04000167 RID: 359
	public Sprite[] NumberIcons;

	// Token: 0x04000168 RID: 360
	public SpriteRenderer NumberImage;

	// Token: 0x04000169 RID: 361
	public SpriteRenderer[] Lights;

	// Token: 0x0400016A RID: 362
	public Sprite LightOn;

	// Token: 0x0400016B RID: 363
	public Sprite LightOff;

	// Token: 0x0400016C RID: 364
	public Collider2D Handle;

	// Token: 0x0400016D RID: 365
	public SpriteRenderer Bars;

	// Token: 0x0400016E RID: 366
	private FloatRange HandleRange = new FloatRange(-2.75f, 0.87f);

	// Token: 0x0400016F RID: 367
	private bool finished;

	// Token: 0x04000170 RID: 368
	private Controller controller = new Controller();

	// Token: 0x04000171 RID: 369
	public AudioClip correctSound;

	// Token: 0x04000172 RID: 370
	public AudioClip incorrectSound;
}
