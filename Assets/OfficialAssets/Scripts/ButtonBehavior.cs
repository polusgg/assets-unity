using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B8 RID: 184
public class ButtonBehavior : UiElement
{
	// Token: 0x06000461 RID: 1121 RVA: 0x0001C1BC File Offset: 0x0001A3BC
	public void OnEnable()
	{
		this.colliders = base.GetComponents<Collider2D>();
		this.myController.Reset();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		if (!this.checkedClickEvent)
		{
			this.checkedClickEvent = true;
			int persistentEventCount = this.OnClick.GetPersistentEventCount();
			for (int i = 0; i < persistentEventCount; i++)
			{
				string persistentMethodName = this.OnClick.GetPersistentMethodName(i);
				if (base.gameObject.name.ToLower().Contains("close") && persistentMethodName.ToLower().Contains("close"))
				{
					base.gameObject.AddComponent<CloseButtonConsoleBehaviour>();
				}
			}
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001C25C File Offset: 0x0001A45C
	public void Update()
	{
		this.myController.Update();
		foreach (Collider2D coll in this.colliders)
		{
			switch (this.myController.CheckDrag(coll))
			{
			case DragState.TouchStart:
				if (this.OnDown)
				{
					this.OnClick.Invoke();
				}
				break;
			case DragState.Dragging:
				if (this.Repeat)
				{
					this.downTime += Time.fixedDeltaTime;
					if (this.downTime >= 0.3f)
					{
						this.downTime = 0f;
						this.OnClick.Invoke();
					}
				}
				else
				{
					this.downTime = 0f;
				}
				break;
			case DragState.Released:
				if (this.OnUp)
				{
					this.OnClick.Invoke();
				}
				break;
			}
		}
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0001C32E File Offset: 0x0001A52E
	public void ReceiveClick()
	{
		this.OnClick.Invoke();
	}

	// Token: 0x0400051C RID: 1308
	public bool OnUp = true;

	// Token: 0x0400051D RID: 1309
	public bool OnDown;

	// Token: 0x0400051E RID: 1310
	public bool Repeat;

	// Token: 0x0400051F RID: 1311
	public Button.ButtonClickedEvent OnClick = new Button.ButtonClickedEvent();

	// Token: 0x04000520 RID: 1312
	private Controller myController = new Controller();

	// Token: 0x04000521 RID: 1313
	private Collider2D[] colliders;

	// Token: 0x04000522 RID: 1314
	private float downTime;

	// Token: 0x04000523 RID: 1315
	public SpriteRenderer spriteRenderer;

	// Token: 0x04000524 RID: 1316
	private bool checkedClickEvent;
}
