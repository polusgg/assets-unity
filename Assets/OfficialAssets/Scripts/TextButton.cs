using System;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class TextButton : MonoBehaviour
{
	// Token: 0x06000D68 RID: 3432 RVA: 0x0005174C File Offset: 0x0004F94C
	public void SetText(string text)
	{
		//this.TextRend.Start();
		//this.TextRend.Text = text;
		//this.TextRend.RefreshMesh();
		//Vector2 size;
		//size..ctor((this.TextRend.Width + 0.1f) * 2f, this.TextRend.Height + 0.1f);
		//this.Background.size = size;
		//this.Hitbox.size = size;
	}

	// Token: 0x04000EF1 RID: 3825
	public TextRenderer TextRend;

	// Token: 0x04000EF2 RID: 3826
	public SpriteRenderer Background;

	// Token: 0x04000EF3 RID: 3827
	public BoxCollider2D Hitbox;
}
