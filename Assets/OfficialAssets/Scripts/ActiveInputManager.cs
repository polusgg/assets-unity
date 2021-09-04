using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class ActiveInputManager : DestroyableSingleton<ActiveInputManager>
{
	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000839 RID: 2105 RVA: 0x00035AD9 File Offset: 0x00033CD9
	private static ActiveInputManager.InputType PlatformDefault
	{
		get
		{
			return ActiveInputManager.InputType.Keyboard;
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00035ADC File Offset: 0x00033CDC
	private void Start()
	{
		//ReInput.players.GetPlayer(0).controllers.AddLastActiveControllerChangedDelegate(new PlayerActiveControllerChangedDelegate(this.OnLastActiveControllerChanged));
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00035AFF File Offset: 0x00033CFF
	private void Update()
	{
		//this.UpdateJoystickState();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00035B07 File Offset: 0x00033D07
	public void SetTouchAsCurrentInput()
	{
		ActiveInputManager.currentControlType = ActiveInputManager.InputType.Touch;
		this.tChangeTime = Math.Max(this.kChangeTime, Math.Max(this.mChangeTime, this.jChangeTime));
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00035B34 File Offset: 0x00033D34
	//private void OnLastActiveControllerChanged(Player player, Controller controller)
	//{
	//	if (controller != null)
	//	{
	//		Debug.Log("OnLastActiveControllerChanged: " + controller.name);
	//		ActiveInputManager.currentControlType = ActiveInputManager.InputType.Joystick;
	//		if (DestroyableSingleton<HudManager>.InstanceExists && !(DestroyableSingleton<HudManager>.Instance.joystick is ConsoleJoystick))
	//		{
	//			DestroyableSingleton<HudManager>.Instance.SetTouchType(DestroyableSingleton<HudManager>.Instance.Joysticks.Length - 2);
	//		}
	//	}
	//	else
	//	{
	//		Debug.Log("OnLastActiveControllerChanged: NULL");
	//		ActiveInputManager.currentControlType = ActiveInputManager.PlatformDefault;
	//		if (DestroyableSingleton<HudManager>.InstanceExists && DestroyableSingleton<HudManager>.Instance.joystick is ConsoleJoystick)
	//		{
	//			DestroyableSingleton<HudManager>.Instance.SetTouchType(SaveManager.ControlMode);
	//		}
	//	}
	//	this.lastUsedController = controller;
	//	if (ActiveInputManager.CurrentInputSourceChanged != null)
	//	{
	//		ActiveInputManager.CurrentInputSourceChanged();
	//	}
	//}

	//// Token: 0x0600083E RID: 2110 RVA: 0x00035BE4 File Offset: 0x00033DE4
	//public void UpdateJoystickState()
	//{
	//	Player player = ReInput.players.GetPlayer(0);
	//	ActiveInputManager.InputType inputType = ActiveInputManager.currentControlType;
	//	if (ActiveInputManager.currentControlType != ActiveInputManager.InputType.Keyboard)
	//	{
	//		if (player.controllers.hasKeyboard)
	//		{
	//			this.kChangeTime = player.controllers.Keyboard.GetLastTimeAnyElementChanged();
	//			if (this.kChangeTime > this.jChangeTime && this.kChangeTime > this.tChangeTime)
	//			{
	//				ActiveInputManager.currentControlType = ActiveInputManager.InputType.Keyboard;
	//			}
	//		}
	//		if (player.controllers.hasMouse)
	//		{
	//			this.mChangeTime = player.controllers.Mouse.GetLastTimeAnyButtonChanged();
	//			if (this.mChangeTime > this.jChangeTime && this.mChangeTime > this.tChangeTime)
	//			{
	//				ActiveInputManager.currentControlType = ActiveInputManager.InputType.Keyboard;
	//			}
	//		}
	//	}
	//	if (player.controllers.joystickCount > 0 && ActiveInputManager.currentControlType != ActiveInputManager.InputType.Joystick)
	//	{
	//		Controller lastActiveController = player.controllers.GetLastActiveController(2);
	//		if (lastActiveController != null)
	//		{
	//			this.jChangeTime = lastActiveController.GetLastTimeAnyElementChanged();
	//			if (this.jChangeTime > this.mChangeTime && this.jChangeTime > this.kChangeTime && this.jChangeTime > this.tChangeTime)
	//			{
	//				ActiveInputManager.currentControlType = ActiveInputManager.InputType.Joystick;
	//			}
	//		}
	//	}
	//	if (inputType != ActiveInputManager.currentControlType)
	//	{
	//		if (ActiveInputManager.CurrentInputSourceChanged != null)
	//		{
	//			ActiveInputManager.CurrentInputSourceChanged();
	//		}
	//		if (DestroyableSingleton<HudManager>.InstanceExists)
	//		{
	//			if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
	//			{
	//				if (!(DestroyableSingleton<HudManager>.Instance.joystick is ConsoleJoystick))
	//				{
	//					DestroyableSingleton<HudManager>.Instance.SetTouchType(DestroyableSingleton<HudManager>.Instance.Joysticks.Length - 2);
	//					return;
	//				}
	//			}
	//			else if (DestroyableSingleton<HudManager>.Instance.joystick is ConsoleJoystick)
	//			{
	//				DestroyableSingleton<HudManager>.Instance.SetTouchType(SaveManager.ControlMode);
	//			}
	//		}
	//	}
	//}

	// Token: 0x040009B0 RID: 2480
	public ActiveInputManager.InputType testCurrentControlType;

	// Token: 0x040009B1 RID: 2481
	public static ActiveInputManager.InputType currentControlType = ActiveInputManager.PlatformDefault;

	// Token: 0x040009B2 RID: 2482
	public double kChangeTime;

	// Token: 0x040009B3 RID: 2483
	public double mChangeTime;

	// Token: 0x040009B4 RID: 2484
	public double jChangeTime;

	// Token: 0x040009B5 RID: 2485
	public double tChangeTime;

	// Token: 0x040009B6 RID: 2486
	public static Action CurrentInputSourceChanged;

	// Token: 0x040009B7 RID: 2487
	private Controller lastUsedController;

	// Token: 0x020003DA RID: 986
	public enum InputType
	{
		// Token: 0x04001AB7 RID: 6839
		Joystick,
		// Token: 0x04001AB8 RID: 6840
		Keyboard,
		// Token: 0x04001AB9 RID: 6841
		Touch
	}
}
