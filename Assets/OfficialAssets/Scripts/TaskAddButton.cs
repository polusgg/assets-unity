using System;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class TaskAddButton : MonoBehaviour
{
	// Token: 0x06000D97 RID: 3479 RVA: 0x000524DC File Offset: 0x000506DC
	private void Awake()
	{
		this.Button = base.GetComponent<PassiveButton>();
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x000524EC File Offset: 0x000506EC
	public void Start()
	{
		if (this.ImpostorTask)
		{
			GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
			this.Overlay.enabled = data.IsImpostor;
			this.Overlay.sprite = this.CheckImage;
			return;
		}
		PlayerTask playerTask = this.FindTaskByType();
		if (playerTask)
		{
			this.Overlay.enabled = true;
			this.Overlay.sprite = (playerTask.IsComplete ? this.CheckImage : this.ExImage);
			return;
		}
		this.Overlay.enabled = false;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00052578 File Offset: 0x00050778
	public void AddTask()
	{
		//if (this.ImpostorTask)
		//{
		//	GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		//	if (data.IsImpostor)
		//	{
		//		PlayerControl.LocalPlayer.RemoveInfected();
		//		this.Overlay.enabled = false;
		//		return;
		//	}
		//	PlayerControl.LocalPlayer.RpcSetInfected(new GameData.PlayerInfo[]
		//	{
		//		data
		//	});
		//	this.Overlay.enabled = true;
		//	return;
		//}
		//else
		//{
		//	PlayerTask playerTask = this.FindTaskByType();
		//	if (!playerTask)
		//	{
		//		PlayerTask playerTask2 = Object.Instantiate<PlayerTask>(this.MyTask, PlayerControl.LocalPlayer.transform);
		//		PlayerControl.LocalPlayer.myTasks.Add(playerTask2);
		//		playerTask2.Id = GameData.Instance.TutOnlyAddTask(PlayerControl.LocalPlayer.PlayerId);
		//		playerTask2.Owner = PlayerControl.LocalPlayer;
		//		playerTask2.Initialize();
		//		this.Overlay.sprite = this.ExImage;
		//		this.Overlay.enabled = true;
		//		return;
		//	}
		//	PlayerControl.LocalPlayer.RemoveTask(playerTask);
		//	this.Overlay.enabled = false;
		//	return;
		//}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x00052670 File Offset: 0x00050870
	private PlayerTask FindTaskByType()
	{
		for (int i = PlayerControl.LocalPlayer.myTasks.Count - 1; i > -1; i--)
		{
			PlayerTask playerTask = PlayerControl.LocalPlayer.myTasks[i];
			if (playerTask.TaskType == this.MyTask.TaskType)
			{
				if (playerTask.TaskType == TaskTypes.DivertPower)
				{
					if (((DivertPowerTask)playerTask).TargetSystem == ((DivertPowerTask)this.MyTask).TargetSystem)
					{
						return playerTask;
					}
				}
				else if (playerTask.TaskType == TaskTypes.EmptyGarbage || playerTask.TaskType == TaskTypes.RecordTemperature || playerTask.TaskType == TaskTypes.UploadData)
				{
					if (playerTask.StartAt == this.MyTask.StartAt)
					{
						return playerTask;
					}
				}
				else
				{
					if (playerTask.TaskType != TaskTypes.ActivateWeatherNodes)
					{
						return playerTask;
					}
					if (((WeatherNodeTask)playerTask).NodeId == ((WeatherNodeTask)this.MyTask).NodeId)
					{
						return playerTask;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0400119B RID: 4507
	public TextRenderer Text;

	// Token: 0x0400119C RID: 4508
	public SpriteRenderer Overlay;

	// Token: 0x0400119D RID: 4509
	public Sprite CheckImage;

	// Token: 0x0400119E RID: 4510
	public Sprite ExImage;

	// Token: 0x0400119F RID: 4511
	public PlayerTask MyTask;

	// Token: 0x040011A0 RID: 4512
	public bool ImpostorTask;

	// Token: 0x040011A1 RID: 4513
	[HideInInspector]
	public PassiveButton Button;
}
