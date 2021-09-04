using System;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class SetNameText : MonoBehaviour
{
	// Token: 0x06000AC1 RID: 2753 RVA: 0x00043F19 File Offset: 0x00042119
	private void Start()
	{
		this.nameText.Text = SaveManager.PlayerName;
	}

	// Token: 0x04000C13 RID: 3091
	[SerializeField]
	private TextRenderer nameText;
}
