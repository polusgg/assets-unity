using System;
using System.Collections;
using System.Text;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class MedScanMinigame : Minigame
{
	// Token: 0x0600076B RID: 1899 RVA: 0x0002F0DC File Offset: 0x0002D2DC
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.medscan = (ShipStatus.Instance.Systems[SystemTypes.MedBay] as MedScanSystem);
		this.gauge.Value = 0f;
		base.transform.position = new Vector3(100f, 0f, 0f);
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		int playerId = (int)data.PlayerId;
		int colorId = data.ColorId;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedID, Array.Empty<object>()) + " ");
		string text = DestroyableSingleton<TranslationController>.Instance.GetString(MedScanMinigame.ColorNames[colorId], Array.Empty<object>()).ToUpperInvariant();
		if (text.Length > 3)
		{
			text = text.Substring(0, 3);
		}
		stringBuilder.Append(text);
		stringBuilder.Append("P" + playerId.ToString());
		stringBuilder.Append(new string(' ', 8));
		stringBuilder.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedHT, Array.Empty<object>()) + " 3' 6\"");
		stringBuilder.Append(new string(' ', 8));
		stringBuilder.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedWT, Array.Empty<object>()) + " 92lb");
		stringBuilder.AppendLine();
		stringBuilder.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedC, Array.Empty<object>()) + " ");
		stringBuilder.Append(DestroyableSingleton<TranslationController>.Instance.GetString(MedScanMinigame.ColorNames[colorId], Array.Empty<object>()).PadRight(17));
		stringBuilder.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedBT, Array.Empty<object>()) + " ");
		stringBuilder.Append(MedScanMinigame.BloodTypes[playerId * 3 % MedScanMinigame.BloodTypes.Length]);
		this.completeString = stringBuilder.ToString();
		this.charStats.text = string.Empty;
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.MedBay, playerId | 128);
		this.walking = base.StartCoroutine(this.WalkToOffset());
		base.SetupInput(true);
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0002F308 File Offset: 0x0002D508
	private IEnumerator WalkToOffset()
	{
		this.state = MedScanMinigame.PositionState.WalkingToOffset;
		PlayerPhysics myPhysics = PlayerControl.LocalPlayer.MyPhysics;
		Vector2 vector = ShipStatus.Instance.MedScanner.Position;
		Vector2 vector2 = Vector2.left.Rotate((float)(PlayerControl.LocalPlayer.PlayerId * 36));
		vector += vector2 / 2f;
		Camera.main.GetComponent<FollowerCamera>().Locked = false;
		yield return myPhysics.WalkPlayerTo(vector, 0.001f, 1f);
		yield return new WaitForSeconds(0.1f);
		Camera.main.GetComponent<FollowerCamera>().Locked = true;
		this.walking = null;
		yield break;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0002F317 File Offset: 0x0002D517
	private IEnumerator WalkToPad()
	{
		this.state = MedScanMinigame.PositionState.WalkingToPad;
		PlayerPhysics myPhysics = PlayerControl.LocalPlayer.MyPhysics;
		Vector2 worldPos = ShipStatus.Instance.MedScanner.Position;
		Camera.main.GetComponent<FollowerCamera>().Locked = false;
		yield return myPhysics.WalkPlayerTo(worldPos, 0.001f, 1f);
		yield return new WaitForSeconds(0.1f);
		Camera.main.GetComponent<FollowerCamera>().Locked = true;
		this.walking = null;
		yield break;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0002F328 File Offset: 0x0002D528
	private void FixedUpdate()
	{
		if (this.MyNormTask.IsComplete)
		{
			return;
		}
		byte playerId = PlayerControl.LocalPlayer.PlayerId;
		if (this.medscan.CurrentUser != playerId)
		{
			if (this.medscan.CurrentUser == 255)
			{
				this.text.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedscanRequested, Array.Empty<object>());
				return;
			}
			GameData.PlayerInfo playerById = GameData.Instance.GetPlayerById(this.medscan.CurrentUser);
			this.text.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedscanWaitingFor, new object[]
			{
				playerById.PlayerName
			});
			return;
		}
		else
		{
			if (this.state != MedScanMinigame.PositionState.WalkingToPad)
			{
				if (this.walking != null)
				{
					base.StopCoroutine(this.walking);
				}
				this.walking = base.StartCoroutine(this.WalkToPad());
				return;
			}
			if (this.walking != null)
			{
				return;
			}
			if (this.ScanTimer == 0f)
			{
				PlayerControl.LocalPlayer.RpcSetScanner(true);
				SoundManager.Instance.PlaySound(this.ScanSound, false, 1f);
				VibrationManager.Vibrate(0.3f, 0.3f, 0f, VibrationManager.VibrationFalloff.None, this.ScanSound, false);
			}
			this.ScanTimer += Time.fixedDeltaTime;
			this.gauge.Value = this.ScanTimer / this.ScanDuration;
			int num = (int)(Mathf.Min(1f, this.ScanTimer / this.ScanDuration * 1.25f) * (float)this.completeString.Length);
			if (num > this.charStats.text.Length)
			{
				this.charStats.text = this.completeString.Substring(0, num);
				if (this.completeString[num - 1] != ' ')
				{
					SoundManager.Instance.PlaySoundImmediate(this.TextSound, false, 0.7f, 0.3f);
				}
			}
			if (this.ScanTimer >= this.ScanDuration)
			{
				PlayerControl.LocalPlayer.RpcSetScanner(false);
				this.text.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedscanCompleted, Array.Empty<object>());
				this.MyNormTask.NextStep();
				ShipStatus.Instance.RpcRepairSystem(SystemTypes.MedBay, (int)(playerId | 64));
				base.StartCoroutine(base.CoStartClose(0.75f));
				return;
			}
			this.text.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedscanCompleteIn, new object[]
			{
				(int)(this.ScanDuration - this.ScanTimer)
			});
			return;
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
	public override void Close()
	{
		base.StopAllCoroutines();
		byte playerId = PlayerControl.LocalPlayer.PlayerId;
		SoundManager.Instance.StopSound(this.TextSound);
		SoundManager.Instance.StopSound(this.ScanSound);
		PlayerControl.LocalPlayer.RpcSetScanner(false);
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.MedBay, (int)(playerId | 64));
		Camera.main.GetComponent<FollowerCamera>().Locked = false;
		base.Close();
	}

	// Token: 0x0400085F RID: 2143
	private static readonly StringNames[] ColorNames = new StringNames[]
	{
		StringNames.ColorRed,
		StringNames.ColorBlue,
		StringNames.ColorGreen,
		StringNames.ColorPink,
		StringNames.ColorOrange,
		StringNames.ColorYellow,
		StringNames.ColorBlack,
		StringNames.ColorWhite,
		StringNames.ColorPurple,
		StringNames.ColorBrown,
		StringNames.ColorCyan,
		StringNames.ColorLime
	};

	// Token: 0x04000860 RID: 2144
	private static readonly string[] BloodTypes = new string[]
	{
		"O-",
		"A-",
		"B-",
		"AB-",
		"O+",
		"A+",
		"B+",
		"AB+"
	};

	// Token: 0x04000861 RID: 2145
	public TMPro.TextMeshPro text;

	// Token: 0x04000862 RID: 2146
	public TMPro.TextMeshPro charStats;

	// Token: 0x04000863 RID: 2147
	public HorizontalGauge gauge;

	// Token: 0x04000864 RID: 2148
	private MedScanSystem medscan;

	// Token: 0x04000865 RID: 2149
	public float ScanDuration = 10f;

	// Token: 0x04000866 RID: 2150
	public float ScanTimer;

	// Token: 0x04000867 RID: 2151
	private string completeString;

	// Token: 0x04000868 RID: 2152
	public AudioClip ScanSound;

	// Token: 0x04000869 RID: 2153
	public AudioClip TextSound;

	// Token: 0x0400086A RID: 2154
	private Coroutine walking;

	// Token: 0x0400086B RID: 2155
	private MedScanMinigame.PositionState state;

	// Token: 0x020003BB RID: 955
	private enum PositionState
	{
		// Token: 0x04001A37 RID: 6711
		None,
		// Token: 0x04001A38 RID: 6712
		WalkingToPad,
		// Token: 0x04001A39 RID: 6713
		WalkingToOffset
	}
}
