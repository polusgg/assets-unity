using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000249 RID: 585
public class GenericPopup : MonoBehaviour
{
	// Token: 0x06000DAE RID: 3502 RVA: 0x00052FBB File Offset: 0x000511BB
	public void Show(string text = "")
	{
		if (this.TextArea)
		{
			this.TextArea.Text = text;
		}
		if (this.TextAreaTMP)
		{
			this.TextAreaTMP.text = text;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00052FFB File Offset: 0x000511FB
	public void Close()
	{
		if (this.destroyOnClose)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040011B9 RID: 4537
	public TextRenderer TextArea;

	// Token: 0x040011BA RID: 4538
	public TextMeshPro TextAreaTMP;

	// Token: 0x040011BB RID: 4539
	public bool destroyOnClose;
}
