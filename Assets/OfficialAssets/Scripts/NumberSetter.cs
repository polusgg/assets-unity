using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class NumberSetter : MonoBehaviour
{
	// Token: 0x060000C0 RID: 192 RVA: 0x00004F10 File Offset: 0x00003110
	private void OnEnable()
	{
		if (this.AllButtons != null)
		{
			if (this.selectableObjects.Count == 0)
			{
				foreach (NumberButton numberButton in this.AllButtons)
				{
					this.selectableObjects.Add(numberButton.Button);
				}
			}
			ControllerManager.Instance.OpenOverlayMenu(base.gameObject.name, this.backButton, this.AllButtons[0].Button, this.selectableObjects, false);
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00004F8B File Offset: 0x0000318B
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.gameObject.name);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00004FA4 File Offset: 0x000031A4
	public void Start()
	{
		//Collider2D component = this.ButtonParent.GetComponent<Collider2D>();
		//NumberButton[] allButtons = this.AllButtons;
		//string[] array = new string[0];
		//switch (this.dateType)
		//{
		//case NumberSetter.DateType.DAYS:
		//{
		//	int num = DateTime.DaysInMonth(SaveManager.BirthDateYear, SaveManager.BirthDateMonth);
		//	array = new string[num];
		//	for (int i = 0; i < num; i++)
		//	{
		//		array[i] = (i + 1).ToString();
		//	}
		//	break;
		//}
		//case NumberSetter.DateType.YEARS:
		//{
		//	int year = DateTime.Now.Year;
		//	array = new string[year - 1900];
		//	for (int j = year; j > 1900; j--)
		//	{
		//		array[year - j] = j.ToString();
		//	}
		//	break;
		//}
		//case NumberSetter.DateType.MONTHS:
		//	array = new string[12];
		//	for (int k = 0; k < 12; k++)
		//	{
		//		array[k] = (k + 1).ToString();
		//	}
		//	break;
		//}
		//Vector3 localPosition;
		//localPosition..ctor(0f, this.ButtonStart, -0.5f);
		//this.AllButtons = new NumberButton[array.Length];
		//for (int l = 0; l < array.Length; l++)
		//{
		//	NumberButton button = Object.Instantiate<NumberButton>(this.ButtonPrefab, this.ButtonParent.Inner);
		//	this.AllButtons[l] = button;
		//	if (this.dateType == NumberSetter.DateType.MONTHS)
		//	{
		//		button.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetMonthStringViaNumber(l + 1);
		//	}
		//	else
		//	{
		//		button.Text.Text = array[l];
		//	}
		//	button.Button.OnClick.AddListener(delegate()
		//	{
		//		this.SetData(button);
		//	});
		//	button.Button.ClickMask = component;
		//	switch (this.dateType)
		//	{
		//	case NumberSetter.DateType.DAYS:
		//		if (array[l] == SaveManager.BirthDateDay.ToString())
		//		{
		//			button.Text.Color = Color.green;
		//		}
		//		break;
		//	case NumberSetter.DateType.YEARS:
		//		if (array[l] == SaveManager.BirthDateYear.ToString())
		//		{
		//			button.Text.Color = Color.green;
		//		}
		//		break;
		//	case NumberSetter.DateType.MONTHS:
		//		button.monthNum = l + 1;
		//		if (button.monthNum == SaveManager.BirthDateMonth)
		//		{
		//			button.Text.Color = Color.green;
		//		}
		//		break;
		//	}
		//	button.transform.localPosition = localPosition;
		//	localPosition.y -= this.ButtonHeight;
		//}
		//this.ButtonParent.YBounds.max = (float)array.Length * this.ButtonHeight - 2f * this.ButtonStart - 0.1f;
		//this.OnEnable();
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00005294 File Offset: 0x00003494
	public void SetData(NumberButton selected)
	{
		this.parent.SetValue(selected.Text.Text);
		for (int i = 0; i < this.AllButtons.Length; i++)
		{
			this.AllButtons[i].Text.Color = Color.white;
		}
		selected.Text.Color = Color.green;
		switch (this.dateType)
		{
		case NumberSetter.DateType.DAYS:
			SaveManager.BirthDateDay = int.Parse(selected.Text.Text);
			break;
		case NumberSetter.DateType.YEARS:
			SaveManager.BirthDateYear = int.Parse(selected.Text.Text);
			break;
		case NumberSetter.DateType.MONTHS:
			SaveManager.BirthDateMonth = selected.monthNum;
			break;
		}
		this.parent.Close();
	}

	// Token: 0x040000B8 RID: 184
	public NumberButton ButtonPrefab;

	// Token: 0x040000B9 RID: 185
	public Scroller ButtonParent;

	// Token: 0x040000BA RID: 186
	public float ButtonStart = 0.5f;

	// Token: 0x040000BB RID: 187
	public float ButtonHeight = 0.5f;

	// Token: 0x040000BC RID: 188
	private NumberButton[] AllButtons;

	// Token: 0x040000BD RID: 189
	public NumberMenu parent;

	// Token: 0x040000BE RID: 190
	public NumberSetter.DateType dateType = NumberSetter.DateType.MONTHS;

	// Token: 0x040000BF RID: 191
	private List<UiElement> selectableObjects = new List<UiElement>();

	// Token: 0x040000C0 RID: 192
	public UiElement backButton;

	// Token: 0x020002C7 RID: 711
	public enum DateType
	{
		// Token: 0x0400163C RID: 5692
		DAYS,
		// Token: 0x0400163D RID: 5693
		YEARS,
		// Token: 0x0400163E RID: 5694
		MONTHS
	}
}
