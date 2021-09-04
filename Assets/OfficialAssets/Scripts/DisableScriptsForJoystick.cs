using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class DisableScriptsForJoystick : MonoBehaviour
{
	// Token: 0x06000893 RID: 2195 RVA: 0x000383BC File Offset: 0x000365BC
	private void Start()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Combine(ActiveInputManager.CurrentInputSourceChanged, new Action(this.OnInputMethodChanged));
		this.OnInputMethodChanged();
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x000383E4 File Offset: 0x000365E4
	private void OnDestroy()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Remove(ActiveInputManager.CurrentInputSourceChanged, new Action(this.OnInputMethodChanged));
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00038408 File Offset: 0x00036608
	private void OnInputMethodChanged()
	{
		MonoBehaviour[] array = this.scripts;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = (ActiveInputManager.currentControlType > ActiveInputManager.InputType.Joystick);
		}
	}

	// Token: 0x040009FA RID: 2554
	public MonoBehaviour[] scripts;
}
