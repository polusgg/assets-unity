using System;
using System.Text;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class NotificationPopper : MonoBehaviour
{
	// Token: 0x0600054E RID: 1358 RVA: 0x00023B6C File Offset: 0x00021D6C
	public void Update()
	{
		if (this.alphaTimer > 0f)
		{
			float num = Camera.main.orthographicSize * Camera.main.aspect;
			if (!DestroyableSingleton<HudManager>.Instance.TaskText.isActiveAndEnabled)
			{
				float height = DestroyableSingleton<HudManager>.Instance.GameSettings.Height;
				Transform transform = DestroyableSingleton<HudManager>.Instance.GameSettings.transform;
				base.transform.localPosition = new Vector3(-num + 0.1f, transform.localPosition.y - height, this.zPos);
			}
			else
			{
				float height2 = DestroyableSingleton<HudManager>.Instance.TaskText.Height;
				Transform parent = DestroyableSingleton<HudManager>.Instance.TaskText.transform.parent;
				base.transform.localPosition = new Vector3(-num + 0.1f, parent.localPosition.y - height2 - 0.2f, this.zPos);
			}
			this.alphaTimer -= Time.deltaTime;
			this.textColor.a = Mathf.Clamp(this.alphaTimer / this.FadeDuration, 0f, 1f);
			this.TextArea.Color = this.textColor;
			if (this.alphaTimer <= 0f)
			{
				this.builder.Clear();
				this.TextArea.Text = string.Empty;
			}
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00023CC8 File Offset: 0x00021EC8
	public void AddItem(string item)
	{
		this.builder.AppendLine(item);
		this.TextArea.Text = this.builder.ToString();
		this.alphaTimer = this.ShowDuration;
		SoundManager.Instance.PlaySound(this.NotificationSound, false, 1f);
	}

	// Token: 0x040005FB RID: 1531
	public TextRenderer TextArea;

	// Token: 0x040005FC RID: 1532
	public float zPos = -350f;

	// Token: 0x040005FD RID: 1533
	private float alphaTimer;

	// Token: 0x040005FE RID: 1534
	public float ShowDuration = 5f;

	// Token: 0x040005FF RID: 1535
	public float FadeDuration = 1f;

	// Token: 0x04000600 RID: 1536
	public Color textColor = Color.white;

	// Token: 0x04000601 RID: 1537
	private StringBuilder builder = new StringBuilder();

	// Token: 0x04000602 RID: 1538
	public AudioClip NotificationSound;
}
