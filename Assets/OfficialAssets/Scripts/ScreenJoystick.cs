using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class ScreenJoystick : MonoBehaviour, IVirtualJoystick
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001DF8D File Offset: 0x0001C18D
	// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0001DF95 File Offset: 0x0001C195
	public Vector2 Delta { get; private set; }

	// Token: 0x060004B8 RID: 1208 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
	private void FixedUpdate()
	{
		this.myController.Update();
		if (this.touchId <= -1)
		{
			for (int i = 0; i < this.myController.Touches.Length; i++)
			{
				Controller.TouchState touchState = this.myController.Touches[i];
				if (touchState.TouchStart)
				{
					bool flag = false;
					int num = Physics2D.OverlapPointNonAlloc(touchState.Position, this.hitBuffer, Constants.NotShipMask);
					for (int j = 0; j < num; j++)
					{
						Collider2D collider2D = this.hitBuffer[j];
						if (collider2D.GetComponent<ButtonBehavior>() || collider2D.GetComponent<PassiveButton>())
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.touchId = i;
						return;
					}
				}
			}
			return;
		}
		Controller.TouchState touchState2 = this.myController.Touches[this.touchId];
		if (touchState2.IsDown)
		{
			Vector2 vector = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
			this.Delta = (touchState2.Position - vector).normalized;
			return;
		}
		this.touchId = -1;
		this.Delta = Vector2.zero;
	}

	// Token: 0x04000582 RID: 1410
	private Collider2D[] hitBuffer = new Collider2D[20];

	// Token: 0x04000584 RID: 1412
	private Controller myController = new Controller();

	// Token: 0x04000585 RID: 1413
	private int touchId = -1;
}
