using System;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class PolishRubyGame : Minigame
{
	// Token: 0x06000159 RID: 345 RVA: 0x000098EC File Offset: 0x00007AEC
	public void Start()
	{
		this.swipes = new int[this.Buttons.Length];
		this.directions = new Vector2[this.Buttons.Length];
		this.Buttons.ForEach(delegate(PassiveButton b)
		{
			b.gameObject.SetActive(false);
		});
		int num = IntRange.Next(3, 5);
		for (int i = 0; i < num; i++)
		{
			(from t in this.Buttons
			where !t.isActiveAndEnabled
			select t).Random<PassiveButton>().gameObject.SetActive(true);
		}
		base.SetupInput(true);
		this.UpdateSpriteColor(false);
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000099A8 File Offset: 0x00007BA8
	private void UpdateSpriteColor(bool cursorOverlapsSmudge)
	{
		Color white = Color.white;
		white.a = (cursorOverlapsSmudge ? 1f : 0.5f);
		SpriteRenderer[] array = this.handSprites;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = white;
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000099EF File Offset: 0x00007BEF
	public void PlaySparkleSound()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySoundImmediate(this.sparkleSound, false, 1f, 1f);
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00009A14 File Offset: 0x00007C14
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.cont.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	this.cursorObject.gameObject.SetActive(true);
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 axis2DRaw = player.GetAxis2DRaw(13, 14);
		//	Vector2 axis2DRaw2 = player.GetAxis2DRaw(16, 17);
		//	Vector3 localPosition = this.cursorObject.localPosition;
		//	Vector3 localPosition2 = this.handWipeObject.localPosition;
		//	localPosition.x += axis2DRaw.x * Time.deltaTime * 3f;
		//	localPosition.y += axis2DRaw.y * Time.deltaTime * 3f;
		//	localPosition2.x = axis2DRaw2.x * 0.35f;
		//	localPosition2.y = axis2DRaw2.y * 0.35f;
		//	this.cursorObject.transform.localPosition = localPosition;
		//	this.handWipeObject.transform.localPosition = localPosition2;
		//	Vector3 position = this.cursorObject.position;
		//	bool flag = false;
		//	for (int i = 0; i < this.Buttons.Length; i++)
		//	{
		//		PassiveButton passiveButton = this.Buttons[i];
		//		if (passiveButton.isActiveAndEnabled && passiveButton.Colliders[0].OverlapPoint(position))
		//		{
		//			flag = true;
		//			Vector2 vector = this.directions[i];
		//			float y = vector.y;
		//			vector.y = localPosition2.x - vector.x;
		//			vector.x = localPosition2.x;
		//			this.directions[i] = vector;
		//			if (Mathf.Sign(y) != Mathf.Sign(vector.y) && vector.y != 0f)
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySoundImmediate(this.rubSounds.Random<AudioClip>(), false, 1f, 1f);
		//				}
		//				VibrationManager.Vibrate(0.1f, 0.1f, 0.05f, VibrationManager.VibrationFalloff.Linear, null, false);
		//				int num = this.swipes[i] = this.swipes[i] + 1;
		//				if (num <= this.swipesToClean)
		//				{
		//					passiveButton.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Palette.ClearWhite, (float)num / (float)this.swipesToClean);
		//					if (num == this.swipesToClean)
		//					{
		//						this.Sparkles[i].enabled = true;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	if (this.oldCursorOverlapsSmudge != flag)
		//	{
		//		this.oldCursorOverlapsSmudge = flag;
		//		this.UpdateSpriteColor(flag);
		//	}
		//}
		//else
		//{
		//	this.cursorObject.gameObject.SetActive(false);
		//	Controller controller = DestroyableSingleton<PassiveButtonManager>.Instance.controller;
		//	if (controller.AnyTouch)
		//	{
		//		Vector2 position2 = controller.Touches[0].Position;
		//		for (int j = 0; j < this.Buttons.Length; j++)
		//		{
		//			PassiveButton passiveButton2 = this.Buttons[j];
		//			if (passiveButton2.isActiveAndEnabled && passiveButton2.Colliders[0].OverlapPoint(position2))
		//			{
		//				Vector2 vector2 = this.directions[j];
		//				float y2 = vector2.y;
		//				vector2.y = position2.x - vector2.x;
		//				vector2.x = position2.x;
		//				this.directions[j] = vector2;
		//				if (Mathf.Sign(y2) != Mathf.Sign(vector2.y) && vector2.y != 0f)
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySoundImmediate(this.rubSounds.Random<AudioClip>(), false, 1f, 1f);
		//					}
		//					int num2 = this.swipes[j] = this.swipes[j] + 1;
		//					if (num2 <= this.swipesToClean)
		//					{
		//						passiveButton2.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Palette.ClearWhite, (float)num2 / (float)this.swipesToClean);
		//						if (num2 == this.swipesToClean)
		//						{
		//							this.Sparkles[j].enabled = true;
		//						}
		//					}
		//				}
		//			}
		//		}
		//	}
		//}
		//bool flag2 = true;
		//for (int k = 0; k < this.Buttons.Length; k++)
		//{
		//	if (this.Buttons[k].isActiveAndEnabled && this.swipes[k] < this.swipesToClean)
		//	{
		//		flag2 = false;
		//		break;
		//	}
		//}
		//if (flag2)
		//{
		//	this.MyNormTask.NextStep();
		//	this.Close();
		//}
	}

	// Token: 0x040001B0 RID: 432
	public PassiveButton[] Buttons;

	// Token: 0x040001B1 RID: 433
	public SpriteRenderer[] Sparkles;

	// Token: 0x040001B2 RID: 434
	public int[] swipes;

	// Token: 0x040001B3 RID: 435
	public Vector2[] directions;

	// Token: 0x040001B4 RID: 436
	public int swipesToClean = 6;

	// Token: 0x040001B5 RID: 437
	public AudioClip[] rubSounds;

	// Token: 0x040001B6 RID: 438
	public AudioClip sparkleSound;

	// Token: 0x040001B7 RID: 439
	public Transform cursorObject;

	// Token: 0x040001B8 RID: 440
	public Transform handWipeObject;

	// Token: 0x040001B9 RID: 441
	public SpriteRenderer[] handSprites;

	// Token: 0x040001BA RID: 442
	private Controller cont = new Controller();

	// Token: 0x040001BB RID: 443
	private bool oldCursorOverlapsSmudge;
}
