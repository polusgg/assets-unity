using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class StaticDoor : MonoBehaviour
{
	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000B91 RID: 2961 RVA: 0x000491CF File Offset: 0x000473CF
	// (set) Token: 0x06000B92 RID: 2962 RVA: 0x000491D7 File Offset: 0x000473D7
	public bool IsOpen { get; private set; }

	// Token: 0x06000B93 RID: 2963 RVA: 0x000491E0 File Offset: 0x000473E0
	public void SetOpen(bool isOpen)
	{
		this.IsOpen = isOpen;
		Collider2D component = base.GetComponent<Collider2D>();
		SpriteRenderer component2 = base.GetComponent<SpriteRenderer>();
		EdgeCollider2D componentInChildren = base.GetComponentInChildren<EdgeCollider2D>();
		if (isOpen)
		{
			component.enabled = false;
			component2.sprite = this.OpenDoorImage;
			if (componentInChildren)
			{
				componentInChildren.enabled = false;
				return;
			}
		}
		else
		{
			component.enabled = true;
			component2.sprite = this.CloseDoorImage;
			if (componentInChildren)
			{
				componentInChildren.enabled = true;
			}
		}
	}

	// Token: 0x04000CF8 RID: 3320
	public Sprite OpenDoorImage;

	// Token: 0x04000CF9 RID: 3321
	public Sprite CloseDoorImage;
}
