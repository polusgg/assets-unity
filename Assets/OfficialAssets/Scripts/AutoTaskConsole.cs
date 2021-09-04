using System;
using Assets.CoreScripts;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class AutoTaskConsole : global::Console
{
	// Token: 0x06000B49 RID: 2889 RVA: 0x00047F08 File Offset: 0x00046108
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

	// Token: 0x06000B4A RID: 2890 RVA: 0x00047F4C File Offset: 0x0004614C
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

	// Token: 0x06000B4B RID: 2891 RVA: 0x00047FB0 File Offset: 0x000461B0
	protected virtual void AfterUse(NormalPlayerTask task)
	{
		if (this.useSound && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.useSound, false, 1f);
		}
		task.NextStep();
		this.Image.color = Palette.HalfWhite;
	}

	// Token: 0x04000CBF RID: 3263
	public AudioClip useSound;
}
