using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
[RequireComponent(typeof(Camera))]
public class SaveIconCamera : DestroyableSingleton<SaveIconCamera>
{
	// Token: 0x0600091B RID: 2331 RVA: 0x0003C0E8 File Offset: 0x0003A2E8
	private void Start()
	{
		this.cam = base.GetComponent<Camera>();
		if (this.cam)
		{
			this.cam.enabled = false;
		}
		this.saveIconDummy.gameObject.SetActive(false);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0003C120 File Offset: 0x0003A320
	private void LateUpdate()
	{
		object obj = SaveIconCamera.lockObject;
		lock (obj)
		{
			if (SaveIconCamera.needsRender)
			{
				SaveIconCamera.renderedPNG = this.RenderSaveIconLocal();
				SaveIconCamera.needsRender = false;
			}
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0003C178 File Offset: 0x0003A378
	[ContextMenu("Test Render Icon")]
	private void TestIcon()
	{
		this.RenderSaveIconLocal();
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0003C184 File Offset: 0x0003A384
	public static byte[] RenderSaveIcon()
	{
		object obj = SaveIconCamera.lockObject;
		lock (obj)
		{
			SaveIconCamera.needsRender = true;
			goto IL_2E;
		}
		IL_24:
		Debug.Log("Waiting for render");
		IL_2E:
		if (!SaveIconCamera.needsRender)
		{
			return SaveIconCamera.renderedPNG;
		}
		goto IL_24;
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0003C1E0 File Offset: 0x0003A3E0
	private byte[] RenderSaveIconLocal()
	{
		this.saveIconDummy.gameObject.SetActive(true);
		this.cam.targetTexture = this.targetTexture;
		this.saveIconDummy.SetAppearanceFromSaveData();
		if (this.saveIconDummy.CurrentPet)
		{
			PlayerControl.SetPlayerMaterialColors((int)SaveManager.BodyColor, this.saveIconDummy.CurrentPet.rend);
			PetBehaviour currentPet = this.saveIconDummy.CurrentPet;
			currentPet.enabled = false;
			currentPet.transform.SetParent(this.saveIconDummy.transform);
			currentPet.transform.position = this.saveIconDummy.transform.position + new Vector3(-0.5f, 0f, -5f);
			SpriteRenderer[] componentsInChildren = currentPet.GetComponentsInChildren<SpriteRenderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = this.saveIconDummy.gameObject.layer;
			}
		}
		this.saveIconDummy.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
		this.saveIconDummy.nameText.RefreshMesh();
		this.cam.Render();
		this.saveIconDummy.gameObject.SetActive(false);
		RenderTexture.active = this.targetTexture;
		Texture2D texture2D = new Texture2D(this.targetTexture.width, this.targetTexture.height, (TextureFormat)3, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)this.targetTexture.width, (float)this.targetTexture.height), 0, 0);
		RenderTexture.active = null;
		return ImageConversion.EncodeToPNG(texture2D);
	}

	// Token: 0x04000A94 RID: 2708
	private Camera cam;

	// Token: 0x04000A95 RID: 2709
	public RenderTexture targetTexture;

	// Token: 0x04000A96 RID: 2710
	public PlayerControl saveIconDummy;

	// Token: 0x04000A97 RID: 2711
	private static object lockObject = 1;

	// Token: 0x04000A98 RID: 2712
	private static volatile bool needsRender = false;

	// Token: 0x04000A99 RID: 2713
	private static byte[] renderedPNG;
}
