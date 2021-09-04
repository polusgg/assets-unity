using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class TowelMinigame : Minigame
{
	// Token: 0x06000184 RID: 388 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.overlapResults = new Collider2D[this.Towels.Length * 3];
		base.SetupInput(true);
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.openHand);
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.holdingHand);
		this.openHand.enabled = true;
		this.holdingHand.enabled = false;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000BF30 File Offset: 0x0000A130
	private void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.controller.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	if (!this.interactionCursor.gameObject.activeSelf)
		//	{
		//		this.interactionCursor.gameObject.SetActive(true);
		//	}
		//	if (this.isHolding)
		//	{
		//		this.openHand.enabled = false;
		//		this.holdingHand.enabled = true;
		//	}
		//	else
		//	{
		//		this.openHand.enabled = true;
		//		this.holdingHand.enabled = false;
		//	}
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 vector;
		//	vector..ctor(player.GetAxis(13), player.GetAxis(14));
		//	Vector3 localPosition = this.interactionCursor.localPosition;
		//	if (!this.isHolding)
		//	{
		//		localPosition.x += vector.x * Time.deltaTime * this.stickMoveSpeed;
		//		localPosition.y += vector.y * Time.deltaTime * this.stickMoveSpeed;
		//	}
		//	else
		//	{
		//		localPosition.x += vector.x * Time.deltaTime * this.stickHeldSpeed;
		//		localPosition.y += vector.y * Time.deltaTime * this.stickHeldSpeed;
		//	}
		//	int num = Physics2D.OverlapCircleNonAlloc(this.interactionCursor.position, this.interactionCursor.transform.localScale.x, this.overlapResults, LayerMask.GetMask(new string[]
		//	{
		//		"UICollide"
		//	}));
		//	localPosition.x = Mathf.Clamp(localPosition.x, this.ValidArea.min.x, this.ValidArea.max.x);
		//	localPosition.y = Mathf.Clamp(localPosition.y, this.ValidArea.min.y, this.ValidArea.max.y);
		//	this.interactionCursor.localPosition = localPosition;
		//	if (player.GetButton(11))
		//	{
		//		if (!this.isHolding)
		//		{
		//			for (int i = 0; i < num; i++)
		//			{
		//				Collider2D collider2D = this.overlapResults[i];
		//				if (collider2D.GetComponent<Rigidbody2D>())
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.TowelGrab, false, 1f);
		//					}
		//					this.heldTowel = collider2D.gameObject;
		//					this.isHolding = true;
		//					break;
		//				}
		//			}
		//		}
		//		else
		//		{
		//			if (this.heldTowel.activeSelf)
		//			{
		//				this.heldTowel.GetComponent<Rigidbody2D>().velocity = (localPosition - this.heldTowel.transform.localPosition) * this.towelDragSpeed;
		//			}
		//			if (this.heldTowel.GetComponent<Collider2D>().IsTouching(this.BasketHitbox))
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySoundImmediate(this.TowelLand, false, 1f, 1f);
		//				}
		//				this.towelsDone++;
		//				this.heldTowel.gameObject.SetActive(false);
		//			}
		//		}
		//	}
		//	if (player.GetButtonUp(11))
		//	{
		//		this.isHolding = false;
		//		if (this.heldTowel)
		//		{
		//			this.heldTowel.GetComponent<Rigidbody2D>().velocity = (localPosition - this.heldTowel.transform.localPosition) * this.towelDragSpeed;
		//			this.heldTowel = null;
		//		}
		//	}
		//}
		//else if (this.interactionCursor.gameObject.activeSelf)
		//{
		//	this.interactionCursor.gameObject.SetActive(false);
		//}
		//foreach (Collider2D collider2D2 in this.Towels)
		//{
		//	if (collider2D2.isActiveAndEnabled)
		//	{
		//		if (!collider2D2.IsTouching(this.BasketHitbox))
		//		{
		//			switch (this.controller.CheckDrag(collider2D2))
		//			{
		//			case DragState.TouchStart:
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.TowelGrab, false, 1f);
		//				}
		//				break;
		//			case DragState.Dragging:
		//			{
		//				Rigidbody2D component = collider2D2.GetComponent<Rigidbody2D>();
		//				component.velocity = (this.controller.DragPosition - component.position) * 4f;
		//				break;
		//			}
		//			}
		//		}
		//		else
		//		{
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySoundImmediate(this.TowelLand, false, 1f, 1f);
		//			}
		//			this.towelsDone++;
		//			collider2D2.gameObject.SetActive(false);
		//		}
		//	}
		//}
		//this.BasketGauge.transform.localPosition = new Vector3(0f, this.towelRange.Lerp((float)this.towelsDone / (float)this.Towels.Length), 0.5f);
		//if (this.towelsDone >= this.Towels.Length)
		//{
		//	this.MyNormTask.NextStep();
		//	base.StartCoroutine(base.CoStartClose(0.75f));
		//}
	}

	// Token: 0x0400022A RID: 554
	private FloatRange towelRange = new FloatRange(-2.5f, -0.15f);

	// Token: 0x0400022B RID: 555
	public SpriteRenderer BasketGauge;

	// Token: 0x0400022C RID: 556
	public Collider2D BasketHitbox;

	// Token: 0x0400022D RID: 557
	public Collider2D[] Towels;

	// Token: 0x0400022E RID: 558
	private Controller controller = new Controller();

	// Token: 0x0400022F RID: 559
	private int towelsDone;

	// Token: 0x04000230 RID: 560
	public AudioClip TowelGrab;

	// Token: 0x04000231 RID: 561
	public AudioClip TowelLand;

	// Token: 0x04000232 RID: 562
	public Transform interactionCursor;

	// Token: 0x04000233 RID: 563
	public SpriteRenderer openHand;

	// Token: 0x04000234 RID: 564
	public SpriteRenderer holdingHand;

	// Token: 0x04000235 RID: 565
	public Vector2Range ValidArea;

	// Token: 0x04000236 RID: 566
	public float stickMoveSpeed = 4f;

	// Token: 0x04000237 RID: 567
	public float stickHeldSpeed = 3f;

	// Token: 0x04000238 RID: 568
	public float towelDragSpeed = 4f;

	// Token: 0x04000239 RID: 569
	private Collider2D[] overlapResults;

	// Token: 0x0400023A RID: 570
	private bool isHolding;

	// Token: 0x0400023B RID: 571
	private GameObject heldTowel;
}
