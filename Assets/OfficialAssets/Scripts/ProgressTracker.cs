using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class ProgressTracker : MonoBehaviour
{
	// Token: 0x0600059A RID: 1434 RVA: 0x00024E51 File Offset: 0x00023051
	public void Start()
	{
		this.TileParent.material.SetFloat("_Buckets", 1f);
		this.TileParent.material.SetFloat("_FullBuckets", 0f);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00024E88 File Offset: 0x00023088
	public void FixedUpdate()
	{
		if (PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.TileParent.enabled = false;
			return;
		}
		if (!this.TileParent.enabled)
		{
			this.TileParent.enabled = true;
		}
		GameData instance = GameData.Instance;
		if (instance && instance.TotalTasks > 0)
		{
			int num = DestroyableSingleton<TutorialManager>.InstanceExists ? 1 : (instance.AllPlayers.Count - PlayerControl.GameOptions.NumImpostors);
			num -= instance.AllPlayers.Count((GameData.PlayerInfo p) => p.Disconnected);
			switch (PlayerControl.GameOptions.TaskBarMode)
			{
			case TaskBarMode.Normal:
				break;
			case TaskBarMode.MeetingOnly:
				if (!MeetingHud.Instance)
				{
					goto IL_108;
				}
				break;
			case TaskBarMode.Invisible:
				base.gameObject.SetActive(false);
				goto IL_108;
			default:
				goto IL_108;
			}
			float num2 = (float)instance.CompletedTasks / (float)instance.TotalTasks * (float)num;
			this.curValue = Mathf.Lerp(this.curValue, num2, Time.fixedDeltaTime * 2f);
			IL_108:
			this.TileParent.material.SetFloat("_Buckets", (float)num);
			this.TileParent.material.SetFloat("_FullBuckets", this.curValue);
		}
	}

	// Token: 0x04000637 RID: 1591
	public MeshRenderer TileParent;

	// Token: 0x04000638 RID: 1592
	private float curValue;
}
