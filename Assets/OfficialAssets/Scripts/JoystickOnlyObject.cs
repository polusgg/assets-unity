using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class JoystickOnlyObject : MonoBehaviour
{
	// Token: 0x060008B3 RID: 2227 RVA: 0x00038C63 File Offset: 0x00036E63
	private void Awake()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Combine(ActiveInputManager.CurrentInputSourceChanged, new Action(this.UpdateState));
		this.UpdateState();
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00038C8B File Offset: 0x00036E8B
	private void OnDestroy()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Remove(ActiveInputManager.CurrentInputSourceChanged, new Action(this.UpdateState));
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00038CAD File Offset: 0x00036EAD
	private void UpdateState()
	{
		base.gameObject.SetActive(ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick);
	}
}
