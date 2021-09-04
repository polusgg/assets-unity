using System;
using TMPro;
using UnityEngine;

// Token: 0x020000B9 RID: 185
internal class ChatBubble : PoolableBehavior
{
	// Token: 0x06000465 RID: 1125 RVA: 0x0001C360 File Offset: 0x0001A560
	public void SetLeft()
	{
		base.transform.localPosition = new Vector3(-3f, 0f, 0f);
		this.ChatFace.flipX = false;
		this.ChatFace.transform.localScale = new Vector3(1f, 1f, 1f);
		this.ChatFace.transform.localPosition = new Vector3(0f, 0.07f, 0f);
		this.Xmark.transform.localPosition = new Vector3(-0.15f, -0.13f, -0.0001f);
		this.votedMark.transform.localPosition = new Vector3(-0.15f, -0.13f, -0.0001f);
		this.NameText.rectTransform.pivot = new Vector2(0f, 1f);
		this.NameText.transform.localPosition = new Vector3(0.5f, 0.358f, 0f);
		this.NameText.horizontalAlignment = HorizontalAlignmentOptions.Left;
		this.TextArea.rectTransform.pivot = new Vector2(0f, 1f);
		this.TextArea.transform.localPosition = new Vector3(0.5f, 0.125f, 0f);
		this.TextArea.horizontalAlignment = HorizontalAlignmentOptions.Left;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
	public void SetNotification()
	{
		//base.transform.localPosition = new Vector3(-2.75f, 0f, 0f);
		//this.ChatFace.flipX = false;
		//this.ChatFace.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		//this.ChatFace.transform.localPosition = new Vector3(0f, 0.18f, 0f);
		//this.Xmark.transform.localPosition = new Vector3(-0.15f, -0.13f, -0.0001f);
		//this.votedMark.transform.localPosition = new Vector3(-0.15f, -0.13f, -0.0001f);
		//this.NameText.rectTransform.pivot = new Vector2(0f, 1f);
		//this.NameText.transform.localPosition = new Vector3(0.5f, 0.358f, 0f);
		//this.NameText.horizontalAlignment = 1;
		//this.TextArea.rectTransform.pivot = new Vector2(0f, 1f);
		//this.TextArea.transform.localPosition = new Vector3(0.5f, 0.125f, 0f);
		//this.TextArea.horizontalAlignment = 1;
		//this.TextArea.text = string.Empty;
		//this.isNotification = true;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001C648 File Offset: 0x0001A848
	public void SetRight()
	{
		//base.transform.localPosition = new Vector3(-2.35f, 0f, 0f);
		//this.ChatFace.flipX = true;
		//this.ChatFace.transform.localScale = new Vector3(1f, 1f, 1f);
		//this.ChatFace.transform.localPosition = new Vector3(4.75f, 0.07f, 0f);
		//this.Xmark.transform.localPosition = new Vector3(0.15f, -0.13f, -0.0001f);
		//this.votedMark.transform.localPosition = new Vector3(0.15f, -0.13f, -0.0001f);
		//this.NameText.rectTransform.pivot = new Vector2(1f, 1f);
		//this.NameText.transform.localPosition = new Vector3(4.35f, 0.358f, 0f);
		//this.NameText.horizontalAlignment = 4;
		//this.TextArea.rectTransform.pivot = new Vector2(1f, 1f);
		//this.TextArea.transform.localPosition = new Vector3(4.35f, 0.125f, 0f);
		//this.TextArea.horizontalAlignment = 4;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0001C7B0 File Offset: 0x0001A9B0
	public void SetName(string playerName, bool isDead, bool voted, Color color)
	{
		this.NameText.text = (playerName ?? "...");
		this.NameText.color = color;
		this.NameText.ForceMeshUpdate(true, true);
		if (isDead)
		{
			this.Xmark.enabled = true;
			this.Background.color = Palette.HalfWhite;
		}
		if (voted)
		{
			this.votedMark.enabled = true;
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001C81A File Offset: 0x0001AA1A
	public override void Reset()
	{
		this.Xmark.enabled = false;
		this.votedMark.enabled = false;
		this.Background.color = Color.white;
		this.isNotification = false;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001C84C File Offset: 0x0001AA4C
	internal void SetText(string chatText)
	{
		this.TextArea.text = chatText;
		this.TextArea.ForceMeshUpdate(true, true);
		this.Background.size = new Vector2(5.52f, 0.2f + this.NameText.GetNotDumbRenderedHeight() + this.TextArea.GetNotDumbRenderedHeight());
	}

	// Token: 0x04000525 RID: 1317
	public SpriteRenderer ChatFace;

	// Token: 0x04000526 RID: 1318
	public SpriteRenderer Xmark;

	// Token: 0x04000527 RID: 1319
	public SpriteRenderer votedMark;

	// Token: 0x04000528 RID: 1320
	public TextMeshPro NameText;

	// Token: 0x04000529 RID: 1321
	public TextMeshPro TextArea;

	// Token: 0x0400052A RID: 1322
	public SpriteRenderer Background;

	// Token: 0x0400052B RID: 1323
	public PlatformIdentifierIcon PlatformIcon;

	// Token: 0x0400052C RID: 1324
	private bool isNotification;
}
