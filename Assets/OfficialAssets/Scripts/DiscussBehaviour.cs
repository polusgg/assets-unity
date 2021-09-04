using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class DiscussBehaviour : MonoBehaviour
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x000252F2 File Offset: 0x000234F2
	public IEnumerator PlayAnimation()
	{
		this.Text.transform.localPosition = this.TextTarget.min;
		yield return this.AnimateText();
		yield return ShhhBehaviour.WaitWithInterrupt(this.HoldDuration);
		yield break;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00025304 File Offset: 0x00023504
	public void Update()
	{
		this.vec.Set(0f, 0f, this.RotateRange.Lerp(Mathf.PerlinNoise(1f, Time.time * 8f)));
		this.LeftPlayer.transform.eulerAngles = this.vec;
		this.vec.Set(0f, 0f, this.RotateRange.Lerp(Mathf.PerlinNoise(2f, Time.time * 8f)));
		this.RightPlayer.transform.eulerAngles = this.vec;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x000253A7 File Offset: 0x000235A7
	private IEnumerator AnimateText()
	{
		for (float t = 0f; t < this.Delay; t += Time.deltaTime)
		{
			yield return null;
		}
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.TextDuration; t += Time.deltaTime)
		{
			float num = t / this.TextDuration;
			this.UpdateText(ref vec, this.TextEasing.Evaluate(num));
			yield return null;
		}
		this.UpdateText(ref vec, 1f);
		yield break;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x000253B6 File Offset: 0x000235B6
	private void UpdateText(ref Vector3 vec, float p)
	{
		this.TextTarget.LerpUnclamped(ref vec, p, -7f);
		this.Text.transform.localPosition = vec;
	}

	// Token: 0x04000642 RID: 1602
	public SpriteRenderer LeftPlayer;

	// Token: 0x04000643 RID: 1603
	public SpriteRenderer RightPlayer;

	// Token: 0x04000644 RID: 1604
	public SpriteRenderer Text;

	// Token: 0x04000645 RID: 1605
	public FloatRange RotateRange = new FloatRange(-5f, 5f);

	// Token: 0x04000646 RID: 1606
	public Vector2Range TextTarget;

	// Token: 0x04000647 RID: 1607
	public AnimationCurve TextEasing;

	// Token: 0x04000648 RID: 1608
	public float Delay = 0.1f;

	// Token: 0x04000649 RID: 1609
	public float TextDuration = 0.5f;

	// Token: 0x0400064A RID: 1610
	public float HoldDuration = 2f;

	// Token: 0x0400064B RID: 1611
	private Vector3 vec;
}
