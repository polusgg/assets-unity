using System;
//using Beebyte.Obfuscator;
using Hazel;
using InnerNet;
using UnityEngine;

// Token: 0x0200011E RID: 286
//[SkipRename]
public class LobbyBehaviour : InnerNetObject
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x0002C4C4 File Offset: 0x0002A6C4
	public void Start()
	{
		LobbyBehaviour.Instance = this;
		SoundManager.Instance.StopAllSound();
		AudioSource audioSource = SoundManager.Instance.PlayNamedSound("DropShipAmb", this.DropShipSound, true, false);
		audioSource.loop = true;
		audioSource.pitch = 1.2f;
		Camera main = Camera.main;
		if (main)
		{
			FollowerCamera component = main.GetComponent<FollowerCamera>();
			if (component)
			{
				component.shakeAmount = 0.03f;
				component.shakePeriod = 400f;
			}
		}
		foreach (PlayerControl playerControl in PlayerControl.AllPlayerControls)
		{
			playerControl.UpdatePlatformIcon();
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0002C580 File Offset: 0x0002A780
	public void FixedUpdate()
	{
		this.timer += Time.deltaTime;
		if (this.timer < 0.25f)
		{
			return;
		}
		this.timer = 0f;
		if (PlayerControl.GameOptions != null)
		{
			int numPlayers = GameData.Instance ? GameData.Instance.PlayerCount : 10;
			DestroyableSingleton<HudManager>.Instance.GameSettings.Text = PlayerControl.GameOptions.ToHudString(numPlayers);
			DestroyableSingleton<HudManager>.Instance.GameSettings.gameObject.SetActive(true);
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0002C609 File Offset: 0x0002A809
	public override void OnDestroy()
	{
		SoundManager.Instance.StopNamedSound("DropShipAmb");
		base.OnDestroy();
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0002C620 File Offset: 0x0002A820
	public override void HandleRpc(byte callId, MessageReader reader)
	{
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0002C622 File Offset: 0x0002A822
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		return false;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0002C625 File Offset: 0x0002A825
	public override void Deserialize(MessageReader reader, bool initialState)
	{
	}

	// Token: 0x040007D1 RID: 2001
	public static LobbyBehaviour Instance;

	// Token: 0x040007D2 RID: 2002
	public AudioClip SpawnSound;

	// Token: 0x040007D3 RID: 2003
	public AnimationClip SpawnInClip;

	// Token: 0x040007D4 RID: 2004
	public Vector2[] SpawnPositions;

	// Token: 0x040007D5 RID: 2005
	public AudioClip DropShipSound;

	// Token: 0x040007D6 RID: 2006
	public SkeldShipRoom[] AllRooms;

	// Token: 0x040007D7 RID: 2007
	private float timer;
}
