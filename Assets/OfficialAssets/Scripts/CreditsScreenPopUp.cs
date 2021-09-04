using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class CreditsScreenPopUp : MonoBehaviour
{
	// Token: 0x060002EC RID: 748 RVA: 0x0001361B File Offset: 0x0001181B
	private void OnEnable()
	{
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00013633 File Offset: 0x00011833
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x04000364 RID: 868
	[Header("Console Controller Navigation")]
	public UiElement BackButton;
}
