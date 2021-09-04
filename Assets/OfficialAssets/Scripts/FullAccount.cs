using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class FullAccount : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x00003A62 File Offset: 0x00001C62
	public void CanSetCustomName(bool canSetName)
	{
		this.randomizeNameButton.SetActive(!canSetName);
		this.editNameButton.SetActive(canSetName);
	}

	// Token: 0x0400003F RID: 63
	[SerializeField]
	private GameObject randomizeNameButton;

	// Token: 0x04000040 RID: 64
	[SerializeField]
	private GameObject editNameButton;
}
