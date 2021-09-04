using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class SecurityLogger : MonoBehaviour
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x000438D4 File Offset: 0x00041AD4
	private void Awake()
	{
		this.filter = default(ContactFilter2D);
		this.filter.useLayerMask = true;
		this.filter.layerMask = Constants.PlayersOnlyMask;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00043904 File Offset: 0x00041B04
	public void FixedUpdate()
	{
		for (int j = 0; j < this.Timers.Length; j++)
		{
			this.Timers[j] -= Time.deltaTime;
		}
		if (PlayerControl.LocalPlayer && PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			return;
		}
		int num = this.Sensor.OverlapCollider(this.filter, SecurityLogger.hits);
		int i;
		int i2;
		for (i = 0; i < num; i = i2)
		{
			PlayerControl playerControl = PlayerControl.AllPlayerControls.FirstOrDefault((PlayerControl p) => p.Collider == SecurityLogger.hits[i]);
			if (playerControl && playerControl.Data != null && !playerControl.Data.IsDead && this.Timers[(int)playerControl.PlayerId] < 0f)
			{
				this.Timers[(int)playerControl.PlayerId] = this.Cooldown;
				this.LogParent.LogPlayer(playerControl, this.MyLocation);
				base.StopAllCoroutines();
				base.StartCoroutine(this.BlinkSensor());
			}
			i2 = i + 1;
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00043A19 File Offset: 0x00041C19
	private IEnumerator BlinkSensor()
	{
		yield return Effects.Wait(0.1f);
		this.Image.color = this.LogParent.BarColors[(int)((byte)this.MyLocation)];
		yield return Effects.Wait(0.1f);
		this.Image.color = new Color(1f, 1f, 1f, 0.5f);
		yield break;
	}

	// Token: 0x04000BFC RID: 3068
	private static Collider2D[] hits = new Collider2D[10];

	// Token: 0x04000BFD RID: 3069
	public SecurityLogBehaviour LogParent;

	// Token: 0x04000BFE RID: 3070
	public SecurityLogBehaviour.SecurityLogLocations MyLocation;

	// Token: 0x04000BFF RID: 3071
	public float Cooldown = 5f;

	// Token: 0x04000C00 RID: 3072
	public SpriteRenderer Image;

	// Token: 0x04000C01 RID: 3073
	public BoxCollider2D Sensor;

	// Token: 0x04000C02 RID: 3074
	private float[] Timers = new float[10];

	// Token: 0x04000C03 RID: 3075
	private ContactFilter2D filter;
}
