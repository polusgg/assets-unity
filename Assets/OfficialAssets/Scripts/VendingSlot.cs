using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class VendingSlot : MonoBehaviour
{
	// Token: 0x06000409 RID: 1033 RVA: 0x0001ABC7 File Offset: 0x00018DC7
	public IEnumerator CoBuy(AudioClip sliderOpen, AudioClip drinkShake, AudioClip drinkLand)
	{
		VibrationManager.Vibrate(0.05f, 0.05f, 0.75f, VibrationManager.VibrationFalloff.None, null, false);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(sliderOpen, false, 1f);
		}
		yield return new WaitForLerp(0.75f, delegate(float v)
		{
			this.GlassImage.size = new Vector2(1f, Mathf.Lerp(1.7f, 0f, v));
			this.GlassImage.transform.localPosition = new Vector3(0f, Mathf.Lerp(0f, 0.85f, v), -1f);
		});
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(drinkShake, false, 1f);
		}
		yield return Effects.SwayX(this.DrinkImage.transform, 0.75f, 0.075f);
		Vector3 localPosition = this.DrinkImage.transform.localPosition;
		localPosition.z = -5f;
		this.DrinkImage.transform.localPosition = localPosition;
		Vector3 vector = localPosition;
		vector.y = -8f - localPosition.y;
		yield return Effects.All(new IEnumerator[]
		{
			Effects.Slide2D(this.DrinkImage.transform, localPosition, vector, 0.75f),
			Effects.Rotate2D(this.DrinkImage.transform, 0f, -FloatRange.Next(-45f, 45f), 0.75f),
			Effects.Sequence(new IEnumerator[]
			{
				Effects.Wait(0.25f),
				this.PlayLand(drinkLand)
			})
		});
		this.DrinkImage.enabled = false;
		yield break;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001ABEB File Offset: 0x00018DEB
	public IEnumerator CloseSlider(AudioClip sliderOpen)
	{
		VibrationManager.Vibrate(0.05f, 0.05f, 0.75f, VibrationManager.VibrationFalloff.None, null, false);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(sliderOpen, false, 1f);
		}
		yield return new WaitForLerp(0.75f, delegate(float v)
		{
			this.GlassImage.size = new Vector2(1f, Mathf.Lerp(0f, 1.7f, v));
			this.GlassImage.transform.localPosition = new Vector3(0f, Mathf.Lerp(0.85f, 0f, v), -1f);
		});
		yield break;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001AC01 File Offset: 0x00018E01
	private IEnumerator PlayLand(AudioClip drinkLand)
	{
		VibrationManager.Vibrate(0.4f, 0.4f, 0.5f, VibrationManager.VibrationFalloff.Linear, null, false);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(drinkLand, false, 1f);
		}
		yield break;
	}

	// Token: 0x040004C7 RID: 1223
	public SpriteRenderer DrinkImage;

	// Token: 0x040004C8 RID: 1224
	public SpriteRenderer GlassImage;

	// Token: 0x040004C9 RID: 1225
	private const float SlideDuration = 0.75f;

	// Token: 0x040004CA RID: 1226
	private const float SlideVibrateIntensity = 0.05f;

	// Token: 0x040004CB RID: 1227
	private const float DrunkThunkVibrateIntensity = 0.4f;
}
