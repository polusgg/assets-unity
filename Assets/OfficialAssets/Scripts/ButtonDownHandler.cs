using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000D9 RID: 217
public class ButtonDownHandler : MonoBehaviour
{
	// Token: 0x06000555 RID: 1365 RVA: 0x00023E16 File Offset: 0x00022016
	public void Start()
	{
		base.GetComponent<PassiveButton>().OnClick.AddListener(new UnityAction(this.StartDown));
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00023E34 File Offset: 0x00022034
	public void OnDisable()
	{
		if (this.downState != null)
		{
			base.StopCoroutine(this.downState);
			this.downState = null;
			this.Target.sprite = this.UpSprite;
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00023E62 File Offset: 0x00022062
	private void StartDown()
	{
		if (this.downState == null)
		{
			this.downState = base.StartCoroutine(this.CoRunDown());
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00023E7E File Offset: 0x0002207E
	private IEnumerator CoRunDown()
	{
		this.Target.sprite = this.DownSprite;
		while (DestroyableSingleton<PassiveButtonManager>.Instance.controller.AnyTouch)
		{
			yield return null;
		}
		this.Target.sprite = this.UpSprite;
		this.downState = null;
		yield break;
	}

	// Token: 0x04000608 RID: 1544
	private Coroutine downState;

	// Token: 0x04000609 RID: 1545
	public SpriteRenderer Target;

	// Token: 0x0400060A RID: 1546
	public Sprite UpSprite;

	// Token: 0x0400060B RID: 1547
	public Sprite DownSprite;
}
