using System;
using System.Collections;
using PowerTools;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class SteamBehaviour : MonoBehaviour
{
	// Token: 0x06000385 RID: 901 RVA: 0x000178D7 File Offset: 0x00015AD7
	public void OnEnable()
	{
		base.StartCoroutine(this.Run());
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000178E6 File Offset: 0x00015AE6
	private IEnumerator Run()
	{
		//for (;;)
		//{
		//	float time = this.PlayRate.Next();
		//	while (time > 0f)
		//	{
		//		time -= Time.deltaTime;
		//		yield return null;
		//	}
		//	this.anim.Play(null, 1f);
		//	while (this.anim.IsPlaying())
		//	{
		//		yield return null;
		//	}
		//}
		yield break;
	}

	// Token: 0x04000426 RID: 1062
	public SpriteAnim anim;

	// Token: 0x04000427 RID: 1063
	public FloatRange PlayRate = new FloatRange(0.5f, 1f);
}
