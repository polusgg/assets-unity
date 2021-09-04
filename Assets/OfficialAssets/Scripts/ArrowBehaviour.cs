using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class ArrowBehaviour : MonoBehaviour
{
	// Token: 0x06000D93 RID: 3475 RVA: 0x000522D3 File Offset: 0x000504D3
	public void Awake()
	{
		this.image = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x000522E4 File Offset: 0x000504E4
	public void Update()
	{
		//Camera main = Camera.main;
		//Vector2 vector = this.target - main.transform.position;
		//float num = vector.magnitude / (main.orthographicSize * this.perc);
		//this.image.enabled = ((double)num > 0.3);
		//Vector2 vector2 = main.WorldToViewportPoint(this.target);
		//if (this.Between(vector2.x, 0f, 1f) && this.Between(vector2.y, 0f, 1f))
		//{
		//	base.transform.position = this.target - vector.normalized * 0.6f;
		//	float num2 = Mathf.Clamp(num, 0f, 1f);
		//	base.transform.localScale = new Vector3(num2, num2, num2);
		//}
		//else
		//{
		//	Vector2 vector3;
		//	vector3..ctor(Mathf.Clamp(vector2.x * 2f - 1f, -1f, 1f), Mathf.Clamp(vector2.y * 2f - 1f, -1f, 1f));
		//	float orthographicSize = main.orthographicSize;
		//	float num3 = main.orthographicSize * main.aspect;
		//	Vector3 vector4;
		//	vector4..ctor(Mathf.LerpUnclamped(0f, num3 * 0.88f, vector3.x), Mathf.LerpUnclamped(0f, orthographicSize * 0.79f, vector3.y), 0f);
		//	base.transform.position = main.transform.position + vector4;
		//	base.transform.localScale = Vector3.one;
		//}
		//base.transform.LookAt2d(this.target);
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x000524BD File Offset: 0x000506BD
	private bool Between(float value, float min, float max)
	{
		return value > min && value < max;
	}

	// Token: 0x04001198 RID: 4504
	public Vector3 target;

	// Token: 0x04001199 RID: 4505
	public float perc = 0.925f;

	// Token: 0x0400119A RID: 4506
	[HideInInspector]
	public SpriteRenderer image;
}
