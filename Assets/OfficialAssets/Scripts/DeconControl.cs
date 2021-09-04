using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A0 RID: 160
public class DeconControl : MonoBehaviour, IUsable
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060003D8 RID: 984 RVA: 0x00019BEC File Offset: 0x00017DEC
	public ImageNames UseIcon
	{
		get
		{
			return ImageNames.UseButton;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060003D9 RID: 985 RVA: 0x00019BF0 File Offset: 0x00017DF0
	public float UsableDistance
	{
		get
		{
			return this.usableDistance;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060003DA RID: 986 RVA: 0x00019BF8 File Offset: 0x00017DF8
	public float PercentCool
	{
		get
		{
			return this.cooldown / 6f;
		}
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00019C08 File Offset: 0x00017E08
	public void SetOutline(bool on, bool mainTarget)
	{
		if (this.Image)
		{
			this.Image.material.SetFloat("_Outline", (float)(on ? 1 : 0));
			this.Image.material.SetColor("_OutlineColor", Color.white);
			this.Image.material.SetColor("_AddColor", mainTarget ? Color.white : Color.clear);
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00019C7D File Offset: 0x00017E7D
	public void Update()
	{
		this.cooldown = Mathf.Max(this.cooldown - Time.deltaTime, 0f);
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00019C9C File Offset: 0x00017E9C
	public float CanUse(GameData.PlayerInfo pc, out bool canUse, out bool couldUse)
	{
		if (this.System.CurState != DeconSystem.States.Idle)
		{
			canUse = false;
			couldUse = false;
			return 0f;
		}
		float num = float.MaxValue;
		PlayerControl @object = pc.Object;
		Vector2 truePosition = @object.GetTruePosition();
		Vector3 position = base.transform.position;
		position.y -= 0.1f;
		couldUse = (@object.CanMove && !pc.IsDead && !PhysicsHelpers.AnythingBetween(truePosition, position, Constants.ShipAndObjectsMask, false));
		canUse = (couldUse && this.cooldown == 0f);
		if (canUse)
		{
			num = Vector2.Distance(truePosition, position);
			canUse &= (num <= this.UsableDistance);
		}
		return num;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00019D58 File Offset: 0x00017F58
	public void Use()
	{
		bool flag;
		bool flag2;
		this.CanUse(PlayerControl.LocalPlayer.Data, out flag, out flag2);
		if (!flag)
		{
			return;
		}
		this.cooldown = 6f;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.UseSound, false, 1f);
		}
		this.OnUse.Invoke();
	}

	// Token: 0x04000486 RID: 1158
	public DeconSystem System;

	// Token: 0x04000487 RID: 1159
	public float usableDistance = 1f;

	// Token: 0x04000488 RID: 1160
	public SpriteRenderer Image;

	// Token: 0x04000489 RID: 1161
	public AudioClip UseSound;

	// Token: 0x0400048A RID: 1162
	public Button.ButtonClickedEvent OnUse;

	// Token: 0x0400048B RID: 1163
	private const float CooldownDuration = 6f;

	// Token: 0x0400048C RID: 1164
	private float cooldown;
}
