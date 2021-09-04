using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class LeafBehaviour : MonoBehaviour
{
	// Token: 0x06000668 RID: 1640 RVA: 0x00029878 File Offset: 0x00027A78
	public void Start()
	{
		LeafBehaviour.ImageFiller.Set(this.Images);
		Sprite sprite = LeafBehaviour.ImageFiller.Get();
		if (!sprite)
		{
			LeafBehaviour.ImageFiller = new RandomFill<Sprite>();
			LeafBehaviour.ImageFiller.Set(this.Images);
			sprite = LeafBehaviour.ImageFiller.Get();
		}
		base.GetComponent<SpriteRenderer>().sprite = sprite;
		Debug.LogError(sprite);
		this.body = base.GetComponent<Rigidbody2D>();
		this.body.angularVelocity = this.SpinSpeed.Next();
		this.body.velocity = this.StartVel.Next();
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00029917 File Offset: 0x00027B17
	public void FixedUpdate()
	{
		if (!this.Held && (double)base.transform.localPosition.x < -2.5)
		{
			this.Parent.LeafDone(this);
		}
	}

	// Token: 0x0400073D RID: 1853
	public Sprite[] Images;

	// Token: 0x0400073E RID: 1854
	public FloatRange SpinSpeed = new FloatRange(-45f, 45f);

	// Token: 0x0400073F RID: 1855
	public Vector2Range StartVel;

	// Token: 0x04000740 RID: 1856
	public float AccelRate = 30f;

	// Token: 0x04000741 RID: 1857
	[HideInInspector]
	public LeafMinigame Parent;

	// Token: 0x04000742 RID: 1858
	public bool Held;

	// Token: 0x04000743 RID: 1859
	private static RandomFill<Sprite> ImageFiller = new RandomFill<Sprite>();

	// Token: 0x04000744 RID: 1860
	[HideInInspector]
	public Rigidbody2D body;
}
