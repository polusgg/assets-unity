﻿using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class SpecimenGame : Minigame
{
	// Token: 0x060007F3 RID: 2035 RVA: 0x000332E8 File Offset: 0x000314E8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		base.SetupInput(true);
		this.SpecimenSprites = new SpriteRenderer[this.Specimens.Length];
		for (int i = 0; i < this.Specimens.Length; i++)
		{
			this.SpecimenSprites[i] = this.Specimens[i].GetComponent<SpriteRenderer>();
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00033340 File Offset: 0x00031540
	public void Update()
	{
		//this.cont.Update();
		//if (this.selectedSpecimen != this.prevSelectedSpecimen)
		//{
		//	if (this.prevSelectedSpecimen != -1)
		//	{
		//		this.SpecimenSprites[this.prevSelectedSpecimen].material.SetFloat("_Outline", 0f);
		//	}
		//	this.prevSelectedSpecimen = this.selectedSpecimen;
		//	this.SpecimenSprites[this.prevSelectedSpecimen].material.SetFloat("_Outline", 10f);
		//}
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	if (!this.amOpening && this.amClosing == Minigame.CloseState.None)
		//	{
		//		Player player = ReInput.players.GetPlayer(0);
		//		Vector2 axis2DRaw = player.GetAxis2DRaw(13, 14);
		//		if (player.GetButton(11))
		//		{
		//			if (!this.prevHadButton)
		//			{
		//				this.inputHandler.disableVirtualCursor = false;
		//				VirtualCursor.instance.SetWorldPosition(this.Specimens[this.selectedSpecimen].transform.position);
		//			}
		//			else
		//			{
		//				Vector3 position = this.Specimens[this.selectedSpecimen].transform.position;
		//				position.x = VirtualCursor.currentPosition.x;
		//				position.y = VirtualCursor.currentPosition.y;
		//				if (Vector2.Distance(position, this.Slots[this.selectedSpecimen].position) < 0.25f)
		//				{
		//					position = this.Slots[this.selectedSpecimen].position;
		//				}
		//				this.Specimens[this.selectedSpecimen].transform.position = position;
		//			}
		//			this.prevHadButton = true;
		//			return;
		//		}
		//		if (this.prevHadButton)
		//		{
		//			this.inputHandler.disableVirtualCursor = true;
		//			if (Vector2.Distance(this.Specimens[this.selectedSpecimen].transform.position, this.Slots[this.selectedSpecimen].position) < 0.25f)
		//			{
		//				this.Specimens[this.selectedSpecimen].transform.position = this.Slots[this.selectedSpecimen].position;
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.PlaceSounds.Random<AudioClip>(), false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		//				}
		//			}
		//			this.CheckForWin();
		//		}
		//		this.prevHadButton = false;
		//		if (axis2DRaw.sqrMagnitude > 0.7f)
		//		{
		//			if (!this.prevHadStick)
		//			{
		//				Vector2 normalized = axis2DRaw.normalized;
		//				int num = -1;
		//				float num2 = 0f;
		//				for (int i = 0; i < this.Specimens.Length; i++)
		//				{
		//					Vector2 vector = this.Specimens[i].transform.position - this.Specimens[this.selectedSpecimen].transform.position;
		//					float magnitude = vector.magnitude;
		//					vector /= magnitude;
		//					float num3 = Vector2.Dot(vector, normalized) / Mathf.Lerp(magnitude, 1f, 0.95f);
		//					if (i != this.selectedSpecimen && (num == -1 || (num3 > num2 && num3 > 0f)))
		//					{
		//						num2 = num3;
		//						num = i;
		//					}
		//				}
		//				if (num != -1)
		//				{
		//					this.selectedSpecimen = num;
		//				}
		//			}
		//			this.prevHadStick = true;
		//			return;
		//		}
		//		this.prevHadStick = false;
		//		return;
		//	}
		//}
		//else
		//{
		//	this.prevHadButton = false;
		//	this.prevHadStick = false;
		//	if (this.prevSelectedSpecimen != -1)
		//	{
		//		this.SpecimenSprites[this.prevSelectedSpecimen].material.SetFloat("_Outline", 0f);
		//		this.prevSelectedSpecimen = -1;
		//	}
		//	for (int j = 0; j < this.Specimens.Length; j++)
		//	{
		//		Collider2D collider2D = this.Specimens[j];
		//		DragState dragState = this.cont.CheckDrag(collider2D);
		//		if (dragState != DragState.Dragging)
		//		{
		//			if (dragState == DragState.Released)
		//			{
		//				if (Vector2.Distance(collider2D.transform.position, this.Slots[j].position) < 0.25f)
		//				{
		//					collider2D.transform.position = this.Slots[j].position;
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.PlaceSounds.Random<AudioClip>(), false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		//					}
		//				}
		//				this.CheckForWin();
		//			}
		//		}
		//		else
		//		{
		//			Vector3 localPosition = this.cont.DragPosition - base.transform.position;
		//			localPosition.z = 0f;
		//			collider2D.transform.localPosition = localPosition;
		//			if (Vector2.Distance(collider2D.transform.position, this.Slots[j].position) < 0.25f)
		//			{
		//				collider2D.transform.position = this.Slots[j].position;
		//			}
		//		}
		//	}
		//}
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00033824 File Offset: 0x00031A24
	private void CheckForWin()
	{
		bool flag = true;
		for (int i = 0; i < this.Specimens.Length; i++)
		{
			if (Vector2.Distance(this.Specimens[i].transform.position, this.Slots[i].position) > 0.25f)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			if (this.prevSelectedSpecimen != -1)
			{
				this.SpecimenSprites[this.prevSelectedSpecimen].material.SetFloat("_Outline", 0f);
			}
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
	}

	// Token: 0x0400093E RID: 2366
	public Collider2D[] Specimens;

	// Token: 0x0400093F RID: 2367
	public Transform[] Slots;

	// Token: 0x04000940 RID: 2368
	private Controller cont = new Controller();

	// Token: 0x04000941 RID: 2369
	public AudioClip[] PlaceSounds;

	// Token: 0x04000942 RID: 2370
	private SpriteRenderer[] SpecimenSprites;

	// Token: 0x04000943 RID: 2371
	public Color highlightColor;

	// Token: 0x04000944 RID: 2372
	private bool prevHadStick;

	// Token: 0x04000945 RID: 2373
	private bool prevHadButton;

	// Token: 0x04000946 RID: 2374
	private int prevSelectedSpecimen = -1;

	// Token: 0x04000947 RID: 2375
	private int selectedSpecimen;
}
