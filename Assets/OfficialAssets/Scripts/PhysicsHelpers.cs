using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public static class PhysicsHelpers
{
	// Token: 0x0600025F RID: 607 RVA: 0x000102A2 File Offset: 0x0000E4A2
	public static bool CircleContains(Vector2 source, float radius, int layerMask)
	{
		return Physics2D.OverlapCircleNonAlloc(source, radius, PhysicsHelpers.colliderHits, layerMask) > 0;
	}

	// Token: 0x06000260 RID: 608 RVA: 0x000102B4 File Offset: 0x0000E4B4
	public static bool AnyEdgeTriggerBetween(Vector2 source, Vector2 target, int layerMask)
	{
		PhysicsHelpers.filter.layerMask = layerMask;
		PhysicsHelpers.filter.useTriggers = true;
		PhysicsHelpers.temp.x = target.x - source.x;
		PhysicsHelpers.temp.y = target.y - source.y;
		int num = Physics2D.Raycast(source, PhysicsHelpers.temp, PhysicsHelpers.filter, PhysicsHelpers.castHits, PhysicsHelpers.temp.magnitude);
		for (int i = 0; i < num; i++)
		{
			if (PhysicsHelpers.castHits[i].collider.isTrigger)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00010350 File Offset: 0x0000E550
	public static bool AnythingBetween(Vector2 source, Vector2 target, int layerMask, bool useTriggers)
	{
		PhysicsHelpers.filter.layerMask = layerMask;
		PhysicsHelpers.filter.useTriggers = useTriggers;
		PhysicsHelpers.temp.x = target.x - source.x;
		PhysicsHelpers.temp.y = target.y - source.y;
		return Physics2D.Raycast(source, PhysicsHelpers.temp, PhysicsHelpers.filter, PhysicsHelpers.castHits, PhysicsHelpers.temp.magnitude) > 0;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x000103C8 File Offset: 0x0000E5C8
	public static bool AnyNonTriggersBetween(Vector2 source, Vector2 dirNorm, float mag, int layerMask)
	{
		int num = Physics2D.RaycastNonAlloc(source, dirNorm, PhysicsHelpers.castHits, mag, layerMask);
		bool result = false;
		for (int i = 0; i < num; i++)
		{
			if (!PhysicsHelpers.castHits[i].collider.isTrigger)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00010410 File Offset: 0x0000E610
	public static bool AnythingBetween(Vector2 source, Vector2 target, int layerMask, bool useTriggers, Collider2D itemToIgnore, Transform objectToIgnore)
	{
		PhysicsHelpers.filter.layerMask = layerMask;
		PhysicsHelpers.filter.useTriggers = useTriggers;
		PhysicsHelpers.temp.x = target.x - source.x;
		PhysicsHelpers.temp.y = target.y - source.y;
		return Physics2D.Raycast(source, PhysicsHelpers.temp, PhysicsHelpers.filter, PhysicsHelpers.castHits, PhysicsHelpers.temp.magnitude) > 0 && PhysicsHelpers.castHits[0].collider != itemToIgnore && PhysicsHelpers.castHits[0].collider.transform != objectToIgnore;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x000104C4 File Offset: 0x0000E6C4
	// Note: this type is marked as 'beforefieldinit'.
	static PhysicsHelpers()
	{
		ContactFilter2D contactFilter2D = default(ContactFilter2D);
		contactFilter2D.useLayerMask = true;
		PhysicsHelpers.filter = contactFilter2D;
	}

	// Token: 0x040002FA RID: 762
	private static Collider2D[] colliderHits = new Collider2D[20];

	// Token: 0x040002FB RID: 763
	private static RaycastHit2D[] castHits = new RaycastHit2D[20];

	// Token: 0x040002FC RID: 764
	private static Vector2 temp = default(Vector2);

	// Token: 0x040002FD RID: 765
	private static ContactFilter2D filter;
}
