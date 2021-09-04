using System;
using System.Collections;
//using QRCoder;
//using QRCoder.Unity;
//using Rewired;
//using Rewired.ControllerExtensions;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class BoardPassGame : Minigame
{
	// Token: 0x060007BB RID: 1979 RVA: 0x000315E4 File Offset: 0x0002F7E4
	public void Start()
	{
		//Texture2D graphic = new UnityQRCode(new QRCodeGenerator().CreateQrCode("Yo holmes, smell you later", 1, false, false, 0, -1)).GetGraphic(1);
		//this.renderer.sprite = Sprite.Create(graphic, new Rect(0f, 0f, (float)graphic.width, (float)graphic.height), Vector2.one / 2f);
		//this.Image.sprite = this.Photos[(int)PlayerControl.LocalPlayer.PlayerId % this.Photos.Length];
		//PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.Image);
		//this.NameText.Text = PlayerControl.LocalPlayer.Data.PlayerName;
		//base.SetupInput(true);
		//this.controller = new Controller();
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x000316B0 File Offset: 0x0002F8B0
	public void Update()
	{
		//this.controller.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	player.controllers.Joysticks[0].GetExtension<IDualShock4Extension>();
		//	if (this.pullButton.gameObject.activeSelf)
		//	{
		//		if (player.GetAxisRaw(13) > 0.7f)
		//		{
		//			this.PullPass();
		//			this.pullButton.gameObject.SetActive(false);
		//		}
		//	}
		//	else if (this.flipButton.gameObject.activeSelf)
		//	{
		//		Vector2 axis2DRaw = player.GetAxis2DRaw(13, 14);
		//		if (axis2DRaw.sqrMagnitude > 0.9f)
		//		{
		//			Vector2 normalized = axis2DRaw.normalized;
		//			if (this.prevHadInput)
		//			{
		//				float num = Vector2.SignedAngle(this.prevStickDir, normalized);
		//				if (num > 0f)
		//				{
		//					this.rotateAngle += num;
		//				}
		//				if (this.rotateAngle > 45f)
		//				{
		//					this.FlipPass();
		//					this.flipButton.gameObject.SetActive(false);
		//				}
		//			}
		//			this.prevStickDir = normalized;
		//			this.prevHadInput = true;
		//		}
		//		else
		//		{
		//			this.prevHadInput = false;
		//		}
		//	}
		//	else if (this.enableControllerPassMovement)
		//	{
		//		Vector3 localPosition = VirtualCursor.currentPosition - base.transform.position;
		//		localPosition.z = -1f;
		//		this.pass.transform.localPosition = localPosition;
		//	}
		//}
		//else if (this.grabbed)
		//{
		//	Vector3 localPosition2 = DestroyableSingleton<PassiveButtonManager>.Instance.controller.DragPosition - base.transform.position;
		//	localPosition2.z = -1f;
		//	this.pass.transform.localPosition = localPosition2;
		//}
		//if (!this.flipButton.isActiveAndEnabled && !this.pullButton.isActiveAndEnabled && !this.MyNormTask.IsComplete)
		//{
		//	if (this.Sensor.IsTouching(this.BarCode))
		//	{
		//		if (this.blinky == null)
		//		{
		//			this.blinky = base.StartCoroutine(this.CoRunBlinky());
		//			return;
		//		}
		//	}
		//	else if (this.blinky != null)
		//	{
		//		base.StopCoroutine(this.blinky);
		//		this.blinky = null;
		//		this.Scanner.sprite = this.ScannerWaiting;
		//	}
		//}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x000318ED File Offset: 0x0002FAED
	private IEnumerator CoRunBlinky()
	{
		int num;
		for (int i = 0; i < 3; i = num)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.scanStartSound, false, 1f);
			}
			this.Scanner.sprite = this.ScannerAccept;
			yield return Effects.Wait(0.1f);
			this.Scanner.sprite = this.ScannerScanning;
			yield return Effects.Wait(0.2f);
			num = i + 1;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.scanSound, false, 1f);
		}
		this.blinky = null;
		this.Scanner.sprite = this.ScannerAccept;
		this.MyNormTask.NextStep();
		yield return base.CoStartClose(0.75f);
		yield break;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x000318FC File Offset: 0x0002FAFC
	public void PullPass()
	{
		base.StartCoroutine(this.CoPullPass());
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0003190B File Offset: 0x0002FB0B
	private IEnumerator CoPullPass()
	{
		this.pullButton.gameObject.SetActive(false);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.slideinSound, false, 1f);
		}
		yield return Effects.Slide2D(this.pass.transform, new Vector2(-10f, 0f), new Vector2(-1.4f, 0f), 0.3f);
		this.flipButton.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0003191A File Offset: 0x0002FB1A
	public void Grab()
	{
		if (!this.flipButton.isActiveAndEnabled && !this.pullButton.isActiveAndEnabled)
		{
			this.grabbed = !this.grabbed;
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00031945 File Offset: 0x0002FB45
	public void FlipPass()
	{
		base.StartCoroutine(this.CoFlipPass());
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00031954 File Offset: 0x0002FB54
	private IEnumerator CoFlipPass()
	{
		this.flipButton.gameObject.SetActive(false);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.flipSound, false, 1f);
		}
		yield return Effects.Lerp(0.2f, delegate(float t)
		{
			this.pass.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f, 90f, t), 0f);
		});
		this.pass.sprite = this.passBack;
		this.renderer.gameObject.SetActive(false);
		this.ImageBg.gameObject.SetActive(false);
		this.NameText.gameObject.SetActive(false);
		yield return Effects.Lerp(0.2f, delegate(float t)
		{
			this.pass.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(90f, 180f, t), 0f);
		});
		this.enableControllerPassMovement = true;
		this.inputHandler.disableVirtualCursor = false;
		VirtualCursor.instance.SetWorldPosition(this.pass.transform.position);
		yield break;
	}

	// Token: 0x040008C6 RID: 2246
	private static Color[] BgColors = new Color[]
	{
		new Color32(101, 170, 119, byte.MaxValue),
		new Color32(85, 93, 182, byte.MaxValue),
		new Color32(198, 127, 174, byte.MaxValue),
		new Color32(161, 126, 100, byte.MaxValue),
		new Color32(149, 219, 209, byte.MaxValue)
	};

	// Token: 0x040008C7 RID: 2247
	public SpriteRenderer renderer;

	// Token: 0x040008C8 RID: 2248
	public SpriteRenderer pass;

	// Token: 0x040008C9 RID: 2249
	public Sprite passBack;

	// Token: 0x040008CA RID: 2250
	public TextRenderer NameText;

	// Token: 0x040008CB RID: 2251
	public SpriteRenderer ImageBg;

	// Token: 0x040008CC RID: 2252
	public SpriteRenderer Image;

	// Token: 0x040008CD RID: 2253
	public Sprite[] Photos;

	// Token: 0x040008CE RID: 2254
	public PassiveButton pullButton;

	// Token: 0x040008CF RID: 2255
	public PassiveButton flipButton;

	// Token: 0x040008D0 RID: 2256
	public SpriteRenderer Scanner;

	// Token: 0x040008D1 RID: 2257
	public Sprite ScannerAccept;

	// Token: 0x040008D2 RID: 2258
	public Sprite ScannerScanning;

	// Token: 0x040008D3 RID: 2259
	public Sprite ScannerWaiting;

	// Token: 0x040008D4 RID: 2260
	public Collider2D Sensor;

	// Token: 0x040008D5 RID: 2261
	public Collider2D BarCode;

	// Token: 0x040008D6 RID: 2262
	public AudioClip slideinSound;

	// Token: 0x040008D7 RID: 2263
	public AudioClip flipSound;

	// Token: 0x040008D8 RID: 2264
	public AudioClip scanStartSound;

	// Token: 0x040008D9 RID: 2265
	public AudioClip scanSound;

	// Token: 0x040008DA RID: 2266
	private Coroutine blinky;

	// Token: 0x040008DB RID: 2267
	private Controller controller;

	// Token: 0x040008DC RID: 2268
	private bool prevHadInput;

	// Token: 0x040008DD RID: 2269
	private float rotateAngle;

	// Token: 0x040008DE RID: 2270
	private Vector2 prevStickDir;

	// Token: 0x040008DF RID: 2271
	private bool enableControllerPassMovement;

	// Token: 0x040008E0 RID: 2272
	private bool grabbed;
}
