using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000088 RID: 136
public class DivertPowerMetagame : Minigame
{
	// Token: 0x06000349 RID: 841 RVA: 0x00015524 File Offset: 0x00013724
	public override void Begin(PlayerTask task)
	{
		return;
		/*
		base.Begin(task);
		Minigame minigame;
		if (this.MyNormTask.taskStep == 0)
		{
			minigame = Object.Instantiate<Minigame>(this.DistributePrefab, base.transform.parent);
		}
		else
		{
			minigame = Object.Instantiate<Minigame>(this.ReceivePrefab, base.transform.parent);
		}
		minigame.Begin(task);
		Object.Destroy(base.gameObject);
		*/
	}

	public void Awake()
	{
		foreach (Minigame minigame in GetComponents<Minigame>().Where(t => t != this))
		{
			minigame.TransType = TransType;
			minigame.OpenSound = OpenSound;
			minigame.CloseSound = CloseSound;
		}
	}

	// Token: 0x040003BF RID: 959
	public Minigame DistributePrefab;

	// Token: 0x040003C0 RID: 960
	public Minigame ReceivePrefab;
}
