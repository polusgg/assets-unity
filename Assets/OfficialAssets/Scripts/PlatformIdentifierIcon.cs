using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
[RequireComponent(typeof(SpriteRenderer))]
public class PlatformIdentifierIcon : MonoBehaviour
{
	// Token: 0x060008C6 RID: 2246 RVA: 0x00038FFF File Offset: 0x000371FF
	public void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.sr.enabled = false;
		this.tr = base.GetComponentInParent<TextRenderer>();
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00039028 File Offset: 0x00037228
	public void SetIcon(RuntimePlatform platform)
	{
		this.sr.enabled = false;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00039044 File Offset: 0x00037244
	[ContextMenu("Update pos")]
	public void UpdatePosition()
	{
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x = -this.tr.Width - base.transform.lossyScale.x * this.sr.sprite.rect.width / this.sr.sprite.pixelsPerUnit;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x000390B8 File Offset: 0x000372B8
	private Sprite GetPrevalidationIcon(RuntimePlatform platform)
	{
		//if (platform <= 11)
		//{
		//	if (platform == 8 || platform == 11)
		//	{
		//		return this.mobile;
		//	}
		//}
		//else
		//{
		//	if (platform == 25)
		//	{
		//		return this.ps4;
		//	}
		//	if (platform == 27)
		//	{
		//		return this.xbox;
		//	}
		//	if (platform == 32)
		//	{
		//		return this.nSwitch;
		//	}
		//}
		//return this.pc;
		return null;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00039106 File Offset: 0x00037306
	private Sprite ValidateIcon(Sprite icon)
	{
		if (icon == this.ps4 || icon == this.xbox || icon == this.nSwitch)
		{
			return this.genericConsole;
		}
		return icon;
	}

	// Token: 0x04000A1B RID: 2587
	private SpriteRenderer sr;

	// Token: 0x04000A1C RID: 2588
	private TextRenderer tr;

	// Token: 0x04000A1D RID: 2589
	public bool isOnHUD;

	// Token: 0x04000A1E RID: 2590
	public Sprite genericConsole;

	// Token: 0x04000A1F RID: 2591
	public Sprite mobile;

	// Token: 0x04000A20 RID: 2592
	public Sprite pc;

	// Token: 0x04000A21 RID: 2593
	public Sprite nSwitch;

	// Token: 0x04000A22 RID: 2594
	public Sprite ps4;

	// Token: 0x04000A23 RID: 2595
	public Sprite xbox;
}
