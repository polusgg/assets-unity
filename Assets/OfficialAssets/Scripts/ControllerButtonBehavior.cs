using System;
//using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016C RID: 364
public class ControllerButtonBehavior : MonoBehaviour
{
	// Token: 0x06000861 RID: 2145 RVA: 0x00036998 File Offset: 0x00034B98
	private void Start()
	{
		//this.player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x000369AC File Offset: 0x00034BAC
	private void Update()
	{
		//if (RadialMenu.instances > 0)
		//{
		//	return;
		//}
		//if (this.player.GetButtonDown((int)this.Action))
		//{
		//	if (this.requiredMenuObject && ControllerManager.Instance.CurrentUiState.MenuName != this.requiredMenuObject.name)
		//	{
		//		return;
		//	}
		//	ButtonBehavior component = base.GetComponent<ButtonBehavior>();
		//	if (component)
		//	{
		//		Button.ButtonClickedEvent onClick = component.OnClick;
		//		if (onClick == null)
		//		{
		//			return;
		//		}
		//		onClick.Invoke();
		//		return;
		//	}
		//	else
		//	{
		//		PassiveButton component2 = base.GetComponent<PassiveButton>();
		//		if (component2)
		//		{
		//			Button.ButtonClickedEvent onClick2 = component2.OnClick;
		//			if (onClick2 == null)
		//			{
		//				return;
		//			}
		//			onClick2.Invoke();
		//		}
		//	}
		//}
	}

	// Token: 0x040009C3 RID: 2499
	public RewiredConstsEnum.Action Action;

	// Token: 0x040009C4 RID: 2500
	public GameObject requiredMenuObject;

	// Token: 0x040009C5 RID: 2501
	//private Player player;
}
