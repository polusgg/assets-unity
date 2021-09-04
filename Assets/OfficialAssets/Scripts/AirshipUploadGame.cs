using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Token: 0x02000039 RID: 57
public class AirshipUploadGame : Minigame
{
	// Token: 0x06000187 RID: 391 RVA: 0x0000C478 File Offset: 0x0000A678
	public void Start()
	{
        this.Hotspot.transform.localPosition = Random.insideUnitCircle.normalized * 2.5f;
        PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.Phone);
        base.SetupInput(false);
        this.glyphColor = this.promptGlyph.color;
    }

	// Token: 0x06000188 RID: 392 RVA: 0x0000C4DC File Offset: 0x0000A6DC
	public void Update()
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		float num = 0f;
		if (this.Hotspot.IsTouching(this.Perfect))
		{
			this.IconRend.sprite = this.PerfectIcon;
			num = Time.deltaTime * 4f;
		}
		else if (this.Hotspot.IsTouching(this.Good))
		{
			this.IconRend.sprite = this.GoodIcon;
			num = Time.deltaTime * 2f;
		}
		else if (this.Hotspot.IsTouching(this.Poor))
		{
			this.IconRend.sprite = this.PoorIcon;
			num = Time.deltaTime;
		}
		else
		{
			this.IconRend.sprite = this.NoneIcon;
		}
		this.cont.Update();
		if (this.glyphColor.a > 0f)
		{
			if (this.glyphDisappearDelay > 0f)
			{
				this.glyphDisappearDelay -= Time.deltaTime;
			}
			else
			{
				this.promptGlyph.color = this.glyphColor;
				this.glyphColor.a = this.glyphColor.a - Time.deltaTime;
			}
		}
		else if (this.promptGlyph.gameObject.activeSelf)
		{
			this.promptGlyph.gameObject.SetActive(false);
		}
		if (Controller.currentTouchType == Controller.TouchType.Joystick)
		{
			this.Phone.transform.position = VirtualCursor.currentPosition;
		}
		this.timer += num;
		if (Constants.ShouldPlaySfx())
		{
			this.beepTimer += (10f - Vector2.Distance(this.Hotspot.transform.localPosition, this.Phone.transform.localPosition)) * num / this.BeepPeriod;
			if (this.beepTimer >= 1f)
			{
				this.beepTimer = 0f;
				SoundManager.Instance.PlaySoundImmediate(this.nearSound, false, 1f, 1f);
			}
		}
		if (this.phoneGrabbed)
		{
			Vector3 vector = this.Phone.transform.position;
			float z = vector.z;
			vector = DestroyableSingleton<PassiveButtonManager>.Instance.controller.Touches[0].Position;
			vector.z = z;
			this.Phone.transform.position = vector;
		}
		this.gauge.Value = this.timer / 20f;
		if (this.timer >= 20f)
		{
			this.MyNormTask.NextStep();
			this.Close();
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000C760 File Offset: 0x0000A960
	public void ToggleGrab()
	{
		this.phoneGrabbed = !this.phoneGrabbed;
	}

	// Token: 0x0400023C RID: 572
	public SpriteRenderer Phone;

	// Token: 0x0400023D RID: 573
	public Collider2D Hotspot;

	// Token: 0x0400023E RID: 574
	public Collider2D Perfect;

	// Token: 0x0400023F RID: 575
	public Collider2D Good;

	// Token: 0x04000240 RID: 576
	public Collider2D Poor;

	// Token: 0x04000241 RID: 577
	public SpriteRenderer IconRend;

	// Token: 0x04000242 RID: 578
	public Sprite PerfectIcon;

	// Token: 0x04000243 RID: 579
	public Sprite GoodIcon;

	// Token: 0x04000244 RID: 580
	public Sprite PoorIcon;

	// Token: 0x04000245 RID: 581
	public Sprite NoneIcon;

	// Token: 0x04000246 RID: 582
	public HorizontalGauge gauge;

	// Token: 0x04000247 RID: 583
	public float moveSpeed = 1f;

	// Token: 0x04000248 RID: 584
	private const float MaxTimer = 20f;

	// Token: 0x04000249 RID: 585
	private float timer;

	// Token: 0x0400024A RID: 586
	public AudioClip nearSound;

	// Token: 0x0400024B RID: 587
	public float BeepPeriod = 0.5f;

	// Token: 0x0400024C RID: 588
	private float beepTimer;

	// Token: 0x0400024D RID: 589
	public Controller cont = new Controller();

	// Token: 0x0400024E RID: 590
	public SpriteRenderer promptGlyph;

	// Token: 0x0400024F RID: 591
	private Color glyphColor;

	// Token: 0x04000250 RID: 592
	private float glyphDisappearDelay = 5f;

	// Token: 0x04000251 RID: 593
	private bool phoneGrabbed;
}
