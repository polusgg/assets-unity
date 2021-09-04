using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
[RequireComponent(typeof(ParticleSystem))]
public class SpriteParticleSystem : MonoBehaviour
{
	// Token: 0x06000937 RID: 2359 RVA: 0x0003C81A File Offset: 0x0003AA1A
	private void OnEnable()
	{
		this.block = new MaterialPropertyBlock();
		this.ren = (base.GetComponent<Renderer>() as ParticleSystemRenderer);
		this.SetPropertyBlock();
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0003C840 File Offset: 0x0003AA40
	private void SetPropertyBlock()
	{
		if (this.block == null)
		{
			this.block = new MaterialPropertyBlock();
		}
		this.ren.GetPropertyBlock(this.block);
		this.block.SetTexture("_MainTex", this.sprite.texture);
		this.ren.SetPropertyBlock(this.block);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0003C89D File Offset: 0x0003AA9D
	private void OnValidate()
	{
		this.SetPropertyBlock();
	}

	// Token: 0x04000AA7 RID: 2727
	public Sprite sprite;

	// Token: 0x04000AA8 RID: 2728
	public ParticleSystemRenderer ren;

	// Token: 0x04000AA9 RID: 2729
	private MaterialPropertyBlock block;
}
