using System;
using System.Collections.Generic;
////using Rewired;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class RewiredCategoryEnabler : MonoBehaviour
{
	// Token: 0x06000916 RID: 2326 RVA: 0x0003BF97 File Offset: 0x0003A197
	private void OnEnable()
	{
		//ReInput.ControllerConnectedEvent += this.ReInput_ControllerConnectedEvent;
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0003BFAA File Offset: 0x0003A1AA
	private void OnDisable()
	{
		//ReInput.ControllerConnectedEvent -= this.ReInput_ControllerConnectedEvent;
	}

    // Token: 0x06000918 RID: 2328 RVA: 0x0003BFBD File Offset: 0x0003A1BD
    //private void ReInput_ControllerConnectedEvent(ControllerStatusChangedEventArgs obj)
    //{
    //    if (this.applyDefaultMapState)
    //    {
    //        this.ApplyDefaultMapState();
    //    }
    //}

    // Token: 0x06000919 RID: 2329 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
    public void ApplyDefaultMapState()
	{
		//IEnumerable<Player> allPlayers = ReInput.players.AllPlayers;
		//Debug.Log("RewiredCategoryEnabler - Applying default map state to all players");
		//int num = 0;
		//foreach (Player player in allPlayers)
		//{
		//	foreach (Joystick joystick in player.controllers.Joysticks)
		//	{
		//		num++;
		//		foreach (RewiredCategoryEnabler.RewiredCategoryState rewiredCategoryState in this.defaultStates)
		//		{
		//			player.controllers.maps.GetMap(joystick, rewiredCategoryState.name, "Default").enabled = rewiredCategoryState.enabled;
		//		}
		//	}
		//}
		//Debug.Log("Applied default map state to " + num.ToString() + " joysticks");
		//if (ConsoleJoystick.inputState != ConsoleJoystick.ConsoleInputState.Menu)
		//{
		//	ConsoleJoystick.ReapplyCurrentInputState();
		//}
	}

	// Token: 0x04000A92 RID: 2706
	public bool applyDefaultMapState;

	// Token: 0x04000A93 RID: 2707
	public RewiredCategoryEnabler.RewiredCategoryState[] defaultStates;

	// Token: 0x02000400 RID: 1024
	[Serializable]
	public class RewiredCategoryState
	{
		// Token: 0x04001B25 RID: 6949
		public string name;

		// Token: 0x04001B26 RID: 6950
		public bool enabled;
	}
}
