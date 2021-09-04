using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200007B RID: 123
public class AirshipExileController : ExileController
{
	// Token: 0x06000302 RID: 770 RVA: 0x00013C3A File Offset: 0x00011E3A
	protected override IEnumerator Animate()
	{
		if (this.exiled != null)
		{
			PlayerControl.SetPlayerMaterialColors(this.exiled.ColorId, this.HandSlot);
		}
		float duration = 0.7f + this.Duration + 0.5f + 2f;
		base.StartCoroutine(Effects.All(new IEnumerator[]
		{
			this.SlowMoSlide2D(this.ForegroundCloud.transform, new Vector2(-1.4f, -2.41f), new Vector2(-0.4f, -2.41f), duration),
			this.SlowMoSlide2D(this.BackgroundCloud.transform, new Vector2(-0.97f, -1.043f), new Vector2(0.25f, -1.043f), duration),
			this.SlowMoSlide2D(this.Cloud1.transform, new Vector2(3f, 0.25f), new Vector2(6.5f, 0.25f), duration),
			this.SlowMoSlide2D(this.Cloud2.transform, new Vector2(-6f, 3f), new Vector2(5f, 3f), duration),
			this.SlowMoSlide2D(this.Cloud3.transform, new Vector2(-4f, -2.2f), new Vector2(4f, -2.2f), duration)
		}));
		yield return DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.black, Color.clear, 0.2f);
		yield return Effects.Wait(0.5f);
		yield return Effects.All(new IEnumerator[]
		{
			this.PlayerFall(),
			this.HandleText()
		});
		yield return new WaitForSeconds(0.5f);
		yield return DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.clear, Color.black, 0.2f);
		yield return this.WrapUpAndSpawn();
		yield break;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00013C49 File Offset: 0x00011E49
	private IEnumerator SlowMoSlide2D(Transform target, Vector2 source, Vector2 dest, float duration)
	{
		Vector3 temp = default(Vector3);
		temp.z = target.localPosition.z;
		for (float time = 0f; time < duration; time += Time.deltaTime * this.CloudSlowMo)
		{
			float num = time / duration;
			temp.x = Mathf.SmoothStep(source.x, dest.x, num);
			temp.y = Mathf.SmoothStep(source.y, dest.y, num);
			target.localPosition = temp;
			yield return null;
		}
		temp.x = dest.x;
		temp.y = dest.y;
		target.localPosition = temp;
		yield break;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00013C75 File Offset: 0x00011E75
	protected IEnumerator WrapUpAndSpawn()
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
			yield return ShipStatus.Instance.PrespawnStep();
			DestroyableSingleton<HudManager>.Instance.StartCoroutine(DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.black, Color.clear, 0.2f));
			PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
			ShipStatus.Instance.EmergencyCooldown = (float)PlayerControl.GameOptions.EmergencyCooldown;
			Camera.main.GetComponent<FollowerCamera>().Locked = false;
			DestroyableSingleton<HudManager>.Instance.SetHudActive(true);
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00013C84 File Offset: 0x00011E84
	private IEnumerator HandleText()
	{
		yield return Effects.Wait(1.75f);
		if (this.exiled != null)
		{
			this.CloudSlowMo = 0.1f;
			this.PlayerSlowMo = 0.1f;
			SoundManager.Instance.PlaySound(this.Stinger, false, 1f);
		}
		float newDur = this.Duration * 0.5f;
		for (float t = 0f; t <= newDur; t += Time.deltaTime)
		{
			int num = (int)(t / newDur * (float)this.completeString.Length);
			if (num > this.Text.text.Length)
			{
				this.Text.text = this.completeString.Substring(0, num);
				this.Text.gameObject.SetActive(true);
				if (this.completeString[num - 1] != ' ')
				{
					SoundManager.Instance.PlaySoundImmediate(this.TextSound, false, 0.8f, 1f);
				}
			}
			yield return null;
		}
		this.Text.text = this.completeString;
		yield return Effects.Wait(1f);
		this.CloudSlowMo = 1f;
		this.PlayerSlowMo = 6f;
		yield return Effects.Wait(0.5f);
		if (PlayerControl.GameOptions.ConfirmImpostor)
		{
			this.ImpostorText.gameObject.SetActive(true);
		}
		yield return Effects.Bloop(0f, this.ImpostorText.transform, 1f, 0.5f);
		yield break;
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00013C93 File Offset: 0x00011E93
	private IEnumerator PlayerFall()
	{
		Camera main = Camera.main;
		float num = main.orthographicSize + 1.5f;
		float num2 = main.orthographicSize * main.aspect;
		Vector2 sourcePos = Vector2.up * num + Vector2.right * num2 / 4f;
		Vector2 targetPos = Vector2.down * num + Vector2.left * num2 / 4f;
		Vector2 vector = (targetPos - sourcePos) / 2f;
		Vector2 anchor = sourcePos + vector + vector.Rotate(-90f).normalized * 0.5f;
		float d = this.Duration;
		for (float t = 0f; t <= d; t += Time.deltaTime * this.PlayerSlowMo)
		{
			float num3 = t / d;
			Vector2 vector2 = Effects.Bezier(num3, sourcePos, targetPos, anchor);
			this.Player.transform.localPosition = vector2;
			float num4 = Mathf.Lerp(0f, 80f, num3);
			this.Player.transform.localEulerAngles = new Vector3(0f, 0f, num4);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400037E RID: 894
	public SpriteRenderer ForegroundCloud;

	// Token: 0x0400037F RID: 895
	public SpriteRenderer BackgroundCloud;

	// Token: 0x04000380 RID: 896
	public SpriteRenderer Cloud1;

	// Token: 0x04000381 RID: 897
	public SpriteRenderer Cloud2;

	// Token: 0x04000382 RID: 898
	public SpriteRenderer Cloud3;

	// Token: 0x04000383 RID: 899
	public SpriteRenderer HandSlot;

	// Token: 0x04000384 RID: 900
	public AudioClip Stinger;

	// Token: 0x04000385 RID: 901
	private float CloudSlowMo = 1f;

	// Token: 0x04000386 RID: 902
	private float PlayerSlowMo = 1f;
}
