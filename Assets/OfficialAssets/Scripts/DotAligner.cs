using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class DotAligner : MonoBehaviour
{
	// Token: 0x06000334 RID: 820 RVA: 0x000150E7 File Offset: 0x000132E7
	public void Start()
	{
		DotAligner.Align(base.transform, this.Width, this.Even);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00015100 File Offset: 0x00013300
	public static void Align(Transform target, float width, bool even)
	{
		int num = 0;
		for (int i = 0; i < target.childCount; i++)
		{
			if (target.GetChild(i).gameObject.activeSelf)
			{
				num++;
			}
		}
		float num2;
		float num3;
		if (even)
		{
			num2 = -width * (float)(num - 1) / 2f;
			num3 = width;
		}
		else if (num > 1)
		{
			num2 = -width / 2f;
			num3 = width / (float)(num - 1);
		}
		else
		{
			num2 = 0f;
			num3 = 0f;
		}
		int num4 = 0;
		for (int j = 0; j < target.childCount; j++)
		{
			Transform child = target.GetChild(j);
			if (child.gameObject.activeSelf)
			{
				child.transform.localPosition = new Vector3(num2 + (float)num4 * num3, 0f, 0f);
				num4++;
			}
		}
	}

	// Token: 0x040003A8 RID: 936
	public float Width = 2f;

	// Token: 0x040003A9 RID: 937
	public bool Even;
}
