using System;
using PowerTools;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000D8 RID: 216
public class ButtonAnimRolloverHandler : MonoBehaviour
{
	// Token: 0x06000551 RID: 1361 RVA: 0x00023D5C File Offset: 0x00021F5C
	public void Start()
	{
		this.target = base.GetComponent<SpriteRenderer>();
		this.animTarget = base.GetComponent<SpriteAnim>();
		PassiveButton component = base.GetComponent<PassiveButton>();
		component.OnMouseOver.AddListener(new UnityAction(this.DoMouseOver));
		component.OnMouseOut.AddListener(new UnityAction(this.DoMouseOut));
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00023DB4 File Offset: 0x00021FB4
	public void DoMouseOver()
	{
		this.animTarget.Play(this.RolloverAnim, 1f);
		if (this.HoverSound)
		{
			SoundManager.Instance.PlaySound(this.HoverSound, false, 1f);
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00023DF0 File Offset: 0x00021FF0
	public void DoMouseOut()
	{
		this.animTarget.Stop();
		this.target.sprite = this.StaticOutImage;
	}

	// Token: 0x04000603 RID: 1539
	public Sprite StaticOutImage;

	// Token: 0x04000604 RID: 1540
	public AnimationClip RolloverAnim;

	// Token: 0x04000605 RID: 1541
	public AudioClip HoverSound;

	// Token: 0x04000606 RID: 1542
	private SpriteRenderer target;

	// Token: 0x04000607 RID: 1543
	private SpriteAnim animTarget;
}
