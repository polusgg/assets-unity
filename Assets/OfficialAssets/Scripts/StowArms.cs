using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class StowArms : Minigame
{
	// Token: 0x06000177 RID: 375 RVA: 0x0000B630 File Offset: 0x00009830
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//if (base.ConsoleId == 1)
		//{
		//	this.GunContent.SetActive(true);
		//	this.RifleContent.SetActive(false);
		//}
		//else
		//{
		//	this.GunContent.SetActive(false);
		//	this.RifleContent.SetActive(true);
		//}
		//foreach (SpriteRenderer playerMaterialColors in this.handSprites)
		//{
		//	PlayerControl.LocalPlayer.SetPlayerMaterialColors(playerMaterialColors);
		//}
		//base.SetupInput(false);
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000178 RID: 376 RVA: 0x0000B6AA File Offset: 0x000098AA
	private AudioClip PickupSound
	{
        get
        {
            if (base.ConsoleId != 1)
            {
                return this.pickupRifle;
            }
            return this.pickupGun;
        }
    }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000179 RID: 377 RVA: 0x0000B6C2 File Offset: 0x000098C2
	private AudioClip PlaceSound
	{
        get
        {
            if (base.ConsoleId != 1)
            {
                return this.placeRifle;
            }
            return this.placeGun;
        }
    }

	// Token: 0x0600017A RID: 378 RVA: 0x0000B6DC File Offset: 0x000098DC
	public void Update()
	{
		//this.cont.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	if (!this.selectorObject.gameObject.activeSelf)
		//	{
		//		this.selectorObject.gameObject.SetActive(true);
		//	}
		//}
		//else if (this.selectorObject.gameObject.activeSelf)
		//{
		//	this.selectorObject.gameObject.SetActive(false);
		//}
		//if (base.ConsoleId == 1)
		//{
		//	this.DoUpdate(this.GunColliders, this.GunsSlots);
		//}
		//else
		//{
		//	this.DoUpdate(this.RifleColliders, this.RifleSlots);
		//}
		//if (this.currentGrabbedObject)
		//{
		//	this.selectorObject.position = this.currentGrabbedObject.transform.position;
		//	this.selectorObject.SetLocalZ(0f);
		//	StowArms.<Update>g__ValidateSelectorActive|21_0(this.selectorSubobjects[0], false);
		//	StowArms.<Update>g__ValidateSelectorActive|21_0(this.selectorSubobjects[1], base.ConsoleId == 1);
		//	StowArms.<Update>g__ValidateSelectorActive|21_0(this.selectorSubobjects[2], base.ConsoleId != 1);
		//	return;
		//}
		//this.selectorObject.position = VirtualCursor.currentPosition;
		//this.selectorObject.SetLocalZ(0f);
		//StowArms.<Update>g__ValidateSelectorActive|21_0(this.selectorSubobjects[0], true);
		//StowArms.<Update>g__ValidateSelectorActive|21_0(this.selectorSubobjects[1], false);
		//StowArms.<Update>g__ValidateSelectorActive|21_0(this.selectorSubobjects[2], false);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000B838 File Offset: 0x00009A38
	private void DoUpdate(Collider2D[] colliders, DragSlot[] slots)
	{
		//this.currentGrabbedObject = null;
		//for (int i = 0; i < colliders.Length; i++)
		//{
		//	Collider2D collider2D = colliders[i];
		//	DragSlot dragSlot = slots[i];
		//	if (!(dragSlot.Occupant == collider2D))
		//	{
		//		switch (this.cont.CheckDrag(collider2D))
		//		{
		//		case DragState.TouchStart:
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.PickupSound, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		//			}
		//			this.currentGrabbedObject = collider2D;
		//			this.grabOffset = collider2D.transform.position - this.cont.DragStartPosition;
		//			break;
		//		case DragState.Holding:
		//		case DragState.Dragging:
		//		{
		//			this.currentGrabbedObject = collider2D;
		//			Vector3 position = base.transform.position;
		//			Vector3 position2 = this.cont.DragPosition + this.grabOffset;
		//			position2.z = position.z;
		//			collider2D.transform.position = position2;
		//			if (Vector2.Distance(collider2D.transform.position, dragSlot.TargetPosition) < 0.25f && !dragSlot.Occupant)
		//			{
		//				collider2D.transform.position = dragSlot.TargetPosition;
		//			}
		//			break;
		//		}
		//		case DragState.Released:
		//			if (Vector2.Distance(collider2D.transform.position, dragSlot.TargetPosition) < 0.25f && !dragSlot.Occupant)
		//			{
		//				collider2D.transform.position = dragSlot.TargetPosition;
		//				dragSlot.Occupant = collider2D;
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.PlaceSound, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		//				}
		//			}
		//			this.CheckForWin(colliders, slots);
		//			break;
		//		}
		//	}
		//}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000BA2C File Offset: 0x00009C2C
	private void CheckForWin(Collider2D[] colliders, DragSlot[] slots)
	{
		bool flag = true;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (Vector2.Distance(colliders[i].transform.position, slots[i].TargetPosition) > 0.25f)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000BAAB File Offset: 0x00009CAB
	//[CompilerGenerated]
	//internal static void <Update>g__ValidateSelectorActive|21_0(GameObject selector, bool shouldBeActive)
	//{
	//	if (selector.gameObject.activeSelf != shouldBeActive)
	//	{
	//		selector.gameObject.SetActive(shouldBeActive);
	//	}
	//}

	// Token: 0x04000209 RID: 521
	public GameObject GunContent;

	// Token: 0x0400020A RID: 522
	public GameObject RifleContent;

	// Token: 0x0400020B RID: 523
	public Transform selectorObject;

	// Token: 0x0400020C RID: 524
	public GameObject[] selectorSubobjects;

	// Token: 0x0400020D RID: 525
	public SpriteRenderer[] handSprites;

	// Token: 0x0400020E RID: 526
	public AudioClip pickupGun;

	// Token: 0x0400020F RID: 527
	public AudioClip placeGun;

	// Token: 0x04000210 RID: 528
	public Collider2D[] GunColliders;

	// Token: 0x04000211 RID: 529
	public DragSlot[] GunsSlots;

	// Token: 0x04000212 RID: 530
	public AudioClip pickupRifle;

	// Token: 0x04000213 RID: 531
	public AudioClip placeRifle;

	// Token: 0x04000214 RID: 532
	public Collider2D[] RifleColliders;

	// Token: 0x04000215 RID: 533
	public DragSlot[] RifleSlots;

	// Token: 0x04000216 RID: 534
	private Controller cont = new Controller();

	// Token: 0x04000217 RID: 535
	private Collider2D currentGrabbedObject;

	// Token: 0x04000218 RID: 536
	private Vector3 grabOffset;
}
