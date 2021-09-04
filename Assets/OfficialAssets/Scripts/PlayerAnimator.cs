using System;
using System.Collections;
using PowerTools;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class PlayerAnimator : MonoBehaviour
{
	// Token: 0x06000422 RID: 1058 RVA: 0x0001B140 File Offset: 0x00019340
	private void Start()
	{
		this.Animator = base.GetComponent<SpriteAnim>();
		this.rend = base.GetComponent<SpriteRenderer>();
		this.rend.material.SetColor("_BackColor", Palette.ShadowColors[0]);
		this.rend.material.SetColor("_BodyColor", Palette.PlayerColors[0]);
		this.rend.material.SetColor("_VisorColor", Palette.VisorColor);
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0001B1CE File Offset: 0x000193CE
	public void FixedUpdate()
	{
		base.transform.Translate(this.velocity * Time.fixedDeltaTime);
		this.UseButton.enabled = (this.NearbyConsoles > 0);
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0001B204 File Offset: 0x00019404
	public void LateUpdate()
	{
		if (this.velocity.sqrMagnitude >= 0.1f)
		{
			if (this.Animator.GetCurrentAnimation() != this.RunAnim)
			{
				this.Animator.Play(this.RunAnim, 1f);
			}
			this.rend.flipX = (this.velocity.x < 0f);
			return;
		}
		if (this.Animator.GetCurrentAnimation() == this.RunAnim)
		{
			this.Animator.Play(this.IdleAnim, 1f);
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001B29D File Offset: 0x0001949D
	public IEnumerator WalkPlayerTo(Vector2 worldPos, bool relax, float tolerance = 0.01f)
	{
		//worldPos.y += 0.3636f;
		//if (!(this.joystick is DemoKeyboardStick))
		//{
		//	this.finger.ClickOn();
		//}
		//for (;;)
		//{
		//	Vector2 vector2;
		//	Vector2 vector = vector2 = worldPos - base.transform.position;
		//	if (vector2.sqrMagnitude <= tolerance)
		//	{
		//		break;
		//	}
		//	float num = Mathf.Clamp(vector.magnitude * 2f, 0.01f, 1f);
		//	this.velocity = vector.normalized * this.Speed * num;
		//	this.joystick.UpdateJoystick(this.finger, this.velocity, true);
		//	yield return null;
		//}
		//if (relax)
		//{
		//	this.finger.ClickOff();
		//	this.velocity = Vector2.zero;
		//	this.joystick.UpdateJoystick(this.finger, this.velocity, false);
		//}
		yield break;
	}

	// Token: 0x040004E0 RID: 1248
	public float Speed = 2.5f;

	// Token: 0x040004E1 RID: 1249
	public VirtualJoystick joystick;

	// Token: 0x040004E2 RID: 1250
	public SpriteRenderer UseButton;

	// Token: 0x040004E3 RID: 1251
	public FingerBehaviour finger;

	// Token: 0x040004E4 RID: 1252
	public AnimationClip RunAnim;

	// Token: 0x040004E5 RID: 1253
	public AnimationClip IdleAnim;

	// Token: 0x040004E6 RID: 1254
	private Vector2 velocity;

	// Token: 0x040004E7 RID: 1255
	[HideInInspector]
	private SpriteAnim Animator;

	// Token: 0x040004E8 RID: 1256
	[HideInInspector]
	private SpriteRenderer rend;

	// Token: 0x040004E9 RID: 1257
	public int NearbyConsoles;
}
