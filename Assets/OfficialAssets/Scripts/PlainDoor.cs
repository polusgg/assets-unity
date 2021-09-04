using System;
using System.Collections;
using Hazel;
using PowerTools;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class PlainDoor : SomeKindaDoor
{
	// Token: 0x06000B89 RID: 2953 RVA: 0x00048F70 File Offset: 0x00047170
	private void Start()
	{
		Vector2 vector = this.myCollider.size;
		this.size = ((vector.x > vector.y) ? vector.y : vector.x);
		this.Open = this.myCollider.isTrigger;
		this.animator.Play(this.Open ? this.OpenDoorAnim : this.CloseDoorAnim, 1000f);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00048FE4 File Offset: 0x000471E4
	public override void SetDoorway(bool open)
	{
		if (this.Open == open)
		{
			return;
		}
		this.Open = open;
		this.myCollider.isTrigger = open;
		this.animator.Play(open ? this.OpenDoorAnim : this.CloseDoorAnim, 1f);
		base.StopAllCoroutines();
		if (!open)
		{
			Vector2 vector = this.myCollider.size;
			base.StartCoroutine(this.CoCloseDoorway(vector.x > vector.y));
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlayDynamicSound(base.name, this.CloseSound, false, new DynamicSound.GetDynamicsFunction(this.DoorDynamics), true);
			}
			VibrationManager.Vibrate(2.5f, base.transform.position, 3f, 0f, VibrationManager.VibrationFalloff.None, this.CloseSound, false);
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlayDynamicSound(base.name, this.OpenSound, false, new DynamicSound.GetDynamicsFunction(this.DoorDynamics), true);
		}
		VibrationManager.Vibrate(2.5f, base.transform.position, 3f, 0f, VibrationManager.VibrationFalloff.None, this.OpenSound, false);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00049113 File Offset: 0x00047313
	private IEnumerator CoCloseDoorway(bool isHort)
	{
		Vector2 s = this.myCollider.size;
		float i = 0f;
		if (isHort)
		{
			while (i < 0.1f)
			{
				i += Time.deltaTime;
				s.y = Mathf.Lerp(0.0001f, this.size, i / 0.1f);
				this.myCollider.size = s;
				yield return null;
			}
		}
		else
		{
			while (i < 0.1f)
			{
				i += Time.deltaTime;
				s.x = Mathf.Lerp(0.0001f, this.size, i / 0.1f);
				this.myCollider.size = s;
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0004912C File Offset: 0x0004732C
	private void DoorDynamics(AudioSource source, float dt)
	{
		if (!PlayerControl.LocalPlayer)
		{
			source.volume = 0f;
			return;
		}
		Vector2 vector = base.transform.position;
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		float num = Vector2.Distance(vector, truePosition);
		if (num > 4f)
		{
			source.volume = 0f;
			return;
		}
		float num2 = 1f - num / 4f;
		source.volume = Mathf.Lerp(source.volume, num2, dt);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000491A8 File Offset: 0x000473A8
	public virtual void Serialize(MessageWriter writer)
	{
		writer.Write(this.Open);
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x000491B6 File Offset: 0x000473B6
	public virtual void Deserialize(MessageReader reader)
	{
		this.SetDoorway(reader.ReadBoolean());
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x000491C4 File Offset: 0x000473C4
	public virtual bool DoUpdate(float dt)
	{
		return false;
	}

	// Token: 0x04000CEE RID: 3310
	public SystemTypes Room;

	// Token: 0x04000CEF RID: 3311
	public int Id;

	// Token: 0x04000CF0 RID: 3312
	public bool Open;

	// Token: 0x04000CF1 RID: 3313
	public BoxCollider2D myCollider;

	// Token: 0x04000CF2 RID: 3314
	public SpriteAnim animator;

	// Token: 0x04000CF3 RID: 3315
	public AnimationClip OpenDoorAnim;

	// Token: 0x04000CF4 RID: 3316
	public AnimationClip CloseDoorAnim;

	// Token: 0x04000CF5 RID: 3317
	public AudioClip OpenSound;

	// Token: 0x04000CF6 RID: 3318
	public AudioClip CloseSound;

	// Token: 0x04000CF7 RID: 3319
	private float size;
}
