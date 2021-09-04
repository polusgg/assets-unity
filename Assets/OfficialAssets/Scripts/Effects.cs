using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004D RID: 77
public static class Effects
{
	// Token: 0x060001E9 RID: 489 RVA: 0x0000E76E File Offset: 0x0000C96E
	public static IEnumerator Action(Action todo)
	{
		todo();
		yield break;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000E77D File Offset: 0x0000C97D
	public static IEnumerator Wait(float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000E78C File Offset: 0x0000C98C
	public static IEnumerator Sequence(params IEnumerator[] items)
	{
		int num;
		for (int i = 0; i < items.Length; i = num)
		{
			yield return items[i];
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000E79B File Offset: 0x0000C99B
	public static IEnumerator All(params IEnumerator[] items)
	{
		Stack<IEnumerator>[] enums = new Stack<IEnumerator>[items.Length];
		for (int i = 0; i < items.Length; i++)
		{
			enums[i] = new Stack<IEnumerator>();
			enums[i].Push(items[i]);
		}
		int num;
		for (int cap = 0; cap < 100000; cap = num)
		{
			bool flag = false;
			for (int j = 0; j < enums.Length; j++)
			{
				if (enums[j].Count > 0)
				{
					flag = true;
					IEnumerator enumerator = enums[j].Peek();
					if (enumerator.MoveNext())
					{
						if (enumerator.Current is IEnumerator)
						{
							enums[j].Push((IEnumerator)enumerator.Current);
						}
					}
					else
					{
						enums[j].Pop();
					}
				}
			}
			if (!flag)
			{
				break;
			}
			yield return null;
			num = cap + 1;
		}
		yield break;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000E7AA File Offset: 0x0000C9AA
	internal static IEnumerator Lerp(float duration, Action<float> action)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			action(t / duration);
			yield return null;
		}
		action(1f);
		yield break;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
	internal static IEnumerator Overlerp(float duration, Action<float> action, float overextend = 0.05f)
	{
		float d = duration * 0.95f;
		for (float t = 0f; t < d; t += Time.deltaTime)
		{
			action(Mathf.Lerp(0f, 1f + overextend, t / d));
			yield return null;
		}
		float d2 = duration * 0.050000012f;
		for (float t = 0f; t < d2; t += Time.deltaTime)
		{
			action(Mathf.Lerp(1f + overextend, 1f, t / d2));
			yield return null;
		}
		action(1f);
		yield break;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000E7DD File Offset: 0x0000C9DD
	internal static IEnumerator ScaleIn(Transform self, float source, float target, float duration)
	{
		if (!self)
		{
			yield break;
		}
		Vector3 localScale = default(Vector3);
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			localScale.x = (localScale.y = (localScale.z = Mathf.SmoothStep(source, target, t / duration)));
			self.localScale = localScale;
			yield return null;
		}
		localScale.z = target;
		localScale.y = target;
		localScale.x = target;
		self.localScale = localScale;
		yield break;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000E801 File Offset: 0x0000CA01
	internal static IEnumerator CycleColors(SpriteRenderer self, Color source, Color target, float rate, float duration)
	{
		if (!self)
		{
			yield break;
		}
		self.enabled = true;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float num = Mathf.Sin(t * 3.1415927f / rate) / 2f + 0.5f;
			self.color = Color.Lerp(source, target, num);
			yield return null;
		}
		self.color = source;
		yield break;
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000E82D File Offset: 0x0000CA2D
	internal static IEnumerator PulseColor(SpriteRenderer self, Color source, Color target, float duration = 0.5f)
	{
		if (!self)
		{
			yield break;
		}
		self.enabled = true;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			self.color = Color.Lerp(target, source, t / duration);
			yield return null;
		}
		self.color = source;
		yield break;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000E851 File Offset: 0x0000CA51
	internal static IEnumerator PulseColor(TextRenderer self, Color source, Color target, float duration = 0.5f)
	{
		if (!self)
		{
			yield break;
		}
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			self.Color = Color.Lerp(target, source, t / duration);
			yield return null;
		}
		self.Color = source;
		yield break;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000E875 File Offset: 0x0000CA75
	public static IEnumerator ColorFade(TextRenderer self, Color source, Color target, float duration)
	{
		if (!self)
		{
			yield break;
		}
		self.enabled = true;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			self.Color = Color.Lerp(source, target, t / duration);
			yield return null;
		}
		self.Color = target;
		yield break;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000E899 File Offset: 0x0000CA99
	public static IEnumerator ColorFade(SpriteRenderer self, Color source, Color target, float duration)
	{
		if (!self)
		{
			yield break;
		}
		self.enabled = true;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			self.color = Color.Lerp(source, target, t / duration);
			yield return null;
		}
		self.color = target;
		yield break;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000E8BD File Offset: 0x0000CABD
	public static IEnumerator Rotate2D(Transform target, float source, float dest, float duration = 0.75f)
	{
		Vector3 temp = target.localEulerAngles;
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			float num = time / duration;
			temp.z = Mathf.SmoothStep(source, dest, num);
			target.localEulerAngles = temp;
			yield return null;
		}
		temp.z = dest;
		target.localEulerAngles = temp;
		yield break;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000E8E1 File Offset: 0x0000CAE1
	public static IEnumerator Slide3D(Transform target, Vector3 source, Vector3 dest, float duration = 0.75f)
	{
		Vector3 localPosition = default(Vector3);
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			float num = time / duration;
			localPosition.x = Mathf.SmoothStep(source.x, dest.x, num);
			localPosition.y = Mathf.SmoothStep(source.y, dest.y, num);
			localPosition.z = Mathf.Lerp(source.z, dest.z, num);
			target.localPosition = localPosition;
			yield return null;
		}
		target.localPosition = dest;
		yield break;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000E905 File Offset: 0x0000CB05
	public static IEnumerator Slide2D(Transform target, Vector2 source, Vector2 dest, float duration = 0.75f)
	{
		Vector3 temp = default(Vector3);
		temp.z = target.localPosition.z;
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			float num = time / duration;
			temp.x = Mathf.SmoothStep(source.x, dest.x, num);
			temp.y = Mathf.SmoothStep(source.y, dest.y, num);
			target.localPosition = temp;
			yield return null;
		}
		temp.x = dest.x;
		temp.y = dest.y;
		target.localPosition = temp;
		yield break;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000E929 File Offset: 0x0000CB29
	public static IEnumerator Slide2DWorld(Transform target, Vector2 source, Vector2 dest, float duration = 0.75f)
	{
		Vector3 temp = default(Vector3);
		temp.z = target.position.z;
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			float num = time / duration;
			temp.x = Mathf.SmoothStep(source.x, dest.x, num);
			temp.y = Mathf.SmoothStep(source.y, dest.y, num);
			target.position = temp;
			yield return null;
		}
		temp.x = dest.x;
		temp.y = dest.y;
		target.position = temp;
		yield break;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000E94D File Offset: 0x0000CB4D
	public static IEnumerator Bounce(Transform target, float duration = 0.3f, float height = 0.15f)
	{
		if (!target)
		{
			yield break;
		}
		Vector3 origin = target.localPosition;
		Vector3 temp = origin;
		for (float timer = 0f; timer < duration; timer += Time.deltaTime)
		{
			float num = timer / duration;
			float num2 = 1f - num;
			temp.y = origin.y + height * Mathf.Abs(Mathf.Sin(num * 3.1415927f * 3f)) * num2;
			if (!target)
			{
				yield break;
			}
			target.localPosition = temp;
			yield return null;
		}
		if (target)
		{
			target.transform.localPosition = origin;
		}
		yield break;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000E96A File Offset: 0x0000CB6A
	public static IEnumerator Shake(Transform target, float duration, float halfWidth, bool taper)
	{
		//Vector3 localPosition = target.localPosition;
		//for (float timer = 0f; timer < duration; timer += Time.deltaTime)
		//{
		//	float num = timer / duration;
		//	Vector3 vector = Random.insideUnitCircle * halfWidth;
		//	if (taper)
		//	{
		//		vector *= 1f - num;
		//	}
		//	target.localPosition += vector;
		//	yield return null;
		//}
		yield break;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000E98E File Offset: 0x0000CB8E
	public static IEnumerator SwayX(Transform target, float duration = 0.75f, float halfWidth = 0.25f)
	{
		if (Effects.activeShakes.Add(target))
		{
			Vector3 origin = target.localPosition;
			for (float timer = 0f; timer < duration; timer += Time.deltaTime)
			{
				float num = timer / duration;
				target.localPosition = origin + Vector3.right * (halfWidth * Mathf.Sin(num * 30f) * (1f - num));
				yield return null;
			}
			target.transform.localPosition = origin;
			Effects.activeShakes.Remove(target);
			origin = default(Vector3);
		}
		yield break;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000E9AB File Offset: 0x0000CBAB
	public static IEnumerator Bloop(float delay, Transform target, float finalSize = 1f, float duration = 0.5f)
	{
		for (float t = 0f; t < delay; t += Time.deltaTime)
		{
			yield return null;
		}
		Vector3 localScale = default(Vector3);
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float z = Effects.ElasticOut(t, duration) * finalSize;
			localScale.x = (localScale.y = (localScale.z = z));
			target.localScale = localScale;
			yield return null;
		}
		localScale.z = finalSize;
		localScale.y = finalSize;
		localScale.x = finalSize;
		target.localScale = localScale;
		yield break;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000E9CF File Offset: 0x0000CBCF
	public static IEnumerator ArcSlide(float duration, Transform target, Vector2 sourcePos, Vector2 targetPos, float anchorDistance)
	{
		Vector2 vector = (targetPos - sourcePos) / 2f;
		Vector2 anchor = sourcePos + vector + vector.Rotate(90f).normalized * anchorDistance;
		float z = target.localPosition.z;
		for (float timer = 0f; timer < duration; timer += Time.deltaTime)
		{
			Vector3 localPosition = Effects.Bezier(timer / duration, sourcePos, targetPos, anchor);
			localPosition.z = z;
			target.localPosition = localPosition;
			yield return null;
		}
		target.transform.localPosition = targetPos;
		yield break;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000E9FC File Offset: 0x0000CBFC
	public static Vector3 Bezier(float t, Vector3 src, Vector3 dest, Vector3 anchor)
	{
		t = Mathf.Clamp(t, 0f, 1f);
		float num = 1f - t;
		return num * num * src + 2f * num * t * anchor + t * t * dest;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000EA50 File Offset: 0x0000CC50
	public static Vector2 Bezier(float t, Vector2 src, Vector2 dest, Vector2 anchor)
	{
		t = Mathf.Clamp(t, 0f, 1f);
		float num = 1f - t;
		return num * num * src + 2f * num * t * anchor + t * t * dest;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
	private static float ElasticOut(float time, float duration)
	{
		time /= duration;
		float num = time * time;
		float num2 = num * time;
		return 33f * num2 * num + -106f * num * num + 126f * num2 + -67f * num + 15f * time;
	}

	// Token: 0x040002D8 RID: 728
	private static HashSet<Transform> activeShakes = new HashSet<Transform>();
}
