using System;
using System.Collections;

// Token: 0x02000024 RID: 36
public class AutoMultistageMinigame : Minigame
{
	// Token: 0x06000118 RID: 280 RVA: 0x00007228 File Offset: 0x00005428
	public override void Begin(PlayerTask task)
	{
		NormalPlayerTask normalPlayerTask = task as NormalPlayerTask;
		for (int i = 0; i < this.Stages.Length; i++)
		{
			this.Stages[i].gameObject.SetActive(i == normalPlayerTask.taskStep);
		}
		this.stage = this.Stages[normalPlayerTask.taskStep];
		this.stage.Console = base.Console;
		this.stage.Begin(task);
		Minigame.Instance = this;
		base.StartCoroutine(this.Run());
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000072AD File Offset: 0x000054AD
	private IEnumerator Run()
	{
		while (this.stage)
		{
			yield return null;
		}
		this.Close();
		yield break;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000072BC File Offset: 0x000054BC
	public override void Close()
	{
		Minigame.Instance = null;
		base.Close();
	}

	// Token: 0x04000124 RID: 292
	public Minigame[] Stages;

	// Token: 0x04000125 RID: 293
	private Minigame stage;
}
