using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class ConsoleJoystick : MonoBehaviour, IVirtualJoystick
{
	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000851 RID: 2129 RVA: 0x00035EDA File Offset: 0x000340DA
	public Vector2 Delta
	{
		get
		{
			return this.delta;
		}
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00035EE2 File Offset: 0x000340E2
	private void OnEnable()
	{
		RewiredControllerSupport.onPostControllersAssigned = (Action)Delegate.Combine(RewiredControllerSupport.onPostControllersAssigned, new Action(ConsoleJoystick.ReapplyCurrentInputState));
		if (ConsoleJoystick.inputState == ConsoleJoystick.ConsoleInputState.Menu)
		{
			ConsoleJoystick.SetMode_Gameplay();
			return;
		}
		ConsoleJoystick.ReapplyCurrentInputState();
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00035F17 File Offset: 0x00034117
	private void OnDisable()
	{
		RewiredControllerSupport.onPostControllersAssigned = (Action)Delegate.Remove(RewiredControllerSupport.onPostControllersAssigned, new Action(ConsoleJoystick.ReapplyCurrentInputState));
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00035F3C File Offset: 0x0003413C
	public static void ReapplyCurrentInputState()
	{
		Debug.LogError("New controller connected, updating current input state");
		switch (ConsoleJoystick.inputState)
		{
		case ConsoleJoystick.ConsoleInputState.Gameplay:
			ConsoleJoystick.SetMode_Gameplay();
			return;
		case ConsoleJoystick.ConsoleInputState.Menu:
			ConsoleJoystick.SetMode_Menu();
			return;
		case ConsoleJoystick.ConsoleInputState.Task:
			ConsoleJoystick.SetMode_Task();
			return;
		case ConsoleJoystick.ConsoleInputState.Sabotage:
			ConsoleJoystick.SetMode_Sabotage();
			return;
		case ConsoleJoystick.ConsoleInputState.Vent:
			ConsoleJoystick.SetMode_Vent();
			return;
		case ConsoleJoystick.ConsoleInputState.QuickChat:
			ConsoleJoystick.SetMode_QuickChat();
			return;
		default:
			return;
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00035F9C File Offset: 0x0003419C
	public static void SetMode_MenuAdditive()
	{
		//ConsoleJoystick.player = ReInput.players.GetPlayer(0);
		//IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
		//for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
		//{
		//	ConsoleJoystick.controller = joysticks[i];
		//	ConsoleJoystick.SetMapEnabled(2, true);
		//}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00035FF8 File Offset: 0x000341F8
	public static void SetMode_Gameplay()
	{
		//ConsoleJoystick.inputState = ConsoleJoystick.ConsoleInputState.Gameplay;
		//ConsoleJoystick.player = ReInput.players.GetPlayer(0);
		//if (VirtualCursor.instance)
		//{
		//	VirtualCursor.instance.gameObject.SetActive(false);
		//}
		//IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
		//for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
		//{
		//	ConsoleJoystick.controller = joysticks[i];
		//	ConsoleJoystick.SetMapEnabled(1, true);
		//	ConsoleJoystick.SetMapEnabled(2, false);
		//	ConsoleJoystick.SetMapEnabled(3, false);
		//	ConsoleJoystick.SetMapEnabled(4, false);
		//}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0003608C File Offset: 0x0003428C
	public static void SetMode_Menu()
	{
		//ConsoleJoystick.inputState = ConsoleJoystick.ConsoleInputState.Menu;
		//ConsoleJoystick.player = ReInput.players.GetPlayer(0);
		//if (VirtualCursor.instance && ControllerManager.Instance && !ControllerManager.Instance.IsUiControllerActive)
		//{
		//	VirtualCursor.instance.gameObject.SetActive(true);
		//}
		//IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
		//for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
		//{
		//	ConsoleJoystick.controller = joysticks[i];
		//	ConsoleJoystick.SetMapEnabled(1, false);
		//	ConsoleJoystick.SetMapEnabled(2, true);
		//	ConsoleJoystick.SetMapEnabled(3, false);
		//	ConsoleJoystick.SetMapEnabled(4, false);
		//}
		//VirtualCursor.horizontalAxis = 9;
		//VirtualCursor.verticalAxis = 10;
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00036144 File Offset: 0x00034344
	public static void SetMode_Task()
	{
        //ConsoleJoystick.inputState = ConsoleJoystick.ConsoleInputState.Task;
        //ConsoleJoystick.player = ReInput.players.GetPlayer(0);
        //if (VirtualCursor.instance && !ControllerManager.Instance.IsUiControllerActive)
        //{
        //    VirtualCursor.instance.gameObject.SetActive(true);
        //}
        //IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
        //for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
        //{
        //    ConsoleJoystick.controller = joysticks[i];
        //    ConsoleJoystick.SetMapEnabled(1, false);
        //    ConsoleJoystick.SetMapEnabled(2, false);
        //    ConsoleJoystick.SetMapEnabled(3, true);
        //    ConsoleJoystick.SetMapEnabled(4, false);
        //}
        //VirtualCursor.horizontalAxis = 13;
        //VirtualCursor.verticalAxis = 14;
    }

	// Token: 0x06000859 RID: 2137 RVA: 0x000361F0 File Offset: 0x000343F0
	public static void SetMode_Sabotage()
	{
		//ConsoleJoystick.inputState = ConsoleJoystick.ConsoleInputState.Sabotage;
		//ConsoleJoystick.player = ReInput.players.GetPlayer(0);
		//if (VirtualCursor.instance)
		//{
		//	VirtualCursor.instance.gameObject.SetActive(false);
		//}
		//IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
		//for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
		//{
		//	ConsoleJoystick.controller = joysticks[i];
		//	ConsoleJoystick.SetMapEnabled(1, true);
		//	ConsoleJoystick.SetMapEnabled(2, false);
		//	ConsoleJoystick.SetMapEnabled(3, true);
		//	ConsoleJoystick.SetMapEnabled(4, false);
		//}
		//VirtualCursor.horizontalAxis = 16;
		//VirtualCursor.verticalAxis = 17;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00036290 File Offset: 0x00034490
	public static void SetMode_Vent()
	{
		//ConsoleJoystick.inputState = ConsoleJoystick.ConsoleInputState.Vent;
		//ConsoleJoystick.player = ReInput.players.GetPlayer(0);
		//if (VirtualCursor.instance && !ControllerManager.Instance.IsUiControllerActive)
		//{
		//	VirtualCursor.instance.gameObject.SetActive(false);
		//}
		//IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
		//for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
		//{
		//	ConsoleJoystick.controller = joysticks[i];
		//	ConsoleJoystick.SetMapEnabled(1, true);
		//	ConsoleJoystick.SetMapEnabled(2, false);
		//	ConsoleJoystick.SetMapEnabled(3, false);
		//	ConsoleJoystick.SetMapEnabled(4, false);
		//}
		//ConsoleJoystick.highlightedVentIndex = -1;
		//VirtualCursor.horizontalAxis = 16;
		//VirtualCursor.verticalAxis = 17;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00036344 File Offset: 0x00034544
	public static void SetMode_QuickChat()
	{
		//ConsoleJoystick.inputState = ConsoleJoystick.ConsoleInputState.QuickChat;
		//ConsoleJoystick.player = ReInput.players.GetPlayer(0);
		//if (VirtualCursor.instance && !ControllerManager.Instance.IsUiControllerActive)
		//{
		//	VirtualCursor.instance.gameObject.SetActive(true);
		//}
		//IList<Joystick> joysticks = ConsoleJoystick.player.controllers.Joysticks;
		//for (int i = 0; i < ConsoleJoystick.player.controllers.joystickCount; i++)
		//{
		//	ConsoleJoystick.controller = joysticks[i];
		//	ConsoleJoystick.SetMapEnabled(1, false);
		//	ConsoleJoystick.SetMapEnabled(2, false);
		//	ConsoleJoystick.SetMapEnabled(3, false);
		//	ConsoleJoystick.SetMapEnabled(4, true);
		//}
		//VirtualCursor.horizontalAxis = 13;
		//VirtualCursor.verticalAxis = 14;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x000363EF File Offset: 0x000345EF
	private static void SetMapEnabled(int rewiredCategoryIndex, bool enableMap)
	{
		//if (ConsoleJoystick.controller == null)
		//{
		//	return;
		//}
		//ConsoleJoystick.player.controllers.maps.GetMap(ConsoleJoystick.controller, rewiredCategoryIndex, 0).enabled = enableMap;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0003641C File Offset: 0x0003461C
	private void Update()
	{
		//if (!PlayerControl.LocalPlayer)
		//{
		//	return;
		//}
		//if (ConsoleJoystick.oldInputState != ConsoleJoystick.inputState)
		//{
		//	if (DestroyableSingleton<HudManager>.Instance.consoleUIObjects[(int)ConsoleJoystick.oldInputState])
		//	{
		//		DestroyableSingleton<HudManager>.Instance.consoleUIObjects[(int)ConsoleJoystick.oldInputState].SetActive(false);
		//	}
		//	if (DestroyableSingleton<HudManager>.Instance.consoleUIObjects[(int)ConsoleJoystick.inputState])
		//	{
		//		DestroyableSingleton<HudManager>.Instance.consoleUIObjects[(int)ConsoleJoystick.inputState].SetActive(true);
		//	}
		//	ConsoleJoystick.oldInputState = ConsoleJoystick.inputState;
		//}
		//if (ConsoleJoystick.inputState == ConsoleJoystick.ConsoleInputState.Menu && ControllerManager.Instance.IsUiControllerActive != DestroyableSingleton<HudManager>.Instance.consoleUIObjects[(int)ConsoleJoystick.inputState].activeSelf)
		//{
		//	DestroyableSingleton<HudManager>.Instance.consoleUIObjects[(int)ConsoleJoystick.inputState].SetActive(ControllerManager.Instance.IsUiControllerActive);
		//}
		//this.delta.x = ConsoleJoystick.player.GetAxis(2);
		//this.delta.y = ConsoleJoystick.player.GetAxis(3);
		//if (this.delta.sqrMagnitude > 1f)
		//{
		//	this.delta = this.delta.normalized;
		//}
		//this.HandleHUD();
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00036544 File Offset: 0x00034744
	private void HandleHUD()
	{
		//if (PlayerControl.LocalPlayer)
		//{
		//	bool canMove = PlayerControl.LocalPlayer.CanMove;
		//	if (canMove && PlayerControl.LocalPlayer.MyPhysics.inputHandler.enabled)
		//	{
		//		PlayerControl.LocalPlayer.MyPhysics.inputHandler.enabled = false;
		//	}
		//	if (!canMove && ConsoleJoystick.inputState == ConsoleJoystick.ConsoleInputState.Gameplay)
		//	{
		//		ConsoleJoystick.SetMode_Menu();
		//		return;
		//	}
		//	if (canMove && ConsoleJoystick.inputState != ConsoleJoystick.ConsoleInputState.Sabotage && ConsoleJoystick.inputState != ConsoleJoystick.ConsoleInputState.Gameplay)
		//	{
		//		ConsoleJoystick.SetMode_Gameplay();
		//		return;
		//	}
		//}
		//if (ConsoleJoystick.inputState == ConsoleJoystick.ConsoleInputState.Vent)
		//{
		//	if (!ControllerManager.Instance.IsUiControllerActive)
		//	{
		//		if (!Vent.currentVent)
		//		{
		//			ConsoleJoystick.SetMode_Gameplay();
		//		}
		//		else
		//		{
		//			bool flag = false;
		//			if (this.delta.sqrMagnitude > 0.25f)
		//			{
		//				flag = true;
		//				Vector2 normalized = this.delta.normalized;
		//				Vector2 vector = Vector2.zero;
		//				float num = float.NegativeInfinity;
		//				int num2 = -1;
		//				for (int i = 0; i < Vent.currentVent.Buttons.Length; i++)
		//				{
		//					if (Vent.currentVent.Buttons[i].isActiveAndEnabled)
		//					{
		//						vector = Vent.currentVent.Buttons[i].transform.localPosition.normalized;
		//						float num3 = Vector2.Dot(normalized, vector);
		//						if (num2 == -1 || num3 > num)
		//						{
		//							num = num3;
		//							num2 = i;
		//						}
		//					}
		//				}
		//				if (num > 0.7f)
		//				{
		//					if (ConsoleJoystick.highlightedVentIndex != -1 && ConsoleJoystick.highlightedVentIndex < Vent.currentVent.Buttons.Length)
		//					{
		//						Vent.currentVent.Buttons[ConsoleJoystick.highlightedVentIndex].spriteRenderer.color = Color.white;
		//						ConsoleJoystick.highlightedVentIndex = -1;
		//					}
		//					ConsoleJoystick.highlightedVentIndex = num2;
		//					Vent.currentVent.Buttons[ConsoleJoystick.highlightedVentIndex].spriteRenderer.color = Color.red;
		//				}
		//				else if (ConsoleJoystick.highlightedVentIndex != -1 && ConsoleJoystick.highlightedVentIndex < Vent.currentVent.Buttons.Length)
		//				{
		//					Vent.currentVent.Buttons[ConsoleJoystick.highlightedVentIndex].spriteRenderer.color = Color.white;
		//					ConsoleJoystick.highlightedVentIndex = -1;
		//				}
		//			}
		//			else if (ConsoleJoystick.highlightedVentIndex != -1 && ConsoleJoystick.highlightedVentIndex < Vent.currentVent.Buttons.Length)
		//			{
		//				Vent.currentVent.Buttons[ConsoleJoystick.highlightedVentIndex].spriteRenderer.color = Color.white;
		//				ConsoleJoystick.highlightedVentIndex = -1;
		//			}
		//			if (!flag)
		//			{
		//			//	if (ConsoleJoystick.player.GetButtonDown(6))
		//			//	{
		//			//		DestroyableSingleton<HudManager>.Instance.UseButton.DoClick();
		//			//	}
		//			//}
		//			//else if (ConsoleJoystick.highlightedVentIndex != -1 && ConsoleJoystick.player.GetButtonDown(6))
		//			//{
		//			//	Vent.currentVent.Buttons[ConsoleJoystick.highlightedVentIndex].spriteRenderer.color = Color.white;
		//			//	Vent.currentVent.Buttons[ConsoleJoystick.highlightedVentIndex].OnClick.Invoke();
		//			//	ConsoleJoystick.highlightedVentIndex = -1;
		//			//}
		//		}
		//	}
		//}
		//else
		//{
		//	//if (ConsoleJoystick.player.GetButtonDown(7))
		//	//{
		//	//	DestroyableSingleton<HudManager>.Instance.ReportButton.DoClick();
		//	//}
		//	//if (ConsoleJoystick.player.GetButtonDown(6))
		//	//{
		//	//	DestroyableSingleton<HudManager>.Instance.UseButton.DoClick();
		//	//}
		//	//if (PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.IsImpostor && ConsoleJoystick.player.GetButtonDown(8))
		//	//{
		//	//	DestroyableSingleton<HudManager>.Instance.KillButton.PerformKill();
		//	//}
		//}
		//if (ConsoleJoystick.player.GetButtonDown(5) && DestroyableSingleton<TaskPanelBehaviour>.InstanceExists)
		//{
		//	DestroyableSingleton<TaskPanelBehaviour>.Instance.ToggleOpen();
		//}
		//if (ConsoleJoystick.player.GetButtonDown(1))
		//{
		//	if (DestroyableSingleton<HudManager>.Instance.GameMenu.IsOpen)
		//	{
		//		DestroyableSingleton<HudManager>.Instance.GameMenu.Close();
		//	}
		//	else
		//	{
		//		DestroyableSingleton<HudManager>.Instance.GameMenu.Open();
		//	}
		//}
		//if (ConsoleJoystick.player.GetButtonDown(4))
		//{
		//	if (ConsoleJoystick.inputState == ConsoleJoystick.ConsoleInputState.Sabotage)
		//	{
		//		ConsoleJoystick.SetMode_Gameplay();
		//		if (MapBehaviour.Instance)
		//		{
		//			MapBehaviour.Instance.Close();
		//		}
		//	}
		//	else
		//	{
		//		DestroyableSingleton<HudManager>.Instance.OpenMap();
		//	}
		//}
		//if (ConsoleJoystick.player.GetButtonDown(12))
		//{
		//	if (ControllerManager.Instance.IsUiControllerActive)
		//	{
		//		return;
		//	}
		//	if (Minigame.Instance)
		//	{
		//		Minigame.Instance.Close();
		//	}
		//	else if (MapBehaviour.Instance)
		//	{
		//		MapBehaviour.Instance.Close();
		//	}
		//	if (Vent.currentVent)
		//	{
		//		ConsoleJoystick.SetMode_Vent();
		//		return;
		//	}
		//	if (ConsoleJoystick.inputState != ConsoleJoystick.ConsoleInputState.Gameplay)
		//	{
		//		ConsoleJoystick.SetMode_Gameplay();
		//	}
		//}
	}

	// Token: 0x040009BC RID: 2492
	private Vector2 delta;

	// Token: 0x040009BD RID: 2493
	//private static Player player;

	// Token: 0x040009BE RID: 2494
	private static Controller controller;

	// Token: 0x040009BF RID: 2495
	private static Controller prevController;

	// Token: 0x040009C0 RID: 2496
	public static ConsoleJoystick.ConsoleInputState inputState = ConsoleJoystick.ConsoleInputState.Menu;

	// Token: 0x040009C1 RID: 2497
	private static ConsoleJoystick.ConsoleInputState oldInputState = ConsoleJoystick.ConsoleInputState.Sabotage;

	// Token: 0x040009C2 RID: 2498
	private static int highlightedVentIndex = 0;

	// Token: 0x020003DE RID: 990
	public enum ConsoleInputState
	{
		// Token: 0x04001AC7 RID: 6855
		Gameplay,
		// Token: 0x04001AC8 RID: 6856
		Menu,
		// Token: 0x04001AC9 RID: 6857
		Task,
		// Token: 0x04001ACA RID: 6858
		Sabotage,
		// Token: 0x04001ACB RID: 6859
		Vent,
		// Token: 0x04001ACC RID: 6860
		QuickChat
	}
}
