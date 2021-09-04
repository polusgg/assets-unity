using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class ShhhBehaviour : MonoBehaviour
{
	// Token: 0x060005AC RID: 1452 RVA: 0x00025420 File Offset: 0x00023620
	public void OnEnable()
	{
		if (this.Autoplay)
		{
			Vector3 localScale = default(Vector3);
			this.UpdateHand(ref localScale, 1f);
			this.UpdateText(ref localScale, 1f);
			localScale.Set(1f, 1f, 1f);
			this.Body.transform.localScale = localScale;
			this.TextImage.color = Color.white;
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0002548E File Offset: 0x0002368E
	public IEnumerator PlayAnimation()
	{
		base.StartCoroutine(this.AnimateHand());
		yield return this.AnimateText();
		yield return ShhhBehaviour.WaitWithInterrupt(this.HoldDuration);
		yield break;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0002549D File Offset: 0x0002369D
	public void Update()
	{
		this.Background.transform.Rotate(0f, 0f, Time.deltaTime * this.RotateSpeed);
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000254C5 File Offset: 0x000236C5
	private IEnumerator AnimateText()
	{
		this.TextImage.color = Palette.ClearWhite;
		for (float t = 0f; t < this.Delay; t += Time.deltaTime)
		{
			yield return null;
		}
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.PulseDuration; t += Time.deltaTime)
		{
			float num = t / this.PulseDuration;
			float num2 = 1f + Mathf.Sin(3.1415927f * num) * this.PulseSize;
			vec.Set(num2, num2, 1f);
			this.Body.transform.localScale = vec;
			this.TextImage.color = Color.Lerp(Palette.ClearWhite, Palette.White, num * 2f);
			yield return null;
		}
		vec.Set(1f, 1f, 1f);
		this.Body.transform.localScale = vec;
		this.TextImage.color = Color.white;
		yield break;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x000254D4 File Offset: 0x000236D4
	private IEnumerator AnimateHand()
	{
		this.Hand.transform.localPosition = this.HandTarget.min;
		Vector3 vec = default(Vector3);
		for (float t = 0f; t < this.Duration; t += Time.deltaTime)
		{
			float p = t / this.Duration;
			this.UpdateHand(ref vec, p);
			yield return null;
		}
		this.UpdateHand(ref vec, 1f);
		yield break;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x000254E4 File Offset: 0x000236E4
	private void UpdateHand(ref Vector3 vec, float p)
	{
		this.HandTarget.LerpUnclamped(ref vec, this.PositionEasing.Evaluate(p), -1f);
		this.Hand.transform.localPosition = vec;
		vec.Set(0f, 0f, this.HandRotate.LerpUnclamped(this.RotationEasing.Evaluate(p)));
		this.Hand.transform.eulerAngles = vec;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00025561 File Offset: 0x00023761
	private void UpdateText(ref Vector3 vec, float p)
	{
		this.TextTarget.LerpUnclamped(ref vec, p, -2f);
		this.TextImage.transform.localPosition = vec;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0002558B File Offset: 0x0002378B
	public static IEnumerator WaitWithInterrupt(float duration)
	{
		float timer = 0f;
		while (timer < duration && !ShhhBehaviour.CheckForInterrupt())
		{
			yield return null;
			timer += Time.deltaTime;
		}
		yield break;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0002559A File Offset: 0x0002379A
	public static bool CheckForInterrupt()
	{
		return Input.anyKeyDown;
	}

	// Token: 0x0400064C RID: 1612
	public SpriteRenderer Background;

	// Token: 0x0400064D RID: 1613
	public SpriteRenderer Body;

	// Token: 0x0400064E RID: 1614
	public SpriteRenderer Hand;

	// Token: 0x0400064F RID: 1615
	public SpriteRenderer TextImage;

	// Token: 0x04000650 RID: 1616
	public float RotateSpeed = 15f;

	// Token: 0x04000651 RID: 1617
	public Vector2Range HandTarget;

	// Token: 0x04000652 RID: 1618
	public AnimationCurve PositionEasing;

	// Token: 0x04000653 RID: 1619
	public FloatRange HandRotate;

	// Token: 0x04000654 RID: 1620
	public AnimationCurve RotationEasing;

	// Token: 0x04000655 RID: 1621
	public Vector2Range TextTarget;

	// Token: 0x04000656 RID: 1622
	public AnimationCurve TextEasing;

	// Token: 0x04000657 RID: 1623
	public float Duration = 0.5f;

	// Token: 0x04000658 RID: 1624
	public float Delay = 0.1f;

	// Token: 0x04000659 RID: 1625
	public float TextDuration = 0.5f;

	// Token: 0x0400065A RID: 1626
	public float PulseDuration = 0.1f;

	// Token: 0x0400065B RID: 1627
	public float PulseSize = 0.1f;

	// Token: 0x0400065C RID: 1628
	public float HoldDuration = 2f;

	// Token: 0x0400065D RID: 1629
	public bool Autoplay;
}
