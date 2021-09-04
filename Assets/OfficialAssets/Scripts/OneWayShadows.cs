using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class OneWayShadows : MonoBehaviour
{
	// Token: 0x06000B3C RID: 2876 RVA: 0x00047B02 File Offset: 0x00045D02
	public void OnEnable()
	{
		LightSource.OneWayShadows.Add(base.gameObject, this);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00047B15 File Offset: 0x00045D15
	public void OnDisable()
	{
		LightSource.OneWayShadows.Remove(base.gameObject);
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00047B28 File Offset: 0x00045D28
	public bool IsIgnored(LightSource lightSource)
	{
		return (this.IgnoreImpostor && PlayerControl.LocalPlayer.Data.IsImpostor) || this.RoomCollider.OverlapPoint(lightSource.transform.position);
	}

	// Token: 0x04000CB2 RID: 3250
	public Collider2D RoomCollider;

	// Token: 0x04000CB3 RID: 3251
	public bool IgnoreImpostor;
}
