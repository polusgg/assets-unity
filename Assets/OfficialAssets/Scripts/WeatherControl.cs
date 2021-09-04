using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class WeatherControl : MonoBehaviour
{
	// Token: 0x06000821 RID: 2081 RVA: 0x0003539C File Offset: 0x0003359C
	internal void SetInactive()
	{
		this.Light.sprite = this.lightOff;
		this.Label.Color = new Color32(146, 135, 163, byte.MaxValue);
		base.StartCoroutine(this.Run());
		this.Switch.flipX = true;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x000353FC File Offset: 0x000335FC
	public void SetActive()
	{
		base.StopAllCoroutines();
		this.Label.Color = new Color32(81, 53, 115, byte.MaxValue);
		this.Background.sprite = this.backgroundLight;
		this.Light.sprite = this.lightOn;
		this.Switch.flipX = false;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0003545D File Offset: 0x0003365D
	private IEnumerator Run()
	{
		for (;;)
		{
			this.Background.sprite = this.backgroundDark;
			yield return Effects.Wait(0.5f);
			this.Background.sprite = this.backgroundLight;
			yield return Effects.Wait(0.5f);
		}
	}

	// Token: 0x04000996 RID: 2454
	public Sprite backgroundLight;

	// Token: 0x04000997 RID: 2455
	public Sprite backgroundDark;

	// Token: 0x04000998 RID: 2456
	public Sprite lightOff;

	// Token: 0x04000999 RID: 2457
	public Sprite lightOn;

	// Token: 0x0400099A RID: 2458
	public SpriteRenderer Background;

	// Token: 0x0400099B RID: 2459
	public SpriteRenderer Switch;

	// Token: 0x0400099C RID: 2460
	public SpriteRenderer Light;

	// Token: 0x0400099D RID: 2461
	public TextRenderer Label;
}
