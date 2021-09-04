using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class TagAmbientSoundPlayer : MonoBehaviour
{
	// Token: 0x06000C91 RID: 3217 RVA: 0x0004D4E4 File Offset: 0x0004B6E4
	public void Start()
	{
		SoundManager.Instance.PlayDynamicSound(base.name + base.GetInstanceID().ToString(), this.AmbientSound, true, new DynamicSound.GetDynamicsFunction(this.Dynamics), false);
		base.StartCoroutine(this.Run());
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0004D536 File Offset: 0x0004B736
	private void Dynamics(AudioSource source, float dt)
	{
		source.volume = Mathf.Lerp(source.volume, this.targetVolume * this.MaxVolume, dt);
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0004D558 File Offset: 0x0004B758
	public void OnDestroy()
	{
		SoundManager.Instance.StopNamedSound(base.name + base.GetInstanceID().ToString());
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0004D588 File Offset: 0x0004B788
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
					if (buffer[i].tag == this.TargetTag)
					{
						flag = true;
						break;
					}
				}
				this.targetVolume = (float)(flag ? 0 : 1);
			}
		}
	}

	// Token: 0x04000E0E RID: 3598
	public AudioClip AmbientSound;

	// Token: 0x04000E0F RID: 3599
	public float MaxVolume = 1f;

	// Token: 0x04000E10 RID: 3600
	public string TargetTag = "NoSnow";

	// Token: 0x04000E11 RID: 3601
	private float targetVolume;
}
