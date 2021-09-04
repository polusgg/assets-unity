using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class MapTaskOverlay : MonoBehaviour
{
	// Token: 0x060004F3 RID: 1267 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
	public void Show()
	{
		base.gameObject.SetActive(true);
		if (PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			return;
		}
		for (int i = 0; i < PlayerControl.LocalPlayer.myTasks.Count; i++)
		{
			try
			{
				PlayerTask playerTask = PlayerControl.LocalPlayer.myTasks[i];
				if (playerTask.HasLocation && !playerTask.IsComplete)
				{
					PooledMapIcon pooledMapIcon = this.icons.Get<PooledMapIcon>();
					pooledMapIcon.transform.localScale = new Vector3(pooledMapIcon.NormalSize, pooledMapIcon.NormalSize, pooledMapIcon.NormalSize);
					if (PlayerTask.TaskIsEmergency(playerTask))
					{
						pooledMapIcon.rend.color = Color.red;
						pooledMapIcon.alphaPulse.enabled = true;
						pooledMapIcon.rend.material.SetFloat("_Outline", 1f);
					}
					else
					{
						pooledMapIcon.rend.color = Color.yellow;
					}
					MapTaskOverlay.SetIconLocation(playerTask, pooledMapIcon);
					this.data.Add(playerTask, pooledMapIcon);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001F4DC File Offset: 0x0001D6DC
	public void Update()
	{
		if (PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			return;
		}
		for (int i = 0; i < PlayerControl.LocalPlayer.myTasks.Count; i++)
		{
			PlayerTask playerTask = PlayerControl.LocalPlayer.myTasks[i];
			if (playerTask.HasLocation && !playerTask.IsComplete && playerTask.LocationDirty)
			{
				PooledMapIcon pooledMapIcon;
				if (!this.data.TryGetValue(playerTask, out pooledMapIcon))
				{
					pooledMapIcon = this.icons.Get<PooledMapIcon>();
					pooledMapIcon.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
					if (PlayerTask.TaskIsEmergency(playerTask))
					{
						pooledMapIcon.rend.color = Color.red;
						pooledMapIcon.alphaPulse.enabled = true;
						pooledMapIcon.rend.material.SetFloat("_Outline", 1f);
					}
					else
					{
						pooledMapIcon.rend.color = Color.yellow;
					}
					this.data.Add(playerTask, pooledMapIcon);
				}
				MapTaskOverlay.SetIconLocation(playerTask, pooledMapIcon);
			}
		}
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001F5EC File Offset: 0x0001D7EC
	private static void SetIconLocation(PlayerTask task, PooledMapIcon mapIcon)
	{
		if (mapIcon.lastMapTaskStep != task.TaskStep)
		{
			mapIcon.lastMapTaskStep = task.TaskStep;
			Vector3 vector = task.Location;
			vector /= ShipStatus.Instance.MapScale;
			vector.z = -1f;
			mapIcon.name = task.name;
			mapIcon.transform.localPosition = vector;
			if (task.TaskStep > 0)
			{
				mapIcon.alphaPulse.enabled = true;
				mapIcon.rend.material.SetFloat("_Outline", 1f);
			}
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001F684 File Offset: 0x0001D884
	public void Hide()
	{
		foreach (KeyValuePair<PlayerTask, PooledMapIcon> keyValuePair in this.data)
		{
			keyValuePair.Value.OwnerPool.Reclaim(keyValuePair.Value);
		}
		this.data.Clear();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040005B2 RID: 1458
	public ObjectPoolBehavior icons;

	// Token: 0x040005B3 RID: 1459
	private Dictionary<PlayerTask, PooledMapIcon> data = new Dictionary<PlayerTask, PooledMapIcon>();
}
