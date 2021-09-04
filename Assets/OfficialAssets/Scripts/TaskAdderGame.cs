using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000246 RID: 582
public class TaskAdderGame : Minigame
{
	// Token: 0x06000D9C RID: 3484 RVA: 0x00052755 File Offset: 0x00050955
	private void OnEnable()
	{
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, null, this.ControllerSelectable, false);
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00052775 File Offset: 0x00050975
	private void OnDisable()
	{
		ControllerManager.Instance.ResetAll();
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x00052784 File Offset: 0x00050984
	public override void Begin(PlayerTask t)
	{
		base.Begin(t);
		this.Root = Object.Instantiate<TaskFolder>(this.RootFolderPrefab, base.transform);
		this.Root.gameObject.SetActive(false);
		Dictionary<SystemTypes, TaskFolder> folders = new Dictionary<SystemTypes, TaskFolder>();
		this.PopulateRoot(this.Root, folders, ShipStatus.Instance.CommonTasks);
		this.PopulateRoot(this.Root, folders, ShipStatus.Instance.LongTasks);
		this.PopulateRoot(this.Root, folders, ShipStatus.Instance.NormalTasks);
		this.Root.SubFolders = (from f in this.Root.SubFolders
		orderby f.FolderName
		select f).ToList<TaskFolder>();
		this.ShowFolder(this.Root);
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x00052858 File Offset: 0x00050A58
	private void PopulateRoot(TaskFolder rootFolder, Dictionary<SystemTypes, TaskFolder> folders, NormalPlayerTask[] taskList)
	{
		//foreach (NormalPlayerTask normalPlayerTask in taskList)
		//{
		//	SystemTypes systemTypes = normalPlayerTask.StartAt;
		//	if (normalPlayerTask is DivertPowerTask)
		//	{
		//		systemTypes = ((DivertPowerTask)normalPlayerTask).TargetSystem;
		//	}
		//	if (systemTypes == SystemTypes.LowerEngine)
		//	{
		//		systemTypes = SystemTypes.UpperEngine;
		//	}
		//	TaskFolder taskFolder;
		//	if (!folders.TryGetValue(systemTypes, out taskFolder))
		//	{
		//		taskFolder = (folders[systemTypes] = Object.Instantiate<TaskFolder>(this.RootFolderPrefab, base.transform));
		//		taskFolder.gameObject.SetActive(false);
		//		if (systemTypes == SystemTypes.UpperEngine)
		//		{
		//			taskFolder.FolderName = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Engines, Array.Empty<object>());
		//		}
		//		else
		//		{
		//			taskFolder.FolderName = DestroyableSingleton<TranslationController>.Instance.GetString(systemTypes);
		//		}
		//		rootFolder.SubFolders.Add(taskFolder);
		//	}
		//	taskFolder.Children.Add(normalPlayerTask);
		//}
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0005291D File Offset: 0x00050B1D
	public void GoToRoot()
	{
		this.Heirarchy.Clear();
		this.ShowFolder(this.Root);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00052938 File Offset: 0x00050B38
	public void GoUpOne()
	{
		if (this.Heirarchy.Count > 1)
		{
			TaskFolder taskFolder = this.Heirarchy[this.Heirarchy.Count - 2];
			this.Heirarchy.RemoveAt(this.Heirarchy.Count - 1);
			this.Heirarchy.RemoveAt(this.Heirarchy.Count - 1);
			this.ShowFolder(taskFolder);
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x000529A4 File Offset: 0x00050BA4
	public void ShowFolder(TaskFolder taskFolder)
	{
		StringBuilder stringBuilder = new StringBuilder(64);
		this.Heirarchy.Add(taskFolder);
		for (int i = 0; i < this.Heirarchy.Count; i++)
		{
			stringBuilder.Append(this.Heirarchy[i].FolderName);
			stringBuilder.Append("\\");
		}
		this.PathText.Text = stringBuilder.ToString();
		for (int j = 0; j < this.ActiveItems.Count; j++)
		{
			Object.Destroy(this.ActiveItems[j].gameObject);
		}
		this.ActiveItems.Clear();
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		for (int k = 0; k < taskFolder.SubFolders.Count; k++)
		{
			TaskFolder taskFolder2 = Object.Instantiate<TaskFolder>(taskFolder.SubFolders[k], this.TaskParent);
			taskFolder2.gameObject.SetActive(true);
			taskFolder2.Parent = this;
			taskFolder2.transform.localPosition = new Vector3(num, num2, 0f);
			taskFolder2.transform.localScale = Vector3.one;
			taskFolder2.Text.RefreshMesh();
			num3 = Mathf.Max(num3, taskFolder2.Text.Height + 1.1f);
			num += this.folderWidth;
			if (num > this.lineWidth)
			{
				num = 0f;
				num2 -= num3;
				num3 = 0f;
			}
			this.ActiveItems.Add(taskFolder2.transform);
			if (taskFolder2 != null && taskFolder2.Button != null)
			{
				ControllerManager.Instance.AddSelectableUiElement(taskFolder2.Button, false);
				if (!string.IsNullOrEmpty(this.restorePreviousSelectionByFolderName) && taskFolder2.FolderName.Equals(this.restorePreviousSelectionByFolderName))
				{
					this.restorePreviousSelectionFound = taskFolder2.Button;
				}
			}
		}
		bool flag = false;
		List<PlayerTask> list = (from t in taskFolder.Children
		orderby t.TaskType
		select t).ToList<PlayerTask>();
		for (int l = 0; l < list.Count; l++)
		{
			TaskAddButton taskAddButton = Object.Instantiate<TaskAddButton>(this.TaskPrefab);
			taskAddButton.MyTask = list[l];
			if (taskAddButton.MyTask.TaskType == TaskTypes.DivertPower)
			{
				SystemTypes targetSystem = ((DivertPowerTask)taskAddButton.MyTask).TargetSystem;
				taskAddButton.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DivertPowerTo, new object[]
				{
					DestroyableSingleton<TranslationController>.Instance.GetString(targetSystem)
				});
			}
			else if (taskAddButton.MyTask.TaskType == TaskTypes.ActivateWeatherNodes)
			{
				int nodeId = ((WeatherNodeTask)taskAddButton.MyTask).NodeId;
				taskAddButton.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.FixWeatherNode, Array.Empty<object>()) + " " + DestroyableSingleton<TranslationController>.Instance.GetString(WeatherSwitchGame.ControlNames[nodeId], Array.Empty<object>());
			}
			else
			{
				taskAddButton.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(taskAddButton.MyTask.TaskType);
			}
			this.AddFileAsChild(taskFolder, taskAddButton, ref num, ref num2, ref num3);
			if (taskAddButton != null && taskAddButton.Button != null)
			{
				ControllerManager.Instance.AddSelectableUiElement(taskAddButton.Button, false);
				if (this.Heirarchy.Count != 1 && !flag)
				{
					TaskFolder component = ControllerManager.Instance.CurrentUiState.CurrentSelection.GetComponent<TaskFolder>();
					if (component != null)
					{
						this.restorePreviousSelectionByFolderName = component.FolderName;
					}
					ControllerManager.Instance.SetDefaultSelection(taskAddButton.Button, null);
					flag = true;
				}
			}
		}
		if (this.Heirarchy.Count == 1)
		{
			TaskAddButton taskAddButton2 = Object.Instantiate<TaskAddButton>(this.InfectedButton);
			taskAddButton2.Text.Text = "Be_Impostor.exe";
			this.AddFileAsChild(this.Root, taskAddButton2, ref num, ref num2, ref num3);
			if (taskAddButton2 != null && taskAddButton2.Button != null)
			{
				ControllerManager.Instance.AddSelectableUiElement(taskAddButton2.Button, false);
				if (this.restorePreviousSelectionFound != null)
				{
					ControllerManager.Instance.SetDefaultSelection(this.restorePreviousSelectionFound, null);
					this.restorePreviousSelectionByFolderName = string.Empty;
					this.restorePreviousSelectionFound = null;
				}
				else
				{
					ControllerManager.Instance.SetDefaultSelection(taskAddButton2.Button, null);
				}
			}
		}
		if (this.Heirarchy.Count == 1)
		{
			ControllerManager.Instance.SetBackButton(this.BackButton);
			return;
		}
		ControllerManager.Instance.SetBackButton(this.FolderBackButton);
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00052E48 File Offset: 0x00051048
	private void AddFileAsChild(TaskFolder taskFolder, TaskAddButton item, ref float xCursor, ref float yCursor, ref float maxHeight)
	{
		item.transform.SetParent(this.TaskParent);
		item.transform.localPosition = new Vector3(xCursor, yCursor, 0f);
		item.transform.localScale = Vector3.one;
		item.Text.RefreshMesh();
		maxHeight = Mathf.Max(maxHeight, item.Text.Height + 1.1f);
		xCursor += this.fileWidth;
		if (xCursor > this.lineWidth)
		{
			xCursor = 0f;
			yCursor -= maxHeight;
			maxHeight = 0f;
		}
		this.ActiveItems.Add(item.transform);
	}

	// Token: 0x040011A2 RID: 4514
	public TextRenderer PathText;

	// Token: 0x040011A3 RID: 4515
	public TaskFolder RootFolderPrefab;

	// Token: 0x040011A4 RID: 4516
	public TaskAddButton TaskPrefab;

	// Token: 0x040011A5 RID: 4517
	public Transform TaskParent;

	// Token: 0x040011A6 RID: 4518
	public List<TaskFolder> Heirarchy = new List<TaskFolder>();

	// Token: 0x040011A7 RID: 4519
	public List<Transform> ActiveItems = new List<Transform>();

	// Token: 0x040011A8 RID: 4520
	public TaskAddButton InfectedButton;

	// Token: 0x040011A9 RID: 4521
	public float folderWidth;

	// Token: 0x040011AA RID: 4522
	public float fileWidth;

	// Token: 0x040011AB RID: 4523
	public float lineWidth;

	// Token: 0x040011AC RID: 4524
	private TaskFolder Root;

	// Token: 0x040011AD RID: 4525
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040011AE RID: 4526
	public UiElement FolderBackButton;

	// Token: 0x040011AF RID: 4527
	public List<UiElement> ControllerSelectable;

	// Token: 0x040011B0 RID: 4528
	private string restorePreviousSelectionByFolderName = string.Empty;

	// Token: 0x040011B1 RID: 4529
	public UiElement restorePreviousSelectionFound;
}
