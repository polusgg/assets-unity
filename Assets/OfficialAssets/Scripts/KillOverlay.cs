using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class KillOverlay : MonoBehaviour
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001E634 File Offset: 0x0001C834
	public bool IsOpen
	{
		get
		{
			return this.showAll != null || this.queue.Count > 0;
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0001E64E File Offset: 0x0001C84E
	public IEnumerator WaitForFinish()
	{
		while (this.showAll != null || this.queue.Count > 0)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0001E660 File Offset: 0x0001C860
	public void ShowOne(GameData.PlayerInfo killer, GameData.PlayerInfo victim)
	{
		IEnumerable<OverlayKillAnimation> killAnims = this.KillAnims;
		if (killer.Object)
		{
			SkinLayer skin = killer.Object.MyPhysics.Skin;
			if (skin.skin && skin.skin.KillAnims.Length != 0)
			{
				killAnims = skin.skin.KillAnims;
			}
		}
		this.queue.Enqueue(() => this.CoShowOne(killAnims.Random<OverlayKillAnimation>(), killer, victim));
		if (this.showAll == null)
		{
			this.showAll = base.StartCoroutine(this.ShowAll());
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0001E71C File Offset: 0x0001C91C
	public void ShowOne(OverlayKillAnimation killAnimPrefab, GameData.PlayerInfo killer, GameData.PlayerInfo victim)
	{
		this.queue.Enqueue(() => this.CoShowOne(killAnimPrefab, killer, victim));
		if (this.showAll == null)
		{
			this.showAll = base.StartCoroutine(this.ShowAll());
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0001E77C File Offset: 0x0001C97C
	private IEnumerator ShowAll()
	{
		while (this.queue.Count > 0 || this.showOne != null)
		{
			if (this.showOne == null)
			{
				this.showOne = base.StartCoroutine(this.queue.Dequeue()());
			}
			yield return null;
		}
		this.showAll = null;
		yield break;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0001E78B File Offset: 0x0001C98B
	private IEnumerator CoShowOne(OverlayKillAnimation killAnimPrefab, GameData.PlayerInfo killer, GameData.PlayerInfo victim)
	{
		//OverlayKillAnimation overlayKillAnimation = Object.Instantiate<OverlayKillAnimation>(killAnimPrefab, base.transform);
		//overlayKillAnimation.Begin(killer, victim);
		//overlayKillAnimation.gameObject.SetActive(false);
		//yield return this.CoShowOne(overlayKillAnimation);
		yield break;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0001E7AF File Offset: 0x0001C9AF
	private IEnumerator CoShowOne(OverlayKillAnimation anim)
	{
		//if (Constants.ShouldPlaySfx())
		//{
		//	SoundManager.Instance.PlaySound(anim.Stinger, false, 1f).volume = anim.StingerVolume;
		//}
		//WaitForSeconds wait = new WaitForSeconds(0.083333336f);
		//this.background.enabled = true;
		//yield return wait;
		//this.background.enabled = false;
		//this.flameParent.SetActive(true);
		//this.flameParent.transform.localScale = new Vector3(1f, 0.3f, 1f);
		//this.flameParent.transform.localEulerAngles = new Vector3(0f, 0f, 25f);
		//yield return wait;
		//this.flameParent.transform.localScale = new Vector3(1f, 0.5f, 1f);
		//this.flameParent.transform.localEulerAngles = new Vector3(0f, 0f, -15f);
		//yield return wait;
		//this.flameParent.transform.localScale = new Vector3(1f, 1f, 1f);
		//this.flameParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		//anim.gameObject.SetActive(true);
		//yield return anim.WaitForFinish();
		//Object.Destroy(anim.gameObject);
		//yield return new WaitForLerp(0.16666667f, delegate(float t)
		//{
		//	this.flameParent.transform.localScale = new Vector3(1f, 1f - t, 1f);
		//});
		//this.flameParent.SetActive(false);
		//this.showOne = null;
		yield break;
	}

	// Token: 0x04000592 RID: 1426
	public SpriteRenderer background;

	// Token: 0x04000593 RID: 1427
	public GameObject flameParent;

	// Token: 0x04000594 RID: 1428
	public OverlayKillAnimation[] KillAnims;

	// Token: 0x04000595 RID: 1429
	public float FadeTime = 0.6f;

	// Token: 0x04000596 RID: 1430
	private Queue<Func<IEnumerator>> queue = new Queue<Func<IEnumerator>>();

	// Token: 0x04000597 RID: 1431
	private Coroutine showAll;

	// Token: 0x04000598 RID: 1432
	private Coroutine showOne;
}
