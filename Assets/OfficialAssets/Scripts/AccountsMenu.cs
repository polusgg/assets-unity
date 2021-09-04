using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class AccountsMenu : MonoBehaviour
{
	// Token: 0x0600003A RID: 58 RVA: 0x00002C93 File Offset: 0x00000E93
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002CA1 File Offset: 0x00000EA1
	public void SetValue(string val)
	{
		this.Close();
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002CA9 File Offset: 0x00000EA9
	public void Close()
	{
		base.gameObject.SetActive(false);
	}
}
