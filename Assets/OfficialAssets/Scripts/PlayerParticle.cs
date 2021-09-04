using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class PlayerParticle : PoolableBehavior
{
	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000726 RID: 1830 RVA: 0x0002D57B File Offset: 0x0002B77B
	// (set) Token: 0x06000727 RID: 1831 RVA: 0x0002D583 File Offset: 0x0002B783
	public Camera FollowCamera { get; set; }

	// Token: 0x06000728 RID: 1832 RVA: 0x0002D58C File Offset: 0x0002B78C
	public void Update()
	{
		//Vector3 vector = base.transform.localPosition;
		//float sqrMagnitude = vector.sqrMagnitude;
		//if (this.FollowCamera)
		//{
		//	Vector3 position = this.FollowCamera.transform.position;
		//	position.z = 0f;
		//	vector += (position - this.lastCamera) * (1f - base.transform.localScale.x);
		//	this.lastCamera = position;
		//	sqrMagnitude = (vector - position).sqrMagnitude;
		//}
		//if (sqrMagnitude > this.maxDistance * this.maxDistance)
		//{
		//	this.OwnerPool.Reclaim(this);
		//	return;
		//}
		//vector += this.velocity * Time.deltaTime;
		//base.transform.localPosition = vector;
		//base.transform.Rotate(0f, 0f, Time.deltaTime * this.angularVelocity);
	}

	// Token: 0x0400080D RID: 2061
	public SpriteRenderer myRend;

	// Token: 0x0400080E RID: 2062
	public float maxDistance = 6f;

	// Token: 0x0400080F RID: 2063
	public Vector2 velocity;

	// Token: 0x04000810 RID: 2064
	public float angularVelocity;

	// Token: 0x04000812 RID: 2066
	private Vector3 lastCamera;
}
