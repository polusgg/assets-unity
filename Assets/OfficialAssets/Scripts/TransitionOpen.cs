using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000ED RID: 237
public class TransitionOpen : MonoBehaviour
{
	// Token: 0x060005EC RID: 1516 RVA: 0x00026C33 File Offset: 0x00024E33
	public void OnEnable()
	{
		base.StartCoroutine(this.AnimateOpen());
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x00026C42 File Offset: 0x00024E42
	public void Toggle()
	{
		base.StopAllCoroutines();
		if (base.isActiveAndEnabled)
		{
			this.Close();
			return;
		}
		base.gameObject.SetActive(true);
		this.OnEnable();
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00026C6B File Offset: 0x00024E6B
	public void Close()
	{
		if (base.isActiveAndEnabled)
		{
			base.StartCoroutine(this.AnimateClose());
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00026C82 File Offset: 0x00024E82
	private IEnumerator AnimateClose()
	{
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.duration; t += Time.deltaTime)
		{
			float num = t / this.duration;
			float num2 = Mathf.SmoothStep(1f, 0f, num);
			vec.Set(num2, num2, num2);
			base.transform.localScale = vec;
			yield return null;
		}
		vec.Set(0f, 0f, 0f);
		base.transform.localScale = vec;
		this.OnClose.Invoke();
		yield break;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00026C91 File Offset: 0x00024E91
	private IEnumerator AnimateOpen()
	{
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.duration; t += Time.deltaTime)
		{
			float num = t / this.duration;
			float num2 = Mathf.SmoothStep(0f, 1f, num);
			vec.Set(num2, num2, num2);
			base.transform.localScale = vec;
			yield return null;
		}
		vec.Set(1f, 1f, 1f);
		base.transform.localScale = vec;
		yield break;
	}

	// Token: 0x040006A5 RID: 1701
	public float duration = 0.2f;

	// Token: 0x040006A6 RID: 1702
	public Button.ButtonClickedEvent OnClose = new Button.ButtonClickedEvent();
}
