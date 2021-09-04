using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Token: 0x0200012A RID: 298
public class PlayerParticles : MonoBehaviour
{
	// Token: 0x0600072B RID: 1835 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
	public void Start()
	{
		this.fill = new RandomFill<PlayerParticleInfo>();
		this.fill.Set(this.Sprites);
		int num = 0;
		while (this.pool.NotInUse > 0)
		{
			PlayerParticle playerParticle = this.pool.Get<PlayerParticle>();
			PlayerControl.SetPlayerMaterialColors(num++, playerParticle.myRend);
			this.PlacePlayer(playerParticle, true);
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0002D704 File Offset: 0x0002B904
	public void Update()
	{
		while (this.pool.NotInUse > 0)
		{
			PlayerParticle part = this.pool.Get<PlayerParticle>();
			this.PlacePlayer(part, false);
		}
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0002D738 File Offset: 0x0002B938
	private void PlacePlayer(PlayerParticle part, bool initial)
	{
		Vector3 vector = Random.insideUnitCircle;
		if (!initial)
		{
			vector.Normalize();
		}
		vector *= this.StartRadius;
		float num = this.scale.Next();
		part.transform.localScale = new Vector3(num, num, num);
		vector.z = -num * 0.001f;
		if (this.FollowCamera)
		{
			Vector3 position = this.FollowCamera.transform.position;
			position.z = 0f;
			vector += position;
			part.FollowCamera = this.FollowCamera;
		}
		part.transform.localPosition = vector;
		PlayerParticleInfo playerParticleInfo = this.fill.Get();
		part.myRend.sprite = playerParticleInfo.image;
		part.myRend.flipX = BoolRange.Next(0.5f);
		Vector2 vector2 = -vector.normalized;
		vector2 = vector2.Rotate(FloatRange.Next(-45f, 45f));
		part.velocity = vector2 * this.velocity.Next();
		part.angularVelocity = playerParticleInfo.angularVel.Next();
		if (playerParticleInfo.alignToVel)
		{
			part.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.up.AngleSigned(vector2));
		}
	}

	// Token: 0x04000816 RID: 2070
	public PlayerParticleInfo[] Sprites;

	// Token: 0x04000817 RID: 2071
	public FloatRange velocity;

	// Token: 0x04000818 RID: 2072
	public FloatRange scale;

	// Token: 0x04000819 RID: 2073
	public ObjectPoolBehavior pool;

	// Token: 0x0400081A RID: 2074
	public float StartRadius;

	// Token: 0x0400081B RID: 2075
	public Camera FollowCamera;

	// Token: 0x0400081C RID: 2076
	private RandomFill<PlayerParticleInfo> fill;
}
