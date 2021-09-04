using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class TaskFolder : MonoBehaviour
{
	// Token: 0x06000DA5 RID: 3493 RVA: 0x00052F1F File Offset: 0x0005111F
	private void Awake()
	{
		this.Button = base.GetComponent<PassiveButton>();
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00052F2D File Offset: 0x0005112D
	public void Start()
	{
		this.Text.Text = this.FolderName;
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x00052F40 File Offset: 0x00051140
	public void OnClick()
	{
		this.Parent.ShowFolder(this);
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00052F4E File Offset: 0x0005114E
	internal List<TaskFolder> OrderBy()
	{
		throw new NotImplementedException();
	}

	// Token: 0x040011B2 RID: 4530
	public string FolderName;

	// Token: 0x040011B3 RID: 4531
	public TextRenderer Text;

	// Token: 0x040011B4 RID: 4532
	public TaskAdderGame Parent;

	// Token: 0x040011B5 RID: 4533
	public List<TaskFolder> SubFolders = new List<TaskFolder>();

	// Token: 0x040011B6 RID: 4534
	public List<PlayerTask> Children = new List<PlayerTask>();

	// Token: 0x040011B7 RID: 4535
	[HideInInspector]
	public PassiveButton Button;
}
