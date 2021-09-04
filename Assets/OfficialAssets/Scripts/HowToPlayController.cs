using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000AC RID: 172
public class HowToPlayController : MonoBehaviour
{
	// Token: 0x0600041B RID: 1051 RVA: 0x0001AF90 File Offset: 0x00019190
	public void Start()
	{
		this.Scenes[2] = this.PCMove;
		for (int i = 1; i < this.Scenes.Length; i++)
		{
			this.Scenes[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < this.DotParent.childCount; j++)
		{
			this.DotParent.GetChild(j).localScale = Vector3.one;
		}
		this.ChangeScene(0);
		ControllerManager.Instance.NewScene(base.name, this.CloseButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0001B02C File Offset: 0x0001922C
	public void Update()
	{
		//if (Input.GetKeyUp(27))
		//{
		//	this.Close();
		//}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0001B03D File Offset: 0x0001923D
	public void NextScene()
	{
		this.ChangeScene(1);
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0001B046 File Offset: 0x00019246
	public void PreviousScene()
	{
		this.ChangeScene(-1);
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0001B04F File Offset: 0x0001924F
	public void Close()
	{
		SceneManager.LoadScene("MainMenu");
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0001B05C File Offset: 0x0001925C
	private void ChangeScene(int del)
	{
		this.Scenes[this.SceneNum].gameObject.SetActive(false);
		this.DotParent.GetChild(this.SceneNum).localScale = Vector3.one;
		this.SceneNum = Mathf.Clamp(this.SceneNum + del, 0, this.Scenes.Length - 1);
		this.Scenes[this.SceneNum].gameObject.SetActive(true);
		this.DotParent.GetChild(this.SceneNum).localScale = new Vector3(1.5f, 1.5f, 1.5f);
		this.leftButton.gameObject.SetActive(this.SceneNum > 0);
		this.rightButton.gameObject.SetActive(this.SceneNum < this.Scenes.Length - 1);
	}

	// Token: 0x040004D7 RID: 1239
	public Transform DotParent;

	// Token: 0x040004D8 RID: 1240
	public SpriteRenderer leftButton;

	// Token: 0x040004D9 RID: 1241
	public SpriteRenderer rightButton;

	// Token: 0x040004DA RID: 1242
	[Header("Console Controller Navigation")]
	public UiElement CloseButton;

	// Token: 0x040004DB RID: 1243
	public UiElement DefaultButtonSelected;

	// Token: 0x040004DC RID: 1244
	public List<UiElement> ControllerSelectable;

	// Token: 0x040004DD RID: 1245
	public ConditionalSceneController PCMove;

	// Token: 0x040004DE RID: 1246
	public SceneController[] Scenes;

	// Token: 0x040004DF RID: 1247
	public int SceneNum;
}
