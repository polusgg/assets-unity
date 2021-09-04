using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200004F RID: 79
public static class GameObjectExtensions
{
	// Token: 0x06000233 RID: 563 RVA: 0x0000F550 File Offset: 0x0000D750
	public static void SetAllGameObjectsActive<T>(this IList<T> self, bool isActive) where T : MonoBehaviour
	{
		for (int i = 0; i < self.Count; i++)
		{
			self[i].gameObject.SetActive(isActive);
		}
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000F588 File Offset: 0x0000D788
	public static T Find<T>(this List<T> self, GameObject toFind) where T : MonoBehaviour
	{
		for (int i = 0; i < self.Count; i++)
		{
			T t = self[i];
			if (t.gameObject == toFind)
			{
				return t;
			}
		}
		return default(T);
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000F5CC File Offset: 0x0000D7CC
	public static void SetWorldZ(this Transform self, float z)
	{
		Vector3 position = self.position;
		position.z = z;
		self.position = position;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
	public static void SetLocalY(this Transform self, float y)
	{
		Vector3 localPosition = self.localPosition;
		localPosition.y = y;
		self.localPosition = localPosition;
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000F614 File Offset: 0x0000D814
	public static void SetLocalZ(this Transform self, float z)
	{
		Vector3 localPosition = self.localPosition;
		localPosition.z = z;
		self.localPosition = localPosition;
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000F638 File Offset: 0x0000D838
	public static void LookAt2d(this Transform self, Vector3 target)
	{
		Vector3 vector = target - self.transform.position;
		vector.Normalize();
		float num = Mathf.Atan2(vector.y, vector.x);
		if (self.transform.lossyScale.x < 0f)
		{
			num += 3.1415927f;
		}
		self.transform.rotation = Quaternion.Euler(0f, 0f, num * 57.29578f);
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
	public static void LookAt2d(this Transform self, Transform target)
	{
		self.LookAt2d(target.transform.position);
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
	public static void DestroyChildren(this Transform self)
	{
		for (int i = self.childCount - 1; i > -1; i--)
		{
			Transform child = self.GetChild(i);
			child.transform.SetParent(null);
            UnityEngine.Object.Destroy(child.gameObject);
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000F704 File Offset: 0x0000D904
	public static void DestroyChildren(this MonoBehaviour self)
	{
		for (int i = self.transform.childCount - 1; i > -1; i--)
		{
			Object.Destroy(self.transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000F740 File Offset: 0x0000D940
	public static void ForEachChild(this GameObject self, Action<GameObject> todo)
	{
		for (int i = self.transform.childCount - 1; i > -1; i--)
		{
			todo(self.transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000F77C File Offset: 0x0000D97C
	public static void ForEachChildBehavior<T>(this MonoBehaviour self, Action<T> todo) where T : MonoBehaviour
	{
		for (int i = self.transform.childCount - 1; i > -1; i--)
		{
			T component = self.transform.GetChild(i).GetComponent<T>();
			if (component)
			{
				todo(component);
			}
		}
	}
}
