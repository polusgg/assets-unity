using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class Scene0Controller : SceneController
{
	// Token: 0x06000427 RID: 1063 RVA: 0x0001B2D4 File Offset: 0x000194D4
	public void OnEnable()
	{
		base.StartCoroutine(this.Run());
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001B2E4 File Offset: 0x000194E4
	public void OnDisable()
	{
		for (int i = 0; i < this.ExtraBoys.Length; i++)
		{
			this.ExtraBoys[i].enabled = false;
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0001B312 File Offset: 0x00019512
	private IEnumerator Run()
	{
		int lastBoy = 0;
		float start = Time.time;
		for (;;)
		{
			float num = (Time.time - start) / this.Duration;
			int num2 = Mathf.RoundToInt((Mathf.Cos(3.1415927f * num + 3.1415927f) + 1f) / 2f * (float)this.ExtraBoys.Length);
			if (lastBoy < num2)
			{
				base.StartCoroutine(this.PopIn(this.ExtraBoys[lastBoy]));
				lastBoy = num2;
			}
			else if (lastBoy > num2)
			{
				lastBoy = num2;
				base.StartCoroutine(this.PopOut(this.ExtraBoys[lastBoy]));
			}
			yield return null;
		}
	//	yield break;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0001B321 File Offset: 0x00019521
	private IEnumerator PopIn(SpriteRenderer boy)
	{
		boy.enabled = true;
		for (float timer = 0f; timer < 0.2f; timer += Time.deltaTime)
		{
			float num = this.PopInCurve.Evaluate(timer / 0.2f);
			boy.transform.localScale = new Vector3(num, num, num);
			yield return null;
		}
		boy.transform.localScale = Vector3.one;
		yield break;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0001B337 File Offset: 0x00019537
	private IEnumerator PopOut(SpriteRenderer boy)
	{
		boy.enabled = true;
		for (float timer = 0f; timer < this.OutDuration; timer += Time.deltaTime)
		{
			float num = this.PopOutCurve.Evaluate(timer / this.OutDuration);
			boy.transform.localScale = new Vector3(num, num, num);
			yield return null;
		}
		boy.transform.localScale = Vector3.one;
		boy.enabled = false;
		yield break;
	}

	// Token: 0x040004EA RID: 1258
	public float Duration = 3f;

	// Token: 0x040004EB RID: 1259
	public SpriteRenderer[] ExtraBoys;

	// Token: 0x040004EC RID: 1260
	public AnimationCurve PopInCurve;

	// Token: 0x040004ED RID: 1261
	public AnimationCurve PopOutCurve;

	// Token: 0x040004EE RID: 1262
	public float OutDuration = 0.2f;
}
