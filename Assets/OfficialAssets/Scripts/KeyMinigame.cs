using System;
//using Rewired;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class KeyMinigame : Minigame
{
	// Token: 0x060007DE RID: 2014 RVA: 0x0003228B File Offset: 0x0003048B
	public void Start()
	{
		this.targetSlotId = (int)(PlayerControl.LocalPlayer ? PlayerControl.LocalPlayer.PlayerId : 0);
		this.Slots[this.targetSlotId].SetHighlight();
		base.SetupInput(true);
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x000322C8 File Offset: 0x000304C8
	public void Update()
	{
		//if (this.keyState == 2)
		//{
		//	return;
		//}
		//KeySlotBehaviour keySlotBehaviour = this.Slots[this.targetSlotId];
		//this.controller.Update();
		//int num = this.keyState;
		//if (num != 0)
		//{
		//	if (num == 1)
		//	{
		//		if (this.moveKeyGlyph.activeSelf)
		//		{
		//			this.moveKeyGlyph.SetActive(false);
		//		}
		//		if (!this.turnKeyGlyph.activeSelf)
		//		{
		//			this.turnKeyGlyph.SetActive(true);
		//		}
		//	}
		//}
		//else
		//{
		//	if (!this.moveKeyGlyph.activeSelf)
		//	{
		//		this.moveKeyGlyph.SetActive(true);
		//	}
		//	if (this.turnKeyGlyph.activeSelf)
		//	{
		//		this.turnKeyGlyph.SetActive(false);
		//	}
		//}
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 vector = Vector2.zero;
		//	vector = player.GetAxis2DRaw(13, 14);
		//	if ((double)vector.sqrMagnitude > 0.5)
		//	{
		//		if (!this.prevHadInput)
		//		{
		//			if (this.keyState == 0)
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.KeyGrab, false, 1f);
		//				}
		//			}
		//			else if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.KeyTurn, false, 1f);
		//			}
		//		}
		//		if (this.keyState == 0)
		//		{
		//			Vector3 vector2 = vector * Time.deltaTime * 5f;
		//			this.key.transform.localPosition += vector2;
		//			if (this.key.IsTouching(keySlotBehaviour.Hitbox))
		//			{
		//				this.KeyImage.sprite = this.insertImage;
		//			}
		//			else
		//			{
		//				this.KeyImage.sprite = this.normalImage;
		//			}
		//		}
		//		else
		//		{
		//			vector = vector.normalized;
		//			if (this.prevHadInput)
		//			{
		//				this.currentAngle += Vector2.SignedAngle(this.prevInputDir, vector);
		//				this.currentAngle = Mathf.Clamp(this.currentAngle, 0f, 90f);
		//				this.key.transform.localEulerAngles = new Vector3(0f, 0f, this.currentAngle);
		//			}
		//			else
		//			{
		//				this.currentAngle = this.key.transform.localEulerAngles.z;
		//			}
		//			this.prevInputDir = vector;
		//		}
		//		this.prevHadInput = true;
		//		return;
		//	}
		//	if (this.prevHadInput)
		//	{
		//		if (this.keyState == 0)
		//		{
		//			if (this.key.IsTouching(keySlotBehaviour.Hitbox))
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.KeyInsert, false, 1f);
		//				}
		//				this.keyState = 1;
		//				this.key.size = new Vector2(2f, 2f);
		//				Vector3 position = keySlotBehaviour.transform.position;
		//				position.z -= 1f;
		//				this.key.transform.position = position;
		//				this.KeyImage.sprite = this.insertImage;
		//				keySlotBehaviour.SetInserted();
		//			}
		//		}
		//		else
		//		{
		//			float num2 = this.key.transform.localEulerAngles.z;
		//			if (num2 > 180f)
		//			{
		//				num2 -= 360f;
		//			}
		//			num2 %= 360f;
		//			if (Mathf.Abs(num2) > 80f)
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.KeyOpen, false, 1f);
		//				}
		//				keySlotBehaviour.SetFinished();
		//				this.key.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
		//				this.keyState = 2;
		//				this.MyNormTask.NextStep();
		//				base.StartCoroutine(base.CoStartClose(0.75f));
		//			}
		//			else
		//			{
		//				this.key.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		//			}
		//		}
		//		this.prevHadInput = false;
		//		return;
		//	}
		//}
		//else
		//{
		//	switch (this.controller.CheckDrag(this.key))
		//	{
		//	case DragState.TouchStart:
		//		if (this.keyState == 0)
		//		{
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.KeyGrab, false, 1f);
		//				return;
		//			}
		//		}
		//		else if (Constants.ShouldPlaySfx())
		//		{
		//			SoundManager.Instance.PlaySound(this.KeyTurn, false, 1f);
		//			return;
		//		}
		//		break;
		//	case DragState.Holding:
		//		break;
		//	case DragState.Dragging:
		//	{
		//		if (this.keyState != 0)
		//		{
		//			Vector2 vector3 = this.key.transform.position;
		//			float num3 = Vector2.SignedAngle(this.controller.DragStartPosition - vector3, this.controller.DragPosition - vector3);
		//			this.key.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Clamp(num3, -90f, 90f));
		//			return;
		//		}
		//		Vector2 vector4 = this.controller.DragPosition - base.transform.position;
		//		this.key.transform.localPosition = vector4;
		//		if (this.key.IsTouching(keySlotBehaviour.Hitbox))
		//		{
		//			this.KeyImage.sprite = this.insertImage;
		//			return;
		//		}
		//		this.KeyImage.sprite = this.normalImage;
		//		return;
		//	}
		//	case DragState.Released:
		//		if (this.keyState == 0)
		//		{
		//			if (this.key.IsTouching(keySlotBehaviour.Hitbox))
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.KeyInsert, false, 1f);
		//				}
		//				this.keyState = 1;
		//				this.key.size = new Vector2(2f, 2f);
		//				Vector3 position2 = keySlotBehaviour.transform.position;
		//				position2.z -= 1f;
		//				this.key.transform.position = position2;
		//				this.KeyImage.sprite = this.insertImage;
		//				keySlotBehaviour.SetInserted();
		//				return;
		//			}
		//		}
		//		else
		//		{
		//			float num4 = this.key.transform.localEulerAngles.z;
		//			if (num4 > 180f)
		//			{
		//				num4 -= 360f;
		//			}
		//			num4 %= 360f;
		//			if (Mathf.Abs(num4) > 80f)
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.KeyOpen, false, 1f);
		//				}
		//				keySlotBehaviour.SetFinished();
		//				this.key.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
		//				this.keyState = 2;
		//				this.MyNormTask.NextStep();
		//				base.StartCoroutine(base.CoStartClose(0.75f));
		//				return;
		//			}
		//			this.key.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		//		}
		//		break;
		//	default:
		//		return;
		//	}
		//}
	}

	// Token: 0x0400090A RID: 2314
	public KeySlotBehaviour[] Slots;

	// Token: 0x0400090B RID: 2315
	private int keyState;

	// Token: 0x0400090C RID: 2316
	public SpriteRenderer KeyImage;

	// Token: 0x0400090D RID: 2317
	public Sprite normalImage;

	// Token: 0x0400090E RID: 2318
	public Sprite insertImage;

	// Token: 0x0400090F RID: 2319
	public BoxCollider2D key;

	// Token: 0x04000910 RID: 2320
	private int targetSlotId;

	// Token: 0x04000911 RID: 2321
	private Controller controller = new Controller();

	// Token: 0x04000912 RID: 2322
	public AudioClip KeyGrab;

	// Token: 0x04000913 RID: 2323
	public AudioClip KeyInsert;

	// Token: 0x04000914 RID: 2324
	public AudioClip KeyOpen;

	// Token: 0x04000915 RID: 2325
	public AudioClip KeyTurn;

	// Token: 0x04000916 RID: 2326
	private bool prevHadInput;

	// Token: 0x04000917 RID: 2327
	private Vector2 prevInputDir;

	// Token: 0x04000918 RID: 2328
	private float currentAngle;

	// Token: 0x04000919 RID: 2329
	public GameObject moveKeyGlyph;

	// Token: 0x0400091A RID: 2330
	public GameObject turnKeyGlyph;
}
