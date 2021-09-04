using System;
using System.Collections;
using PowerTools;
using UnityEngine;

// Token: 0x020001AD RID: 429
public class OverlayKillAnimation : MonoBehaviour
{
	// Token: 0x06000988 RID: 2440 RVA: 0x0003DC14 File Offset: 0x0003BE14
	public void Begin(GameData.PlayerInfo kInfo, GameData.PlayerInfo vInfo)
	{
		if (this.killerParts)
		{
			PlayerControl.SetPlayerMaterialColors(kInfo.ColorId, this.killerParts.Body);
			this.killerParts.Hands.ForEach(delegate(SpriteRenderer b)
			{
				PlayerControl.SetPlayerMaterialColors(kInfo.ColorId, b);
			});
			if (this.killerParts.HatSlot)
			{
				this.killerParts.HatSlot.SetHat(kInfo.HatId, kInfo.ColorId);
			}
			switch (this.KillType)
			{
			case KillAnimType.Stab:
			case KillAnimType.Neck:
				PlayerControl.SetSkinImage(kInfo.SkinId, this.killerParts.SkinSlot);
				break;
			case KillAnimType.Tongue:
			{
				SkinData skinById = DestroyableSingleton<HatManager>.Instance.GetSkinById(kInfo.SkinId);
				this.killerParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById.KillTongueImpostor, 1f);
				break;
			}
			case KillAnimType.Shoot:
			{
				SkinData skinById2 = DestroyableSingleton<HatManager>.Instance.GetSkinById(kInfo.SkinId);
				this.killerParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById2.KillShootImpostor, 1f);
				break;
			}
			}
			if (this.killerParts.PetSlot)
			{
				PetBehaviour petById = DestroyableSingleton<HatManager>.Instance.GetPetById(kInfo.PetId);
				if (petById && petById.scaredClip)
				{
					this.killerParts.PetSlot.GetComponent<SpriteAnim>().Play(petById.idleClip, 1f);
					this.killerParts.PetSlot.sharedMaterial = petById.rend.sharedMaterial;
					PlayerControl.SetPlayerMaterialColors(kInfo.ColorId, this.killerParts.PetSlot);
				}
				else
				{
					this.killerParts.PetSlot.enabled = false;
				}
			}
		}
		if (vInfo != null && this.victimParts)
		{
			this.victimHat = vInfo.HatId;
			PlayerControl.SetPlayerMaterialColors(vInfo.ColorId, this.victimParts.Body);
			if (this.victimParts.HatSlot)
			{
				this.victimParts.HatSlot.SetHat(vInfo.HatId, vInfo.ColorId);
			}
			SkinData skinById3 = DestroyableSingleton<HatManager>.Instance.GetSkinById(vInfo.SkinId);
			switch (this.KillType)
			{
			case KillAnimType.Stab:
				this.victimParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById3.KillStabVictim, 1f);
				break;
			case KillAnimType.Tongue:
				this.victimParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById3.KillTongueVictim, 1f);
				break;
			case KillAnimType.Shoot:
				this.victimParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById3.KillShootVictim, 1f);
				break;
			case KillAnimType.Neck:
				this.victimParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById3.KillNeckVictim, 1f);
				break;
			case KillAnimType.RHM:
				this.victimParts.SkinSlot.GetComponent<SpriteAnim>().Play(skinById3.KillRHMVictim, 1f);
				break;
			}
			if (this.victimParts.PetSlot)
			{
				PetBehaviour petById2 = DestroyableSingleton<HatManager>.Instance.GetPetById(vInfo.PetId);
				if (petById2 && petById2.scaredClip)
				{
					this.victimParts.PetSlot.GetComponent<SpriteAnim>().Play(petById2.scaredClip, 1f);
					this.victimParts.PetSlot.sharedMaterial = petById2.rend.sharedMaterial;
					PlayerControl.SetPlayerMaterialColors(vInfo.ColorId, this.victimParts.PetSlot);
					return;
				}
				this.victimParts.PetSlot.enabled = false;
			}
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0003DFFC File Offset: 0x0003C1FC
	public void SetHatFloor()
	{
		HatBehaviour hatById = DestroyableSingleton<HatManager>.Instance.GetHatById(this.victimHat);
		if (!hatById)
		{
			return;
		}
		this.victimParts.HatSlot.Hat = hatById;
		this.victimParts.HatSlot.SetFloorAnim();
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0003E044 File Offset: 0x0003C244
	public void PlayKillSound()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.Sfx, false, 1f).volume = 0.8f;
			VibrationManager.Vibrate(3f, 3f, 0f, VibrationManager.VibrationFalloff.None, this.Sfx, false);
			DualshockLightManager.Flash(Color.red, 2f, this.Sfx);
		}
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003E0A9 File Offset: 0x0003C2A9
	public IEnumerator WaitForFinish()
	{
		//SpriteAnim[] anims = base.GetComponentsInChildren<SpriteAnim>();
		//if (anims.Length == 0)
		//{
		//	yield return new WaitForSeconds(1f);
		//}
		//else
		//{
		//	for (;;)
		//	{
		//		bool flag = false;
		//		for (int i = 0; i < anims.Length; i++)
		//		{
		//			if (anims[i].IsPlaying(null))
		//			{
		//				flag = true;
		//				break;
		//			}
		//		}
		//		if (!flag)
		//		{
		//			break;
		//		}
		//		yield return null;
		//	}
		//}
		yield break;
	}

	// Token: 0x04000B00 RID: 2816
	public KillAnimType KillType;

	// Token: 0x04000B01 RID: 2817
	public PoolablePlayer killerParts;

	// Token: 0x04000B02 RID: 2818
	public PoolablePlayer victimParts;

	// Token: 0x04000B03 RID: 2819
	private uint victimHat;

	// Token: 0x04000B04 RID: 2820
	public AudioClip Stinger;

	// Token: 0x04000B05 RID: 2821
	public AudioClip Sfx;

	// Token: 0x04000B06 RID: 2822
	public float StingerVolume = 0.6f;
}
