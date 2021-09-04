using System;
using System.Collections;
using System.Collections.Generic;
using InnerNet;
using TMPro;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class ChatController : MonoBehaviour
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001C8AC File Offset: 0x0001AAAC
	public bool IsOpen
	{
		get
		{
			return this.Content.activeInHierarchy;
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001C8B9 File Offset: 0x0001AAB9
	private void Awake()
	{
		this.specialInputHandler = base.GetComponentInChildren<SpecialInputHandler>(true);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
	private void OnEnable()
	{
		if (this.specialInputHandler != null)
		{
			this.specialInputHandler.disableVirtualCursor = true;
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
	public void Toggle()
	{
		CustomNetworkTransform customNetworkTransform = PlayerControl.LocalPlayer ? PlayerControl.LocalPlayer.NetTransform : null;
		if (this.animating || !customNetworkTransform)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.TextArea.text))
		{
			this.TextArea.SetText("", "");
			this.quickChatMenu.ResetGlyphs();
			return;
		}
		base.StopAllCoroutines();
		if (this.IsOpen)
		{
			base.StartCoroutine(this.CoClose());
			if (this.quickChatMenu.gameObject.activeSelf)
			{
				this.quickChatMenu.Toggle();
				return;
			}
		}
		else
		{
			this.Content.SetActive(true);
			customNetworkTransform.Halt();
			base.StartCoroutine(this.CoOpen());
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001C9A7 File Offset: 0x0001ABA7
	public void SetVisible(bool visible)
	{
		Debug.Log("Chat is hidden: " + visible.ToString());
		this.ForceClosed();
		base.gameObject.SetActive(visible);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0001C9D1 File Offset: 0x0001ABD1
	public void ForceClosed()
	{
		base.StopAllCoroutines();
		this.Content.SetActive(false);
		this.animating = false;
		ConsoleJoystick.SetMode_Menu();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001CA01 File Offset: 0x0001AC01
	public IEnumerator CoOpen()
	{
		this.animating = true;
		Vector3 scale = Vector3.one;
		this.BanButton.Hide();
		this.BanButton.SetVisible(true);
		float targetScale = AspectSize.CalculateSize(base.transform.localPosition, this.BackgroundImage.sprite);
		float timer = 0f;
		while (timer < 0.15f)
		{
			timer += Time.deltaTime;
			float num = Mathf.SmoothStep(0f, 1f, timer / 0.15f);
			scale.y = (scale.x = Mathf.Lerp(0.1f, targetScale, num));
			this.Content.transform.localScale = scale;
			this.Content.transform.localPosition = Vector3.Lerp(this.SourcePos, this.TargetPos, num) * targetScale;
			this.BanButton.transform.localPosition = new Vector3(0f, -num * 0.75f, -20f);
			yield return null;
		}
		this.ChatNotifyDot.enabled = false;
		this.animating = false;
		this.GiveFocus();
		ConsoleJoystick.SetMode_QuickChat();
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
		this.quickChatMenu.ResetGlyphs();
		yield break;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001CA10 File Offset: 0x0001AC10
	public IEnumerator CoClose()
	{
		this.animating = true;
		this.BanButton.Hide();
		Vector3 scale = Vector3.one;
		float targetScale = AspectSize.CalculateSize(base.transform.localPosition, this.BackgroundImage.sprite);
		for (float timer = 0f; timer < 0.15f; timer += Time.deltaTime)
		{
			float num = 1f - Mathf.SmoothStep(0f, 1f, timer / 0.15f);
			scale.y = (scale.x = Mathf.Lerp(0.1f, targetScale, num));
			this.Content.transform.localScale = scale;
			this.Content.transform.localPosition = Vector3.Lerp(this.SourcePos, this.TargetPos, num) * targetScale;
			this.BanButton.transform.localPosition = new Vector3(0f, -num * 0.75f, -20f);
			yield return null;
		}
		this.BanButton.SetVisible(false);
		this.Content.SetActive(false);
		this.animating = false;
		ConsoleJoystick.SetMode_Menu();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
		yield break;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001CA20 File Offset: 0x0001AC20
	public void SetPosition(MeetingHud meeting)
	{
		if (meeting)
		{
			base.transform.SetParent(meeting.transform);
			base.transform.localPosition = new Vector3(3.1f, 2.2f, -10f);
			return;
		}
		base.transform.SetParent(DestroyableSingleton<HudManager>.Instance.transform);
		base.GetComponent<AspectPosition>().AdjustPosition();
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001CA88 File Offset: 0x0001AC88
	public void UpdateCharCount()
	{
		Vector2 size = this.TextBubble.size;
		size.y = Math.Max(0.62f, this.TextArea.TextHeight + 0.2f);
		this.TextBubble.size = size;
		Vector3 localPosition = this.TextBubble.transform.localPosition;
		localPosition.y = (0.62f - size.y) / 2f;
		this.TextBubble.transform.localPosition = localPosition;
		Vector3 localPosition2 = this.TypingArea.localPosition;
		localPosition2.y = -2.08f - localPosition.y * 2f;
		this.TypingArea.localPosition = localPosition2;
		int length = this.TextArea.text.Length;
		this.CharCount.text = string.Format("{0}/100", length);
		if (length < 75)
		{
			this.CharCount.color = Color.black;
			return;
		}
		if (length < 100)
		{
			this.CharCount.color = new Color(1f, 1f, 0f, 1f);
			return;
		}
		this.CharCount.color = Color.red;
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
	private void Update()
	{
		if (SaveManager.ChatModeType == QuickChatModes.QuickChatOnly)
		{
			this.OpenKeyboardButton.SetActive(false);
			this.TextArea.enabled = false;
		}
		this.TimeSinceLastMessage += Time.deltaTime;
		if (this.SendRateMessage.isActiveAndEnabled)
		{
			float num = 3f - this.TimeSinceLastMessage;
			if (num < 0f)
			{
				this.SendRateMessage.gameObject.SetActive(false);
				return;
			}
			this.SendRateMessage.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ChatRateLimit, new object[]
			{
				Mathf.CeilToInt(num)
			});
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001CC5C File Offset: 0x0001AE5C
	public void SendChat()
	{
		float num = 3f - this.TimeSinceLastMessage;
		if (num > 0f)
		{
			this.SendRateMessage.gameObject.SetActive(true);
			this.SendRateMessage.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ChatRateLimit, new object[]
			{
				Mathf.CeilToInt(num)
			});
			return;
		}
		if (!PlayerControl.LocalPlayer.RpcSendChat(this.TextArea.text))
		{
			return;
		}
		this.TimeSinceLastMessage = 0f;
		this.TextArea.Clear();
		this.quickChatMenu.ResetGlyphs();
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
	public void AddChatNote(GameData.PlayerInfo srcPlayer, ChatNoteTypes noteType)
	{
		if (srcPlayer == null)
		{
			return;
		}
		if (this.chatBubPool.NotInUse == 0)
		{
			this.chatBubPool.ReclaimOldest();
		}
		ChatBubble chatBubble = this.chatBubPool.Get<ChatBubble>();
		PlayerControl.SetPlayerMaterialColors(srcPlayer.ColorId, chatBubble.ChatFace);
		chatBubble.transform.SetParent(this.scroller.Inner);
		chatBubble.transform.localScale = Vector3.one;
		chatBubble.SetNotification();
		if (noteType == ChatNoteTypes.DidVote)
		{
			int votesRemaining = MeetingHud.Instance.GetVotesRemaining();
			chatBubble.SetName(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MeetingHasVoted, new object[]
			{
				srcPlayer.PlayerName,
				votesRemaining
			}), false, true, Color.green);
		}
		chatBubble.SetText(string.Empty);
		Vector3 localPosition = chatBubble.Background.transform.localPosition;
		localPosition.y = chatBubble.NameText.transform.localPosition.y - chatBubble.Background.size.y / 2f + 0.05f;
		chatBubble.Background.transform.localPosition = localPosition;
		this.AlignAllBubbles();
		if (!this.IsOpen && this.notificationRoutine == null)
		{
			this.notificationRoutine = base.StartCoroutine(this.BounceDot());
		}
		if (srcPlayer.Object != PlayerControl.LocalPlayer)
		{
			SoundManager.Instance.PlaySound(this.MessageSound, false, 1f).pitch = 0.5f + (float)srcPlayer.PlayerId / 10f;
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0001CE7C File Offset: 0x0001B07C
	public void AddChat(PlayerControl sourcePlayer, string chatText)
	{
		//if (!sourcePlayer || !PlayerControl.LocalPlayer)
		//{
		//	return;
		//}
		//GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		//GameData.PlayerInfo data2 = sourcePlayer.Data;
		//if (data2 == null || data == null || (data2.IsDead && !data.IsDead))
		//{
		//	return;
		//}
		//if (this.chatBubPool.NotInUse == 0)
		//{
		//	this.chatBubPool.ReclaimOldest();
		//}
		//ChatBubble chatBubble = this.chatBubPool.Get<ChatBubble>();
		//try
		//{
		//	chatBubble.transform.SetParent(this.scroller.Inner);
		//	chatBubble.transform.localScale = Vector3.one;
		//	bool flag = sourcePlayer == PlayerControl.LocalPlayer;
		//	if (flag)
		//	{
		//		chatBubble.SetRight();
		//	}
		//	else
		//	{
		//		chatBubble.SetLeft();
		//	}
		//	bool flag2 = data.IsImpostor && data2.IsImpostor;
		//	bool voted = MeetingHud.Instance && MeetingHud.Instance.DidVote(sourcePlayer.PlayerId);
		//	PlayerControl.SetPlayerMaterialColors(data2.ColorId, chatBubble.ChatFace);
		//	chatBubble.SetName(data2.PlayerName, data2.IsDead, voted, flag2 ? Palette.ImpostorRed : Color.white);
		//	ClientData client = AmongUsClient.Instance.GetClient(sourcePlayer.OwnerId);
		//	if (client != null && client.platformID != -1)
		//	{
		//		chatBubble.PlatformIcon.SetIcon(client.platformID);
		//	}
		//	if (SaveManager.CensorChat)
		//	{
		//		chatText = BlockedWords.CensorWords(chatText);
		//	}
		//	chatBubble.SetText(chatText);
		//	Vector3 localPosition = chatBubble.Background.transform.localPosition;
		//	localPosition.y = chatBubble.NameText.transform.localPosition.y - chatBubble.Background.size.y / 2f + 0.05f;
		//	chatBubble.Background.transform.localPosition = localPosition;
		//	this.AlignAllBubbles();
		//	if (!this.IsOpen && this.notificationRoutine == null)
		//	{
		//		this.notificationRoutine = base.StartCoroutine(this.BounceDot());
		//	}
		//	if (!flag)
		//	{
		//		SoundManager.Instance.PlaySound(this.MessageSound, false, 1f).pitch = 0.5f + (float)sourcePlayer.PlayerId / 10f;
		//	}
		//}
		//catch (Exception ex)
		//{
		//	Debug.LogError(ex);
		//	this.chatBubPool.Reclaim(chatBubble);
		//}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
	private void AlignAllBubbles()
	{
		float num = 0f;
		List<PoolableBehavior> activeChildren = this.chatBubPool.activeChildren;
		for (int i = activeChildren.Count - 1; i >= 0; i--)
		{
			ChatBubble chatBubble = activeChildren[i] as ChatBubble;
			num += chatBubble.Background.size.y;
			Vector3 localPosition = chatBubble.transform.localPosition;
			localPosition.y = -1.85f + num;
			chatBubble.transform.localPosition = localPosition;
			num += 0.1f;
		}
		this.scroller.YBounds.min = Mathf.Min(0f, -num + this.scroller.Hitbox.bounds.size.y);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0001D181 File Offset: 0x0001B381
	private IEnumerator BounceDot()
	{
		this.ChatNotifyDot.enabled = true;
		yield return Effects.Bounce(this.ChatNotifyDot.transform, 0.3f, 0.15f);
		this.notificationRoutine = null;
		yield break;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001D190 File Offset: 0x0001B390
	public void GiveFocus()
	{
		this.TextArea.GiveFocus();
	}

	// Token: 0x0400052F RID: 1327
	public ObjectPoolBehavior chatBubPool;

	// Token: 0x04000530 RID: 1328
	public Transform TypingArea;

	// Token: 0x04000531 RID: 1329
	public SpriteRenderer TextBubble;

	// Token: 0x04000532 RID: 1330
	public TextBoxTMP TextArea;

	// Token: 0x04000533 RID: 1331
	public TextMeshPro CharCount;

	// Token: 0x04000534 RID: 1332
	public int MaxChat = 15;

	// Token: 0x04000535 RID: 1333
	public Scroller scroller;

	// Token: 0x04000536 RID: 1334
	public GameObject Content;

	// Token: 0x04000537 RID: 1335
	public SpriteRenderer BackgroundImage;

	// Token: 0x04000538 RID: 1336
	public SpriteRenderer ChatNotifyDot;

	// Token: 0x04000539 RID: 1337
	public TextMeshPro SendRateMessage;

	// Token: 0x0400053A RID: 1338
	public Vector3 SourcePos = new Vector3(0f, 0f, -10f);

	// Token: 0x0400053B RID: 1339
	public Vector3 TargetPos = new Vector3(-0.35f, 0.02f, -10f);

	// Token: 0x0400053C RID: 1340
	private const float MaxChatSendRate = 3f;

	// Token: 0x0400053D RID: 1341
	private float TimeSinceLastMessage = 3f;

	// Token: 0x0400053E RID: 1342
	public AudioClip MessageSound;

	// Token: 0x0400053F RID: 1343
	private bool animating;

	// Token: 0x04000540 RID: 1344
	private Coroutine notificationRoutine;

	// Token: 0x04000541 RID: 1345
	public BanMenu BanButton;

	// Token: 0x04000542 RID: 1346
	public QuickChatMenu quickChatMenu;

	// Token: 0x04000543 RID: 1347
	public GameObject OpenKeyboardButton;

	// Token: 0x04000544 RID: 1348
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000545 RID: 1349
	public UiElement DefaultButtonSelected;

	// Token: 0x04000546 RID: 1350
	public List<UiElement> ControllerSelectable;

	// Token: 0x04000547 RID: 1351
	private SpecialInputHandler specialInputHandler;
}
