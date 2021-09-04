using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class NumberMenu : MonoBehaviour
{
	// Token: 0x060000BB RID: 187 RVA: 0x00004EC1 File Offset: 0x000030C1
	private void Awake()
	{
		this.defaultButtonSelected = null;
		this.controllerSelectable = new List<UiElement>();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00004ED5 File Offset: 0x000030D5
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00004EE3 File Offset: 0x000030E3
	public void SetValue(string val)
	{
		this.text.Text = val;
		this.Close();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004EF7 File Offset: 0x000030F7
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040000B3 RID: 179
	public TextRenderer text;

	// Token: 0x040000B4 RID: 180
	public NumberSetter numberSetter;

	// Token: 0x040000B5 RID: 181
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040000B6 RID: 182
	private UiElement defaultButtonSelected;

	// Token: 0x040000B7 RID: 183
	private List<UiElement> controllerSelectable;
}
