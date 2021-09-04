using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DB RID: 219
public class PassiveButton : PassiveUiElement
{
	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000561 RID: 1377 RVA: 0x00024135 File Offset: 0x00022335
	public override bool HandleUp
	{
		get
		{
			return this.OnUp;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x0002413D File Offset: 0x0002233D
	public override bool HandleDown
	{
		get
		{
			return this.OnDown;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000563 RID: 1379 RVA: 0x00024145 File Offset: 0x00022345
	public override bool HandleRepeat
	{
		get
		{
			return this.OnRepeat;
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00024150 File Offset: 0x00022350
	private void OnEnable()
	{
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

	// Token: 0x06000565 RID: 1381 RVA: 0x000241CB File Offset: 0x000223CB
	public override void ReceiveClickDown()
	{
		if (this.ClickSound)
		{
			SoundManager.Instance.PlaySound(this.ClickSound, false, 1f);
		}
		this.OnClick.Invoke();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x000241FC File Offset: 0x000223FC
	public override void ReceiveRepeatDown()
	{
		this.repeatTimer += Time.deltaTime;
		if (this.repeatTimer < this.RepeatDuration)
		{
			return;
		}
		this.repeatTimer = 0f;
		if (this.ClickSound)
		{
			SoundManager.Instance.PlaySound(this.ClickSound, false, 1f);
		}
		this.OnClick.Invoke();
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00024264 File Offset: 0x00022464
	public override void ReceiveClickUp()
	{
		this.ReceiveClickDown();
	}

	// Token: 0x04000613 RID: 1555
	public Button.ButtonClickedEvent OnClick = new Button.ButtonClickedEvent();

	// Token: 0x04000614 RID: 1556
	public AudioClip ClickSound;

	// Token: 0x04000615 RID: 1557
	public bool OnUp = true;

	// Token: 0x04000616 RID: 1558
	public bool OnDown;

	// Token: 0x04000617 RID: 1559
	public bool OnRepeat;

	// Token: 0x04000618 RID: 1560
	public float RepeatDuration = 0.3f;

	// Token: 0x04000619 RID: 1561
	private float repeatTimer;

	// Token: 0x0400061A RID: 1562
	private bool checkedClickEvent;
}
