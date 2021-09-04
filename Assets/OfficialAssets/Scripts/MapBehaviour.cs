using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class MapBehaviour : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001EC84 File Offset: 0x0001CE84
	public bool IsOpen
	{
		get
		{
			return base.isActiveAndEnabled;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001EC8C File Offset: 0x0001CE8C
	public bool IsOpenStopped
	{
		get
		{
			return this.IsOpen && this.countOverlay.isActiveAndEnabled;
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0001ECA3 File Offset: 0x0001CEA3
	private void Awake()
	{
		MapBehaviour.Instance = this;
		this.specialInputHandler = base.GetComponent<SpecialInputHandler>();
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
	private void GenericShow()
	{
		base.transform.localScale = Vector3.one;
		base.transform.localPosition = new Vector3(0f, 0f, -25f);
		Vector3 localScale = this.taskOverlay.transform.localScale;
		if (Mathf.Sign(localScale.x) != Mathf.Sign(ShipStatus.Instance.transform.localScale.x))
		{
			localScale.x *= -1f;
		}
		this.taskOverlay.transform.localScale = localScale;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0001ED58 File Offset: 0x0001CF58
	public void ShowInfectedMap()
	{
		if (this.IsOpen)
		{
			this.Close();
			return;
		}
		if (!PlayerControl.LocalPlayer.CanMove)
		{
			return;
		}
		if (this.specialInputHandler != null)
		{
			this.specialInputHandler.disableVirtualCursor = true;
		}
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.HerePoint);
		this.GenericShow();
		this.infectedOverlay.gameObject.SetActive(true);
		this.ColorControl.SetColor(Palette.ImpostorRed);
		this.taskOverlay.Hide();
		DestroyableSingleton<HudManager>.Instance.SetHudActive(false);
		ConsoleJoystick.SetMode_Sabotage();
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
	public void ShowNormalMap()
	{
		if (this.IsOpen)
		{
			this.Close();
			return;
		}
		if (!PlayerControl.LocalPlayer.CanMove)
		{
			return;
		}
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.HerePoint);
		this.GenericShow();
		this.taskOverlay.Show();
		this.ColorControl.SetColor(new Color(0.05f, 0.2f, 1f, 1f));
		DestroyableSingleton<HudManager>.Instance.SetHudActive(false);
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001EE69 File Offset: 0x0001D069
	public void ShowCountOverlay()
	{
		this.GenericShow();
		this.countOverlay.gameObject.SetActive(true);
		this.taskOverlay.Hide();
		this.HerePoint.enabled = false;
		DestroyableSingleton<HudManager>.Instance.SetHudActive(false);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
	public void FixedUpdate()
	{
		if (!ShipStatus.Instance)
		{
			return;
		}
		Vector3 vector = PlayerControl.LocalPlayer.transform.position;
		vector /= ShipStatus.Instance.MapScale;
		vector.x *= Mathf.Sign(ShipStatus.Instance.transform.localScale.x);
		vector.z = -1f;
		this.HerePoint.transform.localPosition = vector;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0001EF20 File Offset: 0x0001D120
	public void Close()
	{
		base.gameObject.SetActive(false);
		this.countOverlay.gameObject.SetActive(false);
		this.infectedOverlay.gameObject.SetActive(false);
		this.taskOverlay.Hide();
		this.HerePoint.enabled = true;
		DestroyableSingleton<HudManager>.Instance.SetHudActive(true);
	}

	// Token: 0x040005A0 RID: 1440
	public static MapBehaviour Instance;

	// Token: 0x040005A1 RID: 1441
	public AlphaPulse ColorControl;

	// Token: 0x040005A2 RID: 1442
	public SpriteRenderer HerePoint;

	// Token: 0x040005A3 RID: 1443
	public MapCountOverlay countOverlay;

	// Token: 0x040005A4 RID: 1444
	public InfectedOverlay infectedOverlay;

	// Token: 0x040005A5 RID: 1445
	public MapTaskOverlay taskOverlay;

	// Token: 0x040005A6 RID: 1446
	private SpecialInputHandler specialInputHandler;
}
