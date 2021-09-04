using System;
using System.Collections;
using System.Collections.Generic;
using Hazel;
using InnerNet;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class PlayerVoteArea : MonoBehaviour
{
	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x0002351E File Offset: 0x0002171E
	// (set) Token: 0x06000533 RID: 1331 RVA: 0x00023526 File Offset: 0x00021726
	public MeetingHud Parent { get; set; }

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000534 RID: 1332 RVA: 0x0002352F File Offset: 0x0002172F
	// (set) Token: 0x06000535 RID: 1333 RVA: 0x00023537 File Offset: 0x00021737
	public sbyte TargetPlayerId { get; private set; }

	// Token: 0x06000536 RID: 1334 RVA: 0x00023540 File Offset: 0x00021740
	public void SetTargetPlayerId(sbyte targetId)
	{
		this.TargetPlayerId = targetId;
		if (this.PlayerIcon)
		{
			this.Background.material.SetInt("_MaskLayer", (int)(targetId + 2));
			this.PlayerIcon.Body.material.SetInt("_MaskLayer", (int)(targetId + 2));
			this.PlayerIcon.SkinSlot.material.SetInt("_MaskLayer", (int)(targetId + 2));
			this.PlayerIcon.HatSlot.SetMaskLayer((int)(targetId + 2));
		}
		if (this.PlatformIcon)
		{
			PlayerControl playerControl = PlayerControl.AllPlayerControls.Find((PlayerControl pc) => pc.PlayerId == (byte)targetId);
			if (playerControl)
			{
				ClientData client = AmongUsClient.Instance.GetClient(playerControl.OwnerId);
				if (client != null && client.platformID != -1)
				{
					this.PlatformIcon.SetIcon((RuntimePlatform)client.platformID);
				}
			}
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00023648 File Offset: 0x00021848
	public void SetDead(bool isMe, bool didReport, bool isDead)
	{
		this.isDead = isDead;
		this.didReport = didReport;
		this.Megaphone.enabled = didReport;
		this.Overlay.gameObject.SetActive(false);
		this.Overlay.transform.GetChild(0).gameObject.SetActive(isDead);
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0002369C File Offset: 0x0002189C
	public void SetDisabled()
	{
		if (this.isDead)
		{
			return;
		}
		if (this.Overlay)
		{
			this.Overlay.gameObject.SetActive(true);
			this.Overlay.transform.GetChild(0).gameObject.SetActive(false);
			return;
		}
		base.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x000236F9 File Offset: 0x000218F9
	public void SetEnabled()
	{
		if (this.isDead)
		{
			return;
		}
		if (this.Overlay)
		{
			this.Overlay.gameObject.SetActive(false);
			return;
		}
		base.GetComponent<SpriteRenderer>().enabled = true;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0002372F File Offset: 0x0002192F
	public IEnumerator CoAnimateOverlay()
	{
		this.Overlay.gameObject.SetActive(this.isDead);
		if (this.isDead)
		{
			Transform xMark = this.Overlay.transform.GetChild(0);
			this.Overlay.color = Palette.ClearWhite;
			xMark.localScale = Vector3.zero;
			float fadeDuration = 0.5f;
			for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
			{
				this.Overlay.color = Color.Lerp(Palette.ClearWhite, Color.white, t / fadeDuration);
				yield return null;
			}
			this.Overlay.color = Color.white;
			float scaleDuration = 0.15f;
			for (float t = 0f; t < scaleDuration; t += Time.deltaTime)
			{
				float num = Mathf.Lerp(3f, 1f, t / scaleDuration);
				xMark.transform.localScale = new Vector3(num, num, num);
				yield return null;
			}
			xMark.transform.localScale = Vector3.one;
			xMark = null;
		}
		else if (this.didReport)
		{
			float scaleDuration = 1f;
			for (float fadeDuration = 0f; fadeDuration < scaleDuration; fadeDuration += Time.deltaTime)
			{
				float num2 = fadeDuration / scaleDuration;
				float num3 = PlayerVoteArea.TriangleWave(num2 * 3f) * 2f - 1f;
				this.Megaphone.transform.localEulerAngles = new Vector3(0f, 0f, num3 * 30f);
				num3 = Mathf.Lerp(0.7f, 1.2f, PlayerVoteArea.TriangleWave(num2 * 2f));
				this.Megaphone.transform.localScale = new Vector3(num3, num3, num3);
				yield return null;
			}
			this.Megaphone.transform.localEulerAngles = Vector3.zero;
			this.Megaphone.transform.localScale = Vector3.one;
		}
		yield break;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0002373E File Offset: 0x0002193E
	private static float TriangleWave(float t)
	{
		t -= (float)((int)t);
		if (t < 0.5f)
		{
			return t * 2f;
		}
		return 1f - (t - 0.5f) * 2f;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0002376A File Offset: 0x0002196A
	internal void SetVote(sbyte suspectIdx)
	{
		this.didVote = true;
		this.votedFor = suspectIdx;
		this.Flag.enabled = true;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00023786 File Offset: 0x00021986
	public void UnsetVote()
	{
		this.Flag.enabled = false;
		this.votedFor = 0;
		this.didVote = false;
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x000237A2 File Offset: 0x000219A2
	public void ClearButtons()
	{
		this.Buttons.SetActive(false);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x000237B0 File Offset: 0x000219B0
	public void ClearForResults()
	{
		this.resultsShowing = true;
		this.Flag.enabled = false;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x000237C5 File Offset: 0x000219C5
	public void VoteForMe()
	{
		if (!this.voteComplete)
		{
			this.Parent.Confirm(this.TargetPlayerId);
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x000237E0 File Offset: 0x000219E0
	public void Select()
	{
		if (PlayerControl.LocalPlayer.Data.IsDead)
		{
			return;
		}
		if (this.isDead)
		{
			return;
		}
		if (!this.voteComplete && this.Parent.Select((int)this.TargetPlayerId))
		{
			this.Buttons.SetActive(true);
			List<UiElement> selectableElements = new List<UiElement>
			{
				this.CancelButton,
				this.ConfirmButton
			};
			ControllerManager.Instance.OpenOverlayMenu(base.name, this.CancelButton, this.ConfirmButton, selectableElements, false);
		}
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0002386B File Offset: 0x00021A6B
	public void Cancel()
	{
		this.Buttons.SetActive(false);
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0002388C File Offset: 0x00021A8C
	public void Serialize(MessageWriter writer)
	{
		byte state = this.GetState();
		writer.Write(state);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x000238A8 File Offset: 0x00021AA8
	public void Deserialize(MessageReader reader)
	{
		byte b = reader.ReadByte();
		this.votedFor = PlayerVoteArea.GetVotedFor(b);
		this.isDead = ((b & 128) > 0);
		this.didVote = ((b & 64) > 0);
		this.didReport = ((b & 32) > 0);
		this.Flag.enabled = (this.didVote && !this.resultsShowing);
		this.Overlay.gameObject.SetActive(this.isDead);
		this.Megaphone.enabled = this.didReport;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00023938 File Offset: 0x00021B38
	public static sbyte GetVotedFor(byte state)
	{
		return (sbyte)((state & 15) - 1);
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00023941 File Offset: 0x00021B41
	public byte GetState()
	{
		return (byte)((int)(this.votedFor + 1 & 15) | (this.isDead ? 128 : 0) | (this.didVote ? 64 : 0) | (this.didReport ? 32 : 0));
	}

	// Token: 0x040005E3 RID: 1507
	public const byte DeadBit = 128;

	// Token: 0x040005E4 RID: 1508
	public const byte VotedBit = 64;

	// Token: 0x040005E5 RID: 1509
	public const byte ReportedBit = 32;

	// Token: 0x040005E6 RID: 1510
	public const byte VoteMask = 15;

	// Token: 0x040005E7 RID: 1511
	public GameObject Buttons;

	// Token: 0x040005E8 RID: 1512
	public UiElement ConfirmButton;

	// Token: 0x040005E9 RID: 1513
	public UiElement CancelButton;

	// Token: 0x040005EA RID: 1514
	public UiElement PlayerButton;

	// Token: 0x040005EB RID: 1515
	public PoolablePlayer PlayerIcon;

	// Token: 0x040005EC RID: 1516
	public SpriteRenderer Background;

	// Token: 0x040005ED RID: 1517
	public SpriteRenderer Flag;

	// Token: 0x040005EE RID: 1518
	public SpriteRenderer Megaphone;

	// Token: 0x040005EF RID: 1519
	public SpriteRenderer Overlay;

	// Token: 0x040005F0 RID: 1520
	public TextRenderer NameText;

	// Token: 0x040005F1 RID: 1521
	public PlatformIdentifierIcon PlatformIcon;

	// Token: 0x040005F2 RID: 1522
	public bool isDead;

	// Token: 0x040005F3 RID: 1523
	public bool didVote;

	// Token: 0x040005F4 RID: 1524
	public bool didReport;

	// Token: 0x040005F5 RID: 1525
	public sbyte votedFor;

	// Token: 0x040005F6 RID: 1526
	public bool voteComplete;

	// Token: 0x040005F7 RID: 1527
	public bool resultsShowing;
}
