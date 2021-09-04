using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

// Token: 0x02000151 RID: 337
public class PlanetSurveillanceMinigame : Minigame
{
	// Token: 0x060007E9 RID: 2025 RVA: 0x00032F9C File Offset: 0x0003119C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		DestroyableSingleton<HudManager>.Instance.PlayerCam.Locked = true;
		RenderTexture temporary = RenderTexture.GetTemporary(330, 230, 16, GraphicsFormat.None);
		this.texture = temporary;
		this.Camera.targetTexture = temporary;
		this.ViewPort.material.SetTexture("_MainTex", temporary);
		this.survCameras = ShipStatus.Instance.AllCameras;
		this.Dots = new SpriteRenderer[this.survCameras.Length];
		for (int i = 0; i < this.Dots.Length; i++)
		{
			GameObject gameObject = new GameObject("Dot" + i.ToString(), new Type[]
			{
				typeof(SpriteRenderer)
			});
			gameObject.layer = base.gameObject.layer;
			gameObject.transform.SetParent(this.DotParent);
			SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
			component.sprite = this.DotDisabled;
			this.Dots[i] = component;
		}
		DotAligner.Align(this.DotParent, 0.25f, true);
		this.NextCamera(0);
		if (!PlayerControl.LocalPlayer.Data.IsDead)
		{
			ShipStatus.Instance.RpcRepairSystem(SystemTypes.Security, 1);
		}
		base.SetupInput(true);
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x000330D8 File Offset: 0x000312D8
	public void Update()
	{
		if (this.isStatic && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.isStatic = false;
			this.ViewPort.sharedMaterial = this.DefaultMaterial;
			this.ViewPort.material.SetTexture("_MainTex", this.texture);
			this.SabText.gameObject.SetActive(false);
			return;
		}
		if (!this.isStatic && PlayerTask.PlayerHasTaskOfType<HudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.isStatic = true;
			this.ViewPort.sharedMaterial = this.StaticMaterial;
			this.SabText.gameObject.SetActive(true);
		}
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0003317C File Offset: 0x0003137C
	public void NextCamera(int direction)
	{
		if (direction != 0 && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ChangeSound, false, 1f);
		}
		this.Dots[this.currentCamera].sprite = this.DotDisabled;
		this.currentCamera = (this.currentCamera + direction).Wrap(this.survCameras.Length);
		this.Dots[this.currentCamera].sprite = this.DotEnabled;
		SurvCamera survCamera = this.survCameras[this.currentCamera];
		this.Camera.transform.position = survCamera.transform.position + this.survCameras[this.currentCamera].Offset;
		this.LocationName.Text = ((survCamera.NewName > StringNames.ExitButton) ? DestroyableSingleton<TranslationController>.Instance.GetString(survCamera.NewName, Array.Empty<object>()) : survCamera.CamName);
		if (!PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			base.StartCoroutine(this.PulseStatic());
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0003327F File Offset: 0x0003147F
	private IEnumerator PulseStatic()
	{
		this.ViewPort.sharedMaterial = this.StaticMaterial;
		this.ViewPort.material.SetTexture("_MainTex", null);
		yield return Effects.Wait(0.2f);
		this.ViewPort.sharedMaterial = this.DefaultMaterial;
		this.ViewPort.material.SetTexture("_MainTex", this.texture);
		this.isStatic = false;
		yield break;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0003328E File Offset: 0x0003148E
	protected override IEnumerator CoAnimateOpen()
	{
		this.Viewables.SetActive(false);
		this.FillQuad.material.SetFloat("_Center", -5f);
		this.FillQuad.material.SetColor("_Color2", Color.clear);
		for (float timer = 0f; timer < 0.25f; timer += Time.deltaTime)
		{
			this.FillQuad.material.SetColor("_Color2", Color.Lerp(Color.clear, Color.black, timer / 0.25f));
			yield return null;
		}
		this.FillQuad.material.SetColor("_Color2", Color.black);
		this.Viewables.SetActive(true);
		for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		{
			this.FillQuad.material.SetFloat("_Center", Mathf.Lerp(-5f, 0f, timer / 0.1f));
			yield return null;
		}
		for (float timer = 0f; timer < 0.15f; timer += Time.deltaTime)
		{
			this.FillQuad.material.SetFloat("_Center", Mathf.Lerp(-3f, 0.4f, timer / 0.15f));
			yield return null;
		}
		this.FillQuad.material.SetFloat("_Center", 0.4f);
		yield break;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0003329D File Offset: 0x0003149D
	private IEnumerator CoAnimateClose()
	{
		for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		{
			this.FillQuad.material.SetFloat("_Center", Mathf.Lerp(0.4f, -5f, timer / 0.1f));
			yield return null;
		}
		this.Viewables.SetActive(false);
		for (float timer = 0f; timer < 0.3f; timer += Time.deltaTime)
		{
			this.FillQuad.material.SetColor("_Color2", Color.Lerp(Color.black, Color.clear, timer / 0.3f));
			yield return null;
		}
		this.FillQuad.material.SetColor("_Color2", Color.clear);
		yield break;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000332AC File Offset: 0x000314AC
	protected override IEnumerator CoDestroySelf()
	{
		DestroyableSingleton<HudManager>.Instance.PlayerCam.Locked = false;
		yield return this.CoAnimateClose();
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x000332BB File Offset: 0x000314BB
	public override void Close()
	{
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Security, 2);
		base.Close();
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x000332D0 File Offset: 0x000314D0
	public void OnDestroy()
	{
		this.texture.Release();
	}

	// Token: 0x0400092D RID: 2349
	public Camera Camera;

	// Token: 0x0400092E RID: 2350
	public GameObject Viewables;

	// Token: 0x0400092F RID: 2351
	public MeshRenderer ViewPort;

	// Token: 0x04000930 RID: 2352
	public TextRenderer LocationName;

	// Token: 0x04000931 RID: 2353
	public TextRenderer SabText;

	// Token: 0x04000932 RID: 2354
	private RenderTexture texture;

	// Token: 0x04000933 RID: 2355
	public MeshRenderer FillQuad;

	// Token: 0x04000934 RID: 2356
	public Material DefaultMaterial;

	// Token: 0x04000935 RID: 2357
	public Material StaticMaterial;

	// Token: 0x04000936 RID: 2358
	private bool isStatic;

	// Token: 0x04000937 RID: 2359
	private SurvCamera[] survCameras;

	// Token: 0x04000938 RID: 2360
	public Transform DotParent;

	// Token: 0x04000939 RID: 2361
	private SpriteRenderer[] Dots;

	// Token: 0x0400093A RID: 2362
	public Sprite DotEnabled;

	// Token: 0x0400093B RID: 2363
	public Sprite DotDisabled;

	// Token: 0x0400093C RID: 2364
	private int currentCamera;

	// Token: 0x0400093D RID: 2365
	public AudioClip ChangeSound;
}
