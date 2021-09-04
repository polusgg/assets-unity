using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class GamePhotoBehaviour : MonoBehaviour
{
	// Token: 0x06000149 RID: 329 RVA: 0x00009118 File Offset: 0x00007318
	public void Start()
	{
		base.transform.SetLocalZ(1f + this.zOffset);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00009131 File Offset: 0x00007331
	internal IEnumerator Pickup()
	{
		yield return Effects.All(new IEnumerator[]
		{
			Effects.Lerp(0.3f, delegate(float t)
			{
				base.transform.SetLocalZ(1f - t + this.zOffset);
			}),
			Effects.ScaleIn(base.transform, 1f, 1.2f, 0.3f),
			Effects.Sequence(new IEnumerator[]
			{
				Effects.Wait(0.15f),
				Effects.Action(delegate
				{
					this.TargetColor = Color.white;
				})
			})
		});
		yield break;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00009140 File Offset: 0x00007340
	internal IEnumerator Drop(bool inWater)
	{
		yield return Effects.All(new IEnumerator[]
		{
			Effects.Lerp(0.3f, delegate(float t)
			{
				this.transform.SetLocalZ(t + this.zOffset);
			}),
			Effects.ScaleIn(base.transform, 1.2f, 1f, 0.3f),
			Effects.Sequence(new IEnumerator[]
			{
				Effects.Wait(0.15f),
				Effects.Action(delegate
				{
					if (inWater)
					{
						this.TargetColor = GamePhotoBehaviour.InWaterPink;
					}
				})
			})
		});
		yield break;
	}

	// Token: 0x040001A2 RID: 418
	public static readonly Color InWaterPink = Color.Lerp(Color.white, new Color32(211, 106, 129, byte.MaxValue), 0.6f);

	// Token: 0x040001A3 RID: 419
	public float zOffset;

	// Token: 0x040001A4 RID: 420
	public SpriteRenderer Frame;

	// Token: 0x040001A5 RID: 421
	public SpriteRenderer Image;

	// Token: 0x040001A6 RID: 422
	public Collider2D Hitbox;

	// Token: 0x040001A7 RID: 423
	public Color TargetColor = Palette.ClearWhite;
}
