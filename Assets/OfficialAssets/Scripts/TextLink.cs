using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class TextLink : MonoBehaviour
{
	// Token: 0x060002C3 RID: 707 RVA: 0x00011C94 File Offset: 0x0000FE94
	public void Set(Vector2 from, Vector2 to, string target)
	{
		this.targetUrl = target;
		Vector2 vector = to + from;
		base.transform.localPosition = new Vector3(vector.x / 2f, vector.y / 2f, -1f);
		vector = to - from;
		vector.y = -vector.y;
		this.boxCollider.size = vector;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00011CFF File Offset: 0x0000FEFF
	public void Click()
	{
		if (this.targetUrl == "httpstore")
		{
			DestroyableSingleton<StoreMenu>.Instance.Open();
			return;
		}
		Application.OpenURL(this.targetUrl);
	}

	// Token: 0x04000339 RID: 825
	public BoxCollider2D boxCollider;

	// Token: 0x0400033A RID: 826
	public string targetUrl;

	// Token: 0x0400033B RID: 827
	public bool needed;
}
