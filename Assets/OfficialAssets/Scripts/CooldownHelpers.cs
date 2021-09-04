using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public static class CooldownHelpers
{
	// Token: 0x060004BF RID: 1215 RVA: 0x0001E308 File Offset: 0x0001C508
	public static void SetCooldownNormalizedUvs(this SpriteRenderer myRend)
	{
		//if (!myRend.sprite)
		//{
		//	return;
		//}
		//Vector2[] uv = myRend.sprite.uv;
		//Vector4 vector;
		//vector..ctor(2f, -1f, 2f, -1f);
		//for (int i = 0; i < uv.Length; i++)
		//{
		//	if (vector.x > uv[i].x)
		//	{
		//		vector.x = uv[i].x;
		//	}
		//	if (vector.y < uv[i].x)
		//	{
		//		vector.y = uv[i].x;
		//	}
		//	if (vector.z > uv[i].y)
		//	{
		//		vector.z = uv[i].y;
		//	}
		//	if (vector.w < uv[i].y)
		//	{
		//		vector.w = uv[i].y;
		//	}
		//}
		//vector.y -= vector.x;
		//vector.w -= vector.z;
		//myRend.material.SetVector("_NormalizedUvs", vector);
	}
}
