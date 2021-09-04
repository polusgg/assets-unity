using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class FingerBehaviour : MonoBehaviour
{
	// Token: 0x06000415 RID: 1045 RVA: 0x0001AEF2 File Offset: 0x000190F2
	public IEnumerator DoClick(float duration)
	{
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			float num = time / duration;
			if (num < 0.4f)
			{
				float num2 = num / 0.4f;
				num2 = num2 * 2f - 1f;
				if (num2 < 0f)
				{
					float fingerAngle = Mathf.Lerp(this.liftedAngle, this.liftedAngle * 2f, 1f + Mathf.Abs(num2));
					this.SetFingerAngle(fingerAngle);
				}
				else
				{
					float fingerAngle2 = Mathf.Lerp(this.liftedAngle * 2f, 0f, num2);
					this.SetFingerAngle(fingerAngle2);
				}
			}
			else if (num < 0.7f)
			{
				this.ClickOn();
			}
			else
			{
				float num3 = (num - 0.7f) / 0.3f;
				this.Click.enabled = false;
				float fingerAngle3 = Mathf.Lerp(0f, this.liftedAngle, num3);
				this.SetFingerAngle(fingerAngle3);
			}
			yield return null;
		}
		this.ClickOff();
		yield break;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0001AF08 File Offset: 0x00019108
	private void SetFingerAngle(float angle)
	{
		this.Finger.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0001AF2A File Offset: 0x0001912A
	public void ClickOff()
	{
		this.Click.enabled = false;
		this.SetFingerAngle(this.liftedAngle);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0001AF44 File Offset: 0x00019144
	public void ClickOn()
	{
		this.Click.enabled = true;
		this.SetFingerAngle(0f);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001AF5D File Offset: 0x0001915D
	public IEnumerator MoveTo(Vector2 target, float duration)
	{
		Vector3 startPos = base.transform.position;
		Vector3 targetPos = target;
		targetPos.z = startPos.z;
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			float num = time / duration;
			base.transform.position = Vector3.Lerp(startPos, targetPos, num);
			yield return null;
		}
		base.transform.position = targetPos;
		yield break;
	}

	// Token: 0x040004D4 RID: 1236
	public SpriteRenderer Finger;

	// Token: 0x040004D5 RID: 1237
	public SpriteRenderer Click;

	// Token: 0x040004D6 RID: 1238
	public float liftedAngle = -20f;

	// Token: 0x02000344 RID: 836
	public static class Quadratic
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x0006CEB4 File Offset: 0x0006B0B4
		public static float InOut(float k)
		{
			if (k < 0f)
			{
				k = 0f;
			}
			if (k > 1f)
			{
				k = 1f;
			}
			if ((k *= 2f) < 1f)
			{
				return 0.5f * k * k;
			}
			return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
		}
	}
}
