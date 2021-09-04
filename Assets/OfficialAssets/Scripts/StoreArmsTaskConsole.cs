using System;
using Assets.CoreScripts;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class StoreArmsTaskConsole : global::Console
{
	// Token: 0x06000C2A RID: 3114 RVA: 0x0004BAAC File Offset: 0x00049CAC
	private PlayerTask FindTask(PlayerControl pc)
	{
		for (int i = 0; i < pc.myTasks.Count; i++)
		{
			PlayerTask playerTask = pc.myTasks[i];
			if (!playerTask.IsComplete && playerTask.ValidConsole(this))
			{
				return playerTask;
			}
		}
		return null;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0004BAF0 File Offset: 0x00049CF0
	public override void Use()
	{
		bool flag;
		bool flag2;
		base.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		PlayerControl localPlayer = PlayerControl.LocalPlayer;
		NormalPlayerTask normalPlayerTask = (NormalPlayerTask)this.FindTask(localPlayer);
		this.AfterUse(normalPlayerTask);
		DestroyableSingleton<Telemetry>.Instance.WriteUse(localPlayer.PlayerId, normalPlayerTask.TaskType, base.transform.position);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0004BB54 File Offset: 0x00049D54
	protected virtual void AfterUse(NormalPlayerTask task)
	{
		if (this.useSound && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.useSound, false, 1f);
		}
		int num = this.timesUsed + 1;
		this.timesUsed = num;
		if (num % this.usesPerStep == 0)
		{
			task.NextStep();
		}
		int num2 = this.timesUsed % this.Images.Length;
		this.Image.sprite = this.Images[num2];
	}

	// Token: 0x04000D8D RID: 3469
	public AudioClip useSound;

	// Token: 0x04000D8E RID: 3470
	public Sprite[] Images;

	// Token: 0x04000D8F RID: 3471
	public int usesPerStep = 2;

	// Token: 0x04000D90 RID: 3472
	private int timesUsed;
}
