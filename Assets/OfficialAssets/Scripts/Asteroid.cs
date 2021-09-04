using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class Asteroid : PoolableBehavior
{
	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00053103 File Offset: 0x00051303
	// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0005310B File Offset: 0x0005130B
	public Vector3 TargetPosition { get; internal set; }

	// Token: 0x06000DBC RID: 3516 RVA: 0x00053114 File Offset: 0x00051314
	public void FixedUpdate()
	{
		base.transform.localRotation = Quaternion.Euler(0f, 0f, base.transform.localRotation.eulerAngles.z + this.RotateSpeed.Last * Time.fixedDeltaTime);
		Vector3 vector = this.TargetPosition - base.transform.localPosition;
		if (vector.sqrMagnitude > 0.05f)
		{
			vector.Normalize();
			base.transform.localPosition += vector * this.MoveSpeed.Last * Time.fixedDeltaTime;
			return;
		}
		this.OwnerPool.Reclaim(this);
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x000531D0 File Offset: 0x000513D0
	public override void Reset()
	{
		base.enabled = true;
		this.Explosion.enabled = false;
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		this.imgIdx = this.AsteroidImages.RandomIdx<Sprite>();
		component.sprite = this.AsteroidImages[this.imgIdx];
		component.enabled = true;
		ButtonBehavior component2 = base.GetComponent<ButtonBehavior>();
		component2.enabled = true;
		component2.OnClick.RemoveAllListeners();
		base.transform.Rotate(0f, 0f, this.RotateSpeed.Next());
		this.MoveSpeed.Next();
		base.Reset();
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00053269 File Offset: 0x00051469
	public IEnumerator CoBreakApart()
	{
		base.enabled = false;
		base.GetComponent<ButtonBehavior>().enabled = false;
		VibrationManager.Vibrate(0.5f, 0.5f, 0.35f, VibrationManager.VibrationFalloff.Linear, null, false);
		this.Explosion.enabled = true;
		yield return new WaitForLerp(0.1f, delegate(float t)
		{
			this.Explosion.transform.localScale = new Vector3(t, t, t);
		});
		yield return new WaitForSeconds(0.05f);
		yield return new WaitForLerp(0.05f, delegate(float t)
		{
			this.Explosion.transform.localScale = new Vector3(1f - t, 1f - t, 1f - t);
		});
		SpriteRenderer rend = base.GetComponent<SpriteRenderer>();
		yield return null;
		rend.sprite = this.BrokenImages[this.imgIdx];
		yield return new WaitForSeconds(0.2f);
		this.OwnerPool.Reclaim(this);
		yield break;
	}

	// Token: 0x040011BE RID: 4542
	public Sprite[] AsteroidImages;

	// Token: 0x040011BF RID: 4543
	public Sprite[] BrokenImages;

	// Token: 0x040011C0 RID: 4544
	private int imgIdx;

	// Token: 0x040011C1 RID: 4545
	public FloatRange MoveSpeed = new FloatRange(2f, 5f);

	// Token: 0x040011C2 RID: 4546
	public FloatRange RotateSpeed = new FloatRange(-10f, 10f);

	// Token: 0x040011C4 RID: 4548
	public SpriteRenderer Explosion;
}
