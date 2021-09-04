using System;
using System.Collections;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x0200021E RID: 542
public class WaterPlantsGame : Minigame
{
	// Token: 0x06000CE2 RID: 3298 RVA: 0x0004F16D File Offset: 0x0004D36D
	private bool Watered(int x)
	{
		return this.MyNormTask.Data[x] > 0;
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0004F17F File Offset: 0x0004D37F
	private void Watered(int x, bool b)
	{
		//this.MyNormTask.Data[x] = (b ? 1 : 0);
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0004F198 File Offset: 0x0004D398
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		if (this.MyNormTask.taskStep == 0)
		{
			this.WaterCan.transform.localPosition = this.Locations.Random<Transform>().localPosition;
			this.WaterCan.GetComponent<SpriteRenderer>().flipX = BoolRange.Next(0.5f);
			this.stage1.gameObject.SetActive(true);
			this.stage2.gameObject.SetActive(false);
			base.SetupInput(false);
			this.grabCanSubObject.SetActive(true);
			this.holdingCanSubObject.SetActive(false);
			this.waterPlantsSubObject.SetActive(false);
			foreach (SpriteRenderer playerMaterialColors in this.playerHandObjects)
			{
				PlayerControl.LocalPlayer.SetPlayerMaterialColors(playerMaterialColors);
			}
			return;
		}
		if (this.MyNormTask.taskStep == 1)
		{
			this.stage1.gameObject.SetActive(false);
			this.stage2.gameObject.SetActive(true);
			for (int j = 0; j < this.Plants.Length; j++)
			{
				if (this.Watered(j))
				{
					SpriteRenderer spriteRenderer = this.Plants[j];
					spriteRenderer.material.SetFloat("_Desat", 0f);
					spriteRenderer.transform.localScale = Vector3.one;
				}
			}
			base.SetupInput(false);
			this.grabCanSubObject.SetActive(false);
			this.holdingCanSubObject.SetActive(false);
			this.waterPlantsSubObject.SetActive(true);
			foreach (SpriteRenderer playerMaterialColors2 in this.playerHandObjects)
			{
				PlayerControl.LocalPlayer.SetPlayerMaterialColors(playerMaterialColors2);
			}
		}
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0004F334 File Offset: 0x0004D534
	private void Update()
	{
		//this.c.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	ReInput.players.GetPlayer(0).GetButton(11);
		//	Vector3 position = this.selectorObject.transform.position;
		//	position.x = VirtualCursor.currentPosition.x;
		//	position.y = VirtualCursor.currentPosition.y;
		//	this.selectorObject.transform.position = position;
		//}
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0004F3AC File Offset: 0x0004D5AC
	public void PickWaterCan()
	{
		this.grabCanSubObject.SetActive(false);
		this.holdingCanSubObject.SetActive(true);
		this.WaterCan.enabled = false;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.CanGrabSound, false, 1f);
		}
		this.MyNormTask.NextStep();
		base.StartCoroutine(this.CoPickWaterCan());
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0004F413 File Offset: 0x0004D613
	private IEnumerator CoPickWaterCan()
	{
		this.FloatText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WaterPlantsGetCan, Array.Empty<object>());
		this.FloatText.gameObject.SetActive(true);
		yield return Effects.All(new IEnumerator[]
		{
			Effects.ColorFade(this.WaterCan.GetComponent<SpriteRenderer>(), Color.white, Palette.ClearWhite, 0.25f),
			Effects.Slide2D(this.FloatText.transform, this.WaterCan.transform.localPosition + new Vector3(0f, 0.1f, 0f), this.WaterCan.transform.localPosition + new Vector3(0f, 0.5f, 0f), 0.75f),
			Effects.ColorFade(this.FloatText, Color.white, Palette.ClearWhite, 0.75f)
		});
		yield return base.CoStartClose(0.75f);
		yield break;
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0004F424 File Offset: 0x0004D624
	public void WaterPlant(int num)
	{
		if (this.Watered(num))
		{
			return;
		}
		this.Watered(num, true);
		if (Enumerable.Range(0, 4).All(new Func<int, bool>(this.Watered)))
		{
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.WaterPlantSound, false, 1f);
		}
		base.StartCoroutine(this.CoGrowPlant(num));
		if (Controller.currentTouchType == Controller.TouchType.Joystick)
		{
			this.waterParticles.Play();
		}
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0004F4B8 File Offset: 0x0004D6B8
	private IEnumerator CoGrowPlant(int num)
	{
		SpriteRenderer plant = this.Plants[num];
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.PlantGrowSounds.Random<AudioClip>(), false, 1f).pitch = FloatRange.Next(0.9f, 1.1f);
		}
		for (float timer = 0f; timer < 1f; timer += Time.deltaTime)
		{
			float num2 = timer / 1f;
			plant.material.SetFloat("_Desat", (1f - num2) * 0.8f);
			plant.transform.localScale = new Vector3(0.8f, Mathf.Lerp(0.8f, 1.1f, num2), 1f);
			yield return null;
		}
		plant.material.SetFloat("_Desat", 0f);
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.PlantFinishedSounds.Random<AudioClip>(), false, 1f).pitch = FloatRange.Next(0.9f, 1.1f);
		}
		for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		{
			float num3 = timer / 0.1f;
			plant.transform.localScale = new Vector3(Mathf.Lerp(0.8f, 1.1f, num3), Mathf.Lerp(1.1f, 0.95f, num3), 1f);
			yield return null;
		}
		for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		{
			float num4 = timer / 0.1f;
			plant.transform.localScale = new Vector3(Mathf.Lerp(1.1f, 1f, num4), Mathf.Lerp(0.95f, 1f, num4), 1f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04000E59 RID: 3673
	public GameObject stage1;

	// Token: 0x04000E5A RID: 3674
	public GameObject stage2;

	// Token: 0x04000E5B RID: 3675
	public AudioClip CanGrabSound;

	// Token: 0x04000E5C RID: 3676
	public PassiveButton WaterCan;

	// Token: 0x04000E5D RID: 3677
	public SpriteRenderer[] Plants;

	// Token: 0x04000E5E RID: 3678
	public AudioClip WaterPlantSound;

	// Token: 0x04000E5F RID: 3679
	public AudioClip[] PlantGrowSounds;

	// Token: 0x04000E60 RID: 3680
	public AudioClip[] PlantFinishedSounds;

	// Token: 0x04000E61 RID: 3681
	public TextRenderer FloatText;

	// Token: 0x04000E62 RID: 3682
	public Transform[] Locations;

	// Token: 0x04000E63 RID: 3683
	public Transform selectorObject;

	// Token: 0x04000E64 RID: 3684
	public GameObject grabCanSubObject;

	// Token: 0x04000E65 RID: 3685
	public GameObject holdingCanSubObject;

	// Token: 0x04000E66 RID: 3686
	public GameObject waterPlantsSubObject;

	// Token: 0x04000E67 RID: 3687
	private Controller c = new Controller();

	// Token: 0x04000E68 RID: 3688
	public SpriteRenderer[] playerHandObjects;

	// Token: 0x04000E69 RID: 3689
	public ParticleSystem waterParticles;
}
