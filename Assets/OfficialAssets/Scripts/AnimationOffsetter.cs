using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class AnimationOffsetter : MonoBehaviour
{
	// Token: 0x060007AD RID: 1965 RVA: 0x000312EA File Offset: 0x0002F4EA
	private void Start()
	{
		this.anim.speed = FloatRange.Next(0.9f, 1.1f);
	}

	// Token: 0x040008B8 RID: 2232
	public Animator anim;
}
