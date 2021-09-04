using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class AccountSignInList : MonoBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x000026DC File Offset: 0x000008DC
	public void Start()
	{
		//Collider2D component = this.ButtonParent.GetComponent<Collider2D>();
		//string[] array = new string[]
		//{
		//	"Steam",
		//	"Epic",
		//	"Apple",
		//	"Google"
		//};
		//Vector3 localPosition;
		//localPosition..ctor(0f, this.ButtonStart, -0.5f);
		//this.AllButtons = new AccountButton[array.Length];
		//for (int i = 0; i < array.Length; i++)
		//{
		//	AccountButton button = Object.Instantiate<AccountButton>(this.ButtonPrefab, this.ButtonParent.Inner);
		//	this.AllButtons[i] = button;
		//	button.Text.Text = array[i];
		//	if (!this.createAccount)
		//	{
		//		button.Button.OnClick.AddListener(delegate()
		//		{
		//			this.LogInWith(button);
		//		});
		//	}
		//	button.Button.ClickMask = component;
		//	button.transform.localPosition = localPosition;
		//	localPosition.y -= this.ButtonHeight;
		//	this.controllerNavParent.ControllerSelectable.Add(button.Button);
		//}
		//if (array.Length != 0)
		//{
		//	this.controllerNavParent.DefaultButtonSelected = this.AllButtons[0].Button;
		//}
		//this.ButtonParent.YBounds.max = (float)array.Length * this.ButtonHeight - 2f * this.ButtonStart - 0.1f;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000286A File Offset: 0x00000A6A
	public void LogInWith(AccountButton selected)
	{
	}

	// Token: 0x0400000E RID: 14
	public AccountButton ButtonPrefab;

	// Token: 0x0400000F RID: 15
	public Scroller ButtonParent;

	// Token: 0x04000010 RID: 16
	public float ButtonStart = 0.5f;

	// Token: 0x04000011 RID: 17
	public float ButtonHeight = 0.5f;

	// Token: 0x04000012 RID: 18
	private AccountButton[] AllButtons;

	// Token: 0x04000013 RID: 19
	public AccountsMenu parent;

	// Token: 0x04000014 RID: 20
	public ControllerNavMenu controllerNavParent;

	// Token: 0x04000015 RID: 21
	public bool createAccount;
}
