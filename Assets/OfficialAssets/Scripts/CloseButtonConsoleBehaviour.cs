using System;
using UnityEngine;

// Token: 0x02000162 RID: 354
public class CloseButtonConsoleBehaviour : MonoBehaviour
{
	// Token: 0x06000844 RID: 2116 RVA: 0x00035DB2 File Offset: 0x00033FB2
	private void Start()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Combine(ActiveInputManager.CurrentInputSourceChanged, new Action(this.OnInputMethodChanged));
		this.OnInputMethodChanged();
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00035DDA File Offset: 0x00033FDA
	private void OnDestroy()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Remove(ActiveInputManager.CurrentInputSourceChanged, new Action(this.OnInputMethodChanged));
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00035DFC File Offset: 0x00033FFC
	private void OnInputMethodChanged()
	{
		base.gameObject.SetActive(ActiveInputManager.currentControlType > ActiveInputManager.InputType.Joystick);
	}
}
