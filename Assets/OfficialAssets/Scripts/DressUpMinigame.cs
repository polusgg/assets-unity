using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class DressUpMinigame : Minigame
{
	// Token: 0x06000123 RID: 291 RVA: 0x00007B14 File Offset: 0x00005D14
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		int num = this.Hats.RandomIdx<Sprite>();
		this.DummyHat.transform.SetLocalZ((float)((num == 1) ? 1 : -1));
		this.DummyHat.sprite = this.Hats[num];
		this.DummyAccessory.sprite = this.Accessories.Random<Sprite>();
		this.DummyClothes.sprite = this.Clothes.Random<Sprite>();
		this.draggable.enabled = false;
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.grabbyHand);
		base.SetupInput(false);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00007BB0 File Offset: 0x00005DB0
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//Vector3 position = this.cursorObject.position;
		//position.x = VirtualCursor.currentPosition.x;
		//position.y = VirtualCursor.currentPosition.y;
		//this.cursorObject.position = position;
		//if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		//{
		//	if (this.grabbyHand.enabled == this.draggable.enabled)
		//	{
		//		this.grabbyHand.enabled = !this.draggable.enabled;
		//	}
		//}
		//else if (this.grabbyHand.enabled)
		//{
		//	this.grabbyHand.enabled = false;
		//}
		//this.controller.Update();
		//foreach (DressUpCosmetic dressUpCosmetic in this.buttons)
		//{
		//	Vector2 dragPosition = this.controller.DragPosition;
		//	switch (this.controller.CheckDrag(dressUpCosmetic.Hitbox))
		//	{
		//	case DragState.TouchStart:
		//		this.draggable.enabled = true;
		//		this.draggable.sprite = dressUpCosmetic.Rend.sprite;
		//		this.draggable.transform.position = this.controller.DragPosition - this.draggable.sprite.bounds.center;
		//		switch (dressUpCosmetic.Slot)
		//		{
		//		case CosmeticType.Hat:
		//		{
		//			int num = this.Hats.IndexOf(dressUpCosmetic.Rend.sprite);
		//			this.ActualHat.transform.SetLocalZ((float)((num == 1) ? 1 : -1));
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.hatSound, false, 1f);
		//			}
		//			break;
		//		}
		//		case CosmeticType.Accessory:
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.faceSound, false, 1f);
		//			}
		//			break;
		//		case CosmeticType.Skin:
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.clothesSound, false, 1f);
		//			}
		//			break;
		//		}
		//		break;
		//	case DragState.Dragging:
		//	{
		//		Vector3 vector = this.controller.DragPosition;
		//		vector -= this.draggable.sprite.bounds.center;
		//		vector.z = -5f + base.transform.position.z;
		//		switch (dressUpCosmetic.Slot)
		//		{
		//		case CosmeticType.Hat:
		//			if (this.hatHitbox.OverlapPoint(this.controller.DragPosition))
		//			{
		//				vector = this.ActualHat.transform.position;
		//			}
		//			break;
		//		case CosmeticType.Accessory:
		//			if (this.faceHitbox.OverlapPoint(this.controller.DragPosition))
		//			{
		//				vector = this.ActualAccessory.transform.position;
		//			}
		//			break;
		//		case CosmeticType.Skin:
		//			if (this.bodyHitbox.OverlapPoint(this.controller.DragPosition))
		//			{
		//				vector = this.ActualClothes.transform.position;
		//			}
		//			break;
		//		}
		//		this.draggable.transform.position = vector;
		//		break;
		//	}
		//	case DragState.Released:
		//		this.draggable.enabled = false;
		//		switch (dressUpCosmetic.Slot)
		//		{
		//		case CosmeticType.Hat:
		//			if (this.hatHitbox.OverlapPoint(dragPosition))
		//			{
		//				this.SetHat(this.Hats.IndexOf(dressUpCosmetic.Rend.sprite));
		//			}
		//			break;
		//		case CosmeticType.Accessory:
		//			if (this.faceHitbox.OverlapPoint(dragPosition))
		//			{
		//				this.SetAccessory(this.Accessories.IndexOf(dressUpCosmetic.Rend.sprite));
		//			}
		//			break;
		//		case CosmeticType.Skin:
		//			if (this.bodyHitbox.OverlapPoint(dragPosition))
		//			{
		//				this.SetClothes(this.Clothes.IndexOf(dressUpCosmetic.Rend.sprite));
		//			}
		//			break;
		//		}
		//		break;
		//	}
		//}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00007F9C File Offset: 0x0000619C
	public void SetHat(int i)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		this.ActualHat.transform.SetLocalZ((float)((i == 1) ? 1 : -1));
		this.ActualHat.sprite = this.Hats[i];
		if (this.DummyHat.sprite == this.ActualHat.sprite)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.correctSound, false, 1f);
			}
		}
		else if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.incorrectSound, false, 1f);
		}
		this.CheckOutfit();
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00008040 File Offset: 0x00006240
	public void SetAccessory(int i)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		this.ActualAccessory.sprite = this.Accessories[i];
		if (this.DummyAccessory.sprite == this.ActualAccessory.sprite)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.correctSound, false, 1f);
			}
		}
		else if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.incorrectSound, false, 1f);
		}
		this.CheckOutfit();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x000080CC File Offset: 0x000062CC
	public void SetClothes(int i)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		this.ActualClothes.sprite = this.Clothes[i];
		if (this.DummyClothes.sprite == this.ActualClothes.sprite)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.correctSound, false, 1f);
			}
		}
		else if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.incorrectSound, false, 1f);
		}
		this.CheckOutfit();
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00008158 File Offset: 0x00006358
	private void CheckOutfit()
	{
		if (this.DummyHat.sprite == this.ActualHat.sprite && this.DummyClothes.sprite == this.ActualClothes.sprite && this.DummyAccessory.sprite == this.ActualAccessory.sprite)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.finishedSound, false, 1f);
			}
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.7f));
		}
	}

	// Token: 0x04000150 RID: 336
	public SpriteRenderer DummyHat;

	// Token: 0x04000151 RID: 337
	public SpriteRenderer DummyAccessory;

	// Token: 0x04000152 RID: 338
	public SpriteRenderer DummyClothes;

	// Token: 0x04000153 RID: 339
	public SpriteRenderer ActualHat;

	// Token: 0x04000154 RID: 340
	public SpriteRenderer ActualAccessory;

	// Token: 0x04000155 RID: 341
	public SpriteRenderer ActualClothes;

	// Token: 0x04000156 RID: 342
	public DressUpCosmetic[] buttons;

	// Token: 0x04000157 RID: 343
	public Sprite[] Hats;

	// Token: 0x04000158 RID: 344
	public Sprite[] Accessories;

	// Token: 0x04000159 RID: 345
	public Sprite[] Clothes;

	// Token: 0x0400015A RID: 346
	public Collider2D hatHitbox;

	// Token: 0x0400015B RID: 347
	public Collider2D faceHitbox;

	// Token: 0x0400015C RID: 348
	public Collider2D bodyHitbox;

	// Token: 0x0400015D RID: 349
	public SpriteRenderer draggable;

	// Token: 0x0400015E RID: 350
	public AudioClip hatSound;

	// Token: 0x0400015F RID: 351
	public AudioClip faceSound;

	// Token: 0x04000160 RID: 352
	public AudioClip clothesSound;

	// Token: 0x04000161 RID: 353
	public AudioClip correctSound;

	// Token: 0x04000162 RID: 354
	public AudioClip incorrectSound;

	// Token: 0x04000163 RID: 355
	public AudioClip finishedSound;

	// Token: 0x04000164 RID: 356
	private Controller controller = new Controller();

	// Token: 0x04000165 RID: 357
	public SpriteRenderer grabbyHand;

	// Token: 0x04000166 RID: 358
	public Transform cursorObject;
}
