using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.CoreScripts;
using InnerNet;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class EndGameManager : MonoBehaviour
{
	// Token: 0x060002F7 RID: 759 RVA: 0x00013760 File Offset: 0x00011960
	public void Start()
	{
		this.SetEverythingUp();
		base.StartCoroutine(this.CoBegin());
		base.Invoke("ShowButtons", 1.1f);
		ConsoleJoystick.SetMode_Menu();
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0001378A File Offset: 0x0001198A
	private void ShowButtons()
	{
		this.FrontMost.gameObject.SetActive(false);
		this.PlayAgainButton.gameObject.SetActive(true);
		this.ExitButton.gameObject.SetActive(true);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x000137C0 File Offset: 0x000119C0
	private void SetEverythingUp()
	{
		//StatsManager instance = StatsManager.Instance;
		//uint gamesFinished = instance.GamesFinished;
		//instance.GamesFinished = gamesFinished + 1U;
		//bool flag = TempData.DidHumansWin(TempData.EndReason);
		//if (TempData.EndReason == GameOverReason.ImpostorDisconnect)
		//{
		//	StatsManager.Instance.AddDrawReason(TempData.EndReason);
		//	this.WinText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ImpostorDisconnected, Array.Empty<object>());
		//	SoundManager.Instance.PlaySound(this.DisconnectStinger, false, 1f);
		//}
		//else if (TempData.EndReason == GameOverReason.HumansDisconnect)
		//{
		//	StatsManager.Instance.AddDrawReason(TempData.EndReason);
		//	this.WinText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.CrewmatesDisconnected, Array.Empty<object>());
		//	SoundManager.Instance.PlaySound(this.DisconnectStinger, false, 1f);
		//}
		//else
		//{
		//	if (TempData.winners.Any((WinningPlayerData h) => h.IsYou))
		//	{
		//		StatsManager.Instance.AddWinReason(TempData.EndReason);
		//		this.WinText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Victory, Array.Empty<object>());
		//		this.BackgroundBar.material.SetColor("_Color", Palette.CrewmateBlue);
		//		WinningPlayerData winningPlayerData = TempData.winners.FirstOrDefault((WinningPlayerData h) => h.IsYou);
		//		if (winningPlayerData != null)
		//		{
		//			DestroyableSingleton<Telemetry>.Instance.WonGame(winningPlayerData.ColorId, winningPlayerData.HatId);
		//		}
		//	}
		//	else
		//	{
		//		StatsManager.Instance.AddLoseReason(TempData.EndReason);
		//		this.WinText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Defeat, Array.Empty<object>());
		//		this.WinText.Color = Color.red;
		//	}
		//	if (flag)
		//	{
		SoundManager.Instance.PlayDynamicSound("Stinger", this.CrewStinger, false, new DynamicSound.GetDynamicsFunction(this.GetStingerVol), false);
		//	}
		//	else
		//	{
		//		SoundManager.Instance.PlayDynamicSound("Stinger", this.ImpostorStinger, false, new DynamicSound.GetDynamicsFunction(this.GetStingerVol), false);
		//	}
		//}
		//List<WinningPlayerData> list = TempData.winners.OrderBy(delegate(WinningPlayerData b)
		//{
		//	if (!b.IsYou)
		//	{
		//		return 0;
		//	}
		//	return -1;
		//}).ToList<WinningPlayerData>();
		//for (int i = 0; i < list.Count; i++)
		//{
		//	WinningPlayerData winningPlayerData2 = list[i];
		//	int num = (i % 2 == 0) ? -1 : 1;
		//	int num2 = (i + 1) / 2;
		//	float num3 = 1f - (float)num2 * 0.075f;
		//	float num4 = 1f - (float)num2 * 0.035f;
		//	float num5 = (float)((i == 0) ? -8 : -1);
		//	PoolablePlayer poolablePlayer = Object.Instantiate<PoolablePlayer>(this.PlayerPrefab, base.transform);
		//	poolablePlayer.transform.localPosition = new Vector3(0.8f * (float)num * (float)num2 * num4, this.BaseY - 0.25f + (float)num2 * 0.1f, num5 + (float)num2 * 0.01f) * 1.25f;
		//	Vector3 vector = new Vector3(num3, num3, num3) * 1.25f;
		//	poolablePlayer.transform.localScale = vector;
		//	if (winningPlayerData2.IsDead)
		//	{
		//		poolablePlayer.Body.sprite = this.GhostSprite;
		//		poolablePlayer.SetDeadFlipX(i % 2 != 0);
		//	}
		//	else
		//	{
		//		poolablePlayer.SetFlipX(i % 2 == 0);
		//	}
		//	if (!winningPlayerData2.IsDead)
		//	{
		//		DestroyableSingleton<HatManager>.Instance.SetSkin(poolablePlayer.SkinSlot, winningPlayerData2.SkinId);
		//	}
		//	else
		//	{
		//		poolablePlayer.HatSlot.color = new Color(1f, 1f, 1f, 0.5f);
		//	}
		//	PlayerControl.SetPlayerMaterialColors(winningPlayerData2.ColorId, poolablePlayer.Body);
		//	poolablePlayer.HatSlot.SetHat(winningPlayerData2.HatId, winningPlayerData2.ColorId);
		//	PlayerControl.SetPetImage(winningPlayerData2.PetId, winningPlayerData2.ColorId, poolablePlayer.PetSlot);
		//	if (flag)
		//	{
		//		poolablePlayer.NameText.gameObject.SetActive(false);
		//	}
		//	else
		//	{
		//		poolablePlayer.NameText.Text = winningPlayerData2.Name;
		//		if (winningPlayerData2.IsImpostor)
		//		{
		//			poolablePlayer.NameText.Color = Palette.ImpostorRed;
		//		}
		//		poolablePlayer.NameText.transform.localScale = vector.Inv();
		//	}
		//}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00013C03 File Offset: 0x00011E03
	private void GetStingerVol(AudioSource source, float dt)
	{
		this.stingerTime += dt * 0.75f;
		source.volume = Mathf.Clamp(1f / this.stingerTime, 0f, 1f);
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00013C3A File Offset: 0x00011E3A
	public IEnumerator CoBegin()
	{
		Color c = this.WinText.Color;
		Color fade = Color.black;
		Color white = Color.white;
		Vector3 titlePos = this.WinText.transform.localPosition;
		float timer = 0f;
		while (timer < 3f)
		{
			timer += Time.deltaTime;
			float num = Mathf.Min(1f, timer / 3f);
			this.Foreground.material.SetFloat("_Rad", this.ForegroundRadius.ExpOutLerp(num * 2f));
			fade.a = Mathf.Lerp(1f, 0f, num * 3f);
			this.FrontMost.color = fade;
			c.a = Mathf.Clamp(FloatRange.ExpOutLerp(num, 0f, 1f), 0f, 1f);
			this.WinText.Color = c;
			titlePos.y = 2.7f - num * 0.3f;
			this.WinText.transform.localPosition = titlePos;
			yield return null;
		}
		this.FrontMost.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00013C4C File Offset: 0x00011E4C
	public void NextGame()
	{
		this.PlayAgainButton.gameObject.SetActive(false);
		this.ExitButton.gameObject.SetActive(false);
		if (TempData.showAd && !SaveManager.BoughtNoAds)
		{
			TempData.showAd = false;
		}
		base.StartCoroutine(this.CoJoinGame());
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00013C9C File Offset: 0x00011E9C
	public IEnumerator CoJoinGame()
	{
		AmongUsClient.Instance.JoinGame();
		yield return EndGameManager.WaitWithTimeout(() => AmongUsClient.Instance.ClientId >= 0);
		if (AmongUsClient.Instance.ClientId < 0)
		{
			AmongUsClient.Instance.ExitGame(AmongUsClient.Instance.LastDisconnectReason);
		}
		yield break;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00013CA4 File Offset: 0x00011EA4
	public void Exit()
	{
		this.PlayAgainButton.gameObject.SetActive(false);
		this.ExitButton.gameObject.SetActive(false);
		AmongUsClient.Instance.ExitGame(DisconnectReasons.ExitGame);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00013CD3 File Offset: 0x00011ED3
	public static IEnumerator WaitWithTimeout(Func<bool> success)
	{
		float timer = 0f;
		while (timer < 5f && !success())
		{
			yield return null;
			timer += Time.deltaTime;
		}
		yield break;
	}

	// Token: 0x04000371 RID: 881
	public TextRenderer WinText;

	// Token: 0x04000372 RID: 882
	public MeshRenderer BackgroundBar;

	// Token: 0x04000373 RID: 883
	public MeshRenderer Foreground;

	// Token: 0x04000374 RID: 884
	public FloatRange ForegroundRadius;

	// Token: 0x04000375 RID: 885
	public SpriteRenderer FrontMost;

	// Token: 0x04000376 RID: 886
	public PoolablePlayer PlayerPrefab;

	// Token: 0x04000377 RID: 887
	public Sprite GhostSprite;

	// Token: 0x04000378 RID: 888
	public SpriteRenderer PlayAgainButton;

	// Token: 0x04000379 RID: 889
	public SpriteRenderer ExitButton;

	// Token: 0x0400037A RID: 890
	public AudioClip DisconnectStinger;

	// Token: 0x0400037B RID: 891
	public AudioClip CrewStinger;

	// Token: 0x0400037C RID: 892
	public AudioClip ImpostorStinger;

	// Token: 0x0400037D RID: 893
	public float BaseY = -0.25f;

	// Token: 0x0400037E RID: 894
	private float stingerTime;
}
