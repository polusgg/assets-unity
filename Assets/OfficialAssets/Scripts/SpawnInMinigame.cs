using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PowerTools;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class SpawnInMinigame : Minigame
{
	// Token: 0x06000103 RID: 259 RVA: 0x0000646C File Offset: 0x0000466C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		SpawnInMinigame.SpawnLocation[] array = this.Locations.ToArray<SpawnInMinigame.SpawnLocation>();
		array.Shuffle(0);
		array = (from s in array.Take(this.LocationButtons.Length)
		orderby s.Location.x, s.Location.y descending
		select s).ToArray<SpawnInMinigame.SpawnLocation>();
		for (int i = 0; i < this.LocationButtons.Length; i++)
		{
			PassiveButton passiveButton = this.LocationButtons[i];
			SpawnInMinigame.SpawnLocation pt = array[i];
			passiveButton.OnClick.AddListener(delegate()
			{
				this.SpawnAt(pt.Location);
			});
			passiveButton.GetComponent<SpriteAnim>().Stop();
			passiveButton.GetComponent<SpriteRenderer>().sprite = pt.Image;
			passiveButton.GetComponentInChildren<TextRenderer>().Text = DestroyableSingleton<TranslationController>.Instance.GetString(pt.Name, Array.Empty<object>());
			ButtonAnimRolloverHandler component = passiveButton.GetComponent<ButtonAnimRolloverHandler>();
			component.StaticOutImage = pt.Image;
			component.RolloverAnim = pt.Rollover;
			component.HoverSound = pt.RolloverSfx;
		}
		PlayerControl.LocalPlayer.gameObject.SetActive(false);
		PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(new Vector2(-25f, 40f));
		base.StartCoroutine(this.RunTimer());
		ControllerManager.Instance.OpenOverlayMenu(base.name, null, this.DefaultButtonSelected, this.ControllerSelectable, false);
		PlayerControl.HideCursorTemporarily();
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000661C File Offset: 0x0000481C
	public override void Close()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
		if (!this.gotButton)
		{
			this.LocationButtons.Random<PassiveButton>().ReceiveClickUp();
		}
		base.Close();
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000664C File Offset: 0x0000484C
	private IEnumerator RunTimer()
	{
		for (float time = 10f; time >= 0f; time -= Time.deltaTime)
		{
			this.Text.Text = "Time Remaining: " + Mathf.CeilToInt(time).ToString();
			yield return null;
		}
		this.LocationButtons.Random<PassiveButton>().ReceiveClickUp();
		yield break;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000665C File Offset: 0x0000485C
	private void SpawnAt(Vector3 spawnAt)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		this.gotButton = true;
		PlayerControl.LocalPlayer.gameObject.SetActive(true);
		base.StopAllCoroutines();
		PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(spawnAt);
		DestroyableSingleton<HudManager>.Instance.PlayerCam.SnapToTarget();
		this.Close();
	}

	// Token: 0x06000107 RID: 263 RVA: 0x000066B9 File Offset: 0x000048B9
	public IEnumerator WaitForFinish()
	{
		yield return null;
		while (this.amClosing == Minigame.CloseState.None)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x040000F7 RID: 247
	public SpawnInMinigame.SpawnLocation[] Locations;

	// Token: 0x040000F8 RID: 248
	public PassiveButton[] LocationButtons;

	// Token: 0x040000F9 RID: 249
	public TextRenderer Text;

	// Token: 0x040000FA RID: 250
	[Header("Console Controller Navigation")]
	public UiElement DefaultButtonSelected;

	// Token: 0x040000FB RID: 251
	public List<UiElement> ControllerSelectable;

	// Token: 0x040000FC RID: 252
	private bool gotButton;

	// Token: 0x020002D2 RID: 722
	[Serializable]
	public struct SpawnLocation
	{
		// Token: 0x0400165E RID: 5726
		public StringNames Name;

		// Token: 0x0400165F RID: 5727
		public Sprite Image;

		// Token: 0x04001660 RID: 5728
		public AnimationClip Rollover;

		// Token: 0x04001661 RID: 5729
		public AudioClip RolloverSfx;

		// Token: 0x04001662 RID: 5730
		public Vector3 Location;
	}
}
