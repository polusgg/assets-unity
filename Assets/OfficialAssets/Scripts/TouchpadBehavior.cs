using System;
//using Rewired;
//using Rewired.ControllerExtensions;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class TouchpadBehavior : MonoBehaviour
{
	// Token: 0x060008BB RID: 2235 RVA: 0x00038DE9 File Offset: 0x00036FE9
	private void Start()
	{
		this.GetExtension();
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Combine(ActiveInputManager.CurrentInputSourceChanged, new Action(this.GetExtension));
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00038E11 File Offset: 0x00037011
	private void OnDestroy()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Remove(ActiveInputManager.CurrentInputSourceChanged, new Action(this.GetExtension));
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00038E34 File Offset: 0x00037034
	private void GetExtension()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//this.ds4 = null;
		//if (player != null && player.controllers.joystickCount > 0)
		//{
		//	this.ds4 = player.controllers.Joysticks[0].GetExtension<IDualShock4Extension>();
		//}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00038E84 File Offset: 0x00037084
	private void Update()
	{
		//if (this.ds4 != null && this.ds4.touchCount > 0)
		//{
		//	Vector2 vector;
		//	this.ds4.GetTouchPosition(0, ref vector);
		//	if (!this.touching)
		//	{
		//		this.touching = true;
		//		this.firstTouch = true;
		//		this.firstTouchPos = vector;
		//	}
		//	else
		//	{
		//		this.firstTouch = false;
		//	}
		//	this.delta = vector - this.firstTouchPos;
		//	this.delta.x = this.delta.x * (this.touchpadSensitivity * this.aspect);
		//	this.delta.y = this.delta.y * this.touchpadSensitivity;
		//	this.fromCenter = vector - this.toCenter;
		//	this.fromCenter.x = this.fromCenter.x * (this.touchpadSensitivity * this.aspect);
		//	this.fromCenter.y = this.fromCenter.y * this.touchpadSensitivity;
		//	return;
		//}
		//this.touching = false;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00038F70 File Offset: 0x00037170
	public bool IsTouching()
	{
		return this.touching;
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00038F78 File Offset: 0x00037178
	public bool IsFirstTouch()
	{
		return this.firstTouch;
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00038F80 File Offset: 0x00037180
	public void ResetTouch()
	{
		this.touching = false;
		this.firstTouch = false;
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00038F90 File Offset: 0x00037190
	public Vector2 GetTouchVector()
	{
		if (this.touching)
		{
			return this.delta;
		}
		return Vector2.zero;
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00038FA6 File Offset: 0x000371A6
	public Vector2 GetCenterToTouch()
	{
		if (this.touching)
		{
			return this.fromCenter;
		}
		return Vector2.zero;
	}

	// Token: 0x04000A10 RID: 2576
	private float aspect = (float)Screen.width / (float)Screen.height;

	// Token: 0x04000A11 RID: 2577
	private bool touching;

	// Token: 0x04000A12 RID: 2578
	private bool firstTouch;

	// Token: 0x04000A13 RID: 2579
	private Vector2 toCenter = new Vector2(0.5f, 0.5f);

	// Token: 0x04000A14 RID: 2580
	private Vector2 firstTouchPos;

	// Token: 0x04000A15 RID: 2581
	private Vector2 delta;

	// Token: 0x04000A16 RID: 2582
	private Vector2 fromCenter;

	// Token: 0x04000A17 RID: 2583
	//private IDualShock4Extension ds4;

	// Token: 0x04000A18 RID: 2584
	public float touchpadSensitivity = 3f;
}
