using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class ExileController : MonoBehaviour
{
	// Token: 0x06000312 RID: 786 RVA: 0x00014255 File Offset: 0x00012455
	private void Awake()
	{
		this.specialInputHandler = base.GetComponent<SpecialInputHandler>();
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00014264 File Offset: 0x00012464
	public void Begin(GameData.PlayerInfo exiled, bool tie)
	{
		if (this.specialInputHandler != null)
		{
			this.specialInputHandler.disableVirtualCursor = true;
		}
		ExileController.Instance = this;
		this.exiled = exiled;
		this.Text.gameObject.SetActive(false);
		this.Text.text = string.Empty;
		int num = GameData.Instance.AllPlayers.Count((GameData.PlayerInfo p) => p.IsImpostor && !p.IsDead && !p.Disconnected);
		if (exiled != null)
		{
			int num2 = GameData.Instance.AllPlayers.Count((GameData.PlayerInfo p) => p.IsImpostor);
			if (!PlayerControl.GameOptions.ConfirmImpostor)
			{
				this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ExileTextNonConfirm, new object[]
				{
					exiled.PlayerName
				});
			}
			else if (exiled.IsImpostor)
			{
				if (num2 > 1)
				{
					this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ExileTextPP, new object[]
					{
						exiled.PlayerName
					});
				}
				else
				{
					this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ExileTextSP, new object[]
					{
						exiled.PlayerName
					});
				}
			}
			else if (num2 > 1)
			{
				this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ExileTextPN, new object[]
				{
					exiled.PlayerName
				});
			}
			else
			{
				this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ExileTextSN, new object[]
				{
					exiled.PlayerName
				});
			}
			PlayerControl.SetPlayerMaterialColors(exiled.ColorId, this.Player);
			this.PlayerHat.SetHat(exiled.HatId, exiled.ColorId);
			this.PlayerSkin.sprite = DestroyableSingleton<HatManager>.Instance.GetSkinById(exiled.SkinId).EjectFrame;
			if (exiled.IsImpostor)
			{
				num--;
			}
		}
		else
		{
			if (tie)
			{
				this.completeString = "TestPlayer was not the Impostor";
				// this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.NoExileTie, Array.Empty<object>());
			}
			else
			{
				this.completeString = "TestPlayer was not the Impostor";
				// this.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.NoExileSkip, Array.Empty<object>());
			}
			this.Player.gameObject.SetActive(false);
		}
		if (num == 1)
		{
			this.ImpostorText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ImpostorsRemainS, new object[]
			{
				num
			});
		}
		else
		{
			this.ImpostorText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ImpostorsRemainP, new object[]
			{
				num
			});
		}
		base.StartCoroutine(this.Animate());
	}

	// Token: 0x06000314 RID: 788 RVA: 0x000144E5 File Offset: 0x000126E5
	protected virtual IEnumerator Animate()
	{
		// yield return DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.black, Color.clear, 0.2f);
		yield return new WaitForSeconds(1f);
		float num = Camera.main.orthographicSize * Camera.main.aspect + 1f;
		Vector2 left = Vector2.left * num;
		Vector2 right = Vector2.right * num;
		for (float t = 0f; t <= this.Duration; t += Time.deltaTime)
		{
			float num2 = t / this.Duration;
			//this.Player.transform.localPosition = Vector2.Lerp(left, right, this.LerpCurve.Evaluate(num2));
			float num3 = (t + 0.75f) * 25f / Mathf.Exp(t * 0.75f + 1f);
			//this.Player.transform.Rotate(new Vector3(0f, 0f, num3 * Time.deltaTime * 30f));
			if (num2 >= 0.3f)
			{
				int num4 = (int)(Mathf.Min(1f, (num2 - 0.3f) / 0.3f) * (float)this.completeString.Length);
				if (num4 > this.Text.text.Length)
				{
					this.Text.text = this.completeString.Substring(0, num4);
					this.Text.gameObject.SetActive(true);
					if (this.completeString[num4 - 1] != ' ')
					{
						SoundManager.Instance.PlaySoundImmediate(this.TextSound, false, 0.8f, 1f);
					}
				}
			}
			yield return null;
		}
		this.Text.text = this.completeString;
		if (PlayerControl.GameOptions.ConfirmImpostor)
		{
			this.ImpostorText.gameObject.SetActive(true);
		}
		yield return Effects.Bloop(0f, this.ImpostorText.transform, 1f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		// yield return DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.clear, Color.black, 0.2f);
		this.WrapUp();
		yield break;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x000144F4 File Offset: 0x000126F4
	protected void WrapUp()
	{
		if (this.exiled != null)
		{
			PlayerControl @object = this.exiled.Object;
			if (@object)
			{
				@object.Exiled();
			}
			this.exiled.IsDead = true;
		}
		if (DestroyableSingleton<TutorialManager>.InstanceExists || !ShipStatus.Instance.IsGameOverDueToDeath())
		{
			// DestroyableSingleton<HudManager>.Instance.StartCoroutine(DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.black, Color.clear, 0.2f));
			PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
			ShipStatus.Instance.EmergencyCooldown = (float)PlayerControl.GameOptions.EmergencyCooldown;
			Camera.main.GetComponent<FollowerCamera>().Locked = false;
			// DestroyableSingleton<HudManager>.Instance.SetHudActive(true);
			ControllerManager.Instance.ResetAll();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000395 RID: 917
	public static ExileController Instance;

	// Token: 0x04000396 RID: 918
	public TextMeshPro ImpostorText;

	// Token: 0x04000397 RID: 919
	public TextMeshPro Text;

	// Token: 0x04000398 RID: 920
	public SpriteRenderer Player;

	// Token: 0x04000399 RID: 921
	public HatParent PlayerHat;

	// Token: 0x0400039A RID: 922
	public SpriteRenderer PlayerSkin;

	// Token: 0x0400039B RID: 923
	public AnimationCurve LerpCurve;

	// Token: 0x0400039C RID: 924
	public float Duration = 7f;

	// Token: 0x0400039D RID: 925
	public AudioClip TextSound;

	// Token: 0x0400039E RID: 926
	protected string completeString = "TestPlayer was not The Impostor";

	// Token: 0x0400039F RID: 927
	protected GameData.PlayerInfo exiled;

	// Token: 0x040003A0 RID: 928
	private SpecialInputHandler specialInputHandler;
}
