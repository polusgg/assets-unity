using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020001C6 RID: 454
public class SurveillanceMinigame : Minigame
{
	// Token: 0x06000AB9 RID: 2745 RVA: 0x00043C78 File Offset: 0x00041E78
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//DestroyableSingleton<HudManager>.Instance.PlayerCam.Locked = true;
		//this.FilteredRooms = (from i in ShipStatus.Instance.AllRooms
		//where i.survCamera
		//select i).ToArray<PlainShipRoom>();
		//this.textures = new RenderTexture[this.FilteredRooms.Length];
		//for (int j = 0; j < this.FilteredRooms.Length; j++)
		//{
		//	PlainShipRoom plainShipRoom = this.FilteredRooms[j];
		//	Camera camera = Object.Instantiate<Camera>(this.CameraPrefab);
		//	camera.transform.SetParent(base.transform);
		//	camera.transform.position = plainShipRoom.transform.position + plainShipRoom.survCamera.Offset;
		//	camera.orthographicSize = plainShipRoom.survCamera.CamSize;
		//	RenderTexture temporary = RenderTexture.GetTemporary((int)(256f * plainShipRoom.survCamera.CamAspect), 256, 16, 0);
		//	this.textures[j] = temporary;
		//	camera.targetTexture = temporary;
		//	this.ViewPorts[j].material.SetTexture("_MainTex", temporary);
		//}
		//if (!PlayerControl.LocalPlayer.Data.IsDead)
		//{
		//	ShipStatus.Instance.RpcRepairSystem(SystemTypes.Security, 1);
		//}
		//base.SetupInput(true);
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00043DCC File Offset: 0x00041FCC
	public void Update()
	{
		if (this.isStatic && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.isStatic = false;
			for (int i = 0; i < this.ViewPorts.Length; i++)
			{
				this.ViewPorts[i].sharedMaterial = this.DefaultMaterial;
				this.ViewPorts[i].material.SetTexture("_MainTex", this.textures[i]);
				this.SabText[i].gameObject.SetActive(false);
			}
			return;
		}
		if (!this.isStatic && PlayerTask.PlayerHasTaskOfType<HudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.isStatic = true;
			for (int j = 0; j < this.ViewPorts.Length; j++)
			{
				this.ViewPorts[j].sharedMaterial = this.StaticMaterial;
				this.SabText[j].gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x00043EA1 File Offset: 0x000420A1
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

	// Token: 0x06000ABC RID: 2748 RVA: 0x00043EB0 File Offset: 0x000420B0
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

	// Token: 0x06000ABD RID: 2749 RVA: 0x00043EBF File Offset: 0x000420BF
	protected override IEnumerator CoDestroySelf()
	{
		DestroyableSingleton<HudManager>.Instance.PlayerCam.Locked = false;
		yield return this.CoAnimateClose();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x00043ECE File Offset: 0x000420CE
	public override void Close()
	{
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Security, 2);
		base.Close();
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x00043EE4 File Offset: 0x000420E4
	public void OnDestroy()
	{
		for (int i = 0; i < this.textures.Length; i++)
		{
			this.textures[i].Release();
		}
	}

	// Token: 0x04000C09 RID: 3081
	public Camera CameraPrefab;

	// Token: 0x04000C0A RID: 3082
	public GameObject Viewables;

	// Token: 0x04000C0B RID: 3083
	public MeshRenderer[] ViewPorts;

	// Token: 0x04000C0C RID: 3084
	public TextRenderer[] SabText;

	// Token: 0x04000C0D RID: 3085
	private PlainShipRoom[] FilteredRooms;

	// Token: 0x04000C0E RID: 3086
	private RenderTexture[] textures;

	// Token: 0x04000C0F RID: 3087
	public MeshRenderer FillQuad;

	// Token: 0x04000C10 RID: 3088
	public Material DefaultMaterial;

	// Token: 0x04000C11 RID: 3089
	public Material StaticMaterial;

	// Token: 0x04000C12 RID: 3090
	private bool isStatic;
}
