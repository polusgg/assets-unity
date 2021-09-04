using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200007B RID: 123
public class IntroCutscene : MonoBehaviour
{
	// Token: 0x06000306 RID: 774 RVA: 0x00014073 File Offset: 0x00012273
	public IEnumerator CoBegin(List<PlayerControl> yourTeam, bool isImpostor)
	{
		SoundManager.Instance.PlaySound(this.IntroStinger, false, 1f);
		if (this.overlayHandle == null)
		{
			this.overlayHandle = DestroyableSingleton<DualshockLightManager>.Instance.AllocateLight();
		}
		if (!isImpostor)
		{
			this.BeginCrewmate(yourTeam);
			this.overlayHandle.color = Palette.CrewmateBlue;
		}
		else
		{
			this.BeginImpostor(yourTeam);
			this.overlayHandle.color = Palette.ImpostorRed;
		}
		Color c = this.Title.Color;
		Color fade = Color.black;
		Color impColor = Color.white;
		Vector3 titlePos = this.Title.transform.localPosition;
		float timer = 0f;
		while (timer < 3f)
		{
			timer += Time.deltaTime;
			float num = Mathf.Min(1f, timer / 3f);
			this.Foreground.material.SetFloat("_Rad", this.ForegroundRadius.ExpOutLerp(num * 2f));
			fade.a = Mathf.Lerp(1f, 0f, num * 3f);
			this.FrontMost.color = fade;
			c.a = Mathf.Clamp(FloatRange.ExpOutLerp(num, 0f, 1f), 0f, 1f);
			this.Title.Color = c;
			impColor.a = Mathf.Lerp(0f, 1f, (num - 0.3f) * 3f);
			this.ImpostorText.Color = impColor;
			titlePos.y = 2.7f - num * 0.3f;
			this.Title.transform.localPosition = titlePos;
			this.overlayHandle.color.a = Mathf.Min(1f, timer * 2f);
			yield return null;
		}
		timer = 0f;
		while (timer < 1f)
		{
			timer += Time.deltaTime;
			float num2 = timer / 1f;
			fade.a = Mathf.Lerp(0f, 1f, num2 * 3f);
			this.FrontMost.color = fade;
			this.overlayHandle.color.a = 1f - fade.a;
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00014090 File Offset: 0x00012290
	private void BeginCrewmate(List<PlayerControl> yourTeam)
	{
		Vector3 position = this.BackgroundBar.transform.position;
		position.y -= 0.25f;
		this.BackgroundBar.transform.position = position;
		int adjustedNumImpostors = PlayerControl.GameOptions.GetAdjustedNumImpostors(GameData.Instance.PlayerCount);
		if (adjustedNumImpostors == 1)
		{
			this.ImpostorText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.NumImpostorsS, Array.Empty<object>());
		}
		else
		{
			this.ImpostorText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.NumImpostorsP, new object[]
			{
				adjustedNumImpostors
			});
		}
		this.BackgroundBar.material.SetColor("_Color", Palette.CrewmateBlue);
		this.Title.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Crewmate, Array.Empty<object>());
		this.Title.Color = Palette.CrewmateBlue;
		for (int i = 0; i < yourTeam.Count; i++)
		{
			PlayerControl playerControl = yourTeam[i];
			if (playerControl)
			{
				GameData.PlayerInfo data = playerControl.Data;
				if (data != null)
				{
					int num = (i % 2 == 0) ? -1 : 1;
					int num2 = (i + 1) / 2;
					float num3 = ((i == 0) ? 1.2f : 1f) - (float)num2 * 0.12f;
					float num4 = 1f - (float)num2 * 0.08f;
					float num5 = (float)((i == 0) ? -8 : -1);
					PoolablePlayer poolablePlayer = Object.Instantiate<PoolablePlayer>(this.PlayerPrefab, base.transform);
					poolablePlayer.name = data.PlayerName + "Dummy";
					poolablePlayer.SetFlipX(i % 2 == 0);
					poolablePlayer.transform.localPosition = new Vector3(0.8f * (float)num * (float)num2 * num4, this.BaseY - 0.25f + (float)num2 * 0.1f, num5 + (float)num2 * 0.01f) * 1.5f;
					Vector3 localScale = new Vector3(num3, num3, num3) * 1.5f;
					poolablePlayer.transform.localScale = localScale;
					PlayerControl.SetPlayerMaterialColors(data.ColorId, poolablePlayer.Body);
					DestroyableSingleton<HatManager>.Instance.SetSkin(poolablePlayer.SkinSlot, data.SkinId);
					poolablePlayer.HatSlot.SetHat(data.HatId, data.ColorId);
					PlayerControl.SetPetImage(data.PetId, data.ColorId, poolablePlayer.PetSlot);
					poolablePlayer.NameText.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0001430C File Offset: 0x0001250C
	private void BeginImpostor(List<PlayerControl> yourTeam)
	{
		this.ImpostorText.gameObject.SetActive(false);
		this.Title.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Impostor, Array.Empty<object>());
		this.Title.Color = Palette.ImpostorRed;
		for (int i = 0; i < yourTeam.Count; i++)
		{
			PlayerControl playerControl = yourTeam[i];
			if (playerControl)
			{
				GameData.PlayerInfo data = playerControl.Data;
				if (data != null)
				{
					int num = (i % 2 == 0) ? -1 : 1;
					int num2 = (i + 1) / 2;
					float num3 = 1f - (float)num2 * 0.075f;
					float num4 = 1f - (float)num2 * 0.035f;
					float num5 = (float)((i == 0) ? -8 : -1);
					PoolablePlayer poolablePlayer = Object.Instantiate<PoolablePlayer>(this.PlayerPrefab, base.transform);
					poolablePlayer.transform.localPosition = new Vector3((float)(num * num2) * num4, this.BaseY + (float)num2 * 0.15f, num5 + (float)num2 * 0.01f) * 1.5f;
					Vector3 vector = new Vector3(num3, num3, num3) * 1.5f;
					poolablePlayer.transform.localScale = vector;
					poolablePlayer.SetFlipX(i % 2 == 1);
					PlayerControl.SetPlayerMaterialColors(data.ColorId, poolablePlayer.Body);
					DestroyableSingleton<HatManager>.Instance.SetSkin(poolablePlayer.SkinSlot, data.SkinId);
					poolablePlayer.HatSlot.SetHat(data.HatId, data.ColorId);
					PlayerControl.SetPetImage(data.PetId, data.ColorId, poolablePlayer.PetSlot);
					TextRenderer nameText = poolablePlayer.NameText;
					nameText.Text = data.PlayerName;
					nameText.transform.localScale = vector.Inv();
				}
			}
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x000144C6 File Offset: 0x000126C6
	private void OnDestroy()
	{
		if (this.overlayHandle != null)
		{
			this.overlayHandle.Dispose();
			this.overlayHandle = null;
		}
	}

	// Token: 0x0400038B RID: 907
	public static IntroCutscene Instance;

	// Token: 0x0400038C RID: 908
	public TextRenderer Title;

	// Token: 0x0400038D RID: 909
	public TextRenderer ImpostorText;

	// Token: 0x0400038E RID: 910
	public PoolablePlayer PlayerPrefab;

	// Token: 0x0400038F RID: 911
	public MeshRenderer BackgroundBar;

	// Token: 0x04000390 RID: 912
	public MeshRenderer Foreground;

	// Token: 0x04000391 RID: 913
	public FloatRange ForegroundRadius;

	// Token: 0x04000392 RID: 914
	public SpriteRenderer FrontMost;

	// Token: 0x04000393 RID: 915
	public AudioClip IntroStinger;

	// Token: 0x04000394 RID: 916
	public float BaseY = -0.25f;

	// Token: 0x04000395 RID: 917
	private DualshockLightManager.LightOverlayHandle overlayHandle;
}
