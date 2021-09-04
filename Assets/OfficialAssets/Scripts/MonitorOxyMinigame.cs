using System;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class MonitorOxyMinigame : Minigame
{
	// Token: 0x060007E5 RID: 2021 RVA: 0x000329F0 File Offset: 0x00030BF0
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		for (int i = 0; i < this.Sliders.Length; i++)
		{
			BoxCollider2D boxCollider2D = this.Sliders[i];
			Vector3 localPosition = boxCollider2D.transform.localPosition;
			localPosition.y = this.RandomRanges[i].Next();
			boxCollider2D.transform.localPosition = localPosition;
			float value = this.YRange.ReverseLerp(localPosition.y);
			this.Fills[i].Value = value;
		}
		base.SetupInput(true);
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00032A74 File Offset: 0x00030C74
	public void Update()
	{
		//this.controller.Update();
		//Vector2 axis2DRaw = ReInput.players.GetPlayer(0).GetAxis2DRaw(13, 14);
		//if (Mathf.Abs(axis2DRaw.x) > 0.7f)
		//{
		//	if (this.selectCooldown <= 0f)
		//	{
		//		this.selectedIndex += (int)Mathf.Sign(axis2DRaw.x);
		//		this.selectedIndex = Mathf.Clamp(this.selectedIndex, 0, this.Sliders.Length - 1);
		//		this.selectorObject.SetParent(this.Sliders[this.selectedIndex].transform, false);
		//		if (this.prevHadInput)
		//		{
		//			if (this.ActiveSound)
		//			{
		//				this.ActiveSound.Stop();
		//			}
		//			this.prevHadInput = false;
		//		}
		//		this.selectCooldown = 0.2f;
		//	}
		//}
		//else
		//{
		//	this.selectCooldown = 0f;
		//}
		//BoxCollider2D boxCollider2D = this.Sliders[this.selectedIndex];
		//if (boxCollider2D.enabled && Mathf.Abs(axis2DRaw.x) < 0.7f && !this.isTouchInput)
		//{
		//	if (Mathf.Abs(axis2DRaw.y) > 0.05f)
		//	{
		//		if (!this.prevHadInput && Constants.ShouldPlaySfx())
		//		{
		//			this.ActiveSound = SoundManager.Instance.PlaySound(this.DragSounds[this.selectedIndex], true, 0.7f);
		//		}
		//		Vector3 localPosition = boxCollider2D.transform.localPosition;
		//		localPosition.y += axis2DRaw.y * 2f * Time.deltaTime;
		//		localPosition.y = this.YRange.Clamp(localPosition.y);
		//		boxCollider2D.transform.localPosition = localPosition;
		//		float num = this.YRange.ReverseLerp(localPosition.y);
		//		this.Fills[this.selectedIndex].Value = num;
		//		if (this.ActiveSound)
		//		{
		//			this.ActiveSound.pitch = Mathf.Lerp(0.8f, 1.2f, num);
		//		}
		//		this.prevHadInput = true;
		//	}
		//	else
		//	{
		//		if (this.prevHadInput && this.ActiveSound)
		//		{
		//			this.ActiveSound.Stop();
		//		}
		//		SpriteRenderer spriteRenderer = this.Targets[this.selectedIndex];
		//		if (Mathf.Abs(boxCollider2D.transform.localPosition.y - spriteRenderer.transform.localPosition.y) < 0.1f)
		//		{
		//			boxCollider2D.enabled = false;
		//			spriteRenderer.color = Color.green;
		//			if (this.Sliders.All((BoxCollider2D s) => !s.enabled))
		//			{
		//				this.MyNormTask.NextStep();
		//				base.StartCoroutine(base.CoStartClose(0.75f));
		//			}
		//			VibrationManager.Vibrate(0.2f, 0.2f, 0.2f, VibrationManager.VibrationFalloff.None, null, false);
		//		}
		//		this.prevHadInput = false;
		//	}
		//}
		//for (int i = 0; i < this.Sliders.Length; i++)
		//{
		//	BoxCollider2D boxCollider2D2 = this.Sliders[i];
		//	if (boxCollider2D2.enabled)
		//	{
		//		switch (this.controller.CheckDrag(boxCollider2D2))
		//		{
		//		case DragState.TouchStart:
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				this.ActiveSound = SoundManager.Instance.PlaySound(this.DragSounds[i], true, 0.7f);
		//			}
		//			this.isTouchInput = true;
		//			break;
		//		case DragState.Dragging:
		//		{
		//			Vector2 vector = this.controller.DragPosition - boxCollider2D2.transform.parent.position;
		//			Vector3 localPosition2 = boxCollider2D2.transform.localPosition;
		//			localPosition2.y = this.YRange.Clamp(vector.y);
		//			boxCollider2D2.transform.localPosition = localPosition2;
		//			float num2 = this.YRange.ReverseLerp(localPosition2.y);
		//			this.Fills[i].Value = num2;
		//			if (this.ActiveSound)
		//			{
		//				this.ActiveSound.pitch = Mathf.Lerp(0.8f, 1.2f, num2);
		//			}
		//			break;
		//		}
		//		case DragState.Released:
		//		{
		//			if (this.ActiveSound)
		//			{
		//				this.ActiveSound.Stop();
		//			}
		//			SpriteRenderer spriteRenderer2 = this.Targets[i];
		//			if (Mathf.Abs(boxCollider2D2.transform.localPosition.y - spriteRenderer2.transform.localPosition.y) < 0.1f)
		//			{
		//				boxCollider2D2.enabled = false;
		//				spriteRenderer2.color = Color.green;
		//				if (this.Sliders.All((BoxCollider2D s) => !s.enabled))
		//				{
		//					this.MyNormTask.NextStep();
		//					base.StartCoroutine(base.CoStartClose(0.75f));
		//				}
		//				VibrationManager.Vibrate(0.2f, 0.2f, 0.2f, VibrationManager.VibrationFalloff.None, null, false);
		//			}
		//			this.isTouchInput = false;
		//			break;
		//		}
		//		}
		//	}
		//}
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00032F67 File Offset: 0x00031167
	public override void Close()
	{
		if (this.ActiveSound)
		{
			this.ActiveSound.Stop();
		}
		base.Close();
	}

	// Token: 0x04000920 RID: 2336
	public SpriteRenderer[] Targets;

	// Token: 0x04000921 RID: 2337
	public BoxCollider2D[] Sliders;

	// Token: 0x04000922 RID: 2338
	public VerticalSpriteGauge[] Fills;

	// Token: 0x04000923 RID: 2339
	public FloatRange YRange;

	// Token: 0x04000924 RID: 2340
	public FloatRange[] RandomRanges;

	// Token: 0x04000925 RID: 2341
	private Controller controller = new Controller();

	// Token: 0x04000926 RID: 2342
	public AudioClip[] DragSounds;

	// Token: 0x04000927 RID: 2343
	private AudioSource ActiveSound;

	// Token: 0x04000928 RID: 2344
	public Transform selectorObject;

	// Token: 0x04000929 RID: 2345
	private int selectedIndex;

	// Token: 0x0400092A RID: 2346
	private bool prevHadInput;

	// Token: 0x0400092B RID: 2347
	private float selectCooldown;

	// Token: 0x0400092C RID: 2348
	private bool isTouchInput;
}
