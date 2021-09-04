using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class Wire : MonoBehaviour
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000367 RID: 871 RVA: 0x0001661F File Offset: 0x0001481F
	// (set) Token: 0x06000368 RID: 872 RVA: 0x00016627 File Offset: 0x00014827
	public Vector2 BaseWorldPos { get; internal set; }

	// Token: 0x06000369 RID: 873 RVA: 0x00016630 File Offset: 0x00014830
	public void Start()
	{
		this.BaseWorldPos = base.transform.position;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00016648 File Offset: 0x00014848
	public void ResetLine(Vector3 targetWorldPos, bool reset = false)
	{
		if (reset)
		{
			this.Liner.transform.localScale = new Vector3(0f, 0f, 0f);
			this.WireTip.transform.eulerAngles = Vector3.zero;
			this.WireTip.transform.position = base.transform.position;
			return;
		}
		Vector2 vector = targetWorldPos - base.transform.position;
		Vector2 normalized = vector.normalized;
		Vector3 localPosition = default(Vector3);
		localPosition = vector - normalized * 0.075f;
		localPosition.z = -0.01f;
		this.WireTip.transform.localPosition = localPosition;
		float magnitude = vector.magnitude;
		this.Liner.transform.localScale = new Vector3(magnitude, 1f, 1f);
		this.Liner.transform.localPosition = vector / 2f;
		this.WireTip.transform.LookAt2d(targetWorldPos);
		this.Liner.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector2.right, vector));
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0001678C File Offset: 0x0001498C
	public void ConnectRight(WireNode node)
	{
		Vector3 position = node.transform.position;
		this.ResetLine(position, false);
	}

	// Token: 0x0600036C RID: 876 RVA: 0x000167AD File Offset: 0x000149AD
	public void SetColor(Color color, Sprite symbol)
	{
		this.SymbolBase.sprite = symbol;
		this.Liner.material.SetColor("_Color", color);
		this.ColorBase.color = color;
		this.ColorEnd.color = color;
	}

	// Token: 0x040003FA RID: 1018
	private const int WireDepth = -14;

	// Token: 0x040003FB RID: 1019
	public SpriteRenderer Liner;

	// Token: 0x040003FC RID: 1020
	public SpriteRenderer ColorBase;

	// Token: 0x040003FD RID: 1021
	public SpriteRenderer SymbolBase;

	// Token: 0x040003FE RID: 1022
	public SpriteRenderer ColorEnd;

	// Token: 0x040003FF RID: 1023
	public Collider2D hitbox;

	// Token: 0x04000400 RID: 1024
	public SpriteRenderer WireTip;

	// Token: 0x04000401 RID: 1025
	public sbyte WireId;
}
