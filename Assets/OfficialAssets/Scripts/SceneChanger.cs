using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000063 RID: 99
public class SceneChanger : MonoBehaviour
{
	// Token: 0x060002A6 RID: 678 RVA: 0x00010ED4 File Offset: 0x0000F0D4
	public void Click()
	{
		this.ChangeScene();
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00010EDC File Offset: 0x0000F0DC
	private void ChangeScene()
	{
		Debug.Log("SceneChanger::ChangeScene to: " + this.TargetScene);
		this.BeforeSceneChange.Invoke();
		SceneChanger.ChangeScene(this.TargetScene);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00010F09 File Offset: 0x0000F109
	public static void ChangeScene(string target)
	{
		SceneManager.LoadScene(target);
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00010F11 File Offset: 0x0000F111
	public void ExitGame()
	{
		Application.Quit();
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00010F18 File Offset: 0x0000F118
	public void BeginLoadingScene()
	{
		SceneChanger.SceneManagerCallbacks.Init();
		if (this.loadOp == null)
		{
			Debug.Log("Begin async loading " + this.TargetScene);
			this.loadOp = SceneManager.LoadSceneAsync(this.TargetScene);
			this.loadOp.allowSceneActivation = false;
		}
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00010F64 File Offset: 0x0000F164
	public void AllowFinishLoadingScene()
	{
		if (this.loadOp != null)
		{
			Debug.Log(string.Concat(new string[]
			{
				"Allow async load for ",
				this.TargetScene,
				" to complete, currently at ",
				(this.loadOp.progress * 100f).ToString(),
				"%"
			}));
			this.loadOp.allowSceneActivation = true;
			this.loadOp = null;
		}
	}

	// Token: 0x0400030F RID: 783
	public string TargetScene;

	// Token: 0x04000310 RID: 784
	public bool disallowBasedOnSwitchParentalControls;

	// Token: 0x04000311 RID: 785
	public GameObject ConnectIcon;

	// Token: 0x04000312 RID: 786
	public Button.ButtonClickedEvent BeforeSceneChange;

	// Token: 0x04000313 RID: 787
	private AsyncOperation loadOp;

	// Token: 0x0200030D RID: 781
	public static class SceneManagerCallbacks
	{
		// Token: 0x06001509 RID: 5385 RVA: 0x000691A7 File Offset: 0x000673A7
		static SceneManagerCallbacks()
		{
			Debug.LogError("Hooked up SceneManager callback");
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(SceneChanger.SceneManagerCallbacks.SceneManager_activeSceneChanged);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000691C4 File Offset: 0x000673C4
		private static void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000691C6 File Offset: 0x000673C6
		public static void Init()
		{
		}
	}
}
