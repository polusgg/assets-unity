using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class CardSlideGame : Minigame
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00004636 File Offset: 0x00002836
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardPleaseInsert, Array.Empty<object>());
		base.SetupInput(true);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00004668 File Offset: 0x00002868
	public void Update()
	{
		//if (this.MyNormTask.IsComplete)
		//{
		//	return;
		//}
		//Vector3 localPosition = this.col.transform.localPosition;
		//this.myController.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 vector = player.GetAxis2DRaw(13, 14);
		//	float magnitude = vector.magnitude;
		//	CardSlideGame.TaskStages state = this.State;
		//	if (state != CardSlideGame.TaskStages.Before)
		//	{
		//		if (state == CardSlideGame.TaskStages.Inserted)
		//		{
		//			if (magnitude > 0.9f)
		//			{
		//				vector = vector.normalized;
		//				if (this.hadPrev)
		//				{
		//					float num = this.prevStickInput.AngleSigned(vector);
		//					if (num > 180f)
		//					{
		//						num -= 360f;
		//					}
		//					if (num < -180f)
		//					{
		//						num += 360f;
		//					}
		//					float num2 = Mathf.Abs(num) * 0.025f;
		//					float x = localPosition.x;
		//					localPosition.x += num2;
		//					if (num2 > 0.01f)
		//					{
		//						this.dragTime += Time.deltaTime;
		//						this.redLight.color = this.gray;
		//						this.greenLight.color = this.gray;
		//						if (!this.moving)
		//						{
		//							this.moving = true;
		//							if (Constants.ShouldPlaySfx())
		//							{
		//								SoundManager.Instance.PlaySound(this.CardMove.Random<AudioClip>(), false, 1f);
		//							}
		//						}
		//					}
		//					localPosition.x = this.XRange.Clamp(localPosition.x);
		//					float num3 = localPosition.x - x;
		//					float num4 = this.XRange.ReverseLerp(localPosition.x);
		//					float num5 = 0.8f * num3;
		//					VibrationManager.Vibrate(num5 * (1f - num4), num5 * num4, 0.01f, VibrationManager.VibrationFalloff.None, null, false);
		//				}
		//				else
		//				{
		//					this.dragTime = 0f;
		//				}
		//				this.prevStickInput = vector;
		//				this.hadPrev = true;
		//			}
		//			else
		//			{
		//				if (this.hadPrev)
		//				{
		//					if (this.XRange.max - localPosition.x < 0.05f)
		//					{
		//						if (this.AcceptedTime.Contains(this.dragTime))
		//						{
		//							if (Constants.ShouldPlaySfx())
		//							{
		//								SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
		//							}
		//							this.State = CardSlideGame.TaskStages.After;
		//							this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardAccepted, Array.Empty<object>());
		//							base.StartCoroutine(this.PutCardBack());
		//							if (this.MyNormTask)
		//							{
		//								this.MyNormTask.NextStep();
		//							}
		//							this.redLight.color = this.gray;
		//							this.greenLight.color = this.green;
		//						}
		//						else
		//						{
		//							if (Constants.ShouldPlaySfx())
		//							{
		//								SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//							}
		//							if (this.AcceptedTime.max < this.dragTime)
		//							{
		//								this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardTooSlow, Array.Empty<object>());
		//							}
		//							else
		//							{
		//								this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardTooFast, Array.Empty<object>());
		//							}
		//							this.redLight.color = Color.red;
		//							this.greenLight.color = this.gray;
		//						}
		//					}
		//					else
		//					{
		//						if (Constants.ShouldPlaySfx())
		//						{
		//							SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//						}
		//						this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardBadRead, Array.Empty<object>());
		//						this.redLight.color = Color.red;
		//						this.greenLight.color = this.gray;
		//					}
		//				}
		//				localPosition.x = Mathf.Lerp(localPosition.x, this.XRange.min, Time.deltaTime * 4f);
		//				this.hadPrev = false;
		//				this.dragTime = 0f;
		//			}
		//		}
		//	}
		//	else if (player.GetAnyButtonDown())
		//	{
		//		this.State = CardSlideGame.TaskStages.Animating;
		//		base.StartCoroutine(this.InsertCard());
		//	}
		//}
		//else
		//{
		//	switch (this.myController.CheckDrag(this.col))
		//	{
		//	case DragState.NoTouch:
		//		if (this.State == CardSlideGame.TaskStages.Inserted)
		//		{
		//			localPosition.x = Mathf.Lerp(localPosition.x, this.XRange.min, Time.deltaTime * 4f);
		//		}
		//		break;
		//	case DragState.TouchStart:
		//		this.dragTime = 0f;
		//		break;
		//	case DragState.Dragging:
		//		if (this.State == CardSlideGame.TaskStages.Inserted)
		//		{
		//			Vector2 vector2 = this.myController.DragPosition - base.transform.position;
		//			vector2.x = this.XRange.Clamp(vector2.x);
		//			if (vector2.x - localPosition.x > 0.01f)
		//			{
		//				this.dragTime += Time.deltaTime;
		//				this.redLight.color = this.gray;
		//				this.greenLight.color = this.gray;
		//				if (!this.moving)
		//				{
		//					this.moving = true;
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.CardMove.Random<AudioClip>(), false, 1f);
		//					}
		//				}
		//			}
		//			localPosition.x = vector2.x;
		//		}
		//		break;
		//	case DragState.Released:
		//		this.moving = false;
		//		if (this.State == CardSlideGame.TaskStages.Before)
		//		{
		//			this.State = CardSlideGame.TaskStages.Animating;
		//			base.StartCoroutine(this.InsertCard());
		//		}
		//		else if (this.State == CardSlideGame.TaskStages.Inserted)
		//		{
		//			if (this.XRange.max - localPosition.x < 0.05f)
		//			{
		//				if (this.AcceptedTime.Contains(this.dragTime))
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
		//					}
		//					this.State = CardSlideGame.TaskStages.After;
		//					this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardAccepted, Array.Empty<object>());
		//					base.StartCoroutine(this.PutCardBack());
		//					if (this.MyNormTask)
		//					{
		//						this.MyNormTask.NextStep();
		//					}
		//					this.redLight.color = this.gray;
		//					this.greenLight.color = this.green;
		//				}
		//				else
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//					}
		//					if (this.AcceptedTime.max < this.dragTime)
		//					{
		//						this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardTooSlow, Array.Empty<object>());
		//					}
		//					else
		//					{
		//						this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardTooFast, Array.Empty<object>());
		//					}
		//					this.redLight.color = Color.red;
		//					this.greenLight.color = this.gray;
		//				}
		//			}
		//			else
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//				}
		//				this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardBadRead, Array.Empty<object>());
		//				this.redLight.color = Color.red;
		//				this.greenLight.color = this.gray;
		//			}
		//		}
		//		break;
		//	}
		//}
		//this.col.transform.localPosition = localPosition;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00004DAC File Offset: 0x00002FAC
	private IEnumerator PutCardBack()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.WalletOut, false, 1f);
		}
		Vector3 pos = this.col.transform.localPosition;
		Vector3 targ = new Vector3(-1.11f, -1.9f, pos.z);
		float time = 0f;
		for (;;)
		{
			float num = Mathf.Min(1f, time / 0.6f);
			this.col.transform.localPosition = Vector3.Lerp(pos, targ, num);
			this.col.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.75f, num);
			if (time > 0.6f)
			{
				break;
			}
			yield return null;
			time += Time.deltaTime;
		}
		base.StartCoroutine(base.CoStartClose(0.75f));
		yield break;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00004DBB File Offset: 0x00002FBB
	private IEnumerator InsertCard()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.WalletOut, false, 1f);
		}
		Vector3 pos = this.col.transform.localPosition;
		Vector3 targ = new Vector3(this.XRange.min, 0.75f, pos.z);
		float time = 0f;
		for (;;)
		{
			float num = Mathf.Min(1f, time / 0.6f);
			this.col.transform.localPosition = Vector3.Lerp(pos, targ, num);
			this.col.transform.localScale = Vector3.Lerp(Vector3.one * 0.75f, Vector3.one, num);
			if (time > 0.6f)
			{
				break;
			}
			yield return null;
			time += Time.deltaTime;
		}
		this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardPleaseSwipe, Array.Empty<object>());
		this.greenLight.color = this.green;
		this.State = CardSlideGame.TaskStages.Inserted;
		yield break;
	}

	// Token: 0x0400009C RID: 156
	private Color gray = new Color(0.45f, 0.45f, 0.45f);

	// Token: 0x0400009D RID: 157
	private Color green = new Color(0f, 0.8f, 0f);

	// Token: 0x0400009E RID: 158
	private CardSlideGame.TaskStages State;

	// Token: 0x0400009F RID: 159
	private Controller myController = new Controller();

	// Token: 0x040000A0 RID: 160
	private FloatRange XRange = new FloatRange(-2.38f, 2.38f);

	// Token: 0x040000A1 RID: 161
	public FloatRange AcceptedTime = new FloatRange(0.4f, 0.6f);

	// Token: 0x040000A2 RID: 162
	public Collider2D col;

	// Token: 0x040000A3 RID: 163
	public SpriteRenderer redLight;

	// Token: 0x040000A4 RID: 164
	public SpriteRenderer greenLight;

	// Token: 0x040000A5 RID: 165
	public TextRenderer StatusText;

	// Token: 0x040000A6 RID: 166
	public AudioClip AcceptSound;

	// Token: 0x040000A7 RID: 167
	public AudioClip DenySound;

	// Token: 0x040000A8 RID: 168
	public AudioClip[] CardMove;

	// Token: 0x040000A9 RID: 169
	public AudioClip WalletOut;

	// Token: 0x040000AA RID: 170
	public float dragTime;

	// Token: 0x040000AB RID: 171
	private bool moving;

	// Token: 0x040000AC RID: 172
	private Vector2 prevStickInput = Vector2.zero;

	// Token: 0x040000AD RID: 173
	private float xPos = -2.38f;

	// Token: 0x040000AE RID: 174
	private bool hadPrev;

	// Token: 0x020002C4 RID: 708
	private enum TaskStages
	{
		// Token: 0x0400162B RID: 5675
		Before,
		// Token: 0x0400162C RID: 5676
		Animating,
		// Token: 0x0400162D RID: 5677
		Inserted,
		// Token: 0x0400162E RID: 5678
		After
	}
}
