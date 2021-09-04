using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class PoolablePlayer : MonoBehaviour
{
	// Token: 0x06000A13 RID: 2579 RVA: 0x00041524 File Offset: 0x0003F724
	public void SetFlipX(bool flipped)
	{
		this.Body.flipX = flipped;
		this.SkinSlot.flipX = !flipped;
		if (this.PetSlot)
		{
			this.PetSlot.flipX = flipped;
		}
		this.HatSlot.flipX = !flipped;
		if (flipped)
		{
			Vector3 localPosition = this.HatSlot.transform.localPosition;
			localPosition.x = -localPosition.x;
			this.HatSlot.transform.localPosition = localPosition;
			return;
		}
		Vector3 localPosition2 = this.PetSlot.transform.localPosition;
		localPosition2.x = -localPosition2.x;
		this.PetSlot.transform.localPosition = localPosition2;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x000415D8 File Offset: 0x0003F7D8
	public void SetDeadFlipX(bool flipped)
	{
		this.Body.flipX = flipped;
		this.PetSlot.flipX = flipped;
		this.HatSlot.flipX = flipped;
		if (flipped)
		{
			Vector3 localPosition = this.HatSlot.transform.localPosition;
			localPosition.x = -localPosition.x;
			localPosition.y = 0.725f;
			this.HatSlot.transform.localPosition = localPosition;
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00041648 File Offset: 0x0003F848
	internal void UpdateFromSaveManager()
	{
		PlayerControl.SetPlayerMaterialColors((int)SaveManager.BodyColor, this.Body);
		PlayerControl.SetSkinImage(SaveManager.LastSkin, this.SkinSlot);
		PlayerControl.SetPetImage(SaveManager.LastPet, (int)SaveManager.BodyColor, this.PetSlot);
		this.HatSlot.SetHat(SaveManager.LastHat, (int)SaveManager.BodyColor);
	}

	// Token: 0x04000B7A RID: 2938
	public SpriteRenderer Body;

	// Token: 0x04000B7B RID: 2939
	public SpriteRenderer[] Hands;

	// Token: 0x04000B7C RID: 2940
	public HatParent HatSlot;

	// Token: 0x04000B7D RID: 2941
	public SpriteRenderer SkinSlot;

	// Token: 0x04000B7E RID: 2942
	public SpriteRenderer PetSlot;

	// Token: 0x04000B7F RID: 2943
	public TextRenderer NameText;
}
