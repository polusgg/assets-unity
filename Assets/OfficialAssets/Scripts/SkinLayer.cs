using System;
using PowerTools;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class SkinLayer : MonoBehaviour
{
	// Token: 0x170000AA RID: 170
	// (set) Token: 0x06000A08 RID: 2568 RVA: 0x000410D0 File Offset: 0x0003F2D0
	public bool Flipped
	{
		set
		{
			if (!this.skin)
			{
				this.layer.flipX = value;
				return;
			}
			this.layer.flipX = (value && this.animator.Clip != this.skin.IdleLeftAnim && this.animator.Clip != this.skin.RunLeftAnim && this.animator.Clip != this.skin.EnterLeftVentAnim && this.animator.Clip != this.skin.ExitLeftVentAnim);
		}
	}

	// Token: 0x170000AB RID: 171
	// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0004117A File Offset: 0x0003F37A
	public bool Visible
	{
		set
		{
			this.layer.enabled = value;
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x00041188 File Offset: 0x0003F388
	public void SetRun(bool isLeft)
	{
		if (!this.skin || !this.animator)
		{
			this.SetGhost();
			return;
		}
		if (isLeft && this.skin.RunLeftAnim)
		{
			if (!this.animator.IsPlaying(this.skin.RunLeftAnim))
			{
				this.animator.Play(this.skin.RunLeftAnim, 1f);
				this.animator.Time = 0.45833334f;
				return;
			}
		}
		else if (!this.animator.IsPlaying(this.skin.RunAnim))
		{
			this.animator.Play(this.skin.RunAnim, 1f);
			this.animator.Time = 0.45833334f;
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00041254 File Offset: 0x0003F454
	public void SetSpawn(float time = 0f)
	{
		if (!this.skin || !this.animator)
		{
			this.SetGhost();
			return;
		}
		this.animator.Play(this.skin.SpawnAnim, 1f);
		this.animator.Time = time;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x000412AC File Offset: 0x0003F4AC
	internal void SetClimb(bool down)
	{
		if (!this.skin || !this.animator)
		{
			this.SetGhost();
			return;
		}
		this.animator.Play(down ? this.skin.ClimbDownAnim : this.skin.ClimbAnim, 1f);
		this.animator.Time = 0f;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00041318 File Offset: 0x0003F518
	public void SetExitVent(bool isLeft)
	{
		if (!this.skin || !this.animator)
		{
			this.SetGhost();
			return;
		}
		if (isLeft && this.skin.ExitLeftVentAnim)
		{
			this.animator.Play(this.skin.ExitLeftVentAnim, 1f);
		}
		else
		{
			this.animator.Play(this.skin.ExitVentAnim, 1f);
		}
		this.animator.Time = 0f;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000413A4 File Offset: 0x0003F5A4
	public void SetEnterVent(bool isLeft)
	{
		if (!this.skin || !this.animator)
		{
			this.SetGhost();
			return;
		}
		if (isLeft && this.skin.EnterLeftVentAnim)
		{
			this.animator.Play(this.skin.EnterLeftVentAnim, 1f);
		}
		else
		{
			this.animator.Play(this.skin.EnterVentAnim, 1f);
		}
		this.animator.Time = 0f;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00041430 File Offset: 0x0003F630
	public void SetIdle(bool isLeft)
	{
		if (!this.skin || !this.animator)
		{
			this.SetGhost();
			return;
		}
		if (isLeft && this.skin.RunLeftAnim)
		{
			if (!this.animator.IsPlaying(this.skin.IdleLeftAnim))
			{
				this.animator.Play(this.skin.IdleLeftAnim, 1f);
				return;
			}
		}
		else if (!this.animator.IsPlaying(this.skin.IdleAnim))
		{
			this.animator.Play(this.skin.IdleAnim, 1f);
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000414DA File Offset: 0x0003F6DA
	public void SetGhost()
	{
		if (!this.animator)
		{
			return;
		}
		this.animator.Stop();
		this.layer.sprite = null;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00041501 File Offset: 0x0003F701
	internal void SetSkin(uint skinId, bool isLeft)
	{
		this.skin = DestroyableSingleton<HatManager>.Instance.GetSkinById(skinId);
		this.SetIdle(isLeft);
	}

	// Token: 0x04000B77 RID: 2935
	public SpriteRenderer layer;

	// Token: 0x04000B78 RID: 2936
	public SpriteAnim animator;

	// Token: 0x04000B79 RID: 2937
	public SkinData skin;
}
