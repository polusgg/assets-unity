using System;
using System.Collections;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class EmptyGarbageMinigame : Minigame
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x0004E9AC File Offset: 0x0004CBAC
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//int i = 0;
		//this.Objects = new SpriteRenderer[this.NumObjects];
		//RandomFill<SpriteRenderer> randomFill = new RandomFill<SpriteRenderer>();
		//if (this.MyNormTask.StartAt == SystemTypes.LifeSupp)
		//{
		//	randomFill.Set(this.GarbagePrefabs.Union(this.LeafPrefabs));
		//}
		//else
		//{
		//	NormalPlayerTask myNormTask = this.MyNormTask;
		//	if (myNormTask != null && myNormTask.taskStep == 0)
		//	{
		//		if (this.MyNormTask.TaskType == TaskTypes.EmptyChute)
		//		{
		//			randomFill.Set(this.GarbagePrefabs);
		//		}
		//		else
		//		{
		//			randomFill.Set(this.LeafPrefabs);
		//		}
		//	}
		//	else
		//	{
		//		randomFill.Set(this.GarbagePrefabs.Union(this.LeafPrefabs));
		//		while (i < this.SpecialObjectPrefabs.Length)
		//		{
		//			SpriteRenderer spriteRenderer = this.Objects[i] = Object.Instantiate<SpriteRenderer>(this.SpecialObjectPrefabs[i]);
		//			spriteRenderer.transform.SetParent(base.transform);
		//			spriteRenderer.transform.localPosition = this.SpawnRange.Next();
		//			i++;
		//		}
		//	}
		//}
		//while (i < this.Objects.Length)
		//{
		//	SpriteRenderer spriteRenderer2 = this.Objects[i] = Object.Instantiate<SpriteRenderer>(randomFill.Get());
		//	spriteRenderer2.transform.SetParent(base.transform);
		//	Vector3 vector = this.SpawnRange.Next();
		//	vector.z = FloatRange.Next(-0.5f, 0.5f);
		//	spriteRenderer2.transform.localPosition = vector;
		//	spriteRenderer2.color = Color.Lerp(Color.white, Color.black, (vector.z + 0.5f) * 0.7f);
		//	i++;
		//}
		//base.SetupInput(true);
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0004EB54 File Offset: 0x0004CD54
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.controller.Update();
		//Vector3 localPosition = this.Handle.transform.localPosition;
		//float num = this.HandleRange.ReverseLerp(localPosition.y);
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	if (!this.finished)
		//	{
		//		float num2 = Mathf.Clamp01(-ReInput.players.GetPlayer(0).GetAxis(17));
		//		localPosition.y = this.HandleRange.Lerp(1f - num2);
		//		num = this.HandleRange.ReverseLerp(localPosition.y);
		//		if (num2 >= 0.01f)
		//		{
		//			this.hadInput = true;
		//			if (num <= 0.5f && this.Blocker.enabled)
		//			{
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySound(this.LeverDown, false, 1f);
		//					SoundManager.Instance.PlaySound(this.GrinderStart, false, 0.8f);
		//					SoundManager.Instance.StopSound(this.GrinderEnd);
		//					SoundManager.Instance.StopSound(this.GrinderLoop);
		//				}
		//				this.Blocker.enabled = false;
		//				base.StopAllCoroutines();
		//				base.StartCoroutine(this.PopObjects());
		//				base.StartCoroutine(this.AnimateObjects());
		//			}
		//		}
		//		else
		//		{
		//			if (this.hadInput)
		//			{
		//				if (!this.Blocker.enabled)
		//				{
		//					this.Blocker.enabled = true;
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.LeverUp, false, 1f);
		//						SoundManager.Instance.StopSound(this.GrinderStart);
		//						SoundManager.Instance.StopSound(this.GrinderLoop);
		//						SoundManager.Instance.PlaySound(this.GrinderEnd, false, 0.8f);
		//					}
		//				}
		//				if (!this.finished)
		//				{
		//					if (this.Objects.All((SpriteRenderer o) => !o))
		//					{
		//						this.finished = true;
		//						this.MyNormTask.NextStep();
		//						base.StartCoroutine(base.CoStartClose(0.75f));
		//					}
		//				}
		//			}
		//			this.hadInput = false;
		//		}
		//	}
		//}
		//else
		//{
		//	switch (this.controller.CheckDrag(this.Handle))
		//	{
		//	case DragState.NoTouch:
		//		localPosition.y = Mathf.Lerp(localPosition.y, this.HandleRange.max, num + Time.deltaTime * 15f);
		//		break;
		//	case DragState.Dragging:
		//		if (!this.finished)
		//		{
		//			if (num > 0.5f)
		//			{
		//				Vector2 vector = this.controller.DragPosition - base.transform.position;
		//				float num3 = this.HandleRange.ReverseLerp(this.HandleRange.Clamp(vector.y));
		//				localPosition.y = this.HandleRange.Lerp(num3 / 2f + 0.5f);
		//			}
		//			else
		//			{
		//				localPosition.y = Mathf.Lerp(localPosition.y, this.HandleRange.min, num + Time.deltaTime * 15f);
		//				if (this.Blocker.enabled)
		//				{
		//					if (Constants.ShouldPlaySfx())
		//					{
		//						SoundManager.Instance.PlaySound(this.LeverDown, false, 1f);
		//						SoundManager.Instance.PlaySound(this.GrinderStart, false, 0.8f);
		//						SoundManager.Instance.StopSound(this.GrinderEnd);
		//						SoundManager.Instance.StopSound(this.GrinderLoop);
		//					}
		//					this.Blocker.enabled = false;
		//					base.StopAllCoroutines();
		//					base.StartCoroutine(this.PopObjects());
		//					base.StartCoroutine(this.AnimateObjects());
		//				}
		//			}
		//		}
		//		break;
		//	case DragState.Released:
		//		if (!this.Blocker.enabled)
		//		{
		//			this.Blocker.enabled = true;
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.LeverUp, false, 1f);
		//				SoundManager.Instance.StopSound(this.GrinderStart);
		//				SoundManager.Instance.StopSound(this.GrinderLoop);
		//				SoundManager.Instance.PlaySound(this.GrinderEnd, false, 0.8f);
		//			}
		//		}
		//		if (!this.finished)
		//		{
		//			if (this.Objects.All((SpriteRenderer o) => !o))
		//			{
		//				this.finished = true;
		//				this.MyNormTask.NextStep();
		//				base.StartCoroutine(base.CoStartClose(0.75f));
		//			}
		//		}
		//		break;
		//	}
		//}
		//if (Constants.ShouldPlaySfx() && !this.Blocker.enabled && !SoundManager.Instance.SoundIsPlaying(this.GrinderStart))
		//{
		//	SoundManager.Instance.PlaySound(this.GrinderLoop, true, 0.8f);
		//}
		//this.Handle.transform.localPosition = localPosition;
		//Vector3 localScale = this.Bars.transform.localScale;
		//localScale.y = this.HandleRange.ChangeRange(localPosition.y, -1f, 1f);
		//this.Bars.transform.localScale = localScale;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0004F074 File Offset: 0x0004D274
	private IEnumerator PopObjects()
	{
		this.Popper.enabled = true;
		yield return new WaitForSeconds(0.05f);
		this.Popper.enabled = false;
		yield break;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0004F083 File Offset: 0x0004D283
	private IEnumerator AnimateObjects()
	{
		//Vector3 pos = base.transform.localPosition;
		//for (float t = 3f; t > 0f; t -= Time.deltaTime)
		//{
		//	float num = t / 3f;
		//	float num2 = num * 0.1f * 3f;
		//	VibrationManager.Vibrate(num2, num2, 0.01f, VibrationManager.VibrationFalloff.None, null, false);
		//	base.transform.localPosition = pos + Vector2Range.NextEdge() * num * 0.1f;
		//	yield return null;
		//}
		yield break;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0004F094 File Offset: 0x0004D294
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.GrinderStart);
		SoundManager.Instance.StopSound(this.GrinderLoop);
		SoundManager.Instance.StopSound(this.GrinderEnd);
		if (this.MyNormTask && this.MyNormTask.IsComplete)
		{
			ShipStatus.Instance.OpenHatch();
			PlayerControl.LocalPlayer.RpcPlayAnimation((byte)this.MyTask.TaskType);
		}
		base.Close();
	}

	// Token: 0x04000E45 RID: 3653
	private const float GrinderVolume = 0.8f;

	// Token: 0x04000E46 RID: 3654
	public FloatRange HandleRange = new FloatRange(-0.65f, 0.65f);

	// Token: 0x04000E47 RID: 3655
	public Vector2Range SpawnRange;

	// Token: 0x04000E48 RID: 3656
	public Collider2D Blocker;

	// Token: 0x04000E49 RID: 3657
	public AreaEffector2D Popper;

	// Token: 0x04000E4A RID: 3658
	public Collider2D Handle;

	// Token: 0x04000E4B RID: 3659
	public SpriteRenderer Bars;

	// Token: 0x04000E4C RID: 3660
	private Controller controller = new Controller();

	// Token: 0x04000E4D RID: 3661
	private bool finished;

	// Token: 0x04000E4E RID: 3662
	public int NumObjects = 15;

	// Token: 0x04000E4F RID: 3663
	private SpriteRenderer[] Objects;

	// Token: 0x04000E50 RID: 3664
	public SpriteRenderer[] GarbagePrefabs;

	// Token: 0x04000E51 RID: 3665
	public SpriteRenderer[] LeafPrefabs;

	// Token: 0x04000E52 RID: 3666
	public SpriteRenderer[] SpecialObjectPrefabs;

	// Token: 0x04000E53 RID: 3667
	public AudioClip LeverDown;

	// Token: 0x04000E54 RID: 3668
	public AudioClip LeverUp;

	// Token: 0x04000E55 RID: 3669
	public AudioClip GrinderStart;

	// Token: 0x04000E56 RID: 3670
	public AudioClip GrinderLoop;

	// Token: 0x04000E57 RID: 3671
	public AudioClip GrinderEnd;

	// Token: 0x04000E58 RID: 3672
	private bool hadInput;
}
