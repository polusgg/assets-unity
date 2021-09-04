using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class PassiveButtonManager : DestroyableSingleton<PassiveButtonManager>
{
	// Token: 0x06000569 RID: 1385 RVA: 0x00024291 File Offset: 0x00022491
	public void RegisterOne(PassiveUiElement button)
	{
		this.Buttons.Add(button);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0002429F File Offset: 0x0002249F
	public void RemoveOne(PassiveUiElement passiveButton)
	{
		this.Buttons.Remove(passiveButton);
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000242AE File Offset: 0x000224AE
	public void RegisterOne(IFocusHolder focusHolder)
	{
		this.FocusHolders.Add(focusHolder);
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000242BC File Offset: 0x000224BC
	public void RemoveOne(IFocusHolder focusHolder)
	{
		this.FocusHolders.Remove(focusHolder);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000242CC File Offset: 0x000224CC
	public void Update()
	{
		if (!Application.isFocused)
		{
			return;
		}
		this.controller.Update();
		for (int i = 1; i < this.Buttons.Count; i++)
		{
			if (PassiveButtonManager.DepthComparer.Instance.Compare(this.Buttons[i - 1], this.Buttons[i]) > 0)
			{
				this.Buttons.Sort(PassiveButtonManager.DepthComparer.Instance);
				break;
			}
		}
		this.HandleMouseOut();
		for (int j = 0; j < this.Buttons.Count; j++)
		{
			PassiveUiElement passiveUiElement = this.Buttons[j];
			if (!passiveUiElement)
			{
				this.Buttons.RemoveAt(j);
				j--;
			}
			else if (passiveUiElement.isActiveAndEnabled)
			{
				if (passiveUiElement.ClickMask)
				{
					Vector2 position = this.controller.GetTouch(0).Position;
					if (!passiveUiElement.ClickMask.OverlapPoint(position))
					{
						goto IL_212;
					}
				}
				for (int k = 0; k < passiveUiElement.Colliders.Length; k++)
				{
					Collider2D col = passiveUiElement.Colliders[k];
					if (col && col.isActiveAndEnabled)
					{
						this.HandleMouseOver(passiveUiElement, col);
						switch (this.controller.CheckDrag(col))
						{
						case DragState.TouchStart:
							if (passiveUiElement.HandleDown)
							{
								passiveUiElement.ReceiveClickDown();
							}
							break;
						case DragState.Holding:
							if (passiveUiElement.HandleRepeat)
							{
								passiveUiElement.ReceiveRepeatDown();
							}
							break;
						case DragState.Dragging:
							if (passiveUiElement.HandleDrag)
							{
								Vector2 dragDelta = this.controller.DragPosition - this.controller.DragStartPosition;
								passiveUiElement.ReceiveClickDrag(dragDelta);
								this.controller.ResetDragPosition();
							}
							else if (passiveUiElement.HandleRepeat)
							{
								passiveUiElement.ReceiveRepeatDown();
							}
							else if (this.Buttons.Any((PassiveUiElement b2) => b2.HandleDrag && b2.isActiveAndEnabled && b2.transform.position.z > col.transform.position.z))
							{
								this.controller.ClearTouch();
							}
							break;
						case DragState.Released:
							if (passiveUiElement.HandleUp)
							{
								passiveUiElement.ReceiveClickUp();
							}
							break;
						}
					}
				}
			}
			IL_212:;
		}
		if (this.controller.AnyTouchDown)
		{
			Vector2 touch = this.GetTouch(true);
			this.HandleFocus(touch);
		}
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00024520 File Offset: 0x00022720
	private void HandleFocus(Vector2 pt)
	{
		// TODO BE SMARTER
		bool flag = false;
		// Func<Collider2D, bool> <>9__1;
		for (int i = 0; i < this.FocusHolders.Count; i++)
		{
			IFocusHolder focusHolder = this.FocusHolders[i];
			if (!(focusHolder as MonoBehaviour))
			{
				this.FocusHolders.RemoveAt(i);
				i--;
			}
			else if (focusHolder.CheckCollision(pt))
			{
				float depth = (focusHolder as MonoBehaviour).transform.position.z;
				if (!this.Buttons.Any(delegate(PassiveUiElement top)
				{
					if (top.transform.position.z < depth)
					{
						IEnumerable<Collider2D> colliders = top.Colliders;
						// Func<Collider2D, bool> predicate;
						// if ((predicate = <>9__1) == null) // TODO REIMPLEMENT
						// {
						// 	predicate = (<>9__1 = ((Collider2D c) => c.OverlapPoint(pt)));
						// }
						// return colliders.Any(predicate);
					}
					return false;
				}))
				{
					flag = true;
					focusHolder.GiveFocus();
					for (int j = 0; j < this.FocusHolders.Count; j++)
					{
						if (j != i)
						{
							this.FocusHolders[j].LoseFocus();
						}
					}
					break;
				}
				break;
			}
		}
		if (!flag)
		{
			for (int k = 0; k < this.FocusHolders.Count; k++)
			{
				this.FocusHolders[k].LoseFocus();
			}
		}
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00024648 File Offset: 0x00022848
	public void LoseFocusForAll()
	{
		for (int i = 0; i < this.FocusHolders.Count; i++)
		{
			this.FocusHolders[i].LoseFocus();
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0002467C File Offset: 0x0002287C
	private void HandleMouseOut()
	{
		if (this.currentOver)
		{
			bool flag = false;
			for (int i = 0; i < this.controller.Touches.Length; i++)
			{
				Controller.TouchState pt = this.controller.GetTouch(i);
				if (pt.active && this.currentOver.Colliders.Any((Collider2D c) => c.OverlapPoint(pt.Position)))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.currentOver.ReceiveMouseOut();
				this.currentOver = null;
			}
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0002470C File Offset: 0x0002290C
	private void HandleMouseOver(PassiveUiElement button, Collider2D col)
	{
		if (!button.HandleOverOut || button == this.currentOver)
		{
			return;
		}
		if (button.ClickMask)
		{
			Vector2 position = this.controller.GetTouch(0).Position;
			if (!button.ClickMask.OverlapPoint(position))
			{
				return;
			}
		}
		if (this.currentOver && button.transform.position.z > this.currentOver.transform.position.z)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < this.controller.Touches.Length; i++)
		{
			if (this.controller.Touches[i].active && col.OverlapPoint(this.controller.GetTouch(i).Position))
			{
				flag = true;
			}
		}
		if (flag)
		{
			if (this.currentOver && this.currentOver != button)
			{
				this.currentOver.ReceiveMouseOut();
			}
			this.currentOver = button;
			this.currentOver.ReceiveMouseOver();
			return;
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00024818 File Offset: 0x00022A18
	private Vector2 GetTouch(bool getDownTouch)
	{
		if (getDownTouch)
		{
			for (int i = 0; i < this.controller.Touches.Length; i++)
			{
				if (this.controller.Touches[i].TouchStart)
				{
					return this.controller.Touches[i].Position;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.controller.Touches.Length; j++)
			{
				if (this.controller.Touches[j].TouchEnd)
				{
					return this.controller.Touches[j].Position;
				}
			}
		}
		return new Vector2(-5000f, -5000f);
	}

	// Token: 0x0400061B RID: 1563
	public List<PassiveUiElement> Buttons = new List<PassiveUiElement>();

	// Token: 0x0400061C RID: 1564
	private List<IFocusHolder> FocusHolders = new List<IFocusHolder>();

	// Token: 0x0400061D RID: 1565
	private PassiveUiElement currentOver;

	// Token: 0x0400061E RID: 1566
	public Controller controller = new Controller();

	// Token: 0x0400061F RID: 1567
	private PassiveButtonManager.ButtonStates currentState;

	// Token: 0x04000620 RID: 1568
	private Collider2D[] results = new Collider2D[40];

	// Token: 0x02000372 RID: 882
	private enum ButtonStates
	{
		// Token: 0x04001945 RID: 6469
		Up,
		// Token: 0x04001946 RID: 6470
		Down,
		// Token: 0x04001947 RID: 6471
		Drag
	}

	// Token: 0x02000373 RID: 883
	private class DepthComparer : IComparer<MonoBehaviour>
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x0006F8B4 File Offset: 0x0006DAB4
		public int Compare(MonoBehaviour x, MonoBehaviour y)
		{
			if (x == null)
			{
				return 1;
			}
			if (y == null)
			{
				return -1;
			}
			return x.transform.position.z.CompareTo(y.transform.position.z);
		}

		// Token: 0x04001948 RID: 6472
		public static readonly PassiveButtonManager.DepthComparer Instance = new PassiveButtonManager.DepthComparer();
	}
}
