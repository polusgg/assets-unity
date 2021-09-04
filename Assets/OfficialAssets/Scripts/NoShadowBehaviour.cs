using System;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class NoShadowBehaviour : MonoBehaviour
{
	// Token: 0x0600079F RID: 1951 RVA: 0x00030E22 File Offset: 0x0002F022
	public void Start()
	{
		LightSource.NoShadows.Add(base.gameObject, this);
		this.CalculateBoxEdgeCheckPoints();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00030E3C File Offset: 0x0002F03C
	private void CalculateBoxEdgeCheckPoints()
	{
		//this.bc = base.GetComponent<BoxCollider2D>();
		//if (this.bc)
		//{
		//	this.isBox = true;
		//	this.verticalBox = false;
		//	this.boxCheckPoints = new Vector2[8];
		//	Vector2 vector = this.bc.size * 0.5f;
		//	Vector2 vector2;
		//	vector2..ctor(vector.x, 0f);
		//	Vector2 vector3;
		//	vector3..ctor(0f, vector.y);
		//	if (vector.y > vector.x)
		//	{
		//		this.verticalBox = true;
		//		vector3 = vector2;
		//		vector2..ctor(0f, vector.y);
		//	}
		//	int i = 1;
		//	int num = 0;
		//	while (i < 5)
		//	{
		//		Vector2 vector4 = Vector2.Lerp(-vector2, vector2, (float)i / 5f);
		//		this.boxCheckPoints[num] = vector4 + vector3 + this.bc.offset;
		//		this.boxCheckPoints[num + 4] = vector4 - vector3 + this.bc.offset;
		//		i++;
		//		num++;
		//	}
		//}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00030F59 File Offset: 0x0002F159
	public void OnDestroy()
	{
		LightSource.NoShadows.Remove(base.gameObject);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00030F6C File Offset: 0x0002F16C
	private void LateUpdate()
	{
		if (!PlayerControl.LocalPlayer)
		{
			return;
		}
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		if (data == null || !data.IsDead)
		{
			if (this.didHit)
			{
				this.didHit = false;
				ShipStatus instance = ShipStatus.Instance;
				if (instance && instance.CalculateLightRadius(data) > instance.MaxLightRadius / 3f)
				{
					this.SetMaskFunction(8);
					return;
				}
			}
			this.SetMaskFunction(1);
			return;
		}
		this.SetMaskFunction(8);
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00030FE5 File Offset: 0x0002F1E5
	private void SetMaskFunction(int func)
	{
		this.rend.material.SetInt("_Mask", func);
		if (this.shadowChild)
		{
			this.shadowChild.material.SetInt("_Mask", func);
		}
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00031020 File Offset: 0x0002F220
	internal void CheckHit(float lightRadius, Vector2 lightPosition)
	{
		Vector2 vector = base.transform.position;
		if (Vector2.Distance(vector, lightPosition) < lightRadius + 1f)
		{
			if (this.hitOverride)
			{
				if (!PhysicsHelpers.AnythingBetween(lightPosition, vector, Constants.ShadowMask, false, this.hitOverride, base.transform))
				{
					this.didHit = true;
				}
			}
			else if (!PhysicsHelpers.AnythingBetween(lightPosition, vector, Constants.ShadowMask, false, null, base.transform))
			{
				this.didHit = true;
			}
			if (!this.didHit && this.isBox)
			{
				int num = 0;
				int num2 = 4;
				if (this.verticalBox)
				{
					if (lightPosition.x < base.transform.position.x)
					{
						num += 4;
						num2 += 4;
					}
				}
				else if (lightPosition.y < base.transform.position.y)
				{
					num += 4;
					num2 += 4;
				}
				if (this.hitOverride)
				{
					for (int i = num; i < num2; i++)
					{
						if (!PhysicsHelpers.AnythingBetween(lightPosition, base.transform.TransformPoint(this.boxCheckPoints[i]), Constants.ShadowMask, false, this.hitOverride, base.transform))
						{
							this.didHit = true;
							return;
						}
					}
					return;
				}
				for (int j = num; j < num2; j++)
				{
					if (!PhysicsHelpers.AnythingBetween(lightPosition, base.transform.TransformPoint(this.boxCheckPoints[j]), Constants.ShadowMask, false, this.bc, base.transform))
					{
						this.didHit = true;
						return;
					}
				}
			}
		}
	}

	// Token: 0x040008AA RID: 2218
	public Renderer rend;

	// Token: 0x040008AB RID: 2219
	public bool didHit;

	// Token: 0x040008AC RID: 2220
	public Renderer shadowChild;

	// Token: 0x040008AD RID: 2221
	public Collider2D hitOverride;

	// Token: 0x040008AE RID: 2222
	private BoxCollider2D bc;

	// Token: 0x040008AF RID: 2223
	private bool isBox;

	// Token: 0x040008B0 RID: 2224
	private bool verticalBox;

	// Token: 0x040008B1 RID: 2225
	private Vector2[] boxCheckPoints;

	// Token: 0x040008B2 RID: 2226
	private const int edgePoints = 4;

	// Token: 0x040008B3 RID: 2227
	private const int totalPointsPerEdge = 6;
}
