using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
[RequireComponent(typeof(SpriteRenderer))]
public class ConditionalSpriteSetter : MonoBehaviour
{
	// Token: 0x0600084E RID: 2126 RVA: 0x00035E7C File Offset: 0x0003407C
	private void Start()
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		if (component && (this.sprite.Select() || !this.ignoreIfNoSpriteForPlatform))
		{
			component.sprite = this.sprite;
		}
	}

	// Token: 0x040009BA RID: 2490
	public bool ignoreIfNoSpriteForPlatform = true;

	// Token: 0x040009BB RID: 2491
	public ConditionalSprite sprite;
}
