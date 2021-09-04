using System;
using Assets.CoreScripts;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class Console : MonoBehaviour, IUsable
{
	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000480F2 File Offset: 0x000462F2
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.UseButton;
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000480F6 File Offset: 0x000462F6
	public float UsableDistance
	{
		get
		{
			return this.usableDistance;
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06000B52 RID: 2898 RVA: 0x000480FE File Offset: 0x000462FE
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00048108 File Offset: 0x00046308
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.yellow);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.yellow : Color.clear);
		}
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00048180 File Offset: 0x00046380
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		Vector2 truePosition = @object.GetTruePosition();
		Vector3 position = base.transform.position;
		couldUse = ((!pc.IsDead || (PlayerControl.GameOptions.GhostsDoTasks && !this.GhostsIgnored)) && @object.CanMove && (this.AllowImpostor || !pc.IsImpostor) && (!this.onlySameRoom || this.InRoom(truePosition)) && (!this.onlyFromBelow || truePosition.y < position.y) && this.FindTask(@object));
		canUse = couldUse;
		if (canUse)
		{
			num = Vector2.Distance(truePosition, base.transform.position);
			canUse &= (num <= this.UsableDistance);
			if (this.checkWalls)
			{
				canUse &= !PhysicsHelpers.AnythingBetween(truePosition, position, Constants.ShadowMask, false);
			}
		}
		return num;
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x0004826C File Offset: 0x0004646C
	private bool InRoom(Vector2 truePos)
	{
		PlainShipRoom plainShipRoom = ShipStatus.Instance.FastRooms[this.Room];
		if (!plainShipRoom || !plainShipRoom.roomArea)
		{
			return false;
		}
		bool result;
		try
		{
			result = plainShipRoom.roomArea.OverlapPoint(truePos);
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x000482CC File Offset: 0x000464CC
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

	// Token: 0x06000B57 RID: 2903 RVA: 0x00048310 File Offset: 0x00046510
	public virtual void Use()
	{
		bool flag;
		bool flag2;
		this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		PlayerControl localPlayer = PlayerControl.LocalPlayer;
		PlayerTask playerTask = this.FindTask(localPlayer);
		if (playerTask.MinigamePrefab)
		{
			//Minigame minigame = Object.Instantiate<Minigame>(playerTask.GetMinigamePrefab());
			//minigame.transform.SetParent(Camera.main.transform, false);
			//minigame.transform.localPosition = new Vector3(0f, 0f, -50f);
			//minigame.Console = this;
			//minigame.Begin(playerTask);
			//DestroyableSingleton<Telemetry>.Instance.WriteUse(localPlayer.PlayerId, playerTask.TaskType, base.transform.position);
		}
	}

	// Token: 0x04000CC4 RID: 3268
	public float usableDistance = 1f;

	// Token: 0x04000CC5 RID: 3269
	public int ConsoleId;

	// Token: 0x04000CC6 RID: 3270
	public bool onlyFromBelow;

	// Token: 0x04000CC7 RID: 3271
	public bool onlySameRoom;

	// Token: 0x04000CC8 RID: 3272
	public bool checkWalls;

	// Token: 0x04000CC9 RID: 3273
	public bool GhostsIgnored;

	// Token: 0x04000CCA RID: 3274
	public bool AllowImpostor;

	// Token: 0x04000CCB RID: 3275
	public SystemTypes Room;

	// Token: 0x04000CCC RID: 3276
	public TaskTypes[] TaskTypes;

	// Token: 0x04000CCD RID: 3277
	public TaskSet[] ValidTasks;

	// Token: 0x04000CCE RID: 3278
	public SpriteRenderer Image;
}
