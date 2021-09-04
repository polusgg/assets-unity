using System;
using PowerTools;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class Vent : MonoBehaviour, IUsable
{
	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0004BF9E File Offset: 0x0004A19E
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.VentButton;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0004BFA2 File Offset: 0x0004A1A2
	public float UsableDistance
	{
		get
		{
			return 0.75f;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0004BFA9 File Offset: 0x0004A1A9
	public float PercentCool
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0004BFB0 File Offset: 0x0004A1B0
	private void Start()
	{
		this.SetButtons(false);
		this.myRend = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0004BFC8 File Offset: 0x0004A1C8
	public void SetButtons(bool enabled)
	{
		Vent[] array = new Vent[]
		{
			this.Right,
			this.Left,
			this.Center
		};
		Vector2 vector;
		if (this.Right && this.Left)
		{
			vector = (this.Right.transform.position + this.Left.transform.position) / 2f - base.transform.position;
		}
		else
		{
			vector = Vector2.zero;
		}
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			ButtonBehavior buttonBehavior = this.Buttons[i];
			if (enabled)
			{
				Vent vent = array[i];
				if (vent)
				{
					buttonBehavior.gameObject.SetActive(true);
					Vector3 vector2 = vent.transform.position - base.transform.position;
					Vector3 vector3 = vector2.normalized * (0.7f + this.spreadShift);
					vector3.x *= Mathf.Sign(ShipStatus.Instance.transform.localScale.x);
					vector3.y -= 0.08f;
					vector3.z = -10f;
					buttonBehavior.transform.localPosition = vector3;
					buttonBehavior.transform.LookAt2d(vent.transform);
					vector3 = vector3.RotateZ((vector.AngleSigned(vector2) > 0f) ? this.spreadAmount : (-this.spreadAmount));
					buttonBehavior.transform.localPosition = vector3;
					buttonBehavior.transform.Rotate(0f, 0f, (vector.AngleSigned(vector2) > 0f) ? this.spreadAmount : (-this.spreadAmount));
				}
				else
				{
					buttonBehavior.gameObject.SetActive(false);
				}
			}
			else
			{
				buttonBehavior.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0004C1C8 File Offset: 0x0004A3C8
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		couldUse = (pc.IsImpostor && !pc.IsDead && (@object.CanMove || @object.inVent));
		canUse = couldUse;
		if (canUse)
		{
			Vector2 truePosition = @object.GetTruePosition();
			Vector3 position = base.transform.position;
			num = Vector2.Distance(truePosition, position);
			canUse &= (num <= this.UsableDistance && !PhysicsHelpers.AnythingBetween(truePosition, position, Constants.ShipOnlyMask, false));
		}
		return num;
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0004C258 File Offset: 0x0004A458
	public void SetOutline(bool on, bool mainTarget)
	{
		this.myRend.material.SetFloat("_Outline", (float)(on ? 1 : 0));
		this.myRend.material.SetColor("_OutlineColor", Color.red);
		this.myRend.material.SetColor("_AddColor", mainTarget ? Color.red : Color.clear);
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0004C2C0 File Offset: 0x0004A4C0
	public void ClickRight()
	{
		if (this.Right && PlayerControl.LocalPlayer.inVent)
		{
			Vent.DoMove(this.Right.transform.position);
			this.SetButtons(false);
			this.Right.SetButtons(true);
			Vent.currentVent = this.Right;
		}
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0004C31C File Offset: 0x0004A51C
	public void ClickLeft()
	{
		if (this.Left && PlayerControl.LocalPlayer.inVent)
		{
			Vent.DoMove(this.Left.transform.position);
			this.SetButtons(false);
			this.Left.SetButtons(true);
			Vent.currentVent = this.Left;
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0004C378 File Offset: 0x0004A578
	public void ClickCenter()
	{
		if (this.Center && PlayerControl.LocalPlayer.inVent)
		{
			Vent.DoMove(this.Center.transform.position);
			this.SetButtons(false);
			this.Center.SetButtons(true);
			Vent.currentVent = this.Center;
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0004C3D4 File Offset: 0x0004A5D4
	private static void DoMove(Vector3 pos)
	{
		//pos -= PlayerControl.LocalPlayer.Collider.offset;
		//PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(pos);
		//if (Constants.ShouldPlaySfx())
		//{
		//	SoundManager.Instance.PlaySound(ShipStatus.Instance.VentMoveSounds.Random<AudioClip>(), false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		//}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0004C44C File Offset: 0x0004A64C
	public void Use()
	{
		bool flag;
		bool flag2;
		this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		PlayerControl localPlayer = PlayerControl.LocalPlayer;
		if (localPlayer.inVent)
		{
			localPlayer.MyPhysics.RpcExitVent(this.Id);
			this.SetButtons(false);
			return;
		}
		localPlayer.MyPhysics.RpcEnterVent(this.Id);
		this.SetButtons(true);
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0004C4B4 File Offset: 0x0004A6B4
	internal void EnterVent(PlayerControl pc)
	{
		if (pc.AmOwner)
		{
			Vent.currentVent = this;
			ConsoleJoystick.SetMode_Vent();
		}
		if (!this.EnterVentAnim)
		{
			return;
		}
		base.GetComponent<SpriteAnim>().Play(this.EnterVentAnim, 1f);
		if (pc.AmOwner && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.StopSound(ShipStatus.Instance.VentEnterSound);
			SoundManager.Instance.PlaySound(ShipStatus.Instance.VentEnterSound, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0004C54C File Offset: 0x0004A74C
	internal void ExitVent(PlayerControl pc)
	{
		if (pc.AmOwner)
		{
			Vent.currentVent = null;
		}
		if (!this.ExitVentAnim)
		{
			return;
		}
		base.GetComponent<SpriteAnim>().Play(this.ExitVentAnim, 1f);
		if (pc.AmOwner && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.StopSound(ShipStatus.Instance.VentEnterSound);
			SoundManager.Instance.PlaySound(ShipStatus.Instance.VentEnterSound, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		}
	}

	// Token: 0x04000DD5 RID: 3541
	public int Id;

	// Token: 0x04000DD6 RID: 3542
	public Vent Left;

	// Token: 0x04000DD7 RID: 3543
	public Vent Right;

	// Token: 0x04000DD8 RID: 3544
	public Vent Center;

	// Token: 0x04000DD9 RID: 3545
	public ButtonBehavior[] Buttons;

	// Token: 0x04000DDA RID: 3546
	public AnimationClip EnterVentAnim;

	// Token: 0x04000DDB RID: 3547
	public AnimationClip ExitVentAnim;

	// Token: 0x04000DDC RID: 3548
	private SpriteRenderer myRend;

	// Token: 0x04000DDD RID: 3549
	public Vector3 Offset = new Vector3(0f, 0.3636057f, 0f);

	// Token: 0x04000DDE RID: 3550
	public float spreadAmount;

	// Token: 0x04000DDF RID: 3551
	public float spreadShift;

	// Token: 0x04000DE0 RID: 3552
	public static Vent currentVent;
}
