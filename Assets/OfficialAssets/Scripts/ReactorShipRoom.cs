using System;
using PowerTools;
using UnityEngine;

// Token: 0x020001FC RID: 508
public class ReactorShipRoom : SkeldShipRoom
{
	// Token: 0x06000BE7 RID: 3047 RVA: 0x0004A08C File Offset: 0x0004828C
	public void StartMeltdown()
	{
		if (this.Manifolds)
		{
			this.Manifolds.sprite = this.meltdownManifolds;
		}
		if (this.Reactor)
		{
			this.Reactor.Play(this.meltdownReactor, 1f);
		}
		if (this.HighFloor)
		{
			this.HighFloor.Play(this.meltdownHighFloor, 1f);
		}
		if (this.MidFloor1)
		{
			this.MidFloor1.Play(this.meltdownMidFloor, 1f);
		}
		if (this.MidFloor2)
		{
			this.MidFloor2.Play(this.meltdownMidFloor, 1f);
		}
		if (this.LowFloor)
		{
			this.LowFloor.Play(this.meltdownLowFloor, 1f);
		}
		for (int i = 0; i < this.Pipes.Length; i++)
		{
			this.Pipes[i].Play(this.meltdownPipes[i], 1f);
		}
		for (int j = 0; j < this.Supressors.Length; j++)
		{
			this.Supressors[j].Deactivate();
		}
		if (this.Orb)
		{
			this.Orb.material.SetColor("_Color", Color.red);
			this.Orb.material.SetFloat("_Speed", 3f);
		}
		if (this.OrbGlass)
		{
			this.OrbGlass.DegreesPerSecond = 1440f;
		}
		for (int k = 0; k < this.Arms.Length; k++)
		{
			this.Arms[k].SwingRange.min = -0.75f;
			this.Arms[k].SwingRange.max = 0.75f;
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0004A254 File Offset: 0x00048454
	public void StopMeltdown()
	{
		if (this.Manifolds)
		{
			this.Manifolds.sprite = this.normalManifolds;
		}
		if (this.Reactor)
		{
			this.Reactor.Play(this.normalReactor, 1f);
		}
		if (this.HighFloor)
		{
			this.HighFloor.Play(this.normalHighFloor, 1f);
		}
		if (this.MidFloor1)
		{
			this.MidFloor1.Play(this.normalMidFloor, 1f);
		}
		if (this.MidFloor2)
		{
			this.MidFloor2.Play(this.normalMidFloor, 1f);
		}
		if (this.LowFloor)
		{
			this.LowFloor.Play(this.normalLowFloor, 1f);
		}
		for (int i = 0; i < this.Pipes.Length; i++)
		{
			this.Pipes[i].Play(this.normalPipes[i], 1f);
		}
		for (int j = 0; j < this.Supressors.Length; j++)
		{
			this.Supressors[j].Activate();
		}
		if (this.Orb)
		{
			this.Orb.material.SetColor("_Color", Color.white);
			this.Orb.material.SetFloat("_Speed", 1f);
		}
		if (this.OrbGlass)
		{
			this.OrbGlass.DegreesPerSecond = 720f;
		}
		for (int k = 0; k < this.Arms.Length; k++)
		{
			this.Arms[k].SwingRange.min = -0.15f;
			this.Arms[k].SwingRange.max = 0.15f;
		}
	}

	// Token: 0x04000D37 RID: 3383
	public Sprite normalManifolds;

	// Token: 0x04000D38 RID: 3384
	public Sprite meltdownManifolds;

	// Token: 0x04000D39 RID: 3385
	public SpriteRenderer Manifolds;

	// Token: 0x04000D3A RID: 3386
	public AnimationClip normalReactor;

	// Token: 0x04000D3B RID: 3387
	public AnimationClip meltdownReactor;

	// Token: 0x04000D3C RID: 3388
	public SpriteAnim Reactor;

	// Token: 0x04000D3D RID: 3389
	public AnimationClip normalHighFloor;

	// Token: 0x04000D3E RID: 3390
	public AnimationClip meltdownHighFloor;

	// Token: 0x04000D3F RID: 3391
	public SpriteAnim HighFloor;

	// Token: 0x04000D40 RID: 3392
	public AnimationClip normalMidFloor;

	// Token: 0x04000D41 RID: 3393
	public AnimationClip meltdownMidFloor;

	// Token: 0x04000D42 RID: 3394
	public SpriteAnim MidFloor1;

	// Token: 0x04000D43 RID: 3395
	public SpriteAnim MidFloor2;

	// Token: 0x04000D44 RID: 3396
	public AnimationClip normalLowFloor;

	// Token: 0x04000D45 RID: 3397
	public AnimationClip meltdownLowFloor;

	// Token: 0x04000D46 RID: 3398
	public SpriteAnim LowFloor;

	// Token: 0x04000D47 RID: 3399
	public AnimationClip[] normalPipes;

	// Token: 0x04000D48 RID: 3400
	public AnimationClip[] meltdownPipes;

	// Token: 0x04000D49 RID: 3401
	public SpriteAnim[] Pipes;

	// Token: 0x04000D4A RID: 3402
	public SupressorBehaviour[] Supressors;

	// Token: 0x04000D4B RID: 3403
	public MeshRenderer Orb;

	// Token: 0x04000D4C RID: 3404
	public Rotater OrbGlass;

	// Token: 0x04000D4D RID: 3405
	public ChainBehaviour[] Arms;
}
