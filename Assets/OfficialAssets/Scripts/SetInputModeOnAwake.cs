using System;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class SetInputModeOnAwake : MonoBehaviour
{
	// Token: 0x06000928 RID: 2344 RVA: 0x0003C6DC File Offset: 0x0003A8DC
	private void Awake()
	{
		switch (this.inputMode)
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

	// Token: 0x04000AA1 RID: 2721
	public ConsoleJoystick.ConsoleInputState inputMode;
}
