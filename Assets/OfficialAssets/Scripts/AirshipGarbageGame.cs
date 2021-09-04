using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class AirshipGarbageGame : Minigame
{
	// Token: 0x0600010E RID: 270 RVA: 0x00006D00 File Offset: 0x00004F00
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//this.can = Object.Instantiate<GarbageCanBehaviour>(this.GarbagePrefabs[base.ConsoleId], base.transform);
		//base.SetupInput(true);
		//foreach (SpriteRenderer playerMaterialColors in this.handSprites)
		//{
		//	PlayerControl.LocalPlayer.SetPlayerMaterialColors(playerMaterialColors);
		//}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00006D60 File Offset: 0x00004F60
	private void Update()
	{
		//if (this.amOpening)
		//{
		//	return;
		//}
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.controller.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	this.handCursorObject.position = this.can.Handle.transform.position;
		//	if (player.GetButton(24) && player.GetButton(21))
		//	{
		//		if (!this.grabbedHands.activeSelf)
		//		{
		//			this.grabbedHands.SetActive(true);
		//			this.waitingHands.SetActive(false);
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.grabSound, false, 1f);
		//			}
		//		}
		//		this.can.Handle.sprite = this.PulledHandle;
		//		Vector2 axis2DRaw = player.GetAxis2DRaw(13, 14);
		//		if (axis2DRaw.magnitude > 0.9f)
		//		{
		//			if (!this.prevHadLeftInput)
		//			{
		//				this.can.Body.velocity = axis2DRaw.normalized * 6f;
		//			}
		//			this.prevHadLeftInput = true;
		//		}
		//		else
		//		{
		//			this.prevHadLeftInput = false;
		//		}
		//	}
		//	else
		//	{
		//		if (!this.waitingHands.activeSelf)
		//		{
		//			this.grabbedHands.SetActive(false);
		//			this.waitingHands.SetActive(true);
		//		}
		//		this.can.Handle.sprite = this.RelaxeHandle;
		//	}
		//}
		//else
		//{
		//	DragState dragState = this.controller.CheckDrag(this.can.Hitbox);
		//	if (dragState != DragState.TouchStart)
		//	{
		//		if (dragState != DragState.Dragging)
		//		{
		//			this.can.Handle.sprite = this.RelaxeHandle;
		//		}
		//		else
		//		{
		//			this.can.Handle.sprite = this.PulledHandle;
		//			Vector2 vector = this.controller.DragPosition - this.can.Handle.transform.position;
		//			this.can.Body.velocity = 10f * vector;
		//		}
		//	}
		//	else if (Constants.ShouldPlaySfx())
		//	{
		//		SoundManager.Instance.PlaySound(this.grabSound, false, 1f);
		//	}
		//}
		//if (!this.can.Body.IsTouching(this.can.Success))
		//{
		//	this.MyNormTask.NextStep();
		//	base.StartCoroutine(base.CoStartClose(0.7f));
		//}
	}

	// Token: 0x04000113 RID: 275
	public GarbageCanBehaviour[] GarbagePrefabs;

	// Token: 0x04000114 RID: 276
	public Sprite RelaxeHandle;

	// Token: 0x04000115 RID: 277
	public Sprite PulledHandle;

	// Token: 0x04000116 RID: 278
	private GarbageCanBehaviour can;

	// Token: 0x04000117 RID: 279
	public AudioClip grabSound;

	// Token: 0x04000118 RID: 280
	public Controller controller = new Controller();

	// Token: 0x04000119 RID: 281
	public Transform handCursorObject;

	// Token: 0x0400011A RID: 282
	public GameObject waitingHands;

	// Token: 0x0400011B RID: 283
	public GameObject grabbedHands;

	// Token: 0x0400011C RID: 284
	public SpriteRenderer[] handSprites;

	// Token: 0x0400011D RID: 285
	private bool prevHadLeftInput;

	// Token: 0x0400011E RID: 286
	private const float stickVelocityMagnitude = 6f;
}
