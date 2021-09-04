using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000149 RID: 329
public class DoorBreakerGame : Minigame, IDoorMinigame
{
	// Token: 0x060007C7 RID: 1991 RVA: 0x00031A8A File Offset: 0x0002FC8A
	public void SetDoor(PlainDoor door)
	{
		this.MyDoor = door;
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00031A94 File Offset: 0x0002FC94
	public void Start()
	{
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			SpriteRenderer spriteRenderer = this.Buttons[i];
			spriteRenderer.color = Color.gray;
			spriteRenderer.GetComponent<PassiveButton>().enabled = false;
		}
		int j = 0;
		while (j < 4)
		{
			SpriteRenderer spriteRenderer2 = this.Buttons.Random<SpriteRenderer>();
			if (!spriteRenderer2.flipX)
			{
				spriteRenderer2.color = Color.white;
				spriteRenderer2.GetComponent<PassiveButton>().enabled = true;
				spriteRenderer2.flipX = true;
				j++;
			}
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00031B10 File Offset: 0x0002FD10
	public void FlipSwitch(SpriteRenderer button)
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.FlipSound, false, 1f);
		}
		button.color = Color.gray;
		button.flipX = false;
		button.GetComponent<PassiveButton>().enabled = false;
		if (this.Buttons.All((SpriteRenderer s) => !s.flipX))
		{
			ShipStatus.Instance.RpcRepairSystem(SystemTypes.Doors, this.MyDoor.Id | 64);
			this.MyDoor.SetDoorway(true);
			base.StartCoroutine(base.CoStartClose(0.4f));
		}
	}

	// Token: 0x040008E1 RID: 2273
	public PlainDoor MyDoor;

	// Token: 0x040008E2 RID: 2274
	public SpriteRenderer[] Buttons;

	// Token: 0x040008E3 RID: 2275
	public AudioClip FlipSound;
}
