using System;
using System.Collections;
using Hazel;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class ManualDoor : SomeKindaDoor
{
	// Token: 0x06000B81 RID: 2945 RVA: 0x00048C80 File Offset: 0x00046E80
	private void Awake()
	{
		Vector2 vector = this.myCollider.size;
		this.size = ((vector.x > vector.y) ? vector.y : vector.x);
		this.image.SetCooldownNormalizedUvs();
		this.Opening = this.myCollider.isTrigger;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00048CD8 File Offset: 0x00046ED8
	private void Update()
	{
		if (this.Opening && this.openTimer < this.OpenDuration)
		{
			this.openTimer += Time.deltaTime;
			float num = Mathf.SmoothStep(0f, 1f, this.openTimer / this.OpenDuration);
			this.image.material.SetFloat("_PercentY", num);
			return;
		}
		if (!this.Opening && this.openTimer > 0f)
		{
			this.openTimer -= Time.deltaTime;
			float num2 = Mathf.SmoothStep(0f, 1f, this.openTimer / this.OpenDuration);
			this.image.material.SetFloat("_PercentY", num2);
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00048D9C File Offset: 0x00046F9C
	public override void SetDoorway(bool open)
	{
		if (this.Opening == open)
		{
			return;
		}
		this.Opening = open;
		this.myCollider.isTrigger = open;
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

	// Token: 0x06000B84 RID: 2948 RVA: 0x00048EAC File Offset: 0x000470AC
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

	// Token: 0x06000B85 RID: 2949 RVA: 0x00048F28 File Offset: 0x00047128
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

	// Token: 0x06000B86 RID: 2950 RVA: 0x00048F3E File Offset: 0x0004713E
	public virtual void Serialize(MessageWriter writer)
	{
		writer.Write(this.Opening);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00048F4C File Offset: 0x0004714C
	public virtual void Deserialize(MessageReader reader)
	{
		this.SetDoorway(reader.ReadBoolean());
	}

	// Token: 0x04000CE6 RID: 3302
	public bool Opening;

	// Token: 0x04000CE7 RID: 3303
	public BoxCollider2D myCollider;

	// Token: 0x04000CE8 RID: 3304
	public SpriteRenderer image;

	// Token: 0x04000CE9 RID: 3305
	private float size;

	// Token: 0x04000CEA RID: 3306
	public float OpenDuration = 0.3f;

	// Token: 0x04000CEB RID: 3307
	private float openTimer;

	// Token: 0x04000CEC RID: 3308
	public AudioClip OpenSound;

	// Token: 0x04000CED RID: 3309
	public AudioClip CloseSound;
}
