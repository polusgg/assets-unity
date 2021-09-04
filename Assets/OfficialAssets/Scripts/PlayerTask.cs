using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000228 RID: 552
public abstract class PlayerTask : MonoBehaviour
{
	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00050B7D File Offset: 0x0004ED7D
	// (set) Token: 0x06000D2E RID: 3374 RVA: 0x00050B85 File Offset: 0x0004ED85
	public int Index { get; internal set; }

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00050B8E File Offset: 0x0004ED8E
	// (set) Token: 0x06000D30 RID: 3376 RVA: 0x00050B96 File Offset: 0x0004ED96
	public uint Id { get; internal set; }

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00050B9F File Offset: 0x0004ED9F
	// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00050BA7 File Offset: 0x0004EDA7
	public PlayerControl Owner { get; internal set; }

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000D33 RID: 3379
	public abstract int TaskStep { get; }

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000D34 RID: 3380
	public abstract bool IsComplete { get; }

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00050BB0 File Offset: 0x0004EDB0
	public Vector2 Location
	{
		get
		{
			this.LocationDirty = false;
			return this.FindObjectPos().transform.position;
		}
	}

	// Token: 0x06000D36 RID: 3382
	public abstract void Initialize();

	// Token: 0x06000D37 RID: 3383 RVA: 0x00050BCE File Offset: 0x0004EDCE
	public virtual void OnRemove()
	{
	}

	// Token: 0x06000D38 RID: 3384
	public abstract bool ValidConsole(global::Console console);

	// Token: 0x06000D39 RID: 3385
	public abstract void Complete();

	// Token: 0x06000D3A RID: 3386
	public abstract void AppendTaskText(StringBuilder sb);

	// Token: 0x06000D3B RID: 3387 RVA: 0x00050BD0 File Offset: 0x0004EDD0
	internal static bool TaskIsEmergency(PlayerTask arg)
	{
		return arg is NoOxyTask || arg is IHudOverrideTask || arg is ReactorTask || arg is ElectricTask;
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x00050BF8 File Offset: 0x0004EDF8
	protected List<global::Console> FindConsoles()
	{
		List<global::Console> list = new List<global::Console>();
		global::Console[] allConsoles = ShipStatus.Instance.AllConsoles;
		for (int i = 0; i < allConsoles.Length; i++)
		{
			if (this.ValidConsole(allConsoles[i]))
			{
				list.Add(allConsoles[i]);
			}
		}
		return list;
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x00050C3C File Offset: 0x0004EE3C
	public static bool PlayerHasTaskOfType<T>(PlayerControl localPlayer)
	{
		if (!localPlayer)
		{
			return true;
		}
		for (int i = 0; i < localPlayer.myTasks.Count; i++)
		{
			if (localPlayer.myTasks[i] is T)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x00050C80 File Offset: 0x0004EE80
	protected List<Vector2> FindValidConsolesPositions()
	{
		List<Vector2> list = new List<Vector2>();
		global::Console[] allConsoles = ShipStatus.Instance.AllConsoles;
		for (int i = 0; i < allConsoles.Length; i++)
		{
			if (this.ValidConsole(allConsoles[i]))
			{
				list.Add(allConsoles[i].transform.position);
			}
		}
		return list;
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x00050CD0 File Offset: 0x0004EED0
	protected global::Console FindSpecialConsole(Func<global::Console, bool> func)
	{
		global::Console[] allConsoles = ShipStatus.Instance.AllConsoles;
		for (int i = 0; i < allConsoles.Length; i++)
		{
			if (func(allConsoles[i]))
			{
				return allConsoles[i];
			}
		}
		return null;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00050D08 File Offset: 0x0004EF08
	protected global::Console FindObjectPos()
	{
		global::Console[] allConsoles = ShipStatus.Instance.AllConsoles;
		for (int i = 0; i < allConsoles.Length; i++)
		{
			if (this.ValidConsole(allConsoles[i]))
			{
				return allConsoles[i];
			}
		}
		Debug.LogError("Couldn't find location for task: " + base.name);
		return null;
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00050D53 File Offset: 0x0004EF53
	public virtual Minigame GetMinigamePrefab()
	{
		return this.MinigamePrefab;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x00050D5C File Offset: 0x0004EF5C
	protected static bool AllTasksCompleted(PlayerControl player)
	{
		for (int i = 0; i < player.myTasks.Count; i++)
		{
			PlayerTask playerTask = player.myTasks[i];
			if (playerTask is NormalPlayerTask && !playerTask.IsComplete)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000E8D RID: 3725
	public SystemTypes StartAt;

	// Token: 0x04000E8E RID: 3726
	public TaskTypes TaskType;

	// Token: 0x04000E8F RID: 3727
	public Minigame MinigamePrefab;

	// Token: 0x04000E90 RID: 3728
	public bool HasLocation;

	// Token: 0x04000E91 RID: 3729
	public bool LocationDirty = true;
}
