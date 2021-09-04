using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class UseButtonManager : MonoBehaviour
{
	// Token: 0x060005F2 RID: 1522 RVA: 0x00026CC0 File Offset: 0x00024EC0
	public void SetTarget(IUsable target)
	{
		this.currentTarget = target;
		if (target != null)
		{
			if (target is Vent)
			{
				this.UseButton.sprite = DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.VentButton);
			}
			else if (target is OptionsConsole)
			{
				this.UseButton.sprite = DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.OptionsButton);
			}
			else
			{
				this.UseButton.sprite = DestroyableSingleton<TranslationController>.Instance.GetImage(target.UseIcon);
			}
			this.UseButton.SetCooldownNormalizedUvs();
			this.UseButton.material.SetFloat("_Percent", target.PercentCool);
			this.UseButton.color = UseButtonManager.EnabledColor;
			return;
		}
		PlayerControl localPlayer = PlayerControl.LocalPlayer;
		if (((localPlayer != null) ? localPlayer.Data : null) != null && PlayerControl.LocalPlayer.Data.IsImpostor && PlayerControl.LocalPlayer.CanMove)
		{
			this.UseButton.sprite = DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.SabotageButton);
			this.UseButton.SetCooldownNormalizedUvs();
			this.UseButton.color = UseButtonManager.EnabledColor;
			return;
		}
		this.UseButton.sprite = DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.UseButton);
		this.UseButton.color = UseButtonManager.DisabledColor;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00026DF8 File Offset: 0x00024FF8
	public void DoClick()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		if (!PlayerControl.LocalPlayer)
		{
			return;
		}
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		if (this.currentTarget != null)
		{
			PlayerControl.LocalPlayer.UseClosest();
			return;
		}
		if (data != null && data.IsImpostor)
		{
			DestroyableSingleton<HudManager>.Instance.ShowMap(delegate(MapBehaviour m)
			{
				m.ShowInfectedMap();
			});
		}
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00026E6D File Offset: 0x0002506D
	internal void Refresh()
	{
		this.SetTarget(this.currentTarget);
	}

	// Token: 0x040006A7 RID: 1703
	private static readonly Color DisabledColor = new Color(1f, 1f, 1f, 0.3f);

	// Token: 0x040006A8 RID: 1704
	private static readonly Color EnabledColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x040006A9 RID: 1705
	public SpriteRenderer UseButton;

	// Token: 0x040006AA RID: 1706
	private IUsable currentTarget;
}
