using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class DoorCardSwipeGame : Minigame, IDoorMinigame
{
	// Token: 0x0600018F RID: 399 RVA: 0x0000C98A File Offset: 0x0000AB8A
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardPleaseInsert, Array.Empty<object>());
		base.SetupInput(true);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000C9BC File Offset: 0x0000ABBC
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
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
		//	DoorCardSwipeGame.TaskStages state = this.State;
		//	if (state != DoorCardSwipeGame.TaskStages.Before)
		//	{
		//		if (state == DoorCardSwipeGame.TaskStages.Inserted)
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
		//					float y = localPosition.y;
		//					localPosition.y -= num2;
		//					if (num2 > 0.01f)
		//					{
		//						this.dragTime += Time.deltaTime;
		//						if (!this.moving)
		//						{
		//							this.moving = true;
		//							if (Constants.ShouldPlaySfx())
		//							{
		//								SoundManager.Instance.PlaySound(this.CardMove.Random<AudioClip>(), false, 1f);
		//							}
		//						}
		//					}
		//					localPosition.y = this.YRange.Clamp(localPosition.y);
		//					float num3 = localPosition.y - y;
		//					float num4 = this.YRange.ReverseLerp(localPosition.y);
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
		//					if (localPosition.y - this.YRange.min < 0.05f && !BoolRange.Next(0.01f))
		//					{
		//						if (this.dragTime > this.minAcceptedTime)
		//						{
		//							if (Constants.ShouldPlaySfx())
		//							{
		//								SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
		//							}
		//							this.State = DoorCardSwipeGame.TaskStages.After;
		//							this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardAccepted, Array.Empty<object>());
		//							base.StartCoroutine(this.PutCardBack());
		//							ShipStatus.Instance.RpcRepairSystem(SystemTypes.Doors, this.MyDoor.Id | 64);
		//							this.MyDoor.SetDoorway(true);
		//							base.StartCoroutine(base.CoStartClose(0.4f));
		//							this.confirmSymbol.sprite = this.AcceptSymbol;
		//						}
		//						else
		//						{
		//							if (Constants.ShouldPlaySfx())
		//							{
		//								SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//							}
		//							this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardTooFast, Array.Empty<object>());
		//							this.confirmSymbol.sprite = this.RejectSymbol;
		//						}
		//					}
		//					else
		//					{
		//						if (Constants.ShouldPlaySfx())
		//						{
		//							SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//						}
		//						this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardBadRead, Array.Empty<object>());
		//						this.confirmSymbol.sprite = this.RejectSymbol;
		//					}
		//				}
		//				localPosition.y = Mathf.Lerp(localPosition.y, this.YRange.max, Time.deltaTime * 4f);
		//				this.hadPrev = false;
		//				this.dragTime = 0f;
		//			}
		//		}
		//	}
		//	else if (player.GetAnyButtonDown())
		//	{
		//		this.State = DoorCardSwipeGame.TaskStages.Animating;
		//		base.StartCoroutine(this.InsertCard());
		//	}
		//}
		//else
		//{
		//	switch (this.myController.CheckDrag(this.col))
		//	{
		//	case DragState.NoTouch:
		//		if (this.State == DoorCardSwipeGame.TaskStages.Inserted)
		//		{
		//			localPosition.y = Mathf.Lerp(localPosition.y, this.YRange.max, Time.deltaTime * 4f);
		//		}
		//		break;
		//	case DragState.TouchStart:
		//		this.dragTime = 0f;
		//		break;
		//	case DragState.Dragging:
		//		if (this.State == DoorCardSwipeGame.TaskStages.Inserted)
		//		{
		//			Vector2 vector2 = this.myController.DragPosition - base.transform.position;
		//			vector2.y = this.YRange.Clamp(vector2.y);
		//			if (localPosition.y - vector2.y > 0.01f)
		//			{
		//				this.dragTime += Time.deltaTime;
		//				this.confirmSymbol.sprite = null;
		//				if (!this.moving)
		//				{
		//					this.moving = true;
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.CardMove.Random<AudioClip>(), false, 1f);
		//					}
		//				}
		//			}
		//			localPosition.y = vector2.y;
		//		}
		//		break;
		//	case DragState.Released:
		//		this.moving = false;
		//		if (this.State == DoorCardSwipeGame.TaskStages.Before)
		//		{
		//			this.State = DoorCardSwipeGame.TaskStages.Animating;
		//			base.StartCoroutine(this.InsertCard());
		//		}
		//		else if (this.State == DoorCardSwipeGame.TaskStages.Inserted)
		//		{
		//			if (localPosition.y - this.YRange.min < 0.05f && !BoolRange.Next(0.01f))
		//			{
		//				if (this.dragTime > this.minAcceptedTime)
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.AcceptSound, false, 1f);
		//					}
		//					this.State = DoorCardSwipeGame.TaskStages.After;
		//					this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardAccepted, Array.Empty<object>());
		//					base.StartCoroutine(this.PutCardBack());
		//					ShipStatus.Instance.RpcRepairSystem(SystemTypes.Doors, this.MyDoor.Id | 64);
		//					this.MyDoor.SetDoorway(true);
		//					base.StartCoroutine(base.CoStartClose(0.4f));
		//					this.confirmSymbol.sprite = this.AcceptSymbol;
		//				}
		//				else
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//					}
		//					this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardTooFast, Array.Empty<object>());
		//					this.confirmSymbol.sprite = this.RejectSymbol;
		//				}
		//			}
		//			else
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.DenySound, false, 1f);
		//				}
		//				this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardBadRead, Array.Empty<object>());
		//				this.confirmSymbol.sprite = this.RejectSymbol;
		//			}
		//		}
		//		break;
		//	}
		//}
		//this.col.transform.localPosition = localPosition;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000D04D File Offset: 0x0000B24D
	private IEnumerator PutCardBack()
	{
		//if (Constants.ShouldPlaySfx())
		//{
		//	SoundManager.Instance.PlaySound(this.WalletOut, false, 1f);
		//}
		//Vector3 localPosition = this.col.transform.localPosition;
		//Vector3 dest;
		//dest..ctor(0.452f, -1.9f, 0f);
		//yield return Effects.All(new IEnumerator[]
		//{
		//	Effects.Rotate2D(this.col.transform, 90f, 0f, 0.4f),
		//	Effects.Slide3D(this.col.transform, localPosition, dest, 0.4f)
		//});
		//this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardPleaseInsert, Array.Empty<object>());
		//this.State = DoorCardSwipeGame.TaskStages.Before;
		yield break;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000D05C File Offset: 0x0000B25C
	private IEnumerator InsertCard()
	{
		//if (Constants.ShouldPlaySfx())
		//{
		//	SoundManager.Instance.PlaySound(this.WalletOut, false, 1f);
		//}
		//Vector3 localPosition = this.col.transform.localPosition;
		//Vector3 dest;
		//dest..ctor(-1.43f, this.YRange.max, 0f);
		//yield return Effects.All(new IEnumerator[]
		//{
		//	Effects.Rotate2D(this.col.transform, 0f, 90f, 0.4f),
		//	Effects.Slide3D(this.col.transform, localPosition, dest, 0.4f)
		//});
		//this.StatusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SwipeCardPleaseSwipe, Array.Empty<object>());
		//this.State = DoorCardSwipeGame.TaskStages.Inserted;
		yield break;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000D06B File Offset: 0x0000B26B
	public void SetDoor(PlainDoor door)
	{
		this.MyDoor = door;
	}

	// Token: 0x04000257 RID: 599
	private Color gray = new Color(0.45f, 0.45f, 0.45f);

	// Token: 0x04000258 RID: 600
	private Color green = new Color(0f, 0.8f, 0f);

	// Token: 0x04000259 RID: 601
	private DoorCardSwipeGame.TaskStages State;

	// Token: 0x0400025A RID: 602
	private Controller myController = new Controller();

	// Token: 0x0400025B RID: 603
	private FloatRange YRange = new FloatRange(-1.77f, 2f);

	// Token: 0x0400025C RID: 604
	public float minAcceptedTime = 0.3f;

	// Token: 0x0400025D RID: 605
	public Collider2D col;

	// Token: 0x0400025E RID: 606
	public SpriteRenderer confirmSymbol;

	// Token: 0x0400025F RID: 607
	public Sprite AcceptSymbol;

	// Token: 0x04000260 RID: 608
	public Sprite RejectSymbol;

	// Token: 0x04000261 RID: 609
	public TextRenderer StatusText;

	// Token: 0x04000262 RID: 610
	public AudioClip AcceptSound;

	// Token: 0x04000263 RID: 611
	public AudioClip DenySound;

	// Token: 0x04000264 RID: 612
	public AudioClip[] CardMove;

	// Token: 0x04000265 RID: 613
	public AudioClip WalletOut;

	// Token: 0x04000266 RID: 614
	public float dragTime;

	// Token: 0x04000267 RID: 615
	private bool moving;

	// Token: 0x04000268 RID: 616
	private Vector2 prevStickInput = Vector2.zero;

	// Token: 0x04000269 RID: 617
	private bool hadPrev;

	// Token: 0x0400026A RID: 618
	private PlainDoor MyDoor;

	// Token: 0x020002E9 RID: 745
	private enum TaskStages
	{
		// Token: 0x040016A8 RID: 5800
		Before,
		// Token: 0x040016A9 RID: 5801
		Animating,
		// Token: 0x040016AA RID: 5802
		Inserted,
		// Token: 0x040016AB RID: 5803
		After
	}
}
