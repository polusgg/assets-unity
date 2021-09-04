using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class TelescopeGame : Minigame
{
	// Token: 0x060007F7 RID: 2039 RVA: 0x000338E4 File Offset: 0x00031AE4
	public void Start()
	{
		this.TargetItem = this.Items.Random<BoxCollider2D>();
		this.ItemDisplay.sprite = this.TargetItem.GetComponent<SpriteRenderer>().sprite;
		base.StartCoroutine(this.RunBlipSound());
		base.SetupInput(true);
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00033931 File Offset: 0x00031B31
	private IEnumerator RunBlipSound()
	{
		for (;;)
		{
			for (float time = 0f; time < this.BlipDelay.Lerp(Vector2.Distance(this.TargetItem.transform.position, this.Reticle.transform.position) / 10f); time += Time.deltaTime)
			{
				yield return null;
			}
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySoundImmediate(this.BlipSound, false, 1f, 1f);
			}
			VibrationManager.Vibrate(0.3f, 0.3f, 0.01f, VibrationManager.VibrationFalloff.Linear, null, false);
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00033940 File Offset: 0x00031B40
	public void Update()
	{
		NormalPlayerTask myNormTask = this.MyNormTask;
		if (myNormTask != null && myNormTask.IsComplete)
		{
			return;
		}
		Vector3 vector = Vector3.zero;

		Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (vector2.magnitude > 0.01f)
		{
			Vector2 vector3 = -vector2 * Time.deltaTime * 3.5f;
			vector = this.Background.transform.localPosition;
			vector.x = Mathf.Clamp(vector.x + vector3.x, -6f, 6f);
			vector.y = Mathf.Clamp(vector.y + vector3.y, -7f, 7f);
			this.Background.transform.localPosition = vector;
		}
		if (this.grabbed)
		{
			Controller controller = DestroyableSingleton<PassiveButtonManager>.Instance.controller;
			Vector2 vector4 = controller.DragPosition - controller.DragStartPosition;
			vector = this.Background.transform.localPosition;
			vector.x = Mathf.Clamp(vector.x + vector4.x, -6f, 6f);
			vector.y = Mathf.Clamp(vector.y + vector4.y, -7f, 7f);
			this.Background.transform.localPosition = vector;
			controller.ResetDragPosition();
		}
		if (this.Reticle.IsTouching(this.TargetItem))
		{
			if (this.blinky == null)
			{
				this.blinky = base.StartCoroutine(this.CoBlinky());
				return;
			}
		}
		else if (this.blinky != null)
		{
			base.StopCoroutine(this.blinky);
			this.blinky = null;
			this.ReticleImage.color = Color.white;
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00033B15 File Offset: 0x00031D15
	private IEnumerator CoBlinky()
	{
		int num;
		for (int i = 0; i < 3; i = num)
		{
			this.ReticleImage.color = Color.green;
			yield return Effects.Wait(0.1f);
			this.ReticleImage.color = Color.white;
			yield return Effects.Wait(0.2f);
			num = i + 1;
		}
		this.blinky = null;
		this.ReticleImage.color = Color.green;
		this.MyNormTask.NextStep();
		yield return base.CoStartClose(0.75f);
		yield break;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00033B24 File Offset: 0x00031D24
	public void Grab()
	{
		this.grabbed = !this.grabbed;
	}

	// Token: 0x04000948 RID: 2376
	private bool grabbed;

	// Token: 0x04000949 RID: 2377
	public Transform Background;

	// Token: 0x0400094A RID: 2378
	public SpriteRenderer ItemDisplay;

	// Token: 0x0400094B RID: 2379
	public BoxCollider2D[] Items;

	// Token: 0x0400094C RID: 2380
	private BoxCollider2D TargetItem;

	// Token: 0x0400094D RID: 2381
	public BoxCollider2D Reticle;

	// Token: 0x0400094E RID: 2382
	public SpriteRenderer ReticleImage;

	// Token: 0x0400094F RID: 2383
	private Coroutine blinky;

	// Token: 0x04000950 RID: 2384
	public AudioClip BlipSound;

	// Token: 0x04000951 RID: 2385
	public FloatRange BlipDelay = new FloatRange(0.01f, 1f);
}
