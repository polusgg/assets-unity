using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class DummyConsole : MonoBehaviour
{
	// Token: 0x06000412 RID: 1042 RVA: 0x0001AD70 File Offset: 0x00018F70
	public void Start()
	{
		this.rend = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001AD80 File Offset: 0x00018F80
	public void FixedUpdate()
	{
		this.rend.material.SetColor("_OutlineColor", Color.yellow);
		float num = float.MaxValue;
		for (int i = 0; i < this.Players.Length; i++)
		{
			PlayerAnimator playerAnimator = this.Players[i];
			Vector2 vector = base.transform.position - playerAnimator.transform.position;
			vector.y += 0.3636f;
			float magnitude = vector.magnitude;
			if (magnitude < num)
			{
				num = magnitude;
			}
			if (magnitude < this.UseDistance)
			{
				playerAnimator.NearbyConsoles |= 1 << this.ConsoleId;
			}
			else
			{
				playerAnimator.NearbyConsoles &= ~(1 << this.ConsoleId);
			}
		}
		if (num >= this.UseDistance * 2f)
		{
			this.rend.material.SetFloat("_Outline", 0f);
			this.rend.material.SetColor("_AddColor", Color.clear);
			return;
		}
		this.rend.material.SetFloat("_Outline", 1f);
		if (num < this.UseDistance)
		{
			this.rend.material.SetColor("_AddColor", Color.yellow);
			return;
		}
		this.rend.material.SetColor("_AddColor", Color.clear);
	}

	// Token: 0x040004D0 RID: 1232
	public int ConsoleId;

	// Token: 0x040004D1 RID: 1233
	public PlayerAnimator[] Players;

	// Token: 0x040004D2 RID: 1234
	public float UseDistance;

	// Token: 0x040004D3 RID: 1235
	[HideInInspector]
	private SpriteRenderer rend;
}
