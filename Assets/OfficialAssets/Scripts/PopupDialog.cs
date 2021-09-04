using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000184 RID: 388
public class PopupDialog : MonoBehaviour
{
	// Token: 0x060008CD RID: 2253 RVA: 0x00039149 File Offset: 0x00037349
	public static void Display()
	{
		if (!PopupDialog.instance)
		{
			PopupDialog.instance = Object.Instantiate<GameObject>(Resources.Load<GameObject>("WaitForConnectionDialog")).GetComponent<PopupDialog>();
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00039170 File Offset: 0x00037370
	public static void Dispose()
	{
		if (PopupDialog.instance)
		{
			Object.Destroy(PopupDialog.instance);
		}
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00039188 File Offset: 0x00037388
	private void Start()
	{
		ControllerManager.Instance.enabled = false;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00039195 File Offset: 0x00037395
	private void OnDestroy()
	{
		ControllerManager.Instance.enabled = true;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x000391A4 File Offset: 0x000373A4
	public void Update()
	{
		this.textUpdateTimer -= Time.unscaledDeltaTime;
		if (this.textUpdateTimer <= 0f)
		{
			this.textUpdateTimer += this.secondsBetweenDots;
			if (this.currentProgressText.Length == this.maxDots)
			{
				this.currentProgressText = "";
			}
			else
			{
				this.currentProgressText += ".";
			}
			this.workingText.Text = this.currentProgressText;
		}
	}

	// Token: 0x04000A24 RID: 2596
	public TextRenderer workingText;

	// Token: 0x04000A25 RID: 2597
	public float secondsBetweenDots = 0.7f;

	// Token: 0x04000A26 RID: 2598
	public int maxDots = 3;

	// Token: 0x04000A27 RID: 2599
	public string currentProgressText = "";

	// Token: 0x04000A28 RID: 2600
	private float textUpdateTimer;

	// Token: 0x04000A29 RID: 2601
	private static PopupDialog instance;
}
