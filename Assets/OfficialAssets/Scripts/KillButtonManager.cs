using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class KillButtonManager : MonoBehaviour
{
	// Token: 0x060004C0 RID: 1216 RVA: 0x0001E42D File Offset: 0x0001C62D
	public void Start()
	{
		this.renderer.SetCooldownNormalizedUvs();
		this.SetTarget(null);
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0001E444 File Offset: 0x0001C644
	public void PerformKill()
	{
		if (base.isActiveAndEnabled && this.CurrentTarget && !this.isCoolingDown && !PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.LocalPlayer.CanMove)
		{
			PlayerControl.LocalPlayer.RpcMurderPlayer(this.CurrentTarget);
			this.SetTarget(null);
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
	public void SetTarget(PlayerControl target)
	{
		if (this.CurrentTarget && this.CurrentTarget != target)
		{
			this.CurrentTarget.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 0f);
		}
		this.CurrentTarget = target;
		if (this.CurrentTarget)
		{
			SpriteRenderer component = this.CurrentTarget.GetComponent<SpriteRenderer>();
			component.material.SetFloat("_Outline", (float)(this.isActive ? 1 : 0));
			component.material.SetColor("_OutlineColor", Color.red);
			this.renderer.color = Palette.EnabledColor;
			this.renderer.material.SetFloat("_Desat", 0f);
			return;
		}
		this.renderer.color = Palette.DisabledClear;
		this.renderer.material.SetFloat("_Desat", 1f);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0001E590 File Offset: 0x0001C790
	public void SetCoolDown(float timer, float maxTimer)
	{
		float num = Mathf.Clamp(timer / maxTimer, 0f, 1f);
		if (this.renderer)
		{
			this.renderer.material.SetFloat("_Percent", num);
		}
		this.isCoolingDown = (num > 0f);
		if (this.isCoolingDown)
		{
			this.TimerText.Text = Mathf.CeilToInt(timer).ToString();
			this.TimerText.gameObject.SetActive(true);
			return;
		}
		this.TimerText.gameObject.SetActive(false);
	}

	// Token: 0x0400058C RID: 1420
	public PlayerControl CurrentTarget;

	// Token: 0x0400058D RID: 1421
	public SpriteRenderer renderer;

	// Token: 0x0400058E RID: 1422
	public TextRenderer TimerText;

	// Token: 0x0400058F RID: 1423
	public bool isCoolingDown = true;

	// Token: 0x04000590 RID: 1424
	public bool isActive;

	// Token: 0x04000591 RID: 1425
	private Vector2 uv;
}
