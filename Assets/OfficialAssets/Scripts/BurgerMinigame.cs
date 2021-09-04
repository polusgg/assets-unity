using System;
using System.Collections.Generic;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class BurgerMinigame : Minigame
{
	// Token: 0x0600011C RID: 284 RVA: 0x000072D4 File Offset: 0x000054D4
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.burger.Push(this.Plate);
		this.Shadow.enabled = false;
		this.ExpectedToppings[0] = BurgerToppingTypes.Plate;
		if (BoolRange.Next(0.1f))
		{
			this.ExpectedToppings[1] = BurgerToppingTypes.Lettuce;
			this.ExpectedToppings[5] = BurgerToppingTypes.Lettuce;
		}
		else
		{
			this.ExpectedToppings[1] = BurgerToppingTypes.BottomBun;
			this.ExpectedToppings[5] = BurgerToppingTypes.TopBun;
		}
		for (int i = 2; i < 5; i++)
		{
			BurgerToppingTypes top = (BurgerToppingTypes)IntRange.Next(2, 6);
			if (this.ExpectedToppings.Count((BurgerToppingTypes t) => t == top) >= 2)
			{
				i--;
			}
			else
			{
				this.ExpectedToppings[i] = top;
			}
		}
		if (BoolRange.Next(0.01f))
		{
			BurgerToppingTypes burgerToppingTypes = this.ExpectedToppings[5];
			this.ExpectedToppings[5] = this.ExpectedToppings[4];
			this.ExpectedToppings[4] = burgerToppingTypes;
		}
		for (int j = 1; j < this.ExpectedToppings.Length; j++)
		{
			this.PaperSlots[j - 1].sprite = this.PaperToppings[(int)this.ExpectedToppings[j]];
		}
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.handSprite);
		this.internalCursorPos = this.stickCursor.localPosition;
		this.selectionCenterStart = this.stickSelectionCenterPoint.transform.localPosition;
		base.SetupInput(true);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000742C File Offset: 0x0000562C
	private void SelectTopping(BurgerTopping topping)
	{
		if (this.stickSelectedTopping != null)
		{
			this.stickSelectedTopping.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 0f);
		}
		this.stickSelectedTopping = topping;
		if (this.stickSelectedTopping != null)
		{
			this.stickSelectedTopping.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 10f);
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000749C File Offset: 0x0000569C
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.inputHandler.disableVirtualCursor = false;
		//this.controller.Update();
		//if (this.PaperClosed)
		//{
		//	if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//	{
		//		if (this.grabbedTopping)
		//		{
		//			if (this.stickCursor.gameObject.activeSelf)
		//			{
		//				this.stickCursor.gameObject.SetActive(false);
		//			}
		//		}
		//		else if (!this.stickCursor.gameObject.activeSelf)
		//		{
		//			this.stickCursor.gameObject.SetActive(true);
		//		}
		//		if (this.burger.Count > 0)
		//		{
		//			this.stickSelectionCenterPoint.transform.localPosition = this.burger.Peek().transform.localPosition;
		//		}
		//		else
		//		{
		//			this.stickSelectionCenterPoint.transform.localPosition = this.selectionCenterStart;
		//		}
		//		Vector2 axis2DRaw = ReInput.players.GetPlayer(0).GetAxis2DRaw(13, 14);
		//		bool flag = false;
		//		if (!this.grabbedTopping)
		//		{
		//			foreach (BurgerTopping burgerTopping in this.Toppings)
		//			{
		//				if (this.controller.CheckHover(burgerTopping.Hitbox))
		//				{
		//					this.SelectTopping(burgerTopping);
		//					flag = true;
		//					break;
		//				}
		//			}
		//		}
		//		if (!flag)
		//		{
		//			this.SelectTopping(null);
		//		}
		//		Vector3 vector = this.stickSelectionCenterPoint.transform.localPosition + axis2DRaw.x * Vector3.right * this.stickSelectionCenterPoint.size.x * 0.5f + axis2DRaw.y * Vector3.up * this.stickSelectionCenterPoint.size.y * 0.5f;
		//		this.internalCursorPos = Vector3.Lerp(this.internalCursorPos, vector, Time.deltaTime * ((flag && !this.grabbedTopping) ? this.cursorHoverLerpSpeed : this.cursorLerpSpeed));
		//		this.stickCursor.localPosition = this.internalCursorPos;
		//		this.stickCursor.position;
		//		VirtualCursor.instance.SetWorldPosition(this.stickCursor.position);
		//	}
		//	else if (this.stickCursor.gameObject.activeSelf)
		//	{
		//		this.stickCursor.gameObject.SetActive(false);
		//	}
		//	this.grabbedTopping = false;
		//	foreach (BurgerTopping burgerTopping2 in this.Toppings)
		//	{
		//		switch (this.controller.CheckDrag(burgerTopping2.Hitbox))
		//		{
		//		case DragState.TouchStart:
		//			if (burgerTopping2 == this.burger.Peek())
		//			{
		//				this.burger.Pop();
		//			}
		//			if (this.burger.Contains(burgerTopping2))
		//			{
		//				this.controller.ClearTouch();
		//			}
		//			else
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(burgerTopping2.GrabSound, false, 1f);
		//				}
		//				VibrationManager.Vibrate(0.15f, 0.15f, 0.1f, VibrationManager.VibrationFalloff.Linear, null, false);
		//				this.stackHeight = this.burger.Sum((BurgerTopping s) => s.Offset) + burgerTopping2.Offset;
		//				this.Shadow.enabled = true;
		//				Vector3 vector2 = this.controller.DragPosition - base.transform.position;
		//				vector2.z = -1f;
		//				burgerTopping2.transform.localPosition = vector2;
		//				this.Shadow.transform.localPosition = vector2 + new Vector3(0f, -this.stackHeight, 0.8f);
		//			}
		//			break;
		//		case DragState.Holding:
		//			this.grabbedTopping = true;
		//			break;
		//		case DragState.Dragging:
		//		{
		//			this.grabbedTopping = true;
		//			Vector3 vector3 = this.controller.DragPosition - base.transform.position;
		//			vector3.z = -1f;
		//			burgerTopping2.transform.localPosition = vector3;
		//			this.Shadow.transform.localPosition = vector3 + new Vector3(0f, -this.stackHeight, 0.8f);
		//			break;
		//		}
		//		case DragState.Released:
		//		{
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(burgerTopping2.DropSound, false, 1f);
		//			}
		//			VibrationManager.Vibrate(0.35f, 0.35f, 0.1f, VibrationManager.VibrationFalloff.Linear, null, false);
		//			this.Shadow.enabled = false;
		//			BurgerTopping burgerTopping3 = this.burger.Peek();
		//			if (burgerTopping3.Hitbox.IsTouching(burgerTopping2.Hitbox))
		//			{
		//				burgerTopping2.transform.position = burgerTopping3.transform.position + new Vector3(0f, burgerTopping2.Offset, -0.001f);
		//				this.burger.Push(burgerTopping2);
		//				if (this.burger.Count == this.ExpectedToppings.Length)
		//				{
		//					bool flag2 = true;
		//					BurgerToppingTypes[] array = (from tt in this.burger
		//					select tt.ToppingType).Reverse<BurgerToppingTypes>().ToArray<BurgerToppingTypes>();
		//					for (int j = 0; j < array.Length; j++)
		//					{
		//						if (array[j] != this.ExpectedToppings[j])
		//						{
		//							flag2 = false;
		//							break;
		//						}
		//					}
		//					if (flag2)
		//					{
		//						this.MyNormTask.NextStep();
		//						base.StartCoroutine(base.CoStartClose(0.5f));
		//					}
		//				}
		//			}
		//			break;
		//		}
		//		}
		//	}
		//}
		//else if (this.stickCursor.gameObject.activeSelf)
		//{
		//	this.stickCursor.gameObject.SetActive(false);
		//}
		//Vector3 localPosition = this.Paper.localPosition;
		//localPosition.y = Mathf.Lerp(localPosition.y, this.PaperClosed ? 4.8f : 0f, Time.deltaTime * 10f);
		//this.Paper.localPosition = localPosition;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00007AA7 File Offset: 0x00005CA7
	public void TogglePaper()
	{
		this.PaperClosed = !this.PaperClosed;
	}

	// Token: 0x0400012E RID: 302
	public BurgerTopping Plate;

	// Token: 0x0400012F RID: 303
	public BurgerTopping[] Toppings;

	// Token: 0x04000130 RID: 304
	public Sprite[] PaperToppings;

	// Token: 0x04000131 RID: 305
	public SpriteRenderer[] PaperSlots;

	// Token: 0x04000132 RID: 306
	private BurgerToppingTypes[] ExpectedToppings = new BurgerToppingTypes[6];

	// Token: 0x04000133 RID: 307
	private Stack<BurgerTopping> burger = new Stack<BurgerTopping>();

	// Token: 0x04000134 RID: 308
	private Controller controller = new Controller();

	// Token: 0x04000135 RID: 309
	public Transform Paper;

	// Token: 0x04000136 RID: 310
	private const float PaperOpenedY = 0f;

	// Token: 0x04000137 RID: 311
	private const float PaperClosedY = 4.8f;

	// Token: 0x04000138 RID: 312
	private bool PaperClosed;

	// Token: 0x04000139 RID: 313
	public SpriteRenderer Shadow;

	// Token: 0x0400013A RID: 314
	public Transform stickCursor;

	// Token: 0x0400013B RID: 315
	public SpriteRenderer handSprite;

	// Token: 0x0400013C RID: 316
	public BoxCollider2D stickSelectionCenterPoint;

	// Token: 0x0400013D RID: 317
	private Vector3 selectionCenterStart;

	// Token: 0x0400013E RID: 318
	private BurgerTopping stickSelectedTopping;

	// Token: 0x0400013F RID: 319
	private bool grabbedTopping;

	// Token: 0x04000140 RID: 320
	private Vector3 internalCursorPos;

	// Token: 0x04000141 RID: 321
	public float cursorLerpSpeed = 5f;

	// Token: 0x04000142 RID: 322
	public float cursorHoverLerpSpeed = 1f;

	// Token: 0x04000143 RID: 323
	private float stackHeight;
}
