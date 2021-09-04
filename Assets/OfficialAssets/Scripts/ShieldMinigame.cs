using System;
//using Rewired;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class ShieldMinigame : Minigame
{
	// Token: 0x06000B43 RID: 2883 RVA: 0x00047B95 File Offset: 0x00045D95
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.shields = this.MyNormTask.Data[0];
		this.UpdateButtons();
		base.SetupInput(true);
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x00047BC0 File Offset: 0x00045DC0
	public void ToggleShield(int i)
	{
		if (!this.MyNormTask.IsComplete)
		{
			byte b = (byte)(1 << i);
			this.shields ^= b;
			this.MyNormTask.Data[0] = this.shields;
			if ((this.shields & b) != 0)
			{
				if (Constants.ShouldPlaySfx())
				{
					SoundManager.Instance.PlaySound(this.ShieldOnSound, false, 1f);
				}
			}
			else if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.ShieldOffSound, false, 1f);
			}
			if (this.shields == 127)
			{
				this.MyNormTask.NextStep();
				base.StartCoroutine(base.CoStartClose(0.75f));
				//if (!(ShipStatus.Instance.ShieldsImages.Length == 0 || ShipStatus.Instance.ShieldsImages[0].IsPlaying(null)))
				//{
				//	PlayerControl.LocalPlayer.RpcPlayAnimation(1);
				//}
			}
		}
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00047CA4 File Offset: 0x00045EA4
	private void Update()
	{
		//this.controller.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 normalized;
		//	normalized..ctor(player.GetAxis(13), player.GetAxis(14));
		//	int num = 0;
		//	if (normalized.sqrMagnitude > 0.5f)
		//	{
		//		normalized = normalized.normalized;
		//		float num2 = float.NegativeInfinity;
		//		num = -1;
		//		Vector2 vector = this.Shields[0].transform.localPosition;
		//		for (int i = 1; i < this.Shields.Length; i++)
		//		{
		//			float num3 = Vector2.Dot((this.Shields[i].transform.localPosition - vector).normalized, normalized);
		//			if (num == -1 || num3 > num2)
		//			{
		//				num = i;
		//				num2 = num3;
		//			}
		//		}
		//	}
		//	if (this.oldSelectedIndex != num)
		//	{
		//		Vector3 localPosition = this.selectedButtonHighlight.transform.localPosition;
		//		Vector3 localPosition2 = this.Shields[num].transform.localPosition;
		//		localPosition.x = localPosition2.x;
		//		localPosition.y = localPosition2.y;
		//		this.selectedButtonHighlight.transform.localPosition = localPosition;
		//		this.oldSelectedIndex = num;
		//	}
		//	if (player.GetButtonDown(11))
		//	{
		//		this.ToggleShield(num);
		//	}
		//}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00047DED File Offset: 0x00045FED
	public void FixedUpdate()
	{
		this.UpdateButtons();
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00047DF8 File Offset: 0x00045FF8
	private void UpdateButtons()
	{
		int num = 0;
		for (int i = 0; i < this.Shields.Length; i++)
		{
			bool flag = ((int)this.shields & 1 << i) == 0;
			if (!flag)
			{
				num++;
			}
			this.Shields[i].color = (flag ? this.OffColor : this.OnColor);
		}
		if (this.shields == 127)
		{
			this.Gauge.transform.Rotate(0f, 0f, Time.fixedDeltaTime * 45f);
			this.Gauge.color = new Color(1f, 1f, 1f, 1f);
			return;
		}
		float num2 = Mathf.Lerp(0.1f, 0.5f, (float)num / 6f);
		this.Gauge.color = new Color(1f, num2, num2, 1f);
	}

	// Token: 0x04000CB5 RID: 3253
	public Color OnColor = Color.white;

	// Token: 0x04000CB6 RID: 3254
	public Color OffColor = Color.red;

	// Token: 0x04000CB7 RID: 3255
	public SpriteRenderer[] Shields;

	// Token: 0x04000CB8 RID: 3256
	public SpriteRenderer Gauge;

	// Token: 0x04000CB9 RID: 3257
	private byte shields;

	// Token: 0x04000CBA RID: 3258
	public AudioClip ShieldOnSound;

	// Token: 0x04000CBB RID: 3259
	public AudioClip ShieldOffSound;

	// Token: 0x04000CBC RID: 3260
	public Transform selectedButtonHighlight;

	// Token: 0x04000CBD RID: 3261
	private Controller controller = new Controller();

	// Token: 0x04000CBE RID: 3262
	private int oldSelectedIndex = -1;
}
