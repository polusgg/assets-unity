using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class KeySlotBehaviour : MonoBehaviour
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x0003298D File Offset: 0x00030B8D
	internal void SetFinished()
	{
		this.Image.sprite = this.Finished;
		base.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x000329BF File Offset: 0x00030BBF
	internal void SetInserted()
	{
		this.Image.sprite = this.Inserted;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x000329D2 File Offset: 0x00030BD2
	internal void SetHighlight()
	{
		this.Image.sprite = this.Highlit;
	}

	// Token: 0x0400091B RID: 2331
	public Sprite Highlit;

	// Token: 0x0400091C RID: 2332
	public Sprite Inserted;

	// Token: 0x0400091D RID: 2333
	public Sprite Finished;

	// Token: 0x0400091E RID: 2334
	public SpriteRenderer Image;

	// Token: 0x0400091F RID: 2335
	public BoxCollider2D Hitbox;
}
