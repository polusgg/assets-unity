using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class SortGameObject : MonoBehaviour
{
	// Token: 0x06000651 RID: 1617 RVA: 0x00028C39 File Offset: 0x00026E39
	public IEnumerator CoShadowRise()
	{
		for (float timer = 0f; timer < 0.15f; timer += Time.deltaTime)
		{
			float num = timer / 0.15f * 0.35f;
			this.Collider.offset = new Vector2(0f, -num);
			this.Shadow.transform.localPosition = new Vector3(0f, num, -0.0001f);
			yield return null;
		}
		Vector3 localPosition = base.transform.localPosition;
		localPosition.z = 0f;
		base.transform.localPosition = localPosition;
		yield break;
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00028C48 File Offset: 0x00026E48
	public IEnumerator CoShadowFall(bool inBox, AudioClip dropSound)
	{
		if (inBox)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.z = 2.5f;
			base.transform.localPosition = localPosition;
		}
		for (float timer = 0f; timer < 0.15f; timer += Time.deltaTime)
		{
			float num = (1f - timer / 0.15f) * 0.35f;
			this.Collider.offset = new Vector2(0f, -num);
			this.Shadow.transform.localPosition = new Vector3(0f, num, -0.0001f);
			yield return null;
		}
		VibrationManager.Vibrate(0.2f, 0.2f, 0.05f, VibrationManager.VibrationFalloff.None, null, false);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(dropSound, false, 1f);
		}
		yield return Effects.Shake(base.transform, 0.075f, 0.05f, false);
		yield break;
	}

	// Token: 0x04000713 RID: 1811
	public SortGameObject.ObjType MyType;

	// Token: 0x04000714 RID: 1812
	public Collider2D Collider;

	// Token: 0x04000715 RID: 1813
	public SpriteRenderer Image;

	// Token: 0x04000716 RID: 1814
	public SpriteRenderer Shadow;

	// Token: 0x04000717 RID: 1815
	private const float ShadowTime = 0.15f;

	// Token: 0x0200038F RID: 911
	public enum ObjType
	{
		// Token: 0x040019AB RID: 6571
		Plant,
		// Token: 0x040019AC RID: 6572
		Mineral,
		// Token: 0x040019AD RID: 6573
		Animal
	}
}
