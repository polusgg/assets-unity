using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class WireNode : MonoBehaviour
{
	// Token: 0x06000376 RID: 886 RVA: 0x0001704C File Offset: 0x0001524C
	internal void SetColor(Color color, Sprite symbol)
	{
		this.BaseSymbol.sprite = symbol;
		for (int i = 0; i < this.WireColors.Length; i++)
		{
			this.WireColors[i].color = color;
		}
	}

	// Token: 0x04000416 RID: 1046
	public Collider2D hitbox;

	// Token: 0x04000417 RID: 1047
	public SpriteRenderer[] WireColors;

	// Token: 0x04000418 RID: 1048
	public SpriteRenderer BaseSymbol;

	// Token: 0x04000419 RID: 1049
	public sbyte WireId;
}
