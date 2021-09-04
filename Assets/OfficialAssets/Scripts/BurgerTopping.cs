using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class BurgerTopping : MonoBehaviour
{
	// Token: 0x04000144 RID: 324
	public Collider2D Hitbox;

	// Token: 0x04000145 RID: 325
	public AudioClip GrabSound;

	// Token: 0x04000146 RID: 326
	public AudioClip DropSound;

	// Token: 0x04000147 RID: 327
	public BurgerToppingTypes ToppingType;

	// Token: 0x04000148 RID: 328
	public float Offset = 0.3f;
}
