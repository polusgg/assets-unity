using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class MultistageMinigame : Minigame
{
	// Token: 0x06000388 RID: 904 RVA: 0x00017914 File Offset: 0x00015B14
	public override void Begin(PlayerTask task)
	{
		NormalPlayerTask normalPlayerTask = task as NormalPlayerTask;
		if (normalPlayerTask.TaskType == TaskTypes.FuelEngines)
		{
			this.stage = this.Stages[(int)normalPlayerTask.Data[1]];
		}
		else
		{
			this.stage = this.Stages[normalPlayerTask.taskStep];
		}
		this.stage.gameObject.SetActive(true);
		this.stage.Begin(task);
		Minigame.Instance = this;
		UiElement defaultSelection = null;
		foreach (UiElement uiElement in this.ControllerSelectable)
		{
			if (uiElement.isActiveAndEnabled)
			{
				defaultSelection = uiElement;
				break;
			}
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, defaultSelection, this.ControllerSelectable, false);
	}

	// Token: 0x06000389 RID: 905 RVA: 0x000179EC File Offset: 0x00015BEC
	public override void Close()
	{
		Minigame.Instance = null;
		this.stage.Close();
		base.Close();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x04000428 RID: 1064
	public Minigame[] Stages;

	// Token: 0x04000429 RID: 1065
	private Minigame stage;

	// Token: 0x0400042A RID: 1066
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400042B RID: 1067
	public List<UiElement> ControllerSelectable;
}
