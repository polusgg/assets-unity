using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class FillCanistersGame : Minigame
{
	// Token: 0x060007D3 RID: 2003 RVA: 0x00031EBE File Offset: 0x000300BE
	public void Start()
	{
		base.StartCoroutine(this.Run());
		base.SetupInput(true);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00031ED4 File Offset: 0x000300D4
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.FillLoop);
		base.Close();
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00031EEC File Offset: 0x000300EC
	private IEnumerator Run()
	{
		//for (;;)
		//{
		//	this.Canister.Gauge.Value = 0f;
		//	yield return Effects.Slide2D(this.Canister.transform, this.CanisterAppearPosition, this.CanisterStartPosition, 0.1f);
		//	this.controller.ClearTouch();
		//	for (;;)
		//	{
		//		this.controller.Update();
		//		if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//		{
		//			float axisRaw = ReInput.players.GetPlayer(0).GetAxisRaw(14);
		//			if (Mathf.Abs(axisRaw) > 0.2f)
		//			{
		//				if (!this.prevHadInput && Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.GrabSound, false, 1f);
		//				}
		//				float num = FloatRange.ReverseLerp(this.Canister.transform.localPosition.y, this.CanisterDragPosition.y, this.CanisterStartPosition.y);
		//				num += axisRaw * 3f * Time.deltaTime;
		//				num = Mathf.Clamp01(num);
		//				this.Canister.transform.localPosition = Vector3.Lerp(this.CanisterDragPosition, this.CanisterStartPosition, num);
		//				this.prevHadInput = true;
		//			}
		//			else
		//			{
		//				if (this.prevHadInput)
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.DropSound, false, 1f);
		//					}
		//					if (FloatRange.ReverseLerp(this.Canister.transform.localPosition.y, this.CanisterDragPosition.y, this.CanisterStartPosition.y) < 0.05f)
		//					{
		//						goto Block_6;
		//					}
		//				}
		//				this.prevHadInput = false;
		//			}
		//		}
		//		else
		//		{
		//			switch (this.controller.CheckDrag(this.Canister.Hitbox))
		//			{
		//			case DragState.TouchStart:
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.GrabSound, false, 1f);
		//				}
		//				break;
		//			case DragState.Dragging:
		//			{
		//				float num2 = FloatRange.ReverseLerp((this.controller.DragPosition - base.transform.position).y, this.CanisterDragPosition.y, this.CanisterStartPosition.y);
		//				this.Canister.transform.localPosition = Vector3.Lerp(this.CanisterDragPosition, this.CanisterStartPosition, num2);
		//				break;
		//			}
		//			case DragState.Released:
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.DropSound, false, 1f);
		//				}
		//				if (FloatRange.ReverseLerp(this.Canister.transform.localPosition.y, this.CanisterDragPosition.y, this.CanisterStartPosition.y) < 0.05f)
		//				{
		//					goto Block_11;
		//				}
		//				break;
		//			}
		//		}
		//		yield return null;
		//	}
		//	IL_357:
		//	AudioSource fillSound = null;
		//	if (Constants.ShouldPlaySfx())
		//	{
		//		fillSound = SoundManager.Instance.PlaySound(this.FillLoop, true, 1f);
		//	}
		//	yield return Effects.Slide2D(this.Canister.transform, this.CanisterDragPosition, this.CanisterSnapPosition, 0.1f);
		//	yield return Effects.Lerp(this.FillTime, delegate(float t)
		//	{
		//		this.Canister.Gauge.Value = t;
		//	});
		//	if (fillSound)
		//	{
		//		fillSound.Stop();
		//	}
		//	Player player = ReInput.players.GetPlayer(0);
		//	float stickInput = 0f;
		//	stickInput = player.GetAxisRaw(13);
		//	bool hasNoRemoveInput = this.controller.CheckDrag(this.Canister.Hitbox) != DragState.TouchStart && player.GetAxisRaw(13) < 0.9f;
		//	while (hasNoRemoveInput)
		//	{
		//		this.controller.Update();
		//		hasNoRemoveInput = (this.controller.CheckDrag(this.Canister.Hitbox) != DragState.TouchStart && stickInput < 0.9f);
		//		stickInput = player.GetAxisRaw(13);
		//		yield return null;
		//	}
		//	player = null;
		//	if (Constants.ShouldPlaySfx())
		//	{
		//		SoundManager.Instance.PlaySound(this.PlugOutSound, false, 1f);
		//	}
		//	yield return Effects.Slide2D(this.Canister.transform, this.CanisterSnapPosition, this.CanisterAwayPosition, 0.3f);
		//	this.MyNormTask.NextStep();
		//	if (this.MyNormTask.IsComplete)
		//	{
		//		break;
		//	}
		//	continue;
		//	Block_6:
		//	if (Constants.ShouldPlaySfx())
		//	{
		//		SoundManager.Instance.PlaySound(this.PlugInSound, false, 1f);
		//		goto IL_357;
		//	}
		//	goto IL_357;
		//	Block_11:
		//	if (Constants.ShouldPlaySfx())
		//	{
		//		SoundManager.Instance.PlaySound(this.PlugInSound, false, 1f);
		//		goto IL_357;
		//	}
		//	goto IL_357;
		//}
		//base.StartCoroutine(base.CoStartClose(0.75f));
		yield break;
	}

	// Token: 0x040008F0 RID: 2288
	private Vector3 CanisterAppearPosition = new Vector3(0f, 4.5f, 0f);

	// Token: 0x040008F1 RID: 2289
	private Vector3 CanisterStartPosition = new Vector3(-0.75f, 1.5f, 0f);

	// Token: 0x040008F2 RID: 2290
	private Vector3 CanisterDragPosition = new Vector3(0.4f, -1f, 0f);

	// Token: 0x040008F3 RID: 2291
	private Vector3 CanisterSnapPosition = new Vector3(0f, -1f, 0f);

	// Token: 0x040008F4 RID: 2292
	private Vector3 CanisterAwayPosition = new Vector3(8f, -1f, 0f);

	// Token: 0x040008F5 RID: 2293
	public float FillTime = 2.5f;

	// Token: 0x040008F6 RID: 2294
	public CanisterBehaviour Canister;

	// Token: 0x040008F7 RID: 2295
	private Controller controller = new Controller();

	// Token: 0x040008F8 RID: 2296
	public AudioClip FillLoop;

	// Token: 0x040008F9 RID: 2297
	public AudioClip DropSound;

	// Token: 0x040008FA RID: 2298
	public AudioClip GrabSound;

	// Token: 0x040008FB RID: 2299
	public AudioClip PlugInSound;

	// Token: 0x040008FC RID: 2300
	public AudioClip PlugOutSound;

	// Token: 0x040008FD RID: 2301
	private bool prevHadInput;
}
