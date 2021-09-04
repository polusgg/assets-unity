using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class PulseWaitingText : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x00003D52 File Offset: 0x00001F52
	private void Awake()
	{
		base.StartCoroutine(this.GetBig());
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00003D61 File Offset: 0x00001F61
	private IEnumerator GetBig()
	{
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.duration; t += Time.deltaTime)
		{
			float num = t / this.duration;
			float num2 = Mathf.SmoothStep(1f, 1.25f, num);
			vec.Set(num2, num2, num2);
			base.transform.localScale = vec;
			yield return null;
		}
		vec.Set(0f, 0f, 0f);
		base.transform.localScale = vec;
		base.StartCoroutine(this.GetSmall());
		yield break;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00003D70 File Offset: 0x00001F70
	private IEnumerator GetSmall()
	{
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.duration; t += Time.deltaTime)
		{
			float num = t / this.duration;
			float num2 = Mathf.SmoothStep(1.25f, 1f, num);
			vec.Set(num2, num2, num2);
			base.transform.localScale = vec;
			yield return null;
		}
		vec.Set(1f, 1f, 1f);
		base.transform.localScale = vec;
		base.StartCoroutine(this.GetBig());
		yield break;
	}

	// Token: 0x04000056 RID: 86
	public Transform textSource;

	// Token: 0x04000057 RID: 87
	private float duration = 1f;
}
