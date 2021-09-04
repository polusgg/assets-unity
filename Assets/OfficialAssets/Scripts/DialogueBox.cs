using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class DialogueBox : MonoBehaviour
{
	// Token: 0x06000483 RID: 1155 RVA: 0x0001D266 File Offset: 0x0001B466
	private void OnEnable()
	{
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton);
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001D27E File Offset: 0x0001B47E
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0001D290 File Offset: 0x0001B490
	public void Show(string dialogue)
	{
		this.target.Text = dialogue;
		if (Minigame.Instance)
		{
			Minigame.Instance.Close();
		}
		if (Minigame.Instance)
		{
			Minigame.Instance.Close();
		}
		PlayerControl.LocalPlayer.moveable = false;
		PlayerControl.LocalPlayer.NetTransform.Halt();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0001D2FB File Offset: 0x0001B4FB
	public void Hide()
	{
		base.gameObject.SetActive(false);
		if (!PlayerControl.LocalPlayer.inVent)
		{
			PlayerControl.LocalPlayer.moveable = true;
		}
		Camera.main.GetComponent<FollowerCamera>().Locked = false;
	}

	// Token: 0x0400054E RID: 1358
	public TextRenderer target;

	// Token: 0x0400054F RID: 1359
	[Header("Console Controller Navigation")]
	public UiElement BackButton;
}
