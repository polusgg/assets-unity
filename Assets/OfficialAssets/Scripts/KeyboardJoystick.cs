using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class KeyboardJoystick : MonoBehaviour, IVirtualJoystick
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001DD75 File Offset: 0x0001BF75
	public Vector2 Delta
	{
		get
		{
			return this.del;
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0001DD80 File Offset: 0x0001BF80
	private void Update()
	{
		//if (!PlayerControl.LocalPlayer)
		//{
		//	return;
		//}
		//this.del.x = (this.del.y = 0f);
		//if (Input.GetKey(275) || Input.GetKey(100))
		//{
		//	this.del.x = this.del.x + 1f;
		//}
		//if (Input.GetKey(276) || Input.GetKey(97))
		//{
		//	this.del.x = this.del.x - 1f;
		//}
		//if (Input.GetKey(273) || Input.GetKey(119))
		//{
		//	this.del.y = this.del.y + 1f;
		//}
		//if (Input.GetKey(274) || Input.GetKey(115))
		//{
		//	this.del.y = this.del.y - 1f;
		//}
		//KeyboardJoystick.HandleHud();
		//if (Input.GetKeyDown(27))
		//{
		//	if (Minigame.Instance)
		//	{
		//		Minigame.Instance.Close();
		//	}
		//	else if (DestroyableSingleton<HudManager>.InstanceExists && MapBehaviour.Instance && MapBehaviour.Instance.IsOpen)
		//	{
		//		MapBehaviour.Instance.Close();
		//	}
		//	else if (CustomPlayerMenu.Instance)
		//	{
		//		CustomPlayerMenu.Instance.Close(true);
		//	}
		//}
		//this.del.Normalize();
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0001DED0 File Offset: 0x0001C0D0
	private static void HandleHud()
	{
		//if (!DestroyableSingleton<HudManager>.InstanceExists)
		//{
		//	return;
		//}
		//if (Input.GetKeyDown(114))
		//{
		//	DestroyableSingleton<HudManager>.Instance.ReportButton.DoClick();
		//}
		//if (Input.GetKeyDown(32) || Input.GetKeyDown(101))
		//{
		//	DestroyableSingleton<HudManager>.Instance.UseButton.DoClick();
		//}
		//if (Input.GetKeyDown(9))
		//{
		//	DestroyableSingleton<HudManager>.Instance.ShowMap(delegate(MapBehaviour m)
		//	{
		//		m.ShowNormalMap();
		//	});
		//}
		//if (PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.IsImpostor && Input.GetKeyDown(113))
		//{
		//	DestroyableSingleton<HudManager>.Instance.KillButton.PerformKill();
		//}
	}

	// Token: 0x04000581 RID: 1409
	private Vector2 del;
}
