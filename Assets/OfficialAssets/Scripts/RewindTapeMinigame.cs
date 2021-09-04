using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class RewindTapeMinigame : Minigame
{
	// Token: 0x0600015E RID: 350 RVA: 0x00009E8C File Offset: 0x0000808C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.targetTime = BitConverter.ToSingle(this.MyNormTask.Data, 0);
		this.currentTime = BitConverter.ToSingle(this.MyNormTask.Data, 4);
		this.UpdateText(this.TargetText, this.targetTime);
		this.UpdateText(this.CurrentText, this.currentTime);
		this.loopSound = SoundManager.Instance.GetNamedAudioSource("rewindLoop");
		this.loopSound.volume = 0f;
		this.loopSound.pitch = 0.5f;
		this.loopSound.loop = true;
		this.loopSound.clip = this.playLoopSound;
		this.loopSound.Play();
		base.SetupInput(true);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00009F58 File Offset: 0x00008158
	private void UpdateText(TextRenderer targetText, float targetTime)
	{
		int num = (int)(targetTime / 3600f);
		int num2 = (int)((targetTime - (float)(num * 3600)) / 60f);
		int num3 = (int)(targetTime % 60f);
		targetText.Text = string.Format("{0}:{1:00}:{2:00}", num, num2, num3);
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00009FAC File Offset: 0x000081AC
	public void Update()
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		if (this.direction != 0f)
		{
			this.currentTime += this.direction * Time.deltaTime;
			if (this.currentTime < 0f)
			{
				this.currentTime = 0f;
				this.Pause();
			}
			if (this.currentTime > 83544f)
			{
				this.currentTime = 83544f;
				this.Pause();
			}
			if (Mathf.Abs(this.direction) < 120f)
			{
				if (this.direction < -1.05f)
				{
					this.direction -= 0.1f;
				}
				if (this.direction > 1.05f)
				{
					this.direction += 0.1f;
				}
			}
			this.UpdateText(this.CurrentText, this.currentTime);
			this.LeftWheel.transform.Rotate(0f, 0f, 5f * this.direction);
			this.RightWheel.transform.Rotate(0f, 0f, 5f * this.direction);
			float num = FloatRange.ReverseLerp(this.currentTime, 0f, 83544f);
			float num2 = Mathf.Lerp(0.52f, 1f, num);
			this.LeftTape.transform.localScale = new Vector3(num2, num2, num2);
			num2 = Mathf.Lerp(0.52f, 1f, 1f - num);
			this.LeftTape.transform.localScale = new Vector3(num2, num2, num2);
			if (Constants.ShouldPlaySfx())
			{
				this.loopSound.volume = Mathf.Lerp(this.loopSound.volume, 1f, Time.deltaTime);
				float num3 = Mathf.Lerp(0.5f, 1.25f, Mathf.Abs(this.direction) / 2f);
				this.loopSound.pitch = Mathf.Lerp(this.loopSound.pitch, num3, Time.deltaTime);
				return;
			}
		}
		else if (Mathf.Abs(this.targetTime - this.currentTime) <= 1f)
		{
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.5f));
		}
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000A1EC File Offset: 0x000083EC
	private void SetYPos(Transform t, float newYPos)
	{
		Vector3 localPosition = t.localPosition;
		localPosition.y = newYPos;
		t.localPosition = localPosition;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000A210 File Offset: 0x00008410
	public void Rewind()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.buttonSound, false, 1f);
		}
		this.direction = -1.1f;
		this.RewindButton.sprite = this.RewindDown;
		this.PlayButton.sprite = this.PlayNormal;
		this.PauseButton.sprite = this.PauseNormal;
		this.FastFwdButton.sprite = this.FastFwdNormal;
		this.RewindGlyph.color = this.pressedGlyphColor;
		this.PlayGlyph.color = this.upGlyphColor;
		this.PauseGlyph.color = this.upGlyphColor;
		this.FastFwdGlyph.color = this.upGlyphColor;
		this.SetYPos(this.RewindGlyph.transform, this.pressedGlyphYPos);
		this.SetYPos(this.PlayGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.PauseGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.FastFwdGlyph.transform, this.upGlyphYPos);
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000A32C File Offset: 0x0000852C
	public void FastForward()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.buttonSound, false, 1f);
		}
		this.direction = 1.1f;
		this.RewindButton.sprite = this.RewindNormal;
		this.PlayButton.sprite = this.PlayNormal;
		this.PauseButton.sprite = this.PauseNormal;
		this.FastFwdButton.sprite = this.FastFwdDown;
		this.RewindGlyph.color = this.upGlyphColor;
		this.PlayGlyph.color = this.upGlyphColor;
		this.PauseGlyph.color = this.upGlyphColor;
		this.FastFwdGlyph.color = this.pressedGlyphColor;
		this.SetYPos(this.RewindGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.PlayGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.PauseGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.FastFwdGlyph.transform, this.pressedGlyphYPos);
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000A448 File Offset: 0x00008648
	public void Pause()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.buttonSound, false, 1f);
		}
		this.direction = 0f;
		this.RewindButton.sprite = this.RewindNormal;
		this.PlayButton.sprite = this.PlayNormal;
		this.PauseButton.sprite = this.PauseDown;
		this.FastFwdButton.sprite = this.FastFwdNormal;
		this.loopSound.volume = 0f;
		this.loopSound.pitch = 0.5f;
		this.RewindGlyph.color = this.upGlyphColor;
		this.PlayGlyph.color = this.upGlyphColor;
		this.PauseGlyph.color = this.pressedGlyphColor;
		this.FastFwdGlyph.color = this.upGlyphColor;
		this.SetYPos(this.RewindGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.PlayGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.PauseGlyph.transform, this.pressedGlyphYPos);
		this.SetYPos(this.FastFwdGlyph.transform, this.upGlyphYPos);
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000A584 File Offset: 0x00008784
	public void Play()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.buttonSound, false, 1f);
		}
		this.direction = 1f;
		this.RewindButton.sprite = this.RewindNormal;
		this.PlayButton.sprite = this.PlayDown;
		this.PauseButton.sprite = this.PauseNormal;
		this.FastFwdButton.sprite = this.FastFwdNormal;
		this.RewindGlyph.color = this.upGlyphColor;
		this.PlayGlyph.color = this.pressedGlyphColor;
		this.PauseGlyph.color = this.upGlyphColor;
		this.FastFwdGlyph.color = this.upGlyphColor;
		this.SetYPos(this.RewindGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.PlayGlyph.transform, this.pressedGlyphYPos);
		this.SetYPos(this.PauseGlyph.transform, this.upGlyphYPos);
		this.SetYPos(this.FastFwdGlyph.transform, this.upGlyphYPos);
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000A6A0 File Offset: 0x000088A0
	public override void Close()
	{
		SoundManager.Instance.StopNamedSound("rewindLoop");
		BitConverter.GetBytes(this.targetTime).CopyTo(this.MyNormTask.Data, 0);
		BitConverter.GetBytes(this.currentTime).CopyTo(this.MyNormTask.Data, 4);
		base.Close();
	}

	// Token: 0x040001BC RID: 444
	private const float MaxTime = 83544f;

	// Token: 0x040001BD RID: 445
	private float targetTime;

	// Token: 0x040001BE RID: 446
	private float currentTime;

	// Token: 0x040001BF RID: 447
	public SpriteRenderer LeftWheel;

	// Token: 0x040001C0 RID: 448
	public SpriteRenderer LeftTape;

	// Token: 0x040001C1 RID: 449
	public SpriteRenderer RightWheel;

	// Token: 0x040001C2 RID: 450
	public SpriteRenderer RightTape;

	// Token: 0x040001C3 RID: 451
	public TextRenderer TargetText;

	// Token: 0x040001C4 RID: 452
	public TextRenderer CurrentText;

	// Token: 0x040001C5 RID: 453
	public SpriteRenderer RewindButton;

	// Token: 0x040001C6 RID: 454
	public Sprite RewindNormal;

	// Token: 0x040001C7 RID: 455
	public Sprite RewindDown;

	// Token: 0x040001C8 RID: 456
	public SpriteRenderer FastFwdButton;

	// Token: 0x040001C9 RID: 457
	public Sprite FastFwdNormal;

	// Token: 0x040001CA RID: 458
	public Sprite FastFwdDown;

	// Token: 0x040001CB RID: 459
	public SpriteRenderer PlayButton;

	// Token: 0x040001CC RID: 460
	public Sprite PlayNormal;

	// Token: 0x040001CD RID: 461
	public Sprite PlayDown;

	// Token: 0x040001CE RID: 462
	public SpriteRenderer PauseButton;

	// Token: 0x040001CF RID: 463
	public Sprite PauseNormal;

	// Token: 0x040001D0 RID: 464
	public Sprite PauseDown;

	// Token: 0x040001D1 RID: 465
	public SpriteRenderer RewindGlyph;

	// Token: 0x040001D2 RID: 466
	public SpriteRenderer FastFwdGlyph;

	// Token: 0x040001D3 RID: 467
	public SpriteRenderer PlayGlyph;

	// Token: 0x040001D4 RID: 468
	public SpriteRenderer PauseGlyph;

	// Token: 0x040001D5 RID: 469
	public float upGlyphYPos;

	// Token: 0x040001D6 RID: 470
	public float pressedGlyphYPos;

	// Token: 0x040001D7 RID: 471
	public Color upGlyphColor;

	// Token: 0x040001D8 RID: 472
	public Color pressedGlyphColor;

	// Token: 0x040001D9 RID: 473
	private float direction;

	// Token: 0x040001DA RID: 474
	public AudioClip buttonSound;

	// Token: 0x040001DB RID: 475
	public AudioClip playStartSound;

	// Token: 0x040001DC RID: 476
	public AudioClip playLoopSound;

	// Token: 0x040001DD RID: 477
	public AudioClip playStopSound;

	// Token: 0x040001DE RID: 478
	private AudioSource loopSound;
}
