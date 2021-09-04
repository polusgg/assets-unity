using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class EmergencyMinigame : Minigame
{
	// Token: 0x06000BC0 RID: 3008 RVA: 0x00049F45 File Offset: 0x00048145
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00049F57 File Offset: 0x00048157
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.Update();
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected);
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00049F84 File Offset: 0x00048184
	public void Update()
	{
		if (ShipStatus.Instance.Timer < 15f || ShipStatus.Instance.EmergencyCooldown > 0f)
		{
			int num = Mathf.CeilToInt(15f - ShipStatus.Instance.Timer);
			num = Mathf.Max(Mathf.CeilToInt(ShipStatus.Instance.EmergencyCooldown), num);
			this.ButtonActive = false;
			this.StatusText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EmergencyNotReady, Array.Empty<object>());
			this.NumberText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SecondsAbbv, new object[]
			{
				num
			});
			this.ClosedLid.gameObject.SetActive(true);
			this.OpenLid.gameObject.SetActive(false);
			return;
		}
		if (!PlayerControl.LocalPlayer.myTasks.Any(new Func<PlayerTask, bool>(PlayerTask.TaskIsEmergency)))
		{
			if (this.state == 1)
			{
				return;
			}
			this.state = 1;
			int remainingEmergencies = PlayerControl.LocalPlayer.RemainingEmergencies;
			this.StatusText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EmergencyCount, new object[]
			{
				PlayerControl.LocalPlayer.Data.PlayerName
			});
			this.NumberText.text = remainingEmergencies.ToString();
			this.ButtonActive = (remainingEmergencies > 0);
			this.ClosedLid.gameObject.SetActive(!this.ButtonActive);
			this.OpenLid.gameObject.SetActive(this.ButtonActive);
			return;
		}
		else
		{
			if (this.state == 2)
			{
				return;
			}
			this.state = 2;
			this.ButtonActive = false;
			this.StatusText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EmergencyDuringCrisis, Array.Empty<object>());
			this.NumberText.text = string.Empty;
			this.ClosedLid.gameObject.SetActive(true);
			this.OpenLid.gameObject.SetActive(false);
			return;
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0004A178 File Offset: 0x00048378
	public void CallMeeting()
	{
		if (!PlayerControl.LocalPlayer.myTasks.Any(new Func<PlayerTask, bool>(PlayerTask.TaskIsEmergency)) && PlayerControl.LocalPlayer.RemainingEmergencies > 0 && this.ButtonActive)
		{
			this.StatusText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EmergencyRequested, Array.Empty<object>());
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.ButtonSound, false, 1f);
			}
			PlayerControl.LocalPlayer.CmdReportDeadBody(null);
			this.ButtonActive = false;
			VibrationManager.Vibrate(1f, 1f, 0.2f, VibrationManager.VibrationFalloff.None, null, false);
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0004A220 File Offset: 0x00048420
	private float easeOutElastic(float t)
	{
		float num = 0.3f;
		return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - num / 4f) * 6.2831855f / num) + 1f;
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0004A261 File Offset: 0x00048461
	protected override IEnumerator CoAnimateOpen()
	{
		for (float timer = 0f; timer < 0.2f; timer += Time.deltaTime)
		{
			float num = timer / 0.2f;
			base.transform.localPosition = new Vector3(0f, Mathf.SmoothStep(-8f, 0f, num), -50f);
			yield return null;
		}
		base.transform.localPosition = new Vector3(0f, 0f, -50f);
		Vector3 meetingPos = this.meetingButton.localPosition;
		for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		{
			float num2 = timer / 0.1f;
			meetingPos.y = Mathf.Sin(3.1415927f * num2) * 1f / (num2 * 5f + 4f) - 0.882f;
			this.meetingButton.localPosition = meetingPos;
			yield return null;
		}
		meetingPos.y = -0.882f;
		this.meetingButton.localPosition = meetingPos;
		yield break;
	}

	// Token: 0x04000D1C RID: 3356
	public SpriteRenderer ClosedLid;

	// Token: 0x04000D1D RID: 3357
	public SpriteRenderer OpenLid;

	// Token: 0x04000D1E RID: 3358
	public Transform meetingButton;

	// Token: 0x04000D1F RID: 3359
	public TextMeshPro StatusText;

	// Token: 0x04000D20 RID: 3360
	public TextMeshPro NumberText;

	// Token: 0x04000D21 RID: 3361
	public bool ButtonActive = true;

	// Token: 0x04000D22 RID: 3362
	public AudioClip ButtonSound;

	// Token: 0x04000D23 RID: 3363
	private int state;

	// Token: 0x04000D24 RID: 3364
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000D25 RID: 3365
	public UiElement DefaultButtonSelected;

	// Token: 0x04000D26 RID: 3366
	public const int MinEmergencyTime = 15;
}
