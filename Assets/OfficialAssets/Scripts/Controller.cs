using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class Controller
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000DCB6 File Offset: 0x0000BEB6
	public static Controller.TouchType currentTouchType
	{
		get
		{
			return (Controller.TouchType)ActiveInputManager.currentControlType;
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
	public Controller()
	{
		this.Touches = new Controller.TouchState[4];
		for (int i = 0; i < this.Touches.Length; i++)
		{
			this.Touches[i] = new Controller.TouchState();
		}
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000DD14 File Offset: 0x0000BF14
	public bool CheckHover(Collider2D coll)
	{
		if (!coll)
		{
			return false;
		}
		for (int i = 0; i < this.Touches.Length; i++)
		{
			Controller.TouchState touchState = this.Touches[i];
			if (coll.OverlapPoint(touchState.Position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000DD60 File Offset: 0x0000BF60
	public Vector2 HoverPosition
	{
		get
		{
			for (int i = this.Touches.Length - 1; i >= 0; i--)
			{
				Controller.TouchState touchState = this.Touches[i];
				if (touchState.active)
				{
					return touchState.Position;
				}
			}
			return Vector2.zero;
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
	public DragState CheckDrag(Collider2D coll)
	{
		if (!coll)
		{
			return DragState.NoTouch;
		}
		if (this.touchId > -1 && (!this.amTouching || !this.amTouching.isActiveAndEnabled))
		{
			this.touchId = -1;
			this.amTouching = null;
		}
		if (this.touchId <= -1)
		{
			for (int i = 0; i < this.Touches.Length; i++)
			{
				Controller.TouchState touchState = this.Touches[i];
				if (touchState.TouchStart && coll.OverlapPoint(touchState.Position))
				{
					this.amTouching = coll;
					this.touchId = i;
					touchState.dragState = DragState.TouchStart;
					return DragState.TouchStart;
				}
			}
			return DragState.NoTouch;
		}
		if (coll != this.amTouching)
		{
			return DragState.NoTouch;
		}
		Controller.TouchState touchState2 = this.Touches[this.touchId];
		if (!touchState2.IsDown)
		{
			this.amTouching = null;
			this.touchId = -1;
			touchState2.dragState = DragState.Released;
			return DragState.Released;
		}
		if (Vector2.Distance(touchState2.DownAt, touchState2.Position) > 0.05f || touchState2.dragState == DragState.Dragging)
		{
			touchState2.dragState = DragState.Dragging;
			return DragState.Dragging;
		}
		touchState2.dragState = DragState.Holding;
		return DragState.Holding;
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000DEAB File Offset: 0x0000C0AB
	public bool AnyTouch
	{
		get
		{
			return this.Touches[0].IsDown || this.Touches[1].IsDown;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000DECB File Offset: 0x0000C0CB
	public bool AnyTouchDown
	{
		get
		{
			return this.Touches[0].TouchStart || this.Touches[1].TouchStart;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000DEEB File Offset: 0x0000C0EB
	public bool AnyTouchUp
	{
		get
		{
			return this.Touches[0].TouchEnd || this.Touches[1].TouchEnd;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000DF0B File Offset: 0x0000C10B
	public bool FirstDown
	{
		get
		{
			return this.Touches[0].TouchStart;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000DF1A File Offset: 0x0000C11A
	public Vector2 DragPosition
	{
		get
		{
			if (this.touchId < 0)
			{
				return Vector2.zero;
			}
			return this.Touches[this.touchId].Position;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060001CA RID: 458 RVA: 0x0000DF3D File Offset: 0x0000C13D
	public Vector2 DragStartPosition
	{
		get
		{
			return this.Touches[this.touchId].DownAt;
		}
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000DF51 File Offset: 0x0000C151
	public void ResetDragPosition()
	{
		this.Touches[this.touchId].DownAt = this.Touches[this.touchId].Position;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000DF77 File Offset: 0x0000C177
	public void ClearTouch()
	{
		if (this.touchId < 0)
		{
			return;
		}
		Controller.TouchState touchState = this.Touches[this.touchId];
		touchState.dragState = DragState.NoTouch;
		touchState.TouchStart = true;
		this.amTouching = null;
		this.touchId = -1;
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060001CD RID: 461 RVA: 0x0000DFAB File Offset: 0x0000C1AB
	// (set) Token: 0x060001CE RID: 462 RVA: 0x0000DFB3 File Offset: 0x0000C1B3
	public Camera mainCam { get; set; }

	// Token: 0x060001CF RID: 463 RVA: 0x0000DFBC File Offset: 0x0000C1BC
	public void Update()
	{
			if (!this.mainCam)
			{
				this.mainCam = Camera.main;
			}
			Controller.TouchState touchState = this.Touches[0];
			bool mouseButton = Input.GetMouseButton(0);
			touchState.Position = this.mainCam.ScreenToWorldPoint(Input.mousePosition);
			touchState.TouchStart = (!touchState.IsDown && mouseButton);
			if (touchState.TouchStart)
			{
				touchState.DownAt = touchState.Position;
			}
			touchState.TouchEnd = (touchState.IsDown && !mouseButton);
			touchState.IsDown = mouseButton;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000E298 File Offset: 0x0000C498
	public void Reset()
	{
		for (int i = 0; i < this.Touches.Length; i++)
		{
			this.Touches[i] = new Controller.TouchState();
		}
		this.touchId = -1;
		this.amTouching = null;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000E2D3 File Offset: 0x0000C4D3
	public Controller.TouchState GetTouch(int i)
	{
		return this.Touches[i];
	}

	// Token: 0x040002C1 RID: 705
	private const int maxTouchCount = 4;

	// Token: 0x040002C2 RID: 706
	private const int mainTouchIndex = 0;

	// Token: 0x040002C3 RID: 707
	public readonly Controller.TouchState[] Touches = new Controller.TouchState[4];

	// Token: 0x040002C4 RID: 708
	private Collider2D amTouching;

	// Token: 0x040002C5 RID: 709
	private int touchId = -1;

	// Token: 0x040002C6 RID: 710
	private static Vector3 oldMousePos = new Vector3(0f, 0f, 0f);

	// Token: 0x020002EF RID: 751
	public class TouchState
	{
		// Token: 0x040016C0 RID: 5824
		public Vector2 DownAt;

		// Token: 0x040016C1 RID: 5825
		public Vector2 Position;

		// Token: 0x040016C2 RID: 5826
		public bool WasDown;

		// Token: 0x040016C3 RID: 5827
		public bool IsDown;

		// Token: 0x040016C4 RID: 5828
		public bool TouchStart;

		// Token: 0x040016C5 RID: 5829
		public bool TouchEnd;

		// Token: 0x040016C6 RID: 5830
		public DragState dragState;

		// Token: 0x040016C7 RID: 5831
		public bool active;
	}

	// Token: 0x020002F0 RID: 752
	public enum TouchType
	{
		// Token: 0x040016C9 RID: 5833
		Joystick,
		// Token: 0x040016CA RID: 5834
		Mouse,
		// Token: 0x040016CB RID: 5835
		Touch
	}
}
