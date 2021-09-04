using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class InfoTextBox : MonoBehaviour
{
	// Token: 0x06000075 RID: 117 RVA: 0x00003A87 File Offset: 0x00001C87
	public void Awake()
	{
		this.SetOneButton();
		if (this.isConfirmWindow)
		{
			this.SetConfirmWindow();
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003A9D File Offset: 0x00001C9D
	public void Close()
	{
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003AAA File Offset: 0x00001CAA
	public void SetConfirmWindow()
	{
		this.background.color = Color.green;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003ABC File Offset: 0x00001CBC
	public void SetTwoButtons()
	{
		this.button2Trans.gameObject.SetActive(true);
		this.button1Trans.localPosition = new Vector2(2f, this.button1Trans.localPosition.y);
		this.button2Trans.localPosition = new Vector2(-2f, this.button2Trans.localPosition.y);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003B2E File Offset: 0x00001D2E
	public void SetOneButton()
	{
		this.button2Trans.gameObject.SetActive(false);
		this.button1Trans.localPosition = new Vector2(0f, this.button1Trans.localPosition.y);
	}

	// Token: 0x04000041 RID: 65
	public SpriteRenderer background;

	// Token: 0x04000042 RID: 66
	public bool isConfirmWindow;

	// Token: 0x04000043 RID: 67
	public TextRenderer titleTexxt;

	// Token: 0x04000044 RID: 68
	public TextRenderer bodyText;

	// Token: 0x04000045 RID: 69
	public TextRenderer button1Text;

	// Token: 0x04000046 RID: 70
	public TextRenderer button2Text;

	// Token: 0x04000047 RID: 71
	public PassiveButton button1;

	// Token: 0x04000048 RID: 72
	public PassiveButton button2;

	// Token: 0x04000049 RID: 73
	public Transform button1Trans;

	// Token: 0x0400004A RID: 74
	public Transform button2Trans;
}
