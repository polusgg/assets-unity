using System;
//using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016D RID: 365
public class ControllerButtonBehaviourComplex : MonoBehaviour
{
	// Token: 0x06000864 RID: 2148 RVA: 0x00036A4B File Offset: 0x00034C4B
	private void Start()
	{
		//this.player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00036A60 File Offset: 0x00034C60
	private bool ConditionMet()
	{
		//if (this.actionTriggerType == ControllerButtonBehaviourComplex.ActionTriggerType.All)
		//{
		//	foreach (ControllerButtonBehaviourComplex.ActionTrigger actionTrigger in this.actionTriggers)
		//	{
		//		if (!actionTrigger.ConditionMet(this.player))
		//		{
		//			return false;
		//		}
		//	}
		//	return true;
		//}
		//foreach (ControllerButtonBehaviourComplex.ActionTrigger actionTrigger2 in this.actionTriggers)
		//{
		//	if (actionTrigger2.ConditionMet(this.player))
		//	{
		//		return true;
		//	}
		//}
		return false;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00036AD8 File Offset: 0x00034CD8
	private void Update()
	{
		if (this.ConditionMet())
		{
			if (this.requiredMenuObject && ControllerManager.Instance.CurrentUiState.MenuName != this.requiredMenuObject.name)
			{
				return;
			}
			ButtonBehavior component = base.GetComponent<ButtonBehavior>();
			if (component)
			{
				Button.ButtonClickedEvent onClick = component.OnClick;
				if (onClick == null)
				{
					return;
				}
				onClick.Invoke();
				return;
			}
			else
			{
				PassiveButton component2 = base.GetComponent<PassiveButton>();
				if (component2)
				{
					Button.ButtonClickedEvent onClick2 = component2.OnClick;
					if (onClick2 == null)
					{
						return;
					}
					onClick2.Invoke();
				}
			}
		}
	}

	// Token: 0x040009C6 RID: 2502
	public ControllerButtonBehaviourComplex.ActionTriggerType actionTriggerType;

	// Token: 0x040009C7 RID: 2503
	public ControllerButtonBehaviourComplex.ActionTrigger[] actionTriggers;

	// Token: 0x040009C8 RID: 2504
	public GameObject requiredMenuObject;

	// Token: 0x040009C9 RID: 2505
	//private Player player;

	// Token: 0x020003DF RID: 991
	public enum ActionTriggerType
	{
		// Token: 0x04001ACE RID: 6862
		Any,
		// Token: 0x04001ACF RID: 6863
		All
	}

	// Token: 0x020003E0 RID: 992
	[Serializable]
	public struct ActionTrigger
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x00075C48 File Offset: 0x00073E48
		//public bool ConditionMet(Player player)
		//{
		//	switch (this.actionType)
		//	{
		//	case ControllerButtonBehaviourComplex.ActionTrigger.ActionType.Axis_GEqual:
		//		return player.GetAxisRaw((int)this.action) >= this.axisComparisonValue;
		//	case ControllerButtonBehaviourComplex.ActionTrigger.ActionType.Axis_LEqual:
		//		return player.GetAxisRaw((int)this.action) <= this.axisComparisonValue;
		//	case ControllerButtonBehaviourComplex.ActionTrigger.ActionType.Button_Down:
		//		return player.GetButtonDown((int)this.action);
		//	default:
		//		return false;
		//	}
		//}

		// Token: 0x04001AD0 RID: 6864
		public RewiredConstsEnum.Action action;

		// Token: 0x04001AD1 RID: 6865
		public ControllerButtonBehaviourComplex.ActionTrigger.ActionType actionType;

		// Token: 0x04001AD2 RID: 6866
		public float axisComparisonValue;

		// Token: 0x020004BB RID: 1211
		public enum ActionType
		{
			// Token: 0x04001DA7 RID: 7591
			Axis_GEqual,
			// Token: 0x04001DA8 RID: 7592
			Axis_LEqual,
			// Token: 0x04001DA9 RID: 7593
			Button_Down
		}
	}
}
