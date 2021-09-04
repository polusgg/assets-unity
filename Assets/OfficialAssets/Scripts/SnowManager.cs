using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class SnowManager : MonoBehaviour
{
	// Token: 0x060007B4 RID: 1972 RVA: 0x000314EA File Offset: 0x0002F6EA
	private void Start()
	{
		this.rend = base.GetComponent<ParticleSystemRenderer>();
		base.StartCoroutine(this.Run());
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00031505 File Offset: 0x0002F705
	private IEnumerator Run()
	{
		ContactFilter2D filter = default(ContactFilter2D);
		filter.layerMask = Constants.ShipOnlyMask;
		filter.useLayerMask = true;
		filter.useTriggers = true;
		Collider2D[] buffer = new Collider2D[10];
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		for (;;)
		{
			yield return wait;
			if (PlayerControl.LocalPlayer)
			{
				bool flag = false;
				int num = PlayerControl.LocalPlayer.Collider.OverlapCollider(filter, buffer);
				for (int i = 0; i < num; i++)
				{
					if (buffer[i].tag == "NoSnow")
					{
						flag = true;
					}
				}
				if (!this.particles.isPlaying)
				{
					if (!flag)
					{
						this.particles.Play();
					}
				}
				else if (flag)
				{
					this.timer = Mathf.Max(0f, this.timer - 0.2f);
				}
				else
				{
					this.timer = Mathf.Min(1f, this.timer + 0.2f);
				}
				this.SetPartAlpha();
			}
		}
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00031514 File Offset: 0x0002F714
	private void SetPartAlpha()
	{
		//Color color;
		//color..ctor(1f, 1f, 1f, this.timer);
		//this.rend.material.SetColor("_Color", color);
	}

	// Token: 0x040008BA RID: 2234
	public ParticleSystem particles;

	// Token: 0x040008BB RID: 2235
	private ParticleSystemRenderer rend;

	// Token: 0x040008BC RID: 2236
	private float timer;
}
