using System;

// Token: 0x0200022B RID: 555
[Serializable]
public class TaskSet
{
	// Token: 0x06000D52 RID: 3410 RVA: 0x00051027 File Offset: 0x0004F227
	public bool Contains(PlayerTask t)
	{
		return t.TaskType == this.taskType && this.taskStep.Contains(t.TaskStep);
	}

	// Token: 0x04000E97 RID: 3735
	public TaskTypes taskType;

	// Token: 0x04000E98 RID: 3736
	public IntRange taskStep = new IntRange(0, 0);
}
