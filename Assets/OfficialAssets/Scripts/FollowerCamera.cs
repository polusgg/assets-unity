using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// Token: 0x02000097 RID: 151
public class FollowerCamera : MonoBehaviour
{
	// Token: 0x06000392 RID: 914 RVA: 0x00017DAC File Offset: 0x00015FAC
	public void Update()
	{
		//if (this.Target && !this.Locked)
		//{
		//	base.transform.position = Vector3.Lerp(base.transform.position, this.Target.transform.position + this.Offset, 5f * Time.deltaTime);
		//	if (this.shakeAmount > 0f)
		//	{
		//		float num = Mathf.PerlinNoise(0.5f, Time.time * this.shakePeriod) * 2f - 1f;
		//		float num2 = Mathf.PerlinNoise(Time.time * this.shakePeriod, 0.5f) * 2f - 1f;
		//		base.transform.Translate(num * this.shakeAmount, num2 * this.shakeAmount, 0f);
		//	}
		//}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00017E8D File Offset: 0x0001608D
	public void ShakeScreen(float duration, float severity)
	{
		base.StartCoroutine(this.CoShakeScreen(duration, severity));
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00017E9E File Offset: 0x0001609E
	private IEnumerator CoShakeScreen(float duration, float severity)
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();
		for (float t = duration; t > 0f; t -= Time.fixedDeltaTime)
		{
			float num = t / duration;
			this.Offset = Random.insideUnitCircle * num * severity;
			yield return wait;
		}
		this.Offset = Vector2.zero;
		yield break;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00017EBB File Offset: 0x000160BB
	internal void SetTarget(MonoBehaviour target)
	{
		this.Target = target;
		this.SnapToTarget();
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00017ECA File Offset: 0x000160CA
	public void SnapToTarget()
	{
		base.transform.position = this.Target.transform.position;// + this.Offset;
	}

	// Token: 0x04000439 RID: 1081
	public MonoBehaviour Target;

	// Token: 0x0400043A RID: 1082
	public Vector2 Offset;

	// Token: 0x0400043B RID: 1083
	public bool Locked;

	// Token: 0x0400043C RID: 1084
	public float shakeAmount;

	// Token: 0x0400043D RID: 1085
	public float shakePeriod = 1f;
}
