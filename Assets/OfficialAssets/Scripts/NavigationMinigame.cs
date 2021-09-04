using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class NavigationMinigame : Minigame
{
	// Token: 0x0600079A RID: 1946 RVA: 0x000309EC File Offset: 0x0002EBEC
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//this.crossHair = Random.insideUnitCircle.normalized / 2f * 0.6f;
		//Vector3 localPosition;
		//localPosition..ctor(this.crossHair.x * this.TwoAxisImage.bounds.size.x, this.crossHair.y * this.TwoAxisImage.bounds.size.y, -2f);
		//this.CrossHairImage.transform.localPosition = localPosition;
		//this.TwoAxisImage.material.SetVector("_CrossHair", this.crossHair + this.half);
		//base.SetupInput(true);
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00030AC0 File Offset: 0x0002ECC0
	public void FixedUpdate()
	{
		//if (this.MyNormTask && this.MyNormTask.IsComplete)
		//{
		//	return;
		//}
		//this.myController.Update();
		//if (Controller.currentTouchType != Controller.TouchType.Joystick)
		//{
		//	switch (this.myController.CheckDrag(this.hitbox))
		//	{
		//	case DragState.TouchStart:
		//	case DragState.Dragging:
		//	{
		//		Vector2 dragPosition = this.myController.DragPosition;
		//		Vector2 a = dragPosition - (this.TwoAxisImage.transform.position - this.TwoAxisImage.bounds.size / 2f);
		//		this.crossHair = a.Div(this.TwoAxisImage.bounds.size);
		//		if ((this.crossHair - this.half).magnitude < 0.45f)
		//		{
		//			Vector3 localPosition = dragPosition - base.transform.position;
		//			localPosition.z = -2f;
		//			this.CrossHairImage.transform.localPosition = localPosition;
		//			this.TwoAxisImage.material.SetVector("_CrossHair", this.crossHair);
		//			return;
		//		}
		//		break;
		//	}
		//	case DragState.Holding:
		//		break;
		//	case DragState.Released:
		//		if ((this.crossHair - this.half).magnitude < 0.05f)
		//		{
		//			base.StartCoroutine(this.CompleteGame());
		//			this.MyNormTask.NextStep();
		//		}
		//		break;
		//	default:
		//		return;
		//	}
		//	return;
		//}
		//Player player = ReInput.players.GetPlayer(0);
		//Vector2 vector = Vector2.zero;
		//if (player.GetButton(11))
		//{
		//	if (!this.prevHadInput)
		//	{
		//		this.inputHandler.disableVirtualCursor = false;
		//		VirtualCursor.instance.SetWorldPosition(this.CrossHairImage.transform.position);
		//	}
		//	vector = VirtualCursor.currentPosition;
		//	Vector2 a2 = vector - (this.TwoAxisImage.transform.position - this.TwoAxisImage.bounds.size / 2f);
		//	this.crossHair = a2.Div(this.TwoAxisImage.bounds.size);
		//	if ((this.crossHair - this.half).magnitude < 0.45f)
		//	{
		//		Vector3 localPosition2 = vector - base.transform.position;
		//		localPosition2.z = -2f;
		//		this.CrossHairImage.transform.localPosition = localPosition2;
		//		this.TwoAxisImage.material.SetVector("_CrossHair", this.crossHair);
		//	}
		//	this.prevHadInput = true;
		//	return;
		//}
		//if (this.prevHadInput)
		//{
		//	this.inputHandler.disableVirtualCursor = true;
		//	if ((this.crossHair - this.half).magnitude < 0.05f)
		//	{
		//		base.StartCoroutine(this.CompleteGame());
		//		this.MyNormTask.NextStep();
		//	}
		//}
		//this.prevHadInput = false;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00030DE3 File Offset: 0x0002EFE3
	private IEnumerator CompleteGame()
	{
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		Color green = new Color(0f, 0.8f, 0f, 1f);
		Color32 yellow = new Color32(byte.MaxValue, 202, 0, byte.MaxValue);
		this.CrossHairImage.transform.localPosition = new Vector3(0f, 0f, -2f);
		this.TwoAxisImage.material.SetVector("_CrossHair", this.half);
		this.CrossHairImage.color = yellow;
		this.TwoAxisImage.material.SetColor("_CrossColor", yellow);
		yield return wait;
		this.CrossHairImage.color = Color.white;
		this.TwoAxisImage.material.SetColor("_CrossColor", Color.white);
		yield return wait;
		this.CrossHairImage.color = yellow;
		this.TwoAxisImage.material.SetColor("_CrossColor", yellow);
		yield return wait;
		this.CrossHairImage.color = Color.white;
		this.TwoAxisImage.material.SetColor("_CrossColor", Color.white);
		yield return wait;
		this.CrossHairImage.color = green;
		this.TwoAxisImage.material.SetColor("_CrossColor", green);
		yield return base.CoStartClose(0.75f);
		yield break;
	}

	// Token: 0x040008A2 RID: 2210
	public MeshRenderer TwoAxisImage;

	// Token: 0x040008A3 RID: 2211
	public SpriteRenderer CrossHairImage;

	// Token: 0x040008A4 RID: 2212
	public Collider2D hitbox;

	// Token: 0x040008A5 RID: 2213
	private Controller myController = new Controller();

	// Token: 0x040008A6 RID: 2214
	private Vector2 crossHair;

	// Token: 0x040008A7 RID: 2215
	private Vector2 half = new Vector2(0.5f, 0.5f);

	// Token: 0x040008A8 RID: 2216
	private bool prevHadInput;
}
